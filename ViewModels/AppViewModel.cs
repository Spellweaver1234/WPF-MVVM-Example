using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using TaskListMVVM.Commands;
using TaskListMVVM.Dialogs;
using TaskListMVVM.FileServices;
using TaskListMVVM.Models;

namespace TaskListMVVM.ViewModels
{
    public class AppViewModel : BaseViewModel
    {
        private Customer _selectedCustomer;
        private readonly IFileService _fileService;
        private readonly IDialogService _dialogService;

        public ObservableCollection<Customer> Customers { get; }
        public AppViewModel() { }

        public AppViewModel(IFileService fileService, IDialogService dialogService)
        {
            _fileService = fileService;
            _dialogService = dialogService;

            Customers = new Customer().MockData();
        }

        #region Proeties
        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged(nameof(SelectedCustomer));
            }
        }
        #endregion Properties

        #region Commands
        public RelayCommand CommandAdd => new RelayCommand(
            obj => AddAction());

        public RelayCommand CommandRemove => new RelayCommand(
            obj => RemoveAction(obj),
            obj => CanRemove());

        public RelayCommand CommandSort => new RelayCommand(
            obj => SortAction(),
            obj => CanSort());

        public RelayCommand DoubleCommand => new RelayCommand(
            obj => DoubleAction(),
            obj => CanDouble());

        public RelayCommand SaveCommand => new RelayCommand(
            obj => SaveAction());

        public RelayCommand OpenCommand => new RelayCommand(
            obj => OpenAction());
        #endregion Commands

        #region Actions
        private void OpenAction()
        {
            try
            {
                if (_dialogService.OpenFileDialog() != true)
                    return;

                var customers = _fileService.Open(_dialogService.FilePath);

                Customers.Clear();
                foreach (var c in customers)
                    Customers.Add(c);

                _dialogService.ShowMessage("Файл открыт");
            }
            catch (Exception ex)
            {
                _dialogService.ShowMessage(ex.Message);
            }
        }

        private void SaveAction()
        {
            try
            {
                if (_dialogService.SaveFileDialog() != true)
                    return;

                _fileService.Save(_dialogService.FilePath, new List<Customer>(Customers));
                _dialogService.ShowMessage("Файл сохранен");
            }
            catch (Exception ex)
            {
                _dialogService.ShowMessage(ex.Message);
            }
        }

        private void AddAction()
        {
            Customer customer = new Customer();
            Customers.Insert(0, customer);
            SelectedCustomer = customer;
        }

        private void RemoveAction(object obj)
        {
            if (obj is Customer customer)
                Customers.Remove(customer);
        }

        private bool CanRemove()
        {
            return Customers?.Count > 0
                && SelectedCustomer != null;
        }

        private void SortAction()
        {
            var c = Customers[0];
            var s = Customers[2];
            Customers[0] = s;
            Customers[2] = c;
            SelectedCustomer = null;
        }

        private bool CanSort()
        {
            return Customers?.Count > 2;
        }

        private void DoubleAction()
        {
            Customers.Insert(0, SelectedCustomer);
            SelectedCustomer = null;
        }

        private bool CanDouble()
        {
            return SelectedCustomer != null;
        }
        #endregion Actions
    }
}