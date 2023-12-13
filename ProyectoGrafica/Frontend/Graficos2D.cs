using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Graficas.Models;
using Newtonsoft.Json;

namespace Frontend
{
    public partial class Graficos2D : Form
    {
        public Graficos2D()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            LoadSaveData();

        }

        //string path = "C:\\Program Files\\SistemasDistribuidosProyecto\\ProyectoGrafica\\Frontend\\DEBUGArchivosLocales\\Local.txt";
        //string path = "C:\\Users\\diego\\Desktop\\SistemasDistribuidosProyecto-FormulasIdea\\ProyectoGrafica\\Frontend\\DEBUGArchivosLocales\\Local2D.txt";

        static string relativePath = "..\\..\\DEBUGArchivosLocales\\Local2D.txt";
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);

        float x;
        float y;
        int index = -1;

        enum ActionType
        {
            post,
            put,
            delete,
        }

        private async void SendButton(object sender, EventArgs e)
        {
            switch (DropDownBox.SelectedIndex)
            {
                case 0:
                    await POST(null);
                    await GET(ActionType.post, index);
                    DefaultValues();
                    break;
                case 1:
                    if (index != -1)
                    {
                        await PUT();
                        await GET(ActionType.put, index);
                        DefaultValues();
                    }
                    break;
                case 2:
                    await DeleteByIndex();
                    await GET(ActionType.delete, index);
                    DefaultValues();
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

            /*
            GET(Action.post, index);

            DefaultValues();
            */
        }
        #endregion

        #region PUT
        private async Task PUT() {

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

            /*
            GET(Action.put, index);

            DefaultValues();
            */
        }
        #endregion

        #region DELETE
        private async Task DeleteByIndex() {


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

        }
        #endregion

        #region GET
        private async Task GET(ActionType action, int index) {

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44366/api/Graphic2D");

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var graphicJson = await response.Content.ReadAsStringAsync();

            List<GraphicData2D> graphicList = JsonConvert.DeserializeObject<List<GraphicData2D>>(graphicJson);

            UpdateChart(graphicList, action, index);

        }
        #endregion

        #region UpdateChart
        void UpdateChart(List<GraphicData2D> dataPoint, ActionType action, int index)
        {
            if (DataPointsChart.InvokeRequired)
            {
                DataPointsChart.Invoke(new Action(() => UpdateChart(dataPoint, action, index)));
                return;
            }

            switch (action)
            {
                case ActionType.post:
                    DataPointsChart.Series["Data Points"].Points.AddXY(dataPoint[dataPoint.Count-1].x, dataPoint[dataPoint.Count-1].y);
                    break;
                case ActionType.put:
                    DataPointsChart.Series["Data Points"].Points[index].SetValueXY(dataPoint[index +1].x, dataPoint[index +1].y);
                    break;
                case ActionType.delete:
                    if (index == -1)
                    {
                        DataPointsChart.Series["Data Points"].Points.Clear();
                        MessageBox.Show("TODOS LOS DATOS BORRADOS CON EXITO");
                    }
                        
                    else
                        DataPointsChart.Series["Data Points"].Points.RemoveAt(index);
                    break;
            }

            DataPointsChart.Invalidate();
        }

    #endregion

        #region Read From Local TXT
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
                        float.TryParse(GetNumbersFromLocalTXT(ref tempCharArr, j), out sanitizedX);
                        tempData.x = sanitizedX;
                    }

                    if (tempCharArr[j] == 121) // 121 = y
                    {
                        float.TryParse(GetNumbersFromLocalTXT(ref tempCharArr, j), out sanitizedY);
                        tempData.y = sanitizedY;
                        await Task.Delay(2);
                        await POST(tempData);
                    }

                    
                }
            }

            LoadSaveData();
            MessageBox.Show("Datos Cargados con exito");

        }
        #endregion

        # region Get Numbers From Local TXT

        string GetNumbersFromLocalTXT(ref char[] tJs, int i)
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

        #endregion

        #region LoadSaveData
        private async void LoadSaveData ()
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44366/api/Graphic2D");

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var graphicJson = await response.Content.ReadAsStringAsync();

            List<GraphicData2D> graphicList = JsonConvert.DeserializeObject<List<GraphicData2D>>(graphicJson);

            UpdateChartFromSaveData(graphicList);

        }
        void UpdateChartFromSaveData(List<GraphicData2D> graphicList) {

            DataPointsChart.Series["Data Points"].Points.Clear();

            for (int i = 1; i < graphicList.Count; i++)
            {
                DataPointsChart.Series["Data Points"].Points.AddXY(graphicList[i].x, graphicList[i].y);
                //MessageBox.Show("X: "+graphicList[i].x +" Y: "+ graphicList[i].y);
            }
        }

        private void LoadSaveDataButton_Click(object sender, EventArgs e)
        {
            LoadSaveData();
        }
        #endregion

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
            //TextBoxIndex.Text = "";
            //index = -1;
        }

        #endregion

    }
}
