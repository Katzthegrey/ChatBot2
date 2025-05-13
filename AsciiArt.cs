using System;
using System.Drawing;
using System.IO;

namespace ChatBot

{
    public class AsciiArt
    {
        public AsciiArt()
        {
            //gettting full location of project
            string full_location = AppDomain.CurrentDomain.BaseDirectory;

            //replace the bin\debug in full location
            string new_path = full_location.Replace("bin\\Debug\\", "");

            //try and catch for exception handling

            try
            {
                string imagePath = Path.Combine(new_path, "CyberLogo.png");
                using (Bitmap image = new Bitmap(imagePath))
                {
                    //Calling Method
                    ConvertToAscii(image);
                }

            }
            catch (Exception error) 
            {
                Console.WriteLine(error.Message);
            }//end of try &catch
        }
        //Method 
        private void ConvertToAscii(Bitmap image)
        {
            //These are the characters im going to use to diplay my image in ascii
            string asciiLetters = "@%*+=-:. ";
            // Adjusting image to fit in terminal
            image = ResizeImage(image, 100);

            //Using for loop to go through each pixel in image
            //looping on the y axis
            for (int y = 0; y < image.Height; y++) 
            {
                //looping on the x axis
                for (int x = 0; x < image.Width; x++) 
                {
                    //setting color to greyscale
                    Color pixelColor = image.GetPixel(x, y);
                    int greyScale = (int)((pixelColor.R * 0.3) + (pixelColor.G * 0.59) + (pixelColor.B * 0.11));
                    int index = greyScale * (asciiLetters.Length - 1) / 255;
                    Console.Write(asciiLetters[index]);
                }
                Console.WriteLine();
            }
        }

        //method for resizing
         static Bitmap ResizeImage(Bitmap image, int maxWidth)
        {
            int newHeight = image.Height * maxWidth / image.Width;
            Bitmap resizedImage = new Bitmap(maxWidth, newHeight);
            using (Graphics g = Graphics.FromImage(resizedImage)) 
            {
             g.DrawImage(image,0, 0, maxWidth, newHeight);
            }
            return resizedImage;
        }
    }
}