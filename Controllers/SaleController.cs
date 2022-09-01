using APICODERHOUSE.Controllers.DTOs;
using APICODERHOUSE.Models;
using APICODERHOUSE.Repository;
using Microsoft.AspNetCore.Mvc;

namespace APICODERHOUSE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SaleController : ControllerBase
    {
        [HttpGet]
        public List<SaleAndInfo> GetSales()
        {
            return SaleHandler.GetSale();
        }
        [HttpPost]
        public bool CreateSale(SaleProduct saleProduct) //WORKS
        {
            if (UserController.isUserLoggedIn == true)
            {
                try
                {
                    return SaleHandler.CreateSale(new Sale
                    {
                        idProduct = saleProduct.idProduct,
                        comment = saleProduct.comment
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
        [HttpDelete]
        public bool DeleteSale([FromBody] int id)
        {
            if (UserController.isUserLoggedIn == true)
            {
                try
                {
                    return SaleHandler.DeleteSale(id);
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
