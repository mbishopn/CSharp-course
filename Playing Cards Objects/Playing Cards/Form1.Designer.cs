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
            this.deckPictureBox = new System.Windows.Forms.PictureBox();
            this.dealButton = new System.Windows.Forms.Button();
            this.reshuffleButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.deckPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // deckPictureBox
            // 
            this.deckPictureBox.Location = new System.Drawing.Point(917, 34);
            this.deckPictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.deckPictureBox.Name = "deckPictureBox";
            this.deckPictureBox.Size = new System.Drawing.Size(133, 178);
            this.deckPictureBox.TabIndex = 0;
            this.deckPictureBox.TabStop = false;
            // 
            // dealButton
            // 
            this.dealButton.Location = new System.Drawing.Point(933, 220);
            this.dealButton.Margin = new System.Windows.Forms.Padding(4);
            this.dealButton.Name = "dealButton";
            this.dealButton.Size = new System.Drawing.Size(100, 28);
            this.dealButton.TabIndex = 1;
            this.dealButton.Text = "Deal";
            this.dealButton.UseVisualStyleBackColor = true;
            this.dealButton.Click += new System.EventHandler(this.dealButton_Click);
            // 
            // reshuffleButton
            // 
            this.reshuffleButton.Location = new System.Drawing.Point(933, 266);
            this.reshuffleButton.Margin = new System.Windows.Forms.Padding(4);
            this.reshuffleButton.Name = "reshuffleButton";
            this.reshuffleButton.Size = new System.Drawing.Size(100, 28);
            this.reshuffleButton.TabIndex = 2;
            this.reshuffleButton.Text = "Re-shuffle";
            this.reshuffleButton.UseVisualStyleBackColor = true;
            this.reshuffleButton.Click += new System.EventHandler(this.reshuffleButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.reshuffleButton);
            this.Controls.Add(this.dealButton);
            this.Controls.Add(this.deckPictureBox);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.deckPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox deckPictureBox;
        private System.Windows.Forms.Button dealButton;
        private System.Windows.Forms.Button reshuffleButton;
    }
}

