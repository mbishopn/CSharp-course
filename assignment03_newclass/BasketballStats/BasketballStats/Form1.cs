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
            // ---------- E X T E N S I O N   3 --------- I added two new filters, see filterGames() function.
            seasonCB.Items.Add("All");seasonCB.Items.Add("Regular");seasonCB.Items.Add("Pre-Season");seasonCB.Items.Add("Post-Season");
            scoreCB.Items.Add("Off"); scoreCB.Items.Add("Both"); scoreCB.Items.Add("This Team"); scoreCB.Items.Add("Opponent");
            // let's initialize filter comboboxes with all elements item selected for each one
            opponentComboBox.SelectedIndex = 0; locationCB.SelectedItem = "All"; resultsCB.SelectedItem = "All"; seasonCB.SelectedItem = "All";scoreCB.SelectedItem = "Off";

            teams.Sort((u, t) => t.rsgames.Wins.CompareTo(u.rsgames.Wins));

            teamGridReset();

            teamDataGridView.SelectionChanged += new EventHandler(teamDataGridView_SelectionChanged);
            teamDataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(teamDataGridView_ColumnHeaderMouseClick);
            foreach (ComboBox x in this.filters.Controls.OfType<ComboBox>())
            {
                x.SelectedIndexChanged += new EventHandler(filterCB_SelectedIndexChanged);
            }

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
                homeTeam.games.AddGame(homeTeamGame);
                //homeTeam.AddGame(homeTeamGame);
                awayTeamGame.Abbr = awayTeam.Abbr;
                awayTeamGame.OppAbbr = homeTeam.Abbr;
                awayTeam.games.AddGame(awayTeamGame);
                //awayTeam.AddGame(awayTeamGame);
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
            filterGames();

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

            // ------------ C O R E   T A S K   5 ---------- getting data from new class TeamGameList
            //---------- FILL OVERALL STATISTICS -----------
            gpLabel.Text = selectedTeam.games.lista.Count.ToString();
            winsLabel.Text = selectedTeam.games.Wins.ToString();
            lossesLabel.Text = selectedTeam.games.Losses.ToString();
            // ------- PPG: general, wins and losses.
            ppgLabel.Text = selectedTeam.games.PPG;
            ppgWLabel.Text = selectedTeam.games.PPGWins;
            ppgLLabel.Text = selectedTeam.games.PPGLosses;
            // ------- Opp PPG: general, wins and losses.
            ppgOLabel.Text = selectedTeam.games.PPGOpp;
            ppgOWLabel.Text = selectedTeam.games.PPGOppWins;
            ppgOLLabel.Text = selectedTeam.games.PPGOppLosses;

            // ----------- REGULAR SEASON STATISTICS ------------
            // set the Label to the associated property
            rsRecordLabel.Text = selectedTeam.rsgames.Games + " - "+ selectedTeam.rsgames.Wins + " - " + selectedTeam.rsgames.Losses;// Hint A2:  you'll want to do this for PreSeasonRecord
            //----------- C O R E   T A S K   3 ---------- Displaying stats (Overall PPG, PPGWins, PPGLosses, OppPPG, OppPPGWins, OppPPGLosses averages)
            rsppgLabel.Text = selectedTeam.rsgames.PPG;
            rsppgWLabel.Text = selectedTeam.rsgames.PPGWins;
            rsppgLLabel.Text = selectedTeam.rsgames.PPGLosses;
            rsppgOLabel.Text = selectedTeam.rsgames.PPGOpp;
            rsppgOWLabel.Text = selectedTeam.rsgames.PPGOppWins;
            rsppgOLLabel.Text = selectedTeam.rsgames.PPGOppLosses;

            //--------------- C O R E   T A S K   1 ----------- pre & post season records labels
            preRecordLabel.Text = selectedTeam.pregames.Games + " - " + selectedTeam.pregames.Wins + " - " + selectedTeam.pregames.Losses;
            preppgLabel.Text = selectedTeam.pregames.PPG;
            preppgWLabel.Text = selectedTeam.pregames.PPGWins;
            preppgLLabel.Text = selectedTeam.pregames.PPGLosses;
            preppgOLabel.Text = selectedTeam.pregames.PPGOpp;
            preppgOWLabel.Text = selectedTeam.pregames.PPGOppWins;
            preppgOLLabel.Text = selectedTeam.pregames.PPGOppLosses;

            posRecordLabel.Text = selectedTeam.postgames.Games + " - " + selectedTeam.postgames.Wins + " - " + selectedTeam.postgames.Losses;
            posppgLabel.Text = selectedTeam.postgames.PPG;
            posppgWLabel.Text = selectedTeam.postgames.PPGWins;
            posppgLLabel.Text = selectedTeam.postgames.PPGLosses;
            posppgOLabel.Text = selectedTeam.postgames.PPGOpp;
            posppgOWLabel.Text = selectedTeam.postgames.PPGOppWins;
            posppgOLLabel.Text = selectedTeam.postgames.PPGOppLosses;

            gameGridReset(selectedTeam);

        }

        private void teamDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columnName = teamDataGridView.Columns[e.ColumnIndex].Name;
            MessageBox.Show(columnName);
            teams.Sort((u, t) => t.Nickname.CompareTo(u.Nickname));
            teamGridReset();

        }

        // ----------- C O R E   T A S K   4 ---------- Location and Results filter comboboxes
        // A single eventhandler for comboboxes selection, if a combobox is selected, call filterGames() function
        private void filterCB_SelectedIndexChanged(object sender, EventArgs e)
        { filterGames(); }

        // ----------- C O R E   T A S K   4 ----------  FILTERING TEAMGAMES INFORMATION 
        // ---- I decided to handle the 3 filters (opp, location and result) in 1 single function,
        // ---- besides, you can select filters in any order and they will stay even if you select a different team from teamGrid
        // ---- I've found easier to work with int indexes instead of words from comboboxes, so I used them as arguments here
        // ---- when this function is called, checks all filters to apply them at the same time, for every filter
        // ---- in a non-default state function it will create a piece of a predicate which at the end will be assembled to refine
        // ---- games list returned. I did this before reading last extension so I decided to leave this here and create another one
        // ---- inside teamgamelist class according that requirement.
        private void filterGames()
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
            // ------------- condition to filter games by opponents or show them all
            switch(opponentComboBox.SelectedIndex)
            {
                case 0: // all opponents selected, then show them all
                    var query = selectedTeam.GameList.ToList<TeamGame>();
                    gameGridView.DataSource = selectedTeam.GameList;
                    updatefilteredstats(query);
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
            // ------------ condition to filter by location
            switch (locationCB.SelectedIndex)
            {
                case 1:buildpred((x) => x.IsHomeGame);break;
                case 2: buildpred((x) => !x.IsHomeGame);break;
                default: /*buildpred((x) => x.IsHomeGame == x.IsHomeGame)*/; break; // I could just delete this line
            }

            // ------------ condition to filter by results
            switch (resultsCB.SelectedIndex)
            {
                case 1: buildpred((x) => x.IsWin);break;
                case 2: buildpred((x) => !x.IsWin);break;
                default: /*buildpred((x) => x.IsWin==x.IsWin)*/; break; // I could just delete this line
            }
            // ------------ condition to filter by season
            switch (seasonCB.SelectedIndex)
            {
                case 1: buildpred((x) => !x.IsPreSeason && !x.IsPostSeason); break;
                case 2: buildpred((x) => x.IsPreSeason); break;
                case 3: buildpred((x) => x.IsPostSeason); break;
                default: /*buildpred((x) => x.IsWin==x.IsWin)*/; break; // I could just delete this line
            }
            // ------------ condition to filter by score ------- E X T E N S I O N   3 --------
            switch (scoreCB.SelectedIndex)
            {
                case 1: buildpred((x) => x.Points>=100 && x.OppPoints>=100); break;
                case 2: buildpred((x) => x.Points>=100); break;
                case 3: buildpred((x) => x.OppPoints>=100); break;
                default: /*buildpred((x) => x.IsWin==x.IsWin)*/; break; // I could just delete this line
            }

            //------------ once I have all my filters set, send the query

            if (pred != null) // at least one filter selected
            {
                var query = selectedTeam.GameList.ToList<TeamGame>().FindAll(pred);
                gameGridView.DataSource = query;
                updatefilteredstats(query);
            }
            // -------- E X T E N S I O N   1 --------- This function updates statistics every time a filter is applied
            void updatefilteredstats(List<TeamGame> query)
            {
                    filterGLabel.Text = query.Count().ToString();
                    filterWLabel.Text = query.FindAll(x => x.IsWin).Count().ToString();
                    filterLLabel.Text = query.FindAll(x => !x.IsWin).Count().ToString();
                    filterPPGLabel.Text = (Convert.ToDouble(query.Sum(x => x.Points)) / query.Count()).ToString("F");
                    filterOPPGLabel.Text = (Convert.ToDouble(query.Sum(x => x.OppPoints)) / query.Count()).ToString("F");
            }

        }

    }
}

