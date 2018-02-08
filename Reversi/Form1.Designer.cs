namespace Reversi
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
            this.picMain = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblScoreWhite = new System.Windows.Forms.Label();
            this.lblScoreBlack = new System.Windows.Forms.Label();
            this.lblPlayerWhite = new System.Windows.Forms.Label();
            this.lblPlayerBlack = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picMain
            // 
            this.picMain.BackColor = System.Drawing.Color.Green;
            this.picMain.Location = new System.Drawing.Point(0, 0);
            this.picMain.Name = "picMain";
            this.picMain.Size = new System.Drawing.Size(801, 740);
            this.picMain.TabIndex = 0;
            this.picMain.TabStop = false;
            this.picMain.Paint += new System.Windows.Forms.PaintEventHandler(this.picMain_Paint);
            this.picMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picMain_MouseDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblScoreWhite);
            this.panel1.Controls.Add(this.lblScoreBlack);
            this.panel1.Controls.Add(this.lblPlayerWhite);
            this.panel1.Controls.Add(this.lblPlayerBlack);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 745);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(801, 58);
            this.panel1.TabIndex = 1;
            // 
            // lblScoreWhite
            // 
            this.lblScoreWhite.AutoSize = true;
            this.lblScoreWhite.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScoreWhite.Location = new System.Drawing.Point(552, 13);
            this.lblScoreWhite.Name = "lblScoreWhite";
            this.lblScoreWhite.Size = new System.Drawing.Size(0, 25);
            this.lblScoreWhite.TabIndex = 3;
            // 
            // lblScoreBlack
            // 
            this.lblScoreBlack.AutoSize = true;
            this.lblScoreBlack.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScoreBlack.Location = new System.Drawing.Point(144, 13);
            this.lblScoreBlack.Name = "lblScoreBlack";
            this.lblScoreBlack.Size = new System.Drawing.Size(0, 25);
            this.lblScoreBlack.TabIndex = 2;
            // 
            // lblPlayerWhite
            // 
            this.lblPlayerWhite.AutoSize = true;
            this.lblPlayerWhite.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerWhite.Location = new System.Drawing.Point(417, 13);
            this.lblPlayerWhite.Name = "lblPlayerWhite";
            this.lblPlayerWhite.Size = new System.Drawing.Size(129, 25);
            this.lblPlayerWhite.TabIndex = 1;
            this.lblPlayerWhite.Text = "Player White:";
            // 
            // lblPlayerBlack
            // 
            this.lblPlayerBlack.AutoSize = true;
            this.lblPlayerBlack.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerBlack.Location = new System.Drawing.Point(12, 13);
            this.lblPlayerBlack.Name = "lblPlayerBlack";
            this.lblPlayerBlack.Size = new System.Drawing.Size(126, 25);
            this.lblPlayerBlack.TabIndex = 0;
            this.lblPlayerBlack.Text = "Player Black:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(801, 803);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.picMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblPlayerBlack;
        private System.Windows.Forms.Label lblPlayerWhite;
        private System.Windows.Forms.Label lblScoreWhite;
        private System.Windows.Forms.Label lblScoreBlack;
    }
}

