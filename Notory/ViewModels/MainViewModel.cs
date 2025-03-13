using Notory.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Notory.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        public CalendarViewModel CalendarVM { get; }
        public NotesViewModel NotesVM { get; }

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        public ICommand SwitchToCalendarCommand { get; }
        public ICommand SwitchToNotesCommand { get; }

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
            SwitchToCalendarCommand = new RelayCommand(() => CurrentView = CalendarVM);
            SwitchToNotesCommand = new RelayCommand(() => CurrentView = NotesVM);
            CurrentView = CalendarVM;

            // Optional: Initialisieren von IsNotesViewVisible, falls nötig
            IsNotesViewVisible = true; // Zum Beispiel, dass die Notes-Ansicht anfangs sichtbar ist
        }
    }
}
