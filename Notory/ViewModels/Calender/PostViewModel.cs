using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notory.ViewModels.Calender
{
    internal class PostViewModel
    {
        private object SelectedItem = null;

        private readonly DayScheduleViewModel _dayScheduleViewModel;

        public PostViewModel(DayScheduleViewModel dayScheduleViewModel)
        {
            _dayScheduleViewModel = dayScheduleViewModel;

            _dayScheduleViewModel.PropertyChanged += OnDayScheduleViewModelPropertyChanged;

        }
        public void SetPost(object sender)
        {
            SelectedItem = sender;
            Console.WriteLine(SelectedItem);
        }

        private void OnDayScheduleViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DayScheduleViewModel.SelectedItem))
            {
                // Aktualisiere FilterDate, wenn sich SelectedDate ändert
                SelectedItem = _dayScheduleViewModel.SelectedItem;
                SetPost(SelectedItem);
            }
        }
    }
}
