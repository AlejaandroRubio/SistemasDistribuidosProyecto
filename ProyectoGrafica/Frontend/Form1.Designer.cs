namespace Frontend
{
    partial class Form1
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
            this.ZTextBox = new System.Windows.Forms.TextBox();
            this.DropDownBox = new System.Windows.Forms.ComboBox();
            this.TextBoxIndex = new System.Windows.Forms.TextBox();
            this.Index = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DataPointsChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.LoadTxt = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataPointsChart)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 221);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.PostButton);
            // 
            // XTextBox
            // 
            this.XTextBox.Location = new System.Drawing.Point(38, 86);
            this.XTextBox.Name = "XTextBox";
            this.XTextBox.Size = new System.Drawing.Size(100, 20);
            this.XTextBox.TabIndex = 1;
            this.XTextBox.TextChanged += new System.EventHandler(this.XTextBox_TextChanged);
            // 
            // YTextBox
            // 
            this.YTextBox.Location = new System.Drawing.Point(38, 125);
            this.YTextBox.Name = "YTextBox";
            this.YTextBox.Size = new System.Drawing.Size(100, 20);
            this.YTextBox.TabIndex = 2;
            this.YTextBox.TextChanged += new System.EventHandler(this.YTextBox_TextChanged);
            // 
            // ZTextBox
            // 
            this.ZTextBox.Location = new System.Drawing.Point(38, 166);
            this.ZTextBox.Name = "ZTextBox";
            this.ZTextBox.Size = new System.Drawing.Size(100, 20);
            this.ZTextBox.TabIndex = 3;
            this.ZTextBox.TextChanged += new System.EventHandler(this.ZTextBox_TextChanged);
            // 
            // DropDownBox
            // 
            this.DropDownBox.FormattingEnabled = true;
            this.DropDownBox.Items.AddRange(new object[] {
            "POST",
            "PUT",
            "DELETE"});
            this.DropDownBox.Location = new System.Drawing.Point(17, 12);
            this.DropDownBox.Name = "DropDownBox";
            this.DropDownBox.Size = new System.Drawing.Size(121, 21);
            this.DropDownBox.TabIndex = 5;
            // 
            // TextBoxIndex
            // 
            this.TextBoxIndex.Location = new System.Drawing.Point(38, 49);
            this.TextBoxIndex.Name = "TextBoxIndex";
            this.TextBoxIndex.Size = new System.Drawing.Size(100, 20);
            this.TextBoxIndex.TabIndex = 6;
            this.TextBoxIndex.TextChanged += new System.EventHandler(this.TextBoxIndex_TextChanged);
            // 
            // Index
            // 
            this.Index.AutoSize = true;
            this.Index.Location = new System.Drawing.Point(-1, 52);
            this.Index.Name = "Index";
            this.Index.Size = new System.Drawing.Size(33, 13);
            this.Index.TabIndex = 7;
            this.Index.Text = "Index";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Z";
            // 
            // DataPointsChart
            // 
            chartArea1.Name = "ChartArea1";
            this.DataPointsChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.DataPointsChart.Legends.Add(legend1);
            this.DataPointsChart.Location = new System.Drawing.Point(163, 12);
            this.DataPointsChart.Name = "DataPointsChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Data Points";
            this.DataPointsChart.Series.Add(series1);
            this.DataPointsChart.Size = new System.Drawing.Size(398, 232);
            this.DataPointsChart.TabIndex = 11;
            this.DataPointsChart.Text = "DataPointsCharts";
            this.DataPointsChart.Click += new System.EventHandler(this.chart1_Click);
            // 
            // LoadTxt
            // 
            this.LoadTxt.Location = new System.Drawing.Point(15, 192);
            this.LoadTxt.Name = "LoadTxt";
            this.LoadTxt.Size = new System.Drawing.Size(75, 23);
            this.LoadTxt.TabIndex = 12;
            this.LoadTxt.Text = "Load Data";
            this.LoadTxt.UseVisualStyleBackColor = true;
            this.LoadTxt.Click += new System.EventHandler(this.LoadTxt_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 265);
            this.Controls.Add(this.LoadTxt);
            this.Controls.Add(this.DataPointsChart);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Index);
            this.Controls.Add(this.TextBoxIndex);
            this.Controls.Add(this.DropDownBox);
            this.Controls.Add(this.ZTextBox);
            this.Controls.Add(this.YTextBox);
            this.Controls.Add(this.XTextBox);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
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
        private System.Windows.Forms.TextBox ZTextBox;
        private System.Windows.Forms.ComboBox DropDownBox;
        private System.Windows.Forms.TextBox TextBoxIndex;
        private System.Windows.Forms.Label Index;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataVisualization.Charting.Chart DataPointsChart;
        private System.Windows.Forms.Button LoadTxt;
    }
}

