using AutoMapper;
using CustomersManagement.Domain.Customer.Commands;
using CustomersManagement.Domain.Customer.Queries;
using CustomersManagement.DTOs;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CustomersManagement.Controllers
{
    [Route("api/v1/")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator mediatr;
        private readonly IMapper mapper;

        public CustomersController(IMediator mediatr, IMapper mapper)
        {
            this.mediatr = mediatr;
            this.mapper = mapper;
        }

        [Route("customers/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var modelResult = await mediatr.Send(new GetCustomerByIdQuery(id));
            if (!modelResult.Success)
                BadRequest(modelResult.ErrorText);

            return Ok(mapper.Map<CustomerDto>(modelResult.Data));
        }

        [Route("customers")]
        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] CustomerDto customer)
        {
            try
            {
                var result = await mediatr.Send(mapper.Map<CreateCustomerCommand>(customer));

                if (!result.Success)
                    return BadRequest(result.ErrorText);

                return Ok(mapper.Map<CustomerDto>(result.Data));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }
    }
}