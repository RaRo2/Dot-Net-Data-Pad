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

namespace TextEditor
{
    public partial class TextForm : Form
    {
        // Create a new AccountList so that the Form can decide to open in edit or view mode
        AccountList accList = new AccountList();
        // String variable to hold the current file path 
        string currentFileName;

        // Constructor takes a string input containing the username of the user
        public TextForm(string user)
        {
            // Initialises the form
            InitializeComponent();

            // Loads all the accounts within the AccountList and gets the user with a matching username
            accList.LoadAccounts();
            Account thisAccount = accList.getAccount(user);

            // Sets the text within the toolStrip with the username
            usernameToolStripButton.Text = "Username: " + thisAccount.User;

            // Changes mode depending on the accountType                     
            checkAccountType(thisAccount);
        }
        /************************************************************************************************************/


        // checkAccountType() takes an Account input and enables all the write functions if it is an Edit type
        // By default, the buttons are defaulted to read-only
        private void checkAccountType(Account a)
        {
            if (a.Type == "Edit")
            {
                richTextBox.ReadOnly = false;
                newFileToolStripMenuItem.Enabled = true;
                saveFileToolStripMenuItem.Enabled = true;
                saveAsFileToolStripMenuItem.Enabled = true;

                newToolStripButton.Enabled = true;
                saveToolStripButton.Enabled = true;
                saveAsToolStripButton.Enabled = true;
                boldToolStripButton.Enabled = true;
                italicToolStripButton.Enabled = true;
                underlineToolStripButton.Enabled = true;
                fontSizeToolStripComboBox1.Enabled = true;

                cutToolStripButton.Enabled = true;
                copyToolStripButton.Enabled = true;
                pasteToolStripButton.Enabled = true;

                editToolStripMenuItem.Enabled = true;

            }
        }
        /************************************************************************************************************/


        // checkSave() displays a popup Message Box asking the user if they want to save first
        // it only displays this if the text editor Rich Text Box is not empty
        private void checkSave()
        {
            if (richTextBox.Text != "")
            {
                // Set msg as the string to display
                string msg = "Do you want to save changes to this file?";

                // Create new message box and save the file if user selects yes
                DialogResult result = MessageBox.Show(msg, "DNDP", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    saveFile();
                }
            }
        }
        /************************************************************************************************************/


        // newFile() clears the text editor Rich Text Box and sets the current file to null
        private void newFile() 
        {
            // Check if file is empty first
            checkSave();
            richTextBox.Text = "";
            currentFileName = null;
        }
        /************************************************************************************************************/


