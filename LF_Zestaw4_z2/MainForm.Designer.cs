namespace LF_Zestaw4_z2
{
    partial class MainForm
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
            this.buttonDice = new System.Windows.Forms.Button();
            this.buttonDuel = new System.Windows.Forms.Button();
            this.buttonLog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonDice
            // 
            this.buttonDice.Location = new System.Drawing.Point(12, 12);
            this.buttonDice.Name = "buttonDice";
            this.buttonDice.Size = new System.Drawing.Size(75, 23);
            this.buttonDice.TabIndex = 0;
            this.buttonDice.Text = "Dice Poker";
            this.buttonDice.UseVisualStyleBackColor = true;
            this.buttonDice.Click += new System.EventHandler(this.buttonDice_Click);
            // 
            // buttonDuel
            // 
            this.buttonDuel.Location = new System.Drawing.Point(93, 12);
            this.buttonDuel.Name = "buttonDuel";
            this.buttonDuel.Size = new System.Drawing.Size(75, 23);
            this.buttonDuel.TabIndex = 1;
            this.buttonDuel.Text = "Arena Duel";
            this.buttonDuel.UseVisualStyleBackColor = true;
            this.buttonDuel.Click += new System.EventHandler(this.buttonDuel_Click);
            // 
            // buttonLog
            // 
            this.buttonLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonLog.Location = new System.Drawing.Point(59, 41);
            this.buttonLog.Name = "buttonLog";
            this.buttonLog.Size = new System.Drawing.Size(61, 20);
            this.buttonLog.TabIndex = 2;
            this.buttonLog.Text = "Game Logs";
            this.buttonLog.UseVisualStyleBackColor = true;
            this.buttonLog.Click += new System.EventHandler(this.buttonLog_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(180, 73);
            this.Controls.Add(this.buttonLog);
            this.Controls.Add(this.buttonDuel);
            this.Controls.Add(this.buttonDice);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Gry";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonDice;
        private System.Windows.Forms.Button buttonDuel;
        private System.Windows.Forms.Button buttonLog;
    }
}

