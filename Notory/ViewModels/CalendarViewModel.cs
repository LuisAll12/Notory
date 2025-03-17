using System;
using Notory.ViewModels.Calender;

namespace Notory.ViewModels
{
    internal class CalendarViewModel : ViewModelBase
    {
        public CalendarEditPanelViewModel EditPanelVM { get; set; }
        public DayScheduleViewModel DaySceduleVM { get; set; }
        public PostViewModel PostVM { get; set; }

        private object _editpanelview;
        public object EditPanelView
        {
            get => _editpanelview;
            set => SetProperty(ref _editpanelview, value);
        }

        private object _daysceduleview;
        public object DayScedualView
        {
            get => _daysceduleview;
            set => SetProperty(ref _daysceduleview, value);
        }

        private object _postview;
        public object PostView
        {
            get => _postview;
            set => SetProperty(ref _postview, value);
        }

        private object calendarEditPanelVM;
        public object CalendarEditPanelVM
        {
            get => calendarEditPanelVM;
            set => SetProperty(ref calendarEditPanelVM, value);
        }

        private object dayScheduleView;
        public object DayScheduleView
        {
            get => dayScheduleView;
            set => SetProperty(ref dayScheduleView, value);
        }

        public CalendarViewModel()
        {
            EditPanelVM = new CalendarEditPanelViewModel();

            DaySceduleVM = new DayScheduleViewModel(EditPanelVM);
            EditPanelVM.PropertyChanged += EditPanelVM_PropertyChanged;

            PostVM = new PostViewModel();

            CalendarEditPanelVM = EditPanelVM;

            DayScheduleView = DaySceduleVM;
            PostView = PostVM;

        }

        private void EditPanelVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            DaySceduleVM.ApplyFilterDate((DateTime)sender);
        }
    }
}