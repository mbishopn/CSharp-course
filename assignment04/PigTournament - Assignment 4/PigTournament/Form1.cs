using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace PigTournament
{


    public partial class Form1 : Form
    {

        // List of players

        List<IPigPlayer> playerList;

        public Form1()
        {
            InitializeComponent();
            rrgTextbox.Text = "10"; // --- C O R E   T A S K   1 ---- set default rr games number



        }

        private void StartTournamentButton_Click(object sender, EventArgs e)
        {

            // Random rng = new Random();

            // Create four players, of three separate classes
            // We can create as many players of different classes and different settings
            // and put as many as we want in the playerList.

            PPAlwaysStick p1 = new PPAlwaysStick("Stick");
            PPRollNTimes p2 = new PPRollNTimes("Roll2", 2);
            PPRandomStick p3 = new PPRandomStick("R.33", /*rng,*/ 0.33);
            PPRandomStick p4 = new PPRandomStick("R.50", /*rng,*/ 0.50);

            // simple players added
            PPNppt p5 = new PPNppt("25 ppt", 25);
            PPstopIfXNTimes p6 = new PPstopIfXNTimes("x3T", 3);
            PPfrontR p7 = new PPfrontR("FrontR");
            PPkeepClose p8 = new PPkeepClose("K-I-C", 7);

            // specialized players added
            PPspec01 p9 = new PPspec01("1st25-75go", new PPNppt("s1", 25));
            PP4sT p10 = new PP4sT("4 scoring Turns", new PPNppt("s1",25));
            //PPRandomStick p7 = new PPRandomStick("R.33", /*rng,*/ 0.33);
            //PPRandomStick p8 = new PPRandomStick("R.50", /*rng,*/ 0.50);
            //PPRandomStick p9 = new PPRandomStick("R.33", /*rng,*/ 0.33);
            //PPRandomStick p10 = new PPRandomStick("R.50", /*rng,*/ 0.50);


            // List of players, and a dictionary that will hold results.
            playerList = new List<IPigPlayer>() { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10};

            // Two-player tournament

            // how many games to play //--------- C O R E   T A S K   1 ----------  customizable number of games to play
            int roundRobinGames = Convert.ToInt32(rrgTextbox.Text);

            PigTournament t = new PigTournament(playerList, roundRobinGames); // creating a tournament object

            savetournament savegame = new savetournament();

            try
            {
                // ---------------- E X T E N S I O N   5 ------------- serialization, things went complicated trying to save my tournament results object, so I made it simpler, saving a little object with a string inside.
                //// Read fraction c from disk
                using (FileStream fs = new FileStream("../../save2.dat", FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    savegame = (savetournament)bf.Deserialize(fs);

                    fs.Close();

                }
            }
            catch { MessageBox.Show("No previous tournaments saved on dics, creating file."); }

            tournamentReport.Text = "LAST TOURNAMENT RESULTS\r\n\r\n" + savegame.sresults; // Print saved through serialization last tournament results.
            MessageBox.Show("CONTINUE");
            t.start(); // let the tournament start!


            tournamentReport.Text= "TOURNAMENT RESULTS TO BE SAVED\r\n\r\n" + t.results(); // Print tournament results.

            savegame.sresults = t.results();
            using (FileStream fs = new FileStream("../../save2.dat", FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, savegame);
                fs.Close();

            }
        }
        
    }

    
    //--------- C O R E   T A S K   3 ----------- class to handle tournaments
    class PigTournament 
    {
        private List<IPigPlayer> players;
        private Dictionary<IPigPlayer, int> winList = new Dictionary<IPigPlayer, int>();
        private int roundrobingames;
        private TournamentResults tRes = new TournamentResults();
        //private TournamentResults stRes = new TournamentResults();
        //TournamentResults stRes;

        //private TournamentResults stRes = new TournamentResults();
        // ------ T O U R N A M E N T   C O N S T R U C T O R --------
        public PigTournament(List<IPigPlayer> plist,int rrg)
        {
            players = plist;
            roundrobingames = rrg;
        }
        // -------- S T A R T   T O U R N A M E N T   M E T H O D ------
        public void start()
        {
            //TournamentResults tRes = new TournamentResults();
            int j = 0;
            // Use the playerList to set up pairs of two-player games. 
            foreach (IPigPlayer p in players)
            {
                j++; int k = 0;
                foreach (IPigPlayer q in players)
                {
                    k++;
                    // players don't play themselves.
                    if (p == q) continue;

                    // Run roundRobinGames number of games
                    for (int i = 0; i < roundrobingames; i++)
                    {
                        //MessageBox.Show(" jugador " + j + "op " + k + " juego " + (i + 1)); //----------------------------------------------------- verifier
                        // Create a new game
                        PigGame g = new PigGame(new List<IPigPlayer>() { p, q }/*, rng*/);

                        // PLay the game
                        g.playGame();

                        // get the winner
                        IPigPlayer w = g.getWinner();

                        //MessageBox.Show(p.getName() + " vs " + q.getName() + ": winner " + w.getName());
                        // each player has 1 exclusive entry in the log for game results
                        if (w == p)
                        { tRes.addGame(p, q, true); tRes.addGame(q, p, false); }
                        else
                        { tRes.addGame(p, q, false); tRes.addGame(q, p, true); }

                        //// Store the winner's win in the winList
                        if (winList.ContainsKey(w))
                        {
                            winList[w] = winList[w] + 1;
                        }
                        else
                        {
                            winList.Add(w, 1);
                        }
                    }
                }
            }

        }
        // -------- T O U R N A M E N T   R E P O R T   S T R I N G ---------
        public string results()
        {
            //MessageBox.Show(tRes.gamescount + "");

            //TournamentResults stRes;
            //if (true)
            //{
            //    //MessageBox.Show("voy a guardar " + tRes.reslist.Count.ToString() + " registros " + tRes.sgamescount + " " + tRes.rgamescount);

            //    using (FileStream fs = new FileStream("../../save.dat", FileMode.Create))
            //    {
            //        BinaryFormatter bf = new BinaryFormatter();
            //        bf.Serialize(fs, tRes);
            //        fs.Close();

            //    }
            //}
            //// Read fraction c from disk

            //using (FileStream fs = new FileStream("../../save.dat", FileMode.Open))
            //{
            //    BinaryFormatter bf = new BinaryFormatter();
            //    tRes = (TournamentResults)bf.Deserialize(fs);

            //    fs.Close();

            //}
            //MessageBox.Show(tRes.gamescount + "");
            //stRes.reslist =tRes.Results();
            //MessageBox.Show(stRes.gamescount + "");
            //MessageBox.Show("despues serialization " + tRes.reslist.Count.ToString() + " " + tRes.rgamescount + " " + tRes.rgamescount);

            string s ="";
            //---------- substitue of winingList
            foreach(var p in winList.OrderByDescending(x=>x.Value))
            {
                s += p.Key.getName() + " " + p.Value + "\r\n"; 
            }
            s += "\r\n\r\n";

            foreach (IPigPlayer p in players)
            {
                s += p.ToString() + "\t ";
                s += tRes.Results().FindAll(x => x.p == p).Count().ToString() + " Games\t";
                s += "Total W-L: " + tRes.Results().FindAll(x => x.p == p && x.iswin).Count().ToString();
                s += " - " + tRes.Results().FindAll(x => x.p == p && !x.iswin).Count().ToString();
                s+="\r\nPER OPPONENT\t\tW - L\r\n";
                if (players.Count() > 4)
                {
                    foreach (IPigPlayer q in players.FindAll(x => x != p))
                    {
                        s += /*p.getName() + "*/ "vs " + q.getName() + "\t\t";
                        s += tRes.Results().FindAll(x => x.p == p && x.o == q && x.iswin).Count + " - ";
                        s += tRes.Results().FindAll(x => x.p == p && x.o == q && !x.iswin).Count + "\r\n";
                    }
                }
                else
                {
                    foreach (IPigPlayer q in players.FindAll(x => x != p))
                    {
                        s += /*p.getName() + "*/ "vs " + q.getName() + "\t\t";
                    }
                    s += "\r\n";
                    foreach (IPigPlayer q in players.FindAll(x => x != p))
                    {
                        s += tRes.Results().FindAll(x => x.p == p && x.o == q && x.iswin).Count + "  -  " + tRes.Results().FindAll(x => x.p == p && x.o == q && !x.iswin).Count + "\t\t";
                    }
                }
                s += "\r\n";
            }
            
            return s;
        }

        



    }


    ///   -------- I'm sorry, I was trying to save my tournamentresults object or at least the list, but it seems that all must be serializable and since I
    ///   was referencing IPigPlayer, those and all related objects clases should be serializable too. So I decided to simply create an object holding a string with
    ///   results.
    [Serializable]
    class savetournament : ISerializable
    {
        public string sresults { get; set; }
        public savetournament()
        { }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("tournamentResults", sresults, typeof(string));
        }

        public savetournament(SerializationInfo info, StreamingContext context)
        {
            sresults = (string)info.GetValue("tournamentResults", typeof(string));
        }

    }

    [Serializable]
    //--------- C O R E   T A S K   4 ----------- class for tournament reporting
    class TournamentResults //: ISerializable
    {
        public IPigPlayer p { get; private set; }
        public IPigPlayer o { get; private set; }
        public bool iswin { get; private set; }
        // I thought I need to have all about results inside this class
        // Now, I know there are better ways to do this, such as creating a class to store the information
        // of each game and then creating an external list with these objects, but I was determined 
        // to know if it was possible to have a list of objects in the same class as the objects it contains,
        // I found that it's weird and maybe wrong, but it can be used.
        private static List<TournamentResults> _reslist = new List<TournamentResults>();
        public TournamentResults()              // I put this constructor to test / undestand serialization
        {  }
        public TournamentResults(List<TournamentResults> l) // I used this to test / understand serialization
        {
            reslist = l;
        }
        public List<TournamentResults> reslist      // I used this to test / understand serialization
        {
            get => _reslist;
            set
            {
                _reslist = value;
            }
        }

        public void clearList()         // I used this to test / understand serialization
        {
            _reslist.Clear();
        }

        private static int _sgamescount=0;
        public int sgamescount
        {
            get => _sgamescount;
            set
            {
                _sgamescount = value;
            }
        }
        private static int _rgamescount = 0;
        public int rgamescount
        {
            get => _rgamescount;
            set
            {
                _rgamescount = value;
            }
        }

        public void addGame(IPigPlayer p, IPigPlayer o, bool res)
        {
            TournamentResults tmp = new TournamentResults();
            tmp.p = p;
            tmp.o = o;
            tmp.iswin = res;
            reslist.Add(tmp);
        }


        public List<TournamentResults> Results()
        {
            return reslist;
        }
        
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("player", p, typeof(IPigPlayer));
            info.AddValue("opponent", o, typeof(IPigPlayer));
            info.AddValue("iswin", iswin, typeof(bool));
            info.AddValue("reslist", reslist, typeof(List<TournamentResults>));
            this.sgamescount++;
        }
        // For custom deserialization we need a constructor with exactly this signature
        // which can get the key data back out of the SerializationInfo object.
        public TournamentResults(SerializationInfo info, StreamingContext context)
        {
            p = (IPigPlayer)info.GetValue("player", typeof(IPigPlayer));
            o = (IPigPlayer)info.GetValue("opponent", typeof(IPigPlayer));
            iswin = (bool)info.GetValue("iswin", typeof(bool));
            reslist = (List<TournamentResults>)info.GetValue("reslist", typeof(List<TournamentResults>));
            this.rgamescount++;
        }


    }
    // Class that represents a pig game
    class PigGame
    {
        // An array of players.
        PigGamePlayer[] players;

        // Count of players
        int playerCount;

        // current player
        int currentPlayer;

        // score for the currentplayer so far this turn
        int currentPlayerScore;

        // current roll value
        int currentRoll;

        // Random number generator -- don't want to create a new one for each game
        
        Random rng;

        public PigGame(List<IPigPlayer> playerList/*, Random r*/)
        {
            // Build our player array from the playerList

            this.playerCount = playerList.Count;
            this.players = new PigGamePlayer[this.playerCount];

            for (int i = 0; i < playerCount; i++)
            {
                players[i] = new PigGamePlayer(playerList[i]);
            }

            //rng = r;
            rng = new Random();

        }

        public void playGame()
        {
            this.currentPlayer = -1;  // so that the first player will be 0
            this.currentPlayerScore = 0;

            // A game consists of playing Turns until the player that is about to 
            // play *already* is over 100.  That is, after someone gets to 100, everyone else gets one more turn

            while (this.players[this.nextPlayer()].getScore() < 100)
            {
                //MessageBox.Show("Juega " + players[currentPlayer].getPlayer().getName()); // ------------------------------------------- checkpoint
                this.playTurn();
            }

            // IT WAS UNTIL TASK ABOUT CREATING PLAYERS THAT I UNDERSTOOD HOW THE WINNER WAS DEFINED
            //do
            //{
            //    currentPlayer = nextPlayer();
            //    this.playTurn();
            //} while (this.players[currentPlayer].getScore() <= 100);

            // At this point we have a winner
        }

        // I SHOULD HAVE PAID ATTENTION TO NEXT COMMENT TO PREVENT WASTING MY TIME!! :D
        // The winner is the player with the highest score, not just the first player to get 100 or more
        public IPigPlayer getWinner()
        {
            int bestScore = 0;
            IPigPlayer winner = null;

            foreach (PigGamePlayer p in players)
            {
                if (p.getScore() > bestScore)
                {
                    bestScore = p.getScore();
                    winner = p.getPlayer();
                }
            }
            return winner;
        }

        // Helpful utility function to change the current player
        protected int nextPlayer()
        {
            currentPlayer += 1;

            if (currentPlayer == playerCount)
            {
                currentPlayer = 0;
            }
            return currentPlayer;
        }

        // Play a turn
        protected void playTurn()
        {
            // Score starts at 0
            currentPlayerScore = 0;
            // Keep going until we stop
            while (true)
            {
                // Roll a number
                int roll = rng.Next(6) + 1;

                // If it was  a 1, turn is over, and the player scores 0 for the entire turn.
                if (roll == 1)
                {
                    // current player scores 0, turn is over
                    //players[currentPlayer].turnScore(0); // ----------------- modificacion temp mia
                    currentPlayerScore = 0;

                    //break; ----------------- modificacion temp mia

                    //MessageBox.Show("Turn lost");  // ---------------------------------------------------------- checkpoint
                }
                else
                // For any other roll, increment the current player Score
                currentPlayerScore += roll;

                currentRoll = roll;
                //MessageBox.Show("I got " + roll + ", so far this turn " + currentPlayerScore); // --------------------------------- checkpoint
                // Need to build the current state of the game so players have lots 
                // of information on which to base their decisions
                PigGameState g = buildGameState();
                // Ask the player what to do
                bool rollAgain = players[currentPlayer].getPlayer().toRollOrNotToRoll(g);

                // Player is stopping, set their score
                if (!rollAgain)
                {
                    //MessageBox.Show("turn ends"); // ----------------------------------------------------------- checkpoint
                    players[currentPlayer].turnScore(currentPlayerScore);
                    break;
                }
            }

        }

        // One way to build a game state.  It could include more history if we wanted it to.

        protected PigGameState buildGameState()
        {
            PigGameState g = new PigGameState();
            g.currentRoll = currentRoll; // required by some players
            g.playerCount = playerCount;
            g.currentPlayerScoreThisTurn = currentPlayerScore;
            g.currentPlayerScore = players[currentPlayer].getScore();
            g.playerScores = new int[playerCount];
            for (int i = 0; i < playerCount; i++)
            {
                g.playerScores[i] = players[i].getScore();
            }

            return g;

        }

        // Utility function to get a string for an individual game score.
        public string prettyScores()
        {
            String s = "";

            foreach (PigGamePlayer p in players)
            {
                s += p.getPlayer().getName() + " (" + p.getPlayer().getType() + ") " + p.getScore() + "      ";
            }

            return s;
        }

    }

    // Represents the state of a player *during* a game (rather than their strategy)
    class PigGamePlayer
    {
        // Player strategy
        IPigPlayer player;

        // Score
        int score;

        // Score per turn
        List<int> turnScores;

        public PigGamePlayer(IPigPlayer p)
        {
            player = p;
            score = 0;
            turnScores = new List<int>();
        }

        // Add the score for their turn to their total and to the list of scores by turn.
        public void turnScore(int s)
        {
            score += s;
            turnScores.Add(s);
        }

        public int getScore()
        {
            return score;
        }

        public IPigPlayer getPlayer()
        {
            return player;
        }
    }

    class PigGameState
    {
        public int playerCount { get; set; }
        public int[] playerScores { get; set; }
        public int currentPlayerScore { get; set; }
        public int currentPlayerScoreThisTurn { get; set; }
        public int currentRoll { get; set; } // Player wants to decide based on current roll value
    }

    // Player strategy interface.
    // need to be able to get a name, a type, and a decision on whether to roll or not.
    interface IPigPlayer
    {
        bool toRollOrNotToRoll(PigGameState s);
        string getName();
        string getType();
    }

    // abstract base class that handles name and type, but leaves toRollOrNotToRoll for the children
    abstract class BasePigPlayer : IPigPlayer
    {
        abstract public bool toRollOrNotToRoll(PigGameState s);

        protected string _name;
        protected string _type;

        public BasePigPlayer(string n)
        {
            _name = n;
            _type = "Base Player";
        }

        public string getName()
        {
            return _name;
        }

        public string getType()
        {
            return _type;
        }
        /// -------------- C O R E   T A S K   2 --------- joining player's name & type to be shown by an overriden tostring() function.
        public override string ToString()
        {
            return _name + "\tStrategy: " + _type ;
        }

    }

    // This player never rolls again.

    class PPAlwaysStick : BasePigPlayer
    {
        public PPAlwaysStick(string n) : base(n)
        {
            _type = "Always Stick";
        }

        // Ignore the game state, we *always* return false and never roll again.
        public override bool toRollOrNotToRoll(PigGameState s)
        {
            return false;
        }

    }

    // This player randomly rolls again based on the random number generator
    class PPRandomStick : BasePigPlayer
    {
        // Threshold to roll again
        protected Random rng;
        protected double threshold;
        
        // Set my variables
        public PPRandomStick(string n, /*Random r,*/ double t) : base(n)
        {
            _type = "Random Stick";
            //rng = r;
            rng = new Random();
            threshold = t;
        }

        // If the random number is below threshold, roll, otherwise, don't
        public override bool toRollOrNotToRoll(PigGameState s)
        {
            if (rng.NextDouble() < threshold)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }


    // A player that tries to roll N times if they don't roll a 1 first.
    class PPRollNTimes : BasePigPlayer
    {
        // Need to know how many times I've rolled this turn, and also how many times I want to roll.
        protected int _desiredRolls;
        protected int rollsThisTurn = 0;
        protected int lastCurrentPlayerScoreThisTurn = -1;
        protected int lastCurrentPlayerScore = -1;

        public PPRollNTimes(string n, int j) : base(n)
        {
            _type = "Roll N Times";
            _desiredRolls = j;
        }

        public override bool toRollOrNotToRoll(PigGameState s)
        {
            // FIXED, now allowing gamestate with currentPlayerScoreThisTurn 0, we can prevent
            // the exception by resetting rollsthisturn.

            // if currentPlayerScoreThisTurn is different than lastCurrentPLayerScoreThisTurn
            // and my currentScore has changed
            // then it is a new turn  
            // (almost -- possible that I rolled a 2, chose to roll again, failed, and on my *next* turn rolled a 2.
            // But that should only happen once every 36 turns on average, so close enough
                
            if (s.currentPlayerScore != lastCurrentPlayerScore && s.currentPlayerScoreThisTurn != lastCurrentPlayerScoreThisTurn)
            {
                rollsThisTurn = 1; //MessageBox.Show("tiro 1"); -- verifier
            } else
            {
                rollsThisTurn += 1; //MessageBox.Show("tiro 2"); --- verifier
            }

            // store the currentPlayer score
            lastCurrentPlayerScoreThisTurn = s.currentPlayerScoreThisTurn;
            lastCurrentPlayerScore = s.currentPlayerScore;

            // If I haven't rolled enough times yet, then roll
            if (rollsThisTurn < _desiredRolls && s.currentPlayerScoreThisTurn!=0)
            {
                return true;
            }
            rollsThisTurn = 0;
            // Otherwise, end the turn and take my score
            return false;
        }

    }
    
    // ------------- C O R E   T A S K   5 ---------- CREATING 5 DIFFERENT PLAYERS
    class PPNppt : BasePigPlayer // --------------------- simple player rolls to get N points per turn
    {
        // Need to know how many points want to reach to stop rolling.
        protected int desiredScore;

        public PPNppt(string n, int j) : base(n)
        {
            _type = "N Points per Turn";
            desiredScore = j;
        }

        public override bool toRollOrNotToRoll(PigGameState s) 
        {
            if (s.currentPlayerScoreThisTurn >= desiredScore||s.currentPlayerScoreThisTurn==0)
                return false;
            //MessageBox.Show("keep rolling");  // ------------------------------------------------------------------------------verifier
            return true;
        }

    } 
    
    class PPstopIfXNTimes : BasePigPlayer   // simple player continue until rolling same number N times consecutively
    {
        // Need to know how many times I've rolled this turn, and also how many times I want to roll.
        protected int maxApp;
        protected int appearances = 1;
        protected int lastCurrentRoll = -1;
        public PPstopIfXNTimes(string n, int j) : base(n)
        {
            _type = "Roll if XNT";
            maxApp = j;
        }

        public override bool toRollOrNotToRoll(PigGameState s)
        {
            if(s.currentRoll != 1 && s.currentPlayerScore<30 && s.currentPlayerScoreThisTurn<30)
                if (s.currentRoll != lastCurrentRoll)
                {
                    //MessageBox.Show("1st time keep rolling"); // ------------------------------------------------------------------- verifier
                    lastCurrentRoll = s.currentRoll;
                    return true;
                }
                else
                {
                    appearances++;
                    if (appearances < maxApp)
                    {
                        //MessageBox.Show(appearances +" times " + s.currentRoll + ", keep rolling"); // ------------------------------------------------- verifier
                        //lastCurrentPlayerScoreThisTurn = s.currentPlayerScoreThisTurn;
                        return true;
                    }
                }

            lastCurrentRoll = -1;
            appearances = 1; // reset reps counter before ending this turn
            // Otherwise, end the turn and take my score
            return false;
        }

    }

    class PPfrontR : BasePigPlayer   // simple player always wanting to be ahead
    {
        // Need to know how many times I've rolled this turn, and also how many times I want to roll.
        public PPfrontR(string n) : base(n)
        {
            _type = "Roll if XNT";
        }

        public override bool toRollOrNotToRoll(PigGameState s)
        {
            { }

            if (s.currentRoll != 1 && s.currentPlayerScore < s.playerScores.Max() && s.currentPlayerScore + s.currentPlayerScoreThisTurn <= s.playerScores.Max())
                return true;
            // Otherwise, end the turn and take my score
            return false;
        }

    }

    class PPkeepClose : BasePigPlayer   // simple player wants to be within N points behind leader
    {
        int maxDistance;

        // Need to know how many times I've rolled this turn, and also how many times I want to roll.
        public PPkeepClose(string n, int j) : base(n)
        {
            _type = "Roll if XNT";
            maxDistance=j;
        }

        public override bool toRollOrNotToRoll(PigGameState s)
        {
            if (s.currentRoll != 1 && s.currentPlayerScore < s.playerScores.Max()-maxDistance && s.currentPlayerScore + s.currentPlayerScoreThisTurn < s.playerScores.Max()-maxDistance)
                return true;
            // Otherwise, end the turn and take my score
            return false;
        }

    }

    class PPspec01 : BasePigPlayer   // specialized player, 1st turn tries to get over 25, and when gets over 75 keeps rolling till winning
    {
        static bool turn1=true;
        IPigPlayer strategy1;
        public PPspec01(string n, IPigPlayer s1) : base(n)
        {
            _type = "25 1st turn, roll to win after 85";
            strategy1 = s1;
        }

        public override bool toRollOrNotToRoll(PigGameState s)
        {
            if (turn1)
            {
                if (strategy1.toRollOrNotToRoll(s))
                { turn1 = true; return true; }turn1 = false;
            }
            if (s.currentRoll != 1 && s.currentPlayerScore >= 85 && s.currentPlayerScore+s.currentPlayerScoreThisTurn<100)
                return true;
            // Otherwise, end the turn and take my score
            return false;
        }

    }

    class PP4sT : BasePigPlayer   // specialized player, 4 scoring turns, holds at 25 first turn, then subsequents turns divides amount needed
    {                             // to reach 100 by the number of remaining turns. if can't win the first 4 turns, repeats the cycle
        static bool turn1 = true;
        int ttp = 4;
        IPigPlayer strategy1;
        public PP4sT(string n, IPigPlayer s1) : base(n)
        {
            _type = "4 scoring turns";
            strategy1 = s1;
        }

        public override bool toRollOrNotToRoll(PigGameState s)
        {
            if (turn1)
            {
                if (strategy1.toRollOrNotToRoll(s))
                { turn1 = true; return true; }
                turn1 = false;ttp--;
            }
            if (ttp != 0 && s.currentRoll != 1 && s.currentPlayerScoreThisTurn < ((100 - s.currentPlayerScore) / ttp) && s.currentPlayerScore + s.currentPlayerScoreThisTurn <= 100)
            { return true; }
            else { ttp--; if (ttp == 0) ttp = 4; }
            
            // Otherwise, end the turn and take my score
            return false;
        }

    }

    class PPmetaP : BasePigPlayer   // specialized player, multi-strategy player
    {                             
        static bool turn1 = true;
        int ttp = 4;
        IPigPlayer strategy1;
        public PPmetaP(string n, IPigPlayer s1) : base(n)
        {
            _type = "4 scoring turns";
            strategy1 = s1;
        }

        public override bool toRollOrNotToRoll(PigGameState s)
        {
            if (turn1)
            {
                if (strategy1.toRollOrNotToRoll(s))
                { turn1 = true; return true; }
                turn1 = false; ttp--;
            }
            if (ttp != 0 && s.currentRoll != 1 && s.currentPlayerScoreThisTurn < ((100 - s.currentPlayerScore) / ttp) && s.currentPlayerScore + s.currentPlayerScoreThisTurn <= 100)
            { return true; }
            else { ttp--; if (ttp == 0) ttp = 4; }

            // Otherwise, end the turn and take my score
            return false;
        }

    }

    // ------- E X T E N S I O N   4 ------- I haven't done super exhaustive testing, but as far as I've seen the best performer was (surprisingly to me) player Nppt - 25
    // ----- I thought 4sT would do better as I read that strategy was slitghtly better (0.9%) than "hold at 25" but at least in my setup it wasn't like that. Unless, of course
    // ----- I understood/sat up wrong strategy for 4sT. Why does this happen? well, I suppose it's all about odds calculation. According to wikipedia article about Pig Game, "Hold at 25" is
    // ----- one of the most effective strategies.


}
