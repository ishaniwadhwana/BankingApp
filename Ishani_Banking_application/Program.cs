using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace IshaniBanks
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            //Class for login screen
            Login();
        }



    
        static protected void Header()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("                                 ====================================================");
       //     Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("                                         Welcome to Ishani's Banking System");

            Console.WriteLine("                                 ====================================================");

            Console.WriteLine("");
        }


        
        // Login Screen class to check authentication from login.txt file
        
        static protected void Login()
        {
            
            Header();
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("                                         Please Login to Start");
            Console.WriteLine("");

            Console.Write("                                         Enter your Username: ");
            string Username = Console.ReadLine();

            Console.Write("                                         Enter your Password: ");

            string Password = "";

            // do loop to display asterik for password
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    Password += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Enter)
                        break;
                }
            } while (true);


            //Authentication class to check if username and password are correct.
            CheckUserdata(Username, Password);
        }


        /// <summary>
        /// Class to check Username and Password from Login.txt file
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        static protected void CheckUserdata(string Username, string Password)
        {
            Console.WriteLine("");
            Console.WriteLine("");


            //read all credentials from file
            var pwdFile = File.ReadAllLines("login.txt");

            //creating list of files
            var userList = new List<string>(pwdFile);

            bool IsValidUser = false;

            //running for each loop to match the user details
            foreach (string users in userList)
            {
                if (users.Split('|')[0] == Username && users.Split('|')[1] == Password)
                {
                    IsValidUser = true;
                    break;
                }
            }

            //if user is valid
            if (IsValidUser)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("                                    Valid User! Please press Enter key.");
                Console.ReadKey();
                Menu();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("                                  Invalid User! Please Press enter to try again");
                Console.ReadKey();

                Console.Clear();

                Login();
            }
        }


        /// <summary>
        /// Display Main Menu of Ishani Banking System
        /// </summary>
        static protected void Menu()
        {
            Header();

            Console.WriteLine("                                      1. Create a new account");
            Console.WriteLine("                                      2. Search for an account");
            Console.WriteLine("                                      3. Deposit");
            Console.WriteLine("                                      4. Withdraw");
            Console.WriteLine("                                      5. A/C Statement");
            Console.WriteLine("                                      6. Delete account");
            Console.WriteLine("                                      7. Exit");

            Console.WriteLine("");
            Console.WriteLine("                                 ====================================================");
            Console.WriteLine("");

            Console.Write("                                           Enter your choice (1-7): ");

            string selectedOption = Console.ReadLine();

            //switch case based on selected option
            switch (selectedOption)
            {
                case "1":
                    CreateAccount();
                    break;

                case "2":
                    SearchAccount();
                    break;
                case "3":
                    DepositMoney();
                    break;

                case "4":
                    WithdrawMoney();
                    break;

                case "5":
                    AccountStatement();
                    break;

                case "6":
                    DeleteAccount();
                    break;

                case "7":
                    break;

                default:
                    Menu();
                    break;
            }
        }


        
        // class for creating a new account for the user
        
        static protected void CreateAccount()
        {

           
            Header();

            Console.Write("                                   Enter your First Name: ");
            string FirstName = Console.ReadLine();

            Console.Write("                                   Enter your Last Name: ");
            string LastName = Console.ReadLine();

            Console.Write("                                   Enter your Address: ");
            string Address = Console.ReadLine();
            long Phone = 0;

            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("                                   Enter your valid Phone Number: ");

                try
                {
                    Phone = Convert.ToInt64(Console.ReadLine());

                    if (Phone.ToString().Length > 0 && Phone.ToString().Length <= 10)
                    {
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("                         Invalid Phone Number, please enter within 10 digits.");
                    }
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("                           Please Enter numeric phone number only");
                }
            } while (true);

            string Email = "";
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("                                   Enter your Email: ");
                Email = Console.ReadLine();

                if (Email.Contains("@"))
                    break;
            } while (true);

            Console.Write("                                 Is the information correct (y/n): ");
            string Input = Console.ReadLine();

            if (Input.ToLower() == "y")
            {
                //Check and Save the data in a File
                long AccountNum = 100001;
                do
                {
                    if (File.Exists(AccountNum.ToString() + ".txt"))
                    {
                        AccountNum++;
                    }
                    else
                    {
                        string[] AccData = { " Amount:|0", "FirstName:|" + FirstName, "SecondName:|" + LastName, "Address:|" + Address, "Phone:|" + Phone, "Email:|" + Email };

                        File.WriteAllLines(AccountNum + ".txt", AccData);

                        Console.WriteLine("");
                        Console.WriteLine("                          Account Created suuccessfully! Details are provided via email " + Email);
                        SendEmail(Email, AccountNum);
                        Console.WriteLine("");
                        Console.WriteLine("                             Your Account Number is : " + AccountNum.ToString());


                        break;
                    }
                } while (true);

                //Go back to main menu
                Menu();
            }
            else
            {
                //Open the same class to create another account
                CreateAccount();
            }
        }

        //code for searching account
        static protected void SearchAccount()
        {
            Header();
            Console.WriteLine("                                                   ENTER THE DETAILS");
            Console.WriteLine("");

            Console.Write("                                  Please write your Account Number: ");

            long AccountNum = 0;

            AccountNum = Convert.ToInt32(Console.ReadLine());

            if (AccountNum.ToString().Length > 10)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("                         Account number should be less than 10 digit");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("                             Check another account (y/n): ");

                if (Console.ReadLine().ToLower() == "y")
                {
                    SearchAccount();
                }
                else
                {
                    Menu();
                }
            }
            if (File.Exists(AccountNum.ToString() + ".txt"))
            {
                Console.WriteLine("                                  Account found!");

                DisplayAccount(AccountNum);

                Console.Write("                                  Check another account (y/n): ");

                if (Console.ReadLine().ToLower() == "y")
                {
                    SearchAccount();
                }
                else
                {
                    Menu();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("                                      Account not found!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("                                Check another account (y/n): ");

                if (Console.ReadLine().ToLower() == "y")
                {
                    SearchAccount();
                }
                else
                {
                    Menu();
                }
            }
        }

        //code for display account details
        static protected void DisplayAccount(long AccountNumber)
        {
            var account = File.ReadAllLines(AccountNumber.ToString() + ".txt");

            var accData = new List<string>(account);

            Console.WriteLine("");
            Console.WriteLine("                                 ====================================================");
            Console.WriteLine("                                                   ACCOUNT DETAILS");
            Console.WriteLine("                                 ====================================================");
            Console.WriteLine("");

            foreach (string data in accData)
            {
                Console.WriteLine(  data.Split('|')[0] + " " + data.Split('|')[1]);
            }
            Console.WriteLine("                                 ====================================================");
            Console.WriteLine("");

        }

        //code for account statement
        static protected void AccountStatement()
        {
            Header();
            Console.WriteLine("                               ENTER THE DETAILS");
            Console.WriteLine("");

            Console.Write("                                   Please enter Account Number: ");

            long AccountNum = 0;

            AccountNum = Convert.ToInt32(Console.ReadLine());

            if (AccountNum.ToString().Length > 10)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("                               Account number should be less than 10 digit");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("                      Check another account (y/n): ");

                if (Console.ReadLine().ToLower() == "y")
                {
                    AccountStatement();
                }
                else
                {
                    Menu();
                }
            }
            if (File.Exists(AccountNum.ToString() + ".txt"))
            {
                Console.WriteLine("                             Account found! The statement is displayed below...");

                DisplayAccount(AccountNum);

                Console.Write("                             Do you want to Email statement? (y/n): ");

                if (Console.ReadLine().ToLower() == "y")
                {
                    var account = File.ReadAllLines(AccountNum.ToString() + ".txt");

                    var accData = new List<string>(account);

                    string EmailID = string.Empty;

                    foreach (string data in accData)
                    {
                        if (data.Split('|')[0] == "Email:")
                            EmailID = data.Split('|')[1];
                    }

                    SendEmail(EmailID, AccountNum);
                    Console.Write("                          Email sent successfully!...Please check your email.");
                    Console.ReadKey();
                    Menu();
                }
                else
                {
                    Menu();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("                                Account not found!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("                               Check another account (y/n): ");

                if (Console.ReadLine().ToLower() == "y")
                {
                    AccountStatement();
                }
                else
                {
                    Menu();
                }
            }
        }
        //code for deleting user account

        static protected void DeleteAccount()
        {
            Header();
            Console.WriteLine("                              ENTER THE DETAILS");
            Console.WriteLine("");

            Console.Write("                                  Please enter your Account Number: ");

            long AccountNum = 0;

            AccountNum = Convert.ToInt32(Console.ReadLine());

            if (AccountNum.ToString().Length > 10)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("                            Account number should be less than 10 digit");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("                                Check another account (y/n): ");

                if (Console.ReadLine().ToLower() == "y")
                {
                    DeleteAccount();
                }
                else
                {
                    Menu();
                }
            }
            if (File.Exists(AccountNum.ToString() + ".txt"))
            {
                Console.WriteLine("                               Account found! Details are displayed below...");

                DisplayAccount(AccountNum);

                Console.Write("                               Delete account (y/n): ");

                if (Console.ReadLine().ToLower() == "y")
                {
                    File.Delete(AccountNum + ".txt");
                    Console.WriteLine("                            Account Deleted successfully!... Press any key to continue");
                    Console.ReadKey();
                    Menu();
                }
                else
                {
                    Menu();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("                                 Account not found!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("                                Check another account (y/n): ");

                if (Console.ReadLine().ToLower() == "y")
                {
                    DeleteAccount();
                }
                else
                {
                    Menu();
                }
            }
        }

        //code for depositing money into user account
        static protected void DepositMoney()
        {
            Header();
            Console.WriteLine("                             ENTER THE DETAILS");
            Console.WriteLine("");

            Console.Write("                             Please enter your Account Number: ");

            long AccountNum = 0;

            AccountNum = Convert.ToInt32(Console.ReadLine());

            if (AccountNum.ToString().Length > 10)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("                             Account number should be less than 10 digit");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("                             Check another account (y/n): ");

                if (Console.ReadLine().ToLower() == "y")
                {
                    DepositMoney();
                }
                else
                {
                    Menu();
                }
            }
            if (File.Exists(AccountNum.ToString() + ".txt"))
            {
                Console.WriteLine("                              Account found! Enter the amount to deposit.");


                do
                {
                    try
                    {
                        Console.WriteLine("");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("                              Amount: $");
                        long DepositAmount = Convert.ToInt32(Console.ReadLine());

                        var account = File.ReadAllLines(AccountNum.ToString() + ".txt");

                        var accData = new List<string>(account);

                        long currentBalance = Convert.ToInt32(accData[0].Split('|')[1]);

                        currentBalance = currentBalance + DepositAmount;
                        
                        accData[0] = "Amount:|" + currentBalance.ToString();

                        File.WriteAllLines(AccountNum + ".txt", accData);

                        Console.WriteLine("                             Deposit Successfull!... Press any key to return to Main Menu");
                        Console.ReadKey();
                        break;
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("                             Please enter valid amount");
                    }
                } while (true);

                Menu();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("                           Sorry! Account not found!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("                             Check another account (y/n): ");

                if (Console.ReadLine().ToLower() == "y")
                {
                    DepositMoney();
                }
                else
                {
                    Menu();
                }
            }
        }

        //code for withdrawing money from user account
        static protected void WithdrawMoney()
        {
            Header();
            Console.WriteLine("                            ENTER THE DETAILS");
            Console.WriteLine("");

            Console.Write("                            Account Number: ");

            long AccountNum = 0;

            AccountNum = Convert.ToInt32(Console.ReadLine());

            if (AccountNum.ToString().Length > 10)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("                            Account number should be less than 10 digit");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("                            Check another account (y/n): ");

                if (Console.ReadLine().ToLower() == "y")
                {
                    WithdrawMoney();
                }
                else
                {
                    Menu();
                }
            }
            if (File.Exists(AccountNum.ToString() + ".txt"))
            {
                Console.WriteLine("                            Account found! Enter the amount to withdraw.");


                do
                {
                    try
                    {
                        Console.WriteLine("");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("                            Amount: $");
                        long WithdrawAmount = Convert.ToInt32(Console.ReadLine());

                        var account = File.ReadAllLines(AccountNum.ToString() + ".txt");

                        var accData = new List<string>(account);

                        long currentBalance = Convert.ToInt32(accData[0].Split('|')[1]);

                        if (currentBalance < WithdrawAmount)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("                            Sorry! Your account balance is less than the amount you are withdrawing.");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {

                            currentBalance = currentBalance - WithdrawAmount;

                            accData[0] = "Amount:|" + currentBalance.ToString();

                            File.WriteAllLines(AccountNum + ".txt", accData);

                            Console.WriteLine("                            Withdraw Successfull!... Press any key to return to Main Menu");
                            Console.ReadKey();
                            break;
                        }
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("                            Please enter valid amount");
                    }
                } while (true);

                Menu();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("                            Account not found!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("                       Check another account (y/n): ");

                if (Console.ReadLine().ToLower() == "y")
                {
                    WithdrawMoney();
                }
                else
                {
                    Menu();
                }
            }
        }
        //code for email configurations
        static protected void SendEmail(string EmailID, long AccountNumber)
        {
            MailMessage mail = new MailMessage("dummyemailishani@gmail.com", EmailID);
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("dummyemailishani@gmail.com", "Hello@123");

            string Body = File.ReadAllText(AccountNumber.ToString() + ".txt");
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            mail.Subject = "Account: " + AccountNumber.ToString();
            mail.Body = Body;
            client.Send(mail);
        }
    }
}

///end