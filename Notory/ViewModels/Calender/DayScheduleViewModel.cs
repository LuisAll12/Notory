using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Notory.ViewModels.Calender
{
  public class DayScheduleViewModel : INotifyPropertyChanged
  {
    private DateTime _filterDate;
        public DateTime FilterDate
    {
            get => _filterDate;
            set
            {
                if (_filterDate != value)
                {
                    _filterDate = value;
                    OnPropertyChanged(nameof(FilterDate));
                    ApplyFilterDate(FilterDate);
                }
            }
        }

        public ObservableCollection<TimeLineEntry> Entries { get; set; }

    private readonly CalendarEditPanelViewModel _calendarEditPanelViewModel;

    public DayScheduleViewModel(CalendarEditPanelViewModel calendarEditPanelViewModel)
    {
      _calendarEditPanelViewModel = calendarEditPanelViewModel;

      // Initialisiere die Entries-Collection
      Entries = new ObservableCollection<TimeLineEntry>();

            //Abonniere Änderungen an SelectedDate
      _calendarEditPanelViewModel.PropertyChanged += OnCalendarEditPanelViewModelPropertyChanged;

            //Setze den initialen FilterDate-Wert
      FilterDate = _calendarEditPanelViewModel.SelectedDate;


      ApplyFilterDate(FilterDate);
    }

    private void OnCalendarEditPanelViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == nameof(CalendarEditPanelViewModel.SelectedDate))
      {
        // Aktualisiere FilterDate, wenn sich SelectedDate ändert
        FilterDate = _calendarEditPanelViewModel.SelectedDate;
        ApplyFilterDate(FilterDate);
      }
    }

    public void DaySceduleVM_PropertyChanged(object sender, DateTime selectedDate)
        {
            FilterDate = selectedDate;
            ApplyFilterDate(FilterDate);
        }

        public void ApplyFilterDate(DateTime input)
        {
            FilterDate = input;

            var entries = new ObservableCollection<TimeLineEntry>
                {
                    new TimeLineEntry {Date =  new DateTime(2025, 3, 17), TimeFrom = "10:00", TimeTo = "11:00", Title = "Design System Ref...", Subtitle = FilterDate.ToString() },
                    new TimeLineEntry {Date = new DateTime(2025, 2, 19), TimeFrom = "09:00", TimeTo = "09:30", Title = "Daily Team Standup", Subtitle = "" },
                    new TimeLineEntry {Date = new DateTime(2025, 3, 14), TimeFrom = "12:00", TimeTo = "13:00", Title = "Lunch time", Subtitle = "12 PM – 1 PM" },
                    new TimeLineEntry {Date = new DateTime(2025, 2, 19), TimeFrom = "13:00", TimeTo = "14:00", Title = "Dentist Appointment", Subtitle = "" },
                    new TimeLineEntry {Date = new DateTime(2025, 2, 19), TimeFrom = "15:00", TimeTo = "16:00", Title = "Spirit Planning", Subtitle = "4 PM – 4 PM" },
                    new TimeLineEntry {Date = new DateTime(2025, 2, 19), TimeFrom = "17:00", TimeTo = "17:30", Title = "End of day check-in", Subtitle = "" },
                    new TimeLineEntry {Date = new DateTime(2025, 2, 19), TimeFrom = "11:00", TimeTo = "11:30", Title = "", Subtitle = "" },
                    new TimeLineEntry {Date = new DateTime(2025, 2, 19), TimeFrom = "14:00", TimeTo = "14:30", Title = "", Subtitle = "" },
                    new TimeLineEntry {Date = new DateTime(2025, 2, 19), TimeFrom = "18:00", TimeTo = "18:30", Title = "", Subtitle = "" },
                    new TimeLineEntry {Date = new DateTime(2025, 2, 19), TimeFrom = "19:00", TimeTo = "19:30", Title = "", Subtitle = "" },
                    new TimeLineEntry {Date = new DateTime(2025, 2, 19), TimeFrom = "20:00", TimeTo = "20:30", Title = "", Subtitle = "" }
                };

            var filteredEntries = entries
                .Where(e => e.Date.Date == FilterDate.Date)
                .OrderBy(e => DateTime.Parse(e.TimeFrom))
                .ToList();

            Entries.Clear();
            foreach (var entry in filteredEntries)
            {
                Entries.Add(entry);
            }
        }


    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }

  public class TimeLineEntry
  {
    public DateTime Date { get; set; }
    public string TimeFrom { get; set; }
    public string TimeTo { get; set; }
    public string Title { get; set; }
    public string Subtitle { get; set; }

    public int Duration
    {
      get
      {
        if (DateTime.TryParse(TimeFrom, out var from) && DateTime.TryParse(TimeTo, out var to))
        {
          return (int)(to - from).TotalMinutes;
        }
        return 0;
      }
    }

    public bool HasData => !string.IsNullOrEmpty(Title) || !string.IsNullOrEmpty(Subtitle);
  }
}
