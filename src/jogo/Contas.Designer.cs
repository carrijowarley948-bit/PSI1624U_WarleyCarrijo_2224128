namespace jogo
{
    partial class Contas
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Registrar = new System.Windows.Forms.PictureBox();
            this.Login = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Registrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Login)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox4.Image = global::jogo.Properties.Resources.Captura_de_ecrã_2026_04_30_154834;
            this.pictureBox4.Location = new System.Drawing.Point(494, 198);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(100, 297);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox4.TabIndex = 12;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox3.Image = global::jogo.Properties.Resources.Captura_de_ecrã_2026_04_23_143141;
            this.pictureBox3.Location = new System.Drawing.Point(1, 150);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(1063, 85);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 11;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::jogo.Properties.Resources.Captura_de_ecrã_2026_04_30_160113;
            this.pictureBox1.Location = new System.Drawing.Point(37, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(403, 101);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // Registrar
            // 
            this.Registrar.Image = global::jogo.Properties.Resources.Captura_de_ecrã_2026_04_30_160411;
            this.Registrar.Location = new System.Drawing.Point(632, 314);
            this.Registrar.Name = "Registrar";
            this.Registrar.Size = new System.Drawing.Size(403, 80);
            this.Registrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Registrar.TabIndex = 1;
            this.Registrar.TabStop = false;
            this.Registrar.Click += new System.EventHandler(this.Registrar_Click);
            // 
            // Login
            // 
            this.Login.Image = global::jogo.Properties.Resources.Captura_de_ecrã_2026_04_30_160239;
            this.Login.Location = new System.Drawing.Point(37, 314);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(403, 80);
            this.Login.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Login.TabIndex = 0;
            this.Login.TabStop = false;
            this.Login.Click += new System.EventHandler(this.Login_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::jogo.Properties.Resources.Captura_de_ecrã_2026_04_30_155820;
            this.pictureBox2.Location = new System.Drawing.Point(632, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(403, 101);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // Contas
            // 
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1065, 495);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Registrar);
            this.Controls.Add(this.Login);
            this.Name = "Contas";
            this.Load += new System.EventHandler(this.Contas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Registrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Login)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.PictureBox Login;
        private System.Windows.Forms.PictureBox Registrar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}