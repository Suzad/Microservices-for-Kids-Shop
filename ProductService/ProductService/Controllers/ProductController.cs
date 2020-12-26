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



namespace ProductService.Controllers
{
    [Microsoft.AspNetCore.Components.Route("/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
       
        [HttpGet]
        [Route("/[controller]/list")]
        public List<Product> Get()
        {
            
            DBConnection connection = new DBConnection();
           
            return connection.getProduct();
           
        }

       
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [Route("/[controller]/add")]
        public IActionResult Post([FromForm] Product product)
        {
            DBConnection connection = new DBConnection();
            
            if (connection.isDuplicate(product.name, product.categoryId))
            {
                return BadRequest();
            }
            else if (connection.AddProduct(product.name, product.categoryId))
            {
                return Created("successful creation","obj");
            }
            else
            {
                return StatusCode(500,"Error occured");
            }
        }

       
        [HttpPut("{id}")]
        [Route("/[controller]/updateCategory")]
        public void Put([FromForm] Product product)
        {
            DBConnection connection = new DBConnection();
            connection.UpdateProduct(product.id, product.categoryId,product.categoryName);
        }

        
        [HttpDelete("{id}")]
        [Route("/[controller]/remove/{id}")]
        public void Delete(Guid id)
        {
            DBConnection connection = new DBConnection();
            connection.DeleteProduct(id);
        }

        [HttpPost]
        [Route("/[controller]/updateRating")]
        public void UpdateRating([FromBody] List<Rating> ratings )
        {
            DBConnection connection = new DBConnection();
            foreach (var rating in ratings)
            {
                Console.WriteLine(rating);
                connection.RatingUpdate(rating);
            }
            
        }
    }
}
