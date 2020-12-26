using MySql.Data.MySqlClient;
using ProductService.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductService.Database
{
    public class DBConnection
    {

        string serverIp = "localhost";
        string username = "root";
        string password = "suzad";
        string databaseName = "kids_shop";

        MySqlConnection connection;
        MySqlCommand mySqlCommand;

        public DBConnection()
        {
            string dbConnectionString = string.Format("server={0};uid={1};pwd={2};database={3};", serverIp, username, password, databaseName);
            this.connection = new MySqlConnection(dbConnectionString);
            
        }

        public String getConnectionStatus()
        {
            String db = "Database Name : " + this.connection.Database;
            return db;
        }

        public List<Product> getProduct()
        {
            connection.Open();
            List<Product> ProductList = new List<Product>();

            String query = "Select * from product";
            mySqlCommand = new MySqlCommand(query, this.connection);
            var reader = mySqlCommand.ExecuteReader();

            while (reader.Read())
            {
                try
                {
                    Product product = new Product();
                    product.id = Guid.Parse((reader["id"]).ToString());
                    product.name = reader["name"].ToString();
                    product.categoryId = Convert.ToInt32(reader["categoryId"]);
                    product.categoryName = reader["categoryName"].ToString();
                    product.averageRating = (float)reader["averageRating"];
                    product.numberOfRaters = Convert.ToInt32(reader["numberOfRaters"]);

                    ProductList.Add(product);
                }
                catch(Exception e)
                {

                }
            }
            connection.Close();
            return ProductList;
        }

        public bool isDuplicate(string name, int categoryId)
        {
            connection.Open();
            int count =0 ;
            string query = "select count(*) from product where name='"+name+"'";

            MySqlCommand mySqlCommand = new MySqlCommand(query, this.connection);
            
            var reader = mySqlCommand.ExecuteReader();

            while (reader.Read())
            {
                
                Product product = new Product();
                count = Convert.ToInt32(reader["count(*)"]);
               
            }
            connection.Close();
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool AddProduct(string name, int categoryId)
        {
            connection.Open();
            Guid guid = Guid.NewGuid();
            string query = "INSERT INTO product(name,categoryId,id,averageRating,categoryName,numberOfRaters) VALUES(@name, @categoryId,@id,@averageRating,@categoryName,@numberOfRaters)";

            MySqlCommand mySqlCommand = new MySqlCommand(query, this.connection);
            mySqlCommand.Parameters.AddWithValue("@name", name);
            mySqlCommand.Parameters.AddWithValue("@categoryId", categoryId);
            mySqlCommand.Parameters.AddWithValue("@id", guid);
            mySqlCommand.Parameters.AddWithValue("@averageRating", 0);
            mySqlCommand.Parameters.AddWithValue("@categoryName", "null");
            mySqlCommand.Parameters.AddWithValue("@numberOfRaters", 0);
            
            try
            {
                mySqlCommand.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("DatabaseName Error : " + e);
                connection.Close();
                return false;
            }
            
        }

       
    public void UpdateProduct(Guid id, int categoryId, string categoryName)
        {
            string query = "UPDATE product SET categoryId = @categoryId, categoryName = @categoryName WHERE id = @id ";

            MySqlCommand mySqlCommand = new MySqlCommand(query, this.connection);
            mySqlCommand.Parameters.AddWithValue("@categoryName", categoryName);
            mySqlCommand.Parameters.AddWithValue("@categoryId", categoryId);
            mySqlCommand.Parameters.AddWithValue("@id", id);
            try
            {
                mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("DatabaseName Error : " + e);
            }
        }

        public void DeleteProduct(Guid id)
        {
            string query = "DELETE FROM product WHERE id = @id ";

            MySqlCommand mySqlCommand = new MySqlCommand(query, this.connection);
            mySqlCommand.Parameters.AddWithValue("@id", id);

            try
            {
                mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("DatabaseName Error : " + e);
            }
        }

        public void RatingUpdate(Rating rating)
        {
            Guid id = Guid.Parse(rating.product_id);
            float average = rating.average;
            int noRater = rating.count;

            string query = "Update product set id=@id, averageRating=@average, numberOfRaters=@noRater where id=@id";
            connection.Open();
            MySqlCommand mySqlCommand = new MySqlCommand(query, this.connection);
            mySqlCommand.Parameters.AddWithValue("@id", id);
            mySqlCommand.Parameters.AddWithValue("@average", average);
            mySqlCommand.Parameters.AddWithValue("@noRater", noRater);
            

            try
            {
                mySqlCommand.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("DatabaseName Error : " + e);
                connection.Close();
            }
        }
    }
}
