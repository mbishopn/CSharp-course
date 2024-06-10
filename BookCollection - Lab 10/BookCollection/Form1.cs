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
        List<Coin> coins = new List<Coin>();

        public Form1()
        {
            InitializeComponent();

        }

        private void loadBooksButton_Click(object sender, EventArgs e)
        {
            // Add all the books to the List of books when the button is clicked.

            books.Clear();
            books.Add(new Book("Handmaid's Tale", "Margaret Atwood", 1985, BookGenre.ScienceFiction, new  MediaFormat(FormatCategory.Printed,FormatType.Hardcover), 45.50));
            books.Add(new Book("Alias Grace", "Margaret Atwood", 1996, BookGenre.Fiction, new MediaFormat(FormatCategory.Printed, FormatType.Hardcover), "Scott C. Stoness"));
            books.Add(new SeriesBook("Fellowship of the Ring", "J.R.R. Tolkien", "Lord of the Rings", 1, 1954, BookGenre.Fantasy, new MediaFormat(FormatCategory.Printed, FormatType.Hardcover)));
            books.Add(new SeriesBook("The Two Towers", "J.R.R. Tolkien", "Lord of the Rings", 2, 1954, BookGenre.Fantasy,new  MediaFormat(FormatCategory.Printed,FormatType.Hardcover)));
            books.Add(new SeriesBook("The Return of the King", "J.R.R. Tolkien", "Lord of the Rings", 3, 1954, BookGenre.Fantasy, new MediaFormat(FormatCategory.Printed, FormatType.Hardcover)));
            books.Add(new GraphicNovel("Preludes & Nocturnes", "Neil Gaiman", "Sam Keith", "The Sandman", 1, 1988, BookGenre.GraphicNovel, new MediaFormat(FormatCategory.Printed, FormatType.Hardcover)));
            books.Add(new GraphicNovel("The Doll's House", "Neil Gaiman", "Mike Dringenberg", "The Sandman", 2, 1990, BookGenre.GraphicNovel, new MediaFormat(FormatCategory.Printed, FormatType.Hardcover)));
            books.Add(new GraphicNovel("Dream Country", "Neil Gaiman", "Kelly Jones", "The Sandman", 3, 1990, BookGenre.GraphicNovel, new MediaFormat(FormatCategory.Printed, FormatType.Hardcover)));
            books.Add(new GraphicNovel("Season of Mists", "Neil Gaiman", "Matt Wagner", "The Sandman", 4, 1990, BookGenre.GraphicNovel, new MediaFormat(FormatCategory.Printed, FormatType.Hardcover)));
            books.Add(new Biography("A Beautiful Mind", "Sylvia Nasar", "John Nash", 1998, BookGenre.Biography, new MediaFormat(FormatCategory.Printed, FormatType.Hardcover), "Bob Dylan"));

            coins.Clear();
            Coin c = new Coin("USA", "Penny", 1935);
            c.recordAppraisal(2.50);
            coins.Add(c);
            c = new Coin("USA", "Dime", 1910);
            c.recordAppraisal(10.75);
            coins.Add(c);
            coins.Add(new Coin("Canada", "Toonie", 2007));

            // Hint A2:  Once you've created an AudioBook class, it is pretty boring if you don't add an AudioBook to the list.

            // Add all the books in the list to the listbox.
            // Note that we can treat each as a book whether it is a Book, a SeriesBook, etc.

            foreach (Book b in books)
            {
                listBox1.Items.Add(b);
            }

            foreach (Coin c1 in coins)
            {
                listBox2.Items.Add(c1);
            }

            double insuredValue = 0;

            foreach (IInsurable i in books)
            {
                insuredValue += i.InsuredValue;
            }

            foreach (IInsurable i in coins)
            {
                insuredValue += i.InsuredValue;
            }

            label2.Text = "" + insuredValue;

        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // When we click on a book, find the selected item in the listbox
            Book b = (Book) listBox1.SelectedItem;

            // Now set the property grid to show the properties of that object
            propertyGrid1.SelectedObject = b;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Coin c = (Coin)listBox2.SelectedItem;

            propertyGrid1.SelectedObject = c;
        }
    }



    interface IInsurable
    {

        double InsuredValue { get; }
       
        bool IsAppraised { get; }

        void recordAppraisal(double v);      
        
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

    class Coin : IInsurable
    {
        [CategoryAttribute("Origin"), DescriptionAttribute("Country")]
        public string Country { get; private set; }
        [CategoryAttribute("Nominal Value"), DescriptionAttribute("Face Value")]
        public string Denomination { get; private set; }
        [CategoryAttribute("Minted"), DescriptionAttribute("Coining Year")]
        public Int32 Year { get; private set; }

        [CategoryAttribute("Insurance"), DescriptionAttribute("Appraised Value")]
        public double InsuredValue { get; private set; }

        [CategoryAttribute("Insurance"), DescriptionAttribute("Whether it has been Appraised")]
        public bool IsAppraised { get; private set; }

 

        public Coin(string c, string d, int y)
        {
            Country = c;
            Denomination = d;
            Year = y;
        }

        public void recordAppraisal (double v)
        {
            InsuredValue = v;
            IsAppraised = true;
        }
        // ------------- C O R E  T A S K  1 -------- Overriding ToString()
        public override string ToString()
        {
            return $"({Year}) {Denomination} from {Country}";
        }
    }

    enum FormatType { Hardcover,Softcover,Paperback,MOBI,EPUB,PDF,CBZ,MP3,WAV,AAC,Vinyl,CD,Cassette,Tape_8Track,_45}
    enum FormatCategory { Printed, Ebook, Audio, PhysicalRecording}

    interface Mformat
    {
        FormatType getformat();
        FormatCategory category();
    }

    class MediaFormat 
    {
        private FormatType _format;
        public FormatType format
        {
            get => _format;
            set
            {
                _format = value;
            }
        }
        public MediaFormat(FormatCategory c, FormatType f) // constructor
        {
            format = f;
            category = c;
        }
        public FormatCategory category
        {
            get;set;
        }
        public override string ToString()
        {
            return $"{this.category} - {this.format}";
        }
    }


    // Simplifying assumption:  Books have one author

    class Book : IInsurable
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

        public string Reader { get; private set; }

        [CategoryAttribute("Insurance"), DescriptionAttribute("Appraised Value")]
        public double InsuredValue { get; private set; }

        [CategoryAttribute("Insurance"), DescriptionAttribute("Whether it has been Appraised")]
        public bool IsAppraised { get; private set; }

        public bool IsAudioBook
        {
            get
            {
                return Reader != null;
            }
        }
        private MediaFormat _bformat;
        public  MediaFormat bformat
        {
            get;set;
            //get=>_bformat;
            //set
            //{
            //    //List<FormatType> audioformat = new List<FormatType> { FormatType.AAC, FormatType.Cassette, FormatType.CD, FormatType.MP3, FormatType.Tape_8Track, FormatType.Vinyl, FormatType.WAV, FormatType._45 };
            //    List<FormatType> audioformat = new List<FormatType> { FormatType.AAC, FormatType.Cassette, FormatType.CD, FormatType.MP3, FormatType.Tape_8Track, FormatType.Vinyl, FormatType.WAV, FormatType._45 };
            //    if (!IsAudioBook)
            //        if(!audioformat.Contains(bformat.format) && !(bformat.category == FormatCategory.Audio || bformat.category == FormatCategory.PhysicalRecording))
            //        {
            //        _bformat.format = bformat.format;
            //        _bformat.category = bformat.category;
            //        }
                                                   
            //}
        }
    

    // Constructor to fill in the four properies 
    // Hint B3:  modify the constructor to take a BookFormat as well
    public Book (string t, string a, int y, BookGenre g, MediaFormat f)
        {
            Title = t;
            Author = a;
            Year = y;
            Genre = g;
            Reader = null;
            bformat = f;

        }

        public Book(string t, string a, int y, BookGenre g, MediaFormat f , string r) : this(t, a, y, g, f)
        {
            Reader = r;
        }

        public Book(string t, string a, int y, BookGenre g, MediaFormat f, double v) : this(t, a, y, g, f)
        {
            recordAppraisal(v);
        }

        public void recordAppraisal(double v)
        {
            InsuredValue = v;
            IsAppraised = true;
        }

        // Override the Object.ToString function to better represent a book
        public override string ToString()
        {
            return $"{Title} ({Author}" + (IsAudioBook ? $", {Reader}(reader)" : "") + $"), {Year}.  {Genre}";
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

        

        // Constructor takes all 6 bits of information required. 
        // Passes the title, author, year, and genre to the base constructor of Book
        public SeriesBook(string t, string a, string s, int n, int y, BookGenre g, MediaFormat f) : base(t, a, y, g, f)
        {
            Series = s;
            SeriesNumber = n;
        }

        public SeriesBook(string t, string a, string s, int n, int y, BookGenre g, MediaFormat f, string r) : base(t, a, y, g, f, r)
        {
            Series = s;
            SeriesNumber = n;
        }

        // Override the Book.ToString() function
        public override string ToString()
        {
            return $"{Title} ({Author}" + (IsAudioBook ? $", {Reader}(reader)" : "") + $"), { Series} #{SeriesNumber},  {Year}.  {Genre}";
        }
    }

    // Simplifying assumption:  Comic Books are graphic novels.  All Graphic novels come in series (definitely false, but often /generally true)
    // Simplifying assumption:  There is one Artist.  We are ignoring letterers and colourers

    // A GraphicNovel extends a SeriesBook and adds an Artist Property
    class GraphicNovel : SeriesBook
    {
        [CategoryAttribute("Who / When"), DescriptionAttribute("Artist of this Graphic Novel")]
        public string Artist { get; private set; }

       
        // Here the constructor takes 7 properties, and calls the base constructor (SeriesBook) with 6 of them.
        // Note that the Seriesbook base constructor will set two of those properties, and then call *its* base constructor (Book) with the other 4

        // There are no Audio Graphic Novels, so no constructor for an AudioBook

        public GraphicNovel (string t, string a, string artist, string s, int n, int y, BookGenre g, MediaFormat f) : base(t, a, s, n, y, g, f)
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


        public Biography(string t, string a, string s, int y, BookGenre g, MediaFormat f) : base(t, a, y, g, f)
        {
            Subject = s;
        }

        public Biography(string t, string a, string s, int y, BookGenre g, MediaFormat f, string r) : base(t, a, y, g, f, r)
        {
            Subject = s;
        }

        public override string ToString()
        {
            return $"{Title} (Biography of {Subject} by {Author}" + (IsAudioBook ? $", {Reader}(reader)" : "") + $"), {Year}.  {Genre}";
        }
    }

    // Create an AudioBook class here according to the specifications of the assignment.
    // Hint A1:  it is going to look *very* much like the Book class

    class Album : IInsurable
    {
        [CategoryAttribute("Music Genre"), DescriptionAttribute("Music Genre")]
        public string Genre { get; private set; }

        [CategoryAttribute("Insurance"), DescriptionAttribute("Appraised Value")]
        public double InsuredValue { get; private set; }

        [CategoryAttribute("Insurance"), DescriptionAttribute("Whether it has been Appraised")]
        public bool IsAppraised { get; private set; }

        public MediaFormat Format
        { get; set; }

        // poner el constructor default

        // constructor para insurable

        public void recordAppraisal(double v)
        {
            InsuredValue = v;
            IsAppraised = true;
        }

    }

    enum MusicGenre
    {
        Classic,
        Pop,
        Rock,
        Folk,
        Country,
        Jazz
    }
}