/// --------- C O R E   T A S K   5 ----------- Creating TeamGameList class to use it as property inside team class
class TeamGameList
{
    public List<TeamGame> lista = new List<TeamGame>();

    public Int32 Wins
    {
        get
        {
            return lista.FindAll(x => x.IsWin).Count;
        }
    }

    public Int32 Losses
    {
        get
        {
            return lista.FindAll(x => !x.IsWin).Count;
        }
    }
    public Int32 Games
    {
        get
        {
            return lista.Count;
        }
    }
    //----------PPG this team
    public string PPG
    {
        get 
        {
            double totalpoints=lista.Sum(x => x.Points);
            return (totalpoints / Games).ToString("F");
        }
    }
    public string PPGWins
    {
        get
        {
            double totalpoints = lista.FindAll(x => x.IsWin).Sum(x => x.Points);
            return (totalpoints/Wins).ToString("F");
        }
    }
    public string PPGLosses
    {
        get
        {
            double totalpoints = lista.FindAll(x => !x.IsWin).Sum(x => x.Points);
            return (totalpoints / Losses).ToString("F");
        }
    }
    //---------- PPG Opponents
    public string PPGOpp
    {
        get
        {
            double totalpoints = lista.Sum(x => x.OppPoints);
            return (totalpoints / Games).ToString("F");
        }
    }
    public string PPGOppWins
    {
        get
        {
            double totalpoints = lista.FindAll(x => x.IsWin).Sum(x => x.OppPoints);
            return (totalpoints / Wins).ToString("F");
        }
    }
    public string PPGOppLosses
    {
        get
        {
            double totalpoints = lista.FindAll(x => !x.IsWin).Sum(x => x.OppPoints);
            return (totalpoints / Losses).ToString("F");
        }
    }


