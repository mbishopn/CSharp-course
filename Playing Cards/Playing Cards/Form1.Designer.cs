namespace Playing_Cards
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
        /// ---------------------------------------------------------------------- esto no seque pedo
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.deckPictureBox = new System.Windows.Forms.PictureBox();
            this.dealButton = new System.Windows.Forms.Button();
            this.shuffleButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pointsP1label = new System.Windows.Forms.Label();
            this.pointsP2label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.deckPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // deckPictureBox
            // 
            this.deckPictureBox.Location = new System.Drawing.Point(871, 43);
            this.deckPictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.deckPictureBox.Name = "deckPictureBox";
            this.deckPictureBox.Size = new System.Drawing.Size(133, 178);
            this.deckPictureBox.TabIndex = 0;
            this.deckPictureBox.TabStop = false;
            // 
            // dealButton
            // 
            this.dealButton.Location = new System.Drawing.Point(888, 226);
            this.dealButton.Margin = new System.Windows.Forms.Padding(4);
            this.dealButton.Name = "dealButton";
            this.dealButton.Size = new System.Drawing.Size(100, 28);
            this.dealButton.TabIndex = 1;
            this.dealButton.Text = "Deal";
            this.dealButton.UseVisualStyleBackColor = true;
            this.dealButton.Click += new System.EventHandler(this.dealButton_Click);
            // 
            // shuffleButton
            // 
            this.shuffleButton.Location = new System.Drawing.Point(888, 262);
            this.shuffleButton.Margin = new System.Windows.Forms.Padding(4);
            this.shuffleButton.Name = "shuffleButton";
            this.shuffleButton.Size = new System.Drawing.Size(100, 28);
            this.shuffleButton.TabIndex = 2;
            this.shuffleButton.Text = "Re-shuffle";
            this.shuffleButton.UseVisualStyleBackColor = true;
            this.shuffleButton.Click += new System.EventHandler(this.shuffleButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(360, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "P L A Y E R   1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(360, 276);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "P L A Y E R   2";
            // 
            // pointsP1label
            // 
            this.pointsP1label.Location = new System.Drawing.Point(805, 136);
            this.pointsP1label.Name = "pointsP1label";
            this.pointsP1label.Size = new System.Drawing.Size(59, 58);
            this.pointsP1label.TabIndex = 3;
            this.pointsP1label.Text = "--";
            // 
            // pointsP2label
            // 
            this.pointsP2label.Location = new System.Drawing.Point(805, 380);
            this.pointsP2label.Name = "pointsP2label";
            this.pointsP2label.Size = new System.Drawing.Size(59, 62);
            this.pointsP2label.TabIndex = 4;
            this.pointsP2label.Text = "--";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.pointsP2label);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pointsP1label);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.shuffleButton);
            this.Controls.Add(this.dealButton);
            this.Controls.Add(this.deckPictureBox);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.deckPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox deckPictureBox;
        private System.Windows.Forms.Button dealButton;
        private System.Windows.Forms.Button shuffleButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label pointsP1label;
        private System.Windows.Forms.Label pointsP2label;
    }
}

