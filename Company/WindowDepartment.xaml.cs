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
    /// Логика взаимодействия для WindowDepartment.xaml
    /// </summary>
    public partial class WindowDepartment : Window
    {
        Department department;
        public WindowDepartment(Department department)
        {
            InitializeComponent();
            this.department = department;
            DataContext = department;
            dep_Profit.KeyDown += new KeyEventHandler(NumericTextBox_KeyDown);
            this.Topmost = true;
            this.Activate();
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
    }
}
