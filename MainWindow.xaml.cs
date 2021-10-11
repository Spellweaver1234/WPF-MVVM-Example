using System.Windows;

using TaskListMVVM.Dialogs;
using TaskListMVVM.FileServices;
using TaskListMVVM.ViewModels;

namespace TaskListMVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new AppViewModel(new JsonFileService(), new DefaultDialogService());
        }
    }
}
