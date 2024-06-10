namespace BasketballStats
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
            this.teamDataGridView = new System.Windows.Forms.DataGridView();
            this.teamTabControl = new System.Windows.Forms.TabControl();
            this.teamTabPage = new System.Windows.Forms.TabPage();
            this.arenaName = new System.Windows.Forms.Label();
            this.teamName = new System.Windows.Forms.Label();
            this.teamGamesTabPage = new System.Windows.Forms.TabPage();
            this.arenaCapacityLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.teamDataGridView)).BeginInit();
            this.teamTabControl.SuspendLayout();
            this.teamTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // teamDataGridView
            // 
            this.teamDataGridView.AllowUserToAddRows = false;
            this.teamDataGridView.AllowUserToDeleteRows = false;
            this.teamDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.teamDataGridView.Location = new System.Drawing.Point(17, 16);
            this.teamDataGridView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.teamDataGridView.Name = "teamDataGridView";
            this.teamDataGridView.ReadOnly = true;
            this.teamDataGridView.RowHeadersWidth = 51;
            this.teamDataGridView.Size = new System.Drawing.Size(600, 679);
            this.teamDataGridView.TabIndex = 0;
            // 
            // teamTabControl
            // 
            this.teamTabControl.Controls.Add(this.teamTabPage);
            this.teamTabControl.Controls.Add(this.teamGamesTabPage);
            this.teamTabControl.Location = new System.Drawing.Point(656, 16);
            this.teamTabControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.teamTabControl.Name = "teamTabControl";
            this.teamTabControl.SelectedIndex = 0;
            this.teamTabControl.Size = new System.Drawing.Size(335, 679);
            this.teamTabControl.TabIndex = 1;
            // 
            // teamTabPage
            // 
            this.teamTabPage.Controls.Add(this.arenaCapacityLabel);
            this.teamTabPage.Controls.Add(this.arenaName);
            this.teamTabPage.Controls.Add(this.teamName);
            this.teamTabPage.Location = new System.Drawing.Point(4, 25);
            this.teamTabPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.teamTabPage.Name = "teamTabPage";
            this.teamTabPage.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.teamTabPage.Size = new System.Drawing.Size(327, 650);
            this.teamTabPage.TabIndex = 0;
            this.teamTabPage.Text = "Team";
            this.teamTabPage.UseVisualStyleBackColor = true;
            // 
            // arenaName
            // 
            this.arenaName.AutoSize = true;
            this.arenaName.Location = new System.Drawing.Point(28, 57);
            this.arenaName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.arenaName.Name = "arenaName";
            this.arenaName.Size = new System.Drawing.Size(82, 17);
            this.arenaName.TabIndex = 1;
            this.arenaName.Text = "arenaName";
            // 
            // teamName
            // 
            this.teamName.AutoSize = true;
            this.teamName.Location = new System.Drawing.Point(28, 20);
            this.teamName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.teamName.Name = "teamName";
            this.teamName.Size = new System.Drawing.Size(76, 17);
            this.teamName.TabIndex = 0;
            this.teamName.Text = "teamName";
            // 
            // teamGamesTabPage
            // 
            this.teamGamesTabPage.Location = new System.Drawing.Point(4, 25);
            this.teamGamesTabPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.teamGamesTabPage.Name = "teamGamesTabPage";
            this.teamGamesTabPage.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.teamGamesTabPage.Size = new System.Drawing.Size(327, 650);
            this.teamGamesTabPage.TabIndex = 1;
            this.teamGamesTabPage.Text = "Games";
            this.teamGamesTabPage.UseVisualStyleBackColor = true;
            // 
            // arenaCapacityLabel
            // 
            this.arenaCapacityLabel.AutoSize = true;
            this.arenaCapacityLabel.Location = new System.Drawing.Point(28, 105);
            this.arenaCapacityLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.arenaCapacityLabel.Name = "arenaCapacityLabel";
            this.arenaCapacityLabel.Size = new System.Drawing.Size(99, 17);
            this.arenaCapacityLabel.TabIndex = 2;
            this.arenaCapacityLabel.Text = "arenaCapacity";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 751);
            this.Controls.Add(this.teamTabControl);
            this.Controls.Add(this.teamDataGridView);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.teamDataGridView)).EndInit();
            this.teamTabControl.ResumeLayout(false);
            this.teamTabPage.ResumeLayout(false);
            this.teamTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView teamDataGridView;
        private System.Windows.Forms.TabControl teamTabControl;
        private System.Windows.Forms.TabPage teamTabPage;
        private System.Windows.Forms.TabPage teamGamesTabPage;
        private System.Windows.Forms.Label arenaName;
        private System.Windows.Forms.Label teamName;
        private System.Windows.Forms.Label arenaCapacityLabel;
    }
}

