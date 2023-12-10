namespace Frontend
{
    partial class VentanaIncio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VentanaIncio));
            this.buttonGraficas_3D = new System.Windows.Forms.Button();
            this.buttonGraficas_2D = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonGraficas_3D
            // 
            this.buttonGraficas_3D.Location = new System.Drawing.Point(54, 229);
            this.buttonGraficas_3D.Name = "buttonGraficas_3D";
            this.buttonGraficas_3D.Size = new System.Drawing.Size(145, 58);
            this.buttonGraficas_3D.TabIndex = 0;
            this.buttonGraficas_3D.Text = "Graficas 3D";
            this.buttonGraficas_3D.UseVisualStyleBackColor = true;
            this.buttonGraficas_3D.Click += new System.EventHandler(this.buttonGraficas_3D_Click);
            // 
            // buttonGraficas_2D
            // 
            this.buttonGraficas_2D.Location = new System.Drawing.Point(54, 149);
            this.buttonGraficas_2D.Name = "buttonGraficas_2D";
            this.buttonGraficas_2D.Size = new System.Drawing.Size(145, 58);
            this.buttonGraficas_2D.TabIndex = 1;
            this.buttonGraficas_2D.Text = "Graficas 2D";
            this.buttonGraficas_2D.UseVisualStyleBackColor = true;
            this.buttonGraficas_2D.Click += new System.EventHandler(this.buttonGraficas_2D_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(232, 131);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-16, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(287, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "----------------------------------------------------------------------";
            // 
            // VentanaIncio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 295);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonGraficas_2D);
            this.Controls.Add(this.buttonGraficas_3D);
            this.Name = "VentanaIncio";
            this.Text = "VentanaIncio";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGraficas_3D;
        private System.Windows.Forms.Button buttonGraficas_2D;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
    }
}