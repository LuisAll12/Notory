using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Notory.Views
{
    public partial class CalendarEditPanel : UserControl
    {
        private DateTime _currentDate;

        // Selected Date for the DayScheduleView
        public DateTime SelectedDate { get; private set; }

        public CalendarEditPanel()
        {
            InitializeComponent();
            _currentDate = DateTime.Now;
            SelectedDate = _currentDate; // Setzt das heutige Datum als ausgewähltes Datum
            UpdateCalendar();
        }

        private void UpdateCalendar()
        {
            CalendarGrid.Children.Clear();
            CalendarGrid.RowDefinitions.Clear();
            CalendarGrid.ColumnDefinitions.Clear();

            MonthYearText.Text = _currentDate.ToString("MMMM yyyy");

            // Zeilen für Wochentage und Datumsbuttons hinzufügen
            CalendarGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Zeile für Wochentage
            for (int i = 0; i < 6; i++) // 6 Zeilen für die Datumsbuttons
            {
                CalendarGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }

            // Spalten für die 7 Wochentage hinzufügen
            for (int i = 0; i < 7; i++)
            {
                CalendarGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            // Wochentagsnamen hinzufügen
            string[] weekDays = { "MON", "TUE", "WED", "THU", "FRI", "SAT", "SUN" };
            for (int i = 0; i < 7; i++)
            {
                TextBlock dayName = new TextBlock
                {
                    Text = weekDays[i],
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 16,
                    Foreground = Brushes.White
                };
                Grid.SetRow(dayName, 0); // Erste Zeile
                Grid.SetColumn(dayName, i); // Spalte i
                CalendarGrid.Children.Add(dayName);
            }

            // Datumsbuttons hinzufügen
            DateTime firstDayOfMonth = new DateTime(_currentDate.Year, _currentDate.Month, 1);
            int startDay = (int)firstDayOfMonth.DayOfWeek;
            if (startDay == 0) startDay = 7; // Sonntag auf 7 setzen

            int daysInMonth = DateTime.DaysInMonth(_currentDate.Year, _currentDate.Month);
            int totalCells = 6 * 7;
            int currentDay = 1;

            for (int i = 1; i <= totalCells; i++)
            {
                Button dayButton = new Button
                {
                    Margin = new Thickness(2),
                    FontSize = 16,
                    Background = Brushes.Transparent,
                    Foreground = Brushes.White,
                    BorderBrush = Brushes.Transparent,
                    Padding = new Thickness(0),
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Template = (ControlTemplate)this.Resources["RoundButtonTemplate"]
                };

                if (i < startDay || currentDay > daysInMonth)
                {
                    DateTime displayDate;
                    if (i < startDay)
                    {
                        displayDate = firstDayOfMonth.AddDays(i - startDay);
                    }
                    else
                    {
                        displayDate = firstDayOfMonth.AddDays((currentDay - 1) + (i - startDay - daysInMonth + 1));
                    }

                    //dayButton.Content = displayDate.Day.ToString();
                    //dayButton.Background = Brushes.DarkGray;
                    //dayButton.Foreground = Brushes.White;
                    //dayButton.Opacity = 0.2;
                    //dayButton.IsEnabled = false;
                }
                else
                {
                    dayButton.Content = currentDay.ToString();
                    DateTime buttonDate = new DateTime(_currentDate.Year, _currentDate.Month, currentDay);

                    if (buttonDate == SelectedDate)
                    {
                        dayButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5c4ef8"));
                    }
                    else if (buttonDate == DateTime.Today)
                    {
                        if(_currentDate != SelectedDate)
                        {
                            dayButton.Background = Brushes.Transparent;
                        }
                        else
                        {
                            dayButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5c4ef8"));
                        }
                        dayButton.Foreground = Brushes.White;
                    }
                    currentDay++;
                }

                dayButton.Click += DayButton_Click;

                // Position des Buttons im Grid festlegen
                int row = (i + startDay - 2) / 7 + 1; // Zeile berechnen
                int col = (i + startDay - 2) % 7; // Spalte berechnen

                Grid.SetRow(dayButton, row);
                Grid.SetColumn(dayButton, col);
                CalendarGrid.Children.Add(dayButton);
            }
        }

        private void DayButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int selectedDay = int.Parse(button.Content.ToString());

            SelectedDate = new DateTime(_currentDate.Year, _currentDate.Month, selectedDay);
            UpdateCalendar();

            MessageBox.Show($"Selected Date: {_currentDate.ToString("MMMM")} {button.Content}, {_currentDate.Year}");
        }

        private void PrevMonthButton_Click(object sender, RoutedEventArgs e)
        {
            _currentDate = _currentDate.AddMonths(-1);
            UpdateCalendar();
        }

        private void NextMonthButton_Click(object sender, RoutedEventArgs e)
        {
            _currentDate = _currentDate.AddMonths(1);
            UpdateCalendar();
        }
    }
}