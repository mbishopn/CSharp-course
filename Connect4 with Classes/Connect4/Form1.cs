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
        BoardSpace[,] board = new BoardSpace[7, 6];

        // Want an array of possible players, including the empty space
        Player playerOne;
        Player playerTwo;

        // We also need to know whose turn it is (player 1 or player 2)
        BoardSpace currentPlayer;


        // We need to know who won -- to start, nobody
        BoardSpace winner;

        // Winner Hint 1
        // we could set up an array of 4 Points here that will hold the 4 winning
        // checkers if there is a winner.
        Point[] winningCheckers = new Point[4];

        public Connect4()
        {
            InitializeComponent();

            // set a form property that may or may not help with grpahical rendering issues
            this.DoubleBuffered = true;

            // Start with the winnerBox and the winnerLabel invisible.
            // We'll make them pop out when there is a winner.
            winnerBox.Visible = false;
            winnerLabel.Visible = false;

            playerSetup();
            boardSetup();

        }

        private void playerSetup()
        {
            // Create the players and set the current player
            Player p1 = new Player();
            p1.name = "Player One";
            p1.id = 1;
            p1.colour = Brushes.Yellow;
            p1.isEmptySpace = false;

            Player p2 = new Player();
            p2.name = "Player Two";
            p2.id = 2;
            p2.colour = Brushes.Red;
            p2.isEmptySpace = false;

            playerOne = p1;
            playerTwo = p2;

            currentPlayer = new BoardSpace();
            currentPlayer.rect = new Rectangle(10, 10, 80, 80);
            currentPlayer.player = p1;

            winner = new BoardSpace();
            winner.rect = new Rectangle(10, 10, 80, 80);
            winner.isEmpty = true;
        }

        private void boardSetup()
        {

            // For every column in the board
            for (int column = 0; column < 7; column++)
            {
                // for every row in each column
                for (int row = 0; row < 6; row++)
                {
                    // Create a new boardspace that starts empty
                    BoardSpace square = new BoardSpace();
                    square.column = column;
                    square.row = row;
                    square.isEmpty = true;
                    square.player = null;
                    square.rect = new Rectangle(column * 100 + 10, row * 100 + 10, 80, 80); // squares know where to draw themselves

                    board[column, row] = square;
                }
            }
        }

        private Brush pickBrushColour(BoardSpace square)
        {
            // Start by setting the brush Colour to White
            Brush brush = Brushes.White;

            if (!square.isEmpty)
            {
                brush = square.player.colour;
            }                

            // return the brush we've chosen.
            return brush;
        }
        private void drawBoard(Graphics g) {

            // We have a graphics context that represents the board, a 7 x 6 grid
            // We are going to use 100 x 100 pixel squares

            // For every column in the board
            for (int column = 0; column < 7; column++)
            {
                // for every row in each column
                for (int row = 0; row < 6; row++)
                {
                    drawChecker(g, board[column,row]);
                }
            }


        }

        private void drawChecker(Graphics g, BoardSpace square)
        {
            // pick the brush colour, then fill the ellipse in the square's defined rectangle
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.FillEllipse(pickBrushColour(square),square.rect);

        }


        // Switch the current player
        private void switchPlayer()
        {
            if (currentPlayer.player == playerOne)
            {
                currentPlayer.player = playerTwo;
            } else
            {
                currentPlayer.player = playerOne;
            }
        }

        // play a checker in a column

        private void playColumn(int column)
        {

            // If the top row is full, can't play here
            // Just stop and do nothing
            if (!board[column,0].isEmpty)
            {
                return;
            }

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
                if (board[column,row].isEmpty)
                {
                    // If this spot is empty, then I could play here
                    rowToPlay = row;
                }
                    
            }

            // set the last playable row in this column to the current player

            board[column, rowToPlay].isEmpty = false;
            board[column, rowToPlay].player = currentPlayer.player;

            // See if anyone has won!
            checkWinner();

            // switch player.
            switchPlayer();

            // We've changed whose turn it is and the representation of the board.
            // Better make them re-draw themselves

            boardBox.Invalidate(); // force re-draw
            playerBox.Invalidate();
        }

        private void winChecker(int startColumn, int endColumn, int startRow, int endRow, int colMultiplier, int rowMultiplier)
        {
            // Starting at startColumn and going to endColumn
            for (int column = startColumn; column <= endColumn; column++)
            {
                // Starting at startRow and going to endRow
                for (int row = startRow; row <= endRow; row++)
                {
                    // If this spot is empty, then we can't have a winner
                    if (board[column, row].isEmpty) { continue; }

                    // We have a potential winner -- if we move 3 more steps in the appropriate direction and 
                    // we keep seeing the same player as the potential winner, then we have an actual winner!

                    Player potentialWinner = board[column, row].player;

                    // Check the next 3 rows and columns, moving each one each time, if they all match, then we have a winner!
                    for (int winCheck = 1; winCheck <= 3; winCheck++)
                    {
                        // Figure out which column and row to actually check 
                        int colCheck = column + colMultiplier * winCheck;
                        int rowCheck = row + rowMultiplier * winCheck;

                        // See if that was a winner.
                        if (board[colCheck, rowCheck].player != potentialWinner)
                        {
                            potentialWinner = null;
                        }
                    }

                    if (potentialWinner != null)
                    {
                        winner.isEmpty = false;
                        winner.player = potentialWinner;
                        showWinner();
                        return;
                    }

                }
            }
        }

        private void checkWinner()
        {
            //checkHorizontalWinner();
            //checkVerticalWinner();
            //checkDownRightWinner();
            //checkDownLeftWinner();

            winChecker(0, 3, 0, 5, 1, 0); // horizontal-- first 4 columns, all rows, move across columns
            if (winner.isEmpty)
            {
                winChecker(0, 6, 0, 2, 0, 1); // vertical -- all columns, first 3 rows, move down rows
            }
            if (winner.isEmpty)
            {
                winChecker(0, 3, 0, 2, 1, 1); // right down diag-- first 4 columns, first 3 rows, move one column and one row
            }
            if (winner.isEmpty)
            {
                winChecker(3, 6, 0, 2, -1, 1); // left down diag-- last 4 colums, first 3 rows, move one column left and one row down
            }

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
            drawChecker(e.Graphics, currentPlayer);
        }

        // To draw the winnerBox, draw a check in the brushcolour
        // of the winnning player
        private void winnerBox_Paint(object sender, PaintEventArgs e)
        {
            drawChecker(e.Graphics, winner);
        }


        // If any column button gets clicked, try to play a checker
        // in that column.

        private void button1_Click(object sender, EventArgs e)
        {
            playColumn(0);
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

    }
}

// A player has a name, an Id, and a brush colour
// The name is new, the Id is 0, 1, or 2 as before
// The colour is just the colour of the brush for that player, 
// no need for a function to look up the brush colour from the playerId
// There is also a boolean specifying whether this player represents the empty space

class Player
{
    public String name;
    public int id;
    public Brush colour;
    public bool isEmptySpace;
}

class BoardSpace
{
    public Player player;
    public bool isEmpty;
    public int column;
    public int row;
    public Rectangle rect;
}

