using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.ConstrainedExecution;
using static Milestone_2._1.inventoryForm;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

/*
 * Lindsey DeDecker
 * CST - 150
 * Milestone 2
 * July 13, 2024
 * Gaddis, T. (2019). Starting out with Visual C# (5th ed.). Pearson. ISBN-13: 9780135183519
 * Amazon. 2024, July 13. Top 10 Amazon Best Selling Books. Amazon. https://www.amazon.com/s?k=top+10+amazon+best+selling+books&gad_source=1&hvadid=677950796869&hvdev=c&hvlocphy=9018570&hvnetw=g&hvqmt=e&hvrand=9619227536578927051&hvtargid=kwd-2230422507596&hydadcr=7635_13589696&tag=googhydr-20&ref=pd_sl_5gz9bxjlcl_e
 * Microsoft. 2024, July 14. DataGridView Class. Microsoft. https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.datagridview?view=windowsdesktop-8.0
 */

namespace Milestone_2._1
{
    public partial class inventoryForm : Form
    {
        //An array to hold all of the books
        List<Book> books = new List<Book>();

        //saving value of the row click
        int selectedRow = -1;



        public inventoryForm()
        {
            InitializeComponent();
            //Make the label for inventory not visible
            lblInventory.Visible = false;
            lblTitle.Visible = false;
            dataInfo.Visible = false;

        }

        private void inventoryForm_Load(object sender, EventArgs e)
        {

        }



        private void lblInventory_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }
        public class Book
        {
            //Create attributes of the class book
            private string title = "";
            private string author = "";
            private double quantity;
            private double price;
            private double isbn;
            private string genre = "";

            //getters and setters for the varaibles I want for the books
            public string Title
            {
                get { return title; }
                set { title = value; }

            }
            public string Author
            {
                get { return author; }
                set { author = value; }

            }
            public double Quantity
            {
                get { return quantity; }
                set { quantity = value; }

            }
            public double Price
            {
                get { return price; }
                set { price = value; }

            }

            public double Isbn
            {
                get { return isbn; }
                set { isbn = value; }

            }
            public string Genre
            {
                get { return genre; }
                set { genre = value; }

            }
        }

        private void lblInventory_Click(object sender, EventArgs e)
        {


            //Pulls text from the inventory file
            using StreamReader sr = new StreamReader("C:\\Users\\kydec\\Desktop\\Lindsey School\\CSR 150\\Activity 2\\Milestone 2.1\\bin\\Debug\\net8.0-windows\\Inventory.txt");
            {
                //loop to run while there is text to read from the file
                while (!sr.EndOfStream)
                {
                    //putting each line into a string
                    string line = sr.ReadLine();
                    //breaking appart the string by |
                    string[] info = line.Split(new char[] { '|' });


                    //creating a new book from each part of the string
                    Book b = new Book();

                    //Creating variables for all items in the stirng.  
                    //converting some to double and int
                    b.Title = info[0];
                    b.Author = info[1];
                    b.Price = double.Parse(info[2]);
                    b.Quantity = double.Parse(info[3]);
                    b.Isbn = double.Parse(info[4]);
                    b.Genre = info[5];

                    //Add the book to the book array
                    books.Add(b);


                    //The label for the inventory will be displayed
                    dataInfo.Visible = true;
                }

                //loop to display all books into the grid
                foreach (Book b in books)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataInfo);

                    row.Cells[0].Value = b.Title;
                    row.Cells[1].Value = b.Author;
                    row.Cells[2].Value = b.Price;
                    row.Cells[3].Value = b.Quantity;
                    row.Cells[4].Value = b.Isbn;
                    row.Cells[5].Value = b.Genre;

                    dataInfo.Rows.Add(row);
                }
            }
        }
        private void dataInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {



        }

        private void addInventory_Click(object sender, EventArgs e)
        {

            string text = "Click on the quanitity of the book you would like to add one to.";
            MessageBox.Show(text);

        }

        private void dataInfo_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataInfo_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataInfo_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {


            //This is used to test and track that the cell that is being clicked is showing correctly in the code. 
            System.Text.StringBuilder messageBoxCS = new System.Text.StringBuilder();
            messageBoxCS.AppendFormat("{0} = {1}", "ColumnIndex", e.ColumnIndex);
            messageBoxCS.AppendLine();
            messageBoxCS.AppendFormat("{0} = {1}", "RowIndex", e.RowIndex);
            messageBoxCS.AppendLine();

            //This stops and prompts the user if they do not click on the cell that has the inventory in it.
            if (e.ColumnIndex != 3)
            {
                string message = "Please click on the quanitity of the book you would like to add 1 to.";
                MessageBox.Show(message);
            }

            //setting the row clicked as selected row
            selectedRow = e.RowIndex;

            //Checking that the selected row is greater than 0
            if (selectedRow < 0)
            {
                return;
            }


            Book selectedBook = books[selectedRow];

            //adding 1 to quantity
            selectedBook.Quantity += 1;

            //putting that new value into the grid
            dataInfo.Rows[selectedRow].Cells[3].Value = selectedBook.Quantity;
        }

        //This button will replace the file with the updated books. 
        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            using StreamWriter sw = new StreamWriter("C:\\Users\\kydec\\Desktop\\Lindsey School\\CSR 150\\Activity 2\\Milestone 2.1\\bin\\Debug\\net8.0-windows\\Inventory.txt");
            {
                foreach (Book b in books)
                {
                    string line = $"{b.Title}|{b.Author}|{b.Price}|{b.Quantity}|{b.Isbn}|{b.Genre}";
                    sw.WriteLine(line);
                }
            }

            string confirmation = "Your changes have been saved!";
            MessageBox.Show(confirmation);


        }
    }
}