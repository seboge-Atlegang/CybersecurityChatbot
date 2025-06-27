// Fixes and improvements based on the provided MainWindow.xaml.cs and MainWindow.xaml
using System;
using System.Collections.Generic;
using System.Windows;

namespace CybersecurityChatbot
{
    public partial class MainWindow : Window
    {
        // Fixed declaration: use List<CybersecurityTask>, renamed from invalid syntax
        private List<CybersecurityTask> tasks = new List<CybersecurityTask>();
        private List<string> activityLog = new List<string>();
        private string currentTopic = "";

        public MainWindow()
        {
            InitializeComponent();
            AddBotMessage("Hey girl! I'm your Cybersecurity Awareness Bot and i am here to guide you as a girl. Type 'help' to start.");
        }

        private void AddBotMessage(string message)
        {
            ChatListBox.Items.Add("Bot: " + message);
            ChatListBox.ScrollIntoView(ChatListBox.Items[ChatListBox.Items.Count - 1]);
        }

        private void AddUserMessage(string message)
        {
            ChatListBox.Items.Add("You: " + message);
            ChatListBox.ScrollIntoView(ChatListBox.Items[ChatListBox.Items.Count - 1]);
        }

        private void LogActivity(string action)
        {
            string entry = $"[{DateTime.Now:HH:mm}] {action}";
            activityLog.Add(entry);
            if (activityLog.Count > 10)
                activityLog.RemoveAt(0);
        }

        private string GetActivityLogText()
        {
            if (activityLog.Count == 0)
                return "No recent actions logged.";
            return "Here’s a summary of recent actions:\n" + string.Join("\n", activityLog);
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string input = UserInputTextBox.Text.Trim();
            if (string.IsNullOrEmpty(input))
            {
                AddBotMessage("Please enter a question or command.");
                return;
            }

            AddUserMessage(input);
            UserInputTextBox.Clear();

            string response = GetResponse(input.ToLower());
            AddBotMessage(response);
        }

        private string GetResponse(string input)
        {
            if (input.Contains("add task") || input.Contains("set reminder") || input.Contains("remind me"))
            {
                AddTaskButton_Click(null, null);
                return "Let's add a new cybersecurity task.";
            }

            if (input.Contains("play quiz") || input.Contains("start quiz") || input.Contains("quiz me"))
            {
                PlayQuizButton_Click(null, null);
                return "Starting the cybersecurity quiz!";
            }

            if (input.Contains("show activity log") || input.Contains("what have you done"))
            {
                return GetActivityLogText();
            }

            if (input.Contains("help"))
            {
                return "You can ask about password safety, phishing, privacy, scams, malware, or play the quiz!";
            }

            if (input.Contains("bye") || input.Contains("exit"))
            {
                Application.Current.Shutdown();
                return "";
            }

            if (input.Contains("password"))
                return "Use long, unique passwords. Consider a password manager!";
            if (input.Contains("phishing"))
                return "Be wary of suspicious emails asking for personal info.";
            if (input.Contains("privacy"))
                return "Review app permissions regularly and use strong privacy settings.";
            if (input.Contains("scams"))
                return "Be on the look out for Pop-ups saying Your PC is infected! with a fake Microsoft Support phone number.";
            if (input.Contains("malware"))
                return "Never open random emails because it can be Infected Email Attachments,PDFs, Word, or Excel files with malicious macros";

            return "Sorry, I don't understand that yet. Try asking about cybersecurity topics or type 'help'.";
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            AddTaskWindow addTaskWindow = new AddTaskWindow();
            addTaskWindow.Owner = this;
            bool? result = addTaskWindow.ShowDialog();

            if (result == true && addTaskWindow.NewTask != null)
            {
                tasks.Add(addTaskWindow.NewTask);

                string reminderInfo = addTaskWindow.NewTask.ReminderDate.HasValue
                    ? $"Reminder set for {addTaskWindow.NewTask.ReminderDate.Value.ToShortDateString()}"
                    : "No reminder set";

                AddBotMessage($"Task added: '{addTaskWindow.NewTask.Title}'. {reminderInfo}");
                LogActivity($"Task added: '{addTaskWindow.NewTask.Title}' ({reminderInfo})");
            }
        }

        private void PlayQuizButton_Click(object sender, RoutedEventArgs e)
        {
            QuizWindow quizWindow = new QuizWindow();
            quizWindow.Owner = this;
            LogActivity("Quiz started.");
            quizWindow.ShowDialog();
            LogActivity("Quiz ended.");
        }

        private void ShowLogButton_Click(object sender, RoutedEventArgs e)
        {
            AddBotMessage(GetActivityLogText());
        }
    }

    public class CybersecurityTask
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; }
    }
}
