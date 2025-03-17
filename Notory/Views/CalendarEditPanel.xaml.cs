using Notory.ViewModels.Calender;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Notory.Views
{
    public partial class CalendarEditPanel : UserControl
    {
        public CalendarEditPanel()
        {
            InitializeComponent();
      }
        public CalendarEditPanel(DayScheduleViewModel dayScheduleViewModel)
        {
            InitializeComponent();
            this.DataContext = new CalendarEditPanel(dayScheduleViewModel);
        }
    }
}