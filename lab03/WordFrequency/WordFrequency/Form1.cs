using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Have to bring in system.IO to use the File class
using System.IO;


namespace WordFrequency
{
    public partial class Form1 : Form
    {

        // Create a dictionary that will store how many times each word
        // appears in a loaded file.   The word is the Key and is a string
        // and the count is the value and is an integer

        Dictionary<string, int> wordCountsf1 = new Dictionary<string, int>();
        Dictionary<string, int> wordCountsf2 = new Dictionary<string, int>(); // --- Dictionary for 2nd file EXTENSION 4

        // Create a dictionary that will store an association between a 
        // Textbox that will have a word provided by the user and the label
        // that will show the output value.   The Textbox is the Key and the
        // Label is the value.   This is mostly to make the code less tedious
        // and show another foreach loop

        Dictionary<TextBox, Label> index1 = new Dictionary<TextBox, Label>();
        Dictionary<TextBox, Label> index2 = new Dictionary<TextBox, Label>(); // --- textboxes/labels for 2nd file
        Dictionary<TextBox, Label> index3 = new Dictionary<TextBox, Label>(); // --- for word characters dictionary


        // Keep track of how many lines we've processed
        int lineCount;

        // Keep track of how many words we've processed
        // Note: this is overall words, not a count of the *different* words.
        int wordCount;


        public Form1()
        {
            InitializeComponent();
            // calling function to setup controls displayed, basically hides all controls related to file2 for compare feature
            comparison_setup(false);
            // We have a file drop-down list.
            // Add the names of the two files in the Input Directory.

            // Note:  it is *possible* to use a Directory object to get a list
            // of all the files in a directory of a particular type and then 
            // build the fileList that way.  Which would be *another* loop.
            // But I only downloaded two files.

            fileList.Items.Add("AnneOfGreenGables.txt");
            fileList.Items.Add("TheAdventuresOfSherlockHolmes.txt");

            // Put our textbox-label pairs in the index Dictionary so that we 
            // know which label to update when a textbox changes without writing 
            // a big switch statement or having to have ten different event handlers

            index1.Add(textBox1, label1);
            index1.Add(textBox2, label2);
            index1.Add(textBox3, label3);
            index1.Add(textBox4, label4);
            index1.Add(textBox5, label5);
            index1.Add(textBox6, label6);
            index1.Add(textBox7, label7);
            index1.Add(textBox8, label8);
            index1.Add(textBox9, label9);
            index1.Add(textBox10, label10);

            // -------------- DICTIONARY TO RELATE WORDS TEXTBOXES - LABELS FOR PRINTING COUNTERS FOR SECOND FILE IN COMPARISON FUNCTION
            index2.Add(textBox1, label11);
            index2.Add(textBox2, label12);
            index2.Add(textBox3, label13);
            index2.Add(textBox4, label14);
            index2.Add(textBox5, label15);
            index2.Add(textBox6, label16);
            index2.Add(textBox7, label17);
            index2.Add(textBox8, label18);
            index2.Add(textBox9, label19);
            index2.Add(textBox10, label20);
            // --------------- Dictionary to pair words textboxes and characters count labels  ----------------- //
            index3.Add(textBox1, label21);
            index3.Add(textBox2, label22);
            index3.Add(textBox3, label23);
            index3.Add(textBox4, label24);
            index3.Add(textBox5, label25);
            index3.Add(textBox6, label26);
            index3.Add(textBox7, label27);
            index3.Add(textBox8, label28);
            index3.Add(textBox9, label29);
            index3.Add(textBox10, label30);

            // Add the indexWordBox_TextChanged event handler to each textBox in the index list
            // index.Keys gets a list of all the keys in our dictionary (e.g. all the Textboxes)
            // the foreach loop does something for each Textbox;  inside the loop, the name of 
            // the current textbox we are looking at is 'indexWordBox'

            // Note:  the first time through the loop, indexWordBox = textbox1
            //        the second time through the loop, indexWordBox = textbox2
            //        etc. until we've looked at every textbox that we put in the dictionary

            foreach (TextBox indexWordBox in index1.Keys)
            {
                // Add the event handler to the current Textbox. // ------ works for 1 or two files because textbox is the same for both
                indexWordBox.TextChanged += new System.EventHandler(this.indexWordBox_TextChanged);
            }
        }

        // If we have a textbox with a word in it, we need to find the count 
        // for that word from the wordCounts Dictionary, then update the text 
        // of the label associated with that textbox with the count.

