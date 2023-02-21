using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TextEditor
{
    public partial class LoginForm : Form
    {
        // Create two string for the user's enterred username and password
        string enterUser, enterPass;
        // Create a new AccountList so that this Form can check the account database
        AccountList accList = new AccountList();

        // Constructor initialises the form and loads in the accounts the account list
        public LoginForm()
        {
            InitializeComponent();
            accList.LoadAccounts();
        }
        /************************************************************************************************************/


        // When the exit button is clickec, close the application
        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /************************************************************************************************************/


        // When the login button is clicked attempt to login the user
        private void loginButton_Click(object sender, EventArgs e)
        {
            // If the login is a success
            if (accList.loginSuccess(enterUser, enterPass))
            {
                // Create a new Text Form and show it
                TextForm textform = new TextForm(enterUser);
                textform.Show();

                // Hide this form
                this.Hide();
            }
            // If the login failed
            else
            {
                // Display the error label
                errorLabel.Visible = true;
            }
        }
        /************************************************************************************************************/


        // When the register button is clicked
        private void registerButton_Click(object sender, EventArgs e)
        {
            // Create a new Register Form and show it
            RegisterForm regForm = new RegisterForm();
            regForm.Show();

            // Hide this form
            this.Hide();
        }
        /************************************************************************************************************/


        // checkLogin() enables the login button if both username and password fields are not empty
        private void checkLogin()
        {
            if (userTextBox.Text != "" && passTextBox.Text != "")
            {
                loginButton.Enabled = true;
            }
        }
        /************************************************************************************************************/


        // Check for checkLogin() everytime the password text box is changed
        private void passTextBox_TextChanged(object sender, EventArgs e)
        {
            checkLogin();
            enterPass = passTextBox.Text;
        }
        /************************************************************************************************************/


        // Check for checkLogin() everytime the username text box is changed
        private void userTextBox_TextChanged(object sender, EventArgs e)
        {
            checkLogin();
            enterUser = userTextBox.Text;
        }
    }
}
