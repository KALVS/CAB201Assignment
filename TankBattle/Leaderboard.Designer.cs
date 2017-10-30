namespace TankBattle
{
    partial class Leaderboard
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
            this.lblLeaderboardHeader = new System.Windows.Forms.Label();
            this.lbxFinalScores = new System.Windows.Forms.ListBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblLeaderboardHeader
            // 
            this.lblLeaderboardHeader.AutoSize = true;
            this.lblLeaderboardHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLeaderboardHeader.Location = new System.Drawing.Point(109, 9);
            this.lblLeaderboardHeader.Name = "lblLeaderboardHeader";
            this.lblLeaderboardHeader.Size = new System.Drawing.Size(206, 39);
            this.lblLeaderboardHeader.TabIndex = 0;
            this.lblLeaderboardHeader.Text = "Player won!";
            this.lblLeaderboardHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbxFinalScores
            // 
            this.lbxFinalScores.FormattingEnabled = true;
            this.lbxFinalScores.Location = new System.Drawing.Point(31, 59);
            this.lbxFinalScores.Name = "lbxFinalScores";
            this.lbxFinalScores.Size = new System.Drawing.Size(368, 147);
            this.lbxFinalScores.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(175, 212);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(99, 37);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // Leaderboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 261);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lbxFinalScores);
            this.Controls.Add(this.lblLeaderboardHeader);
            this.Name = "Leaderboard";
            this.Text = "Leaderboard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLeaderboardHeader;
        private System.Windows.Forms.ListBox lbxFinalScores;
        private System.Windows.Forms.Button btnClose;
    }
}