        // openFile() creates a new Open File Dialog Window and displays the contents of a selected file in the Rich Text Box
        private void openFile() 
        {
            // Check if file is empty first
            checkSave();

            // Create a new Open File Dialog window
            OpenFileDialog open = new OpenFileDialog();

            // If the user clicks OK
            if (open.ShowDialog() == DialogResult.OK)
            {
                // Check the file type,
                // Open txt files as plain text
                if (Path.GetExtension(open.FileName) == ".txt")
                {
                    richTextBox.LoadFile(open.FileName, RichTextBoxStreamType.PlainText);
                }

                // Open rtf files as formatted rtf text
                else if (Path.GetExtension(open.FileName) == ".rtf")
                {
                    richTextBox.LoadFile(open.FileName);
                    currentFileName = open.FileName;
                }

                // Display an error message if the user chooses a different file type
                else
                {
                    string msg = $"Unable to open \n\n{Path.GetFileNameWithoutExtension(open.FileName)}.";
                    msg += "\n\nMore information in Help > About";
                    MessageBox.Show(msg, "DNDP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                currentFileName = open.FileName;
            }
        }
        /************************************************************************************************************/


        // saveFile() saves the file depending on which file type it belongs
        private void saveFile() 
        {
            // If file already exists and is an rtf file, save it as an rtf file
            if (File.Exists(currentFileName) && Path.GetExtension(currentFileName) == ".rtf")
            {
                richTextBox.SaveFile(currentFileName);
            }
            // If file already exists but is a txt file, save it as a txt file using plain text
            else if (File.Exists(currentFileName) && Path.GetExtension(currentFileName) == ".txt")
            {
                richTextBox.SaveFile(currentFileName, RichTextBoxStreamType.PlainText);
            }
            // If it is anything else, saveAs()
            else
            {
                saveAsFile();
            }
        }
        /************************************************************************************************************/


        // saveAsFile() opens a Save File Dialog window and saves the file depending on the file name and type given
        private void saveAsFile() 
        {
            // Check if the text editor Rich Text Box is empty
            if (richTextBox.Text != "")
            {
                // Create a new Save File Dialog window with rtf and txt files as the only filters
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Rich Text Format (*.rtf)|*.rtf*|Text File (*.txt)|*.txt";
                save.FileName = "Untitled";

                // If the user clicks OK
                if (save.ShowDialog() == DialogResult.OK)
                {
                    // Set the new file name as whatever the user inputs
                    string newFileName = save.FileName;

                    // If the user selects the rtf filter, save the file as an rtf file
                    if (save.FilterIndex == 1)
                    {
                        currentFileName = save.FileName;
                        richTextBox.SaveFile(currentFileName + ".rtf");
                    }

                    // If the user selects the txt filter, save the file as a txt file
                    else if (save.FilterIndex == 2)
                    {
                        currentFileName = save.FileName;
                        richTextBox.SaveFile(currentFileName, RichTextBoxStreamType.PlainText);
                    }
                }
            }
        }
        /************************************************************************************************************/


        // about() displays a small Message Box with information regarding the program
        private void about() 
        {
            string msg = "DNDP is a simple text editor which allows users to view \n" +
                         "and edit text based files. DNDP supports viewing .txt \n" +
                         "and .rtf files and also editing/saving .rtf files.\n";
            MessageBox.Show(msg, "DNDP", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /************************************************************************************************************/


        // Toggles bold text on highlighted text when bold button is clicked
        private void boldToolStripButton_Click(object sender, EventArgs e)
        {
            // Set the starting position and the length of the highlighted text
            int start = richTextBox.SelectionStart;
            int length = richTextBox.SelectionLength;

            // Create a new Font
            Font newFont;

            // If there is selected text
            if (length != 0)
            {
                // Loop through each character within the selected text
                for (int i = 0; i < length; i++)
                {
                    // Select the character at 'i' and save its font in currentFont
                    richTextBox.Select(start + i, 1);
                    Font currentFont = richTextBox.SelectionFont;

                    // If the font is already bolded, remove bold from the new FontStyle
                    if (currentFont.Bold)
                    {
                        newFont = new Font(currentFont, currentFont.Style & ~FontStyle.Bold);
                    }
                    // If not, add bold to new FontStyle
                    else
                    {
                        newFont = new Font(currentFont, currentFont.Style | FontStyle.Bold);
                    }
                    // Set the selected character's font with the new FontStyle
                    richTextBox.SelectionFont = newFont;
                }
                // Reselect the text
                richTextBox.Select(start, length);
            }

            // If there isn't selected text, toggle bold on where the user's cursor is 
            else
            {
                Font currentFont = richTextBox.SelectionFont;
                richTextBox.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, currentFont.Style | FontStyle.Bold);
            }
        }
        /************************************************************************************************************/


        // Toggles italic text on highlighted text when italic button is clicked
        private void italicToolStripButton_Click(object sender, EventArgs e)
        {
            // Set the starting position and the length of the highlighted text
            int start = richTextBox.SelectionStart;
            int length = richTextBox.SelectionLength;

            // Create a new Font
            Font newFont;

            // If there is selected text
            if (length != 0)
            {
                // Loop through each character within the selected text
                for (int i = 0; i < length; i++)
                {
                    // Select the character at 'i' and save its font in currentFont
                    richTextBox.Select(start + i, 1);
                    Font currentFont = richTextBox.SelectionFont;

                    // If the font is already italic, remove italic from the new FontStyle
                    if (currentFont.Italic)
                    {
                        newFont = new Font(currentFont, currentFont.Style & ~FontStyle.Italic);
                    }
                    // If not, add italic to new FontStyle
                    else
                    {
                        newFont = new Font(currentFont, currentFont.Style | FontStyle.Italic);
                    }
                    // Set the selected character's font with the new FontStyle
                    richTextBox.SelectionFont = newFont;
                }
                // Reselect the text
                richTextBox.Select(start, length);
            }

            // If there isn't selected text, toggle italic on where the user's cursor is 
            else
            {
                Font currentFont = richTextBox.SelectionFont;
                richTextBox.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, currentFont.Style | FontStyle.Italic);
            }
        }
        /************************************************************************************************************/


        // Toggles underline text on highlighted text when underline button is clicked
        private void underlineToolStripButton_Click(object sender, EventArgs e)
        {
            // Set the starting position and the length of the highlighted text
            int start = richTextBox.SelectionStart;
            int length = richTextBox.SelectionLength;

            // Create a new Font
            Font newFont;

            // If there is selected text
            if (length != 0)
            {
                for (int i = 0; i < length; i++)
                {
                    // Select the character at 'i' and save its font in currentFont
                    richTextBox.Select(start + i, 1);
                    Font currentFont = richTextBox.SelectionFont;

                    // If the font is already underlined, remove underline from the new FontStyle
                    if (currentFont.Underline)
                    {
                        newFont = new Font(currentFont, currentFont.Style & ~FontStyle.Underline);
                    }
                    // If not, add italic to new FontStyle
                    else
                    {
                        newFont = new Font(currentFont, currentFont.Style | FontStyle.Underline);
                    }
                    // Set the selected character's font with the new FontStyle
                    richTextBox.SelectionFont = newFont;
                }
                // Reselect the text
                richTextBox.Select(start, length);
            }

            // If there isn't selected text, toggle underline on where the user's cursor is 
            else
            {
                Font currentFont = richTextBox.SelectionFont;
                richTextBox.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, currentFont.Style | FontStyle.Underline);
            }
        }
        /************************************************************************************************************/


        // Changes text size on highlighted text when font size dropbown box is closed
        private void fontSizeToolStripComboBox1_DropDownClosed(object sender, EventArgs e)
        {
            // Set the starting position and the length of the highlighted text
            int start = richTextBox.SelectionStart;
            int length = richTextBox.SelectionLength;

            // If there is selected text
            if (length != 0)
            {

                for (int i = 0; i < length; i++)
                {
                    // Select the character at 'i' and save its font in currentFont
                    richTextBox.Select(start + i, 1);
                    Font currentFont = richTextBox.SelectionFont;

                    // Set the new font size depending on which option is selected
                    richTextBox.SelectionFont = new Font(currentFont.FontFamily, int.Parse(fontSizeToolStripComboBox1.Text), currentFont.Style);
                }
                // Reselect the text
                richTextBox.Select(start, length);
            }
            // If there isn't selected text, set the font size to where the user's cursor is 
            else
            {
                Font currentFont = richTextBox.SelectionFont;
                richTextBox.SelectionFont = new Font(currentFont.FontFamily, int.Parse(fontSizeToolStripComboBox1.Text), currentFont.Style);
            }
        }
        /************************************************************************************************************/


        // Returns to the login form when user clicks the logout button
        private void logoutFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm logForm = new LoginForm();
            logForm.Show();
            this.Hide();
        }

