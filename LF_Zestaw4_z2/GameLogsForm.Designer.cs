namespace LF_Zestaw4_z2
{
    partial class GameLogsForm
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
            this.boxLogs = new System.Windows.Forms.TextBox();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // boxLogs
            // 
            this.boxLogs.BackColor = System.Drawing.Color.White;
            this.boxLogs.Location = new System.Drawing.Point(12, 41);
            this.boxLogs.MaxLength = 16777216;
            this.boxLogs.Multiline = true;
            this.boxLogs.Name = "boxLogs";
            this.boxLogs.ReadOnly = true;
            this.boxLogs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.boxLogs.Size = new System.Drawing.Size(316, 288);
            this.boxLogs.TabIndex = 0;
            this.boxLogs.WordWrap = false;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(12, 12);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(63, 23);
            this.buttonRefresh.TabIndex = 1;
            this.buttonRefresh.Text = "Odśwież";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(81, 12);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(63, 23);
            this.buttonClear.TabIndex = 2;
            this.buttonClear.Text = "Wyczyść";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // GameLogsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 341);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.boxLogs);
            this.Name = "GameLogsForm";
            this.Text = "Game Logs";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox boxLogs;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonClear;
    }
}