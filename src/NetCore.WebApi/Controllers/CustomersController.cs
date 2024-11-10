using NetCore.Application.Customer.ChangeEmail;
using NetCore.Application.Customer.CreateCustomer;
using NetCore.Application.Customer.GetCustomer;
using NetCore.Application.Customer.Shared;
using NetCore.Application.Customer.VerifyEmail;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace NetCore.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("Handles all operations related to customers, including retrieval, creation, email change, and email verification.")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet]
        [SwaggerOperation(Summary = "Retrieves customer data")]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomer([FromQuery] GetCustomerQuery query)
        {
            var client = _mediator.CreateRequestClient<GetCustomerQuery>();
            var response = await client.GetResponse<CustomerDto>(query);
            return Ok(response);
        }

        [HttpPost()]
        [SwaggerOperation(Summary = "Creates a new customer")]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateCustomer(CreateCustomerCommand command)
        {
            var client = _mediator.CreateRequestClient<CreateCustomerCommand>();
            var customer = await client.GetResponse<CustomerDto>(command);
            return CreatedAtAction(nameof(GetCustomer), new GetCustomerQuery(customer.Message.Email), customer);
        }

        [HttpPost("change-email")]
        [SwaggerOperation(Summary = "Changes the customer's email")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ChangeEmail(ChangeEmailCommand command)
        {
            await _mediator.Send<ChangeEmailCommand>(command);
            return NoContent();
        }

        [HttpPost("verify-email")]
        [SwaggerOperation(Summary = "Verifies the customer's email")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> VerifyEmail(VerifyEmailCommand command)
        {
            await _mediator.Send<VerifyEmailCommand>(command);
            return NoContent();
        }

    }
}