        // Cut the selected text when the cut button is clicked
        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            richTextBox.Cut();
        }

        // Copy the selected text when the copy button is clicked
        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            richTextBox.Copy();
        }

        // Paste the selected text when the paste button is clicked
        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            richTextBox.Paste();
        }

        // Cut the selected text when the cut menu item is clicked
        private void cutEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Cut();
        }

        // Copy the selected text when the copy menu item is clicked
        private void copyEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Copy();
        }

        // Paste the selected text when the paste menu item is clicked
        private void pasteEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Paste();
        }

        // Create a new file when the new button is clicked
        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            newFile();
        }

        // Open a file when the open button is clicked
        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            openFile();
        }

        // Save the file when the save button is clicked
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        // Save as the file when the save as button is clicked
        private void saveAsToolStripButton_Click(object sender, EventArgs e)
        {
            saveAsFile();
        }

        // Create a new file when the new menu item is clicked
        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newFile();
        }

        // Open a file when the open menu item is clicked
        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFile();
        }

        // Save a file when the save menu item is clicked
        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        // Save as the file when the save as menu item is clicked
        private void saveAsFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveAsFile();
        }

        // Open the help window when the help button is clicked
        private void aboutToolStripButton_Click(object sender, EventArgs e)
        {
            about();
        }

        // Open the help window when the help menu item is clicked
        private void aboutHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about();
        }

    }
}
