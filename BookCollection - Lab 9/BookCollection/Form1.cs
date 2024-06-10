using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookCollection
{
    public partial class Form1 : Form
    {
        List<Book> books = new List<Book>();

        public Form1()
        {
            InitializeComponent();

        }

        private void loadBooksButton_Click(object sender, EventArgs e)
        {
            // Add all the books to the List of books when the button is clicked.

            books.Clear();
            books.Add(new Book("Handmaid's Tale", "Margaret Atwood", 1985, BookGenre.ScienceFiction, BookFormats.MP3)); // wrong format to check validation
            books.Add(new Book("Alias Grace", "Margaret Atwood", 1996, BookGenre.Fiction,BookFormats.EPUB));
            books.Add(new SeriesBook("Fellowship of the Ring", "J.R.R. Tolkien", "Lord of the Rings", 1, 1954, BookGenre.Fantasy, BookFormats.Paperback));
            books.Add(new SeriesBook("The Two Towers", "J.R.R. Tolkien", "Lord of the Rings", 2, 1954, BookGenre.Fantasy, BookFormats.Hardcover));
            books.Add(new SeriesBook("The Return of the King", "J.R.R. Tolkien", "Lord of the Rings", 3, 1954, BookGenre.Fantasy, BookFormats.PDF));
            books.Add(new GraphicNovel("Preludes & Nocturnes", "Neil Gaiman", "Sam Keith", "The Sandman", 1, 1988, BookGenre.GraphicNovel, BookFormats.MOBI));
            books.Add(new GraphicNovel("The Doll's House", "Neil Gaiman", "Mike Dringenberg", "The Sandman", 2, 1990, BookGenre.GraphicNovel, BookFormats.CBZ));
            books.Add(new GraphicNovel("Dream Country", "Neil Gaiman", "Kelly Jones", "The Sandman", 3, 1990, BookGenre.GraphicNovel, BookFormats.CBZ));
            books.Add(new GraphicNovel("Season of Mists", "Neil Gaiman", "Matt Wagner", "The Sandman", 4, 1990, BookGenre.GraphicNovel, BookFormats.Softcover));
            books.Add(new Biography("A Beautiful Mind", "Sylvia Nasar", "John Nash", 1998, BookGenre.Biography, BookFormats.EPUB));

            // Hint A2:  Once you've created an AudioBook class, it is pretty boring if you don't add an AudioBook to the list.

           // ---------- I put this book format wrong on purpose to check format validation ----
            books.Add(new AudioBook("Alias Grace", "Margaret Atwood", 1996, BookGenre.Fiction,BookFormats.CBZ,"Audible"));

            // Add all the books in the list to the listbox.
            // Note that we can treat each as a book whether it is a Book, a SeriesBook, etc.

            foreach (Book b in books)
            {
                listBox1.Items.Add(b);
            }

        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // When we click on a book, find the selected item in the listbox
            Book b = (Book) listBox1.SelectedItem;

            // Now set the property grid to show the properties of that object
            propertyGrid1.SelectedObject = b;

            // Call our sample override and overload functions, and fill in the labels
            label2.Text = b.myBookType();
            label3.Text = b.getBookType();

            // This is code that we can use to make sure that the overloaded function is called
            // if b is a SeriesBook -- if it is being treated as a 'Book', we will never call
            // the SeriesBook.myBookType() function

            if (b is SeriesBook)
            {
                SeriesBook s = (SeriesBook)b;
                //label2.Text = s.myBookType();
            }
        }
    }

    // An enum of possible book Genres

    public enum BookGenre
    {
        Fiction,
        Mystery,
        ScienceFiction,
        Fantasy,
        Thriller,
        Romance,
        HistoricalFiction,
        YoungAdult,
        NonFiction,
        Biography,
        History,
        Science,
        SelfHelp,
        Food,
        Humour,
        Travel,
        GraphicNovel

    }

    // Hint B1: create a BookFormat enum with the 10 formats listed in the Lab requirements
    public enum BookFormats
    {
        Unknown,
        Hardcover,
        Softcover,
        Paperback,
        MOBI,
        EPUB,
        PDF,
        MP3,
        WAV,
        AAC,
        CBZ
    }
    // Simplifying assumption:  Books have one author

    class Book
    {

        // For each of the properties, we are going to specify a Category and a Description
        // This is both for reflection documentation for external classes and so that the 
        // PropertyGrid provides a richer interface.
        // Not going to add comments because the type and the attributes should explain what 
        // each property is.

        [CategoryAttribute("Book / Series"), DescriptionAttribute("Title of the published work")]
        public string Title { get; private set; }

        [CategoryAttribute("Who / When"), DescriptionAttribute("Main Author of the book")]
        public string Author { get; private set; }

        [CategoryAttribute("Who / When"), DescriptionAttribute("Year of publication")]
        public Int32 Year { get; private set; }

        [CategoryAttribute("Genre"), DescriptionAttribute("Genre of the Work")]
        public BookGenre Genre { get; private set; }

        // -------- C O R E  T A S K  3 ---------- Format validation backing store variable
        protected BookFormats _format;   // -- I made protected to avoid declaring this field in subclasses

        // -------- C O R E  T A S K  2 ---------- Adding Format property
        [CategoryAttribute("Format"), DescriptionAttribute("Available Book Format")]
        public virtual BookFormats Format
        {

            get => _format;
            set
            {
                List<BookFormats> validFormats = new List<BookFormats>() { BookFormats.Hardcover, BookFormats.Softcover, BookFormats.Paperback, BookFormats.EPUB, BookFormats.MOBI, BookFormats.PDF };
                if (validFormats.Contains(value))
                    _format = value;
                else   // I felt need to put this to provide some way to handle non-booktype compatible formats
                    MessageBox.Show("Setting this value to Unknown", "INVALIDFORMAT FOR THIS BOOK TYPE");
                    //throw new ArgumentOutOfRangeException("Invalid Format", "Inappropriate format for this book type, forcing a valid one");
            }
        }

        // Hint B2:  we want a Format property -- it is going to look almost *exactly* like Genre

        // Constructor to fill in the four properies 
        // Hint B3:  modify the constructor to take a BookFormat as well
        //----------- B O O K   C O N S T R U C T O R --------
        public Book (string t, string a, int y, BookGenre g,BookFormats f)
        {

            Title = t;
            Author = a;
            Year = y;
            Genre = g;
            Format = f;
        }

        // Function that will have to be Overloaded
        public string myBookType()
        {
            return "Book";
        }

        // Function that can be overridden
        public virtual string getBookType()
        {
            return "Book";
        }

        // Property that can be overridden

        [CategoryAttribute("BookType"), DescriptionAttribute("Type of Book")]
        public virtual string BookType
        {
            get
            {
                return "Book";
            }
        }

        // Override the Object.ToString function to better represent a book
        public override string ToString()
        {
            return $"{Title} ({Author}), {Year}.  {Genre}";
        }
    }

    // Simplifying assumption:   Books in Series can be given numbers

    class SeriesBook : Book
    {
        // Series Book is a derived class from the base class of Book, with two additional properties

        [CategoryAttribute("Book / Series"), DescriptionAttribute("Series the book belongs to")]
        public string Series { get; private set; }

        [CategoryAttribute("Book / Series"), DescriptionAttribute("Order of the book in this series")]
        public Int32 SeriesNumber { get; private set; }

        // Overload function
        public new string myBookType()
        {
            return "Series Book";
        }

        // Override function
        public override string getBookType()
        {
            return "Series Book";
        }

        // override property
        public override string BookType
        {
            get
            {
                return "Series Book";
            }
        }

        // Constructor takes all 6 bits of information required. 
        // Passes the title, author, year, and genre to the base constructor of Book
        public SeriesBook(string t, string a, string s, int n, int y, BookGenre g, BookFormats f) : base(t, a, y, g,f)
        {
            Series = s;
            SeriesNumber = n;
        }

        // Override the Book.ToString() function
        public override string ToString()
        {
            return $"{Title} ({Author}), {Series} #{SeriesNumber},  {Year}.  {Genre}";
        }
    }

    // Simplifying assumption:  Comic Books are graphic novels.  All Graphic novels come in series (definitely false, but often /generally true)
    // Simplifying assumption:  There is one Artist.  We are ignoring letterers and colourers

    // A GraphicNovel extends a SeriesBook and adds an Artist Property
    class GraphicNovel : SeriesBook
    {
        [CategoryAttribute("Who / When"), DescriptionAttribute("Artist of this Graphic Novel")]
        public string Artist { get; private set; }

        public new string myBookType()
        {
            return "Graphic Novel Series";
        }

        public override string getBookType()
        {
            return "Graphic Novel Series";
        }

        public override string BookType
        {
            get
            {
                return "Graphic Novel Series";
            }
        }

        // -------- C O R E  T A S K  3 ------- property overridden to comply graphic novel specific validation
        public override BookFormats Format
        {

            get => _format;
            set
            {
                List<BookFormats> validFormats = new List<BookFormats>() { BookFormats.Hardcover, BookFormats.Softcover, BookFormats.Paperback, BookFormats.EPUB, BookFormats.MOBI, BookFormats.PDF, BookFormats.CBZ };
                if (validFormats.Contains(value))
                    _format = value;
                else
                    MessageBox.Show("Invalid format");
            }
        }

        // Here the constructor takes 7 properties, and calls the base constructor (SeriesBook) with 6 of them.
        // Note that the Seriesbook base constructor will set two of those properties, and then call *its* base constructor (Book) with the other 4
        public GraphicNovel (string t, string a, string artist, string s, int n, int y, BookGenre g, BookFormats f) : base(t, a, s, n, y, g, f)
        {
            Artist = artist;
        }

        // Override the ToString() method
        public override string ToString()
        {
            return $"{Title} ({Author}, {Artist}(artist) ), {Series} #{SeriesNumber},  {Year}.  {Genre}";
        }
    }

    class Biography : Book
    {
        [CategoryAttribute("Who / When"), DescriptionAttribute("Subject of this Biography")]
        public string Subject { get; private set; }

        public override string BookType
        {
            get
            {
                return "Biography";
            }
        }

        public Biography(string t, string a, string s, int y, BookGenre g, BookFormats f) : base(t, a, y, g, f)
        {
            Subject = s;
        }
 
        public override string ToString()
        {
            return $"{Title} (Biography of {Subject} by {Author}), {Year}.  {Genre}";
        }
    }

    // ------------------ C O R E   T A S K   1 ------------------- AudioBook Class
    // Create an AudioBook class here according to the specifications of the assignment.
    // Hint A1:  it is going to look *very* much like the Book class
    class AudioBook : Book
    {
        [CategoryAttribute("Reader"), DescriptionAttribute("Where to listen this Book")]
        public string Reader { get; private set; }

        public AudioBook(string t, string a, int y, BookGenre g, BookFormats f, string r) : base(t, a, y, g, f)
        {
            Reader = r;

        }
        // -------- C O R E  T A S K  3 ------- property overridden to comply audiobook specific validation
        public override BookFormats Format
        {

            get => _format;
            set
            {
                List<BookFormats> validFormats = new List<BookFormats>() { BookFormats.MP3, BookFormats.AAC, BookFormats.WAV};
                if (validFormats.Contains(value))
                    _format = value;
                else          
                { MessageBox.Show("Invalid format for AudioBook"); _format = BookFormats.MP3; }
            }
        }

        // ANSWER TO QUESTION 1: We see string returned by ToString() from base class Book, because there's no override in this SubClass
        // ANSWER TO QUESTION 2: Now I see value returned by this ToString() override function including info about BookType and Reader
        public override string ToString()
        {
            return $"{Title} (Audio Book for {Reader}) by {Author}, {Year}. {Genre}";
          // return Title + " (Audio Book) by " + Author + ", " + Year + ". " + Genre;
        }
        public override string BookType
        {
            get
            { 
                return "Audio Book";
            }
        }


    }

}
