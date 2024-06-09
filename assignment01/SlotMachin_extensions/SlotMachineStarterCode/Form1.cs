using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; //--- to read custom values file
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

Above comments were written after accomplishing core-requirements, I had no idea what was yet to come :D

still TO-DO: using arrays and spinning wheels at different speeds -- DONE!!

Thanks Scott! I'm really enjoying this hands-on course.
 
 */

namespace SlotMachineStarterCode
{
    public partial class Form1 : Form
    {
        int timerCounter, img1, img2, img3;
        int[] w = new int[]{1,2,3}; //----- E X T E  N S I O N   6 --------- array to store different spin speed for each wheel
        // ------------ E X T E N S I O N     5 ---------- Using array for images
        Image[] reel= new Image[4];
        //------ default values for spin cost, payouts and other variables
        int spinCost = 2,
            all7 = 25,
            sameFruit = 10,
            anyFruit = 1,
            initBalance = 25,
            bonusCost = 3,
            bonusPrize = 5,
            balance=0,
            spent=0,
            won=0; 

        public Form1()
        {
            InitializeComponent();
            // ------- EXTENSION 4 ------- using a config file to customize spin price and payouts
            readConfigFile();
                      
            // --- I don't know how to re-initialize the program, so I created a function to
            // --- set all controls (pictureboxes, labels and buttons) to load form state every
            // --- time I click reset button
            resetGame();
        }

        // ----------------------------------   R E S E T    F U N C T I O N   -----------------------------------
        // --- this is my reset function, I just moved what was on Form1() and added initial values for labels
        // --- and other controls, after going back and forth through the code
        private void resetGame()
        {
            // Load my slot machine images from disk
            reel[0] = Image.FromFile("../../Images/seven.png");
            reel[1] = Image.FromFile("../../Images/grape.png");
            reel[2] = Image.FromFile("../../Images/lemon.png");
            reel[3] = Image.FromFile("../../Images/pineapple.png");

            // we have 512 x 512 pixel images, make them fit the 128 x 128 pictureBoxes
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;

            // Start all Images as seven
            pictureBox1.Image = reel[0];
            pictureBox2.Image = reel[0];
            pictureBox3.Image = reel[0];

            bonusFruitPb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            showBonus(false);   // hiding bonus selection panel

            // I created updateControlPanel(), toggleButtons() and gameStats(), because their actions
            // are required when app starts, during the game and when reset button is pressed.
            // reset displays for balance, spent and won
            updateControlPanel('i');

            // ------ E X T E N S I O N    1 --- Make buttons invisible until balance drops below $2 
            // after EXTENSION 4 value depends on spinCost value, at the end I created a function to
            // handle all buttons behaviour
            toggleButtons("011");

            //-----  E X T E N S I O N    2 --- resetting game stats
            a7label.Text = "$" + all7;
            sFlabel.Text = "$" + sameFruit;
            aFlabel.Text = "$" + anyFruit;
            label1.Text = "$0";
            bLabel.Text = "$" + bonusPrize;
            gameStats(0);
            spinCostLabel.Text = spinCost.ToString();

            //---------- E X T E N S I O N     3 ------- bonus button and related controls INITIAL VALUES
            // -- hide the bonus panel
            bonusPanel.Visible = false;
            // -- let's put the images to select bonus fruit
            bonusPb1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            bonusPb2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            bonusPb3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            bonusPb1.Image = reel[1];
            bonusPb2.Image = reel[2];
            bonusPb3.Image = reel[3];
            // -- creating bonus fruit click handlers.
            bonusPb1.Click += new EventHandler(bonusPb_Click);
            bonusPb2.Click += new EventHandler(bonusPb_Click);
            bonusPb3.Click += new EventHandler(bonusPb_Click);
            // explanation on in bonus selection panel is updated with current cost and prize
            label2.Text = "Click on a fruit to select it, if that fruit appears twice after next spin you'll get a $" +
                          bonusPrize + " bonus. Spin with bonus cost: $" + bonusCost;
        }

