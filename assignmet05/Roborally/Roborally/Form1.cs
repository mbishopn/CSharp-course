using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

// Assignment Ideas

// Add walls all the way around the board.
// Add Pits -- graphics in 2,4.  Like a wall, but blocks movement in any direction.
// Add a turn left and a turn right conveyor
// Add a turn counter
// Add a pusher that only pushed on turns 2 and 4


namespace Roborally
{
    public partial class RoboRallyForm : Form
    {

        Board b;
        public static int baseSquareDimension = 300;

        int boardWidth = 6;
        int boardHeight = 4;
        int boardZoom = 100;
        int turnNumber = 1;            // ----------- C O R E   T A S K   2 ----------- Turn counter

        RobotControls rc;

        public RoboRallyForm()
        {
            InitializeComponent();

            RoboRallyImageSet.loadImages();

            PictureBox p = new PictureBox();

             b = new Board(boardWidth, boardHeight, boardZoom, p);
            this.Controls.Add(p);
            b.drawBoard();

            rc = new RobotControls(this, new Point(boardWidth * boardZoom + 50, 0));
            rc.addAction("Move One", new Move());
            rc.addAction("Move Two", new Move(2));
            rc.addAction("Reverse", new Move(-1));
            rc.addAction("Turn Right", new Turn(Rotation.clockwise));
            rc.addAction("Turn Left", new Turn(Rotation.widdershins));
            rc.addAction("U Turn", new Turn(Rotation.uturn));

            this.Controls.Add(rc);

        }

        public async void doTurn_Click(object sender, System.EventArgs e)
        {
            Action a  = rc.getSelectedAction();
            b.addRobotAction(a);
            //MessageBox.Show("DoTurnhandler");
            b.doRobotAction();
            b.drawBoard();

            await Task.Delay(800);

            foreach(Action act in b.getActions())
            {
                act.performAction();
            }
            b.drawBoard();
         ///   -----------C O R E T A S K   2---------- - Turn counter
            if (turnNumber == 5)
                turnNumber = 1;
            else
                turnNumber++;
            turnLabel.Text = turnNumber.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

    class RobotControls : Panel
    {
        private int rowHeight = 30;

        Button doTurn = new Button();

        public RobotControls(RoboRallyForm f, Point topLeft)
        {
            this.Location = topLeft;
            this.Size = new Size(200, 500);

            doTurn.Text = "Do Turn";
            doTurn.Click += new EventHandler(f.doTurn_Click);
            this.Controls.Add(doTurn);
        }

        private static int actionCount;

        public void addAction(string name, Action a)
        {
            RadioButton rb = new RadioButton();
            rb.Location = new Point(0, actionCount * rowHeight);
            rb.Text = name;
            rb.Tag = a;
            this.Controls.Add(rb);

            if (actionCount == 0)
            {
                rb.Checked = true;
            }
       
            actionCount += 1;

            doTurn.Location = new Point(0, actionCount * rowHeight);
        }

        public Action getSelectedAction()
        {
            Action a = null;

            foreach(Control c in this.Controls)
            {
                if (c is RadioButton && ((RadioButton) c).Checked) {
                    //MessageBox.Show("Click Detected");
                    a = (Action)c.Tag;
                    //MessageBox.Show(a.ToString());
                    break;
                }
            }
            return a;
        }

    }

    class Board
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public int SpaceSize { get; set; }

        private BoardSpace[,] spaces;

        private PictureBox boardPictureBox;

        Robot robot;

        public Board(int w, int h, int s, PictureBox p)
        {
            Width = w;
            Height = h;
            SpaceSize = s;
            spaces = new BoardSpace[w, h];
            List<Wall> perimeter = new List<Wall>();

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    spaces[i, j] = new BoardSpace(this, i, j);
                    // ----------------  C O R E   T A S K   1  ------------------ Outside walls
                    if (j == 0)
                        perimeter.Add(new Wall(this, new Point(i, j), Direction.Up));
                    if (j == h - 1)
                        perimeter.Add(new Wall(this, new Point(i, j), Direction.Down));
                    if (i == 0)
                        perimeter.Add(new Wall(this, new Point(i, j), Direction.Left));
                    if (i == w - 1)
                        perimeter.Add(new Wall(this, new Point(i, j), Direction.Right));



                }
            }

            foreach (Wall x in perimeter)
                addFeature(x);

            robot = new Robot(this, new Point(0, 3));
            addFeature(robot);

