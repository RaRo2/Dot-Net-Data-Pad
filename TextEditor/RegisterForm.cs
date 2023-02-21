using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TextEditor
{
    public partial class RegisterForm : Form
    {
        // Create a new AccountList so that this form can check the account database
        AccountList accList = new AccountList();

        // Constructor initialises the form and loads in the accounts from the account list
        public RegisterForm()
        {
            InitializeComponent();
            accList.LoadAccounts();
        }
        /************************************************************************************************************/


        // When the submit button is clicked, attempt to add an account
        private void submitButton_Click(object sender, EventArgs e)
        {
            // Create a string to hold the account information
            string info;

            // If the username already exists within the account list, display the respective error message
            if (accList.userExist(userTextBox.Text))
            {
                errorLabel.Text = "Username is already taken.";
                errorLabel.Visible = true;
            }

            // If the password is less than 6 characters, display the respective error message
            else if (passTextBox.Text.Length < 6)
            {
                errorLabel.Text = "Password must be at least 6 characters.";
                errorLabel.Visible = true;
            }

            // If both the passwords do not match, display the respective error message
            else if (confirmPassTextBox.Text != passTextBox.Text)
            {
                errorLabel.Text = "Both passwords must match.";
                errorLabel.Visible = true;
            }

            // If there are no more errors
            else
            {
                // Enter all the text box information into the info string
                info = userTextBox.Text + "," + passTextBox.Text + "," + userTypeComboBox.Text
                     + "," + fNameTextBox.Text + "," + lastNameTextBox.Text + "," + dobDateTimePicker.Text;

                // Add the account to the account list
                accList.AddAccount(info);

                // Open the login form
                LoginForm logForm = new LoginForm();
                logForm.Show();
                
                // Close this form
                this.Hide();
            }
        }

        // If the cancel button is clicked
        private void cancelButton_Click(object sender, EventArgs e)
        {
            // Open the login form
            LoginForm logForm = new LoginForm();
            logForm.Show();

            // Close this form
            this.Hide();
        }
        /************************************************************************************************************/


        // checkSubmit() enables the submit button when all the text boxes are filled
        private void checkSubmit()
        {
            if (userTextBox.Text != "" && passTextBox.Text != "" && confirmPassTextBox.Text != ""
                && fNameTextBox.Text != "" && lastNameTextBox.Text != "" && userTypeComboBox.SelectedIndex > -1)
            {
                submitButton.Enabled = true;
            }
            else
            {
                submitButton.Enabled = false;
            }
        }
        /************************************************************************************************************/


        // Check for checkSubmit() when there is any text change
        private void userTextBox_TextChanged(object sender, EventArgs e)
        {
            checkSubmit();
        }
        /************************************************************************************************************/


        // Check for checkSubmit() when there is any text change
        private void passTextBox_TextChanged(object sender, EventArgs e)
        {
            checkSubmit();
        }
        /************************************************************************************************************/


        // Check for checkSubmit() when there is any text change
        private void confirmPassTextBox_TextChanged(object sender, EventArgs e)
        {
            checkSubmit();
        }
        /************************************************************************************************************/


        // Check for checkSubmit() when there is any text change
        private void fNameTextBox_TextChanged(object sender, EventArgs e)
        {
            checkSubmit();
        }
        /************************************************************************************************************/


        // Check for checkSubmit() when there is any text change
        private void lastNameTextBox_TextChanged(object sender, EventArgs e)
        {
            checkSubmit();
        }
        /************************************************************************************************************/


        // Check for checkSubmit() when there is any value change
        private void dobDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            checkSubmit();
        }
        /************************************************************************************************************/


        // Check for checkSubmit() when there is any value change
        private void userTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkSubmit();
        }
    }
}
