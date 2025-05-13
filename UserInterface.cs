using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;

namespace ChatBot
{
    public class UserInterface
    {
        private string userName = string.Empty;
        private string userAsking = string.Empty;
        private ArrayList answers = new ArrayList();
        private ArrayList ignore = new ArrayList();
        //Added memory recall for new global var
        private MemoryRecall memory = new MemoryRecall();
        public UserInterface()
        {
            //calling methods in 
            InitializeAnswers();
            InitializeIgnoreWords();
            ShowWelcomeMessage();

            // Handle user name
            HandleUserName();

            // Main conversation loop
            RunConversation();
        }

        private void InitializeAnswers()
        {
            answers.Add("passwords help as a decryption key to sensitive data which has been encrypted to limit who has access to the data.".ToLower());
            answers.Add("if a password is compromised an attacker could gain access to sensitive data rendering the encryption useless.".ToLower());
            answers.Add("strong and unique passwords are recommended to further enhance your security".ToLower());
            answers.Add("phishing is a method attackers use to disguise themselves as legitimate companies to trick users into revealing sensitive information.".ToLower());
            answers.Add("the most common type of phishing is email phishing where attackers send emails appearing to be from legitimate sources.".ToLower());
            answers.Add("to avoid phishing attacks, always verify email addresses and website URLs before entering any information.".ToLower());
        }

        private void InitializeIgnoreWords()
        {
            ignore.Add("tell");
            ignore.Add("me");
            ignore.Add("about");
            ignore.Add("what");
            ignore.Add("how");
            ignore.Add("is");
            ignore.Add("are");
            ignore.Add("can");
            ignore.Add("you");
        }

        private void ShowWelcomeMessage()
        {
            Console.WriteLine("=======================================================");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[ Welcome to the cybersecurity bot ]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("=======================================================");
            Console.WriteLine("Welcome to chatbot");
        }

        private void HandleUserName()
        {
            // Check for existing user
            var storedName = memory.GetStoredUserName();
            if (!string.IsNullOrEmpty(storedName))
            {
                userName = storedName;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("ChatBot: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Welcome back, {userName}!");
                ShowPreviousQuestions();
            }
            else
            {
                GetNewUserName();
            }
        }

        private void ShowPreviousQuestions()
        {
            var previousQuestions = memory.GetUserQuestions(userName);
            if (previousQuestions.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("ChatBot: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Last time we discussed:");
                foreach (var question in previousQuestions)
                {
                    Console.WriteLine($"- {question}");
                }
            }
        }

        private void GetNewUserName()
        {
            bool validName = false;
            while (!validName)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("ChatBot: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Please enter your name");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("User: ");
                Console.ForegroundColor = ConsoleColor.White;

                string input = Console.ReadLine()?.Trim();

                if (!string.IsNullOrEmpty(input) && Regex.IsMatch(input, @"^[A-Za-z\s'-]+$"))
                {
                    userName = input;
                    validName = true;
                    memory.StoreUserName(userName);
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
            Console.WriteLine($"Welcome {userName}! How may I assist you today?");
        }

        private void RunConversation()
        {
            do
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{userName}: ");
                Console.ForegroundColor = ConsoleColor.White;
                userAsking = Console.ReadLine();

                if (userAsking != "exit")
                {
                    ProcessUserQuestion(userAsking);
                }

            } while (userAsking != "exit");

            Console.WriteLine("Thank you for using the cybersecurity awareness bot. Goodbye!");
        }

