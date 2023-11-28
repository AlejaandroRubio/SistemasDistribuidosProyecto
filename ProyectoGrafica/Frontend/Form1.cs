using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Graficas.Models;
using Graficas.Services;
using Newtonsoft.Json;

namespace Frontend
{
    public partial class Form1 : Form
    {

        float x;
        float y;
        float z;

        int index = -1;

        public Form1()
        {
            InitializeComponent();
        }

        private async void POST()
        {

            var newGraphic = new GraphicsData();

            newGraphic.x = x;
            newGraphic.y = y;
            newGraphic.z = z;

            var newgraphicStr = JsonConvert.SerializeObject(newGraphic, Newtonsoft.Json.Formatting.Indented);

            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44366/api/Graphic");


            var content = new StringContent(newgraphicStr, null, "application/json");

            request.Content = content;

            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var graphicJson = await response.Content.ReadAsStringAsync();


            DefaultValues();

        }

        private async void PUT()
        {

            var putGraphic = new GraphicsData();
            putGraphic.x = x;
            putGraphic.y = y;
            putGraphic.z = z;

            var putGraphicStr = JsonConvert.SerializeObject(putGraphic, Newtonsoft.Json.Formatting.Indented);

            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Put, "https://localhost:44366/api/Graphic/" + index);

            var content = new StringContent(putGraphicStr, null, "application/json");
            
            request.Content = content;

            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var graphicJson = await response.Content.ReadAsStringAsync();

            DefaultValues();
        }

        

        private async void DeleteByIndex()
        {
           var putGraphic = new GraphicsData();
            putGraphic.x = x;
            putGraphic.y = y;
            putGraphic.z = z;

            var putGraphicStr = JsonConvert.SerializeObject(putGraphic, Newtonsoft.Json.Formatting.Indented);

            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Delete, "https://localhost:44366/api/Graphic/" + index);

            /* Susceptible a Borrarse */

            var content = new StringContent(putGraphicStr, null, "application/json");
            
            request.Content = content;

            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var graphicJson = await response.Content.ReadAsStringAsync();

            /* --------------------------------- */

            DefaultValues();
            
        }


        private async void PostButton(object sender, EventArgs e)
        {

            // MessageBox.Show(DropDownBox.SelectedIndex);

            switch(DropDownBox.SelectedIndex)
            {
                case 0:
                    POST();
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

        private async void GetButton_Click(object sender, EventArgs e)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44366/api/Graphic");

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var graphicJson = await response.Content.ReadAsStringAsync();
            MessageBox.Show(graphicJson);      
        }


      #region LEGACY
        
        private void XTextBox_TextChanged(object sender, EventArgs e)
        {
            float.TryParse(XTextBox.Text, out x);
            
        }

        private void YTextBox_TextChanged(object sender, EventArgs e)
        {
            float.TryParse(YTextBox.Text, out y);
        }

        private void ZTextBox_TextChanged(object sender, EventArgs e)
        {
            float.TryParse(ZTextBox.Text, out z);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void TextBoxIndex_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(TextBoxIndex.Text, out index);
        }



        void DefaultValues()
        {
            XTextBox.Text = "";
            YTextBox.Text = "";
            ZTextBox.Text = "";

            TextBoxIndex.Text = "";
            index = -1;
        }



    }
#endregion
}