using System.Windows;

using TaskListMVVM.Dialogs;
using TaskListMVVM.FileServices;
using TaskListMVVM.ViewModels;

namespace TaskListMVVM
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new AppViewModel(new JsonFileService(), new DefaultDialogService());
        }
    }
}
