namespace Frontend
{
    partial class Graficos2D
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.DropDownBox = new System.Windows.Forms.ComboBox();
            this.DataPointsChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Index = new System.Windows.Forms.Label();
            this.TextBoxIndex = new System.Windows.Forms.TextBox();
            this.YTextBox = new System.Windows.Forms.TextBox();
            this.XTextBox = new System.Windows.Forms.TextBox();
            this.LoadTxt = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.LoadSaveDataButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataPointsChart)).BeginInit();
            this.SuspendLayout();
            // 
            // DropDownBox
            // 
            this.DropDownBox.FormattingEnabled = true;
            this.DropDownBox.Items.AddRange(new object[] {
            "POST",
            "PUT",
            "DELETE"});
            this.DropDownBox.Location = new System.Drawing.Point(32, 13);
            this.DropDownBox.Margin = new System.Windows.Forms.Padding(4);
            this.DropDownBox.Name = "DropDownBox";
            this.DropDownBox.Size = new System.Drawing.Size(160, 24);
            this.DropDownBox.TabIndex = 6;
            // 
            // DataPointsChart
            // 
            chartArea1.Name = "ChartArea1";
            this.DataPointsChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.DataPointsChart.Legends.Add(legend1);
            this.DataPointsChart.Location = new System.Drawing.Point(218, 13);
            this.DataPointsChart.Margin = new System.Windows.Forms.Padding(4);
            this.DataPointsChart.Name = "DataPointsChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Data Points";
            this.DataPointsChart.Series.Add(series1);
            this.DataPointsChart.Size = new System.Drawing.Size(627, 343);
            this.DataPointsChart.TabIndex = 12;
            this.DataPointsChart.Text = "DataPointsCharts";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 152);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 16);
            this.label2.TabIndex = 18;
            this.label2.Text = "Y";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 104);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 16);
            this.label1.TabIndex = 17;
            this.label1.Text = "X";
            // 
            // Index
            // 
            this.Index.AutoSize = true;
            this.Index.Location = new System.Drawing.Point(8, 58);
            this.Index.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Index.Name = "Index";
            this.Index.Size = new System.Drawing.Size(39, 16);
            this.Index.TabIndex = 16;
            this.Index.Text = "Index";
            // 
            // TextBoxIndex
            // 
            this.TextBoxIndex.Location = new System.Drawing.Point(60, 54);
            this.TextBoxIndex.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxIndex.Name = "TextBoxIndex";
            this.TextBoxIndex.Size = new System.Drawing.Size(132, 22);
            this.TextBoxIndex.TabIndex = 15;
            this.TextBoxIndex.TextChanged += new System.EventHandler(this.TextBoxIndex_TextChanged);
            // 
            // YTextBox
            // 
            this.YTextBox.Location = new System.Drawing.Point(60, 148);
            this.YTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.YTextBox.Name = "YTextBox";
            this.YTextBox.Size = new System.Drawing.Size(132, 22);
            this.YTextBox.TabIndex = 14;
            this.YTextBox.TextChanged += new System.EventHandler(this.YTextBox_TextChanged);
            // 
            // XTextBox
            // 
            this.XTextBox.Location = new System.Drawing.Point(60, 100);
            this.XTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.XTextBox.Name = "XTextBox";
            this.XTextBox.Size = new System.Drawing.Size(132, 22);
            this.XTextBox.TabIndex = 13;
            this.XTextBox.TextChanged += new System.EventHandler(this.XTextBox_TextChanged);
            // 
            // LoadTxt
            // 
            this.LoadTxt.Location = new System.Drawing.Point(11, 302);
            this.LoadTxt.Margin = new System.Windows.Forms.Padding(4);
            this.LoadTxt.Name = "LoadTxt";
            this.LoadTxt.Size = new System.Drawing.Size(94, 52);
            this.LoadTxt.TabIndex = 20;
            this.LoadTxt.Text = "Load Local Data";
            this.LoadTxt.UseVisualStyleBackColor = true;
            this.LoadTxt.Click += new System.EventHandler(this.LoadTxt_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(60, 204);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 38);
            this.button1.TabIndex = 19;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.SendButton);
            // 
            // LoadSaveDataButton
            // 
            this.LoadSaveDataButton.Location = new System.Drawing.Point(116, 302);
            this.LoadSaveDataButton.Margin = new System.Windows.Forms.Padding(4);
            this.LoadSaveDataButton.Name = "LoadSaveDataButton";
            this.LoadSaveDataButton.Size = new System.Drawing.Size(94, 52);
            this.LoadSaveDataButton.TabIndex = 21;
            this.LoadSaveDataButton.Text = "Load Save Data";
            this.LoadSaveDataButton.UseVisualStyleBackColor = true;
            this.LoadSaveDataButton.Click += new System.EventHandler(this.LoadSaveDataButton_Click);
            // 
            // Graficos2D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 367);
            this.Controls.Add(this.LoadSaveDataButton);
            this.Controls.Add(this.LoadTxt);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Index);
            this.Controls.Add(this.TextBoxIndex);
            this.Controls.Add(this.YTextBox);
            this.Controls.Add(this.XTextBox);
            this.Controls.Add(this.DataPointsChart);
            this.Controls.Add(this.DropDownBox);
            this.Name = "Graficos2D";
            this.Text = "Graficos2D";
            ((System.ComponentModel.ISupportInitialize)(this.DataPointsChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox DropDownBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart DataPointsChart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Index;
        private System.Windows.Forms.TextBox TextBoxIndex;
        private System.Windows.Forms.TextBox YTextBox;
        private System.Windows.Forms.TextBox XTextBox;
        private System.Windows.Forms.Button LoadTxt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button LoadSaveDataButton;
    }
}