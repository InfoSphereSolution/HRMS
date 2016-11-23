using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace DAL
{
  public  class SQLHelp
    {
      static string CS = ConfigurationManager.ConnectionStrings["HRMS"].ConnectionString;

   //   static string _sqlConnection = string.Empty;
      static SqlConnection _sqlConnection;

         static SQLHelp()
        {
            _sqlConnection = new SqlConnection(CS);            
        }


        static SqlCommand CreateCollections(List<SqlParameter> parameters, SqlCommand cmd)
        {
            if (parameters == null)
            {
                return cmd;
            }
            else
            {
                foreach (SqlParameter param in parameters)

                    cmd.Parameters.Add(param);
                return cmd;
            }
        }

        public static DataTable ExecuteReader(string Procedure_Name, List<SqlParameter> parameters)
        {
            DataTable data = null;
            try
            {

                SqlCommand cmd = new SqlCommand(Procedure_Name, _sqlConnection);
                cmd = CreateCollections(parameters, cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                if (_sqlConnection.State != ConnectionState.Open)
                {
                    _sqlConnection.Open();
                }
                DataSet dataset = new DataSet("ds");
                adapter.Fill(dataset);
                _sqlConnection.Close();
                return dataset.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return data;
        }

        public static DataTable ExecuteSelect(string query)
        {
            DataTable data = null;
            try
            {

                SqlCommand cmd = new SqlCommand(query, _sqlConnection);
                
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dataset = new DataSet("ds");
                adapter.Fill(dataset);
                return dataset.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return data;
        }


        public static object ExecuteScalarValue(string query)
        {
            object objValue=null;
            try
            {
                _sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, _sqlConnection);
               objValue = cmd.ExecuteScalar();              
                return objValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objValue;
        }

        public static int ExecuteNonQuery(string Procedure_Name, List<SqlParameter> parameters)
        {
            int rowsAffected=0;           
            try
            {
                SqlCommand cmd = new SqlCommand(Procedure_Name, _sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd = CreateCollections(parameters, cmd);
                _sqlConnection.Open();
                rowsAffected= cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

            return rowsAffected;
        }


        public static DataSet DSExecuteQuery1(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {

            using (var command = new SqlCommand(commandText, _sqlConnection))
            {
                DataSet ds = new DataSet();
                command.CommandType = commandType;
                command.Parameters.AddRange(parameters);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                _sqlConnection.Close();
                return ds;
            }
        }



        public static DataSet DSExecuteQuery(string commandText, List<SqlParameter> parameters)
        {

            try
            {

                SqlCommand cmd = new SqlCommand(commandText, _sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
       
                if (parameters != null)
                {
                    cmd = CreateCollections(parameters, cmd);

                }
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                _sqlConnection.Close();
                return ds;
            }

            catch (Exception ex)
            {
                throw ex;
            }



        }

        public static void ExecuteNonQuery2(string destinationtable, DataTable dt)
        {

            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(CS))
            {

                sqlBulkCopy.DestinationTableName = "Tbl_Document_Details";

                _sqlConnection.Open();
                sqlBulkCopy.WriteToServer(dt);
                _sqlConnection.Close();
                
            }
        }
        public static bool CopyToServer(string destinationTable, DataTable dtToCopy)
        {
            try
            {

                _sqlConnection.Open();
                using (SqlBulkCopy bulkCopy =
                          new SqlBulkCopy(_sqlConnection))
                {
                    bulkCopy.DestinationTableName = destinationTable;
                    // Write from the source to the destination.
                    bulkCopy.WriteToServer(dtToCopy);
                    _sqlConnection.Close();
                }

            }
            catch
            {

                return false;
            }


            return true;
        }


        public static int ExecuteTransaction(string Procedure_Name, List<SqlParameter> parameters, string transactionStatus)
        {
            int rowsAffected = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(Procedure_Name, _sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd = CreateCollections(parameters, cmd);

                if (transactionStatus.Equals("Begin"))
                {

                    cmd.Connection.Close();
                    _sqlConnection.Open();
                }
                rowsAffected = cmd.ExecuteNonQuery();

                if (transactionStatus.Equals("End"))
                {
                    cmd.Connection.Close();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return rowsAffected;
        }

        //Simple tranjection
        public static int ExecuteSingleTransaction(string Procedure_Name, List<SqlParameter> parameters, string transactionStatus)
        {
            int rowsAffected = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(Procedure_Name, _sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd = CreateCollections(parameters, cmd);

                if (transactionStatus.Equals("Begin"))
                {

                    cmd.Connection.Close();
                    _sqlConnection.Open();
                }
                rowsAffected = cmd.ExecuteNonQuery();



            }
            catch (Exception ex)
            {

                throw ex;
            }

            return rowsAffected;
        }


    }
}
