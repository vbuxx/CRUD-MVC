using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstTry.Models;
using System.Data.SqlClient;

namespace FirstTry.Controllers
{
    public class ProductController : Controller
    {
        SqlConnection sqlConnection;

        string connectionString = "Data Source=DESKTOP-PQO8BSH;Initial Catalog=Tokoku;User ID=me;Password=12345678";

        //Read Feature
        public IActionResult Index()
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

            return View(Products);
        }

        //Create Feature
        //a. POST
        public IActionResult Input(Product product)
        {
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
                    Console.WriteLine("-------Input Berhasil-------");
                    sqlConnection.Close();


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                    Console.WriteLine("-------Input Gagal-------");
                }
            }

            return View("Berhasil");
        }


        // GET: Product/Edit/id
        public IActionResult Edit(Product product)
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
                   


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }

            }
            return View();

        }
    }
}
