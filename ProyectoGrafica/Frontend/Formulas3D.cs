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


        bool loadJustOnce = false;

        // Animaciones
        float amplitude;
        float frequency;
        bool animate;
        bool step;

        OpenGLGeneratedMesh receivedMesh;


        public Formulas3D()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;

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
        }


        private async Task GET()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44366/api/Graphic");

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var graphicJson = await response.Content.ReadAsStringAsync();

            receivedMesh = new OpenGLGeneratedMesh();
            receivedMesh = JsonConvert.DeserializeObject<OpenGLGeneratedMesh>(graphicJson);
        }

        
        void CallRenderer()
        {
            using (MainPipeline program = new MainPipeline(960, 540))
            {
                if (loadJustOnce)
                {
                    program.SetUpVerticesIndexAndTextures(receivedMesh.tempVertexList, receivedMesh.indices, receivedMesh.tempTexturesCoords , amplitude, frequency, animate, step);
                    loadJustOnce = false;
                }

                program.Run();
            }
        }


        private async void GenerateChartBtn(object sender, EventArgs e)
        {
            loadJustOnce = true;
            GraphicsDataRequest sendData = new GraphicsDataRequest();
            sendData.x = x;
            sendData.z = z;
            sendData.formula = formula;
            sendData.premadeFunctionIndex = comboBox1.SelectedIndex;
            
            await POST(sendData);
            await GET();

            CallRenderer();
        }

        #region Forms

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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            animate= checkBox1.Checked;
        }

        private void textBoxAmplitud_TextChanged(object sender, EventArgs e)
        {
            float.TryParse(textBoxAmplitud.Text, out amplitude);
        }

        private void textBoxFrecuencia_TextChanged(object sender, EventArgs e)
        {
            float.TryParse(textBoxFrecuencia.Text, out frequency);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            step = checkBox2.Checked;
        }

        #endregion
    }
}
