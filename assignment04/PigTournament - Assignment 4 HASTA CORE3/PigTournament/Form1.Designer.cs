namespace PigTournament
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
            this.StartTournamentButton = new System.Windows.Forms.Button();
            this.tournamentReport = new System.Windows.Forms.TextBox();
            this.rrgTextbox = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // StartTournamentButton
            // 
            this.StartTournamentButton.Location = new System.Drawing.Point(409, 113);
            this.StartTournamentButton.Margin = new System.Windows.Forms.Padding(4);
            this.StartTournamentButton.Name = "StartTournamentButton";
            this.StartTournamentButton.Size = new System.Drawing.Size(196, 28);
            this.StartTournamentButton.TabIndex = 0;
            this.StartTournamentButton.Text = "Start Tournament";
            this.StartTournamentButton.UseVisualStyleBackColor = true;
            this.StartTournamentButton.Click += new System.EventHandler(this.StartTournamentButton_Click);
            // 
            // tournamentReport
            // 
            this.tournamentReport.AcceptsReturn = true;
            this.tournamentReport.Location = new System.Drawing.Point(213, 199);
            this.tournamentReport.Margin = new System.Windows.Forms.Padding(4);
            this.tournamentReport.Multiline = true;
            this.tournamentReport.Name = "tournamentReport";
            this.tournamentReport.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tournamentReport.Size = new System.Drawing.Size(633, 287);
            this.tournamentReport.TabIndex = 1;
            // 
            // rrgTextbox
            // 
            this.rrgTextbox.Location = new System.Drawing.Point(718, 119);
            this.rrgTextbox.Mask = "99";
            this.rrgTextbox.Name = "rrgTextbox";
            this.rrgTextbox.Size = new System.Drawing.Size(42, 22);
            this.rrgTextbox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(635, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "RR Games";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rrgTextbox);
            this.Controls.Add(this.tournamentReport);
            this.Controls.Add(this.StartTournamentButton);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartTournamentButton;
        private System.Windows.Forms.TextBox tournamentReport;
        private System.Windows.Forms.MaskedTextBox rrgTextbox;
        private System.Windows.Forms.Label label1;
    }
}