            Wall w1 = new Wall(this, new Point(2, 2), Direction.Up);
            addFeature(w1);
            Wall w2 = new Wall(this, new Point(2, 2), Direction.Left);
            addFeature(w2);
            Wall w3 = new Wall(this, new Point(2, 2), Direction.Right);
            addFeature(w3);

            // Hint A:  The above adds three walls in three spots in the same square
            // You should be able to loop through the board setting up the external walls
            // Remember, there should be walls at co-ordinates (i,0), (0,j), (Width-1,j) and (i,Height-1)
            // (assuming we want to surround the board with walls

            Gear g = new Gear(this, new Point(1, 1), Rotation.clockwise);
            addFeature(g);
            // ---- E X T E N S I O N  6 ---- It might not seem the nicest map but i'm trying to see how robot behaves in a band
            // 
            Conveyor c1 = new Conveyor(this, new Point(0, 1), Direction.Up);c1.Actions[0].priority = 50;
            addFeature(c1);
            Conveyor c2 = new Conveyor(this, new Point(0, 2), Direction.Up);c2.Actions[0].priority = 50;
            addFeature(c2);
            Conveyor c3 = new Conveyor(this, new Point(0, 0), Direction.Right,Rotation.clockwise);
            c3.Actions[0].priority = 49;c3.Actions[1].priority = 50;
            addFeature(c3);
            Conveyor c4 = new Conveyor(this, new Point(1, 0), Direction.Right);c4.Actions[0].priority=43;
            addFeature(c4);
            Conveyor c5 = new Conveyor(this, new Point(2, 0), Direction.Right); c5.Actions[0].priority = 43;
            addFeature(c5);
            Conveyor c6 = new Conveyor(this, new Point(3, 0), Direction.Right); c6.Actions[0].priority = 43;
            addFeature(c6);
            Conveyor c7 = new Conveyor(this, new Point(4, 0), Direction.Right); c7.Actions[0].priority = 42;
            addFeature(c7);
            Conveyor c8 = new Conveyor(this, new Point(5, 0), Direction.Down, Rotation.clockwise);
            addFeature(c8);
            Conveyor c9 = new Conveyor(this, new Point(5, 1), Direction.Down);
            addFeature(c9);
            Conveyor cA = new Conveyor(this, new Point(5, 2), Direction.Down);
            addFeature(cA);

            Conveyor cB = new Conveyor(this, new Point(5, 3), Direction.Left,Rotation.clockwise);
            addFeature(cB);

            Pit P1 = new Pit(this, new Point(3, 3), Direction.Up);
            addFeature(P1);

            Winner ws = new Winner(this, new Point(4, 3));
            addFeature(ws);

            // Hint B6:  Add a pit to the board here in any empty square
            //Conveyor cl = new Conveyor(this, new Point(4, 3), Direction.Right, Rotation.widdershins);
            //addFeature(cl);
            //Conveyor cr = new Conveyor(this, new Point(5, 1), Direction.Left, Rotation.clockwise);
            //addFeature(cr);
            //Conveyor[,] band = new Conveyor[6, 4];




            boardPictureBox = p;

            boardPictureBox.Location = new Point(10, 10);
            boardPictureBox.Size = new Size(this.Width * this.SpaceSize, this.Height * this.SpaceSize);
            boardPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public void drawBoard()
        {
            // draw all the squares to a new board image, then replace it.
            Bitmap board = new Bitmap(this.Width * RoboRallyForm.baseSquareDimension, this.Height * RoboRallyForm.baseSquareDimension);

            Graphics g = Graphics.FromImage(board);

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    spaces[i, j].drawSpace(g);
                }
            }

            

