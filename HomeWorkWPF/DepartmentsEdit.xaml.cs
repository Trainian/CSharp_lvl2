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
    /// Логика взаимодействия для DepartmentsEdit.xaml
    /// </summary>
    public partial class DepartmentsEdit : Window
    {
        private MainWindow _win;
        private Department _department;
        public DepartmentsEdit(MainWindow win, Department department)
        {
            InitializeComponent();
            this._win = win;
            _department = department;
            IdTextBox.Text = department.Id != -1 ? department.Id.ToString() : (Model.ListDepartments.Count + 1).ToString();
            TitleTextBox.Text = department.Title.ToString();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _department.Title = TitleTextBox.Text.ToString();
            _department.Id = Int32.Parse(IdTextBox.Text);
            _win.Update();
            this.Close();
        }
    }
}
