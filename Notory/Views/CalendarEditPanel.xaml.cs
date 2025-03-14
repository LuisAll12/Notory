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
using System.Windows.Shapes;

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
            SelectedDate = _currentDate;
            UpdateCalendar();
        }

        private void UpdateCalendar()
        {
          CalendarGrid.Children.Clear();

          MonthYearText.Text = _currentDate.ToString("MMMM yyyy");

          DateTime firstDayOfMonth = new DateTime(_currentDate.Year, _currentDate.Month, 1);

          int startDay = (int)firstDayOfMonth.DayOfWeek;
          if (startDay == 0) startDay = 7; 

          int daysInMonth = DateTime.DaysInMonth(_currentDate.Year, _currentDate.Month);

          int totalCells = 6 * 7;
          int currentDay = 1;

          for (int i = 1; i <= totalCells; i++)
          {
            Button dayButton = new Button
            {
              Margin = new Thickness(2),
              FontSize = 16,
              Background = Brushes.White,
              Foreground = Brushes.Black
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

              dayButton.Content = displayDate.Day.ToString();
              dayButton.Background = Brushes.LightGray; 
              dayButton.Foreground = Brushes.Gray;
              dayButton.IsEnabled = false; 
            }
            else
            {
              dayButton.Content = currentDay.ToString();
              if (currentDay == DateTime.Now.Day && _currentDate.Month == DateTime.Now.Month && _currentDate.Year == DateTime.Now.Year)
              {
                dayButton.Background = Brushes.LightBlue; 
              }
              currentDay++;
            }
            dayButton.Click += DayButton_Click;
            CalendarGrid.Children.Add(dayButton);
          }
        }

        private void DayButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int selectedDay = int.Parse(button.Content.ToString());

            SelectedDate = new DateTime(_currentDate.Year, _currentDate.Month, selectedDay);
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
