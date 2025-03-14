using Notory.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Notory.ViewModels;

namespace Notory.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        public RelayCommand SwitchToCalendarCommand { get; set; }
        public RelayCommand SwitchToNotesCommand { get; set; }


        public CalendarViewModel CalendarVM { get; }
        public NotesViewModel NotesVM { get; }

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }


        private bool _isNotesViewVisible;  // Deklaration des privaten Backing-Fields

        public bool IsNotesViewVisible
        {
            get { return _isNotesViewVisible; }
            set
            {
                if (_isNotesViewVisible != value)
                {
                    _isNotesViewVisible = value;
                    OnPropertyChanged(nameof(IsNotesViewVisible)); // Falls du INotifyPropertyChanged verwendest
                }
            }
        }

        public MainViewModel()
        {
            CalendarVM = new CalendarViewModel();
            NotesVM = new NotesViewModel();

            CurrentView = CalendarVM;

            SwitchToCalendarCommand = new RelayCommand(o =>
            {
                CurrentView = CalendarVM;
            });
                
            SwitchToNotesCommand = new RelayCommand(p => 
            {
                CurrentView = NotesVM;
            });



            IsNotesViewVisible = false; 
        }
    }
}