        private void ProcessUserQuestion(string question)
        {
            // Store the question first
            memory.StoreQuestion(userName, question);

            // Check for similar questions
            var similarQuestions = memory.FindSimilarQuestions(question);
            if (similarQuestions.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("ChatBot: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("I found similar questions you asked before:");
                foreach (var q in similarQuestions)
                {
                    Console.WriteLine($"- {q}");
                }
                Console.WriteLine();
            }

            // Process the question
            ArrayList filteredWords = FilterQuestionWords(question);
            ArrayList matchedAnswers = FindMatchingAnswers(filteredWords);

            DisplayAnswers(matchedAnswers);
        }

        private ArrayList FilterQuestionWords(string question)
        {
            string[] words = question.Split(' ');
            ArrayList filteredWords = new ArrayList();

            foreach (string word in words)
            {
                if (!ignore.Contains(word.ToLower()))
                {
                    filteredWords.Add(word.ToLower());
                }
            }

            return filteredWords;
        }

        private ArrayList FindMatchingAnswers(ArrayList filteredWords)
        {
            ArrayList matchedAnswers = new ArrayList();

            foreach (string answer in answers)
            {
                foreach (string word in filteredWords)
                {
                    if (answer.Contains(word) && !matchedAnswers.Contains(answer))
                    {
                        matchedAnswers.Add(answer);
                    }
                }
            }

            return matchedAnswers;
        }

        private void DisplayAnswers(ArrayList matchedAnswers)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("ChatBot: ");
            Console.ForegroundColor = ConsoleColor.Gray;

            if (matchedAnswers.Count > 0)
            {
                foreach (string answer in matchedAnswers)
                {
                    Console.WriteLine(answer);
                }
                Console.WriteLine("Hope this response was helpful!");
            }
            else
            {
                Console.WriteLine("Couldn't quite get that. Here are some topics I know about:");
                Console.WriteLine("- Passwords\n- Phishing\n- Security basics");
                Console.WriteLine("Try asking about one of these.");
            }
        }
    }

    public class MemoryRecall
    {
        private string memoryFilePath;

        public MemoryRecall()
        {
            string full_path = AppDomain.CurrentDomain.BaseDirectory;
            string new_path = full_path.Replace("bin\\Debug\\", "").Replace("bin\\Release\\", "");
            memoryFilePath = Path.Combine(new_path, "memory.txt");

            if (!File.Exists(memoryFilePath))
            {
                File.CreateText(memoryFilePath).Close();
            }
        }

        public void StoreUserName(string name)
        {
            var currentMemory = LoadMemory();
            currentMemory.RemoveAll(x => x.StartsWith("USERNAME:"));
            currentMemory.Add($"USERNAME:{name}");
            SaveMemory(currentMemory);
        }

        public string GetStoredUserName()
        {
            List<string> memory = LoadMemory();
            string userEntry = memory.Find(x => x.StartsWith("USERNAME:"));

            if (userEntry == null)
            {
                return string.Empty;
            }
            else if (userEntry.StartsWith("USERNAME:"))
            {
                return userEntry.Substring("USERNAME:".Length);
            }
            else
            {
                return string.Empty;
            }
        }

        public void StoreQuestion(string userName, string question)
        {
            var currentMemory = LoadMemory();
            string entry = $"QUESTION:{userName},{question}";
            if (!currentMemory.Contains(entry))
            {
                currentMemory.Add(entry);
                SaveMemory(currentMemory);
            }
        }

        public List<string> GetUserQuestions(string userName)
        {
            var questions = new List<string>();
            foreach (var entry in LoadMemory())
            {
                if (entry.StartsWith($"QUESTION:{userName},"))
                {
                    questions.Add(entry.Substring($"QUESTION:{userName},".Length));
                }
            }
            return questions;
        }

        public List<string> FindSimilarQuestions(string currentQuestion)
        {
            var similar = new List<string>();
            var currentWords = currentQuestion.Split(' ');
            var ignoreWords = new List<string> { "what", "how", "tell", "me", "about", "is", "are" };

            foreach (var entry in LoadMemory())
            {
                if (entry.StartsWith("QUESTION:"))
                {
                    var question = entry.Substring(entry.IndexOf(',') + 1);
                    int matchCount = 0;

                    foreach (var word in currentWords)
                    {
                        if (!ignoreWords.Contains(word.ToLower()) &&
                            question.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            matchCount++;
                        }
                    }

                    if (matchCount > 1) // At least 2 matching words
                    {
                        similar.Add(question);
                    }
                }
            }
            return similar;
        }

        private List<string> LoadMemory()
        {
            return new List<string>(File.ReadAllLines(memoryFilePath));
        }

        private void SaveMemory(List<string> memory)
        {
            File.WriteAllLines(memoryFilePath, memory);
        }
    }
}