        private void setImage(PictureBox pb, int n)
        {
                pb.Image = reel[n];     // ------- E X T E N S I O N   4 ------ Using array for images and reducing code in setimage()
        }
        private void rotateImages(int reel)
        {
            Random rnd = new Random();
            switch (reel)
            {
                case 1: setImage(pictureBox1, rnd.Next(0, 4));break;
                case 2: setImage(pictureBox2, rnd.Next(0, 4)); break;
                case 3: setImage(pictureBox3, rnd.Next(0, 4)); break;
            }
        }

        //---------------------     T I M E R      F U N C T I O N      --------------------------
        private void timer1_Tick(object sender, EventArgs e)
        {
            timerCounter += 1;

            // first 10 ticks are fast (1/10 of a second), second two are slower (400ms)
            //if (timerCounter == 12)
            //{
            //    timer1.Interval = 400;
            //}

            //if (timerCounter <= 14)
            //{
            //    // rotate all the images every tick for the first 12 ticks
            //    rotateImages();
            //}
            //if (timerCounter <= 14)
            //{
            // assigning its speed to each wheel
            if (timerCounter % w[0] == 0)
            {
                rotateImages(1);img1 += 1;
            }
            if (timerCounter % w[1] == 0)
            {
                rotateImages(2);img2 += 1;
            }
            if (timerCounter % w[2] == 0)
            {
                rotateImages(3);img3 += 1;
            }//}


            if (timerCounter > 38)
            {
                // stop the timer, we are done
                timer1.Stop();
                //MessageBox.Show(w[0] + " " + w[1] + " " + w[2],"speed");
                //MessageBox.Show(img1 + " " + img2 + " " + img3,"cambios");
                toggleButtons("211"); // enabling spin button when spin ends
                spinResult();       // now get results!
            }
        }

        // -------------    E X T E N S I O N      4    -------------   U S E    A    C O N F I G    F I L E  ----------------
        private void readConfigFile()
        {
            int line_count = 0;  // you can put extra things in the file, but I'm reading 7 parameters maximum
            try
            {
                foreach (var line in File.ReadLines("../../slotmachine.txt"))
                {
                    if (line.StartsWith("$") && line.EndsWith("$"))
                    {
                        string[] confpair = line.Trim('$').Split('=');
                        switch (confpair[0])
                        {
                            case "spinCost":
                                spinCost = Convert.ToInt32(confpair[1]); line_count++;
                                break;
                            case "all7":
                                a7label.Text = "$" + confpair[1];
                                all7 = Convert.ToInt32(confpair[1]); line_count++;
                                break;
                            case "sameFruit":
                                sFlabel.Text = "$" + confpair[1];
                                sameFruit = Convert.ToInt32(confpair[1]); line_count++;
                                break;
                            case "anyFruit":
                                aFlabel.Text = "$" + confpair[1];
                                anyFruit = Convert.ToInt32(confpair[1]); line_count++;
                                break;
                            case "initBalance":
                                initBalance = Convert.ToInt32(confpair[1]); line_count++;
                                break;
                            case "bonusCost":
                                bonusCost = Convert.ToInt32(confpair[1]); line_count++;
                                break;
                            case "bonusPrize":
                                bonusPrize = Convert.ToInt32(confpair[1]); line_count++;
                                break;
                        }
                    }
                    if (line_count > 8)
                    { break; }
                }
            }catch(FileNotFoundException a) { MessageBox.Show(a.Message,"Config File not found: Loading Defaults"); }
            finally { /*I don't know what should be here*/ }
        }

        // ----------------------------  U P D A T E    C O N T R O L    P A N E L    D I S P L A Y  -----------------------
        private void updateControlPanel(char x)
        {
            switch(x)
            {
                case 's':               //----- updates only balance and spent immediately after click on spin button
                    balanceLabel.Text = balance.ToString();
                    spentLabel.Text = spent.ToString();
                    break;
                case 'c':               // ---- updates balance and won last try when spin ends
                    balanceLabel.Text = balance.ToString();
                    wonLabel.Text = won.ToString();
                    break;
                case 'i':               // ---- initilizes control panel info
                    balanceLabel.Text = (balance=initBalance).ToString();
                    spentLabel.Text = (spent=0).ToString();
                    wonLabel.Text = (won=0).ToString();
                    break;
            }
        }

