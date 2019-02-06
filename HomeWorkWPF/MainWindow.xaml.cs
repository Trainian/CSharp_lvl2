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
            ListViewName.ItemsSource = Model.ListEmployees;
            ComboBoxName.ItemsSource = Model.ListDepartments;
            Model.CreateModel();
        }

        private void ListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StatusBarChange();
        }

        private void ComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StatusBarChange();
        }

        private void DepartmentButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Department newDepartment = new Department();
            DepartmentsEdit departmentsEdit = new DepartmentsEdit(this,newDepartment,true);
            departmentsEdit.Owner = this;
            departmentsEdit.ShowDialog();
        }

        private void DepartmentButtonChange_Click(object sender, RoutedEventArgs e)
        {
            Department selectedDepartment = (Department)ComboBoxName.SelectedItem;
            DepartmentsEdit departmentsEdit = new DepartmentsEdit(this, selectedDepartment,false);
            departmentsEdit.Owner = this;
            departmentsEdit.ShowDialog();
        }

        private void WorkersButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Employee newEmployee = new Employee();
            WorkersEdit workersEdit = new WorkersEdit(this, newEmployee,true);
            workersEdit.Owner = this;
            workersEdit.ShowDialog();
        }

        private void WorkersButtonChange_Click(object sender, RoutedEventArgs e)
        {
            Employee checkedEmployee = (Employee)ListViewName.SelectedItem;
            WorkersEdit workersEdit = new WorkersEdit(this, checkedEmployee,false);
            workersEdit.Owner = this;
            workersEdit.ShowDialog();
        }

        public void Update()
        {
            ListViewName.Items.Refresh();
            ComboBoxName.Items.Refresh();
        }

        private void StatusBarChange()
        {
            StatusBarWorkerName.Content = $"{ListViewName.SelectedItem}";
            StatusBarWorkerId.Content = $"id - {ComboBoxName.SelectedIndex}";
        }

    }

}
