using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect4
{
    public partial class Connect4 : Form
    {
        // the board is a 7 x 6 grid of columns and rows
        // column 0 is at the left, column 6 is at the right
        // row 0 is at the top, row 5 is at the bottom
        int[,] board = new int[7, 6];

        // We also need to know whose turn it is (player 1 or player 2)
        int player;
 // -----------------------   C O R E    T A S K    1   --------------------- Creating a brushes array to be used with pickBrushColour()
        Brush[] brushcolor = new Brush[] { Brushes.White, Brushes.Yellow, Brushes.Red,Brushes.Black };

        // We need to know who won -- to start, nobody
        int winner;
 // -----------------------   C O R E    T A S K    2   --------------------- using an array to store winner checkers/coordinates.
        // ----I tried both, points and bidimensional array. 
        //----Q U E S T I O N---: for points struct is there a way to assign X and Y values in a single command?
        Point[] wpts = new Point[4];
        //int[,] wpts_array = new int[4, 2]; //----- bidimensional array to store winner checkers

        // Winner Hint 1
        // we could set up an array of 4 Points here that will hold the 4 winning
        // checkers if there is a winner.


        public Connect4()
        {
            InitializeComponent();

            // set a form property that may or may not help with grpahical rendering issues
            this.DoubleBuffered = true;
            //--- I created this function to try many times when added an extension/functionality
            resetGame();
            // ----- using a single event handler for column buttons -------
            foreach (Button b in this.Controls.OfType<Button>().Where(x=>x.Name.StartsWith("button")))
            {
                b.Click += new System.EventHandler(this.columnButton_Click);
            }
        }

        // ------------------------------- R E S E T    F U N C T I O N  ------------------------------------
        private void resetGame()
        {
            //--------------    E X T E N S I O N    4    --------------- buttons to change player's color, default p1:yellow, p2:red
            csP1.BackColor = Color.Yellow;
            csP2.BackColor = Color.Red;
            resetButton.Visible = false; //--- reset button is hidden until p1 makes first move
            //--- clean up the board, delete all moves
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 6; j++)
                    board[i, j] = 0;
            //--- set all top buttons to initial state
            foreach (Button x in this.Controls.OfType<Button>())
            { x.Enabled = true;x.BackColor = Color.LightGray; x.ForeColor = Color.Black; }

            // Start with the winnerBox and the winnerLabel invisible.
            // We'll make them pop out when there is a winner.
            winnerBox.Visible = false;
            winnerLabel.Visible = false;
            //--- reset checkers color
            brushcolor[1] = Brushes.Yellow;
            brushcolor[2] = Brushes.Red;
            //--- first player 1
            player = 1;
            //--- and no winner yet
            winner = 0;
            // redraw everything
            boardBox.Invalidate();
            playerBox.Invalidate();
            winnerBox.Invalidate();

        }

        // --------------------------    C O R E    T A S K    1    -------------------------- using brush array to select colors
        private Brush pickBrushColour(int playerValue)
        {
            return brushcolor[playerValue];
        }

        //----------------------------- D R A W    B O A R D   F U N C T I O N --------------------------
        private void drawBoard(Graphics g) {

            // We have a graphics context that represents the board, a 7 x 6 grid
            // We are going to use 100 x 100 pixel squares

            //----- boardbox, playerbox and winnerbox color
            winnerBox.BackColor=playerBox.BackColor=boardBox.BackColor = Color.CornflowerBlue;
            
            // For every column in the board

            for (int column = 0; column < 7; column++)
            {
                // for every row in each column
                for (int row = 0; row < 6; row++)
                {
                    // pick the right brush colour based on what the board
                    // representation says the checker colour is.
                    Brush brush = pickBrushColour(board[column, row]);

                    // draw the checker.
                    // we are going to draw the checker on Graphics object g
                    // we are going to find the top left of the box for this column and row
                    // and start 10 pixels after that in both cases
                    // We also pass in the brush colour that we chose above
                    drawChecker(g, column * 100 + 10, row * 100 + 10,brush,100);
                }
            }

 // ---------------------------     C O R E   T A S K   2     --------------------- Drawing circles to show the winners
            if (winner > 0)
            {
                for (int i = 0; i <= 3; i++)
                {   
                    drawChecker(g, wpts[i].X * 100 + 10, wpts[i].Y * 100 + 10, brushcolor[3], 70);
                }
 // -----------------------     E X T E N S I O N    1     ------------------------ Drawing a line accross winners centers
                Pen greenpen = new Pen(Color.Green, 4);
                g.DrawLine(greenpen, wpts[0].X*100+50,wpts[0].Y*100+50,wpts[3].X*100+50,wpts[3].Y*100+50);
                //g.DrawLine(greenpen, wpts_array[0,0] * 100 + 50, wpts_array[0,1] * 100 + 50, wpts_array[3,0]*100+50, wpts_array[3,1]*100+50);
                
            }
            // Winner Hint 3
            // If there is a winner, we want to draw a smaller circle in the middle of the winning checkers.
            // So first, we only want to do this if there is a winner -- everything should be wrapped in an
            // if block.   Then, for every Point p in our list of winning Checkers, we want to draw a circle.
            // that sounds like a loop to me -- what kind is easiest to do something for every element of a collection?

            // If we have a Point p, we need to figure out the spot to draw the rectangle
            // This is the math:
            // Rectangle spot = new Rectangle(p.X * 100 + 40, p.Y * 100 + 40, 20, 20);
            // Then you can use fillElipse and give it a brush and the rectangle above to draw the spot.

        }

        private void drawChecker(Graphics g, int x, int y, Brush brush,int percentage)
        {
            // We are going to pick a rectangle that starts at x,y on Graphics context g
            // we are going to draw an 80 x 80 rectangle (which is in fact a square)
            // We are going to put an ellipse in that rectangle (which is in fact a circle because the rectangle is a square)
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Rectangle spot = new Rectangle(x - 10 + ((100 - (80 * percentage / 100)) / 2), y -10 + ((100 - (80 * percentage / 100)) / 2), 80 *percentage/100, 80*percentage/100);
            g.FillEllipse(brush, spot);
        }

        // Switch the current player
        private void switchPlayer()
        {
            if (player == 1)
            {
                player = 2;
            }
            else
            {
                player = 1;
            }
        }

        // play a checker in a column

        private void playColumn(int column)
        {

            // If the top row is full, can't play here
            // Just stop and do nothing
            if (board[column,0] > 0)
            {
                return;
            }
            resetButton.Visible = true;
            // I'm going to start at the top and figure out if 
            // i could play a checker in this row.
            // If I *can*, then great, the checker will fall down 
            // to this row.
            // If I *can't*, then I'm not able to play in *this* row,
            // but since rowToPlay still is set to the row above me,
            // that is where I could play.

            int rowToPlay = 0;

            for (int row = 0; row < 6; row++)
            {
                if (board[column,row]==0)
                {
                    // If this spot is empty, then I could play here
                    rowToPlay = row;
                }
                    
            }

            // set the last playable row in this column to the current player

            board[column, rowToPlay] = player;
                
            // See if anyone has won!
            checkWinner();

            // switch player.
            switchPlayer();

            // We've changed whose1turn it is and the representation of the board.
            // Better make them re-draw themselves

            boardBox.Invalidate(); // force re-draw
 // ---------------    E X T E N S I O N    3    -------------- pop message to let user know column is full and now button is disabled
            if (rowToPlay == 0 && winner==0)
            {
                MessageBox.Show("NO MORE CHECKERS OVER HERE", "COLUMN " + (column + 1) + " FULL.");
                foreach (Button x in this.Controls.OfType<Button>().Where(x => x.Name.EndsWith((column + 1).ToString())))
                {
                    x.ForeColor = Color.White;x.BackColor = Color.Red;
                    x.Enabled = false;
                }
            }
            playerBox.Invalidate();
        }

        /*
        private void checkHorizontalWinner()
        {
            // On every row
            for (int row = 0; row < 6; row++)
            {
                // Check the first 4 columns as starting points to win.
                for (int column = 0; column < 4; column++)
                {
                    // If this spot is empty, then we can't have a winner
                    if (board[column,row] == 0) { continue; }

                    int potentialWinner = board[column, row];
                    // Check the next 3 columns, if they all match, then we have a winner!
                    for (int winCheckColumn = 1; winCheckColumn <= 3; winCheckColumn++)
                    {
                        if (board[column+winCheckColumn,row] != potentialWinner)
                        {
                            potentialWinner = 0;
                        }
                    }

                    if (potentialWinner > 0)
                    {
                        winner = potentialWinner;
                        showWinner();
                        return;
                    }

                }
            }
        }

        private void checkVerticalWinner()
        {
            // On every column
            for (int column = 0; column < 7; column++)
            {
                // Check the first 3 rows as starting points
                for (int row = 0; row < 3; row++)
                {
                    // If this spot is empty, then we can't have a winner
                    if (board[column, row] == 0) { continue; }

                    int potentialWinner = board[column, row];
                    // Check the next 3 rows, if they all match, then we have a winner!
                    for (int winCheckRow = 1; winCheckRow <= 3; winCheckRow++)
                    {
                        if (board[column, row + winCheckRow] != potentialWinner)
                        {
                            potentialWinner = 0;
                        }
                    }

                    if (potentialWinner > 0)
                    {
                        winner = potentialWinner;
                        showWinner();
                        return;
                    }

                }
            }
        }
        private void checkDownRightWinner()
        {
            // On first 4 columns
            for (int column = 0; column < 4; column++)
            {
                // Check the first 3 rows as starting points
                for (int row = 0; row < 3; row++)
                {
                    // If this spot is empty, then we can't have a winner
                    if (board[column, row] == 0) { continue; }

                    int potentialWinner = board[column, row];
                    // Check the next 3 rows and columns, moving each one each time, if they all match, then we have a winner!
                    for (int winCheckDiag = 1; winCheckDiag <= 3; winCheckDiag++)
                    {
                        if (board[column+winCheckDiag, row + winCheckDiag] != potentialWinner)
                        {
                            potentialWinner = 0;
                        }
                    }

                    if (potentialWinner > 0)
                    {
                        winner = potentialWinner;
                        showWinner();
                        return;
                    }

                }
            }
        }
        private void checkDownLeftWinner()
        {
            // On last 4 columns
            for (int column = 3; column < 7; column++)
            {
                // Check the first 3 rows as starting points
                for (int row = 0; row < 3; row++)
                {
                    // If this spot is empty, then we can't have a winner
                    if (board[column, row] == 0) { continue; }

                    int potentialWinner = board[column, row];
                    // Check the next 3 rows and columns, moving each one each time, if they all match, then we have a winner!
                    for (int winCheckDiag = 1; winCheckDiag <= 3; winCheckDiag++)
                    {
                        if (board[column - winCheckDiag, row + winCheckDiag] != potentialWinner)
                        {
                            potentialWinner = 0;
                        }
                    }

                    if (potentialWinner > 0)
                    {
                        winner = potentialWinner;
                        showWinner();
                        return;
                    }

                }
            }
        }
        */

        private bool winChecker(int startColumn, int endColumn, int startRow, int endRow, int colMultiplier, int rowMultiplier)
        {
            // Starting at startColumn and going to endColumn
            for (int column = startColumn; column <= endColumn; column++)
            {
                // Starting at startRow and going to endRow
                for (int row = startRow; row <= endRow; row++)
                {
                    // If this spot is empty, then we can't have a winner
                    if (board[column, row] == 0) { continue; }

                    // We have a potential winner -- if we move 3 more steps in the appropriate direction and 
                    // we keep seeing the same player as the potential winner, then we have an actual winner!

                    int potentialWinner = board[column, row];

                    // Winner Hint 2a
                    // we might just have found the first checker of our winning combination.
                    // we want to set the 0-index of winningCheckers to a new Point object
                    // we use Point because it holds an x and a y value -- in this case, 
                    // we want to create a new Point(column,row) to store the column as the x and
                    // the y as the row.

                    // Check the next 3 rows and columns, moving each one each time, if they all match, then we have a winner!
                    for (int winCheck = 1; winCheck <= 3; winCheck++)
                    {
                        // Figure out which column and row to actually check 
                        int colCheck = column + colMultiplier * winCheck;
                        int rowCheck = row + rowMultiplier * winCheck;

                        // Winner Hint 2b
                        // We might have the winCheck-th checker of our winning combination.
                        // Set the winCheck index of winningCheckers to a new Point(colCheck,rowCheck);

                        // See if that was a winner.
 //----------------------------   C O R E   T A S K   2   ----------------------- storing possible winner's first spot into winner array
                        // wpts_array[0, 0] = column;
                        // wpts_array[0, 1] = row;
                        wpts[0].X = column;
                        wpts[0].Y = row;

                        if (board[colCheck, rowCheck] != potentialWinner)
                        {
                            potentialWinner = 0;
                        }
                        else
                        {
 //-----------------------------   C O R E   T A S K   2   ---------------------- storing last 3 checkers coordinates into winner array
                            wpts[winCheck].X = colCheck;
                            wpts[winCheck].Y = rowCheck;
                         //   wpts_array[winCheck, 0] = colCheck;
                         //   wpts_array[winCheck, 1] = rowCheck;
                        }

                    }

                    if (potentialWinner > 0)
                    {
                        winner = potentialWinner;
                        showWinner();
// ------------------------------    E X T E N S I O N    2    -------------------------- disabling buttons in columns
                        foreach (Button x in this.Controls.OfType<Button>())
                            x.Enabled = false;
                        resetButton.Enabled = true;
                        return false;
                    }
                }
            }return true;
        }

        private void checkWinner()
        {
            // I made winChecker() to return false when it founds a winner to avoid subsequent calls to it, otherwise it will
            // always be called 4 times and that could lead to store wrong values in winner array, this happened
            // only with vertical winnings, but I didn't test all possible cases so to be sure this was a quick fix.

            if (winChecker(0, 3, 0, 5, 1, 0))//; // horizontal-- first 4 columns, all rows, move across columns
            if(winChecker(0, 6, 0, 2, 0, 1))//; // vertical -- all columns, first 3 rows, move down rows
            if(winChecker(0, 3, 0, 2, 1, 1))//; // right down diag-- first 4 columns, first 3 rows, move one column and one row
            winChecker(3, 6, 0, 2, -1, 1); // left down diag-- last 4 colums, first 3 rows, move one column left and one row down


        }

        // If there is a winner, show the winnerBox and the label, and re-draw the winner box.

        private void showWinner()
        {
            winnerBox.Visible = true;
            winnerLabel.Visible = true;
            winnerBox.Invalidate();
        }


        // To paint the boardBox, call drawBoard
        private void boardBox_Paint(object sender, PaintEventArgs e)
        {
            drawBoard(e.Graphics);
        }

        // To paint the playerbox, draw a checker in the brushcolour
        // of the current player.

        private void playerBox_Paint(object sender, PaintEventArgs e)
        {
            drawChecker(e.Graphics, 10, 10, pickBrushColour(player),100);
        }

        // To draw the winnerBox, draw a check in the brushcolour
        // of the winnning player
        private void winnerBox_Paint(object sender, PaintEventArgs e)
        {
            drawChecker(e.Graphics, 10, 10, pickBrushColour(winner),100);
        }


        // If any column button gets clicked, try to play a checker
        // in that column.
        // --------- Using a single event handler for columns buttons

        private void columnButton_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            playColumn(Convert.ToInt32(b.Name[6].ToString()) - 1);
        }

        /*
        private void button1_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            playColumn(Convert.ToInt32(b.Name[6])-1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            playColumn(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            playColumn(2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            playColumn(3);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            playColumn(4);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            playColumn(5);
        }

          private void button7_Click(object sender, EventArgs e)
        {
            playColumn(6);
        }
        */

        //--------------    E X T E N S I O N    4    --------------- Function to change player's colors

        private void csPlayer_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {

                int flag = 0;
                foreach(Button x in groupBox1.Controls.OfType<Button>())
                {
                    if (x.BackColor == colorDialog1.Color)
                        flag++;
                }
                if (flag == 0 || b.BackColor==colorDialog1.Color)
                {
                    Brush newColor = new SolidBrush(colorDialog1.Color);
                    brushcolor[Convert.ToInt32(b.Name[3].ToString())] = newColor;
                    b.BackColor = colorDialog1.Color;
                }
                else
                    MessageBox.Show("Really, smart guy?");
            }
            boardBox.Invalidate();
            playerBox.Invalidate();
            winnerBox.Invalidate();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            resetGame();
        }
    }
}