        //------------------------------   S P I N     B U T T O N    C L I C K   --------------------------------
        private void spinButton_Click(object sender, EventArgs e)
        {
            // Start time and reset timerCounter
            toggleButtons("200"); // just disabling spin button during the spin, don't want to press it more than once

            if (balance < spinCost)
                MessageBox.Show("Not enough money to spin, use ADD $5 button","WARNING");
            else
            {
                if (bonusFruitPb.Visible)
                { 
                    spent += bonusCost; balance -= bonusCost; 
                }
                else
                {
                    spent += spinCost; balance -= spinCost;
                }
                    updateControlPanel('s');
                    timerCounter = 0;
                img1 = img2 = img3 = timerCounter;
                    timer1.Interval = 20; // 100 ms or 1/10 of a second
             //-----   E X T E N S  I O N    6    ----- each time, the wheels change their spin speed
                Random rnd = new Random();      
                    w = w.OrderBy(x => rnd.Next(1,4)).ToArray();
                    timer1.Start();
                }
        }

        ///  -----------------    C A L C U L A T E     W I N N I N G S    A F T E R     S P I N     ------------------
        private void spinResult()
        {
            if(pictureBox1.Image==pictureBox2.Image && pictureBox2.Image==pictureBox3.Image)
            {
                if (pictureBox1.Image==reel[0])
                {
                    gameStats(4);
                }
                else
                {
                    gameStats(3);
                }
            }
            else
            {
                if(pictureBox1.Image== reel[0] || pictureBox2.Image== reel[0] || pictureBox3.Image== reel[0])
                {
                    gameStats(1);
                }
                else
                {
                    gameStats(2);
                }
            }
            updateControlPanel('c');
            // ---------- E X T E N S I O N  1 --- Make buttons invisible till balance drops below $2 // after EXTENSION 4 thresold is determined by spinCost value
            if (balance < spinCost)
            {
                toggleButtons("120");
                //showBonus(false);
            }
            //---- E X T E N S I O N    3 --- Can't use bonus if there's no enough money, I'm putting
            // ---  this condition isolated only to keep extension task separated.
            if (balance < bonusCost)
            { toggleButtons("220");showBonus(false); }

        }

        // ------------------  C O R E    T A S K S  ---------------  A D D     R E S E T       B U T T O N    ----------------
        private void resetButton_Click(object sender, EventArgs e)
        {
            //readConfigFile();
            resetGame();
        }

        // ------------------  C O R E    T A S K S   --------  A D D    F I V E   D O L L A R S   B U T T O N  ---------------
        private void add5Button_Click(object sender, EventArgs e)
        {

            balanceLabel.Text = (balance += 5).ToString();
            //toggleButtons(1, true);// spinButton.Enabled = true;
            if (balance > spinCost)
                toggleButtons("012");
            if (balance > bonusCost)
                toggleButtons("211");

            //bonusButton.Visible = true;
        }

