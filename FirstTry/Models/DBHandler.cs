using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace FirstTry.Models
{
    public class DBHandler
    {
        SqlConnection sqlConnection;

        string connectionString = "Data Source=DESKTOP-PQO8BSH;Initial Catalog=Tokoku;User ID=me;Password=12345678";

        public List<Product> GetAll()
        {
            string query = "SELECT * FROM Products";

            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            List<Product> Products = new List<Product>();

            try
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Product product = new Product();
                            product.Id = Convert.ToInt32(sqlDataReader[0]);
                            product.Name = sqlDataReader[1].ToString();
                            product.Stock = Convert.ToInt32(sqlDataReader[2]);
                            product.Price = Convert.ToInt32(sqlDataReader[3]);
                            Products.Add(product);
                        }
                        sqlDataReader.Close();
                    }
                    else
                    {
                        Console.WriteLine("No Data Rows");
                    }
                    sqlDataReader.Close();
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }

            return Products;
        }

        /*
        void GetById(int id)
        {
            string query = "SELECT * FROM Products WHERE Id = @id";

            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@id";
            sqlParameter.Value = id;

            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.Add(sqlParameter);
            try
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Console.WriteLine(sqlDataReader[0] + " - " + sqlDataReader[1] + " - " + sqlDataReader[2] + " - " + sqlDataReader[3]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Rows");
                    }
                    sqlDataReader.Close();
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }
        */
        public bool Insert(Product product)
        {
            Program program = new Program();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter name = new SqlParameter("@name", product.Name);
                sqlCommand.Parameters.Add(name);

                SqlParameter stock = new SqlParameter("@stock", product.Stock);
                sqlCommand.Parameters.Add(stock);

                SqlParameter price = new SqlParameter("@price", product.Price);
                sqlCommand.Parameters.Add(price);

                try
                {
                    sqlCommand.CommandText = "INSERT INTO Products " +
                        " VALUES (@name,@stock,@price)";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    sqlConnection.Close();
                    Console.WriteLine("-------Insert Berhasil-------");
                    return true;


                }
                catch (Exception ex)
                {
                    Console.WriteLine("-------Insert Gagal-------");
                    Console.WriteLine(ex.InnerException);
                    return false;
                }
            }
        }

        // Update
        public bool Update(Product product)
        {
    
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter id = new SqlParameter("@id", product.Id);
                sqlCommand.Parameters.Add(id);

                SqlParameter name = new SqlParameter("@name", product.Name);
                sqlCommand.Parameters.Add(name);

                SqlParameter stock = new SqlParameter("@stock", product.Stock);
                sqlCommand.Parameters.Add(stock);

                SqlParameter price = new SqlParameter("@price", product.Price);
                sqlCommand.Parameters.Add(price);

                try
                {
                    sqlCommand.CommandText = "UPDATE Products " +
                        "SET Name = @name, Stock = @stock, Price = @price " + " WHERE Id = @id; ";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    sqlConnection.Close();
                    Console.WriteLine("-------Update Berhasil-------");
                    return true;


                }
                catch (Exception ex)
                {
                    Console.WriteLine("-------Update Gagal-------");
                    Console.WriteLine(ex.InnerException);
                    return false;
                }

            }
        }
        // Delete
        public bool Delete(int productId)
        {
            Program program = new Program();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter id = new SqlParameter("@id", productId);
                sqlCommand.Parameters.Add(id);


                try
                {
                    sqlCommand.CommandText = "DELETE FROM dbo.Products WHERE Id = @id; ";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    sqlConnection.Close();
                    Console.WriteLine("-------Delete Berhasil-------");
                    return true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("-------Delete Gagal-------");
                    Console.WriteLine(ex.InnerException);
                    return false;
                }

            }
        }
    }
}
