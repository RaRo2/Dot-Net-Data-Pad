using System;
using System.Collections.Generic;
using System.Text;

namespace TextEditor
{
    class Account
    {
        // Create the fields for the account
        string user, pass, type, fName, lName;
        int dobDay, dobMonth, dobYear;

        // Constructor initialises all the fields
        public Account()
        {
            user = "username";
            pass = "pass123";
            type = "View";
            fName = "John";
            lName = "Doe";
            dobDay = 1;
            dobMonth = 1;
            dobYear = 2000;
        }
        /************************************************************************************************************/


        // Public accessors for user, pass and type
        public string User { get { return user; } }
        public string Pass { get { return pass; } }
        public string Type { get { return type; } }
        /************************************************************************************************************/


        // LoadAccount() takes a string and sepeerates it info individual fields
        public void LoadAccount(string fileLine)
        {
            // Create an array of string seperated by the commas in the string
            string[] fields = fileLine.Split(',');

            // Assign the values to the respective properties
            user = fields[0];
            pass = fields[1];
            type = fields[2];
            fName = fields[3];
            lName = fields[4];

            // Create another array of strings seperated by a dash in the string
            string[] dob = fields[5].Split('-');

            // Assign the values to the respective properties
            dobDay = int.Parse(dob[0]);
            dobMonth = int.Parse(dob[1]);
            dobYear = int.Parse(dob[2]);
        }
        /************************************************************************************************************/


        // toString() returns the account information as a collated string
        public string toString()
        {
            string info = (this.user + " | " + this.pass + " | " + this.type + " | ");
            info += (this.fName + " | " + this.lName + " | ");
            info += (this.dobDay + "/" + this.dobMonth + "/" + this.dobYear );
            return info;
        }
    }
}
