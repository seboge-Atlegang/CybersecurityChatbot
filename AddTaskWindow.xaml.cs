using System;
using System.Windows;

namespace CybersecurityChatbot
{
    public partial class AddTaskWindow : Window
    {
        public CybersecurityTask NewTask { get; private set; }

        public AddTaskWindow()
        {
            InitializeComponent();
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleTextBox.Text.Trim();
            string description = DescriptionTextBox.Text.Trim();
            DateTime? reminderDate = ReminderDatePicker.SelectedDate;

            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Please enter a title for the task.", "Missing Title", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NewTask = new CybersecurityTask
            {
                Title = title,
                Description = description,
                ReminderDate = reminderDate
            };

            DialogResult = true; // Signals success to the parent window
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Signals cancellation
            Close();
        }
    }
}
