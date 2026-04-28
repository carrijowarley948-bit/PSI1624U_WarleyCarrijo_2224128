namespace jogo
{
    partial class PongJogo
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
            this.components = new System.ComponentModel.Container();
            this.Player = new System.Windows.Forms.PictureBox();
            this.ball = new System.Windows.Forms.PictureBox();
            this.computer = new System.Windows.Forms.PictureBox();
            this.GameTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Player)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ball)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.computer)).BeginInit();
            this.SuspendLayout();
            // 
            // Player
            // 
            this.Player.Image = global::jogo.Properties.Resources.player;
            this.Player.Location = new System.Drawing.Point(12, 146);
            this.Player.Name = "Player";
            this.Player.Size = new System.Drawing.Size(30, 120);
            this.Player.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Player.TabIndex = 0;
            this.Player.TabStop = false;
            // 
            // ball
            // 
            this.ball.Image = global::jogo.Properties.Resources.ball;
            this.ball.Location = new System.Drawing.Point(382, 197);
            this.ball.Name = "ball";
            this.ball.Size = new System.Drawing.Size(30, 30);
            this.ball.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ball.TabIndex = 1;
            this.ball.TabStop = false;
            // 
            // computer
            // 
            this.computer.Image = global::jogo.Properties.Resources.computer;
            this.computer.Location = new System.Drawing.Point(758, 146);
            this.computer.Name = "computer";
            this.computer.Size = new System.Drawing.Size(30, 120);
            this.computer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.computer.TabIndex = 2;
            this.computer.TabStop = false;
            // 
            // GameTimer
            // 
            this.GameTimer.Enabled = true;
            this.GameTimer.Interval = 20;
            this.GameTimer.Tick += new System.EventHandler(this.GameTimerEvent);
            // 
            // PongJogo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.computer);
            this.Controls.Add(this.ball);
            this.Controls.Add(this.Player);
            this.DoubleBuffered = true;
            this.Name = "PongJogo";
            this.Text = "PongJogo";
            this.Load += new System.EventHandler(this.PongJogo_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyIsDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyIsUp);
            ((System.ComponentModel.ISupportInitialize)(this.Player)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ball)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.computer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Player;
        private System.Windows.Forms.PictureBox ball;
        private System.Windows.Forms.PictureBox computer;
        private System.Windows.Forms.Timer GameTimer;
    }
}