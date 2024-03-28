using Hangman;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace HangmanTests
{
    [TestClass]
    public class HangmanGameTests
    {
        [TestMethod]
        public void HangmanGame_Constructor_WithValidInput()
        {
            // Arrange
            string targetWord = "hangman";
            int tries = 10;

            // Act
            Hangman.HangmanGame game = new HangmanGame(targetWord, tries);

            // Assert
            Assert.AreEqual(targetWord, game.TargetWord);
            Assert.AreEqual(tries, game.MaxAttempts);
            Assert.AreEqual(new string('_', targetWord.Length), game.UserWord);
        }

        [TestMethod]
        public void HangmanGame_IsValidChar()
        {
            // Arrange
            string word = "hangman";
            int tries = 10;
            HangmanGame game = new HangmanGame(word, tries);

            // Act
            bool isValid = game.IsValidChar('h');

            // Assert
            Assert.IsTrue(isValid);
            Assert.AreEqual("h______", game.UserWord);
        }

        [TestMethod]
        public void HangmanGame_IsComplete()
        {
            // Arrange
            string word = "hangman";
            int tries = 10;
            HangmanGame game = new HangmanGame(word, tries);

            // Act
            game.IsValidChar('h');
            game.IsValidChar('a');
            game.IsValidChar('n');
            game.IsValidChar('g');
            game.IsValidChar('m');

            // Assert
            Assert.IsTrue(game.IsComplete());
        }
    }
}