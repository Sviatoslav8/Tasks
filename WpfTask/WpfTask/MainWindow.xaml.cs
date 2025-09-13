using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace WpfTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private string text;
        private Task task;
        private CancellationTokenSource tokenSource;
        private static string filePath;
        private async void saveFileBt_Click(object sender, RoutedEventArgs e)
        {
            text = textTb.Text;
            textTb.Clear();
            tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;
            task = Task.Run(() => AnalyzeText(text, token),token);
            try
            {
                string report = await (Task<string>)task;
                if (token.IsCancellationRequested)
                {
                    this.tbInfo.Text = "Аналіз скасовано";
                }
                else
                {
                    await SaveReportToFileAsync(report);
                    this.tbInfo.Text = "Звіт успішно збережено у файл.";
                }
            }
            catch (OperationCanceledException)
            {
                this.tbInfo.Text = "Аналіз скасовано";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                tokenSource.Dispose();
                tokenSource = null;
            }
        }
        private async Task SaveReportToFileAsync(string report)
        {
            filePath = @"E:\allfolder\path.txt";

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true))
            {
                using (StreamWriter writer = new StreamWriter(fileStream))
                {
                    await writer.WriteAsync(report);
                    await writer.FlushAsync();
                }
            }
        }
        private string AnalyzeText(string text, CancellationToken token)
        {
            for (int i = 0; i < 5; i++)
            {
                token.ThrowIfCancellationRequested();
                Thread.Sleep(1000);
            }
            token.ThrowIfCancellationRequested();
            int sentenceCount = Regex.Matches(text, @"[\.!?]").Count;
            int charCount = 0;
            foreach (char c in text)
            {
                if (!char.IsWhiteSpace(c))
                    charCount++;
            }
            int wordCount = Regex.Matches(text, @"\b\w+\b").Count;
            int questionCount = Regex.Matches(text, @"\?").Count;
            int exclamationCount = Regex.Matches(text, @"!").Count;

            string report = $"Звіт тексту:\nКількість речень: {sentenceCount}\nКількість символів (без пробілів): {charCount}\nКількість слів: {wordCount}\nКількість питальних речень: {questionCount}\nКількість окличних речень: {exclamationCount}\n";

            return report;
        }

        private async void saveTextBoxBt_Click(object sender, RoutedEventArgs e)
        {
            text = textTb.Text;
            textTb.Clear();
            tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;
            task = Task.Run(() => AnalyzeText(text, token), token);
            try
            {
                string report = await (Task<string>)task;
                if (token.IsCancellationRequested)
                {
                    this.tbInfo.Text = "Аналіз скасовано";
                }
                else
                {
                    this.tbInfo.Text = report;
                }
            }
            catch (OperationCanceledException)
            {
                this.tbInfo.Text = "Аналіз скасовано";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                tokenSource.Dispose();
                tokenSource = null;
            }
        }

        private void stopBt_Click(object sender, RoutedEventArgs e)
        {
            if (tokenSource != null && !tokenSource.IsCancellationRequested)
            {
                tokenSource.Cancel();
            }
        }
    }
}
