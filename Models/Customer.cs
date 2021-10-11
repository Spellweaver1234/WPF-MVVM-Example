using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TaskListMVVM.Models
{
    public class Customer : INotifyPropertyChanged
    {
        private bool isSolved;
        private string name;
        private string task;
        private DateTime deadline;

        public Customer() { }

        public Customer(string name, string task, int price, DateTime deadline)
        {
            Name = name;
            Task = task;
            Price = price;
            IsSolved = false;
            Deadline = deadline;
        }

        public ObservableCollection<Customer> MockData()
        {
            return new ObservableCollection<Customer>()
            {
            new Customer("Евгений", "Починить принтер", 500, new DateTime(2019, 5, 11)),
            new Customer("Женя", "Заказать пиццу на др", 350, new DateTime(2019, 6, 15)),
            new Customer("Толя", "Обновить windows в 405 кабинете", 100, new DateTime(2019, 6, 17)),
            new Customer("Никита", "Купить 15м сетевого кабеля RJ-45", 400, new DateTime(2019, 6, 19))
            };
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Task
        {
            get => task;
            set
            {
                task = value;
                OnPropertyChanged(nameof(Task));
            }
        }

        public int Price { get; }

        public string DeadlineString
        {
            get => $"{Deadline.Day}.{Deadline.Month}.{Deadline.Year}";
        }

        public DateTime Deadline
        {
            get => deadline;
            set
            {
                deadline = value;
                OnPropertyChanged(nameof(Deadline));
            }
        }

        public bool IsSolved
        {
            get => isSolved;
            set
            {
                isSolved = value;
                OnPropertyChanged(nameof(IsSolved));
                OnPropertyChanged(nameof(Color));
            }
        }

        public string Color
        {
            get => isSolved ?
                "Green" : DateTime.Now.CompareTo(deadline) == -1 ?
                "Black" : "Red";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}