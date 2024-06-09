/*
 MANUEL BISHOP NORIEGA
 STUDENT ID: 4362207
 COMP1012 - ASSIGNMENT 2 - SNAKE GAME
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
// ------------ CORE TASK 4 --------- Loading a map file
// I'll need this library to be able to read files
using System.IO;

namespace Snake
{
    // Assignment Ideas
 
    // configure board (rows, columns), start button -- make sure that rows is <= 50 and columns <= 50
    // Make the snake 'n' blocks longer when it eats.  Hint:  that means not dequeuing the tail for *more* than one tick.
    // Create a function to add a random length-3 wall after the snake eats
    // High score file -- Input directory, read in the file, loop through, figure out where the new score sits, save file
    // Draw a better snake -- the 'square' isn't a very good graphic.  But you could do better, esp. if you up the square size
    // Capture grid clicks to let users toggle on and off walls in the set-up.
    
    public partial class Form1 : Form
    {
        // Constants cannot change
        // these represent the various states of a gamespace
        // Each square can have one of the below graphics

        const int empty = 0;
        const int wall = 1;
        const int snakebody = 2;
        const int snakehead = 3;
        const int food = 4;
        const int collision = 5;

          // How big is each square in pixels (e.g. 10 x 10)
        int squareSize = 10;
        int tmpSize = 0;
       
        // Starting speed of the timer
        int startingSpeed = 400;

        // Amount the timer gets faster each time that the snake eats
        int speedIncrement = 10;

        // Fastest speed that the snake can achieve
        int minInterval = 50;

        // Size of the board
        Point boardsize;

        // colour palette array matching the boardspace constants
        // That is, empty is black, walls are gray, snakebody yellow, snakehead yellowgreen, food is blue and collision is red.
        Brush[] pallette = { Brushes.Black, Brushes.Gray, Brushes.Yellow, Brushes.YellowGreen, Brushes.Blue, Brushes.Red };

        // 2-D array that will hold the game grid
        int[,] gamegrid; 

        // A Queue of points holding the co-ordinates of all the bits of the snake
        Queue<Point> snake = new Queue<Point>();

        // A Point holding the co-ordinates in the gamegrid of the snakehead
        Point snakeheadPosition;
        
        // Whether the game is done or not
        Boolean gameOver = false;
        Boolean pause = false;     // --------------------- who would reject having a pause button?
        // direction will change based on the key pressed
        // A direction is represented by a point, which shows us how the 
        // co-ordinate on the gamegrid will change by moving in that direction

        Point up = new Point(0, -1);
        Point down = new Point(0, 1);
        Point left = new Point(-1, 0);
        Point right = new Point(1, 0);

        // A point to hold the current direction.   Will be set to up, down, right, or left.
        Point direction;

        // Hint A1
        // If you want to store and show the current score, you'll want to 
        // declare an integer variable here to store it.
        
        // Hint C1
        // You need to create a variable to store the initial snake length
        int snakeLength;
        bool grow2; // -----  E X T E N S I O N   5  ------

        bool defaultmap = true;

        //--------------  F O R M   1  --------------
        public Form1()
        {
            InitializeComponent();

            // Set the starting speed of the timer
            //timer1.Interval = startingSpeed;

            // set the boardsize
            //boardsize = new Point(15, 20);

            // build a gamegrid of integers of the appropriate boardsize
            //gamegrid = new int[boardsize.X, boardsize.Y];

            // Set the initial direction to Up
            //direction = up;
            
            // Create the board
            //initializeBoard();
           
            listBox1.SelectedIndex = 0;
            // ----------- E X T E N S I O N  3 ------------ allow start button work after game ends ( complement )
            startButton.Enabled = false; // load map before starting the game right?

            hsLabel.Text = "0"; // ------------ E X T E N S I O N   4 ------------ session high score 
            
            // Create the snake
            //initializeSnake(1,6);

            
            // Create the initial food
            //createFood();
            

            // Tell the panel holding the game that it needs to be repainted.
            //gamespace.Invalidate();

            // Hint A2
            // You'll want to set the starting score to 0
            // You'll also want to show the starting score

            // Hint B1
            // We want the starting value of the 'squareSizeTextBox' to be whatever is stored in squareSpace
            // set the Text of your squareSizeTextBox to the value of the squareSpace variable

            // Some technical event handlers that need to be added to each control on the form
            // Basically this will let the arrow keys work even if there are input controls on the form.

            foreach (Control c in this.Controls)
            {
                c.PreviewKeyDown += new PreviewKeyDownEventHandler(myPreviewKeyDown);
            }

            //defaultmap = true;
            //boardsize.X = 15; boardsize.Y = 20;
            //gamegrid = new int[boardsize.X, boardsize.Y];
            //direction = up;
            //initializeGame();
            //initializeSnake(1, 6);
            //startButton.Enabled = true;


        }

        // --------- I N I T I A L I Z E    B O A R D   F U N C T I O N --------
        private void initializeBoard()
        {
            // we want walls all around the outside.
            // that means everything in the first and last row and column are walls.

            // Note that this will set the corners to walls twice, but that shouldn't hurt anything.

            for (int row = 1; row < boardsize.Y -1; row++)
            {
                gamegrid[0, row] = wall;
                gamegrid[boardsize.X-1, row] = wall;
            }

            for (int column = 0; column < boardsize.X; column++)
            { 
                gamegrid[column, 0] = wall;
                gamegrid[column, boardsize.Y-1] = wall;
            }

            
          

        }

        //------------ I N I T I A L I Z E   S N A K E   F U N C T I O N ------------
        private void initializeSnake(int minL, int maxL)
        {
            // Find the middle square of the board
            int headX = boardsize.X / 2;
            int headY = boardsize.Y / 2;

            // length 3 snake to start
            // Set the snakehead position to that middle square
            snakeheadPosition = new Point(headX, headY);
            snake.Clear();

            // Hint C2
            // If you are going to have variable length snakes, you have to 
            // do something a number of times that you aren't sure of.

            // A snake will *always* have a head.

            // For a snakeLength of between 2 and 7, there will be between 1 and 6 body segments
            // If you look at the tail segment code below, there are two identical lines
            // the only thing that changes is what we are adding to headY (+2 the first time, +1 the second time)

            // That suggests a loop.   We start at the bottom segment at the end of the tail, which has the 
            // largest Y co-ordinate

            // So the starting index of your for loop will want to be snakeLength -1  (e.g. 6 if we have a length 7 snake)
            // Each time through the loop it will get smaller

            // Start of Loop
            //------------- C O R E  T A S K  3 ---------- variable snake length
            // first create an aleatory snake length which will have between 1 and 6 body segments
            Random rnd = new Random();
            if (listBox1.SelectedIndex == 0)        // ------------- E X T E N S I O N   1 ------ user customizable snake size
                snakeLength = rnd.Next(minL,maxL);
            //MessageBox.Show(snakeLength.ToString());
            for (int i = snakeLength; i > 0; i--)
            {
                // Put a Length-2 tail on the snake, by enqueueing its last tail segment
                // and setting the value of the gamegrid at the right spot.
                snake.Enqueue(new Point(headX, headY + i));
                gamegrid[headX, headY + i] = snakebody;

                // Put a the middle segment in the queue and encode the gamegrid
                //snake.Enqueue(new Point(headX, headY + 1));
                //gamegrid[headX, headY + 1] = snakebody;

            }
            // End of Loop

            // Put the head in place
            snake.Enqueue(snakeheadPosition);
            gamegrid[headX, headY] = snakehead;

        }

        //--------- C R E A T E   F O O D   F U N C T I O N ----------
        private void createFood()
        {

            // Randomly pick a spot for the new food
            // If that square is not empty, try again
            // keep looping until you pick the right spot

            // Note:  If you ever get to a point where there are no 
            // empty squares, you break the game because this will loop forever and ever and ever and ever and ever and ...
            Random rnd = new Random();
            int x;
            int y;
            do
            {
                x = rnd.Next(0, boardsize.X);
                y = rnd.Next(0, boardsize.Y);
            } while (gamegrid[x, y] != empty);

            // Set the chosen spot to be food
            gamegrid[x, y] = food;

            // Speed up
            timer1.Interval -= speedIncrement;

            // But not too fast
            if (timer1.Interval < minInterval)
            {
                timer1.Interval = minInterval;
            }
        }

        // -------- M O V E   S N A K E   F U N C T I O N --------
        private void moveSnake()
        {
            // we want to move the snake in the current direction.
            // first, make the current snakeheadPosition on the gamegrid into a snakebody
            // then means dequeuing the tail, setting that part of the gameboard to empty
            // then setting a new snakeheadPosition, enqueuing that to the snake, and updating the gamegrid with that.
            

            gamegrid[snakeheadPosition.X, snakeheadPosition.Y] = snakebody;

            // the snakehead is about to move.
            snakeheadPosition = new Point(snakeheadPosition.X + direction.X, snakeheadPosition.Y + direction.Y);

            // If it is food, then great!  Eat the food, make the snake longer, and create more food.
            if (gamegrid[snakeheadPosition.X, snakeheadPosition.Y] == food)
            {
                // we make the snake longer by *not* dequeuing in this case.
                // Change the food to the snakehead
                gamegrid[snakeheadPosition.X, snakeheadPosition.Y] = snakehead;

                //-------------------- C O R E   T A S K   1 ------------ Keeping the score
                scoreLabel.Text = (Convert.ToInt32(scoreLabel.Text) + 1).ToString();
                // Hint A3
                // If you are keeping score, the snake just ate some food
                // Add one to the score, and update the score display
                grow2 = true;  // ----- E X T E N S I O N   5 ----
                createFood();
            }
            else if (gamegrid[snakeheadPosition.X, snakeheadPosition.Y] != empty)
            {
                // we have a collision and the game is over.
                gamegrid[snakeheadPosition.X, snakeheadPosition.Y] = collision;
                gameOver = true;
                
            } else // only thing left is empty
            {
                // set the snakehead
                gamegrid[snakeheadPosition.X, snakeheadPosition.Y] = snakehead;

                // Dequeue the snake
                // Hint E1 -- this is the part to skip the turn *after* the snake eats
                if (!grow2)
                {
                    Point tail = snake.Dequeue();
                    gamegrid[tail.X, tail.Y] = empty;
                }
                grow2 = false;

            }

            // Add the new snakehead to the snake queue
            snake.Enqueue(snakeheadPosition);

            // re-draw the board
            gamespace.Invalidate();
        }

        //-------- D R A W   G A M E   F U N C T I O N --------
        private void drawGame(Graphics g)
        {
            // Loop through the grid, and draw every square

            for (int row = 0; row < boardsize.Y; row++)
            {
                for (int column = 0; column < boardsize.X; column++)
                {
                    drawSquare(g, row, column, gamegrid[column, row]);
                }
            }

        }

        // ---------- D R A W   S Q U A R E   F U N C T I O N ----------
        private void drawSquare(Graphics g, int row, int column, int space)
        {
            // space is the type of space, and pallette holds the brushes based on the space values
            Rectangle square = new Rectangle(column * squareSize, row *squareSize, squareSize, squareSize);
            g.FillRectangle(pallette[space], square);
        }

        private void gamespace_Paint(object sender, PaintEventArgs e)
        {
            drawGame(e.Graphics);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (gameOver)
            {
                timer1.Stop();
                // ------------ E X T E N S I O N   4 -------------  high score
                if (Convert.ToInt32(hsLabel.Text) < Convert.ToInt32(scoreLabel.Text))
                    hsLabel.Text = scoreLabel.Text;
                // ------------------ E X T E N S I O N   3 ------------------- Start button ready for a new game
                startButton.Enabled = true;
                // let user know that he's lost
                MessageBox.Show($"SCORE:{scoreLabel.Text}", "YOU LOST");
                // reload last game map
                if (defaultmap)
                    defaultMapButton.PerformClick();
                else
                    loadMapButton.PerformClick();
                // get ready to play
                startButton.Focus();

            }
            else
            {
                moveSnake();
            }
        }

        private void Form1_KeyDown(object sender,KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right || e.KeyCode == Keys.L)
            {
                direction = right;
            }
            if(e.KeyCode == Keys.Left || e.KeyCode == Keys.J)
            {
                direction = left;
            }
            if(e.KeyCode == Keys.Up || e.KeyCode == Keys.I)
            {
                direction = up;
            }
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.K)
            {
                direction = down;
            }
            e.Handled = true;
        }

        // Start Button Click
        private void startButton_Click(object sender, EventArgs e)
        {
            // Set the focus onto the gamespace panel
            gamespace.Focus();

            // Start the timer (which starts the game because the timer moves the snake)
            timer1.Start();
            startButton.Enabled = false;
            
        }

        private void myPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                case Keys.Up:
                case Keys.Left:
                case Keys.Right:
                    e.IsInputKey = true;
                    break;
                case Keys.P:  // I like to be able to pause the game ;)
                    if (!pause)
                    { timer1.Stop(); pause = true;startButton.Enabled = true; }
                    else
                    { timer1.Start(); pause = false;gamespace.Focus(); startButton.Enabled = false; }
                    break;
            }
        }

        //----------------- E X T E N S I O N   1 ------ User customizable snake size
        // initially I would use this for boardsize but I decided to stick to requested textbox, but for extension this will work
        // user can select whether random snake size or specific size from 2 to 7 squares.
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != 0)
                snakeLength = Convert.ToInt32(listBox1.SelectedItem);
            //squareSize = Convert.ToInt32(listBox1.SelectedItem);
            //gamespace.Invalidate();
        }

        //----------------- C O R E   T A S K   2 ------ Changing board size REQUESTED SOLUTION
        // This function evaluates whether new board size is applied or not
        // if board size changes, then go to start button if not, stay on textbox and try again
        private void changeBoardSize()
        {
            try
            {
                if (Convert.ToInt32(textBox1.Text) >= 5 && Convert.ToInt32(textBox1.Text) <= 25)
                {
                    squareSize = Convert.ToInt32(textBox1.Text);
                    gamespace.Invalidate();
                    startButton.Focus();
                }
                else
                {
                    textBox1.Text = tmpSize.ToString();
                    MessageBox.Show("can't use that value");
                    textBox1.Focus();
                }

            }
            catch
            {
                textBox1.Text = tmpSize.ToString();
                MessageBox.Show("can't use that value");
                textBox1.Focus();
            }

        }

        // I can apply board size change by leaving the textbox 
        private void textBox1_Leave(object sender, EventArgs e)
        {
            changeBoardSize();
        }

        // I can apply change to board size when user press enter key
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar==(int)Keys.Enter)
            {
                e.Handled = true;
                changeBoardSize();
            }
        }

        // I wan't to store current board size to set it back if user input an invalid value
        private void textBox1_Enter(object sender, EventArgs e)
        {
            tmpSize = Convert.ToInt32(textBox1.Text);
        }

        private void initializeGame()
        {
            Array.Clear(gamegrid, boardsize.X, boardsize.Y);
            timer1.Interval = startingSpeed;
            initializeBoard();
            createFood();
            gamespace.Invalidate();
            startButton.Enabled = true;
            startButton.Focus();
            gameOver = false;
            pause = false;
            scoreLabel.Text = "0";
            bool grow2 = false;

        }


        //------------ D E F A U L T   M A P    B U T T O N   C L I C K -------------
        private void defaultMapButton_Click(object sender, EventArgs e)
        {
            defaultmap = true;
            // -------- E X T E N  S I O N   2 --------- Customizable board size. this work only for Default Map, not for map loaded from file.
            // ----- I set 2 textboxes to let user input size (cols and rows), then make those values boardsize X and Y
            // ------ I know I should validate few things here and/or handle possible exceptions, but clock is ticking and need to speedup,
            // ------ so because basic functionality is there, this is good enough for now (I hope so :D)
            boardsize.X = Convert.ToInt32(bsColsTextbox.Text);boardsize.Y = Convert.ToInt32(bsRowsTextbox.Text);
            gamegrid = new int[boardsize.X, boardsize.Y];
            direction = up;
            initializeGame();
            initializeSnake(1, 6);
            startButton.Enabled = true;
        }

        //----------- L O A D   M A P   F I L E   B U T T O N   C L I C K ---------
        private void loadMapButton_Click(object sender, EventArgs e)
        {
            defaultmap = false;
            int numLine = 0,Cols=0, Rows = 0;
            snakeLength = 0;
            snake.Clear();
            startButton.Enabled = true;
            try
            {
                foreach (var line in File.ReadLines("../../Boards/BoardA.txt"))
                {
                    int col = 0;
                    switch (numLine)
                    {
                        case 0:
                            Cols = Convert.ToInt32(line.ToString());
                            MessageBox.Show(Cols + "");
                            boardsize.X = Cols;
                            bsColsTextbox.Text = Cols.ToString();
                            break;
                        case 1:
                            Rows = Convert.ToInt32(line.ToString());
                            MessageBox.Show(Rows + "");
                            boardsize.Y = Rows;
                            bsRowsTextbox.Text = Rows.ToString(); ;
                            gamegrid = new int[boardsize.X, boardsize.Y];
                            break;
                        case var x when (numLine > 1 && numLine < Rows + 2):
                            foreach (string value in line.Split(','))
                            {
                                if (col < Cols)
                                { gamegrid[col, numLine - 2] = Convert.ToInt32(value); col++; }
                                else
                                { MessageBox.Show("Error loading map on line " + (numLine + 1)); break; }

                            }
                            break;
                        case var x when (numLine > Rows + 1):
                            snakeLength++;
                            string[] data = line.Split(',');

                            if (data[0] == "3")
                            {
                                snakeheadPosition = new Point(Convert.ToInt32(data[1]), Convert.ToInt32(data[2]));
                                gamegrid[snakeheadPosition.X, snakeheadPosition.Y] = snakehead;
                            }
                            else
                            {
                                snake.Enqueue(new Point(Convert.ToInt32(data[1]), Convert.ToInt32(data[2])));
                                gamegrid[Convert.ToInt32(data[1]), Convert.ToInt32(data[2])] = snakebody;
                            }
                            break;
                    }
                    numLine++;
                }

                snake.Enqueue(snakeheadPosition);
                direction = up;
                initializeGame();
            }
            catch
            {
                MessageBox.Show("Error loading map file");
                
            }
            //MessageBox.Show((snakeLength+1) + "mide la serpiente");
            //initializeSnake(snakeLength, snakeLength);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // ----------------- E X T E N S I O N   6 ------------- interactive walls, nice task !
        private void gamespace_MouseClick(object sender, MouseEventArgs e)
        {

            xyMousePointer.Text = $"X: {e.Location.X/squareSize}  Y: {e.Location.Y/squareSize}";
            if (((e.Location.X / squareSize) < boardsize.X) && ((e.Location.Y / squareSize) < boardsize.Y))
            {
                gamegrid[e.Location.X / squareSize, e.Location.Y / squareSize] = wall;
                gamespace.Invalidate();
            }
        }




        // Hint B2
        // When the value of your textbox changes, you should re-set the size
        // of the square.  If you add a squareSizeTextBox and double-click it,
        // you should get a squareSizeTextBox_TextChanged handler 
        // In that handler, you want to set squareSize to the value of the textbox
        // And then you'll want to call gamespace.Invalidate() to get it to repaint



        // Hint D

        // Read the board configuration from a save file -- first line is columns, second line is rows, then you have 'rows' lines
        //    each line is a comma-separated string of values that match the constants we've defined.
        // Finally, you have the snake queue to build.

        //    e.g. 
        //    10
        //    10
        //    1,1,1,1,1,1,1,1,1,1
        //    1,0,0,0,0,0,0,0,0,1
        //    1,0,0,0,5,0,0,0,0,1
        //    1,0,0,1,1,1,1,0,0,1
        //    1,0,0,0,0,0,0,0,0,1
        //    1,0,0,0,0,0,0,0,0,1
        //    1,0,0,0,3,0,0,0,0,1
        //    1,0,0,0,2,0,0,0,0,1
        //    1,0,0,0,2,0,0,0,0,1
        //    1,1,1,1,1,1,1,1,1,1
        //    2,4,8
        //    2,4,7
        //    3,4,6

        //   that is a file representing a 10x10 board with a wall in the middle, a snake at the bottom heading right towards it, 
        //   and food starting on the other side of the wall
        //   The tail of the snake is at 4,8, the middle is at 4,7, and the head is at 4,6

        //  The above file is in the Boards directory in the project, in a file called BoardA.txt

        //  Create a button for 'Default'
        //  Create a button for the above board

        //  When 'Default' is clicked, that is the default map.
        //  set boardSize= new Point(15,20), then call initializeBoard(), initializeSnake(), createFood()

        // When 'BoardA' is clicked, we read in the file, and that builds the board

        // read the file and get an array of each line
        // string[] fileLines = File.ReadAllLines("../../Boards/BoardA.txt") 

        // the first line (fileLines[0]) should have one number
        // That is the number of columns

        // the second line (fileLines[1]) should have one number
        // That is the number of rows.

        // Once you have that, you can set the boardSize = new Point(columns,rows)

        // Now you need a loop that reads lines 2 through rows+1
        // Hint start at fileLines[2].   The final index should be rows +1

        // Each line consists of several columns.   They are separated by commas.
        // you can .split(',') the line just like we did to get 'words' in the Word Frequency code

        // For every number, you need to know what row and column you are in
        // Set the value of gamegrid(column,row) to the number you are looking at
        // Hint:  the map row is two less than the file row
        // Hint:  if you set string[] mySpaces = line.split(',') then you can use an indexed for loop to walk through the columns
        // Hint:  the index of the for loop will give you the column.

        // Finally, you need to build the snake.
        // For a length three snake, that will be the last 3 lines of the file
        // You can .split(',') those too into a data array
        // data[0] will be whether this is a tail or the head
        // data[1] will be the column and data[2] will be the row.

        // You'll need to create a new Point (data[1],data[2])
        // And then you will need to snake.Enqueue() the new point.



    }
}
