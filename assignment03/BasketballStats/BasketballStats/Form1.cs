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
using System.Globalization;

using System.Linq.Expressions;


// Assignment

// (0.5 pt) -- Into the 'team' Pane, add the pre-season record and the post-season record 
// (0.5 pt) -- Whenever we add a field, we have to change dataGridReset to make it not show.   Add a loop to make all the fields *not* visible, then make the ones we want to see visible and give a column index.
// (1 pt) -- Need More Stats!!!    We have Wins, Losses, Points Per Game.  Add Opponent Points Per Game.  And both of the above for wins and for losses.  (e.g. My points per game in a win)
// (2 pt)  -- Add labels, drop-downs, or buttons to the Game list datagridview that let you see:  all games, home games, away games;   all games, wins, losses; If you pick home / wins it should only show home games that were won.  

// (4 pt) -- We have to repeat all the logic for wins, losses, games, points per game, etc. for each set of games
//        -- e.g. reg season, post season, wins, losses, home gmes, away games, etc.
//        -- Really these are a property of a set of games.
//        -- Create a new TeamGameList class that holds a filtered list of games.
//        -- All the stats (e.g. wins,games,etc.) should be in the TeamGameList class instead
//        -- A Team would then have a number of sets of games:  AllGames, GamesWon, GamesLost, RegularSeasonGames, RegularSeasonGamesWon, etc.
//        -- Instead of calling myTeam.RegSeasonWins to get RegularSeasonWins, you would call myTeam.RegularSeasonGames.Wins

// Extensions
// (1 pt) -- Filtered Stats -- show the right stats when you filter the game list.
// (1 pt) -- really, the drop-down filters filter to one of your TeamGameLists -- connect the filters to get the games from a TeamGameList instead of building an all new filter (just for home/away and wins/losses, ignore opponent here)
// (2 pt) -- Advanced Filters!   Change the filters so you can get a list of games by your score (e.g. <100) or your opp's score (e.g. >100) -- of course, the stats should update
// (2 pt) -- Custom game lists -- Instead of having a *lot* of properties that return TeamGameLists, just have a single Games property that has a Dictionary of TeamGameLists.  So myTeam.Games["All"] would be all games, myTeam.Games["HomeWins"], etc.  
//        --  So you can add custom things to the dictionary with myTeam.Games.Add("HomeWins",myTeam.Games["Home"].findAll(x => x.IsWin))




namespace BasketballStats
{
    public partial class Form1 : Form
    {

        // Create our list of teams
        List<Team> teams = new List<Team>();

        Team selectedTeam;

        public Form1()
        {
            InitializeComponent();

            loadTeamData();
            loadGameData();

            teams.Sort((t, u) => t.City.CompareTo(u.City));

            opponentComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            opponentComboBox.Items.Add("All Opponents");
            foreach (Team t in teams)
            {
                opponentComboBox.Items.Add(t.City + " " + t.Nickname);
            }

            // Hint D1 -- you'll have two more combo boxes to set up, but they will each have three explicit strings added.
            locationCB.Items.Add("All"); locationCB.Items.Add("Home"); locationCB.Items.Add("Away");
            resultsCB.Items.Add("All"); resultsCB.Items.Add("Wins"); resultsCB.Items.Add("Losses");
            teams.Sort((u, t) => t.RegSeasonWins.CompareTo(u.RegSeasonWins));

            teamGridReset();

            teamDataGridView.SelectionChanged += new EventHandler(teamDataGridView_SelectionChanged);
            teamDataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(teamDataGridView_ColumnHeaderMouseClick);
            // let's initialize filter comboboxes with all elements item selected for each one
            opponentComboBox.SelectedIndex = 0; locationCB.SelectedItem = "All"; resultsCB.SelectedItem = "All";
        }
        // loads all teams info creating an object per every team in file (30 entries)
        private void loadTeamData()
        {
            // Read in the file.  Each line is a team (except the header row)

            foreach (string line in File.ReadLines("../../data/teams.csv"))
            {

                // Team has a static function that will create a team from a line of this data.
                Team t = Team.readTeamData(line);
                
                // If this isn't the header line
                if (!t.IsHeader)
                {
                    teams.Add(t);
                }
            }
        }

