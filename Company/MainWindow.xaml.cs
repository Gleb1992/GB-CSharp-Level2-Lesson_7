using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace CompanyApp
{

    public class Node
    {
        public string Name { get; set; }
        public ObservableCollection<Node> Nodes { get; set; }
    }
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Урок 7. Знакомство с технологией WPF. 
        //Изменить WPF-приложение для ведения списка сотрудников компании (из урока №5), используя связывание данных, ListView, ObservableCollection и INotifyPropertyChanged.
        //1. Создать сущности Employee и Department и заполните списки сущностей начальными данными.
        //2. Для списка сотрудников и списка департаментов предусмотреть визуализацию (отображение). Это можно сделать, например, с использованием ComboBox или ListView.
        //3. Предусмотреть возможность редактирования сотрудников и департаментов. Должна быть возможность изменить департамент у сотрудника.Список департаментов для выбора, можно выводить в ComboBox, это все можно выводить на дополнительной форме.
        //4. Предусмотреть возможность создания новых сотрудников и департаментов.Реализовать данную возможность либо на форме редактирования, либо сделать новую форму.

        Company company;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = company = new Company();
            treeView.SelectedItemChanged += (a, arg) =>
            {
                if (treeView.SelectedItem is Department)
                {
                    company.ViewDepartment = (Department)treeView.SelectedItem;
                    company.Employee = company.ViewDepartment.Employees;
                    company.ViewEmployee = company.ViewDepartment.Employees[0];
                }
                if (treeView.SelectedItem is Employee)                
                    company.ViewEmployee = (Employee)treeView.SelectedItem;                
            };
        }

        private void TreeViewItem_OnItemSelected(object sender, RoutedEventArgs e) => company.TreeViewItem_OnItemSelected(sender, e);

        public void DeleteDepartment(object sender, RoutedEventArgs e) => company.DeleteDepartment();

        public void AddDepartment(object sender, RoutedEventArgs e) => company.AddDepartment();

        public void DeleteEpmloyee(object sender, RoutedEventArgs e) => company.DeleteEpmloyee();

        public void AddEpmloyee(object sender, RoutedEventArgs e) => company.AddEpmloyee();

        private void WindowEmp(object sender, RoutedEventArgs e)
        {
            if (company.ViewEmployee != null && company.ViewDepartment != null)
                (new WindowEmployee(company.ViewEmployee, company)).Show();
        }

        private void WindowDep(object sender, RoutedEventArgs e)
        {
            if (company.ViewDepartment != null)
                (new WindowDepartment(company.ViewDepartment)).Show();
        }
    }
}
