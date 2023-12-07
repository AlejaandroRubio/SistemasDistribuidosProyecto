namespace Frontend
{
    partial class Graficos3D
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.button1 = new System.Windows.Forms.Button();
            this.XTextBox = new System.Windows.Forms.TextBox();
            this.YTextBox = new System.Windows.Forms.TextBox();
            this.DropDownBox = new System.Windows.Forms.ComboBox();
            this.TextBoxIndex = new System.Windows.Forms.TextBox();
            this.Index = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DataPointsChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.LoadTxt = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.DropDownBoxFormula = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.DataPointsChart)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(110, 329);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.PostButton);
            // 
            // XTextBox
            // 
            this.XTextBox.Location = new System.Drawing.Point(51, 106);
            this.XTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.XTextBox.Name = "XTextBox";
            this.XTextBox.Size = new System.Drawing.Size(132, 22);
            this.XTextBox.TabIndex = 1;
            this.XTextBox.TextChanged += new System.EventHandler(this.XTextBox_TextChanged);
            // 
            // YTextBox
            // 
            this.YTextBox.Location = new System.Drawing.Point(51, 154);
            this.YTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.YTextBox.Name = "YTextBox";
            this.YTextBox.Size = new System.Drawing.Size(132, 22);
            this.YTextBox.TabIndex = 2;
            this.YTextBox.TextChanged += new System.EventHandler(this.YTextBox_TextChanged);
            // 
            // DropDownBox
            // 
            this.DropDownBox.FormattingEnabled = true;
            this.DropDownBox.Items.AddRange(new object[] {
            "POST",
            "PUT",
            "DELETE"});
            this.DropDownBox.Location = new System.Drawing.Point(23, 15);
            this.DropDownBox.Margin = new System.Windows.Forms.Padding(4);
            this.DropDownBox.Name = "DropDownBox";
            this.DropDownBox.Size = new System.Drawing.Size(160, 24);
            this.DropDownBox.TabIndex = 5;
            // 
            // TextBoxIndex
            // 
            this.TextBoxIndex.Location = new System.Drawing.Point(51, 60);
            this.TextBoxIndex.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxIndex.Name = "TextBoxIndex";
            this.TextBoxIndex.Size = new System.Drawing.Size(132, 22);
            this.TextBoxIndex.TabIndex = 6;
            this.TextBoxIndex.TextChanged += new System.EventHandler(this.TextBoxIndex_TextChanged);
            // 
            // Index
            // 
            this.Index.AutoSize = true;
            this.Index.Location = new System.Drawing.Point(-1, 64);
            this.Index.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Index.Name = "Index";
            this.Index.Size = new System.Drawing.Size(39, 16);
            this.Index.TabIndex = 7;
            this.Index.Text = "Index";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 110);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 158);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Y";
            // 
            // DataPointsChart
            // 
            chartArea1.Name = "ChartArea1";
            this.DataPointsChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.DataPointsChart.Legends.Add(legend1);
            this.DataPointsChart.Location = new System.Drawing.Point(250, 15);
            this.DataPointsChart.Margin = new System.Windows.Forms.Padding(4);
            this.DataPointsChart.Name = "DataPointsChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Data Points";
            this.DataPointsChart.Series.Add(series1);
            this.DataPointsChart.Size = new System.Drawing.Size(624, 383);
            this.DataPointsChart.TabIndex = 11;
            this.DataPointsChart.Text = "DataPointsCharts";
            this.DataPointsChart.Click += new System.EventHandler(this.chart1_Click);
            // 
            // LoadTxt
            // 
            this.LoadTxt.Location = new System.Drawing.Point(2, 329);
            this.LoadTxt.Margin = new System.Windows.Forms.Padding(4);
            this.LoadTxt.Name = "LoadTxt";
            this.LoadTxt.Size = new System.Drawing.Size(100, 28);
            this.LoadTxt.TabIndex = 12;
            this.LoadTxt.Text = "Load Data";
            this.LoadTxt.UseVisualStyleBackColor = true;
            this.LoadTxt.Click += new System.EventHandler(this.LoadTxt_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "Formula";
            // 
            // DropDownBoxFormula
            // 
            this.DropDownBoxFormula.FormattingEnabled = true;
            this.DropDownBoxFormula.Items.AddRange(new object[] {
            "x^2 * y/2",
            "Sin(x) + Cos(y)",
            "sqrt(x^2+y^2)",
            "X^2 + y^3 * x + y^5"});
            this.DropDownBoxFormula.Location = new System.Drawing.Point(2, 203);
            this.DropDownBoxFormula.Name = "DropDownBoxFormula";
            this.DropDownBoxFormula.Size = new System.Drawing.Size(181, 24);
            this.DropDownBoxFormula.TabIndex = 15;
            // 
            // Graficos3D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 406);
            this.Controls.Add(this.DropDownBoxFormula);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LoadTxt);
            this.Controls.Add(this.DataPointsChart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Index);
            this.Controls.Add(this.TextBoxIndex);
            this.Controls.Add(this.DropDownBox);
            this.Controls.Add(this.YTextBox);
            this.Controls.Add(this.XTextBox);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Graficos3D";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataPointsChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox XTextBox;
        private System.Windows.Forms.TextBox YTextBox;
        private System.Windows.Forms.ComboBox DropDownBox;
        private System.Windows.Forms.TextBox TextBoxIndex;
        private System.Windows.Forms.Label Index;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataVisualization.Charting.Chart DataPointsChart;
        private System.Windows.Forms.Button LoadTxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox DropDownBoxFormula;
    }
}

