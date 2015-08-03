using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Keep_Adding
{
    class HelperClass
    {
        private static List<string> missingButtons = new List<string> { "1", "", "", "", "5", "6", "7", "", "9" };
        public static readonly string HIGHSCORE = "HighScore";
        public static readonly string CURRENTSCORE = "CurrentScore";
        
        public static void assignNumbersToButton(List<Button> buttonList)
        {
            int buttonNumber = 1;
            foreach (Button button in buttonList)
            {
                button.Content = buttonNumber;
                buttonNumber++;
            }
        }

        public static int returnRandomTargetNumber()
        {
            Random rnd = new Random();
            return rnd.Next(1, 51);
        }

        public static void removeNumbersFromButton(List<Button> buttonList)
        {
            for (int i = 0; i < 9; i++)
            {
                buttonList[i].Content = missingButtons[i];
            }
        }

        public static void shuffleButtons(List<Button> buttonList)
        {
            List<string> shuffleButtons = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            shuffleButtons.Shuffle();

            for (int i = 0; i < 9; i++)
            {
                buttonList[i].Content = shuffleButtons[i];
            }
        }

        public static void shuffleMissingButtons(List<Button> buttonList)
        {
            List<string> shuffleMissingButtons = new List<string> { "1", "", "", "", "5", "6", "7", "", "9" };
            shuffleMissingButtons.Shuffle();

            for (int i = 0; i < 9; i++)
            {
                buttonList[i].Content = shuffleMissingButtons[i];
            }
        }

        public static void saveHighScores(int score)
        {
            if (!ApplicationData.Current.LocalSettings.Values.ContainsKey(HIGHSCORE))
            {
                Debug.WriteLine("Saving HighScore data");
                ApplicationData.Current.LocalSettings.Values[HIGHSCORE] = score;
            }

            int highScore = (int)ApplicationData.Current.LocalSettings.Values[HIGHSCORE];
            if (score > highScore)
            {
                ApplicationData.Current.LocalSettings.Values[HIGHSCORE] = score;
            }
        }

        public static void saveCurrentScore(int score)
        {
            ApplicationData.Current.LocalSettings.Values[CURRENTSCORE] = score;
        }

        public static int getScoreBasedOnType(string scoreType)
        {
            int savedScore = 0;
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey(scoreType))
            {
                savedScore = (int)ApplicationData.Current.LocalSettings.Values[scoreType];
            }

            return savedScore;
        }

        
        public static void callMethodsRandomly(List<Button> buttonList) 
        {
            var listOfMethods = new List<Action> { 
                                        new Action( () => removeNumbersFromButton(buttonList)),
                                        new Action( () => shuffleButtons(buttonList)), 
                                        new Action( () => shuffleMissingButtons(buttonList))};

            listOfMethods.Shuffle();
            var method = listOfMethods[0];
            method();
        }

        public static int executeGameLogicAndReturnTimerValue(int score, List<Button>buttonList)
        {
            int timesTicked = 10;
            //timer count logic
            /*
             * if score is greater than 5 then timer will start at 7
             * 
             * 
             */
            if (score >= 0 && score <= 5) // 5
            {
                timesTicked = 10;

            }
            else if (score > 5 && score <= 10) //10
            {
                timesTicked = 7;
            }
            else if (score > 10 && score <= 15) //15 - 20
            {
                HelperClass.removeNumbersFromButton(buttonList);
                timesTicked = 7;
            }
            else if (score > 15 && score <= 20) //20-25
            {
                HelperClass.shuffleButtons(buttonList);
                timesTicked = 5;
            }
            else if (score > 20 && score <= 25)
            {
                HelperClass.shuffleMissingButtons(buttonList);
                timesTicked = 5;
            }
            else
            {
                timesTicked = 4;
                HelperClass.callMethodsRandomly(buttonList);
            }

            return timesTicked;
        }

    }

    public static class ExtensionMethods
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            Random rnd = new Random();
            while (n > 1)
            {
                int k = (rnd.Next(0, n) % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