        // this function creates 2 teamGame objects from every line in file, one for hometeam and one for visitorteam
        private void loadGameData()
        {
            bool headerRow = true;
            foreach (string line in File.ReadLines("../../data/games2122.csv"))
            {
                // Skip the first line, it is a header
                if (headerRow)
                {
                    headerRow = false;
                    continue;
                }
                 
                TeamGame homeTeamGame = TeamGame.readGameData(true, line);
                TeamGame awayTeamGame = TeamGame.readGameData(false, line);

                Team homeTeam = teams.Find(x => x.TeamID == homeTeamGame.TeamID);
                Team awayTeam = teams.Find(x => x.TeamID == awayTeamGame.TeamID);
                homeTeamGame.Abbr = homeTeam.Abbr;
                homeTeamGame.OppAbbr = awayTeam.Abbr;
                homeTeam.AddGame(homeTeamGame);
                awayTeamGame.Abbr = awayTeam.Abbr;
                awayTeamGame.OppAbbr = homeTeam.Abbr;
                awayTeam.AddGame(awayTeamGame);
            }
        }

        private void teamGridReset()
        {
            //loads datagridview control with objects stored in teams list
            teamDataGridView.DataSource = teams;
            // DataGridHint:   Whenever you add a property, make sure to hide it above or you will see it
            // Hint B1a:  teamDataGridView.Columns will give you a list of all the columns
            // Loop through that and make them all invisible.
            //------ C O R E   T A S K   2 --- Column visibility
            foreach (DataGridViewColumn column in teamDataGridView.Columns)
                column.Visible = false;

            // Hint B2a:  You turned off *everything* above -- you'll need to explicitly make the below columns visible
            teamDataGridView.Columns["City"].DisplayIndex = 0;
            teamDataGridView.Columns["NickName"].DisplayIndex = 1;
            teamDataGridView.Columns["RegSeasonRecord"].DisplayIndex = 2;
            teamDataGridView.Columns["PreSeasonRecord"].DisplayIndex = 3;
            teamDataGridView.Columns["PostSeasonRecord"].DisplayIndex = 4;
            teamDataGridView.Columns["ArenaCapacity"].DisplayIndex = 5;
            //------ C O R E   T A S K   2 --- Column visibility
            teamDataGridView.Columns["City"].Visible = true;
            teamDataGridView.Columns["NickName"].Visible = true;
            teamDataGridView.Columns["RegSeasonRecord"].Visible = true;
            teamDataGridView.Columns["PreSeasonRecord"].Visible = true;
            teamDataGridView.Columns["PostSeasonRecord"].Visible = true;
            teamDataGridView.Columns["ArenaCapacity"].Visible = true;


            teamDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            teamDataGridView.MultiSelect = false;
        }

        private void gameGridReset(Team t)
        {
            gameGridView.DataSource = t.GameList;

            // Can do way better here -- clear all the columns except the ones we want to see.

            //------ C O R E   T A S K   2 --- Column visibility
            foreach (DataGridViewColumn column in gameGridView.Columns)
                column.Visible = false;

            // Hint B2a:  Do hint B1a here too!
            // Hint B2b:  You'd better make the columns you want visible here -- and set their display index if you care.
            //------ C O R E   T A S K   2 --- Column visibility
            gameGridView.Columns["OppAbbr"].Visible = true;
            gameGridView.Columns["IsHomeGame"].Visible = true;
            gameGridView.Columns["GameDate"].Visible = true;
            gameGridView.Columns["IsWin"].Visible = true;
            gameGridView.Columns["Score"].Visible = true;

            gameGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gameGridView.MultiSelect = false;
            //  --------   I want to keep my filters even when I pick another team from left grid
            filterGames(opponentComboBox.SelectedIndex, locationCB.SelectedIndex, resultsCB.SelectedIndex);

        }

        private void teamDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            // Get the set of selected rows.
            // Since multiselect is false, there will only be one row selected, so get the first one
            // DataBoundItem is the Team in that spot in the row.

            if (teamDataGridView.SelectedRows.Count == 0)
            {
                return;
            }
            //MessageBox.Show(teamDataGridView.Rows.Count.ToString());
            selectedTeam = (Team)teamDataGridView.SelectedRows[0].DataBoundItem;
            teamName.Text = selectedTeam.Nickname;
            arenaName.Text = selectedTeam.Arena;

