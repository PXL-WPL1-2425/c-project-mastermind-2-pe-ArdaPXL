using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mastermind_2_PE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> colors = new List<string> { "Red", "Yellow", "Orange", "White", "Green", "Blue" };
        private List<string> _code;
        private int _attemptsLeft = 10;
        private int _score = 100;

    

        public MainWindow()
        {
            InitializeComponent();
            _code = new List<string>();
            StartNewGame();
            PopulateComboBoxes();
            StartGame();
        }

        private void PopulateComboBoxes()
        {
            List<string> colorOptions = new List<string> { "Red", "Yellow", "Orange", "White", "Green", "Blue" };

            ComboBox1.ItemsSource = colorOptions;
            ComboBox2.ItemsSource = colorOptions;
            ComboBox3.ItemsSource = colorOptions;
            ComboBox4.ItemsSource = colorOptions;
        }


        private void StartGame()
        {
            Random rand = new Random();
            _code = new List<string>();
            for (int i = 0; i < 4; i++)
            {
                _code.Add(colors[rand.Next(colors.Count)]);
            }

            _attemptsLeft = 10;
            _score = 100;

            ComboBox1.SelectedItem = null;
            ComboBox2.SelectedItem = null;
            ComboBox3.SelectedItem = null;
            ComboBox4.SelectedItem = null;

            ScoreLabel.Content = $"Score: {_score}";
            AttemptsLabel.Content = $"Attempts Left: {_attemptsLeft}";
            ListBoxHistory.Items.Clear();
            DialogResult.ToString();
        }

        private void StartNewGame()
        {
            Random rand = new Random();
            _code = new List<string>();
            for (int i = 0; i < 4; i++)
            {
                _code.Add(colors[rand.Next(colors.Count)]);
            }

            _attemptsLeft = 10;
            _score = 100;

            ComboBox1.SelectedItem = null;
            ComboBox2.SelectedItem = null;
            ComboBox3.SelectedItem = null;
            ComboBox4.SelectedItem = null;

            ScoreLabel.Content = $"Score: {_score}";
            AttemptsLabel.Content = $"Attempts Left: {_attemptsLeft}";
            ListBoxHistory.Items.Clear();

            MessageBox.Show("New game started! Try to guess the code.");
        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> selectedColors = new List<string>
        {
            ComboBox1.SelectedItem?.ToString() ?? "unknown",
            ComboBox2.SelectedItem?.ToString() ?? "unknown",
            ComboBox3.SelectedItem?.ToString() ?? "unknown",
            ComboBox4.SelectedItem?.ToString() ?? "unknown"
        };

            if (selectedColors.Contains("unknown"))
            {
                MessageBox.Show("Please select all colors before checking.");
                return;
            }

            List<string> feedback = new List<string>();
            for (int i = 0; i < selectedColors.Count; i++)
            {
                // Update feedback based on correctness
                if (selectedColors[i] == _code[i])
                {
                    feedback.Add("Correct");
                    UpdateBorderColor(i, Brushes.DarkRed); // Correct color
                }
                else if (_code.Contains(selectedColors[i]))
                {
                    feedback.Add("Wrong Place");
                    UpdateBorderColor(i, Brushes.Wheat); // Incorrect position
                    _score -= 1;
                }
                else
                {
                    feedback.Add("Wrong");
                    UpdateBorderColor(i, Brushes.Transparent); // Incorrect color
                    _score -= 2;
                }
            }

            ScoreLabel.Content = $"Score: {_score}";
            AttemptsLabel.Content = $"Attempts Left: {_attemptsLeft}";
            ListBoxHistory.Items.Add($"Attempt: {string.Join(", ", selectedColors)} | Feedback: {string.Join(", ", feedback)}");

            _attemptsLeft--;
            if (selectedColors.SequenceEqual(_code))
            {
                MessageBox.Show($"You guessed the code! Final Score: {_score}");
                AskToPlayAgain();
            }
            else if (_attemptsLeft == 0)
            {
                MessageBox.Show($"Game over! The code was: {string.Join(", ", _code)}");
                AskToPlayAgain();
            }
        }

        private void AskToPlayAgain()
        {
            
                Close();
            
        }

        private void UpdateBorderColor(int index, Brush color)
        {
            // Apply border color based on feedback
            switch (index)
            {
                case 0:
                    Border1.BorderBrush = color;
                    break;
                case 1:
                    Border2.BorderBrush = color;
                    break;
                case 2:
                    Border3.BorderBrush = color;
                    break;
                case 3:
                    Border4.BorderBrush = color;
                    break;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                string selectedColor = comboBox.SelectedItem?.ToString() ?? "";
                SolidColorBrush solidColorBrush = selectedColor switch 
                {
                    "Red" => Brushes.Red,
                    "Yellow" => Brushes.Yellow,
                    "Orange" => Brushes.Orange,
                    "White" => Brushes.White,
                    "Green" => Brushes.Green,
                    "Blue" => Brushes.Blue,
                    _ => Brushes.Transparent
                };
                SolidColorBrush _solidColorBrush = solidColorBrush;
                Brush colorBrush = _solidColorBrush;

                if (comboBox == ComboBox1) FeedbackEllipse1.Fill = colorBrush;
                if (comboBox == ComboBox2) FeedbackEllipse2.Fill = colorBrush;
                if (comboBox == ComboBox3) FeedbackEllipse3.Fill = colorBrush;
                if (comboBox == ComboBox4) FeedbackEllipse4.Fill = colorBrush;
            }
        }
    }
}
