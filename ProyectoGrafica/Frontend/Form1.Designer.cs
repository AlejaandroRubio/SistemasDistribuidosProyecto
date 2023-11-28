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
            this.button1 = new System.Windows.Forms.Button();
            this.XTextBox = new System.Windows.Forms.TextBox();
            this.YTextBox = new System.Windows.Forms.TextBox();
            this.ZTextBox = new System.Windows.Forms.TextBox();
            this.GetButton = new System.Windows.Forms.Button();
            this.DropDownBox = new System.Windows.Forms.ComboBox();
            this.TextBoxIndex = new System.Windows.Forms.TextBox();
            this.Index = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(63, 168);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.PostButton);
            // 
            // XTextBox
            // 
            this.XTextBox.Location = new System.Drawing.Point(38, 51);
            this.XTextBox.Name = "XTextBox";
            this.XTextBox.Size = new System.Drawing.Size(100, 20);
            this.XTextBox.TabIndex = 1;
            this.XTextBox.TextChanged += new System.EventHandler(this.XTextBox_TextChanged);
            // 
            // YTextBox
            // 
            this.YTextBox.Location = new System.Drawing.Point(38, 90);
            this.YTextBox.Name = "YTextBox";
            this.YTextBox.Size = new System.Drawing.Size(100, 20);
            this.YTextBox.TabIndex = 2;
            this.YTextBox.TextChanged += new System.EventHandler(this.YTextBox_TextChanged);
            // 
            // ZTextBox
            // 
            this.ZTextBox.Location = new System.Drawing.Point(38, 131);
            this.ZTextBox.Name = "ZTextBox";
            this.ZTextBox.Size = new System.Drawing.Size(100, 20);
            this.ZTextBox.TabIndex = 3;
            this.ZTextBox.TextChanged += new System.EventHandler(this.ZTextBox_TextChanged);
            // 
            // GetButton
            // 
            this.GetButton.Location = new System.Drawing.Point(220, 168);
            this.GetButton.Name = "GetButton";
            this.GetButton.Size = new System.Drawing.Size(73, 23);
            this.GetButton.TabIndex = 4;
            this.GetButton.Text = "Load Data";
            this.GetButton.UseVisualStyleBackColor = true;
            this.GetButton.Click += new System.EventHandler(this.GetButton_Click);
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
            this.TextBoxIndex.Location = new System.Drawing.Point(193, 12);
            this.TextBoxIndex.Name = "TextBoxIndex";
            this.TextBoxIndex.Size = new System.Drawing.Size(100, 20);
            this.TextBoxIndex.TabIndex = 6;
            this.TextBoxIndex.TextChanged += new System.EventHandler(this.TextBoxIndex_TextChanged);
            // 
            // Index
            // 
            this.Index.AutoSize = true;
            this.Index.Location = new System.Drawing.Point(154, 15);
            this.Index.Name = "Index";
            this.Index.Size = new System.Drawing.Size(33, 13);
            this.Index.TabIndex = 7;
            this.Index.Text = "Index";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Z";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 216);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Index);
            this.Controls.Add(this.TextBoxIndex);
            this.Controls.Add(this.DropDownBox);
            this.Controls.Add(this.GetButton);
            this.Controls.Add(this.ZTextBox);
            this.Controls.Add(this.YTextBox);
            this.Controls.Add(this.XTextBox);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox XTextBox;
        private System.Windows.Forms.TextBox YTextBox;
        private System.Windows.Forms.TextBox ZTextBox;
        private System.Windows.Forms.Button GetButton;
        private System.Windows.Forms.ComboBox DropDownBox;
        private System.Windows.Forms.TextBox TextBoxIndex;
        private System.Windows.Forms.Label Index;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