            // set the Label to the associated property
            regularSeasonRecordLabel.Text = selectedTeam.RegSeasonRecord;  // Hint A2:  you'll want to do this for PreSeasonRecord
            //--------------- C O R E   T A S K   1 ----------- pre & post season records labels
            preSeasonRecordLabel.Text = selectedTeam.PreSeasonRecord;
            postSeasonRecordLabel.Text = selectedTeam.PostSeasonRecord;

            // Hint C2:  You'll need to add a Opponent's points per game label, 
            // and show the OppPtsPerRegSeasonGame Property for the selected team in that label below
            //----------- C O R E   T A S K   3 ---------- Displaying stats (Overall PPG, PPGWins, PPGLosses, OppPPG, OppPPGWins, OppPPGLosses averages)
            regularSeasonPPGLabel.Text = selectedTeam.PtsPerRegSeasonGame;
            OppRegularSeasonPPGLabel.Text = selectedTeam.OppPtsPerRegSeasonGame;
            // Hint C4:  You'll need to do the above for the other 4 stats
            PPGWinsLabel.Text = selectedTeam.PPGWins;
            PPGLossesLabel.Text = selectedTeam.PPGLosses;
            OppPPGWinsLabel.Text = selectedTeam.OppPPGWins;
            OppPPGLossesLabel.Text = selectedTeam.OppPPGLosses;

            gameGridReset(selectedTeam);

        }

