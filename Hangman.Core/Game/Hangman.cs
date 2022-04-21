using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HangmanRenderer.Renderer;


namespace Hangman.Core.Game
{
    public class HangmanGame
    {
        private GallowsRenderer _renderer;
        //private string _guessword;
        //private string _guessProgress;


        public HangmanGame()
        {
            _renderer = new GallowsRenderer();            
        }

        public void Run()
        {
            int lives = 6;
            bool won = false;
            int realWord = 0;

            string input;
            char guess;

            _renderer.Render(5, 5, 6);
            
            Random random = new Random((int)DateTime.Now.Ticks);
            
            string[] word = 
                { 
                "Beef Burger","Pizza","Pasta", "Fish and Chips","Steak","Chicken and Rice ", "Lamb Chops", "Quesadille", "Ramen",
                "Mashed Potatoes", "Salad", "Sushi", "Chicken Curry","Beef Curry", "Lamb Curry", "Chicken Burger",
                "Ribs and Chips", "Tuna Salad", "Caesar Salad", "Bean Soup" 
                };

            string wordGuess = word[random.Next(0, word.Length)];
            string wordGuessUppercase = wordGuess.ToUpper();

            StringBuilder  player = new StringBuilder(wordGuess.Length);
            for(int index = 0; index < wordGuess.Length; index++ )
            player.Append('*');

            List<char> correctGuesses = new List<char>();
            List<char> incorrectGuesses = new List<char>();

            Console.SetCursorPosition(0, 13);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Try to guess what's on the menu ");
            Console.SetCursorPosition(0, 15);
            
            Console.ForegroundColor = ConsoleColor.Green;


            while (!won && lives > 0)
            {
                Console.SetCursorPosition(0, 13);
                Console.Write("lives left: {0}", lives +". What is your guess: " );
                
                input = Console.ReadLine().ToUpper();
                guess = input[0];
                
                if (correctGuesses.Contains(guess))
                {
                    
                    Console.WriteLine("------------------------------------ \nYou've already guessed {0} ", guess);
                    
                    //breaks one iteration in the loop, if a specified condition occurs, and continues with the next iteration in the loop.
                    continue;
                }
                else if (incorrectGuesses.Contains(guess)) 
                {
                    
                    Console.WriteLine("------------------------------------ \nYou've already guessed {0}, but it does not exist ", guess);
                    continue;
                }

                if(wordGuessUppercase.Contains(guess))
                {
                    correctGuesses.Add(guess);

                    for (int i = 0; i < wordGuess.Length; i++) 
                    { 
                      if(wordGuessUppercase[i] == guess)
                        {
                            player[i] = wordGuess[i];
                            realWord++;
                        }
    
                    }
                    if(realWord == wordGuess.Length)
                    won = true;
                }
                else 
                { 
                incorrectGuesses.Add(guess);
                    Console.SetCursorPosition(0, 17);
                    Console.WriteLine("------------------------------------ \n{0} Does not exist", guess);
                    Console.SetCursorPosition(0, 17);
                    Console.WriteLine("                                                                           ");
                    Console.WriteLine("                                                                           ");

                    lives--;
                    _renderer.Render(5, 5, lives);
                    continue;
                }
                Console.WriteLine(player.ToString());

            }
            if (won)
                
                Console.WriteLine("---------------------------------- \nWINNER!!!");
            else
                Console.WriteLine("---------------------------------- \nOH NO, YOU'VE DIED.\nThe correct word is {0}", wordGuess);

            
        }
        /*public void GuessingGame(string guessword)
        {
            _guessword = guessword;

            for (int index = 0; index < _guessword.Length; index++)
            {
                _guessProgress += "*";
            }

        }

        public string GetGuessProgress()
        {
            return _guessProgress;
        }

        public void AddGuess(char letter)
        {
            char[] guessProgressArray = _guessProgress.ToCharArray();

            for (int index = 0; index < _guessword.Length; index++)
            {
                if (_guessword[index] == letter)
                {
                    guessProgressArray[index] = letter;

                }
            }

            _guessProgress = new string(guessProgressArray);
        }*/
    }
}

       
        
    
    