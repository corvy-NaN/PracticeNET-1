﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsConstatcs
{
    public class DataAccessLayer
    {
        private SqlConnection conn = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=WinFormContact;Data Source=Lightning");
    

        public void InsertContact(Contacts contact)
        {
            try
            {
                conn.Open();
                string query = @"
                                INSERT INTO Contacts([FirstName], [LastName], [Phone], [Address])
                                VALUES (@FirstName, @LastName, @Phone, @Address)";

                SqlParameter firstName = new SqlParameter();
                firstName.ParameterName = "@FirstName";
                firstName.Value = contact.FirstName;
                firstName.DbType = System.Data.DbType.String;

                SqlParameter lastName = new SqlParameter("@LastName", contact.LastName);
                SqlParameter phone = new SqlParameter("@Phone", contact.Phone);
                SqlParameter address = new SqlParameter("@Address", contact.Address);

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(firstName);
                command.Parameters.Add(lastName);
                command.Parameters.Add(phone);
                command.Parameters.Add(address);

                command.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
        }

        public void UpdateContact(Contacts contact)
        {
            try
            {
                conn.Open();
                string query = @"UPDATE Contacts
                                 SET FirstName = @FirstName, LastName = @LastName, Phone = @Phone, Address = @Address
                                WHERE Id = @Id";

                SqlParameter id = new SqlParameter("@Id", contact.Id);
                SqlParameter FirstName = new SqlParameter("@FirstName", contact.FirstName);
                SqlParameter lastName = new SqlParameter("@LastName", contact.LastName);
                SqlParameter phone = new SqlParameter("@Phone", contact.Phone);
                SqlParameter address = new SqlParameter("@Address", contact.Address);

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(id);
                command.Parameters.Add(FirstName);
                command.Parameters.Add(lastName);
                command.Parameters.Add(phone);
                command.Parameters.Add(address);

                command.ExecuteNonQuery();
            }
            catch(Exception)
            {
                throw;
            }
            finally { conn.Close(); }

        }

        public void DeleteContact(int id)
        {
            try
            {
                conn.Open();
                string query = @"DELETE FROM Contacts WHERE id  = @id";

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(new SqlParameter("@Id", id));
                command.ExecuteNonQuery();

            }
            catch
            {
                throw;
            }
            finally { conn.Close(); }
        }

        public List<Contacts> GetContacts(string search = null)
        {
            List<Contacts> contacts = new List<Contacts>();
            try
            {
                conn.Open();
                string query = @" SELECT Id, FirstName, LastName, Phone, Address
                                FROM Contacts ";

                SqlCommand command = new SqlCommand();

                if (!string.IsNullOrEmpty(search))
                {
                    query +=    @"WHERE FirstName LIKE @Search OR 
                                LastName LIKE @Search OR 
                                Phone LIKE @Search OR 
                                Address LIKE @Search";
                    command.Parameters.Add(new SqlParameter("@Search", $"%{search}%"));
                }

                command.CommandText = query;
                command.Connection = conn;
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    contacts.Add(new Contacts
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString()
                    });
                }
            }
            catch (Exception) 
            {
                throw;
            }
            finally { conn.Close(); }
            return contacts;
        }
    }
}
