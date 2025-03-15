using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Notory.ViewModels.Calender;

namespace Notory.Views
{
    /// <summary>
    /// Interaktionslogik für DayScheduleView.xaml
    /// </summary>
    public partial class DayScheduleView : UserControl
    {
        public DayScheduleView()
        {
            InitializeComponent();
        }
        public DayScheduleView(CalendarEditPanelViewModel calendarEditPanelViewModel)
        {
            InitializeComponent();
            this.DataContext = new DayScheduleViewModel(calendarEditPanelViewModel);
        }

    }
}
