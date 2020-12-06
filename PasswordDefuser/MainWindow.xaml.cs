using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace PasswordDefuser {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private static readonly string[] passwords = {
            "about", "after", "again", "below", "could", "every", "first", "found", "great",
            "house", "large", "learn", "never", "other", "place", "plant", "point", "right", "small", "sound", "spell",
            "still", "study", "their", "there", "these",
            "thing", "think", "three", "water", "where", "which", "world", "would", "write"
        };

        public MainWindow() {
            InitializeComponent();
        }

        private void UpdateSearch(object sender, TextChangedEventArgs e) {
            string[] first = FirstBox.Text.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
            string[] second = SecondBox.Text.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
            string[] last = LastBox.Text.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
            List<char> firstChars =
                (from possibleChar in first where possibleChar.Length == 1 select possibleChar.ToCharArray()[0])
                .ToList();

            List<char> secondChars = (from possibleChar2 in second
                where possibleChar2.Length == 1
                select possibleChar2.ToCharArray()[0]).ToList();

            List<char> lastChars = (from possibleCharLast in last
                where possibleCharLast.Length == 1
                select possibleCharLast.ToCharArray()[0]).ToList();

            string regex = constructRegex(firstChars, secondChars, lastChars);
            List<string> matches = passwords.Where(possiblePw => Regex.IsMatch(possiblePw, regex)).ToList();

            Result.Text = "";
            StringBuilder resultBuilder = new StringBuilder();
            foreach (string match in matches) {
                resultBuilder.Append(match);
                resultBuilder.Append(", ");
            }

            string resultText = resultBuilder.ToString();
            if (resultText.Length > 0)
                Result.Text = resultBuilder.ToString().Substring(0, resultBuilder.Length - 2);
        }

        private string constructRegex(List<char> first, List<char> second, List<char> last) {
            StringBuilder regexBuilder = new StringBuilder();
            regexBuilder.Append("^[");
            if (first.Count == 0)
                regexBuilder.Append("a-z");
            else
                foreach (char firstChar in first)
                    regexBuilder.Append(firstChar);
            regexBuilder.Append("][");
            if (second.Count == 0)
                regexBuilder.Append("a-z");
            else
                foreach (char secondChar in second)
                    regexBuilder.Append(secondChar);
            regexBuilder.Append("][a-z]+[");
            if (last.Count == 0)
                regexBuilder.Append("a-z");
            else
                foreach (char lastChar in last)
                    regexBuilder.Append(lastChar);

            regexBuilder.Append("]");
            return regexBuilder.ToString();
        }

        private void onReset(object sender, RoutedEventArgs e) {
            FirstBox.Text = "";
            SecondBox.Text = "";
            LastBox.Text = "";
        }
    }
}