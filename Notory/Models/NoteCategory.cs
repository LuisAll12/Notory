using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notory.Models
{
    internal class NoteCategory
    {
        public string Name { get; set; }
        public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();
    }
}