﻿using Microsoft.Data.SqlClient;
using WCO_Api.WEBModels;

namespace WCO_Api.Database
{
    /// <summary>
    /// Class <c>AccountDatabase</c> propiedad AccountDatabase que realiza las operaciones
    /// asociadas a Account con WCO DB.
    /// </summary>
    public class AccountDatabase
    {
        string CONNECTION_STRING;

        public AccountDatabase()
        {
            CONNECTION_STRING = "Data Source=localhost;Initial Catalog=WCODB;Integrated Security=True";
        }

        /// <summary>
        /// Method <c>insertAccount</c> método que se comunica con WCO DB para realizar la inserción 
        /// de una nueva cuenta.
        /// </summary>
        public async Task<int> insertAccount(AccountWEB account)
        {
           
            SqlTransaction transaction = null;
            SqlConnection myConnection = null;
            SqlCommand command = null;

            try
            {

                myConnection = new SqlConnection(CONNECTION_STRING);

                myConnection.Open();

                //Start the transaction
                transaction = myConnection.BeginTransaction();

                string query =
                          $"INSERT INTO [dbo].[Account] ([nickname], [email], [name], [lastName], [birthdate], [country], [password], [isAdmin])" +
                          $"VALUES ('{account.nickname}', '{account.email}', '{account.name}', '{account.lastname}', '{account.birthdate}', " +
                          $"'{account.country}', '{account.password}', '{account.isAdmin}');";

                command = new SqlCommand(query, myConnection);

                //assosiate the command-variable with the transaction
                command.Transaction = transaction;
                //Se inserta a la tabla torneos el torneo en sí
                command.ExecuteNonQuery();

                transaction.Commit();

                return 1;
            }
            catch (Exception error)
            {
                transaction.Rollback();
                Console.WriteLine(error);
                return -1;
            }
            finally
            {
                myConnection.Close();
            }

        }

        public async Task<AccountWEB?> getAccountByEmail(string inputEmail)
        {
            SqlConnection myConnection = new();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = $"SELECT * " +
                $"FROM [dbo].[Account]" +
                $"WHERE email = '{inputEmail}';";

            SqlCommand sqlCmd = new(query, myConnection);

            myConnection.Open();

            //SqlDataReader reader = sqlCmd.ExecuteReader();
            AccountWEB? fromResponse = new();

            using (SqlDataReader reader = sqlCmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    fromResponse.password = reader.GetValue(6).ToString();
                    fromResponse.email = reader.GetValue(0).ToString();
                }
                else
                {
                    fromResponse = null;
                }
            }
            myConnection.Close();

            return fromResponse;

        }

        public async Task<List<AccountWEB?>> getAccountByNickname(string nick)
        {
            SqlConnection myConnection = new();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = $"SELECT * " +
                $"FROM [dbo].[Account]" +
                $"WHERE nickname = '{nick}';";

            SqlCommand sqlCmd = new(query, myConnection);

            myConnection.Open();

            //SqlDataReader reader = sqlCmd.ExecuteReader();

            List<AccountWEB> accountL = new();

            using (SqlDataReader reader = sqlCmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    AccountWEB account = new();

                    account.nickname = reader.GetValue(0).ToString();
                    account.email = reader.GetValue(1).ToString();
                    account.name = reader.GetValue(2).ToString();
                    account.lastname = reader.GetValue(3).ToString();
                    account.birthdate = reader.GetValue(4).ToString();
                    account.country = reader.GetValue(5).ToString();
                    account.password = reader.GetValue(6).ToString();

                    accountL.Add(account);
                }

            }
            myConnection.Close();

            return accountL;
        }

        public async Task<List<AccountWEB>> getInformationAccountByEmail(string email)
        {
            SqlConnection myConnection = new();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = $"SELECT * " +
                $"FROM [dbo].[Account]" +
                $"WHERE email = '{email}';";

            SqlCommand sqlCmd = new(query, myConnection);

            myConnection.Open();

            //SqlDataReader reader = sqlCmd.ExecuteReader();

            List<AccountWEB> accountL = new();

            using (SqlDataReader reader = sqlCmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    AccountWEB account = new();

                    account.nickname = reader.GetValue(0).ToString();
                    account.email = reader.GetValue(1).ToString();
                    account.name = reader.GetValue(2).ToString();
                    account.lastname = reader.GetValue(3).ToString();
                    account.birthdate = reader.GetValue(4).ToString();
                    account.country = reader.GetValue(5).ToString();
                    account.password = reader.GetValue(6).ToString();

                    accountL.Add(account);
                }

            }
            myConnection.Close();

            return accountL;
        }

        public async Task<bool> getRoleAccountByEmail(string email)
        {
            SqlConnection myConnection = new();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = $"SELECT [isAdmin] " +
                $"FROM [dbo].[Account]" +
                $"WHERE email = '{email}';";

            SqlCommand sqlCmd = new(query, myConnection);

            myConnection.Open();

            //SqlDataReader reader = sqlCmd.ExecuteReader();

            bool accIsAdmin = false;

            using (SqlDataReader reader = sqlCmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    accIsAdmin = (bool)reader.GetValue(0);

                }
                
            }

            myConnection.Close();

            return accIsAdmin;

        }
    }
}