        private void updateAWord(TextBox indexWordBox, Dictionary<string,int> wordCounts, Dictionary<TextBox,Label> index)
        {
            // First, we are going to want to know the word to get the count for
            // That is just the Text of the Textbox we are processing.
            string indexWord = indexWordBox.Text;

            // We are going to need to update the label associated with this textbox.
            // Fortunately, we put the Textbox as a Key in the index Dictionary.
            // If we look up our textbox in the index Dictionary, we get back the associated label
            // e.g. Label1 for Textbox1, Label2 for Textbox2, etc.

            Label indexCountLabel = index[indexWordBox];
            // The wordCounts dictionary contains the counts of all the words that got
            // counted when the file was processed.   If no files have yet been processed,
            // then the dictionary is empty, and it contains no words.

            // -----------------------------             E X T E N S I O N       5            ----------------------------//
            // ----- Well, this was the last addition and to be honest I was tired and conscious of I would be delivering out of time so 
            // ----- I just used length method to get number of characters instead of using foreach to go through all the word counting characters, I'm sorry!
            index3[indexWordBox].Text = indexWord.Length.ToString();
            // First check if the word we are looking up is in the dictionary
            if (wordCounts.ContainsKey(indexWord))
            {
                // If it *is* in the dictionary, then get its Count (and turn it into a string)
                // And set the text of the label we want to change to the count.
                
                //   -------------------    C O R E   R E Q U I R E M E N T     1   -------------------------  //
                //----- Percentaje added, I just calclulate percentage, I have to cast one integer value in order to get a decimal result and then round it 
                indexCountLabel.Text = wordCounts[indexWord].ToString() + " --- " + decimal.Round((wordCounts[indexWord] / (decimal)wordCounts.Count()) * 100, 2) + "%";
            }
            else
            {
                // If it is *not* in the dictionary, then we know the count for this word
                // is zero.  Note that this will happen for *every* word in an empty dictionary
                indexCountLabel.Text = "0";
            }
        }

        // We want to update all the words
        private void updateAllWords(Dictionary<string,int> wordCounts, Dictionary<TextBox,Label> index)
        {
            // The index dictionary has all the textboxes we are interested in.
            // Each one is a key in the dictionary
            // index.Keys gives us a collection of keys
            // Foreach key / textbox, we want to updateAWord

            foreach (TextBox indexWordBox in index.Keys)
            {
                updateAWord(indexWordBox,wordCounts,index);

            }
        }

        // This is the LoadFile button.  Should have a better name.
        // When it gets clicked, we want to get the selected file, load the file, 
        // and process each line in the file

        private void button1_Click(object sender, EventArgs e)
        {
            load_file(1,wordCountsf1,index1,fileList.SelectedItem.ToString());  // ---- Now clicking on the button calls load_file function
        }

        // ----- this function loads the file contents into dictionary, takes 4 arguments:
        // ---- 1) file number: 1 if it's only 1 file or the first file to compare, 2 for the second file in comparison mode
        // ---- 2) dictionary to store words/counts
        // ---- 3) dictionary index (textboxes and labels) to show counters / percentages
        // ---- 4) name of file to read, selected from filelist control in the form
        private void load_file(int filenumber,Dictionary<string,int> wordCounts,Dictionary<TextBox,Label> index, string filename)
        {
            // We are counting lines and words for the file, so 
            // set them both to zero because this is a new file
            lineCount = 0;
            wordCount = 0;

            // Clear out the dictionary.   
            // wordCounts.Clear() would probably be better practice here.
            // Why create an entire new dictionary if we don't need to?
            // ---wordCountsf1 = new Dictionary<string, int>();
            wordCounts.Clear();

            // The name of the file is the SelectedItem of the fileList listBox
            // grab the SelectedItem

           // string filename = fileList.SelectedItem.ToString();

            // The file path is ../../Input/ because that is where I put the files
            // File.ReadLines will open the file and then create a Collection of lines
            // Note that it reads one line each time through the loop -- the collection
            // gets created on demand rather than reading the entire file ahead of time

            foreach (var line in File.ReadLines("../../Input/" + filename))
            {
                // Increment the line count by 1 as we are reading a line
                lineCount += 1;
                // We *could* update the # of lines processed so far here, but that
                // turns out to be hideously slow. 
                //linesProcessedLabel.Text = lineCount.ToString();
                // Process the line
                processLine(line,wordCounts);
            }
            //  Now that we've processed all lines of the file,
            // update the labels with the counts of lines and words processed.


            //    ----------------------        E X T E N S I O N    2       ----------------------  //
            // ------- next loop is to read all elements in wordCountsf1 directory and copy them to a txt file 
            StreamWriter outputfile = new StreamWriter("../../Output/" + filename);//filename.Remove(filename.Length-4) + "_Dictionary.txt");
            foreach (string n in wordCounts.Keys)
            {
                outputfile.WriteLine(n + "," + wordCounts[n]);
            }
            outputfile.Close(); // ----- now I know how relevant is to properly close the file


            // ---------------------------     E X T E N S I O N      3     --------------------------///
            // -------- next helps to get the most common word by ordering elements in dicionary descendently and then picking the first word
            var max = wordCounts.OrderByDescending(x => x.Value).First().Key;
            //label12.Text = max;
            if (filenumber==1)
            {
                mcw1.Text = max;
                lines1.Text = lineCount.ToString();
                words1.Text = wordCount.ToString();
                file1label.Text = filename;
            }
            else
            {
                mcw2.Text = max;
                lines2.Text = lineCount.ToString();
                words2.Text = wordCount.ToString();
                file2label.Text = filename;
            }

            // Update all the words in the word list with the counts from the file
            updateAllWords(wordCounts,index);
        }


