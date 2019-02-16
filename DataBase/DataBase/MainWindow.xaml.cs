using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Reflection;
using System.Configuration;

namespace DataBase
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlDataAdapter adapterPeople = new SqlDataAdapter();
        private SqlDataAdapter adapterDepartment = new SqlDataAdapter();
        private DataTable dtPeople = new DataTable();
        private DataTable dtDepartment = new DataTable();
        private DataSet ds = new DataSet();
        private SqlConnection connection;

        
        public MainWindow()
        {
            InitializeComponent();
            ConnectToPeoplesDb();
            ConnectToDepartmentsDb();

            dtDepartment.TableName = "dtDepartment";
            dtPeople.TableName = "dtPeople";
            ds.Tables.AddRange(new DataTable[] { dtDepartment, dtPeople });

            //dtDepartment.Constraints.Add(new ForeignKeyConstraint(dtPeople.Columns["Department_id"],
            //    dtDepartment.Columns["ID"]));
        }

        private void ConnectToPeoplesDb()
        {
            //Создаем и кофигурирем подключение
            string connectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("SELECT * FROM Workers", connection);

            //Создаем и конфиригурирем объединения (средний слой)
            adapterPeople.SelectCommand = command;

            //Создаем и конфигурирем слой взаиодействия с WPF (третий слой)
            adapterPeople.Fill(dtPeople);

            //Связываем DataGrid с DataTable (предварительно добавив в XAML DataGrid'a; "ItemSource="{Binding}")
            DataGridBD.DataContext = dtPeople.DefaultView;

            //INSERT
            command = new SqlCommand(@"INSERT INTO People (FIO, BirthDate) VALUES (@FIO, @BirthDate); SET @ID = @@IDENTITY;", connection);
            command.Parameters.Add("@FIO", SqlDbType.NVarChar, -1, "FIO");
            command.Parameters.Add("@Birthday", SqlDbType.NVarChar, -1, "BirthDate");
            SqlParameter param = command.Parameters.Add("@ID", SqlDbType.Decimal, 0, "ID");
            param.Direction = ParameterDirection.Output;
            adapterPeople.InsertCommand = command;

            //DELETE
            command = new SqlCommand("DELETE FROM Workers WHERE ID = @ID", connection);
            param = command.Parameters.Add("@ID", SqlDbType.Decimal, -1, "ID");
            param.SourceVersion = DataRowVersion.Original;
            adapterPeople.DeleteCommand = command;

            //UPDATE
            command = new SqlCommand("UPDATE Workers SET FIO = 'New' WHERE ID = @ID");
            param = command.Parameters.Add("@ID", SqlDbType.Decimal, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            adapterPeople.UpdateCommand = command;
        }

        private void ConnectToDepartmentsDb()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand("SELECT * FROM Departments", connection);
            adapterDepartment.SelectCommand = command;
            adapterDepartment.Fill(dtDepartment);
            ComboBoxBD.ItemsSource = dtDepartment.DefaultView;
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView newRowView = (DataRowView)DataGridBD.SelectedItem;
            newRowView.Row.Delete();
            adapterPeople.Update(dtPeople);
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView newRow = (DataRowView)DataGridBD.SelectedItem;
            newRow.BeginEdit();
            EditWindow editWindow = new EditWindow(newRow.Row);
            editWindow.ShowDialog();
            if (editWindow.DialogResult.HasValue && editWindow.DialogResult.Value)
            {
                newRow.EndEdit();
                adapterPeople.Update(dtPeople);
            }
            else
            {
                newRow.CancelEdit();
            }
        }
    }
}
