/*! 
 * An implementation of the Hangman game in C#.
 * 
 * Author:
 *  Robert Twyman Skelly
 * Copyright:
 *  (c) 2024 Robert Twyman Skelly
 * 
 * Usage:
 *  dotnet run
 *  dotnet run <word>
 *  dotnet run <word> <tries>
 * 
 * Examples:
 *  dotnet run hangman 10
 */

namespace HangmanCS
{
    class Hangman
    {
        static void Main(string[] args)
        {
            // Arg 1 can be the word to guess
            string word = args.Length > 0 ? args[0] : "hangman";

            // Arg 2 can be the number of tries
            int tries = args.Length > 1 ? int.Parse(args[1]) : 10;

            // Create a new game
            HangmanGame game = new HangmanGame(word, tries);

            // Start the game
            game.Run();
        }


    }

    class HangmanGame

    {
        private string targetWord;
        private string userWord;
        private int maxAttempts;
        private int numAttempts;

        // Create a constructor that takes a word and number of tries
        public HangmanGame(string word, int attempts)
        {
            if (word.Length < 1)
            {
                throw new ArgumentException("Word must be at least 1 character long");
            }

            // Check if every letter in the word is a letter
            foreach (char letter in word)
            {
                if (!Char.IsLetter(letter))
                {
                    throw new ArgumentException("Word must contain only letters");
                }
            }

            if (attempts < 1)
            {
                throw new ArgumentException("Tries must be at least 1");
            }

            targetWord = word;
            maxAttempts = attempts;
            userWord = new string('_', word.Length);
            numAttempts = 0;
        }

        public void Reset(string newTargetWord = "")
        {
            if (newTargetWord.Length > 0)
            {
                targetWord = newTargetWord;
            }
            numAttempts = 0;
            userWord = new string('_', targetWord.Length);
        }

        private bool isValidChar(char letter)
        {
            if (targetWord.Contains(letter))
            {
                for (int i = 0; i < targetWord.Length; i++)
                {
                    if (targetWord[i] == letter)
                    {
                        userWord = userWord.Substring(0, i) + letter + userWord.Substring(i + 1);
                    }
                }
                return true;
            }
            numAttempts++;
            return false;

        }
        private bool isComplete()
        {
            return userWord == targetWord;
        }


        public void Run()
        {
            Reset();
            Console.WriteLine("Welcome to Hangman!");

            while (numAttempts < maxAttempts)
            {
                Console.WriteLine($"\nCurrent Guess\t {userWord}");
                Console.WriteLine($"You have {maxAttempts - numAttempts} attempts remaining...");

                Console.Write("Enter a letter: ");
                // Read the line from the console, it might be empty
                string input = Console.ReadLine() ?? "";

                if (input.Length > 1 || input.Length < 1)
                {
                    Console.WriteLine("Invalid input. Input must be a single letter. Please try again.");
                    continue;
                }

                char letter_guess = input.ToLower()[0];

                // Check if the input is a valid letter
                if (!Char.IsLetter(letter_guess))
                {
                    Console.WriteLine("Invalid input. Input must be a letter. Please try again.");
                    continue;
                }

                // Check if the letter has already been guessed

                if (userWord.Contains(letter_guess))
                {
                    Console.WriteLine("Warning: You've already guessed that letter!");
                    continue;
                }


                if (isValidChar(input[0]))
                {
                    Console.WriteLine("Correct!");
                }
                else
                {
                    Console.WriteLine("Incorrect!");
                }

                if (isComplete())
                {
                    Console.WriteLine($"Congratulations! You've won! The word was: {userWord}");
                    return;
                }
            }
            Console.WriteLine($"You've lost! The word was {targetWord}. You achieved {userWord}");
        }
    }
}