        //  ----- E X T E N S I O N  1 ----    H I N D I N G / S H O W I N G    A D D 5  /  R E S E T    G A M E    B U T T O N S ------
        // after EXTENSION 4 threshold value is determined by spin variable
        // argument is composed of 3 chars each could have this values:
        //       0=disable/hide  1=enable/show  2=no changes
        // char1= add5 and reset buttons, char2=spin button, char3= bonus button
        private void toggleButtons(string arsb)
        {
            int count = 0;
            foreach(char x in arsb)
            {
                if (x != '2')
                {
                    switch (count)
                    {
                        case 0:
                            add5Button.Visible = Convert.ToBoolean(Convert.ToInt32((arsb[count]).ToString()));
                            resetButton.Visible = Convert.ToBoolean(Convert.ToInt32((arsb[count]).ToString()));
                            break;
                        case 1:
                            spinButton.Enabled = Convert.ToBoolean(Convert.ToInt32((arsb[count]).ToString()));
                            break;
                        case 2:
                            bonusButton.Enabled = Convert.ToBoolean(Convert.ToInt32((arsb[count]).ToString()));
                            break;
                    }
                }
                count++;
            }
        }
        // -------- E X T E N S I O N    2 ------- G A M E    S T A T S    U P D A T I N G    F U N C T I O N  ---------------
        private void gameStats(int n)
        {
            string tmp; // -- temp string variable to manipulate text in labels
            switch(n)
            {
                case 1:
                    won = 0;
                    stats0label.Text = (Convert.ToInt32(tmp = stats0label.Text.TrimEnd('x')) + 1).ToString() + "x";// updates 0win stats
                    break;
                case 2:     
                    won = anyFruit;
                    statsAfLabel.Text = (Convert.ToInt32(tmp = statsAfLabel.Text.TrimEnd('x')) + 1).ToString() + "x";// updates af stats
                    break;
                case 3:     
                    won = sameFruit;
                    statsSfLabel.Text = (Convert.ToInt32(tmp = statsSfLabel.Text.TrimEnd('x')) + 1).ToString() + "x";// updates sf stats
                    break;
                case 4:     
                    won = all7;
                    statsA7Label.Text = (Convert.ToInt32(tmp = statsA7Label.Text.TrimEnd('x')) + 1).ToString() + "x"; // updates a7 stats
                    break;
                case 0:     // resetting statistics
                    foreach (Label value in this.groupBox2.Controls.OfType<Label>().Where(x => x.Name.StartsWith("stats")))
                    {
                        value.Text="0x";
                    }
                    break;
            }
            if (bonusFruitPb.Visible)   // should I add bonus to winnings?
            { checkBonus(); }
            balance += won;             // add all winnings to balance

            // now update our winnings stats
            winningsLabel.Text = ((Convert.ToInt32(tmp = statsBlabel.Text.TrimEnd('x')) * bonusPrize) +
                                 (Convert.ToInt32(tmp = statsAfLabel.Text.TrimEnd('x')) * anyFruit) + 
                                 (Convert.ToInt32(tmp = statsSfLabel.Text.TrimEnd('x')) * sameFruit) + 
                                 (Convert.ToInt32(tmp = statsA7Label.Text.TrimEnd('x')) * all7)).ToString();
        }

        // -------------- E X T E N S I O N    3 --------------- B O N U S   F U N C T I O N S  ----------------
        // ------------------------ calling bonus selection panel ------------------------
        private void bonusButton_Click(object sender, EventArgs e)
        {
            bonusPanel.BringToFront();
            bonusPanel.Visible = true;
        }
        //-----------------------  selecting bonus fruit ---------------------------
        private void bonusPb_Click(object sender, EventArgs e)
        {
            PictureBox x = sender as PictureBox;
            bonusFruitPb.Image = x.Image;
            showBonus(true);
            bonusPanel.Visible = false;
        }
        // ----------------- canceling bonus selection  ----------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            bonusPanel.Visible = false;
        }
        // ------------------------ is the bonus earned? ---------------------------
        private void checkBonus()
        {
            int count = 0;
            foreach(PictureBox imagen in this.Controls.OfType<PictureBox>().Where(x=>x.Name.StartsWith("pictureBox")))
            {
                if (imagen.Image==bonusFruitPb.Image)
                { count++; }

            }
            if (count > 1)
            {
                MessageBox.Show("YOU'VE GOT A $ " + bonusPrize + " EXTRA BONUS!!!", " --- BONUS WINNER --- ");
                // increment bonuses statistics, i should put this inside gamestats()
                statsBlabel.Text = (Convert.ToInt32(statsBlabel.Text.TrimEnd('x')) + 1).ToString() + "x";
                //add bonus to normal payout
                won += bonusPrize;
            }
        }
        // -----------------shows bonus info (if selected)---------------------------
        private void showBonus(bool flag)
        {
            if (flag)
                spinCostLabel.Text = bonusCost.ToString();
            else
                spinCostLabel.Text = spinCost.ToString();
            bonusFlabel.Text = "$ " + bonusPrize + " extra if appears twice";
            bonusFruitPb.Visible = flag;
            bonusFlabel.Visible = flag;
            bonusDbutton.Visible = flag;
        }
        // ------------------------------No more bonuses please!---------------------
        private void bonusDbutton_Click(object sender, EventArgs e)
        {
            showBonus(false);
        }

    }
}
