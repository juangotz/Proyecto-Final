using APICODERHOUSE.Controllers.DTOs;
using APICODERHOUSE.Models;
using APICODERHOUSE.Repository;
using Microsoft.AspNetCore.Mvc;

namespace APICODERHOUSE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet("GetProducts")]
        public List<Product> GetProductos()
        {
            return ProductHandler.GetProductos();
        }
        [HttpGet("GetSoldProducts")]
        public List<SoldProduct> GetSoldProducts(int id)
        {
            return ProductHandler.GetSoldProducts(id);
        }
        [HttpDelete]
        public bool DeleteProduct([FromBody] int id)
        {
            if (UserController.isUserLoggedIn == true)
            {
                try
                {
                    return ProductHandler.DeleteProduct(id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        [HttpPut]
        public bool UpdateDescription([FromBody] PutProduct putProduct)
        {
            if (UserController.isUserLoggedIn == true)
            {
                try
                {
                    return ProductHandler.UpdateDescription(new Product()
                    {
                        id = putProduct.id,
                        description = putProduct.description
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        [HttpPost]
        public bool CreateProduct([FromBody] PostProduct postProduct)
        {
            if (UserController.isUserLoggedIn == true)
            {
                try
                {
                    return ProductHandler.CreateProduct(new Product()
                    {
                        description = postProduct.description,
                        price = postProduct.price,
                        stock = postProduct.stock,
                        priceSell = postProduct.priceSell,
                        idUser = TokenHandler.userToken
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
