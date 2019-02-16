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
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Threading;
using HomeWorkWPF.Modal;

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
            Connection().GetAwaiter();
        }

        private async Task Connection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            ServerDB.ConnectionToWorkers();
            ServerDB.ConnectionToDepartments();
            ListViewName.ItemsSource = ServerDB.dtWorkers.DefaultView;
            ComboBoxName.ItemsSource = ServerDB.dtDepartments.DefaultView;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StatusBarChange();
        }

        private void DepartmentButtonAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DepartmentButtonChange_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WorkersButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            DataRow drv = ServerDB.dtWorkers.NewRow();
            WorkersEdit newWorkersEdit = new WorkersEdit(drv);
            newWorkersEdit.ShowDialog();
            if (newWorkersEdit.DialogResult.HasValue && newWorkersEdit.DialogResult.Value)
            {
                ServerDB.dtWorkers.Rows.Add(drv);
                ServerDB.AdapterWorkers.Update(ServerDB.dtWorkers);
            }
        }

        private void WorkersButtonChange_Click(object sender, RoutedEventArgs e)
        {
            DataRowView drv = (DataRowView)ListViewName.SelectedItem;
            drv.BeginEdit();
            WorkersEdit newWorkersEdit = new WorkersEdit(drv.Row);
            newWorkersEdit.ShowDialog();
            if (newWorkersEdit.DialogResult.HasValue && newWorkersEdit.DialogResult.Value)
            {
                drv.EndEdit();
                ServerDB.AdapterWorkers.Update(ServerDB.dtWorkers);
            }
            else
                drv.CancelEdit();
        }

        private void WorkersButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView drv = (DataRowView)ListViewName.SelectedItem;
            drv.Row.Delete();
            ServerDB.AdapterWorkers.Update(ServerDB.dtWorkers);
        }

        private void StatusBarChange()
        {
            DataRowView dr = (DataRowView)ComboBoxName.SelectedItem;
            DataRowView drw = (DataRowView)ListViewName.SelectedItem;
            if (drw != null)
                StatusBarWorkerName.Content = $"{drw["FIO"]}, {drw["BirthDate"]:d}";
            if (dr != null)
                StatusBarWorkerId.Content = $"id: {dr["ID"]} - {dr[1]}";
        }

    }

}
