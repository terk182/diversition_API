using App.Products.Models;
using App.Products.Models.Requests;
using App.Products.Models.Responses;
using App.Products.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// เรียกดู product ทั้งหมด
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("get")]
        public IActionResult Getproducts()
        {
            var productList = _productService.GetAllProduct();
            return Ok(new { data = productList });
        }
        /// <summary>
        /// เรียกดู product ตาม ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("products/{productID}")]
        public IActionResult GetproductsById(int productID)
        {
            var product = _productService.GetAllProductById(productID);
            return Ok(new { data = product });
        }
        /// <summary>
        /// เพิ่ม product 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("add")]
        public IActionResult AddProduct([FromBody] Product product)
        {
            if (product == null || string.IsNullOrEmpty(product.ProductName))
            {
                return BadRequest(new { message = "Invalid product data." });
            }

            _productService.AddProduct(product);

            return Ok(new
            {
                message = "Product added successfully.",
                product = product
            });
        }
        /// <summary>
        /// เพิ่ม product review
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("products/reviews")]
        public IActionResult reviews([FromBody] ReviewsRequest request)
        {
            var result = _productService.addreview(request);
            return Ok(result);
        }
        /// <summary>
        /// ดู review ตา product ID
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("products/reviews/{productID}")]
        public IActionResult Getreviews(int productID)
        {
            var result = _productService.GetreviewByProductId(productID);
            var responce = new BaseResponse<List<ReviewsRequest>>();
            responce.Success = true;
            responce.Data = result;
            return Ok(responce);
        }
    }
}