        // Process a Line of text from the file
        private void processLine(string myLine, Dictionary<string,int> wordCounts)
        {
            string corrected_word; // ---- temporary string to refine words splitted
            // We take the line and split it into a collection of words
            // We use String.Split and split on the space character

            //  ---------------------  C O R E     2    A N D     E X T E N S I O N    1    starts here  ----------------------   //
            // ------- In order to get more accurate results, I'm considering an additional
            // ------- string existing in the text  -- , used to indicate dialogs as word splitter
            string[] delim = { " ", "--" };
            // We want to process each 'word' in the collection of words in the line
            // -- this overload of split function makes possible to add another separator/splitter
            foreach (string word in myLine.Split(delim, StringSplitOptions.RemoveEmptyEntries)) 
            {
                // ---- Turns out that methods like .Replace or .TrimEnd don't affect the actual string so
                // ---- we must assign the modified value to another string
                corrected_word = "";
                /*      ---- I tried this as suggested, but removes - from composed words such as re-use, well-conducted
                 *      ---- and I found Trim method easy to use considering only affects first and last characters in the string evaluated
                 * foreach(char c in word)
                    {
                        if(char.IsLetter(c))
                        { corrected_word = corrected_word + c; }
                    }
                */

                if (!word.EndsWith("s’"))
                {
                    //corrected_word = corrected_word.Replace(",", ".").Replace(".’","").Replace(".", "");
                    corrected_word = word.Trim(',', '.', '!', '?', '(', ')', '"', '/', '“', '”', '[', ']', ':', ';', '_', '‘', '’', '-');  //--- removes all unwanted symbols in a row
                }
                else
                {
                    corrected_word = word.Trim(',', '.', '!', '?', '(', ')', '"', '/', '“', '”', '[', ']', ':', ';', '_', '‘', '-');
                }
                // ---- We don't want empty words to be counted 
                // ---- or added to our dictionary, I don't know why yet, but there are some of them
                if (corrected_word.Length > 0)
                {
                    // Add one to the number of words seen
                    wordCount += 1;
                    // Commented out for efficiency.  Updating the label 100,000+ times is *slow*
                    //wordsProcessedLabel.Text = wordCount.ToString();

                    // Is the word in the dictionary yet?
                    if (wordCounts.ContainsKey(corrected_word))
                    {
                        // If so, get its current value, add one to it, and set that as the new value
                        wordCounts[corrected_word] = wordCounts[corrected_word] + 1;
                    }
                    else
                    {
                        // If it is not in the dictionary yet, add it with a count of 1
                        // (because this is the first time we've seen it)
                        wordCounts.Add(corrected_word, 1);
                    }
                }
                else { } // counts empty words
            }
        }

        // If any textbox gets changed, we want to update the label
        // that shows the wordCount associated with the word in the 
        // textbox for the currently processed file.

        private void indexWordBox_TextChanged(object sender, EventArgs e)
        {
            // We know that the sender is a textbox.
            TextBox indexWordBox = (TextBox)sender;

            // Call the updateAWord function to update the count for the 
            // word now in the textbox
            updateAWord(indexWordBox, wordCountsf1, index1);

            if (button2.Text == "BACK")
            {
                updateAWord(indexWordBox,wordCountsf2,index2);
            }
           
        }

        private void clean_form()
        {
            // put all counter labels to 0
            for (int i = 1; i <= 30; i++)
            {
                var controles = this.Controls.OfType<Label>().Where(x => x.Name.Equals("label" + i));
                foreach (var ctrl in controles)
                {
                    ctrl.Text = "0";
                }
            }
            file1label.Text = "";
            lines1.Text = "0";
            words1.Text = "0";
            mcw1.Text = "-";

        }

        // toggles form layout according the function required ( 2 files word count comparison or single file word counting)
        // I use button.text value to know which function is currently selected
        // it also performs 2 files loading when it's needed
        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text=="BACK")
            { comparison_setup(false); }
            else 
            { 
                comparison_setup(true);
                load_file(1,wordCountsf1, index1, fileList.Items[0].ToString());
                load_file(2,wordCountsf2, index2, fileList.Items[1].ToString());
            }
        }

        // this function basically hides or make visible controls to display second file data
        // it also enables/disables load button for single file word count function
        private void comparison_setup(Boolean flag)
        {
            for (int i = 11; i <= 20; i++)
            {
                var controles = this.Controls.OfType<Label>().Where(x => x.Name.Equals("label" + i));
                
                foreach (var ctrl in controles)
                {
                    ctrl.Visible = flag;
                }

            }
            file2label.Visible = flag;
            lines2.Visible = flag;
            words2.Visible = flag;
            mcw2.Visible = flag;
            fileList.Enabled = !flag;

            button1.Enabled = !flag;
            if (flag)
            { 
                button2.Text = "BACK";
            }
            else
            {
                button2.Text = "COMPARE";
                clean_form();
            }
            // change text to labels to the top of counters, just to know from which file they show counts


        }


    }
}
