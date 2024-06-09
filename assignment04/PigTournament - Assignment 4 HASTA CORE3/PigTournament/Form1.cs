using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            PPAlwaysStick p1 = new PPAlwaysStick("S");
            PPRollNTimes p2 = new PPRollNTimes("Roll2", 2);
            PPRandomStick p3 = new PPRandomStick("R.33", /*rng,*/ 0.33);
            //PPAlwaysStick/*PPRandomStick*/ p3 = new PPAlwaysStick("otro");// PPRandomStick("R.33", /*rng,*/ 0.33);
            PPRandomStick p4 = new PPRandomStick("R.50", /*rng,*/ 0.50);


            // List of players, and a dictionary that will hold results.
            playerList = new List<IPigPlayer>() { p1, p2, p3, p4 };
            //Dictionary<IPigPlayer, int> winList = new Dictionary<IPigPlayer, int>();

            // Two-player tournament

            // how many games to play //--------- C O R E   T A S K   1 ----------  customizable number of games to play
            int roundRobinGames = Convert.ToInt32(rrgTextbox.Text);

            PigTournament t = new PigTournament(playerList, roundRobinGames); // creating a tournament object

            //// Use the playerList to set up pairs of two-player games. 
            //foreach (IPigPlayer p in playerList)
            //{
            //    foreach (IPigPlayer q in playerList)
            //    {
            //        // players don't play themselves.
            //        if (p == q) continue;

            //        // Run roundRobinGames number of games
            //        for (int i = 0; i < roundRobinGames; i++)
            //        {
            //            // Create a new game
            //            PigGame g = new PigGame(new List<IPigPlayer>() { p, q }/*, rng*/);

            //            // PLay the game
            //            g.playGame();

            //            // get the winner
            //            IPigPlayer w = g.getWinner();

            //            // Store the winner's win in the winList
            //            if (winList.ContainsKey(w))
            //            {
            //                winList[w] = winList[w] + 1;
            //            }
            //            else
            //            {
            //                winList.Add(w, 1);
            //            }

            //        }
            //    }
            //}

            t.start(); // let the tournament start!
            tournamentReport.Text= t.results();
            
            // Report the results of the tournament in the tournamentReport textbox.
            //tournamentReport.Text = t.results();
            //tournamentReport.Text = "";
            //foreach (var kvp in winList.OrderByDescending(x=>x.Value))// show results sorted by points
            //{
            //    IPigPlayer p = (IPigPlayer)kvp.Key;
            //    //string s = p.getName() + " (" + p.getType() + ").               Wins: " + kvp.Value + "\r\n";
            //    // --------- C O R E   T A S K   2 ---------- Using BasePlayer overriden function toString()
            //    string s = p.ToString() + ".\t\tWins: " + kvp.Value + "\r\n";
            //    tournamentReport.Text += s;
            //}
 
            

        }

    }

    //--------- C O R E   T A S K   3 ----------- class to handle tournaments
    class PigTournament
    {
        private List<IPigPlayer> players;
        private Dictionary<IPigPlayer, int> winList = new Dictionary<IPigPlayer, int>();
        private int roundrobingames;
        private TournamentResults tRes = new TournamentResults();

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
                        //MessageBox.Show(" jugador " + j + "op " + k + " juego " + (i + 1));
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

                        // Store the winner's win in the winList
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
            //foreach (IPigPlayer p in players)
            //{
            //    //            MessageBox.Show(tRes.Results(p));

            //    MessageBox.Show(results());
            //    //MessageBox.Show(g.p.getName() + " vs " + g.o.getName() + " winner is " + (g.iswin ? g.p.getName() : g.o.getName()));
            //        // s = g.p.getName() + " vs " + g.o.getName() + " winner is " + (g.iswin ? g.p.getName() : g.o.getName());

            //    // MessageBox.Show(g.p.getName() + " vs " + g.o.getName() + (g.iswin ? g.p.getName() : g.o.getName()));
            //}  // show all tournament results

        }
        // -------- T O U R N A M E N T   R E P O R T   S T R I N G ---------
        public string results()
        {
            string s="";
            foreach (IPigPlayer p in players)
            {
                //MessageBox.Show(p.getName());
                // all wins for this player
                s += "------------------------------------------------------------------------------------------------------------------------------------\r\n";
                s += p.getName() + "\tStrategy: " + p.getType().ToString() + "\t ";
                s += tRes.Results().FindAll(x => x.p == p).Count().ToString() + " Games\t";
                s += "Total W-L: " + tRes.Results().FindAll(x => x.p == p && x.iswin).Count().ToString();
                s += " - " + tRes.Results().FindAll(x => x.p == p && !x.iswin).Count().ToString() + "\r\nPer Opp\t\tW-L\r\n";
                foreach (IPigPlayer q in players.FindAll(x => x != p))
                {
                    s += /*p.getName() + "*/ "vs " + q.getName() + "\t\t" + tRes.Results().FindAll(x => x.p == p && x.o == q && x.iswin).Count + " - ";
                    s += tRes.Results().FindAll(x => x.p == p && x.o == q && !x.iswin).Count + "\r\n";
                }
                s += "\r\n";
            }
            //// Report the results of the tournament in the tournamentReport textbox.
            //string s="";

            ////tournamentReport.Text = "";
            ////foreach (var kvp in winList.OrderByDescending(x => x.Value))// show results sorted by points
            ////{
            ////    IPigPlayer p = (IPigPlayer)kvp.Key;
            ////    //string s = p.getName() + " (" + p.getType() + ").               Wins: " + kvp.Value + "\r\n";
            ////    // --------- C O R E   T A S K   2 ---------- Using BasePlayer overriden function toString()
            ////    s += p.ToString() + ".\t\tWins: " + kvp.Value + "\r\n";
            ////    //tournamentReport.Text += s;
            ////}
            //MessageBox.Show(s);
            return s;

        }


    }

    //--------- C O R E   T A S K   4 ----------- class for tournament reporting
    class TournamentResults
    {
        public IPigPlayer p;
        public IPigPlayer o;
        public bool iswin;
        // I know this list shouldn't be here, but I understood we need to have all about results inside this class
        static List<TournamentResults> reslist = new List<TournamentResults>();

        public void addGame(IPigPlayer p, IPigPlayer o, bool res)
        {
            TournamentResults tmp = new TournamentResults();
            tmp.p = p;
            tmp.o = o;
            tmp.iswin = res;
           // MessageBox.Show("saving "+p.getName() + " vs " + o.getName() + " - winner " + (res ? p.getName() : o.getName()));
            reslist.Add(tmp);
        }

        public List<TournamentResults> Results(/*IPigPlayer p*/)
        {
            //string s = p.getName()+" resultados ";
            //string s = p.getName();
            //foreach (var m in tRes.FindAll(x => x.o != p))

            //    s += m.p.getName() +" "+ m.o.getName() +" "+ m.iswin;
            //foreach (var m in tRes.FindAll(x => x.o != p).GroupBy(x=>x.o))
            //
            //    s += m.Key.getName() + " : ";
            //    //s += tRes.FindAll(x => x.p == p && x.o == o && x.iswin).Count()+"-";
            //    //s += tRes.FindAll(x => x.p == p && x.o == o && !x.iswin).Count() + "\n";
            //}
          //  MessageBox.Show(tRes.Count() + " registros");
            //foreach(var g in tRes.FindAll(x=>x.p==p))
            //    MessageBox.Show("PRINTING " + g.p.getName() + " vs " + g.o.getName() + " - winner " + (g.iswin ? g.p.getName() : g.o.getName()));

            return reslist;
        }

        //------------------- aqui empieza el segundo intento
        //Dictionary<IPigPlayer, Dictionary<int, IPigPlayer>> tRes = new Dictionary<IPigPlayer, Dictionary<int,IPigPlayer>>();

        //public void addGame(IPigPlayer p, IPigPlayer o, int res)
        //{
        //    Dictionary<int,IPigPlayer> Opp = new Dictionary<int,IPigPlayer>();
        //    Opp.Add(res,o);
        //    if (tRes.ContainsKey(p))
        //        if (tRes[p].ContainsKey(res))
        //            tRes[p][res] = tRes[p][res] + 1;
        //        else
        //            tRes[p].Add(o, 1);
        //    else
        //        tRes.Add(p, Opp);
        //    Opp.Clear();
        //}

        //public string gettRes(int games)
        //{
        //    string s="Win-Loss Record\n";
        //    foreach (IPigPlayer p in tRes.Keys)
        //    {   
        //        s += p.getName() + " : \t";
        //        foreach (IPigPlayer o in tRes[p].Keys)
        //        { 
        //            s += o.getName() + "(" + tRes[p][o] + "-" + (games-tRes[p][o]) + ")"; 
        //        }
        //        s += "\n";
        //    }
        //    return s;
        //}
        //--------------------------------- aqui acaba el segundo intento
        //IPigPlayer player { get;set; }
        //IPigPlayer opponent { get; set; }
        //int win=0;
        //List<TournamentResults> listresults = new List<TournamentResults>();

        //public void add(IPigPlayer p1, IPigPlayer p2)
        //{
            
        //    player = p1;
        //    opponent = p2;
        //    win++;
        //    //listresults.Add(game);
        //}


        //public List<TournamentResults> getlist()
        //{
        //    return (List<TournamentResults>)listresults.OrderByDescending(x=>x.player);
        //}

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
            // There was a problem here, because sometimes a player accumulated winner score but because opponent
            // was allowed to play one last time, opponent final score could be higher than the winner so
            // I changed condition to finish game immediately after one reaches the winner score.
            do
            {
                currentPlayer = nextPlayer();
                this.playTurn();
                //MessageBox.Show(currentPlayer + " " + currentPlayerScore + " acumulado: " + players[currentPlayer].getScore());
            } while (this.players[currentPlayer].getScore() < 10);

            // At this point we have a winner
        }

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
                    players[currentPlayer].turnScore(0);
                    break;
                }

                // For any other roll, increment the current player Score
                currentPlayerScore += roll;

                // Need to build the current state of the game so players have lots 
                // of information on which to base their decisions
                PigGameState g = buildGameState();
                //--------------------------------------------------------------------------------------------------------aqui agarra los resultados
                // Ask the player what to do
                bool rollAgain = players[currentPlayer].getPlayer().toRollOrNotToRoll(g);

                // Player is stopping, set their score
                if (!rollAgain)
                {
                    players[currentPlayer].turnScore(currentPlayerScore);
                    break;
                }
            }

        }

        // One way to build a game state.  It could include more history if we wanted it to.

        protected PigGameState buildGameState()
        {
            PigGameState g = new PigGameState();

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
            return _name + " (" + _type + ")";
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
            // if currentPlayerScoreThisTurn is different than lastCurrentPLayerScoreThisTurn
            // and my currentScore has changed
            // then it is a new turn  
            // (almost -- possible that I rolled a 2, chose to roll again, failed, and on my *next* turn rolled a 2.
            // But that should only happen once every 36 turns on average, so close enough
            if (s.currentPlayerScore != lastCurrentPlayerScore && s.currentPlayerScoreThisTurn != lastCurrentPlayerScoreThisTurn)
            {
                rollsThisTurn = 1;
            } else
            {
                rollsThisTurn += 1;
            }

            // store the currentPlayer score
            lastCurrentPlayerScoreThisTurn = s.currentPlayerScoreThisTurn;
            lastCurrentPlayerScore = s.currentPlayerScore;

            // If I haven't rolled enough times yet, then roll
            if (rollsThisTurn < _desiredRolls)
            {
                return true;
            }

            // Otherwise, end the turn and take my score
            return false;
        }

    }

}
