using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MyContacts
{
    internal class ContactsRepository : IContactsRepository
    {
        private string ConnectionString = "Data Source=.;Initial Catalog=Contact_DB;Integrated Security=True;";

 

        public bool Insert(string name, string family, string mobile, int age, string email, string address)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                string query = "Insert Into MyContacts(Name, Family,Mobile, Age, Email,Address) values(@Name, @Family ,@Mobile, @Age, @Email,@Address)";
                SqlCommand command = new SqlCommand(query,connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Family", family);
                command.Parameters.AddWithValue("@Mobile", mobile);
                command.Parameters.AddWithValue("@Age", age);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Address", address);

                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }



            finally
            {
                connection.Close();
            }
        }

        public DataTable SelectAll()
        {
            string query = "SELECT * FROM MyContacts";
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);
            return data;

        }

        public DataTable SelectRow(int contactId)
        {
            string query = "SELECT * FROM MyContacts where ContactID="+contactId;
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);
            return data;
        }

        public bool Update(int contactId, string name, string family, string mobile, int age, string email, string address)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                string query = "Update  MyContacts set Name=@Name,Family=@Family,Mobile=@Mobile,Age=@Age,Email=@Email,Address=@Address where ContactID = @ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", contactId);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Family", family);
                command.Parameters.AddWithValue("@Mobile", mobile);
                command.Parameters.AddWithValue("@Age", age);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Address", address);

                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch 
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
        public bool Delete(int contactId)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                string query = "Delete FROM MyContacts where ContactID = @ID";
                SqlCommand command = new SqlCommand(query,connection);
                command.Parameters.AddWithValue("@ID", contactId);
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public DataTable Search(string parameter)
        {
            string query = "SELECT * FROM MyContacts where Name like @parameter or Family like @parameter";
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            adapter.SelectCommand.Parameters.AddWithValue("@parameter", "%" + parameter+ "%");
            DataTable data = new DataTable();
            adapter.Fill(data);
            return data;
        }
    }
}
