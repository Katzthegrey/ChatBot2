using System;
using System.Media;
using System.IO;

namespace ChatBot
{
    public class voice_greeting
    {
        public voice_greeting()
        {
            //gettting full location of project
            string full_location = AppDomain.CurrentDomain.BaseDirectory;

            //replace the bin\debug in full location
            string new_path = full_location.Replace("bin\\Debug\\","");
            

            //try and catch
            try
            {
              string full_path = Path.Combine(new_path,"Greeting.wav");

                //Now we create an instance for the soundplay class
                using (SoundPlayer play = new SoundPlayer(full_path))
                {
                    //play the audio
                    play.PlaySync();
                }//end of using


            }
            catch ( Exception Error )
            {
                Console.Write( Error.Message );
            }//endof catch
        }//end of constructor
    }//end of class
}//end of namespace