        private void teamDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columnName = teamDataGridView.Columns[e.ColumnIndex].Name;
            MessageBox.Show(columnName);
            teams.Sort((u, t) => t.Nickname.CompareTo(u.Nickname));
            teamGridReset();

        }

        private void opponentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //// Find the opponent name selected in the combo box
            //String teamname = (String)opponentComboBox.SelectedItem;

            //// Find the team selected in the teamDataGridView
            //Team selectedTeam = (Team)teamDataGridView.SelectedRows[0].DataBoundItem;
            filterGames(opponentComboBox.SelectedIndex, locationCB.SelectedIndex, resultsCB.SelectedIndex);

            // If it is all opponents, then add the full game list.
            //if (teamname == "All Opponents")
            //{
            //    gameGridView.DataSource = selectedTeam.GameList;
            //    return;
            //}

            // If we wan to see a specific team, then find the right team from the teamname
            // Team selectedOpponent = teams.Find(t => teamname.Contains(t.Nickname));

            // Now, get the read-only list of all games from the selected team, make a mutable copy (ToList()), then filter so that the 
            // OppAbbr property in the game matches the Abbr of the selected opponent.
            // gameGridView.DataSource = selectedTeam.GameList.ToList<TeamGame>().FindAll(g => g.OppAbbr == selectedOpponent.Abbr);
        }

        // Hint D2:  There will be a couple of SelectedIndexChanged events, one for each combo box
        // They will both call the same function
        // That function will get the selected item from both combo boxes.
        // ----------- C O R E   T A S K   4 ---------- Location and Results filter comboboxes
        private void locationCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterGames(opponentComboBox.SelectedIndex,locationCB.SelectedIndex, resultsCB.SelectedIndex);
        }
        private void resultsCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterGames(opponentComboBox.SelectedIndex,locationCB.SelectedIndex, resultsCB.SelectedIndex);
        }

        // ----------- C O R E   T A S K   4 ----------  FILTERING TEAMGAMES INFORMATION 
        // ---- I decided to handle the 3 filters (opp, location and result) in 1 single function,
        // ---- besides, you can select filters in any order and they will stay even if you select a different team from teamGrid
        // ---- I've found easier to work with int indexes instead of words from comboboxes, so I used them as arguments here
        private void filterGames(int team, int loc, int res)
        {
            // selectedTeam is required to load games info, but is null until form1 is completely loaded
            // and this function is called during form1 loading due to comboboxes initialization so while form1 loads, I get out this function.
            if (selectedTeam == null) return; 

            // function to build predicate to filter results from Games list
            Predicate<TeamGame> pred = null;
            void buildpred(Predicate<TeamGame> newpred)
            {
                if (pred == null)
                {
                    pred = newpred;
                    return;
                }
                var oldpred = pred;
                pred = x => oldpred(x) && newpred(x);
            }
            // condition to filter games by opponents or show them all
            switch(team)
            {
                case 0: // all opponents selected, then show them all
                    gameGridView.DataSource = selectedTeam.GameList;
                    break;
                default:
                    if (opponentComboBox.SelectedItem != null)
                    {   // if specific opp selected, add that criteria to prediacate
                        String teamname = (String)opponentComboBox.SelectedItem;
                        Team selectedOpponent = teams.Find(t => teamname.Contains(t.Nickname));
                        buildpred(g => g.OppAbbr == selectedOpponent.Abbr);
                    }
                    break;
            }
            // condition to filter by location
            switch (loc)
            {
                case 1:buildpred((x) => x.IsHomeGame);break;
                case 2: buildpred((x) => !x.IsHomeGame);break;
                default: /*buildpred((x) => x.IsHomeGame == x.IsHomeGame)*/; break; // I could just delete this line
            }

            //  condition to filter by results
            switch (res)
            {
                case 1: buildpred((x) => x.IsWin);break;
                case 2: buildpred((x) => !x.IsWin);break;
                default: /*buildpred((x) => x.IsWin==x.IsWin)*/; break; // I could just delete this line
            }
            //------------ once I have all my filters set, send the query

            if (pred != null) // at least one filter selected
                gameGridView.DataSource = selectedTeam.GameList.ToList<TeamGame>().FindAll(pred);
            //else  // just after form loads, or anytime I chose all opponents from opp combobox 
                // gameGridView.DataSource = selectedTeam.GameList;

        }
    }
        // Then it should get a list:   selectedTeam.GameList.ToList<TeamGame>();

        // If the home/away filter is set to all, do nothing
        // If the home/away filter is set to 'home', then filter the list to only home games
        // If the home/away filter is set to 'away', then filter the list to only away games

        // Then we will apply the win/loss filter to the above resulting list
        // If the win/loss filter is set to all, do nothing else
        // If the win/loss filter is set to win, filter the home/away filtered list to only wins
        // If the win/loss filter is set to loss, filter the home/away filtered list to only losses



}

    /// --------------  C L A S S   T E A M  --------------
    class Team
    {
        public String TeamID {get;  private set;}
        public String Abbr { get; private set; }
        public String Nickname { get; private set; }
        public String City { get; private set; }
        public String Arena { get; private set; }
        public Int32 ArenaCapacity { get; private set; }

        List<TeamGame> games = new List<TeamGame>();

        // Property which notes that this Team was loaded from a header line in the file

        public bool IsHeader
        {
            get
            {
                return TeamID == "TEAM_ID";
            }
        }

        public void AddGame (TeamGame g)
        {
            games.Add(g);    
        }
        //-------- returns all wins for a team
        public Int32 Wins
        {
            get
            {
                return games.FindAll(x => x.IsWin).Count; 
            }
        }

        public Int32 RegSeasonWins
        {
            get
            {
                return games.FindAll(x => x.IsWin && !x.IsPreSeason && !x.IsPostSeason).Count;
            }
        }
        public Int32 RegSeasonLosses
        {
            get
            {
                return games.FindAll(x => !x.IsWin && !x.IsPreSeason && !x.IsPostSeason).Count;
            }
        }

        public Int32 Games
        {
            get
            {
                return games.Count;
            }
        }

        public Int32 RegSeasonGames
        {
            get
            {
                return games.FindAll(x => !x.IsPreSeason && !x.IsPostSeason).Count;
            }
        }

        public IReadOnlyList<TeamGame> GameList
        {
            get
            {
                return (IReadOnlyList<TeamGame>)this.games;
            }
        }

        public String PtsPerRegSeasonGame
        {
            get
            {
                // First, get a filtered lists of only regular season games
                List<TeamGame> regularSeasonGames = games.FindAll(x => !x.IsPreSeason && !x.IsPostSeason);

                // we'll need to know how many points were scored and how many total points scored 
                double gamesPlayed = regularSeasonGames.Count;
                double totalPoints = 0;

                // Loop through the list of regularSeasonGames, adding up the points
                foreach(TeamGame g in regularSeasonGames)
                {
                    totalPoints += g.Points;
                }

                // Return the average as a string formatted to display  a float.
                return (totalPoints / gamesPlayed).ToString("F");
            }
        }

        // Hint C1:   you'll want to add an OppPtsPerRegSeasonGame;  will look *very* much like the above
        //----------- C O R E   T A S K   3 ---------- Displaying stats (Overall PPG, PPGWins, PPGLosses, OppPPG, OppPPGWins, OppPPGLosses averages)
        public String OppPtsPerRegSeasonGame
        {
            get
            {
                List<TeamGame> regularSeasonGames = games.FindAll(x => !x.IsPreSeason && !x.IsPostSeason);
                double gamesPlayed = regularSeasonGames.Count;
                double totalPoints = 0;
                foreach (TeamGame g in regularSeasonGames)
                {
                    totalPoints += g.OppPoints;
                }
                return (totalPoints / gamesPlayed).ToString("F");
            }
        }
        // Hint C2:   you'll need 4 more properties -- but they will basically look like the above as well
        // The only difference is whether we are adding up Points or OppPoints, and then whether we are filtering the 
        // games to regular season games or regular season wins or regular season losses.
        public String PPGWins // ---------- C O R E  T A S K  3 -----------
        {
            get
            {
                List<TeamGame> regularSeasonGames = games.FindAll(x => x.IsWin && !x.IsPreSeason && !x.IsPostSeason);
                double gamesPlayed = regularSeasonGames.Count;
                double totalPoints = 0;
                foreach (TeamGame g in regularSeasonGames)
                { totalPoints += g.Points; }
                return (totalPoints / gamesPlayed).ToString("F");
            }
        }
        public String PPGLosses // ---------- C O R E  T A S K  3 -----------
        {
            get
            {
                List<TeamGame> regularSeasonGames = games.FindAll(x => !x.IsWin && !x.IsPreSeason && !x.IsPostSeason);
                double gamesPlayed = regularSeasonGames.Count;
                double totalPoints = 0;
                foreach (TeamGame g in regularSeasonGames)
                { totalPoints += g.Points; }
                return (totalPoints / gamesPlayed).ToString("F");
            }
        }
        public String OppPPGWins // ---------- C O R E  T A S K  3 -----------
        {
            get
            {
                List<TeamGame> regularSeasonGames = games.FindAll(x => x.IsWin && !x.IsPreSeason && !x.IsPostSeason);
                double gamesPlayed = regularSeasonGames.Count;
                double totalPoints = 0;
                foreach (TeamGame g in regularSeasonGames)
                {totalPoints += g.OppPoints;}
                return (totalPoints / gamesPlayed).ToString("F");
            }
        }
        public String OppPPGLosses // ---------- C O R E  T A S K  3 -----------
        {
            get
            {
                List<TeamGame> regularSeasonGames = games.FindAll(x => !x.IsWin && !x.IsPreSeason && !x.IsPostSeason);
                double gamesPlayed = regularSeasonGames.Count;
                double totalPoints = 0;
                foreach (TeamGame g in regularSeasonGames)
                {totalPoints += g.OppPoints;}
                return (totalPoints / gamesPlayed).ToString("F");
            }
        }
        public String RegSeasonRecord
        {
            get
            {
                return RegSeasonWins + "-" + RegSeasonLosses;
            }
        }

        // Hint A1 -- create a PreSeasonRecord property and a PostSeasonRecordProperty
        // ----------- C O R E   T A S K   1 ---------- PRE & POST SEASON RECORDS DERIVATED PROPERTIES
        public string PreSeasonRecord
        {
            get
            {
                return (games.FindAll(x => x.IsWin && x.IsPreSeason).Count) + "-" + (games.FindAll(x => !x.IsWin && x.IsPreSeason).Count);
            }
        }
        public string PostSeasonRecord
        {
            get
            {
                return (games.FindAll(x => x.IsWin && x.IsPostSeason).Count) + "-" + (games.FindAll(x => !x.IsWin && x.IsPostSeason).Count);
            }
        }


        // Up to you if you want to create a PreSeasonWins and PreSeasonLosses properties as well or if you want to fold that logic into the PreSeasonRecord property


        public override string ToString()
        {
            return City + " " + Nickname;
        }

        public static Team readTeamData(string teamLine)
        {
            Team t = new Team();

            // Reading from a .csv with the following data
            // LEAGUE_ID,TEAM_ID,MIN_YEAR,MAX_YEAR,ABBREVIATION,NICKNAME,YEARFOUNDED,CITY,ARENA,ARENACAPACITY,OWNER,GENERALMANAGER,HEADCOACH,DLEAGUEAFFILIATION
            string[] data = teamLine.Split(',');

            // private Properties are accessible within the class
            // even when you aren't inside the object!

            t.TeamID = data[1];
            t.Abbr = data[4];
            t.Nickname = data[5];
            t.City = data[7];
            t.Arena = data[8];
            int ac;
            if (Int32.TryParse(data[9], out ac))
            {
                t.ArenaCapacity = ac;
            }

            return t;
        }

    }

    /// -----------  C L A S S   T E A M G A M E  ---------
    class TeamGame
    {
        public String TeamID { get; private set; }
        public String OppID { get; private set; }
        public String GameID { get; private set; }

        public String OppAbbr { get; set; }
        public String Abbr { get; set; }

        public bool IsHomeGame { get; private set; }

        public DateTime GameDate { get; private set; }

        public Int32 Points { get; private set; }
        public Int32 Assists { get; private set; }
        public Int32 Rebounds { get; private set; }

        public Double FG_Pct { get; private set; }
        public Double FT_Pct { get; private set; }
        public Double FG3_Pct { get; private set; }

        public Int32 OppPoints { get; private set; }
        public Int32 OppAssists { get; private set; }
        public Int32 OppRebounds { get; private set; }

        public Double OppFG_Pct { get; private set; }
        public Double OppFT_Pct { get; private set; }
        public Double OppFG3_Pct { get; private set; }

        // Derived property.  We won the game if we scored more points.
        public bool IsWin
        {
            get
            {
                return Points > OppPoints;
            }
        }

        public bool IsPreSeason
        {
            get
            {
                return GameDate < new DateTime(2021,10,19);
            }
        }

        public bool IsPostSeason
        {
            get
            {
                return GameDate > new DateTime(2022, 4, 11);
            }
        }

        public string Score
        {
            get
            {
                return Points + "-" + OppPoints;
            }
        }


        public static TeamGame readGameData(bool homegame, string gameLine)
        {
            TeamGame tg = new TeamGame();

            // home game offsets
            int myOffset = 6;
            int oppOffset = 13;

            // reading from a .csv with the following data
            // GAME_DATE_EST,GAME_ID,GAME_STATUS_TEXT,HOME_TEAM_ID,VISITOR_TEAM_ID,SEASON,TEAM_ID_home,PTS_home,FG_PCT_home,FT_PCT_home,FG3_PCT_home,AST_home,REB_home,TEAM_ID_away,PTS_away,FG_PCT_away,FT_PCT_away,FG3_PCT_away,AST_away,REB_away,HOME_TEAM_WINS

            string[] data = gameLine.Split(',');

            tg.GameDate = DateTime.Parse(data[0]);
            tg.GameID = data[1];

            tg.IsHomeGame = homegame;

            if (!homegame)
            {
                myOffset = 13;
                oppOffset = 6;
            }

            tg.TeamID = data[myOffset + 0];
            tg.OppID = data[oppOffset + 0];

            tg.Points = (Int32) Double.Parse(data[myOffset + 1], CultureInfo.InvariantCulture);
            tg.OppPoints = (Int32)Double.Parse(data[oppOffset + 1], CultureInfo.InvariantCulture);

            tg.Assists = (Int32)Double.Parse(data[myOffset + 5], CultureInfo.InvariantCulture);
            tg.OppAssists = (Int32)Double.Parse(data[oppOffset + 5], CultureInfo.InvariantCulture);

            tg.Rebounds = (Int32)Double.Parse(data[myOffset + 6], CultureInfo.InvariantCulture);
            tg.OppRebounds = (Int32)Double.Parse(data[oppOffset + 6], CultureInfo.InvariantCulture);

            // Culture info is required to properly parse doubles on my wife's computer set to French 
            tg.FG_Pct = Double.Parse(data[myOffset + 2],CultureInfo.InvariantCulture);
            tg.OppFG_Pct = Double.Parse(data[oppOffset + 2], CultureInfo.InvariantCulture);

            tg.FT_Pct = Double.Parse(data[myOffset + 3], CultureInfo.InvariantCulture);
            tg.OppFT_Pct = Double.Parse(data[oppOffset + 3], CultureInfo.InvariantCulture);

            tg.FG3_Pct = Double.Parse(data[myOffset + 4], CultureInfo.InvariantCulture);
            tg.OppFG3_Pct = Double.Parse(data[oppOffset + 4], CultureInfo.InvariantCulture);


            return tg;
        }



    }


class TeamGameList
{
    List<TeamGame> games = new List<TeamGame>();
    
    
    //-------- null constructor
    public TeamGameList()
    {

    }
    //-------- Constructor which accepts a list of TeamGame
    public TeamGameList(List<TeamGame> list)
    {

    }



}