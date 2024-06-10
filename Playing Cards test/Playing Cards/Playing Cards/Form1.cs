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

            // create the two hands and a the pictureboxes to display their cards
            topHand = new Hand();
            bottomHand = new Hand();

            buildHandDisplay(topHand,5,50);
            buildHandDisplay(bottomHand, 5, 250);
        }

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

        private void dealButton_Click(object sender, EventArgs e)
        {

            // Hint B: either check there are enough cards left to deal (and if not, reshuffle) OR
            // wrap the below in a try-catch block, and if there is an exception, reshuffle and re-call the button click
            // Hint:  dealButton_Click(sender,e); will re-call the function from inside the function!

            foreach (PictureBox cp in topHand.cardPictureBoxes)
            {
                Card c = myDeck.cards.Dequeue();
                cp.Image = c.image;
                topHand.cards.Add(c);
            }
            foreach (PictureBox cp in bottomHand.cardPictureBoxes)
            {
                Card c = myDeck.cards.Dequeue();
                cp.Image = c.image;
                bottomHand.cards.Add(c);
            }
            
        }
    }
}

class Deck
{
    public Queue<Card> cards;
}

class Hand
{
    public List<Card> cards;
    public PictureBox[] cardPictureBoxes;
}

class Card
{
    public string suit;
    public string rank;
    public int numberRank;
    public Image image;
}