            boardPictureBox.Image = board;
        }

        public void removeFeature(Feature f)
        {
            spaces[f.Location.X, f.Location.Y].removeFeature(f);
        }

        public void addFeature(Feature f)
        {
            spaces[f.Location.X, f.Location.Y].addFeature(f);
        }

        public void addRobotAction(Action a)
        {
            robot.Action = a;
            a.myFeature = robot;
            a.onlyMe = true;
            a.priority = 500;

            if (a is Move)
            {
                ((Move)a).setDirection(robot.Direction);
            }
        }

        public void doRobotAction()
        {
            robot.Action.performAction();
            drawBoard();
        }

        public BoardSpace getBoardSpace(Point p)
        {
            return spaces[p.X, p.Y];
        }

        public List<Action> getActions()
        {
            List<Action> actionList = new List<Action>();

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    foreach (Feature f in spaces[i, j].getFeatures())
                    {
                        foreach (Action a in f.Actions)
                        {
                            actionList.Add(a);
                        }
                    }
                }
            }
            actionList.Sort();
            return actionList;
        }
    }
    
    
    abstract class Action :IComparable<Action>
    {
        public int priority { get; set; }

        public bool onlyMe { get; set; }
        public abstract void performAction();
        public Feature myFeature { get; set; }
        public override string ToString()
        {
            return base.ToString();
        }

        public int CompareTo(Action b)
        {
            return this.priority.CompareTo(b.priority);
        }
    }

    // --------- E X T E N S I O N   5 -------- winner square
    class IsWin : Action
    {
        public override void performAction()
        {
            foreach(Feature f in myFeature.FullBoard.getBoardSpace(myFeature.Location).getFeatures())
            {
                if (f.IsMoveable)
                {
                    if (f is Robot)
                    {
                        MessageBox.Show("YOU WON!!");
                    }
                }
                    
            }
        }
    }

    class Move : Action
    {
        private int _distance;
        private Point _direction;

        public Move()
        {
            _distance = 1;
            _direction = Roborally.Direction.Up;
            priority = 50;
        }

        public Move(int d) : this()
        {
            _distance = d;
        }

        public void setDirection(Point d)
        {
            _direction = d;

            if (this._distance == -1)
            {
                _direction = Direction.spin(Rotation.uturn, d);
            }
        }

        public override void performAction()
        {
            if (onlyMe)
            {
                for (int i = 0; i < Math.Abs(_distance); i++)
                {
                    doMove(myFeature);
                }
            } else
            {
                List<Feature> ftl = (List<Feature>) myFeature.FullBoard.getBoardSpace(myFeature.Location).getFeatures();
                Feature moveable = ftl.Find(x => x.IsMoveable);
                if (moveable != null)
                {
                    doMove(moveable);
                }
            }



        }

        protected void doMove(Feature f)
        {
            // Can I move?  Are there any features on the current space that block movement in the direction I'm going?
            foreach(Feature ft in f.FullBoard.getBoardSpace(f.Location).getFeatures())
            {
                // If there is a feature blocking my move in that direction leaving, cannot move.
                if (ft.blocksMove(_direction, true))
                {
                    return;
                }
            }

            // What about in the space I'd like to move to?  I might be moving down into a square that blocks
            // moving up
            Point movingTo = new Point(f.Location.X + _direction.X, f.Location.Y + _direction.Y);

            foreach(Feature ft in f.FullBoard.getBoardSpace(movingTo).getFeatures())
            {
                // have to check if it blocks move in the uturn of my direction, entering.
                if(ft.blocksMove(Direction.spin(Rotation.uturn,_direction),false))
                {
                    return;
                }
            }
            // remove from current space
            f.FullBoard.removeFeature(f);

            // figure out new space
            f.Location = movingTo;

            // TODO
            // get anything moveable in new space
            // move that

            // put in new space
            f.FullBoard.addFeature(f);
        }

        public override string ToString()
        {
            return "Move: " + _distance + " " + _direction + " " + onlyMe;
        }
    }

    class Turn : Action
    {
        private Rotation _rotation;

        public Turn(Rotation r)
        {
            _rotation = r;
            priority = 100;
        }

 
        public override void performAction()
        {
            if (onlyMe)
            {
                myFeature.Direction = Direction.spin(_rotation, myFeature.Direction);
            }
            else
            {
                // what should I turn?  Anything moveable on this space.

                foreach (Feature ft in myFeature.FullBoard.getBoardSpace(myFeature.Location).getFeatures())
                {
                    if (ft.IsMoveable)
                    {
                        ft.Direction = Direction.spin(_rotation, ft.Direction);
                    }
                }
            }

        }
    }

    static class Direction
    {
        static public Point Up = new Point(0, -1);
        static public Point Down = new Point(0, 1);
        static public Point Left = new Point(-1, 0);
        static public Point Right = new Point(1, 0);


        static public Point spin(Rotation _rotation, Point p)
        {
            if (p == Direction.Up && _rotation == Rotation.clockwise)
            {
                return Direction.Right;
            }
            if (p == Direction.Right && _rotation == Rotation.clockwise)
            {
                return Direction.Down;
            }
            if (p == Direction.Down && _rotation == Rotation.clockwise)
            {
                return Direction.Left;
            }
            if (p == Direction.Left && _rotation == Rotation.clockwise)
            {
                return Direction.Up;
            }


            if (p == Direction.Left && _rotation == Rotation.widdershins)
            {
                return Direction.Down;
            }
            if (p == Direction.Down && _rotation == Rotation.widdershins)
            {
                return Direction.Right;
            }
            if (p == Direction.Right && _rotation == Rotation.widdershins)
            {
                return Direction.Up;
            }
            if (p == Direction.Up && _rotation == Rotation.widdershins)
            {
                return Direction.Left;
            }


            if (p == Direction.Left && _rotation == Rotation.uturn)
            {
                return Direction.Right;
            }
            if (p == Direction.Right && _rotation == Rotation.uturn)
            {
                return Direction.Left;
            }
            if (p == Direction.Up && _rotation == Rotation.uturn)
            {
                return Direction.Down;
            }
            if (p == Direction.Down && _rotation == Rotation.uturn)
            {
                return Direction.Up;
            }

            return p;
        }


    }

    public enum Rotation
    {
        clockwise, widdershins, uturn
    }


    abstract class Feature
    {
        public List<Action> Actions { get; protected set; }
        public abstract bool IsMoveable { get; }
        public Image Image { get; protected set; }

        public Point Location { get; set; }
        public Board FullBoard {get; set;}

        public Point Direction { get; set; }

        public Feature(Board f, Point l)        // ------- CONSTRUCTOR
        {
            Location = l;
            FullBoard = f;
            Direction = Roborally.Direction.Up;
            Actions = new List<Action>();
        }

        public Feature(Board f, Point l, Point d): this(f,l)         // --------- CONTRUCTOR
        {
            Direction = d;
        }

        public virtual bool blocksMove(Point direction, bool leaving)
        {
            return false;
        }

        public virtual bool blocksLight(Point direction)
        {
            return false;
        }

    }

    class BoardSpace
    {
        List<Feature> features = new List<Feature>();
        Point coords;

        // Need the fullBoard because some features have non-local effects
        Board fullBoard;

        public BoardSpace(Board b, int x, int y)
        {
            coords = new Point(x, y);
            fullBoard = b;

            // bottom layer feature is always a floor unless we are building a pit.
            addFeature(new Floor(b,coords));
        }

        public void addFeature(Feature f)
        {
            features.Add(f);
        }

        public void removeFeature(Feature f)
        {
            features.Remove(f);
        }

        public IReadOnlyList<Feature> getFeatures()
        {
            return features;
        }

        public void drawSpace(Graphics g)
        {
            Point drawCoords = new Point(coords.X * RoboRallyForm.baseSquareDimension, coords.Y * RoboRallyForm.baseSquareDimension);
            Size drawSize = new Size(RoboRallyForm.baseSquareDimension, RoboRallyForm.baseSquareDimension);
            foreach (Feature f in features)
            {
                Image im = (Image)f.Image.Clone();
                if (f.Direction == Direction.Right) {
                    im.RotateFlip(RotateFlipType.Rotate90FlipNone);  
                }
                if (f.Direction == Direction.Left)
                {
                    im.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                if (f.Direction == Direction.Down)
                {
                    im.RotateFlip(RotateFlipType.Rotate180FlipNone);
                }

                g.DrawImage(im, new Rectangle(drawCoords,drawSize));
            }
        }


    }

    class Robot : Feature
    {
        // The UI can change the action of a robot.
        // For most features, once the actions are set, they are set.
        public Action Action { get; set; }
        public override bool IsMoveable
        {
            get { return true; }
        }

        public Robot(Board f, Point l) : base(f,l)
        {
            this.Image = RoboRallyImageSet.RobotOne;
        }
    }

    class StationaryFeature: Feature
    {
        public override bool IsMoveable
        {
            get { return false; }
        }

        public StationaryFeature(Board f, Point l) : base(f,l)
        {
        }
        public StationaryFeature(Board f, Point l, Point d) : base(f, l, d)
        {
        }
    }

    class Floor : StationaryFeature
    {
        public Floor(Board f, Point l) : base (f,l)
        {
            this.Image = RoboRallyImageSet.Floor;
        }
    }

    class Gear : StationaryFeature
    {

        public Gear(Board f, Point l, Rotation r) : base(f, l)
        {
            Action a;
            if (r == Rotation.widdershins)
            {
                this.Image = RoboRallyImageSet.WiddershinsGear;
                a = new Turn(r);
            } else
            {
                this.Image = RoboRallyImageSet.ClockWiseGear;
                a = new Turn(Rotation.clockwise);
            }
            a.myFeature = this;
            this.Actions.Add(a);

        }
    }

    class Conveyor : StationaryFeature
    {
        public Conveyor(Board f, Point l, Point d) : base(f, l,d)
        {
            Move m = new Move();
            this.Image = RoboRallyImageSet.Conveyor;
            m.myFeature = this;
            m.priority = 40;
            m.setDirection(d);
            this.Actions.Add(m);
        }

        // Hint C4:  You'll likely want another constructor that takes the rotation direction if it is 
        // a left or right corner conveyor.  For that sort of conveyor you'll need to pick the correct
        // image, and also add a turn -- and once you've created the turn action, you'll need to 
        // set its priority to lower than the move action so the object will spin and then move.
        public Conveyor(Board f, Point l, Point d, Rotation r) : base (f,l,d)
        {
            Action t;
            if (r == Rotation.widdershins)
            {
                this.Image = RoboRallyImageSet.ConveyorL;
                t = new Turn(r);
            }
            else
            {
                this.Image = RoboRallyImageSet.ConveyorR;
                t = new Turn(Rotation.clockwise);
            }
            t.priority = 41;
            t.myFeature = this;
            this.Actions.Add(t);


            Move m = new Move();
            m.myFeature = this;
            m.priority = 60;
            m.setDirection(d);
            this.Actions.Add(m);
            MessageBox.Show(t.priority.CompareTo(m.priority) + "");
        }

    }


    class Wall : StationaryFeature
    {
        public Wall(Board f, Point l, Point d): base (f,l,d)
        {
            this.Image = RoboRallyImageSet.Wall;
        }


        // Walls block movement in their direction, either into or out of the square
        public override bool blocksMove(Point direction, bool leaving)
        {
            // Blocks movement whether entering or leaving.
            return this.Direction == direction;

            // Hint B5:  Pits block all movement leaving (e.g. if leaving == true) but not entering
        }

        // Walls block light in their direction
        public override bool blocksLight(Point direction)
        {
            // Hint B4:  Pits don't block light
            return this.Direction == direction;
        }
    }

    class Winner : StationaryFeature
    {
        public Winner(Board f, Point l) : base(f, l)
        {
            this.Image = RoboRallyImageSet.Winner;
            IsWin w = new IsWin();
            w.myFeature = this;
            w.priority = 102;
            this.Actions.Add(w);
            //Move m = new Move();
            //this.Image = RoboRallyImageSet.Conveyor;
            //m.myFeature = this;
            //m.setDirection(d);
            //this.Actions.Add(m);
        }
    }

        // Hint B3:  Pit class is going to look a lot like the Wall class.
        //------------ C O R E   T A S K   3 --------------   Pitt class
        class Pit : StationaryFeature // stationaryfeature is abstract for this
    {
        public Pit(Board f, Point l, Point d) : base(f, l, d)  /// ------- PIT CONSTRUCTOR
        {
            this.Image = RoboRallyImageSet.Pit;
        }


        // Walls block movement in their direction, either into or out of the square
        public override bool blocksMove(Point direction, bool leaving)
        {
            // Blocks movement whether entering or leaving.
            if (leaving == true)
                return true;
            return false;
            // Hint B5:  Pits block all movement leaving (e.g. if leaving == true) but not entering
        }

        // Walls block light in their direction  // -------- I think Pit doesn't need this method.
        public override bool blocksLight(Point direction)
        {
            // Hint B4:  Pits don't block light
            return this.Direction == direction;
        }
    }


    static class RoboRallyImageSet
    {
        static private Image tileset;
        static private Image board;

        static public Image Floor { get; private set; }
        static public Image RobotOne { get; private set; }
        static public Image Wall { get; private set; }
        static public Image ClockWiseGear { get; private set; }
        static public Image WiddershinsGear { get; private set; }

        static public Image Conveyor { get; private set; }
        
        // Hint B1:  You'll need to add a Pit property for the Pit iamge
        static public Image Pit { get; private set; }
        // Hint C1:  Just like for Pit, you'll need ConveyorLeft and ConveyorRight
        static public Image ConveyorL { get; private set; }
        static public Image ConveyorR { get; private set; }
        static public Image Winner { get; private set; }
        public static void loadImages()
        {
            tileset = Image.FromFile("../../Images/2005TileSetA.png");
            board = Image.FromFile("../../Images/2005board.png");

            Color transparentColor = extract(tileset, 0, 0).GetPixel(0, 0);

            // get the first floor Image
            //Floor = ((Bitmap)board).Clone(new Rectangle(new Point(0, 0), new Size(RoboRallyForm.baseSquareDimension, RoboRallyForm.baseSquareDimension)), board.PixelFormat);
            Floor = extract(board, 0, 0);
            RobotOne = extract(tileset, 3, 3, transparentColor);
            Wall = extract(tileset, 0, 4, transparentColor);
            Wall.RotateFlip(RotateFlipType.Rotate90FlipNone);
            ClockWiseGear = extract(tileset, 2, 0, transparentColor);
            WiddershinsGear = extract(tileset, 2, 0, transparentColor);
            Conveyor = extract(tileset, 0, 0, transparentColor);

            // Hint B2:  You'll need to extract the Pit image from tileset at 2,4.
            Pit = extract(tileset, 2, 4, transparentColor);
            // Hint C2:  YOu'll need to extract the conveyor left image from tileset at 0,1
            ConveyorL = extract(tileset, 0, 1, transparentColor);
            ConveyorL.RotateFlip(RotateFlipType.Rotate90FlipNone);
            // Hint C3:  You'll need to extract the conveyor right image from tileset at 0,1, then Do a RotateNoneFlipY (or maybe FlipX)
            ConveyorR = extract(tileset, 0, 1, transparentColor);
            ConveyorR.RotateFlip(RotateFlipType.Rotate270FlipY);
            // extracting winner square
            Winner = extract(tileset, 3, 1, transparentColor);

        }

        private static Bitmap extract(Image i, int x, int y)
        {
            Point topLeft = new Point(x * RoboRallyForm.baseSquareDimension, y * RoboRallyForm.baseSquareDimension);
            Size sz = new Size(RoboRallyForm.baseSquareDimension, RoboRallyForm.baseSquareDimension);
            Rectangle rect = new Rectangle(topLeft, sz);

            return ((Bitmap)i).Clone(rect, board.PixelFormat);
        }

        private static Bitmap extract(Image i, int x, int y, Color tc)
        {
            Bitmap bmp = extract(i, x, y);
            bmp.MakeTransparent(tc);
            return bmp;
        }

        public static void testImageCode()
        {
            Image tileset = Image.FromFile("../../Images/2005TileSetA.png");
            Image board = Image.FromFile("../../Images/2005board.png");

            PictureBox p = new PictureBox();
            p.Location = new Point(10, 10);
            p.Size = new Size(new Point(1050, 750));
            p.SizeMode = PictureBoxSizeMode.StretchImage;
            p.Image = tileset;

            Bitmap cropImage = ((Bitmap)tileset).Clone(new Rectangle(new Point(0, 0), new Size(300, 300)), tileset.PixelFormat);

            cropImage.MakeTransparent(cropImage.GetPixel(0, 0));

            PictureBox t = new PictureBox();
            t.Location = new Point(1100, 0);
            t.Size = new Size(300, 300);
            t.Image = cropImage;

            var boardImage = ((Bitmap)board).Clone(new Rectangle(new Point(0, 0), new Size(300, 300)), board.PixelFormat);

            PictureBox b = new PictureBox();
            b.Location = new Point(1100, 350);
            b.Size = new Size(300, 300);
            b.Image = boardImage;

            Image img = new Bitmap(boardImage.Width, boardImage.Height);
            using (Graphics gr = Graphics.FromImage(img))
            {
                gr.DrawImage(boardImage, new Point(0, 0));
                gr.DrawImage(cropImage, new Point(0, 0));
            }

            PictureBox o = new PictureBox();
            o.Location = new Point(1100, 700);
            o.Size = new Size(300, 300);
            o.Image = img;

        }
    }

}



// Let's start with an n x m board of tiles
// Add a robot (1) symbol
// Tiles have a set of features / actions somehow.

// Robots are separate

// Actions are a separate class. 

// Robots can also have actions, but of course, their actions vary, board actions don't.

// To draw the board, we draw each board square, which knows its own features and locations.

// A robot is just a self-moving feature whose action can change.