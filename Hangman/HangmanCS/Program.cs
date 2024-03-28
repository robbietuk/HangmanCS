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

class Program
{
    static void Main(string[] args)
    {
        // Arg 1 can be the word to guess
        string word = args.Length > 0 ? args[0] : "hangman";

        // Arg 2 can be the number of tries
        int tries = args.Length > 1 ? int.Parse(args[1]) : 10;

        // Create a new game
        Hangman.HangmanGame game = new Hangman.HangmanGame(word, tries);

        // Start the game
        game.Run();
    }
}

namespace Hangman
{
    public class HangmanGame
    {
        private string _targetWord;
        private string _userWord;
        private int _maxAttempts;
        private int _numAttempts;

        public string TargetWord
        {
            get { return _targetWord; }
            set { _targetWord = value; }
        }
        public string UserWord
        {
            get { return _userWord; }
            set { _userWord = value; }
        }

        public int MaxAttempts
        {
            get { return _maxAttempts; }
            set { _maxAttempts = value; }
        }
        public int NumAttempts
        {
            get { return _numAttempts; }
            set { _numAttempts = value; }
        }

        public HangmanGame(string targetWord, int attempts)
        {
            if (targetWord.Length < 1)
            {
                throw new ArgumentException("Word must be at least 1 character long");
            }

            // Check if every letter in the word is a letter
            foreach (char letter in targetWord)
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

            _targetWord = targetWord;
            _maxAttempts = attempts;
            _userWord = new string('_', targetWord.Length);
            _numAttempts = 0;
        }

        public void Reset(string newTargetWord = "")
        {
            if (newTargetWord.Length > 0)
            {
                _targetWord = newTargetWord;
            }
            _numAttempts = 0;
            _userWord = new string('_', _targetWord.Length);
        }

        public bool IsValidChar(char letter)
        {
            if (_targetWord.Contains(letter))
            {
                for (int i = 0; i < _targetWord.Length; i++)
                {
                    if (_targetWord[i] == letter)
                    {
                        _userWord = _userWord.Substring(0, i) + letter + _userWord.Substring(i + 1);
                    }
                }
                return true;
            }
            _numAttempts++;
            return false;

        }
        public bool IsComplete()
        {
            return _userWord == _targetWord;
        }

        public bool Lost()
        {
            return _numAttempts >= _maxAttempts;
        }


        public void Run()
        {
            Reset();
            Console.WriteLine("Welcome to Hangman!");

            while (_numAttempts < _maxAttempts)
            {
                Console.WriteLine($"\nCurrent Guess\t {_userWord}");
                Console.WriteLine($"You have {_maxAttempts - _numAttempts} attempts remaining...");

                Console.Write("Enter a letter: ");
                // Read the line from the console, it might be empty
                string input = Console.ReadLine() ?? "";

                if (input.Length > 1 || input.Length < 1)
                {
                    Console.WriteLine("Invalid input. Input must be a single letter. Please try again.");
                    continue;
                }

                char letterGuess = input.ToLower()[0];

                // Check if the input is a valid letter
                if (!Char.IsLetter(letterGuess))
                {
                    Console.WriteLine("Invalid input. Input must be a letter. Please try again.");
                    continue;
                }

                // Check if the letter has already been guessed

                if (_userWord.Contains(letterGuess))
                {
                    Console.WriteLine("Warning: You've already guessed that letter!");
                    continue;
                }


                if (IsValidChar(input[0]))
                {
                    Console.WriteLine("Correct!");
                }
                else
                {
                    Console.WriteLine("Incorrect!");
                }

                if (IsComplete())
                {
                    Console.WriteLine($"Congratulations! You've won! The word was: {_userWord}");
                    return;
                }
            }
            Console.WriteLine($"You've lost! The word was {_targetWord}. You achieved {_userWord}");
        }
    }
}
