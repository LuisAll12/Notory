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
            DataContext = new ViewModels.Calender.CalendarEditPanelViewModel();
        if (DataContext is ViewModels.Calender.CalendarEditPanelViewModel viewModel)
        {
          Console.WriteLine("DataContext ist korrekt gesetzt.");
        }
        else
        {
          Console.WriteLine("DataContext ist NICHT korrekt gesetzt.");
        }
      }
    }
}