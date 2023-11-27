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
        
        public Form1()
        {
            InitializeComponent();
        }

        private async void PostButton(object sender, EventArgs e)
        {
            var newGraphic = new GraphicsData();

            float.TryParse(XTextBox.Text, out x);
            float.TryParse(YTextBox.Text, out y);
            float.TryParse(ZTextBox.Text, out z);

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


            XTextBox.Text = "";
            YTextBox.Text = "";
            ZTextBox.Text = "";

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

        }

        private void ZTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

       
        private void YTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
#endregion
}