namespace jogo
{
    partial class Form1
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
            this.Damas = new System.Windows.Forms.PictureBox();
            this.Nave = new System.Windows.Forms.PictureBox();
            this.Zumbi = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Damas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Zumbi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Damas
            // 
            this.Damas.Image = global::jogo.Properties.Resources.Captura_de_ecrã_2026_04_23_144047;
            this.Damas.Location = new System.Drawing.Point(588, 337);
            this.Damas.Name = "Damas";
            this.Damas.Size = new System.Drawing.Size(200, 89);
            this.Damas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Damas.TabIndex = 3;
            this.Damas.TabStop = false;
            this.Damas.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // Nave
            // 
            this.Nave.Image = global::jogo.Properties.Resources.Captura_de_ecrã_2026_04_23_144821;
            this.Nave.Location = new System.Drawing.Point(309, 337);
            this.Nave.Name = "Nave";
            this.Nave.Size = new System.Drawing.Size(200, 89);
            this.Nave.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Nave.TabIndex = 2;
            this.Nave.TabStop = false;
            this.Nave.Click += new System.EventHandler(this.Nave_Click);
            // 
            // Zumbi
            // 
            this.Zumbi.Image = global::jogo.Properties.Resources.Captura_de_ecrã_2026_04_23_143818;
            this.Zumbi.Location = new System.Drawing.Point(12, 337);
            this.Zumbi.Name = "Zumbi";
            this.Zumbi.Size = new System.Drawing.Size(200, 89);
            this.Zumbi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Zumbi.TabIndex = 1;
            this.Zumbi.TabStop = false;
            this.Zumbi.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::jogo.Properties.Resources.Captura_de_ecrã_2026_04_23_143635;
            this.pictureBox1.Location = new System.Drawing.Point(-1, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(806, 315);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Damas);
            this.Controls.Add(this.Nave);
            this.Controls.Add(this.Zumbi);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.Damas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Zumbi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox Zumbi;
        private System.Windows.Forms.PictureBox Nave;
        private System.Windows.Forms.PictureBox Damas;
    }
}