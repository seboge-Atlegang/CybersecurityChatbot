using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CybersecurityChatbot
{
    public partial class QuizWindow : Window
    {
        private List<Question> questions;
        private int currentQuestionIndex = 0;
        private int score = 0;

        public QuizWindow()
        {
            InitializeComponent();
            LoadQuestions();
            ShowQuestion();
        }

        private void LoadQuestions()
        {
            questions = new List<Question>
            {
                new Question
                {
                    Text = "What should you do if you receive an email asking for your password?",
                    Options = new List<string> { "Reply with your password", "Delete the email", "Report the email as phishing", "Ignore it" },
                    CorrectIndex = 2
                },
                new Question
                {
                    Text = "What does 2FA stand for?",
                    Options = new List<string> { "Two-Factor Authentication", "Two File Access", "Transfer File Account", "Trusted Firewall Access" },
                    CorrectIndex = 0
                },
                new Question
                {
                    Text = "Which password is the strongest?",
                    Options = new List<string> { "123456", "Password123", "Qwerty", "P@ssw0rd!2024" },
                    CorrectIndex = 3
                },
                new Question
                {
                    Text = "Why should you keep your software up to date?",
                    Options = new List<string> { "It looks nicer", "It adds new bugs", "It protects against known vulnerabilities", "It's optional" },
                    CorrectIndex = 2
                },
                new Question
                {
                    Text = "What is phishing?",
                    Options = new List<string> { "A sport", "An email scam", "A hacking tool", "A firewall" },
                    CorrectIndex = 1
                }
            };
        }

        private void ShowQuestion()
        {
            if (currentQuestionIndex >= questions.Count)
            {
                ShowFinalScore();
                return;
            }

            FeedbackTextBlock.Visibility = Visibility.Collapsed;
            AnswerButtonsPanel.Children.Clear();

            var question = questions[currentQuestionIndex];
            QuestionTextBlock.Text = $"Q{currentQuestionIndex + 1}: {question.Text}";

            for (int i = 0; i < question.Options.Count; i++)
            {
                var button = new Button
                {
                    Content = question.Options[i],
                    Tag = i,
                    Margin = new Thickness(0, 5, 0, 5),
                    Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 224, 243)),
                    Foreground = System.Windows.Media.Brushes.Black,
                    FontWeight = FontWeights.SemiBold
                };
                button.Click += AnswerButton_Click;
                AnswerButtonsPanel.Children.Add(button);
            }

            NextQuestionButton.Visibility = Visibility.Hidden;
        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedButton = sender as Button;
            int selectedIndex = (int)selectedButton.Tag;
            var question = questions[currentQuestionIndex];

            if (selectedIndex == question.CorrectIndex)
            {
                FeedbackTextBlock.Text = "Correct!Well done girl 🎉";
                FeedbackTextBlock.Foreground = System.Windows.Media.Brushes.Green;
                score++;
            }
            else
            {
                FeedbackTextBlock.Text = $"Incorrect! The correct answer is: {question.Options[question.CorrectIndex]}";
                FeedbackTextBlock.Foreground = System.Windows.Media.Brushes.Red;
            }

            FeedbackTextBlock.Visibility = Visibility.Visible;
            DisableAnswerButtons();
            NextQuestionButton.Visibility = Visibility.Visible;
        }

        private void DisableAnswerButtons()
        {
            foreach (Button btn in AnswerButtonsPanel.Children)
            {
                btn.IsEnabled = false;
            }
        }

        private void NextQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            currentQuestionIndex++;
            ShowQuestion();
        }

        private void ShowFinalScore()
        {
            AnswerButtonsPanel.Children.Clear();
            QuestionTextBlock.Text = $"Quiz Complete!\nYou scored {score} out of {questions.Count}.";

            if (score == questions.Count)
                FeedbackTextBlock.Text = "Outstanding queen! You’re a cybersecurity pro!";
            else if (score >= questions.Count / 2)
                FeedbackTextBlock.Text = "Great job queen! Keep learning to stay safe online!";
            else
                FeedbackTextBlock.Text = "Keep learning to stay safe online queen! 💡";

            FeedbackTextBlock.Foreground = System.Windows.Media.Brushes.DarkBlue;
            FeedbackTextBlock.Visibility = Visibility.Visible;

            NextQuestionButton.Visibility = Visibility.Hidden;
        }
    }

    public class Question
    {
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public int CorrectIndex { get; set; }
    }
}
