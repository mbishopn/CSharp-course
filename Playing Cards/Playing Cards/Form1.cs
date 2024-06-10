/*
 *  MANUEL BISHOP NORIEGA
 *  ID 4362207
 *  COMP1012 - LAB 06 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Playing_Cards
{
    public partial class Form1 : Form
    {
        Deck fullDeck;
        Deck myDeck;

        Random rng;

        Hand topHand;
        Hand bottomHand;

        //Dictionary<int, int> cPattern;

        PictureBox[] cardPictureBoxes;
        public Form1()
        {
            InitializeComponent();

            deckPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            deckPictureBox.Image = Image.FromFile("../../Images/deck.png");

            rng = new Random();

            // build the full deck of cards.  Once it is built, never change it.  Also, we never change cards.
            buildFullDeck();

            // create a new deck (basically a copy of the full deck), then shuffle it
            // Hint A -- these two lines are what a reshuffle button should do.
            myDeck = newDeck();
            shuffle(7);
            shuffleButton.Visible = false; // --- I used this with reshuffle button addition, but because I ended up using the same deal button
                                           // --- I don't want to show it anymore, I left it to show that I did core task 1 according to request
                                           // --- before I modified the dealbutton behaviour

            // create the two hands and a the pictureboxes to display their cards
            topHand = new Hand();
            bottomHand = new Hand();
            // create cPattern lists to store patterns found in each hand
            topHand.cPattern = new Dictionary<string, int>();
            bottomHand.cPattern = new Dictionary<string, int>();


            buildHandDisplay(topHand,5,50);
            buildHandDisplay(bottomHand, 5, 250);
        }

        /// -------------------- B U I L D   H A N D   F U N C T I O N -------------------
        private void buildHandDisplay(Hand h, int n, int yOffset)
        {
            h.cardPictureBoxes = new PictureBox[n];
            h.cards = new List<Card>();

            for (int i = 0; i < h.cardPictureBoxes.Length; i++)
            {
                PictureBox p = new PictureBox
                {
                    Location = new Point(i * 110 + 40, yOffset),
                    Size = new Size(100, 145),
                    SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
                };
                h.cardPictureBoxes[i] = p;
                this.Controls.Add(p);
            }
        }

        /// -------------------- B U I L D   F U L L    D E C K    F U N C T I O N -------------------
        private void buildFullDeck()
        {
            fullDeck = new Deck();
            fullDeck.cards = new Queue<Card>();

            String[] suits = { "hearts", "clubs", "diamonds", "spades" };
            String[] rankNames = { "ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "jack", "queen", "king" };
            String[] suffix = { "", "", "", "", "", "", "", "", "", "", "", "2", "2", "2" };

            foreach (String suit in suits)
            {
                for (int r = 1; r<=rankNames.Length; r++)
                {
                    Card c = new Card();
                    c.suit = suit;
                    c.rank = rankNames[r-1];
                    c.numberRank = r;
                    String filename = c.rank + "_of_" + c.suit + suffix[r - 1];
                    try
                    {
                        c.image = Image.FromFile("../../Images/" + filename + ".png");
                    } catch (FileNotFoundException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        fullDeck.cards.Enqueue(c);
                    }
                }
            }
        }

        /// -------------------- B U I L D   N E W   D E C K   F U N C T I O N -------------------
        private Deck newDeck()
        {
            Deck newDeck = new Deck();

            newDeck.cards = new Queue<Card>();

            foreach(Card c in fullDeck.cards)
            {
                newDeck.cards.Enqueue(c);
            }

            return newDeck;
        }

        /// -------------------- S H U F F L E   F U N C T I O N -------------------
        private void shuffle(int riffles)
        {
            Queue<Card> left = new Queue<Card>();
            Queue<Card> right = new Queue<Card>();

            for (int i = 0; i < riffles; i++) 
            {
                while(myDeck.cards.Count > 0)
                {
                    if(rng.Next(10) <5)
                    {
                        left.Enqueue(myDeck.cards.Dequeue());
                    } else
                    {
                        right.Enqueue(myDeck.cards.Dequeue());
                    }
                }
                // put them back in the deck, left half then right half

                while(left.Count > 0)
                {
                    myDeck.cards.Enqueue(left.Dequeue());
                }
                while (right.Count > 0)
                {
                    myDeck.cards.Enqueue(right.Dequeue());
                }
            }
        }

        // -------------------------------  C H E C K   P A T T E R N   F U N C T I O N  -------------------------------------
        // -- This function analyzes each hands for find special card patterns (pair, 2pairs, triple, straight, flush, fullhouse or four of a kind/poker)
        // -- patterns found are stored in cPattern list for each hand, I also use an int value hpattern in each hand to know what's the highest pattern in it
        private Dictionary<string,int> checkPattern(List<Card> h, out int hpattern )
        {
            Dictionary<string, int> pattern = new Dictionary<string, int>();
            
            hpattern = 0;
            h = h.OrderByDescending(x => x.numberRank).ToList();

            // check if player has a flush
            if (h.GroupBy(x => x.suit).Count() == 1)
                {
                    pattern.Add(h.First().suit, h.First().numberRank); // I store suit in key to recover that value later
                    hpattern = 5;
                    return pattern;
                }

            // if there are no repeated cards, let's check if it's a straight, we know there's no repetition because all cards have different rank
            if (h.GroupBy(x => x.numberRank).Count() == 5)
            {
                // let's see if cards have consecutive number ranks
                for (int i = 0/*h.Count() - 1*/; i < 4; i++)
                {
                    if (h[i].numberRank - h[i+1].numberRank != 1)
                        break;
                    else if(i==3)
                    { // we have a straight!
                        pattern.Add("straight", h.First().numberRank/*"-"*/); hpattern = 4;
                        return pattern;
                    }
                }
            }
            else
            {
                // there are cards reapeated then let's check if player has pair, 2 pairs, triple, or four of a kind
                foreach (var c in h.GroupBy(x => x.numberRank).OrderByDescending(x => x.Count()))
                {
                    switch (c.Count())
                    {
                        case 2:
                            if (pattern.ContainsKey("pair")) // if there's a pair previously then is the 2nd pair
                            {
                                pattern.Add("pair2", c.Key);
                                pattern = pattern.OrderByDescending(x => x.Value).ToDictionary(a=>a.Key,b=>b.Value);
                                hpattern = 2;
                            }
                            else 
                                if (pattern.ContainsKey("triple")) // if there's a triple previously, then now it's a full house
                                {
                                    pattern.Add("fullhouse", pattern["triple"]);
                                    pattern = pattern.OrderByDescending(x => x.Key).ToDictionary(a => a.Key, b => b.Value);
                                    hpattern = 6;
                                }
                                else  // if NO previous pair/triple then this is the first pair
                                {
                                    pattern.Add("pair", c.Key); hpattern = 1;
                                }
                            break;
                        case 3: // hand has a triple
                            pattern.Add("triple", c.Key); hpattern = 3;
                            break;
                        case 4: // I found four of a kind
                            pattern.Add("poker", c.Key); hpattern = 7;
                            break;
                        default:
                            break;
                    }
                }
            }
            return pattern;
            
        }
        // ------------------ C H E C K W I N N E R   F U N C T I O N ---------------
        // -------- compares patterns, highest cards and hand values from both players to define who's the winner
        // --- I'd like to get a more compact code for comparisons but I honestly didn't know how.
        private void checkWinner()
        {
            // CALL checkPattern to find any pattern in hands
            topHand.cPattern = checkPattern(topHand.cards, out topHand.hpattern);
            bottomHand.cPattern = checkPattern(bottomHand.cards, out bottomHand.hpattern);
            // sorting hands cards from highest to lowest card value
            topHand.cards = topHand.cards.OrderByDescending(x => x.numberRank).ToList();
            bottomHand.cards = bottomHand.cards.OrderByDescending(x => x.numberRank).ToList();

            // print hands information aside cards: HAND VALUE, highest card (HC) and highest pattern (HP), this was for me but I decided to leave it there
            pointsP1label.Text = topHand.value + " pts" + "\n HC=" + topHand.cards.First().numberRank + "\n HP= " + topHand.hpattern;
            pointsP2label.Text = bottomHand.value + " pts" + "\n HC=" + bottomHand.cards.First().numberRank + "\n HP= " + bottomHand.hpattern;

            //MessageBox.Show("patrones manos: " + topHand.hpattern + " - " + bottomHand.hpattern);
            // if at least one hand has a pattern then let's evaluate it
            if (topHand.hpattern != 0 || bottomHand.hpattern != 0)
            {
                if (topHand.hpattern > bottomHand.hpattern) // tophand pattern value is higher
                    showWinner(1, topHand.cPattern.First().Key, topHand.cPattern.First().Value, topHand.hpattern);
                else
                    if (topHand.hpattern < bottomHand.hpattern)// bottomhand pattern value is higher
                        showWinner(2, bottomHand.cPattern.First().Key, bottomHand.cPattern.First().Value, bottomHand.hpattern);
                    else // if patterns in both hands are the same so let's see their composing cards values
                        if (topHand.cPattern.First().Value > bottomHand.cPattern.First().Value) // tophand pattern value is higher
                            showWinner(1, topHand.cPattern.First().Key, topHand.cPattern.First().Value, topHand.hpattern);
                        else
                            if (topHand.cPattern.First().Value < bottomHand.cPattern.First().Value) // bottomhand pattern value is higher
                                showWinner(2, bottomHand.cPattern.First().Key, bottomHand.cPattern.First().Value, bottomHand.hpattern);
                            else // if there's composed patterns like fullhouse or two pairs let's check second pattern values
                                if (topHand.cPattern.Count() > 1 && bottomHand.cPattern.Count() > 1)
                                    if (topHand.cPattern.Last().Value > bottomHand.cPattern.Last().Value)
                                        showWinner(1, topHand.cPattern.Last().Key, topHand.cPattern.Last().Value, topHand.hpattern);
                                    else
                                        if (topHand.cPattern.Last().Value < bottomHand.cPattern.Last().Value)
                                            showWinner(2, bottomHand.cPattern.Last().Key, bottomHand.cPattern.Last().Value, bottomHand.hpattern);
                                        else // no difference between patterns values, let's check hands values to untie
                                            if (topHand.value > bottomHand.value)
                                                showWinner(1, "With highest hand", 0, topHand.hpattern);
                                            else
                                                if (topHand.value < bottomHand.value)
                                                    showWinner(2, "With Highest hand", 0, bottomHand.hpattern);
                                                else
                                                    MessageBox.Show("split the pot");
            }
            else // if there's no pattern then check for highest card and untie with hand values
                if (topHand.cards.First().numberRank > bottomHand.cards.First().numberRank)
                    showWinner(1, topHand.cards.First().suit, topHand.cards.First().numberRank, 0);
                else
                    if (topHand.cards.First().numberRank < bottomHand.cards.First().numberRank)
                        showWinner(2, bottomHand.cards.First().suit, bottomHand.cards.First().numberRank, 0);
                    else
                        if (topHand.value > bottomHand.value)
                            showWinner(1,"",0,0);
                        else
                            if (topHand.value < bottomHand.value)
                                showWinner(2,"",0,0);
                            else
                                MessageBox.Show("split the pot");

            // discard cards already played
            topHand.cards.Clear();
            bottomHand.cards.Clear();
            // reset patterns dictionaries and hpattern 
            topHand.cPattern.Clear();
            bottomHand.cPattern.Clear();
            topHand.hpattern = 0;
            bottomHand.hpattern = 0;
        }

        // -------------------------- S H O W   W I N N E R   F U N C T I O N ---------------------------
        // --- Pops up message boxes according the winner and his hand
        private void showWinner(int player, string patternName, int patternRank, int patternValue)
        {

            if (player < 1)
                MessageBox.Show("Split the pot");
            else
                foreach (Label l in this.Controls.OfType<Label>().Where(x => x.Name.StartsWith("label" + player))) // I should probably use something different here
                    l.Text += " -- H A S   W O N";
                switch (patternValue)
                {
                    case 0:
                        if (patternRank == 0)
                            MessageBox.Show("Player " + player + "WON!\nWith highest hand value");
                        else
                            MessageBox.Show("Player " + player + "WON!\nWith Highest card " + fullDeck.cards.ElementAt(patternRank - 1).rank + " of " + patternName);
                        break;
                    case 2:
                        MessageBox.Show("Player " + player + "WON!\nWith TWO Pairs"); break;
                    case 4:
                        MessageBox.Show("Player " + player + "WON!\nWith STRAIGHT"); break;
                    case 5:
                        MessageBox.Show("Player " + player + "WON!\nWith FLUSH of " + patternName); break;
                    case 6:
                        MessageBox.Show("Player " + player + "WON!\nWith FULL HOUSE of " + fullDeck.cards.ElementAt(patternRank-1).rank + "s");
                        break;
                    case 7:
                        MessageBox.Show("Player " + player + "WON!\nWith POKER of " + fullDeck.cards.ElementAt(patternRank-1).rank + "s");
                        break;
                    default:
                        MessageBox.Show("Player " + player + "WON!\nWith " + patternName + " of " + fullDeck.cards.ElementAt(patternRank-1).rank + "s");
                        break;
                }
        }


        /// ---------------------------------- D E A L  B U T T O N ----------------------------------
        /// --- It deals the players hands, it also handles re-shuffle when deck cards are not enough for a new deal
        /// 
        /// **** NOTE: I Added 2 4 lables, 1 label above each hand to show the player (label1 and label2),
        ///             and 1 to right of each hand to show some hand info (pointsP1label and pointsP2label)
        ///             I use them here, and inside checkWinner and showWinner functions.
        private void dealButton_Click(object sender, EventArgs e)
        {
            label1.Text = "P L A Y E R   1";label2.Text = "P L A Y E R  2";  
            if (dealButton.Text == "Deal") // --- This is to modify dealbutton behaviour from deal to reshuffle and viceversa
            {
                if (myDeck.cards.Count >= 10)  //-------- C O R E  T A S K  2 ----- Error/exception handling
                                               // It seemed to me that adding this count check was simpler than using try-catch
                {
                    // Hint B: either check there are enough cards left to deal (and if not, reshuffle) OR
                    // wrap the below in a try-catch block, and if there is an exception, reshuffle and re-call the button click
                    // Hint:  dealButton_Click(sender,e); will re-call the function from inside the function!
                    //shuffleButton.Visible = false;
                    // ------ C O R E   T A S K  3 ----- in this function, hands values and winner are handled
                    topHand.value = 0;bottomHand.value = 0; // --- initialize hand values every deal
                    //topHand.hcard = 0;bottomHand.hcard = 0; // --- initialize highest card value -- E X T E N S I O N  1 --

                    foreach (PictureBox cp in topHand.cardPictureBoxes)
                    {
                        Card c = myDeck.cards.Dequeue();
                        cp.Image = c.image;
                        topHand.cards.Add(c);
                        topHand.value += c.numberRank;  //-- add card value to hand value
                        cp.Visible = true;
                    }

                    foreach (PictureBox cp in bottomHand.cardPictureBoxes)
                    {
                        Card c = myDeck.cards.Dequeue();
                        cp.Image = c.image;
                        bottomHand.cards.Add(c);
                        bottomHand.value += c.numberRank;   // -- add card value to hand value
                        cp.Visible = true;
                    }
                    checkWinner(); // once both hands are dealt, find out who wins!
                }
                else  //--- C O R E   T A S K  2 -- If cards aren't enough for new hands, then force user to reshuffle
                {
                    dealButton.Text = "Re-shuffle";
                    //shuffleButton.Visible = true;
                    MessageBox.Show("Not enough cards, re-shuffle the deck");
                }
            }
            else
            {   // --- Clean up the table and reshuffle, then you can deal again
                foreach (PictureBox cp in topHand.cardPictureBoxes)
                    cp.Visible = false;
                foreach (PictureBox cp in bottomHand.cardPictureBoxes)
                    cp.Visible = false;
                //--- resuffle was done initially by another button as requested in core task 1
                myDeck = newDeck();
                shuffle(3);
                dealButton.Text="Deal";
                pointsP1label.Text = "--";
                pointsP2label.Text = "--";
            }
        }

        // ----- C O R E   T A S K  1 ---- reshuffle button
        // --- In the end I decided to use dealbutton to reshuffle
        private void shuffleButton_Click(object sender, EventArgs e)
        {
            //    myDeck = newDeck();
            //    shuffle(7);
            //    shuffleButton.Visible = false;
        }
    }
}

class Deck
{
    public Queue<Card> cards;
}

class Hand
{                          // -- E X T E N S I O N   1 -- adding hcard/hcardo to break ties, I ended up sorting hands by rank to get highest cards
    public List<Card> cards;
    public PictureBox[] cardPictureBoxes;
    // we're going to use this diccionary to store patterns in hand, and will save highest pattern in hpattern to compare players highest patterns
    public Dictionary<string,int> cPattern;
    public int hpattern;                    // 0: NO pattern 1: first pair, 2: second pair, 3:triple, 4: straight, 5: flush, 6: full house, 7:four of a kind
    public int value;                       // this property is added to store hand value
    //public Card hcardo;                     // I added an object that way I could untie using suit values -- NOT USED AT THE END
    //public int hcard;                     //Initially I used this int but it's not enough if highest card in both hands have same rank
}

class Card
{
    public string suit;
    public string rank;
    public int numberRank;
    public Image image;
}
