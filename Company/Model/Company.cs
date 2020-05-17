using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CompanyApp
{
    public interface IView
    {
        ObservableCollection<Department> Departments { get; set; }
        Department ViewDepartment { get; set; }
        ObservableCollection<Employee> Employee { get; set; }
        Employee ViewEmployee { get; set; }
    }


    public class Company : INotifyPropertyChanged
    {

        private ObservableCollection<Department> departments = new ObservableCollection<Department>();
        Department viewDepartment;
        private ObservableCollection<Employee> employee = new ObservableCollection<Employee>();
        Employee viewEmployee;
        double costs = 0.0;
        double profit = 0.0;
        Random random = new Random();
        int positionDep = 0;

        public double Costs { get => costs; set => costs = value; }

        public double Profit { get => profit; set => profit = value; }

        public ObservableCollection<Department> Departments
        {
            get => departments;
            set
            {
                departments = value;
                OnPropertyChanged("SelectedDepartment");
            }
        }

        public Department ViewDepartment
        {
            get => viewDepartment;
            set
            {
                viewDepartment = value;
                OnPropertyChanged("ViewDepartment");
            }
        }

        public ObservableCollection<Employee> Employee
        {
            get => employee;
            set
            {
                employee = value;
                OnPropertyChanged("SelectedEmployee");
            }
        }

        public Employee ViewEmployee
        {
            get => viewEmployee;
            set
            {
                viewEmployee = value;
                OnPropertyChanged("ViewEmployee");
            }
        }

        public Company() => FillWithRandomData();
        
        public void FillWithRandomData()
        {
            var dep = random.Next(2, 10);
            for (positionDep = 0; positionDep < dep; ++positionDep)
            {
                departments.Add(new Department($"Отдел {positionDep}", random.Next(-100000, 10000000), new ObservableCollection<Employee>()));
                var empl = random.Next(5, 40);
                for (var j = 0; j < empl; ++j)
                {
                    var rate = random.Next(10000, 100000);
                    departments[positionDep].Employees.Add(new Employee(global::CompanyApp.Employee.allName[random.Next(0, global::CompanyApp.Employee.allName.Length)], random.Next(18, 100), rate, rate));
                }
            }
            --positionDep;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "") =>  PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public void AddEpmloyee()
        {
            var rate = random.Next(10000, 100000);
            Employee empl = new Employee(global::CompanyApp.Employee.allName[random.Next(0, global::CompanyApp.Employee.allName.Length)], random.Next(18, 100), rate, rate);
            Employee.Add(empl);
            ViewEmployee = empl;
        }

        public void DeleteEpmloyee()
        {
            if (viewEmployee != null && employee.Count > 1)
                employee.Remove(viewEmployee);
        }
        public void AddDepartment()
        {
            var e = new ObservableCollection<Employee>();
            var empl = random.Next(5, 40);
            for (var j = 0; j < empl; ++j)
            {
                var rate = random.Next(10000, 100000);
                e.Add(new Employee(global::CompanyApp.Employee.allName[random.Next(0, global::CompanyApp.Employee.allName.Length)], random.Next(18, 100), rate, rate));
            }
            departments.Add(new Department($"Отдел {++positionDep}", random.Next(-100000, 10000000), e));
        }
        

        public void DeleteDepartment()
        {
            if (viewDepartment != null && departments.Count > 1)
                departments.Remove(viewDepartment);
        }

        public void TreeViewItem_OnItemSelected(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.OriginalSource as TreeViewItem;
            if (item != null)
            {
                TreeViewItem treeitem = (TreeViewItem)GetSelectedTreeViewItemParent(item);
                if (treeitem.Header is Department)
                {
                    ViewDepartment = (Department)treeitem.Header;
                    Employee = ViewDepartment.Employees;
                }
            }
        }

        public ItemsControl GetSelectedTreeViewItemParent(TreeViewItem item)
        {
            var p = VisualTreeHelper.GetParent(item);
            while (!(p is TreeViewItem || p is TreeView))
                p = VisualTreeHelper.GetParent(p);
            return p as ItemsControl;
        }
        /// <summary>
        /// Событие при выборе отдела
        /// </summary>
        //public void SelectDepartment()
        //{
        //    if (departments.Count > 0)
        //        departments = ((Department)listDepartment.SelectedItem).Employees;
        //}
    }
}
