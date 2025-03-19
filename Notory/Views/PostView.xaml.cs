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
using Markdig;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using MessageBox = System.Windows.MessageBox;

namespace Notory.Views
{
    /// <summary>
    /// Interaktionslogik für PostView.xaml
    /// </summary>
    public partial class PostView : System.Windows.Controls.UserControl
    {
        private bool _isBold = false;
        private bool _isItalic = false;
        private bool _isUnderline = false;
        public PostView()
        {
            InitializeComponent();
            LoadFonts();
            LoadFontSizes();
        }
        private void LoadFonts()
        {
            FontFamilyComboBox.ItemsSource = Fonts.SystemFontFamilies;
            FontFamilyComboBox.SelectedItem = new System.Windows.Media.FontFamily("Arial");
        }

        private void LoadFontSizes()
        {
            FontSizeComboBox.ItemsSource = new double[] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            FontSizeComboBox.SelectedItem = 12.0;
        }

        private void FontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Editor.Selection != null)
            {
                Editor.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, FontFamilyComboBox.SelectedItem);
            }
        }

        private void FontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Editor.Selection != null)
            {
                Editor.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, FontSizeComboBox.SelectedItem);
            }
        }

        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            _isBold = !_isBold;
            if (_isBold == true) Editor.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            else Editor.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
        }

        private void ItalicButton_Click(object sender, RoutedEventArgs e)
        {
            _isItalic = !_isItalic;
            if (_isItalic == true) Editor.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
            else Editor.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
        }

        private void UnderlineButton_Click(object sender, RoutedEventArgs e)
        {
            _isUnderline = !_isUnderline;
            if (_isUnderline == true) Editor.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            else Editor.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);

            // UnderLineButton.IsChecked = _isUnderline;
        }

        private void ForegroundColorButton_Click(object sender, RoutedEventArgs e)
        {
            var colorDialog = new System.Windows.Forms.ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Editor.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(System.Windows.Media.Color.FromArgb(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B)));
            }
        }

        private void BackgroundColorButton_Click(object sender, RoutedEventArgs e)
        {
            var colorDialog = new System.Windows.Forms.ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Editor.Selection.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(System.Windows.Media.Color.FromArgb(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B)));
            }
        }

        private void CheckboxButton_Click(object sender, RoutedEventArgs e)
        {
            var checkbox = new System.Windows.Controls.CheckBox { Content = "Checkbox" };
            var inlineUIContainer = new InlineUIContainer(checkbox, Editor.CaretPosition);
            Editor.CaretPosition.Paragraph.Inlines.Add(inlineUIContainer);
        }

        private void BulletListButton_Click(object sender, RoutedEventArgs e)
        {
            var list = new List { MarkerStyle = TextMarkerStyle.Disc };
            var paragraph = new Paragraph();
            list.ListItems.Add(new ListItem(paragraph));
            Editor.Document.Blocks.Add(list);
        }

        private void NumberedListButton_Click(object sender, RoutedEventArgs e)
        {
            var list = new List { MarkerStyle = TextMarkerStyle.Decimal };
            var paragraph = new Paragraph();
            list.ListItems.Add(new ListItem(paragraph));
            Editor.Document.Blocks.Add(list);
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = System.IO.Path.Combine(documentsPath, "NotoryFile.xaml");
            SaveToXml(filePath);
        }
        private void SaveToXml(string filePath)
        {
            try
            {
                string directory = System.IO.Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                System.Diagnostics.Debug.WriteLine($"Saving file to: {System.IO.Path.GetFullPath(filePath)}");
                var range = new TextRange(Editor.Document.ContentStart, Editor.Document.ContentEnd);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    range.Save(stream, System.Windows.DataFormats.Xaml);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show($"Access denied: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show($"I/O error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        } 


        private void LoadFromXml(string filePath)
        {
            if (File.Exists(filePath))
            {
                var range = new TextRange(Editor.Document.ContentStart, Editor.Document.ContentEnd);
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    range.Load(stream, System.Windows.DataFormats.Xaml);
                }
            }
        }
    }
}
