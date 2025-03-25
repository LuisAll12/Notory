using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media;
using Notory.ViewModels.Calender;

namespace Notory.Views
{
    public partial class PostView : System.Windows.Controls.UserControl
    {
        public PostView()
        {
            InitializeComponent();
        }

        private void PostView_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is PostViewModel vm)
            {
                Editor.Document = vm.Document ?? new FlowDocument();
            }
        }

        private void FontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = DataContext as PostViewModel;
            if (vm != null && e.AddedItems.Count > 0)
            {
                var fontFamily = e.AddedItems[0] as FontFamily;
                vm.FontFamilyCommand.Execute(new Tuple<TextSelection, FontFamily>(Editor.Selection, fontFamily));
            }
        }

        private void FontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = DataContext as PostViewModel;
            if (vm != null && e.AddedItems.Count > 0)
            {
                var fontSize = (double)e.AddedItems[0];
                vm.FontSizeCommand.Execute(new Tuple<TextSelection, double>(Editor.Selection, fontSize));
            }
        }
        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as PostViewModel;
            vm?.BoldCommand.Execute(Editor.Selection);
        }

        private void ItalicButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as PostViewModel;
            vm?.ItalicCommand.Execute(Editor.Selection);
        }

        private void UnderlineButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as PostViewModel;
            vm?.UnderlineCommand.Execute(Editor.Selection);
        }

        private void ForegroundColorButton_Click(object sender, RoutedEventArgs e)
        {
            var colorDialog = new System.Windows.Forms.ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var wpfColor = System.Windows.Media.Color.FromArgb(
                    colorDialog.Color.A,
                    colorDialog.Color.R,
                    colorDialog.Color.G,
                    colorDialog.Color.B);

                Editor.Selection.ApplyPropertyValue(
                    TextElement.ForegroundProperty,
                    new SolidColorBrush(wpfColor));
            }
        }

        private void BackgroundColorButton_Click(object sender, RoutedEventArgs e)
        {
            var colorDialog = new System.Windows.Forms.ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var wpfColor = System.Windows.Media.Color.FromArgb(
                    colorDialog.Color.A,
                    colorDialog.Color.R,
                    colorDialog.Color.G,
                    colorDialog.Color.B);

                Editor.Selection.ApplyPropertyValue(
                    TextElement.BackgroundProperty,
                    new SolidColorBrush(wpfColor));
            }
        }

        private void CheckboxButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as PostViewModel;
            vm?.CheckboxCommand.Execute(Editor.CaretPosition);
        }

        private void BulletListButton_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as PostViewModel)?.BulletListCommand.Execute(null);
        }

        private void NumberedListButton_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as PostViewModel)?.NumberedListCommand.Execute(null);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "XAML Files (*.xaml)|*.xaml|All files (*.*)|*.*",
                DefaultExt = "xaml",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                (DataContext as PostViewModel)?.SaveCommand.Execute(saveFileDialog.FileName);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as PostViewModel)?.DeleteCommand.Execute(null);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as PostViewModel)?.CancelCommand.Execute(null);
        }



    }
}