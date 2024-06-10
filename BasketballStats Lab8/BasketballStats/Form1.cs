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



namespace BasketballStats
{
    public partial class Form1 : Form
    {

        // Create our list of teams
        List<Team> teams = new List<Team>();

        // we want to know which team is selected in the interface.
        Team selectedTeam;

        public Form1()
        {
            InitializeComponent();

            // Read in the file.  Each line is a team (except the header row)

            foreach(string line in File.ReadLines("../../data/teams.csv"))
            {

                // Team has a static function that will create a team from a line of this data.
                Team t = Team.readTeamData(line);

                // If this isn't the header line, add to the team list.
                if(!t.IsHeader)
                {
                    teams.Add(t);
                }
            }

            // Sort the teams by City name.
            // List<T>.Sort expects a comparison function

            // If your object implements iComparable, you can just sort on that.  
            // It will call the compareTo function to determine which object goes before what.
            //teams.Sort();

            // You can use a lmabda expressions
            // This is basically a short-cut.
            // take two arguments, t and u.
            // return the value of calling t.City.CompareTo with u.City as the argument.

            //teams.Sort((t, u) => t.City.CompareTo(u.City));
            //teams.Sort((t, u) => u.City.CompareTo(t.City));    // change the order of the comparison to sort in the opposite order

            // You can also use a custom function in the class
            //teams.Sort(Team.CompareByNickname);

            // Hint B4
            // Sort by the ArenaCapacity instead
            // You probably don't want to change the compareTo in the class
            // But either another custom function or a lambda expression would work great (see examples above)
            //teams.Sort((t, u) => u.ArenaCapacity.CompareTo(t.ArenaCapacity));

            // ------------- C O R E  T A S K  2 ---------------- Sorting the list prior to put data in te grid
            // ------- E X T E N S I O N  2 -------- Sorting by City after sorting by ArenaCapacity
            teams = teams.OrderBy(x => x.City).OrderByDescending(x => x.ArenaCapacity).ToList();

            // Once you've sorted the list, set up the grid.
            teamGridReset();

            // Add a selection changed Event Handler for when someone clicks on a team.
            teamDataGridView.SelectionChanged += new EventHandler(teamDataGridView_SelectionChanged);

            // register a column header click handler
            teamDataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(teamDataGridView_ColumnHeaderMouseClick);
        }

        private void teamGridReset()
        {
            // Provide the grid with a DataSource
            teamDataGridView.DataSource = teams;
           // teamDataGridView.bin
            // Make some columns invisible
            //teamDataGridView.Columns["IsHeader"].Visible = false;
            //teamDataGridView.Columns["TeamID"].Visible = false;
            //teamDataGridView.Columns["Abbr"].Visible = false;
            //teamDataGridView.Columns["Arena"].Visible = false;

            // Tell it the order of the columns I want visible
            teamDataGridView.Columns["City"].DisplayIndex = 0;
            teamDataGridView.Columns["NickName"].DisplayIndex = 1;

            // Change the selection mode so that I can only select an entire row rather than individual cells
            // And only let the user select one row at a time.
            teamDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            teamDataGridView.MultiSelect = false;
        }

        private void teamDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            // Get the set of selected rows.
            // Since multiselect is false, there will only be one row selected, so get the first one
            // DataBoundItem is the Team in that spot in the row.

            // There might not be a selected row, do nothing
            if (teamDataGridView.SelectedRows.Count == 0)
            {
                return;
            }

            // if there is, get the right Team
            selectedTeam = (Team) teamDataGridView.SelectedRows[0].DataBoundItem;

            // Set the team name and the Arena Name in the 'Team' pane
            //////// ----------------- C O R E   T A S K   1 -----------------------
            teamName.Text = selectedTeam.TeamName;
            arenaName.Text = selectedTeam.Arena;
            arenaCapacityLabel.Text = selectedTeam.ArenaCapacity.ToString();

            // Hint A2
            // change the teamName to the TeamName property once you've got it defined.

            // Hint B3
            // You'll want to set the arenaCapacitylabel.Text to the arena capacity property
        }


        // If a column header gets clicked, get the name of the column, show it in a messageBox, and then sort by Nickname.

        private void teamDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columnName = teamDataGridView.Columns[e.ColumnIndex].Name;
            //MessageBox.Show(columnName);
            
            // ------------- E X T E N S I O N   3 ------------- Sorting team Grid according 
                 teams=teams.OrderBy(x => typeof(Team).GetProperty(columnName).GetValue(x,null)).ToList();

            // remember, once the teams list has been sorted, the datasource has to be re-set
            teamGridReset();
            
        }
    }


    class Team : IComparable<Team>
    {
        public String TeamID {get;  private set;}
        public String Abbr { get; private set; }
        public String Nickname { get; private set; }
        public String City { get; private set; }
        public String Arena { get; private set; }

        // Hint B1
        // You'll want to define an ArenaCapacity property here
        // It will be an implicit property
        /// ------------- C O R E   T A S K   2 ------------------ Adding Arena Capacity property to Class Team
        public int ArenaCapacity { get; private set; }
        // Property which notes that this Team was loaded from a header line in the file

        public bool IsHeader
        {
            get
            {
                return TeamID == "TEAM_ID";
            }
        }

        // Hint A1 -- if we want to add a TeamName calculated property to the class
        // it will look a lot like IsHeader above.
        // Of course, TeamName will return a String calculated by concatenating City and Nickname
        /// --------------- C O R E   T A S K   1 ----------------- Calculated/derived property
        public string TeamName
        {
            get
            {
                return City + " " + Nickname;
            }
        }

        public int CompareTo(Team otherTeam)
        {
            return this.City.CompareTo(otherTeam.City);
        } 

        public static int CompareByNickname(Team t, Team u)
        {
            // Short version
            //return t.Nickname.CompareTo(u.Nickname);

            // Can also build things out in full.
            // A Comparision function should return -1 if t is smaller
            // 0 if they are equal
            // 1 if t is bigger

            // I'm ignoring the complexity of whether t or u might be null,
            // and whether one or more of them might have an empty Nickname

            if (t.Nickname == u.Nickname)
            {
                return 0;
            }

            // We could use this if we were comparing numbers
            //if (t.Nickname < u.Nickname)
            //{
            //    return -1;
            //} else
            //{
            //    return 1;
            //}

            return String.Compare(t.Nickname, u.Nickname);

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

            // Hint B2
            // you'll want to read the value of ArenaCapacity from data[9].
            // -------------------------  C O R E   T A S K   2  -------------------------- 
            int ac;
            if (Int32.TryParse(data[9], out ac))     // ----------- EXTENSION 1 ------------ converting ArenaCapacity to Int32
                t.ArenaCapacity = ac;
            else
                t.ArenaCapacity = 0;                // there are some rows without this value set, so I made it 0

            return t;
        }

    }
}
