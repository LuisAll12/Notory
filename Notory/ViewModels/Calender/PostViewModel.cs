using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using MongoDB.Bson;
using MongoDB.Driver;
using Notory.Helpers;
using Notory.Models;

namespace Notory.ViewModels.Calender
{
    public class PostViewModel : INotifyPropertyChanged
    {
        private int _selectedItem;
        private CalendarPost _selectedPost;
        private readonly DayScheduleViewModel _dayScheduleViewModel;
        private readonly MongoDBService _mongoDBService;
        private FlowDocument _document = new FlowDocument();

        public PostViewModel(DayScheduleViewModel dayScheduleViewModel)
        {
            _dayScheduleViewModel = dayScheduleViewModel;
            _dayScheduleViewModel.PropertyChanged += OnDayScheduleViewModelPropertyChanged;

            _mongoDBService = new MongoDBService();

            // Initialize Commands
            BoldCommand = new RelayCommand(ExecuteBold);
            ItalicCommand = new RelayCommand(ExecuteItalic);
            UnderlineCommand = new RelayCommand(ExecuteUnderline);
            FontFamilyCommand = new RelayCommand(ExecuteFontFamily);
            FontSizeCommand = new RelayCommand(ExecuteFontSize);
            CheckboxCommand = new RelayCommand(ExecuteCheckbox);
            BulletListCommand = new RelayCommand(ExecuteBulletList);
            NumberedListCommand = new RelayCommand(ExecuteNumberedList);
            SaveCommand = new RelayCommand(ExecuteSave);
            DeleteCommand = new RelayCommand(ExecuteDelete);
            CancelCommand = new RelayCommand(ExecuteCancel);

            // Load Fonts and Sizes
            FontFamilies = new ObservableCollection<FontFamily>(Fonts.SystemFontFamilies.OrderBy(f => f.Source));
            FontSizes = new ObservableCollection<double>(new double[] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 });
            SelectedFontFamily = FontFamilies.FirstOrDefault(f => f.Source == "Arial");
            SelectedFontSize = 12;
        }

        // Properties
        public ObservableCollection<FontFamily> FontFamilies { get; }
        public ObservableCollection<double> FontSizes { get; }

        private FontFamily _selectedFontFamily;
        public FontFamily SelectedFontFamily
        {
            get => _selectedFontFamily;
            set
            {
                _selectedFontFamily = value;
                OnPropertyChanged(nameof(SelectedFontFamily));
            }
        }

        private double _selectedFontSize;
        public double SelectedFontSize
        {
            get => _selectedFontSize;
            set
            {
                _selectedFontSize = value;
                OnPropertyChanged(nameof(SelectedFontSize));
            }
        }

        public FlowDocument Document
        {
            get => _document;
            set
            {
                _document = value;
                OnPropertyChanged(nameof(Document));
            }
        }

