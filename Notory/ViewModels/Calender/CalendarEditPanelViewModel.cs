using MongoDB.Bson.Serialization.Serializers;
using Notory.Helpers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using MongoDB.Bson;


namespace Notory.ViewModels.Calender
{
    public class CalendarEditPanelViewModel : INotifyPropertyChanged
    {
        public MongoDBService mongodb { get; set; }

        private DateTime _currentDate = DateTime.Now;
        private DateTime _selectedDate;

        public DateTime CurrentDate
        {
            get { return _currentDate; }
            set
            {
                _currentDate = value;
                OnPropertyChanged(nameof(CurrentDate));
                UpdateCalendar();
            }
        }

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged(nameof(SelectedDate)); //<--- wichtig!
                }
            }
        }

        public ObservableCollection<CalendarDay> CalendarDays { get; set; }

        private DateTime _monthYearText;
        public DateTime MonthYearText
        {
            get { return _monthYearText; }
            set
            {
                _monthYearText = value;
                OnPropertyChanged(nameof(MonthYearText));
            }
        }

        public ICommand PrevMonthCommand { get; }
        public ICommand NextMonthCommand { get; }
        public ICommand DayButtonClickCommand { get; }
        public ICommand OpenWindow_NewPostCommand { get; }

        public CalendarEditPanelViewModel()
        {
            // Initialisiere CalendarDays
            CalendarDays = new ObservableCollection<CalendarDay>();
            // Initialize MongoDBService
            mongodb = new MongoDBService();
            // Setze das aktuelle Datum
            CurrentDate = DateTime.Now;
            SelectedDate = CurrentDate;

            // Initialisiere die Commands
            PrevMonthCommand = new RelayCommand(PrevMonth);
            NextMonthCommand = new RelayCommand(NextMonth);
            DayButtonClickCommand = new RelayCommand(DayButtonClick);
            OpenWindow_NewPostCommand = new RelayCommand(OpenWindow_NewCalendarPost);

            // Aktualisiere den Kalender
            UpdateCalendar();
        }

        private void UpdateCalendar()
        {
            MonthYearText = SelectedDate;

            CalendarDays.Clear();

            DateTime firstDayOfMonth = new DateTime(CurrentDate.Year, CurrentDate.Month, 1);
            int startDay = (int)firstDayOfMonth.DayOfWeek;
            if (startDay == 0) startDay = 7;

            int daysInMonth = DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month);
            int totalCells = 6 * 7;
            int currentDay = 1;

            for (int i = 1; i <= totalCells; i++)
            {
              var calendarDay = new CalendarDay();

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

                calendarDay.Day = displayDate.Day.ToString();
                calendarDay.IsEnabled = false;
                          calendarDay.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1b1021"));
                calendarDay.Foreground = Brushes.White;
              }
              else
              {
                calendarDay.Day = currentDay.ToString();
                DateTime buttonDate = new DateTime(CurrentDate.Year, CurrentDate.Month, currentDay);

                if (buttonDate == SelectedDate)
                {
                  calendarDay.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5c4ef8"));
                }
                else if (buttonDate == DateTime.Today)
                {
                  if (CurrentDate != SelectedDate)
                  {
                    calendarDay.Background = Brushes.Transparent;
                  }
                  else
                  {
                    calendarDay.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5c4ef8"));
                  }
                  calendarDay.Foreground = Brushes.White;
                }
                currentDay++;
              }

              CalendarDays.Add(calendarDay);
            }
        }

    private void PrevMonth()
        {
            CurrentDate = CurrentDate.AddMonths(-1);
        }

        private void NextMonth()
        {
            CurrentDate = CurrentDate.AddMonths(1);
        }

        private void DayButtonClick(object parameter)
        {
            if (parameter is CalendarDay calendarDay && int.TryParse(calendarDay.Day, out int selectedDay))
            {
                SelectedDate = new DateTime(CurrentDate.Year, CurrentDate.Month, selectedDay);
                UpdateCalendar();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(SelectedDate, new PropertyChangedEventArgs(propertyName));
        }

        private void OpenWindow_NewCalendarPost()
        {
            Console.WriteLine("Test1");
            // Open Window
            var document = new BsonDocument
            {
                {"Title", "Exam.net downloaded" },
                {"Date", "19.06.2020" },
                {"Text", "Blablabla bliblibliblibli, hubububububububububububububuububub, linganguliguligu niwapa linganggu linganggu" }
            };
            //Send Data to MongoDB
            mongodb.SetPost(document);
        }
    }

    public class CalendarDay
    {
        public string Day { get; set; }
        public bool IsEnabled { get; set; } = true;
        public Brush Background { get; set; } = Brushes.Transparent;
        public Brush Foreground { get; set; } = Brushes.White;
    }
}