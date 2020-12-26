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
            connection.Open();
        }

        public String getConnectionStatus()
        {
            String db = "Database Name : " + this.connection.Database;
            return db;
        }

        public List<Product> getProduct()
        {
            List<Product> ProductList = new List<Product>();

            String query = "Select * from product";
            mySqlCommand = new MySqlCommand(query, this.connection);
            var reader = mySqlCommand.ExecuteReader();

            while (reader.Read())
            {
                //try
                //{
                    Product product = new Product();
                    product.id = Convert.ToInt32(reader["id"]);
                    product.name = reader["name"].ToString();
                    product.categoryId = Convert.ToInt32(reader["categoryId"]);
                    product.categoryName = reader["categoryName"].ToString();
                    product.averageRating = (float)reader["averageRating"];
                    product.numberOfRaters = Convert.ToInt32(reader["numberOfRaters"]);

                    ProductList.Add(product);
                //}
                //catch(Exception e)
                //{

//                }
            }
            return ProductList;
        }

        public System.Web.Mvc.ActionResult StatusCoidng(string name, int categoryId)
        {
            int count = 0;
            //int dup=0;
            string query = "select count(*) from product where name='"+name+"'";

            MySqlCommand mySqlCommand = new MySqlCommand(query, this.connection);
            //mySqlCommand.Parameters.AddWithValue("@name", name);
            //mySqlCommand.Parameters.("@dup", dup);

            var reader = mySqlCommand.ExecuteReader();

            while (reader.Read())
            {
                //try
                //{
                Product product = new Product();
                count = Convert.ToInt32(reader["count(*)"]);
                //}
                //catch(Exception e)
                //{

                //                }
            }

            if (count > 0)
            {
                //return 400;
                //StatusCodeResult statusCodeResult=new StatusCodeResult(400);
                //return statusCodeResult.ExecuteResult();
                //int statusCode = 400;
                //return new HttpStatusCodeResult(statusCode);
                return new HttpStatusCodeResult((HttpStatusCode)400, "Bad Request");
            }
            else if (count == 0)
            {
                AddProduct(name, categoryId);
                //return StatusCode(200);
                //return 0;
                return new HttpStatusCodeResult((HttpStatusCode)201, "success");
            }
            else
            {
                return new HttpStatusCodeResult((HttpStatusCode)500, "Problem");
            }
            
        }

        public void AddProduct(string name, int categoryId)
        {
            //Product product = new Product();
            string query = "INSERT INTO product(name,categoryId) VALUES(@name, @categoryId)";

            MySqlCommand mySqlCommand = new MySqlCommand(query, this.connection);
            mySqlCommand.Parameters.AddWithValue("@name", name);
            mySqlCommand.Parameters.AddWithValue("@categoryId", categoryId);
            //mySqlCommand.Parameters.AddWithValue("@CreditHour", product.CreditHour);
            //mySqlCommand.Parameters.AddWithValue("@CourseTeacher", product.CourseTeacher);
            //mySqlCommand.Parameters.AddWithValue("@GuestTeacher", product.GuestTeacher);
            

            try
            {
                mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("DatabaseName Error : " + e);
                //return "DatabaseName Error : " + e;
            }
            //return "Course Successfully Added with Course Code: " + course.CourseId;
        }

        /*public ActionResult StatusFunc(Product product, string name)
        {
            /*if (product.name != null)
            {
                return new HttpStatusCodeResult((HttpStatusCode)500,
               "Error");
            }
            return new HttpStatusCodeResult((HttpStatusCode)201,
               "Successful"); 
            string query = "SELECT COUNT(NAME) FROM product WHERE name=@name";
            MySqlCommand mySqlCommand = new MySqlCommand(query, this.connection);
            mySqlCommand.Parameters.AddWithValue("@name", name);
        }*/


    public void UpdateProduct(int id, int categoryId, string categoryName)
        {
            string query = "UPDATE product SET categoryId = @categoryId, categoryName = @categoryName WHERE id = @id ";

            MySqlCommand mySqlCommand = new MySqlCommand(query, this.connection);
            mySqlCommand.Parameters.AddWithValue("@categoryName", categoryName);
            mySqlCommand.Parameters.AddWithValue("@categoryId", categoryId);
            mySqlCommand.Parameters.AddWithValue("@id", id);
            //mySqlCommand.Parameters.AddWithValue("@CreditHour", product.CreditHour);
            //mySqlCommand.Parameters.AddWithValue("@CourseTeacher", product.CourseTeacher);
            //mySqlCommand.Parameters.AddWithValue("@GuestTeacher", product.GuestTeacher);

            try
            {
                mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("DatabaseName Error : " + e);
                //return "DatabaseName Error : " + e;
            }
            //return "Course Successfully Added with Course Code: " + course.CourseId;
        }

        public void DeleteProduct(int id)
        {
            string query = "DELETE FROM product WHERE id = @id ";

            MySqlCommand mySqlCommand = new MySqlCommand(query, this.connection);
            //mySqlCommand.Parameters.AddWithValue("@categoryName", categoryName);
            //mySqlCommand.Parameters.AddWithValue("@categoryId", categoryId);
            mySqlCommand.Parameters.AddWithValue("@id", id);
            //mySqlCommand.Parameters.AddWithValue("@CreditHour", product.CreditHour);
            //mySqlCommand.Parameters.AddWithValue("@CourseTeacher", product.CourseTeacher);
            //mySqlCommand.Parameters.AddWithValue("@GuestTeacher", product.GuestTeacher);

            try
            {
                mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("DatabaseName Error : " + e);
                //return "DatabaseName Error : " + e;
            }
            //return "Course Successfully Added with Course Code: " + course.CourseId;
        }
    }
}
