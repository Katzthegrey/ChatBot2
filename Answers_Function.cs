using System;
using System.Collections;
using System.Linq;

namespace ChatBot
{
    public class Answers_Function
    {
        //Creating global variable
        ArrayList answer = new ArrayList();
        ArrayList ignore = new ArrayList();

        

        public Answers_Function()
        {
            //Calling my methods here
            answers();
            ignored();
            

            //prompting the user for the question
            Console.WriteLine("What would you like to know about cyber security?");
            string user_question = Console.ReadLine()?.ToLower();

            //using split function to store words
            string[] store_word = user_question.Split(' ');
            ArrayList store_final_words = new ArrayList();

           

            //for loop to go through each word
            for (int i = 0; i < store_word.Length; i++)
            {
                if (!ignore.Contains(store_word[i]))
                {
                     store_final_words.Add(store_word[i]);
                }
                //remember to add error handling if user doesnt input a question that coresponds
            }//end of word loop 

            //varifying if variables are found
            Boolean found = false;
            string message = string.Empty;

            //nested for loop to get final answers
            for (int j = 0; j < store_final_words.Count; j++) 
            {
              //filtering through temp array list
              for (int i = 0; i < answer.Count; i++ ) 
                {
                    //if statement to check if word is found
                    if (answer[i].ToString().ToLower().Contains(store_final_words[j].ToString().ToLower()))
                    {
                        //append the answer
                        message += answer[i] + "\n";
                        found = true;
                         
                    }
                }
            }

            //error handling
            if (found)
            {
                Console.WriteLine(message);
            }
            else 
            {
                Console.WriteLine("Couldnt quite get that could you please search something related to security");
            }

        }//end of constructor

        //method for answering
        private void answers()
        {
            answer.Add("Passwords help as a decryption key to sensitive data which has been encrypted to limit who has access to the data.".ToLower());
            answer.Add("If a Password is compromised an attacker could gain access to sensitive data rendering the encryption useless.".ToLower());
            answer.Add("Strong and unique passwords are recommended to further enhance your security".ToLower());
            answer.Add("Phishing is a method an attacker uses where by they disguise themselves as legit/trusted companies to trick users to reveal sensitive information such as passwords and banking details etc.".ToLower());
            answer.Add("The most common thype of Phishing is email phishing where attackers will send you emails appearing from legitamate sources".ToLower());
            answer.Add("Ways to avoid geting Phishing attacks would be to check the email address and website URL if they are legit before entering any information".ToLower());
        }//end of method

        //method for ignoring words
        private void ignored()
        {
            ignore.Add("tell");
            ignore.Add("me");
            ignore.Add("about");

        }



    }//eof
}//end of namespace