    // ------- CONSTRUCTOR WITH EMPTY LIST
    public TeamGameList()
    {
        //List<TeamGame> lista = new List<TeamGame>();
        //MessageBox.Show(lista.Count.ToString());
    }
    // ------- CONSTRUCTOR RECEIVING A LIST AS ARGUMENT
    public TeamGameList(List<TeamGame> list)
    {
        lista = list;
    }

    public void AddGame(TeamGame g)
    {
        lista.Add(g);
    }

    // -----  E X T E N S I O N   4 ------ Dynamic custom game lists, I created this function,
    //  it works but I'm using it only once to get Reg Season records in teams grid
    public TeamGameList filter(Predicate<TeamGame> arg1)
    {
        TeamGameList filteredList = new TeamGameList(lista.FindAll(arg1));

        return filteredList;
    }
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

    // ----- C O R E   T A SK 5 ---- Now using TeamGameList objects as properties holding games lists
    public TeamGameList games = new TeamGameList();
    public TeamGameList rsgames
    { get { return new TeamGameList(games.lista.FindAll(x => !x.IsPreSeason && !x.IsPostSeason)); } }
    public TeamGameList pregames
    { get { return new TeamGameList(games.lista.FindAll(x => x.IsPreSeason)); } }
    public TeamGameList postgames
    { get { return new TeamGameList(games.lista.FindAll(x => x.IsPostSeason)); } }

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
        // games.Add(g);    
        games.AddGame(g);
        }
        //-------- returns all wins for a team
        //public Int32 Wins
        //{
        //    get
        //    {
        //        return games.lista.FindAll(x => x.IsWin).Count; 
        //    }
        //}

        //public Int32 RegSeasonWins
        //{
        //    get
        //    {
        //        return games.lista.FindAll(x => x.IsWin && !x.IsPreSeason && !x.IsPostSeason).Count;
        //    }
        //}
        //public Int32 RegSeasonLosses
        //{
        //    get
        //    {
        //        return games.lista.FindAll(x => !x.IsWin && !x.IsPreSeason && !x.IsPostSeason).Count;
        //    }
        //}

        //public Int32 Games
        //{
        //    get
        //    {
        //        return games.lista.Count;
        //    }
        //}

        //public Int32 RegSeasonGames
        //{
        //    get
        //    {
        //        return games.lista.FindAll(x => !x.IsPreSeason && !x.IsPostSeason).Count;
        //    }
        //}

        public IReadOnlyList<TeamGame> GameList
        {
            get
            {
                return (IReadOnlyList<TeamGame>)games.lista;
            }
        }

        //public String PtsPerRegSeasonGame
        //{
        //    get
        //    {
        //        // First, get a filtered lists of only regular season games
        //        List<TeamGame> regularSeasonGames = games.lista.FindAll(x => !x.IsPreSeason && !x.IsPostSeason);

        //        // we'll need to know how many points were scored and how many total points scored 
        //        double gamesPlayed = regularSeasonGames.Count;
        //        double totalPoints = 0;

        //        // Loop through the list of regularSeasonGames, adding up the points
        //        foreach(TeamGame g in regularSeasonGames)
        //        {
        //            totalPoints += g.Points;
        //        }

        //        // Return the average as a string formatted to display  a float.
        //        return (totalPoints / gamesPlayed).ToString("F");
        //    }
        //}

        // Hint C1:   you'll want to add an OppPtsPerRegSeasonGame;  will look *very* much like the above
        //----------- C O R E   T A S K   3 ---------- Displaying stats (Overall PPG, PPGWins, PPGLosses, OppPPG, OppPPGWins, OppPPGLosses averages)
        //public String OppPtsPerRegSeasonGame
        //{
        //    get
        //    {
        //        List<TeamGame> regularSeasonGames = games.lista.FindAll(x => !x.IsPreSeason && !x.IsPostSeason);
        //        double gamesPlayed = regularSeasonGames.Count;
        //        double totalPoints = 0;
        //        foreach (TeamGame g in regularSeasonGames)
        //        {
        //            totalPoints += g.OppPoints;
        //        }
        //        return (totalPoints / gamesPlayed).ToString("F");
        //    }
        //}

        // Hint C2:   you'll need 4 more properties -- but they will basically look like the above as well
        // The only difference is whether we are adding up Points or OppPoints, and then whether we are filtering the 
        // games to regular season games or regular season wins or regular season losses.
        //public String PPGWins // ---------- C O R E  T A S K  3 -----------
        //{
        //    get
        //    {
        //        List<TeamGame> regularSeasonGames = games.lista.FindAll(x => x.IsWin && !x.IsPreSeason && !x.IsPostSeason);
        //        double gamesPlayed = regularSeasonGames.Count;
        //        double totalPoints = 0;
        //        foreach (TeamGame g in regularSeasonGames)
        //        { totalPoints += g.Points; }
        //        return (totalPoints / gamesPlayed).ToString("F");
        //    }
        //}
        //public String PPGLosses // ---------- C O R E  T A S K  3 -----------
        //{
        //    get
        //    {
        //        List<TeamGame> regularSeasonGames = games.lista.FindAll(x => !x.IsWin && !x.IsPreSeason && !x.IsPostSeason);
        //        double gamesPlayed = regularSeasonGames.Count;
        //        double totalPoints = 0;
        //        foreach (TeamGame g in regularSeasonGames)
        //        { totalPoints += g.Points; }
        //        return (totalPoints / gamesPlayed).ToString("F");
        //    }
        //}
        //public String OppPPGWins // ---------- C O R E  T A S K  3 -----------
        //{
        //    get
        //    {
        //        List<TeamGame> regularSeasonGames = games.lista.FindAll(x => x.IsWin && !x.IsPreSeason && !x.IsPostSeason);
        //        double gamesPlayed = regularSeasonGames.Count;
        //        double totalPoints = 0;
        //        foreach (TeamGame g in regularSeasonGames)
        //        {totalPoints += g.OppPoints;}
        //        return (totalPoints / gamesPlayed).ToString("F");
        //    }
        //}
    //public String OppPPGLosses // ---------- C O R E  T A S K  3 -----------
    //{
    //    get
    //    {
    //        List<TeamGame> regularSeasonGames = games.lista.FindAll(x => !x.IsWin && !x.IsPreSeason && !x.IsPostSeason);
    //        double gamesPlayed = regularSeasonGames.Count;
    //        double totalPoints = 0;
    //        foreach (TeamGame g in regularSeasonGames)
    //        {totalPoints += g.OppPoints;}
    //        return (totalPoints / gamesPlayed).ToString("F");
    //    }
    //}
    public String RegSeasonRecord
    {
        get
        {   // ------------- E X T E N S I O N   4 ------------- using function taking predicates
            return games.filter(x => !x.IsPreSeason && !x.IsPostSeason).Wins + "-" + games.filter(x => !x.IsPreSeason && !x.IsPostSeason).Losses + "cotorrete";
            //return rsgames.Wins + "-" + rsgames.Losses; 

        }
    }

    // Hint A1 -- create a PreSeasonRecord property and a PostSeasonRecordProperty
    // ----------- C O R E   T A S K   1 ---------- PRE & POST SEASON RECORDS DERIVATED PROPERTIES
    public string PreSeasonRecord
        {
            get
            {
                return (games.lista.FindAll(x => x.IsWin && x.IsPreSeason).Count) + "-" + (games.lista.FindAll(x => !x.IsWin && x.IsPreSeason).Count);
            }
        }
        public string PostSeasonRecord
        {
            get
            {
                return (games.lista.FindAll(x => x.IsWin && x.IsPostSeason).Count) + "-" + (games.lista.FindAll(x => !x.IsWin && x.IsPostSeason).Count);
            }
        }


        // Up to you if you want to create a PreSeasonWins and PreSeasonLosses properties as well or if you want to fold that logic into the PreSeasonRecord property


        //public override string ToString()
        //{
        //    return City + " " + Nickname;
        //}

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

