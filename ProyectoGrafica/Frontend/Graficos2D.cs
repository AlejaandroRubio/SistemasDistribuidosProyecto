using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Graficas.Models;
using Graficas.Services;
using Newtonsoft.Json;
using Graficas.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Frontend
{
    public partial class Graficos2D : Form
    {
        public Graficos2D()
        {
            InitializeComponent();
        }

        //string path = "C:\\Program Files\\SistemasDistribuidosProyecto\\ProyectoGrafica\\Frontend\\DEBUGArchivosLocales\\Local.txt";
        string path = "C:\\Users\\caoal_7ce87t7\\SistemasDistribuidosProyecto\\ProyectoGrafica\\Frontend\\DEBUGArchivosLocales\\Local2D.txt";

        float x;
        float y;
        int index = -1;

        enum Action
        {
            post,
            put,
            delete,
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (DropDownBox.SelectedIndex)
            {
                case 0:
                    POST(null);
                    break;
                case 1:
                    if (index != -1)
                        PUT();
                    break;
                case 2:
                    DeleteByIndex();
                    break;
            }

        }

        #region POST
        private async Task POST(GraphicData2D data) {

            var newGraphic = new GraphicData2D();

            if (data == null)
            {
                newGraphic.x = x;
                newGraphic.y = y;
            }
            else
            {
                newGraphic.x = data.x;
                newGraphic.y = data.y;
               
            }

            var newgraphicStr = JsonConvert.SerializeObject(newGraphic, Newtonsoft.Json.Formatting.Indented);

            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44366/api/Graphic2D");


            var content = new StringContent(newgraphicStr, null, "application/json");

            request.Content = content;

            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var graphicJson = await response.Content.ReadAsStringAsync();


            GET(Action.post, index);

            DefaultValues();

        }
        #endregion

        #region PUT
        private async void PUT() {

            var putGraphic = new GraphicData2D();
            putGraphic.x = x;
            putGraphic.y = y; 

            var putGraphicStr = JsonConvert.SerializeObject(putGraphic, Newtonsoft.Json.Formatting.Indented);

            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Put, "https://localhost:44366/api/Graphic2D/" + index);

            var content = new StringContent(putGraphicStr, null, "application/json");

            request.Content = content;

            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var graphicJson = await response.Content.ReadAsStringAsync();


            GET(Action.put, index);

            DefaultValues();

        }
        #endregion

        #region DELETE
        private async void DeleteByIndex() {


            var putGraphic = new GraphicData2D();
            putGraphic.x = x;
            putGraphic.y = y;

            var putGraphicStr = JsonConvert.SerializeObject(putGraphic, Newtonsoft.Json.Formatting.Indented);

            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Delete, "https://localhost:44366/api/Graphic2D/" + index);

            var content = new StringContent(putGraphicStr, null, "application/json");

            request.Content = content;

            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var graphicJson = await response.Content.ReadAsStringAsync();

            /* --------------------------------- */

            GET(Action.delete, index);

            DefaultValues();

        }
        #endregion

        #region GET
        private async void GET(Action action, int index) {

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44366/api/Graphic2D");

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var graphicJson = await response.Content.ReadAsStringAsync();

            UpdateChart(SanitazeString(graphicJson, action, index), action, index);

            MessageBox.Show(graphicJson);


        }
        #endregion

        #region SanatizeString

        GraphicData2D SanitazeString(string json, Action action, int index)
        {
            GraphicData2D tempData = new GraphicData2D();
            float sanitizedX = 0;
            float sanitizedY = 0;

            // Maximo 2410 Datos
            char[] tJs = json.ToCharArray();


            if (action == Action.post)
            {
                for (int i = json.Length - 1; i > 0; i--)
                {
        

                    if (tJs[i] == 121) // 121 = y
                    {
                        float.TryParse(GetNumbersFromJson(ref tJs, i), out sanitizedY);
                    }

                    if (tJs[i] == 120) // 120 = x
                    {
                        float.TryParse(GetNumbersFromJson(ref tJs, i), out sanitizedX);
                        goto exit;
                    }
                }
            }
            else if (action == Action.put)
            {
                #region PUT
                int tempIndexX = index;
                bool inTheRightIndex = false;

                for (int i = 0; i < json.Length; i++)
                {
                    if (tJs[i] == 120 && tempIndexX <= 0) // 120 = x
                    {
                        float.TryParse(GetNumbersFromJson(ref tJs, i), out sanitizedX);
                        inTheRightIndex = true;
                    }

                    if (tJs[i] == 121 && inTheRightIndex) // 121 = y
                    {
                        float.TryParse(GetNumbersFromJson(ref tJs, i), out sanitizedY);
                        goto exit;
                    }

                    

                    if (tempIndexX >= 0 && tJs[i] == 120) // 120 = x
                        tempIndexX--;
                }
                #endregion
            }
            else if (action == Action.delete)
            {

            }


        exit:
            tempData.x = sanitizedX;
            tempData.y = sanitizedY;


            /* MessageBox.Show("TempData  " +
                            " X = " + tempData.x +
                            " Y = " + tempData.y +);

            */
            return tempData;

        }

        #endregion


        #region UpdateChart
        void UpdateChart(GraphicData2D dataPoint, Action action, int index)
        {
            switch (action)
            {
                case Action.post:
                    DataPointsChart.Series["Data Points"].Points.AddXY(dataPoint.x, dataPoint.y);
                    break;
                case Action.put:
                    DataPointsChart.Series["Data Points"].Points[index].SetValueXY(dataPoint.x, dataPoint.y);
                    break;
                case Action.delete:
                    if (index == -1)
                        DataPointsChart.Series["Data Points"].Points.Clear();
                    else
                        DataPointsChart.Series["Data Points"].Points.RemoveAt(index);
                    break;
            }

            DataPointsChart.Invalidate();
        }
        #endregion

        #region LeerDeTXT
        private void LoadTxt_Click(object sender, EventArgs e)
        {

            ReadAndLoadTXT();
        }

        public async void ReadAndLoadTXT()
        {
            string[] tJs = File.ReadAllLines(path);

            float sanitizedX = 0;
            float sanitizedY = 0;

            char[] tempCharArr = { };

            for (int i = 0; i < tJs.Length; i++)
            {
                GraphicData2D tempData = new GraphicData2D();

                tempCharArr = tJs[i].ToCharArray();

                for (int j = 0; j < tempCharArr.Length - 1; j++)
                {
                    if (tempCharArr[j] == 120) // 120 = x
                    {
                        float.TryParse(GetNumbersFromJson(ref tempCharArr, j), out sanitizedX);
                        tempData.x = sanitizedX;
                    }

                    if (tempCharArr[j] == 121) // 121 = y
                    {
                        float.TryParse(GetNumbersFromJson(ref tempCharArr, j), out sanitizedY);
                        tempData.y = sanitizedY;
                        await Task.Delay(2);
                        await POST(tempData);
                    }

                    
                }
            }

        }
        #endregion

        string GetNumbersFromJson(ref char[] tJs, int i)
            {
                string tempValue = "";
                int j = 0;
                /* Idenitificar X, Y para conseguir el numero asignado a cada una */
                j = i + 3;
                while (true)
                {
                    if (tJs[j] == 46)
                        tempValue += ",";
                    else
                        tempValue += tJs[j];

                    if (j < tJs.Length - 1)
                        j++;
                    else
                        break;

                    if (tJs[j] == 44 || tJs[j] == 125)
                        break;
                }
                return tempValue;
            }



            #region Datos

            private void TextBoxIndex_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(TextBoxIndex.Text, out index);
        }

        private void XTextBox_TextChanged(object sender, EventArgs e)
        {
            float.TryParse(XTextBox.Text, out x);
        }

        private void YTextBox_TextChanged(object sender, EventArgs e)
        {
            float.TryParse(YTextBox.Text, out y);
        }


        void DefaultValues()
        {
            XTextBox.Text = "";
            YTextBox.Text = "";
            TextBoxIndex.Text = "";
            index = -1;
        }
        #endregion


    }
}
