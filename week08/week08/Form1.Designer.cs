namespace week08
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
            this.components = new System.ComponentModel.Container();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.btnBallColor = new System.Windows.Forms.Button();
            this.btnSelectBall = new System.Windows.Forms.Button();
            this.btnSelectCar = new System.Windows.Forms.Button();
            this.lblNext = new System.Windows.Forms.Label();
            this.createTimer = new System.Windows.Forms.Timer(this.components);
            this.conveyorTimer = new System.Windows.Forms.Timer(this.components);
            this.btnSelectPresent = new System.Windows.Forms.Button();
            this.btnPresentColor = new System.Windows.Forms.Button();
            this.btnSelectPresent2 = new System.Windows.Forms.Button();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainPanel.Controls.Add(this.btnSelectPresent2);
            this.mainPanel.Controls.Add(this.btnPresentColor);
            this.mainPanel.Controls.Add(this.btnSelectPresent);
            this.mainPanel.Controls.Add(this.btnBallColor);
            this.mainPanel.Controls.Add(this.btnSelectBall);
            this.mainPanel.Controls.Add(this.btnSelectCar);
            this.mainPanel.Controls.Add(this.lblNext);
            this.mainPanel.Location = new System.Drawing.Point(4, 3);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(764, 445);
            this.mainPanel.TabIndex = 0;
            // 
            // btnBallColor
            // 
            this.btnBallColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnBallColor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnBallColor.Location = new System.Drawing.Point(111, 74);
            this.btnBallColor.Name = "btnBallColor";
            this.btnBallColor.Size = new System.Drawing.Size(88, 27);
            this.btnBallColor.TabIndex = 3;
            this.btnBallColor.UseVisualStyleBackColor = false;
            // 
            // btnSelectBall
            // 
            this.btnSelectBall.Location = new System.Drawing.Point(112, 9);
            this.btnSelectBall.Name = "btnSelectBall";
            this.btnSelectBall.Size = new System.Drawing.Size(87, 59);
            this.btnSelectBall.TabIndex = 2;
            this.btnSelectBall.Text = "Ball";
            this.btnSelectBall.UseVisualStyleBackColor = true;
            // 
            // btnSelectCar
            // 
            this.btnSelectCar.Location = new System.Drawing.Point(8, 9);
            this.btnSelectCar.Name = "btnSelectCar";
            this.btnSelectCar.Size = new System.Drawing.Size(88, 59);
            this.btnSelectCar.TabIndex = 1;
            this.btnSelectCar.Text = "Car";
            this.btnSelectCar.UseVisualStyleBackColor = true;
            // 
            // lblNext
            // 
            this.lblNext.AutoSize = true;
            this.lblNext.Location = new System.Drawing.Point(350, 9);
            this.lblNext.Name = "lblNext";
            this.lblNext.Size = new System.Drawing.Size(85, 17);
            this.lblNext.TabIndex = 0;
            this.lblNext.Text = "Coming next";
            // 
            // createTimer
            // 
            this.createTimer.Enabled = true;
            this.createTimer.Interval = 3000;
            // 
            // conveyorTimer
            // 
            this.conveyorTimer.Enabled = true;
            this.conveyorTimer.Interval = 10;
            // 
            // btnSelectPresent
            // 
            this.btnSelectPresent.Location = new System.Drawing.Point(205, 9);
            this.btnSelectPresent.Name = "btnSelectPresent";
            this.btnSelectPresent.Size = new System.Drawing.Size(87, 59);
            this.btnSelectPresent.TabIndex = 4;
            this.btnSelectPresent.Text = "Present";
            this.btnSelectPresent.UseVisualStyleBackColor = true;
            // 
            // btnPresentColor
            // 
            this.btnPresentColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnPresentColor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnPresentColor.Location = new System.Drawing.Point(204, 74);
            this.btnPresentColor.Name = "btnPresentColor";
            this.btnPresentColor.Size = new System.Drawing.Size(88, 27);
            this.btnPresentColor.TabIndex = 5;
            this.btnPresentColor.UseVisualStyleBackColor = false;
            this.btnPresentColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnSelectPresent2
            // 
            this.btnSelectPresent2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnSelectPresent2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnSelectPresent2.Location = new System.Drawing.Point(204, 107);
            this.btnSelectPresent2.Name = "btnSelectPresent2";
            this.btnSelectPresent2.Size = new System.Drawing.Size(88, 27);
            this.btnSelectPresent2.TabIndex = 6;
            this.btnSelectPresent2.UseVisualStyleBackColor = false;
            this.btnSelectPresent2.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mainPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Timer createTimer;
        private System.Windows.Forms.Timer conveyorTimer;
        private System.Windows.Forms.Button btnSelectBall;
        private System.Windows.Forms.Button btnSelectCar;
        private System.Windows.Forms.Label lblNext;
        private System.Windows.Forms.Button btnBallColor;
        private System.Windows.Forms.Button btnSelectPresent2;
        private System.Windows.Forms.Button btnPresentColor;
        private System.Windows.Forms.Button btnSelectPresent;
    }
}

