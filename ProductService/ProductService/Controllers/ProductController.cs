using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductService.Database;
using ProductService.Database.Entity;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductService.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // GET: api/<ProductController>
        [HttpGet]
        //[ResponseType(typeof(List<Product>))]
        [Route("/[controller]/list")]
        public List<Product> Get()
        {
            //return new string[] { "value1", "value2" };
            DBConnection connection = new DBConnection();
            //return connection.getConnectionStatus();
            return connection.getProduct();
            //return new HttpResponseMessage(HttpStatusCode.NotModified);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductController>
        [HttpPost]
        [Route("/[controller]/add")]
        public void Post([FromForm] Product product)
        {
            DBConnection connection = new DBConnection();
            connection.AddProduct(product.name, product.categoryId);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        [Route("/[controller]/updateCategory")]
        public void Put([FromForm] Product product)
        {
            DBConnection connection = new DBConnection();
            connection.UpdateProduct(product.id, product.categoryId,product.categoryName);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        [Route("/[controller]/remove/{id}")]
        public void Delete(int id)
        {
            DBConnection connection = new DBConnection();
            connection.DeleteProduct(id);
        }
    }
}
