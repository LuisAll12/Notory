using Notory.Helpers;
using Notory.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;

namespace Notory.ViewModels.Calender
{
    internal class PostViewModel : INotifyPropertyChanged
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
                    OnPropertyChanged(nameof(SelectedItem));
                    SetPost(SelectedItem);
                }
            }
        }

        private readonly DayScheduleViewModel _dayScheduleViewModel;

        public PostViewModel(DayScheduleViewModel dayScheduleViewModel)
        {
            _dayScheduleViewModel = dayScheduleViewModel;

            _dayScheduleViewModel.PropertyChanged += OnDayScheduleViewModelPropertyChanged;

        }
        public void SetPost(int sender)
        {
            SelectedItem = sender;

            CalendarPost post = _dayScheduleViewModel.Entries.FirstOrDefault(p => p.Id == SelectedItem);
            if (post != null) { Console.WriteLine("True"); }else { Console.WriteLine("False"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnDayScheduleViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DayScheduleViewModel.SelectedItem))
            {
                // Aktualisiere FilterDate, wenn sich SelectedDate ändert
                SelectedItem = _dayScheduleViewModel.SelectedItem;
                SetPost(SelectedItem);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
