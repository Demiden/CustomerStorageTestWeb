using System;
using System.Net;
using System.Threading.Tasks;
using CustomerStorageTestWeb.Controllers.Mapping;
using CustomerStorageTestWeb.Models;
using CustomerStorageTestWeb.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CustomerStorageTestWeb.Controllers
{
    [ApiController]
    [Route("api")]
    [SwaggerTag("API for work with customer entities")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomersRepository _customersRepository;

        public CustomerController(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }
        
        [HttpGet("customers")]
        [SwaggerOperation("Get customers", "Get all customers")]
        public async Task<IActionResult> Get()
        {
            var result = await _customersRepository.GetUsers();
            return Ok(result);
        }
        
        [HttpGet("customers/{customerId:guid}")]
        [SwaggerOperation("Get customer", "Get customer by id")]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(Customer))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid customerId)
        {
            var result = await _customersRepository.GetUserById(customerId);
            
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        
        [HttpPost("customers")]
        [SwaggerOperation("Add customer", "Add unregistered customer")]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(Guid))]
        public async Task<IActionResult> Add(AddCustomerRequest customer)
        {
            var customerId = await _customersRepository.AddCustomer(customer.ConstructCustomer());

            return Ok(customerId);
        }
        
        [HttpPut("customers")]
        [SwaggerOperation("Update customer", "Update registered customer")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update(UpdateCustomerRequest customer)
        {
            var result = await _customersRepository.UpdateCustomer(customer.ConstructCustomer());

            if (!result)
                return NotFound();

            return Ok();
        }
        
        [HttpDelete("customers/{customerId:guid}")]
        [SwaggerOperation("Delete customer", "Delete customer by id")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid customerId)
        {
            var result = await _customersRepository.Delete(customerId);
            if (!result)
                return NotFound();

            return Ok();
        }
    }
}