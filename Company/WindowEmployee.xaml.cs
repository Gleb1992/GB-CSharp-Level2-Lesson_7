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
using System.Windows.Shapes;

namespace CompanyApp
{
    /// <summary>
    /// Логика взаимодействия для WindowEmployee.xaml
    /// </summary>
    public partial class WindowEmployee : Window
    {
        Employee employee;
        Company company;

        public WindowEmployee(Employee employee, Company company)
        {
            InitializeComponent();
            this.employee = employee;
            this.company = company;
            DataContext = employee;

            dep.ItemsSource = company.Departments;
            dep.SelectedItem = company.ViewDepartment;

            tb_Name.KeyDown += new KeyEventHandler(LetterTextBox_KeyDown);
            tb_Age.KeyDown += new KeyEventHandler(NumericTextBox_KeyDown);
            tb_Salary.KeyDown += new KeyEventHandler(NumericTextBox_KeyDown);
            //this.Closed += delegate { listView.Items.Refresh(); };
            this.Topmost = true;
            this.Activate();
        }

        /// <summary>
        /// Изменить отдел дял сотркудника
        /// </summary>
        private void Dep_SelectChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((Department)dep.SelectedItem != company.ViewDepartment)
            {
                ((Department)dep.SelectedItem).Employees.Add((Employee)employee);
                company.ViewDepartment.Employees.Remove((Employee)employee);
                company.ViewDepartment = (Department)dep.SelectedItem;
            }
        }

        /// <summary>
        /// Пропускать символы кроме чисел
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumericTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) && 
                e.Key != Key.Back || e.Key == Key.Space)
                e.Handled = true;
        }

        /// <summary>
        /// Пропускать символы кроме букв
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LetterTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)))
                e.Handled = true;
        }
    }
}
