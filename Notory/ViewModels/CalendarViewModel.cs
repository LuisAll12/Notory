using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notory.ViewModels.Calender;

namespace Notory.ViewModels
{
    internal class CalendarViewModel : ViewModelBase
    {
        public CalendarEditPanelViewModel EditPanelVM { get; set; }
        public DaySceduleViewModel DaySceduleVM { get; set; }
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

    }
}
