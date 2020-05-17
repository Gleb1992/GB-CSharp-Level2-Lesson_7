using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp
{
    public class Department: INotifyPropertyChanged
    {
        string name = "";
        ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
        long profit = 0;
        public event PropertyChangedEventHandler PropertyChanged;

        public Department(string name, long profit, ObservableCollection<Employee> employees)
        {
            this.employees = employees;
            Name = name;
            Profit = profit;
        }

        public string Name 
        {
            get => name;
            set
            {
                name = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Name)));
            }
        }

        public long Profit
        {
            get => profit;
            set
            {
                profit = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Profit)));
            }
        }

        public ObservableCollection<Employee> Employees 
        { 
            get => employees;
            set 
            {
                employees = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Convert.ToString(this.Employees)));
            }
        }
    }
}
