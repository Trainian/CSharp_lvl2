using System;
using System.Collections.Generic;
using System.Data;
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
using HomeWorkWPF.Modal;

namespace HomeWorkWPF
{
    /// <summary>
    /// Логика взаимодействия для WorkersEdit.xaml
    /// </summary>
    public partial class WorkersEdit : Window
    {
        private DataRow _drv;
        public WorkersEdit(DataRow drv)
        {
            _drv = drv;
            InitializeComponent();
            DepartmentComboBoxName.ItemsSource = ServerDB.dtDepartments.DefaultView;
            FillDataRow();
        }

        private void FillDataRow()
        {
            FIOTextBox.Text = _drv["FIO"].ToString();
            AgeTextBox.Text = $"{_drv["BirthDate"].ToString()}";
            if (!_drv.IsNull("Department_id"))
                DepartmentComboBoxName.SelectedIndex = Int32.Parse(_drv["Department_id"].ToString()) - 1;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _drv["FIO"] = FIOTextBox.Text;
            _drv["BirthDate"] = AgeTextBox.Text;
            _drv["Department_id"] = DepartmentComboBoxName.SelectedIndex + 1;
            DialogResult = true;
        }

        private void Close_Click(object sender, EventArgs e)
        {
            if (DialogResult == null)
            {
                DialogResult = false;
            }
        }
    }
}
