using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Keep_Adding
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<Button> buttonList;
        private int targetNumber = 0;
        private int addition = 0;
        private static int score = 0;
        private int timesTicked = 10;
        DispatcherTimer timer;

        public MainPage()
        {
            this.InitializeComponent();
            buttonList = new List<Button>();
            createListOfButtons();

            timer = new DispatcherTimer();
            timer.Tick += DispatcherTimerEventHandler;
            timer.Interval = new TimeSpan(0, 0, 0, 1);

            assignEventsToButton(buttonList);
            HelperClass.assignNumbersToButton(buttonList);
            displayRandomTargetNumber();
        }

        private void displayRandomTargetNumber()
        {
            timer.Stop();
            targetNumber = HelperClass.returnRandomTargetNumber();
            TargetNumber.Text = targetNumber + "";

            timesTicked = HelperClass.executeGameLogicAndReturnTimerValue(score, buttonList);
            timer.Start();
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            addition = 0;
            userInputTextBlock.Text = "0";
        }

        private void DispatcherTimerEventHandler(object sender, object e)
        {
            timesTicked -= 1;
            timerTextBlock.Text = timesTicked + "";
            if (timesTicked == 0)
            {
                timer.Stop();
                HelperClass.saveHighScores(score);
                HelperClass.saveCurrentScore(score);

                int highScore = HelperClass.getScoreBasedOnType(HelperClass.HIGHSCORE);
                Debug.WriteLine("your high score is :" + highScore);

                int currentScore = HelperClass.getScoreBasedOnType(HelperClass.CURRENTSCORE);
                Debug.WriteLine("your high score is :" + currentScore);
                //TODO:
                //this.Frame.Navigate(typeof(GameOver));
            }
        }

        public void assignEventsToButton(List<Button> buttonList)
        {
            foreach (Button button in buttonList)
            {
                button.Click += addNumberButtonClick;
            }
        }

        private void addNumberButtonClick(object sender, RoutedEventArgs e)
        {
            Button pressedButton = sender as Button;
            if (pressedButton.Content != null)
            {
                string clickedNumber = pressedButton.Content.ToString();
                Debug.WriteLine("Button is Clicked :" + clickedNumber);
                Debug.WriteLine("Current value of targetnumber is :" + targetNumber);

                addition += Int32.Parse(clickedNumber);
                userInputTextBlock.Text = addition + "";

                if (addition == targetNumber)
                {
                    scoreTextBlock.Text = "Score :" + (score += 1);
                    displayRandomTargetNumber();
                    addition = 0;
                    userInputTextBlock.Text = "0";
                }
            }
        }

        private void createListOfButtons()
        {
            buttonList.Add(button1);
            buttonList.Add(button2);
            buttonList.Add(button3);
            buttonList.Add(button4);
            buttonList.Add(button5);
            buttonList.Add(button6);
            buttonList.Add(button7);
            buttonList.Add(button8);
            buttonList.Add(button9);
        }
    }
}
