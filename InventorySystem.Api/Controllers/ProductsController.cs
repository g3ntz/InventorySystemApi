using AutoMapper;
using InventorySystem.Core.Models;
using InventorySystem.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace InventorySystem.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            var products = _productService.GetAll();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = _productService.GetById(id);

            return Ok(product);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Product product)
        {
            var existingProduct = _productService.GetById(id);

            if (existingProduct == null)
                return NotFound();

            try
            {
                _productService.Update(id, product);
            }
            catch (Exception)
            {
                return BadRequest("Couldn't update product details");
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingProduct = _productService.GetById(id);
            if (existingProduct == null)
                return NotFound();

            try
            {
                _productService.Delete(existingProduct);
            }
            catch (Exception)
            {
                return BadRequest("Couldn't delete product details");
            }

            return Ok();
        }
    }
}
