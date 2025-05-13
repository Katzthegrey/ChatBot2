using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace ChatBot
{
    public class UserInterface
    {
        //declaring my global variables
        private string userName = string.Empty;
        private string userAsking = string.Empty;
        ArrayList answers = new ArrayList();
        ArrayList ignore = new ArrayList();
        public UserInterface()
        {
            //calling methods
            Answers();
            Ignore();

            //welcoming the user
            Console.WriteLine("=======================================================");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[ Welcome to the cybersecurity bot ]");


            //Resetting Color to White
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("========================================================");

            //Welcoming message
            Console.WriteLine("Welcome to chatbot");

            //prompting for user name
            //input validation using while
            bool ValidUserName = false;
            while (!ValidUserName)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("ChatBot: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Please enter your name");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("User: ");
                Console.ForegroundColor = ConsoleColor.White;

                string input = Console.ReadLine()?.Trim();

                //name input validation accepting string characters only
                if (!string.IsNullOrEmpty(input) && Regex.IsMatch(input, @"^[A-Za-z\s'-]+$"))
                {
                    userName = input;
                    ValidUserName = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid name. Only letters and spaces allowed.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("ChatBot: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Welcome " + userName + "!" + " how may I be of Assistance today?");

            do
            {
  

                //changing color for the user side
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(userName + " :");

                //Resetting color back to white
                Console.ForegroundColor = ConsoleColor.White;
                userAsking = Console.ReadLine();


                Answers(userAsking);

                
            } while (userAsking != "exit");





         
        }
        //Method for answering
        private void Answers(string asked) 
        {
            //if else for the case of if the user inputs anything besides exit the program runs
            if (asked != "exit")
            {
                // Process user input
                string[] words = asked.Split(' ');
                ArrayList filteredWords = new ArrayList();

                // Remove ignored words
                foreach (string word in words)
                {
                    if (!ignore.Contains(word))
                    {
                        filteredWords.Add(word);
                    }
                }

                // Find matching answers
                ArrayList matchedAnswers = new ArrayList();
                foreach (string answer in answers)
                {
                    foreach (string word in filteredWords)
                    {
                        if (answer.Contains(word)) 
                        {
                            if (!matchedAnswers.Contains(answer))
                            {
                                matchedAnswers.Add(answer);
                            }
                        }
                    }
                }

                // Display results
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("ChatBot: ");
                Console.ForegroundColor = ConsoleColor.Gray;

                if (matchedAnswers.Count > 0)
                {
                    foreach (string answer in matchedAnswers)
                    {
                        Console.WriteLine(answer);
                    }

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("ChatBot: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Hope this response was helpful!");
                }
                else
                {
                    Console.WriteLine("Couldn't quite get that. Ask about passwords, phishing, etc.");
                }

            }
            else
            {
                Console.WriteLine("Thank you for using the cybersecurity awareness bot. Goodbye!");
                Environment.Exit(0);
            }
        }

        // Initializing arraylist for answers answers 
        private void Answers()
        {
            answers.Add("passwords help as a decryption key to sensitive data which has been encrypted to limit who has access to the data.".ToLower());
            answers.Add("if a password is compromised an attacker could gain access to sensitive data rendering the encryption useless.".ToLower());
            answers.Add("strong and unique passwords are recommended to further enhance your security".ToLower());
            answers.Add("phishing is a method attackers use to disguise themselves as legitimate companies to trick users into revealing sensitive information.".ToLower());
            answers.Add("the most common type of phishing is email phishing where attackers send emails appearing to be from legitimate sources.".ToLower());
            answers.Add("to avoid phishing attacks, always verify email addresses and website URLs before entering any information.".ToLower());
        }

        // Initializing arraylist for ignored words
        private void Ignore()

        {
            ignore.Add("tell");
            ignore.Add("me");
            ignore.Add("about");
            ignore.Add("what");
            ignore.Add("how");
        }
    }
}