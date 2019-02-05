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

namespace HomeWorkWPF
{
    /// <summary>
    /// Логика взаимодействия для WorkersEdit.xaml
    /// </summary>
    public partial class WorkersEdit : Window
    {
        private Employee _employee;
        private MainWindow _win;
        public WorkersEdit(MainWindow win,Employee employee)
        {
            InitializeComponent();
            this._win = win;
            this._employee = employee;
            foreach (Department department in Model.ListDepartments)
            {
                DepartmentComboBoxName.Items.Add(department);
            }

            FIOTextBox.Text = employee.FIO;
            AgeTextBox.Text = employee.Age.ToString();
            DepartmentComboBoxName.SelectedIndex = employee.DepartmentId;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _employee.FIO = FIOTextBox.Text;
            _employee.Age = Int32.Parse(AgeTextBox.Text);
            _employee.DepartmentId = DepartmentComboBoxName.SelectedIndex;
            _win.Update();
            this.Close();
        }
    }
}
