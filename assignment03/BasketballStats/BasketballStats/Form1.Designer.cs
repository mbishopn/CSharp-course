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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.PPGLossesLabel = new System.Windows.Forms.Label();
            this.PPGWinsLabel = new System.Windows.Forms.Label();
            this.regularSeasonPPGLabel = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.OppPPGLossesLabel = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.OppPPGWinsLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.OppRegularSeasonPPGLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.postSeasonRecordLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.preSeasonRecordLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.regularSeasonRecordLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.arenaName = new System.Windows.Forms.Label();
            this.teamName = new System.Windows.Forms.Label();
            this.teamGamesTabPage = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.resultsCB = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.locationCB = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.opponentComboBox = new System.Windows.Forms.ComboBox();
            this.gameGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.teamDataGridView)).BeginInit();
            this.teamTabControl.SuspendLayout();
            this.teamTabPage.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.teamGamesTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // teamDataGridView
            // 
            this.teamDataGridView.AllowUserToAddRows = false;
            this.teamDataGridView.AllowUserToDeleteRows = false;
            this.teamDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.teamDataGridView.Location = new System.Drawing.Point(13, 13);
            this.teamDataGridView.Name = "teamDataGridView";
            this.teamDataGridView.ReadOnly = true;
            this.teamDataGridView.RowHeadersWidth = 51;
            this.teamDataGridView.Size = new System.Drawing.Size(450, 552);
            this.teamDataGridView.TabIndex = 0;
            // 
            // teamTabControl
            // 
            this.teamTabControl.Controls.Add(this.teamTabPage);
            this.teamTabControl.Controls.Add(this.teamGamesTabPage);
            this.teamTabControl.Location = new System.Drawing.Point(492, 13);
            this.teamTabControl.Name = "teamTabControl";
            this.teamTabControl.SelectedIndex = 0;
            this.teamTabControl.Size = new System.Drawing.Size(720, 552);
            this.teamTabControl.TabIndex = 1;
            // 
            // teamTabPage
            // 
            this.teamTabPage.Controls.Add(this.groupBox2);
            this.teamTabPage.Controls.Add(this.groupBox1);
            this.teamTabPage.Controls.Add(this.label10);
            this.teamTabPage.Controls.Add(this.label13);
            this.teamTabPage.Controls.Add(this.postSeasonRecordLabel);
            this.teamTabPage.Controls.Add(this.label8);
            this.teamTabPage.Controls.Add(this.preSeasonRecordLabel);
            this.teamTabPage.Controls.Add(this.label6);
            this.teamTabPage.Controls.Add(this.label3);
            this.teamTabPage.Controls.Add(this.regularSeasonRecordLabel);
            this.teamTabPage.Controls.Add(this.label1);
            this.teamTabPage.Controls.Add(this.arenaName);
            this.teamTabPage.Controls.Add(this.teamName);
            this.teamTabPage.Location = new System.Drawing.Point(4, 22);
            this.teamTabPage.Name = "teamTabPage";
            this.teamTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.teamTabPage.Size = new System.Drawing.Size(712, 526);
            this.teamTabPage.TabIndex = 0;
            this.teamTabPage.Text = "Team";
            this.teamTabPage.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.PPGLossesLabel);
            this.groupBox2.Controls.Add(this.PPGWinsLabel);
            this.groupBox2.Controls.Add(this.regularSeasonPPGLabel);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Location = new System.Drawing.Point(23, 100);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(157, 109);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            // 
            // PPGLossesLabel
            // 
            this.PPGLossesLabel.AutoSize = true;
            this.PPGLossesLabel.Location = new System.Drawing.Point(112, 80);
            this.PPGLossesLabel.Name = "PPGLossesLabel";
            this.PPGLossesLabel.Size = new System.Drawing.Size(28, 13);
            this.PPGLossesLabel.TabIndex = 23;
            this.PPGLossesLabel.Text = "0.00";
            // 
            // PPGWinsLabel
            // 
            this.PPGWinsLabel.AutoSize = true;
            this.PPGWinsLabel.Location = new System.Drawing.Point(112, 54);
            this.PPGWinsLabel.Name = "PPGWinsLabel";
            this.PPGWinsLabel.Size = new System.Drawing.Size(28, 13);
            this.PPGWinsLabel.TabIndex = 22;
            this.PPGWinsLabel.Text = "0.00";
            // 
            // regularSeasonPPGLabel
            // 
            this.regularSeasonPPGLabel.AutoSize = true;
            this.regularSeasonPPGLabel.Location = new System.Drawing.Point(112, 27);
            this.regularSeasonPPGLabel.Name = "regularSeasonPPGLabel";
            this.regularSeasonPPGLabel.Size = new System.Drawing.Size(28, 13);
            this.regularSeasonPPGLabel.TabIndex = 21;
            this.regularSeasonPPGLabel.Text = "0.00";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 80);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "PPG Losses";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 54);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(56, 13);
            this.label15.TabIndex = 18;
            this.label15.Text = "PPG Wins";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 25);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 13);
            this.label17.TabIndex = 16;
            this.label17.Text = "Overall PPG";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.OppPPGLossesLabel);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.OppPPGWinsLabel);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.OppRegularSeasonPPGLabel);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(23, 210);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(157, 109);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Opponents";
            // 
            // OppPPGLossesLabel
            // 
            this.OppPPGLossesLabel.AutoSize = true;
            this.OppPPGLossesLabel.Location = new System.Drawing.Point(112, 80);
            this.OppPPGLossesLabel.Name = "OppPPGLossesLabel";
            this.OppPPGLossesLabel.Size = new System.Drawing.Size(28, 13);
            this.OppPPGLossesLabel.TabIndex = 21;
            this.OppPPGLossesLabel.Text = "0.00";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 80);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "PPG Losses";
            // 
            // OppPPGWinsLabel
            // 
            this.OppPPGWinsLabel.AutoSize = true;
            this.OppPPGWinsLabel.Location = new System.Drawing.Point(112, 54);
            this.OppPPGWinsLabel.Name = "OppPPGWinsLabel";
            this.OppPPGWinsLabel.Size = new System.Drawing.Size(28, 13);
            this.OppPPGWinsLabel.TabIndex = 19;
            this.OppPPGWinsLabel.Text = "0.00";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "PPG Wins";
            // 
            // OppRegularSeasonPPGLabel
            // 
            this.OppRegularSeasonPPGLabel.AutoSize = true;
            this.OppRegularSeasonPPGLabel.Location = new System.Drawing.Point(112, 25);
            this.OppRegularSeasonPPGLabel.Name = "OppRegularSeasonPPGLabel";
            this.OppRegularSeasonPPGLabel.Size = new System.Drawing.Size(28, 13);
            this.OppRegularSeasonPPGLabel.TabIndex = 17;
            this.OppRegularSeasonPPGLabel.Text = "0.00";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Overall PPG";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 161);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "PPG Losses";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(21, 134);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 13);
            this.label13.TabIndex = 16;
            this.label13.Text = "PPG Wins";
            // 
            // postSeasonRecordLabel
            // 
            this.postSeasonRecordLabel.AutoSize = true;
            this.postSeasonRecordLabel.Location = new System.Drawing.Point(520, 84);
            this.postSeasonRecordLabel.Name = "postSeasonRecordLabel";
            this.postSeasonRecordLabel.Size = new System.Drawing.Size(22, 13);
            this.postSeasonRecordLabel.TabIndex = 9;
            this.postSeasonRecordLabel.Text = "0-0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(405, 84);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Post-Season";
            // 
            // preSeasonRecordLabel
            // 
            this.preSeasonRecordLabel.AutoSize = true;
            this.preSeasonRecordLabel.Location = new System.Drawing.Point(332, 84);
            this.preSeasonRecordLabel.Name = "preSeasonRecordLabel";
            this.preSeasonRecordLabel.Size = new System.Drawing.Size(22, 13);
            this.preSeasonRecordLabel.TabIndex = 7;
            this.preSeasonRecordLabel.Text = "0-0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(218, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Pre-Season";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Points Per Game";
            // 
            // regularSeasonRecordLabel
            // 
            this.regularSeasonRecordLabel.AutoSize = true;
            this.regularSeasonRecordLabel.Location = new System.Drawing.Point(136, 84);
            this.regularSeasonRecordLabel.Name = "regularSeasonRecordLabel";
            this.regularSeasonRecordLabel.Size = new System.Drawing.Size(22, 13);
            this.regularSeasonRecordLabel.TabIndex = 3;
            this.regularSeasonRecordLabel.Text = "0-0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Regular Season";
            // 
            // arenaName
            // 
            this.arenaName.AutoSize = true;
            this.arenaName.Location = new System.Drawing.Point(21, 46);
            this.arenaName.Name = "arenaName";
            this.arenaName.Size = new System.Drawing.Size(62, 13);
            this.arenaName.TabIndex = 1;
            this.arenaName.Text = "arenaName";
            // 
            // teamName
            // 
            this.teamName.AutoSize = true;
            this.teamName.Location = new System.Drawing.Point(21, 16);
            this.teamName.Name = "teamName";
            this.teamName.Size = new System.Drawing.Size(58, 13);
            this.teamName.TabIndex = 0;
            this.teamName.Text = "teamName";
            // 
            // teamGamesTabPage
            // 
            this.teamGamesTabPage.Controls.Add(this.label14);
            this.teamGamesTabPage.Controls.Add(this.resultsCB);
            this.teamGamesTabPage.Controls.Add(this.label5);
            this.teamGamesTabPage.Controls.Add(this.locationCB);
            this.teamGamesTabPage.Controls.Add(this.label4);
            this.teamGamesTabPage.Controls.Add(this.label2);
            this.teamGamesTabPage.Controls.Add(this.opponentComboBox);
            this.teamGamesTabPage.Controls.Add(this.gameGridView);
            this.teamGamesTabPage.Location = new System.Drawing.Point(4, 22);
            this.teamGamesTabPage.Name = "teamGamesTabPage";
            this.teamGamesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.teamGamesTabPage.Size = new System.Drawing.Size(712, 526);
            this.teamGamesTabPage.TabIndex = 1;
            this.teamGamesTabPage.Text = "Games";
            this.teamGamesTabPage.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(486, 26);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(42, 13);
            this.label14.TabIndex = 8;
            this.label14.Text = "Results";
            // 
            // resultsCB
            // 
            this.resultsCB.FormattingEnabled = true;
            this.resultsCB.Location = new System.Drawing.Point(396, 42);
            this.resultsCB.Name = "resultsCB";
            this.resultsCB.Size = new System.Drawing.Size(144, 21);
            this.resultsCB.TabIndex = 7;
            this.resultsCB.SelectedIndexChanged += new System.EventHandler(this.resultsCB_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(298, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Location";
            // 
            // locationCB
            // 
            this.locationCB.Cursor = System.Windows.Forms.Cursors.Default;
            this.locationCB.FormattingEnabled = true;
            this.locationCB.Location = new System.Drawing.Point(208, 42);
            this.locationCB.Name = "locationCB";
            this.locationCB.Size = new System.Drawing.Size(144, 21);
            this.locationCB.TabIndex = 5;
            this.locationCB.SelectedIndexChanged += new System.EventHandler(this.locationCB_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(112, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Opponent";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Filters";
            // 
            // opponentComboBox
            // 
            this.opponentComboBox.FormattingEnabled = true;
            this.opponentComboBox.Location = new System.Drawing.Point(22, 42);
            this.opponentComboBox.Name = "opponentComboBox";
            this.opponentComboBox.Size = new System.Drawing.Size(144, 21);
            this.opponentComboBox.TabIndex = 2;
            this.opponentComboBox.SelectedIndexChanged += new System.EventHandler(this.opponentComboBox_SelectedIndexChanged);
            // 
            // gameGridView
            // 
            this.gameGridView.AllowUserToAddRows = false;
            this.gameGridView.AllowUserToDeleteRows = false;
            this.gameGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gameGridView.Location = new System.Drawing.Point(6, 81);
            this.gameGridView.Name = "gameGridView";
            this.gameGridView.ReadOnly = true;
            this.gameGridView.RowHeadersWidth = 51;
            this.gameGridView.Size = new System.Drawing.Size(694, 439);
            this.gameGridView.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1242, 657);
            this.Controls.Add(this.teamTabControl);
            this.Controls.Add(this.teamDataGridView);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.teamDataGridView)).EndInit();
            this.teamTabControl.ResumeLayout(false);
            this.teamTabPage.ResumeLayout(false);
            this.teamTabPage.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.teamGamesTabPage.ResumeLayout(false);
            this.teamGamesTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView teamDataGridView;
        private System.Windows.Forms.TabControl teamTabControl;
        private System.Windows.Forms.TabPage teamTabPage;
        private System.Windows.Forms.TabPage teamGamesTabPage;
        private System.Windows.Forms.Label arenaName;
        private System.Windows.Forms.Label teamName;
        private System.Windows.Forms.Label regularSeasonRecordLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView gameGridView;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox opponentComboBox;
        private System.Windows.Forms.Label postSeasonRecordLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label preSeasonRecordLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label PPGLossesLabel;
        private System.Windows.Forms.Label PPGWinsLabel;
        private System.Windows.Forms.Label regularSeasonPPGLabel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label OppPPGLossesLabel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label OppPPGWinsLabel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label OppRegularSeasonPPGLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox resultsCB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox locationCB;
    }
}

