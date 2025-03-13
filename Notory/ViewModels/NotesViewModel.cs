using Notory.Helpers;
using Notory.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Notory.ViewModels
{
    internal class NotesViewModel : INotifyPropertyChanged
    {
        public ICommand SelectedNoteCommand { get; set; }

        private bool _isNotesViewVisible;

        public bool IsNotesViewVisible
        {
            get { return _isNotesViewVisible; }
            set
            {
                if (_isNotesViewVisible != value)
                {
                    _isNotesViewVisible = value;
                    OnPropertyChanged(nameof(IsNotesViewVisible));
                }
            }
        }

        public NotesViewModel()
        {
            // Initialize the command
            SelectedNoteCommand = new RelayCommand(SelectedNote);
        }
        private void SelectedNote(object parameter)
        {
            // Handle the selection logic here, for example:
            var selectedNote = parameter as Note;
            if (selectedNote != null)
            {
                // Perform actions with the selected note
                // Example: Console.WriteLine(selectedNote.Title);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
