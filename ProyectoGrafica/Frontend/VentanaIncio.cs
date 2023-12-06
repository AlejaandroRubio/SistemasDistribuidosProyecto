using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frontend
{
    public partial class VentanaIncio : Form
    {
        public VentanaIncio()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void buttonGraficas_3D_Click(object sender, EventArgs e)
        {
            Graficos3D Graficos3D = new Graficos3D();
            Graficos3D.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonGraficas_2D_Click(object sender, EventArgs e)
        {

            Graficos2D Graficos2D = new Graficos2D();
            Graficos2D.Show();

        }
    }
}
