namespace Connect4
{
    partial class Connect4
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
            this.boardBox = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.playerBox = new System.Windows.Forms.PictureBox();
            this.CurrentPlayer = new System.Windows.Forms.Label();
            this.winnerLabel = new System.Windows.Forms.Label();
            this.winnerBox = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.csP2 = new System.Windows.Forms.Button();
            this.csP1 = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.resetButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.boardBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.winnerBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // boardBox
            // 
            this.boardBox.BackColor = System.Drawing.SystemColors.HotTrack;
            this.boardBox.Location = new System.Drawing.Point(30, 83);
            this.boardBox.Margin = new System.Windows.Forms.Padding(4);
            this.boardBox.Name = "boardBox";
            this.boardBox.Size = new System.Drawing.Size(933, 738);
            this.boardBox.TabIndex = 0;
            this.boardBox.TabStop = false;
            this.boardBox.Paint += new System.Windows.Forms.PaintEventHandler(this.boardBox_Paint);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(77, 22);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(40, 37);
            this.button1.TabIndex = 1;
            this.button1.Text = "\\/";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(210, 22);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(40, 38);
            this.button2.TabIndex = 2;
            this.button2.Text = "\\/";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(344, 22);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(40, 38);
            this.button3.TabIndex = 3;
            this.button3.Text = "\\/";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(477, 22);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(40, 38);
            this.button4.TabIndex = 4;
            this.button4.Text = "\\/";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(610, 22);
            this.button5.Margin = new System.Windows.Forms.Padding(4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(40, 38);
            this.button5.TabIndex = 5;
            this.button5.Text = "\\/";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(744, 22);
            this.button6.Margin = new System.Windows.Forms.Padding(4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(40, 38);
            this.button6.TabIndex = 6;
            this.button6.Text = "\\/";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(877, 22);
            this.button7.Margin = new System.Windows.Forms.Padding(4);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(40, 38);
            this.button7.TabIndex = 7;
            this.button7.Text = "\\/";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // playerBox
            // 
            this.playerBox.BackColor = System.Drawing.SystemColors.HotTrack;
            this.playerBox.Location = new System.Drawing.Point(1042, 389);
            this.playerBox.Margin = new System.Windows.Forms.Padding(4);
            this.playerBox.Name = "playerBox";
            this.playerBox.Size = new System.Drawing.Size(133, 123);
            this.playerBox.TabIndex = 8;
            this.playerBox.TabStop = false;
            this.playerBox.Paint += new System.Windows.Forms.PaintEventHandler(this.playerBox_Paint);
            // 
            // CurrentPlayer
            // 
            this.CurrentPlayer.AutoSize = true;
            this.CurrentPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.CurrentPlayer.Location = new System.Drawing.Point(1037, 361);
            this.CurrentPlayer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.CurrentPlayer.Name = "CurrentPlayer";
            this.CurrentPlayer.Size = new System.Drawing.Size(137, 25);
            this.CurrentPlayer.TabIndex = 9;
            this.CurrentPlayer.Text = "Current Player";
            // 
            // winnerLabel
            // 
            this.winnerLabel.AutoSize = true;
            this.winnerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.winnerLabel.Location = new System.Drawing.Point(1069, 567);
            this.winnerLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.winnerLabel.Name = "winnerLabel";
            this.winnerLabel.Size = new System.Drawing.Size(81, 25);
            this.winnerLabel.TabIndex = 11;
            this.winnerLabel.Text = "Winner!";
            // 
            // winnerBox
            // 
            this.winnerBox.BackColor = System.Drawing.SystemColors.HotTrack;
            this.winnerBox.Location = new System.Drawing.Point(1042, 595);
            this.winnerBox.Margin = new System.Windows.Forms.Padding(4);
            this.winnerBox.Name = "winnerBox";
            this.winnerBox.Size = new System.Drawing.Size(133, 123);
            this.winnerBox.TabIndex = 10;
            this.winnerBox.TabStop = false;
            this.winnerBox.Paint += new System.Windows.Forms.PaintEventHandler(this.winnerBox_Paint);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.csP2);
            this.groupBox1.Controls.Add(this.csP1);
            this.groupBox1.Location = new System.Drawing.Point(1041, 108);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(133, 137);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Color selector";
            // 
            // csP2
            // 
            this.csP2.Location = new System.Drawing.Point(12, 82);
            this.csP2.Name = "csP2";
            this.csP2.Size = new System.Drawing.Size(108, 40);
            this.csP2.TabIndex = 13;
            this.csP2.Text = "Player 2";
            this.csP2.UseVisualStyleBackColor = true;
            this.csP2.Click += new System.EventHandler(this.csPlayer_Click);
            // 
            // csP1
            // 
            this.csP1.Location = new System.Drawing.Point(12, 28);
            this.csP1.Name = "csP1";
            this.csP1.Size = new System.Drawing.Size(108, 40);
            this.csP1.TabIndex = 12;
            this.csP1.Text = "Player 1";
            this.csP1.UseVisualStyleBackColor = true;
            this.csP1.Click += new System.EventHandler(this.csPlayer_Click);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(1041, 15);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(133, 52);
            this.resetButton.TabIndex = 15;
            this.resetButton.Text = "Reset Game";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // Connect4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1237, 1010);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winnerLabel);
            this.Controls.Add(this.winnerBox);
            this.Controls.Add(this.CurrentPlayer);
            this.Controls.Add(this.playerBox);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.boardBox);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Connect4";
            this.Text = "Connect 4";
            ((System.ComponentModel.ISupportInitialize)(this.boardBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.winnerBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox boardBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.PictureBox playerBox;
        private System.Windows.Forms.Label CurrentPlayer;
        private System.Windows.Forms.Label winnerLabel;
        private System.Windows.Forms.PictureBox winnerBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button csP2;
        private System.Windows.Forms.Button csP1;
        private System.Windows.Forms.Button resetButton;
    }
}

