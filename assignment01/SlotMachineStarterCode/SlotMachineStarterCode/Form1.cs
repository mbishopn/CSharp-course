using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/*
  --------------------------    C O R E     R E Q U I R E M E N T S   ----------------------------
 I think the most difficult part was to calculate the winnings, but I'm affraid that's because I tend
to make simple things complex :D
 At the beginning, I thought I would assign different values to each figure in reels to calculate
winnings, so after a spin I would add 3 values and according to it calculate the money earned, but then
I realized that it was simpler if I just compared the 3 images to decide and that's what I did in
function spindResult(). I have the feeling that I still can make it simpler by using foreach's (or 
something else) instead of if's but I was running out of time to deliver and decided to leave it like that.

Other thing I haven't been able to figure out how to get back to state when form loads for the first time.
In visual basic as far as I remember it was enough to call form1.load() but here it doesn't work, so I put
all lines initializing the game in a function named resetGame().
 
 */

namespace SlotMachineStarterCode
{
    public partial class Form1 : Form
    {
        int timerCounter;
        Image seven;
        Image lemon;
        Image grape;
        Image pineapple;

        public Form1()
        {
            InitializeComponent();
            resetGame(); // I don't know how to re-initialize the program, so I created this function to
                         // set all controls (pictureboxes, labels and buttons) to load form state every time I call it e.g. from reset button
        }

        private void resetGame() //---- this is my reset function, I just moved what was on Form1() and added initial values for labels
        {
            // Load my slot machine images from disk
            seven = Image.FromFile("../../Images/seven.png");
            grape = Image.FromFile("../../Images/grape.png");
            lemon = Image.FromFile("../../Images/lemon.png");
            pineapple = Image.FromFile("../../Images/pineapple.png");

            // we have 512 x 512 pixel images, make them fit the 128 x 128 pictureBoxes
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;

            // Start all Images as seven
            pictureBox1.Image = seven;
            pictureBox2.Image = seven;
            pictureBox3.Image = seven;
            //------ I'm using this labels as variables to keep track of balance and money spent, I don't know if this complies with good practices, please let me know.
            balance.Text = "25";
            spent.Text = "0";
            won.Text = "0";

        }
        private void setImage(PictureBox pb, int n)
        {
            switch (n)
            {
                case 1:
                    pb.Image = grape;
                    break;
                case 2:
                    pb.Image = lemon;
                    break;
                case 4:
                    pb.Image = seven;
                    break;
                case 3:
                    pb.Image = pineapple;
                    break;
            }
        }
        private void rotateImages()
        {
            Random rnd = new Random();
            setImage(pictureBox1, rnd.Next(1, 5));
            setImage(pictureBox2, rnd.Next(1, 5));
            setImage(pictureBox3, rnd.Next(1, 5));
        }

        private void spinButton_Click(object sender, EventArgs e)
        {
            // Start time and reset timerCounter
            spinButton.Enabled = false;  //----- we don't want to click again this button before spin ends.

            if (Convert.ToInt32(balance.Text) < 2)
                MessageBox.Show("NO ENOUGH MONEY, USE ADD $5 button");
            else
            { 
                spent.Text = (Convert.ToInt32(spent.Text) + 2).ToString();
                balance.Text = (Convert.ToInt32(balance.Text) - 2).ToString();
                timerCounter = 0;
                timer1.Interval = 100; // 100 ms or 1/10 of a second
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timerCounter += 1;

            // first 10 ticks are fast (1/10 of a second), second two are slower (400ms)
            if (timerCounter == 10)
            {
                timer1.Interval = 400;
            }

            if (timerCounter <= 12)
            {
                // rotate all the images every tick for the first 12 ticks
                rotateImages();
            }

            if (timerCounter > 12)
            {
                // stop the timer, we are done
                timer1.Stop();
                spinButton.Enabled = true;
                spinResult();
            }
        }

        ///  ------------------ S P I N     R E S U L T   ------- Calculates the winnings.
        private void spinResult()
        {
            if(pictureBox1.Image==pictureBox2.Image && pictureBox2.Image==pictureBox3.Image)
            {
                if (pictureBox1.Image==seven)
                {
                    balance.Text = (Convert.ToInt32(balance.Text) + 25).ToString();
                    won.Text = "25";
                }
                else
                {
                    balance.Text = (Convert.ToInt32(balance.Text) + 10).ToString();
                    won.Text = "10";
                }
            }
            else
            {
                if(pictureBox1.Image==seven||pictureBox2.Image==seven||pictureBox3.Image==seven)
                {
                    won.Text = "):";
                }
                else
                {
                    balance.Text = (Convert.ToInt32(balance.Text) + 1).ToString();
                    won.Text = "1";
                }
            }
        }

        // ----------------------------   R E S E T       B U T T O N    ---------------------------
        private void resetButton_Click(object sender, EventArgs e)
        {
            resetGame();
        }

        // ----------------------------  A D D    F I V E   D O L L A R S   B U T T O N  ---------------
        private void add5Button_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(balance.Text) > 20)
                MessageBox.Show("Are you kidding?");  // ---- Try a couple of times before asking for money, we're running a bussiness here right?
            else
                balance.Text = (Convert.ToInt32(balance.Text) + 5).ToString();
            spinButton.Enabled = true;
        }
    }
}
