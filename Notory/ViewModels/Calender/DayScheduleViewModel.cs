using Notory.Helpers;
using Notory.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Notory.ViewModels.Calender
{
      public class DayScheduleViewModel 
      {
            private int _selectedItem;
            public int SelectedItem
            {
                get => _selectedItem;
                set
                {
                    if (_selectedItem != value)
                    {
                        _selectedItem = value;
                        OnPropertyChanged(nameof(SelectedItem)); //<--- wichtig!
                    }
                }
            }
            
            private DateTime _filterDate;
            public DateTime FilterDate{
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
            public ICommand DayItemClickCommand { get; }

            public ObservableCollection<CalendarPost> Entries { get; set; }

            private readonly CalendarEditPanelViewModel _calendarEditPanelViewModel;

            public DayScheduleViewModel(CalendarEditPanelViewModel calendarEditPanelViewModel)
            {
                  _calendarEditPanelViewModel = calendarEditPanelViewModel;
                  
                  // Initialisiere die Entries-Collection
                  Entries = new ObservableCollection<CalendarPost>();

                        //Abonniere Änderungen an SelectedDate
                  _calendarEditPanelViewModel.PropertyChanged += OnCalendarEditPanelViewModelPropertyChanged;

                        //Setze den initialen FilterDate-Wert
                  FilterDate = _calendarEditPanelViewModel.SelectedDate;
                  DayItemClickCommand = new RelayCommand(DayItem_Click);


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
                
                var entries = new ObservableCollection<CalendarPost>
                    {
                        new CalendarPost {MongoId= "67e32f04fa736c5b396d0d0a",Id = 1, Date = new DateTime(2025, 3, 17), TimeFrom = "10:00", TimeTo = "11:00", Title = "Design System Ref...", Subtitle = FilterDate.ToString(), BackgroundColor = "#725cfc"},
                        new CalendarPost {MongoId= "67e32f04fa736c5b396d0d0a",Id = 2, Date = new DateTime(2025, 3, 17), TimeFrom = "09:00", TimeTo = "09:30", Title = "Daily Team Standup", Subtitle = "", BackgroundColor = "#ff7b43"},
                        new CalendarPost {MongoId= "67e32f04fa736c5b396d0d0a",Id = 3, Date = new DateTime(2025, 3, 17), TimeFrom = "12:00", TimeTo = "13:00", Title = "Lunch time", Subtitle = "12 PM – 1 PM" , BackgroundColor = "#3a3c49"},
                        new CalendarPost {MongoId= "67e32f04fa736c5b396d0d0a",Id = 4, Date = new DateTime(2025, 3, 17), TimeFrom = "13:00", TimeTo = "15:00", Title = "Dentist Appointment", Subtitle = "" , BackgroundColor = "#5b9dfe"},
                        new CalendarPost {MongoId= "67e32f04fa736c5b396d0d0a",Id = 5, Date = new DateTime(2025, 3, 17), TimeFrom = "15:00", TimeTo = "16:00", Title = "Spirit Planning", Subtitle = "4 PM – 4 PM" , BackgroundColor = "#da5ece"},
                        new CalendarPost {MongoId= "67e32f04fa736c5b396d0d0a",Id = 6, Date = new DateTime(2025, 3, 17), TimeFrom = "17:00", TimeTo = "17:30", Title = "End of day check-in", Subtitle = "" , BackgroundColor = "#338076"},
                        new CalendarPost {MongoId= "67e32f04fa736c5b396d0d0a",Id = 7, Date = new DateTime(2025, 3, 17), TimeFrom = "11:00", TimeTo = "11:30", Title = "", Subtitle = "" , BackgroundColor = "#000"},
                        new CalendarPost {MongoId= "67e32f04fa736c5b396d0d0a",Id = 8, Date = new DateTime(2025, 3, 17), TimeFrom = "14:00", TimeTo = "14:30", Title = "", Subtitle = "" , BackgroundColor = "#000"},
                        new CalendarPost {MongoId= "67e32f04fa736c5b396d0d0a",Id = 9, Date = new DateTime(2025, 3, 17), TimeFrom = "18:00", TimeTo = "18:30", Title = "", Subtitle = "" , BackgroundColor = "#000"},
                        new CalendarPost {MongoId= "67e32f04fa736c5b396d0d0a",Id = 10, Date = new DateTime(2025, 3, 17), TimeFrom = "19:00", TimeTo = "19:30", Title = "", Subtitle = "" , BackgroundColor = "#000"},
                        new CalendarPost {MongoId= "67e32f04fa736c5b396d0d0a",Id = 11, Date = new DateTime(2025, 3, 17), TimeFrom = "20:00", TimeTo = "20:30", Title = "", Subtitle = "" , BackgroundColor = "#000"}
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
                // Jetzt ist die Liste ready → ViewModels können reagieren
                OnPropertyChanged(nameof(Entries));

                // Wenn du willst, kannst du den zuletzt gewählten Eintrag wieder selektieren
                // oder bewusst SelectedItem resetten
                if (Entries.Any(e => e.Id == SelectedItem))
                {
                    SelectedItem = SelectedItem; // Triggert PropertyChanged nochmal
                }
                else
                {
                    SelectedItem = 0; // Reset
                }
            }


            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(SelectedItem, new PropertyChangedEventArgs(propertyName));
            }
            private void DayItem_Click(object sender)
            {
                Console.WriteLine("DayItemClick");
                if (sender is CalendarPost calendarPost)
                    {
                        SelectedItem = calendarPost.Id; 
                    }


                //_postViewModel.SetPost(SelectedItem);
            }
      }


}
