using Frontend.Renderer;
using Graficas.Models;
using Newtonsoft.Json;
using OpenTK;
using ProyectoGrafica.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frontend
{
    public partial class Formulas3D : Form
    {
        int x;
        int z;
        string formula;

        string receivedData = "";

        public Formulas3D()
        {
            InitializeComponent();
        }

        private async Task POST(GraphicsDataRequest data)
        {

            var newgraphicStr = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);

            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44366/api/Graphic");


            var content = new StringContent(newgraphicStr, null, "application/json");

            request.Content = content;

            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var graphicJson = await response.Content.ReadAsStringAsync();


            receivedData = "";

            await GET();

            OpenGLGeneratedMesh reconstructedMesh = ReconstructMesh();

            CallRenderer(reconstructedMesh);

        }

        private async Task GET()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44366/api/Graphic");

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var graphicJson = await response.Content.ReadAsStringAsync();

            receivedData = graphicJson;

        }


        OpenGLGeneratedMesh ReconstructMesh()
        {
            // Acomodar el Json para poder analizarlo
            receivedData = receivedData.Replace(",", "|");
            receivedData = receivedData.Replace(".", ",");

            OpenGLGeneratedMesh mesh = new OpenGLGeneratedMesh();

            // Estados
            int whatImGenerating = 0;

            bool loopDone = true;
            bool analizingIndex = false;

            // Valores de los vertices
            float vTempX = 0;
            float vTempY = 0;
            float vTempZ = 0;

            float tTempU = 0;
            float tTempV = 0;


            for (int i = 0; i < receivedData.Length; i++)
            {
                char tempChar = receivedData[i];

                // PASO 1: Generar lista de vertices
                if (whatImGenerating == 0)
                {
                    if (tempChar == 88) // X
                    {
                        // Al hacerlo arriba estaba sobreescribiendo todos los vectores
                        string number = GetNumbersFromJson(ref receivedData, i, whatImGenerating);
                        float.TryParse(number, out vTempX);
                    }

                    if (tempChar == 89) // Y
                    {
                        string number = GetNumbersFromJson(ref receivedData, i, whatImGenerating);
                        float.TryParse(number, out vTempY);
                    }

                    if (tempChar == 90) // Z
                    {
                        string number = GetNumbersFromJson(ref receivedData, i, whatImGenerating);
                        float.TryParse(number, out vTempZ);

                        Vector3 tempVertex = new Vector3(vTempX, vTempY, vTempZ);

                        mesh.tempVertexList.Add(tempVertex);
                    }

                    if (tempChar == 93 && loopDone) // 93 = ]
                    {
                        whatImGenerating++;
                        loopDone = false;
                    }
                }

                // PASO 2: Recuperar Indices
                if (whatImGenerating == 1)
                {
                    // Aqui solo hay numeros enteros, todos estan separados por un | asi que pasarlo a la funcion y ya
                    if (tempChar != '|' && tempChar != '"' && tempChar != ':' && tempChar != '[' && tempChar != ']' && tempChar != '{' && tempChar != '}' && analizingIndex)
                    {
                        string number = GetNumbersFromJson(ref receivedData, i, whatImGenerating);
                        uint tempuInt = 0;
                        uint.TryParse(number, out tempuInt);

                        mesh.indices.Add(tempuInt);
                    }

                    // Asi sabe que hay que empezar con los indices
                    if (tempChar == 91)
                        analizingIndex = true;
                    

                    if (tempChar == 93 && loopDone) // 91 = [
                    {
                        whatImGenerating++;
                        loopDone = false;
                    }

                }

                // PASO 3: Recuperar las Coordenadas
                if (whatImGenerating == 2)
                {
                    if (tempChar == 88) // X
                    {
                        // Al hacerlo arriba estaba sobreescribiendo todos los vectores
                        string number = GetNumbersFromJson(ref receivedData, i, whatImGenerating);
                        float.TryParse(number, out tTempU);
                    }

                    if (tempChar == 89) // Y
                    {
                        string number = GetNumbersFromJson(ref receivedData, i, whatImGenerating);
                        float.TryParse(number, out tTempV);

                        Vector2 tempCoord = new Vector2(tTempU, tTempV);

                        mesh.tempTexturesCoords.Add(tempCoord);
                    }
                }

                loopDone = true;
            }

            return mesh;
        }

        // Tryparse solo acepta , por ejemplo 0,1
        string GetNumbersFromJson(ref string tJs, int i, int action)
        {
            StringBuilder tempValue = new StringBuilder();
            int j = 0;

            if (action == 0)        // Consiguiendo los Vertices
                j = i + 3;
            else if (action == 1)   // Consiguiendo los indices
                j = i;
            else if (action == 2)   // Consiguiendo las coordenadas
                j = i + 3;

            while (j < tJs.Length)
            {
               char currentChar = tJs[j];
               if (currentChar == 124 || currentChar == 93 || currentChar == 125) // 124 = '|' }
                   break;

               tempValue.Append(currentChar);
               j++;
            }

             return tempValue.ToString();
        }




        void CallRenderer(OpenGLGeneratedMesh reconstructedMesh)
        {
            using (MainPipeline program = new MainPipeline(1280, 720))
            {
                program.SetUpVerticesIndexAndTextures(reconstructedMesh.tempVertexList, reconstructedMesh.indices, reconstructedMesh.tempTexturesCoords);
                program.Run();
            }
        }



        private void GenerateChartBtn(object sender, EventArgs e)
        {
            GraphicsDataRequest sendData = new GraphicsDataRequest();
            sendData.x = x;
            sendData.z = z;
            sendData.formula = formula;
            sendData.premadeFunctionIndex = comboBox1.SelectedIndex;
            //MessageBox.Show(comboBox1.SelectedIndex.ToString());
            POST(sendData);
        }


        private void xTextBox(object sender, EventArgs e)
        {
            int.TryParse(textBox2.Text, out x);
        }

        private void zTextBox(object sender, EventArgs e)
        {
            int.TryParse(textBox3.Text, out z);
        }

        private void formulaTextbox(object sender, EventArgs e)
        {
            formula = textBox1.Text;
        }
    }
}