        // Commands
        public ICommand BoldCommand { get; }
        public ICommand ItalicCommand { get; }
        public ICommand UnderlineCommand { get; }
        public ICommand FontFamilyCommand { get; }
        public ICommand FontSizeCommand { get; }
        public ICommand CheckboxCommand { get; }
        public ICommand BulletListCommand { get; }
        public ICommand NumberedListCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }

        // Command Methods
        private void ExecuteBold(object parameter)
        {
            if (parameter is TextSelection selection)
            {
                var currentWeight = selection.GetPropertyValue(TextElement.FontWeightProperty);
                var newWeight = currentWeight is FontWeight weight && weight == FontWeights.Bold
                    ? FontWeights.Normal
                    : FontWeights.Bold;
                selection.ApplyPropertyValue(TextElement.FontWeightProperty, newWeight);
            }
        }

        private void ExecuteItalic(object parameter)
        {
            if (parameter is TextSelection selection)
            {
                var currentStyle = selection.GetPropertyValue(TextElement.FontStyleProperty);
                var newStyle = currentStyle is FontStyle style && style == FontStyles.Italic
                    ? FontStyles.Normal
                    : FontStyles.Italic;
                selection.ApplyPropertyValue(TextElement.FontStyleProperty, newStyle);
            }
        }

        private void ExecuteUnderline(object parameter)
        {
            if (parameter is TextSelection selection)
            {
                var currentDecorations = selection.GetPropertyValue(Inline.TextDecorationsProperty);
                selection.ApplyPropertyValue(Inline.TextDecorationsProperty,
                    currentDecorations == TextDecorations.Underline ? null : TextDecorations.Underline);
            }
        }

        private void ExecuteFontFamily(object parameter)
        {
            if (parameter is Tuple<TextSelection, FontFamily> args)
            {
                args.Item1.ApplyPropertyValue(TextElement.FontFamilyProperty, args.Item2);
            }
        }

        private void ExecuteFontSize(object parameter)
        {
            if (parameter is Tuple<TextSelection, double> args)
            {
                args.Item1.ApplyPropertyValue(TextElement.FontSizeProperty, args.Item2);
            }
        }

        private void ExecuteCheckbox(object parameter)
        {
            if (parameter is TextPointer caretPosition)
            {
                var checkbox = new System.Windows.Controls.CheckBox { Content = "Checkbox" };
                var inlineUIContainer = new InlineUIContainer(checkbox, caretPosition);
                if (caretPosition.Paragraph != null)
                {
                    caretPosition.Paragraph.Inlines.Add(inlineUIContainer);
                }
            }
        }

        private void ExecuteBulletList(object parameter)
        {
            var list = new List { MarkerStyle = TextMarkerStyle.Disc };
            var paragraph = new Paragraph();
            list.ListItems.Add(new ListItem(paragraph));
            Document.Blocks.Add(list);
        }

        private void ExecuteNumberedList(object parameter)
        {
            var list = new List { MarkerStyle = TextMarkerStyle.Decimal };
            var paragraph = new Paragraph();
            list.ListItems.Add(new ListItem(paragraph));
            Document.Blocks.Add(list);
        }

        private void ExecuteSave(object parameter)
        {
            try
            {
                // Text als XAML-Format extrahieren
                var range = new TextRange(Document.ContentStart, Document.ContentEnd);
                using (var stream = new MemoryStream())
                {
                    range.Save(stream, DataFormats.Xaml);
                    string xamlText = Encoding.UTF8.GetString(stream.ToArray());



                    _mongoDBService.SavePost(xamlText, SelectedPost);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Console.WriteLine("All good");
            
        }

        private void ExecuteDelete(object parameter)
        {
            // Implement Delete Logic
        }

        private void ExecuteCancel(object parameter)
        {
            // Implement Cancel Logic
        }

        // Existing methods for CalendarPost handling
        public int SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                    SetPost(SelectedItem);
                }
            }
        }

        public CalendarPost SelectedPost
        {
            get => _selectedPost;
            set
            {
                if (_selectedPost != value)
                {
                    _selectedPost = value;
                    OnPropertyChanged(nameof(SelectedPost));
                }
            }
        }

        public async void SetPost(int sender)
        {
            try
            {
                var post = _dayScheduleViewModel?.Entries?.FirstOrDefault(p => p?.Id == sender);
                if (post == null)
                {
                    Console.WriteLine($"Post mit ID {sender} nicht in lokaler Liste gefunden");
                    return;
                }

                SelectedPost = post;

                if (string.IsNullOrWhiteSpace(SelectedPost.MongoId))
                {
                    Console.WriteLine("MongoId ist null oder leer");
                    return;
                }

                CalendarPost mongoPost = await _mongoDBService.GetPost(SelectedPost.MongoId);
                if (mongoPost == null)
                {
                    Console.WriteLine($"Post mit MongoId {SelectedPost.MongoId} nicht in DB gefunden");
                    return;
                }

                var flowDoc = ConvertXamlToFlowDocument(mongoPost.Content);
                if (flowDoc != null)
                {
                    Document = flowDoc;
                }
                else
                {
                    Console.WriteLine("Konvertierung zu FlowDocument fehlgeschlagen");
                    Document = new FlowDocument();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler in SetPost: {ex.Message}");
                Document = new FlowDocument();
            }
        }

        private FlowDocument ConvertXamlToFlowDocument(string xamlContent)
        {
            if (string.IsNullOrWhiteSpace(xamlContent))
                return new FlowDocument();

            try
            {
                var flowDoc = new FlowDocument();
                var range = new TextRange(flowDoc.ContentStart, flowDoc.ContentEnd);

                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(xamlContent)))
                {
                    range.Load(stream, DataFormats.Xaml);
                }
                return flowDoc;
            }
            catch
            {
                return new FlowDocument();
            }
        }

        private void OnDayScheduleViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SelectedItem = _dayScheduleViewModel.SelectedItem;
            SetPost(SelectedItem);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}