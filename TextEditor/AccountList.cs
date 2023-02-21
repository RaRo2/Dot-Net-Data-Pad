using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TextEditor
{
    class AccountList
    {
        // Create a list to store the information within the login text file
        private List<Account> accounts;

        // Constructor creates a new instance of List<>
        public AccountList()
        {
            accounts = new List<Account>();
        }

        // LoadAccounts() processes a txt file and stores the accounts inside the accounts list
        public void LoadAccounts()
        {
            try
            {
                // Read the login file using StreamReader
                StreamReader fileContent = new StreamReader("login.txt");

                // Create a new account
                Account account;

                // Read all the lines within the text file
                while (!fileContent.EndOfStream)
                {
                    // Create an object of the Account class
                    account = new Account();

                    // Store the line within a new string 
                    string line = fileContent.ReadLine();

                    // Load the account and add it to the accounts list
                    account.LoadAccount(line);
                    accounts.Add(account);
                }
                fileContent.Close();
            }

            catch (FileNotFoundException e)
            {
                Console.WriteLine("Account file could not be found.");
            }
        }
        /************************************************************************************************************/


        // AddAccount() takes a string and adds it in the account list as well as the account file
        public void AddAccount(string info)
        {
            try
            {
                // Use StreamWriter to write in the account file
                StreamWriter outputFile = new StreamWriter("login.txt", append: true);

                // Create a new account and load the account into the accounts list
                Account account = new Account();
                account.LoadAccount(info);
                accounts.Add(account);

                // Write a new line at the bottom of the account file sing StreamWriter
                outputFile.Write("\n"+info);
                outputFile.Close();
            }

            catch (FileNotFoundException e)
            {
                Console.WriteLine("Account file could not be found.");
            }
        }
        /************************************************************************************************************/


        // getAccont() takes a string and returns an account if any users in the account list match
        public Account getAccount(string user)
        {
            // If any accounts' username matches, return the account
            foreach (Account a in accounts)
            {
                if (user == a.User) return a;
            }
            // If not, return null
            return null;
        }
        /************************************************************************************************************/


        // loginSuccess() takes two strings and returns if both match with an account in the account file
        public bool loginSuccess(string user, string pass)
        {
            // If any accounts' username AND passwords match, return true
            foreach (Account a in accounts)
            {
                if (user == a.User && pass == a.Pass) return true;
            }
            // If not, return false
            return false;
        }
        /************************************************************************************************************/


        // userExists() takes a username string and returns if any accounts exist with that username
        public bool userExist(string user)
        {
            // If any account's username match, return true
            foreach (Account a in accounts)
            {
                if (user == a.User) return true;
            }
            // If not, return false
            return false;
        }
        /************************************************************************************************************/


        // printList() prints all the account information within the console for debugging purposes
        public void printList()
        {
            foreach (Account a in accounts)
            {
                Console.WriteLine(a.toString());
            }
        }

    }
}
