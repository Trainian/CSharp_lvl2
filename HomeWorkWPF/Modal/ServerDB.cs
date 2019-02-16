using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace HomeWorkWPF.Modal
{
    public static class ServerDB
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private static SqlCommand _command;
        private static SqlConnection _connection;
        public static SqlDataAdapter AdapterWorkers = new SqlDataAdapter();
        public static SqlDataAdapter AdapterDepartments = new SqlDataAdapter();
        public static DataTable dtWorkers = new DataTable();
        public static DataTable dtDepartments = new DataTable();

        public static void ConnectionToWorkers()
        {
            _connection = new SqlConnection(ConnectionString);
            _command = new SqlCommand("SELECT * FROM Workers LEFT JOIN Departments ON Workers.Department_id = Departments.ID", _connection);
            AdapterWorkers.SelectCommand = _command;
            AdapterWorkers.Fill(dtWorkers);

            //INSERT
            _command = new SqlCommand("INSERT INTO Workers (FIO, BirthDate, Department_id) VALUES (@FIO, @BirthDate, @Department_id); SET @ID = @@IDENTITY", _connection);
            _command.Parameters.Add("@FIO", SqlDbType.Text, 280, "FIO");
            _command.Parameters.Add("@BirthDate", SqlDbType.DateTime, -1, "BirthDate");
            _command.Parameters.Add("@Department_id", SqlDbType.Decimal, -1, "Department_id");
            SqlParameter param = _command.Parameters.Add("@ID", SqlDbType.Decimal, -1, "ID");
            param.Direction = ParameterDirection.Output;
            AdapterWorkers.InsertCommand = _command;

            //DELETE
            _command = new SqlCommand("DELETE FROM Workers WHERE ID = @ID", _connection);
            _command.Parameters.Add("@ID", SqlDbType.Decimal, -1, "ID");
            AdapterWorkers.DeleteCommand = _command;

            //UPDATE
            _command = new SqlCommand("UPDATE Workers SET FIO = @FIO, BirthDate = @BirthDate, Department_id = @Department_id WHERE ID = @ID", _connection);
            _command.Parameters.Add("@FIO", SqlDbType.Text, 200, "FIO");
            _command.Parameters.Add("@BirthDate", SqlDbType.DateTime, -1, "BirthDate");
            _command.Parameters.Add("@Department_id", SqlDbType.Decimal, -1, "Department_id");
            param = _command.Parameters.Add("@ID", SqlDbType.Decimal, -1, "ID");
            param.SourceVersion = DataRowVersion.Original;
            AdapterWorkers.UpdateCommand = _command;
       }

        public static void ConnectionToDepartments()
        {
            _connection = new SqlConnection(ConnectionString);
            _command = new SqlCommand("SELECT * FROM Departments", _connection);
            AdapterDepartments.SelectCommand = _command;
            AdapterDepartments.Fill(dtDepartments);
        }
    }
}
