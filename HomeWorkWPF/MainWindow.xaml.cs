using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace HomeWorkWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Model.CreateModel();
            Update();
        }

        private void ListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DepartmentButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Department newDepartment = new Department();
            DepartmentsEdit departmentsEdit = new DepartmentsEdit(this,newDepartment);
            departmentsEdit.Owner = this;
            Model.ListDepartments.Add(newDepartment);
            departmentsEdit.ShowDialog();
        }

        private void DepartmentButtonChange_Click(object sender, RoutedEventArgs e)
        {
            Department selectedDepartment = (Department)ComboBoxName.SelectedItem;
            DepartmentsEdit departmentsEdit = new DepartmentsEdit(this, selectedDepartment);
            departmentsEdit.Owner = this;
            departmentsEdit.ShowDialog();
        }

        private void WorkersButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Employee newEmployee = new Employee();
            WorkersEdit workersEdit = new WorkersEdit(this, newEmployee);
            workersEdit.Owner = this;
            Model.ListEmployees.Add(newEmployee);
            workersEdit.ShowDialog();
        }

        private void WorkersButtonChange_Click(object sender, RoutedEventArgs e)
        {
            Employee checkedEmployee = (Employee)ListViewName.SelectedItem;
            WorkersEdit workersEdit = new WorkersEdit(this, checkedEmployee);
            workersEdit.Owner = this;
            workersEdit.ShowDialog();
        }

        public void Update()
        {
            ListViewName.Items.Clear();
            ComboBoxName.Items.Clear();
            foreach (Employee employee in Model.ListEmployees)
            {
                ListViewName.Items.Add(employee);
            }
            ListViewName.SelectedIndex = 0;

            foreach (Department department in Model.ListDepartments)
            {
                ComboBoxName.Items.Add(department);
            }
            ComboBoxName.SelectedIndex = 0;
        }
    }

}
