using AuthenticationPlugin;
using AutoMapper;
using InventorySystem.Api.Dtos.ProductDetails;
using InventorySystem.Core.Models;
using InventorySystem.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventorySystem.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsDetailsController : ControllerBase
    {
        private readonly IProductDetailsService _productDetailsService;
        private readonly IMapper _mapper;

        public ProductsDetailsController(IProductDetailsService productDetailsService, IMapper mapper)
        {
            _productDetailsService = productDetailsService;
            _mapper = mapper;
        }


        [HttpGet]
        public ActionResult<IEnumerable<ProductDetails>> GetAll()
        {
            var productsDetails = _productDetailsService.GetAll();
            var Dto = _mapper.Map<IEnumerable<ProductDetails>, List<GetProductDetailsDto>>(productsDetails);
            return Ok(Dto);
        }


        [HttpGet("{id}")]
        public ActionResult<ProductDetails> GetById(int id)
        {
            var productDetails = _productDetailsService.GetById(id);

            return Ok(productDetails);
        }


        [HttpPost]
        public ActionResult Create([FromBody] CreateProductDetailsDto createProductDetailsDto)
        {
            var productDetailsToCreate = _mapper.Map<CreateProductDetailsDto, ProductDetails>(createProductDetailsDto);

            var newProductDetails = new ProductDetails();
            try
            {
                newProductDetails = _productDetailsService.Create(productDetailsToCreate);
            }
            catch (Exception)
            {
                return BadRequest("Coulnd't create product details");
            }

            return Ok(newProductDetails);
        }


        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] ProductDetails productDetails)
        {
            var existingProductDetails = _productDetailsService.GetById(id);

            if (existingProductDetails == null)
                return NotFound();

            try
            {
                _productDetailsService.Update(id, productDetails);
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
            var existingProductDetails = _productDetailsService.GetById(id);
            if (existingProductDetails == null)
                return NotFound();

            try
            {
                _productDetailsService.Delete(existingProductDetails);
            }
            catch (Exception)
            {
                return BadRequest("Couldn't delete product details");
            }

            return Ok();
        }
    }
}
