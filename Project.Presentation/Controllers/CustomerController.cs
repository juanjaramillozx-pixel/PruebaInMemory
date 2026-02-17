using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs;
using Project.Application.UseCases;

namespace Project.Presentation.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly CreateCustomerUseCase _createCustomer;
    private readonly GetAllCustomersUseCase _getAllCustomers;
    private readonly UpdateCustomerUseCase _updateCustomer;
    private readonly DeleteCustomerUseCase _deleteCustomer;

    public CustomerController(
        CreateCustomerUseCase createCustomer,
        GetAllCustomersUseCase getAllCustomers,
        UpdateCustomerUseCase updateCustomer,
        DeleteCustomerUseCase deleteCustomer)
    {
        _createCustomer = createCustomer;
        _getAllCustomers = getAllCustomers;
        _updateCustomer = updateCustomer;
        _deleteCustomer = deleteCustomer;
    }

    // GET /api/customers
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await _getAllCustomers.ExecuteAsync(ct);
        return Ok(result);
    }

    // POST /api/customers
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateCustomerDto dto,
        CancellationToken ct)
    {
        await _createCustomer.ExecuteAsync(dto, ct);
        return Created(string.Empty, null);
    }

    // PUT /api/customers/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateCustomerDto dto,
        CancellationToken ct)
    {
        if (id != dto.Id)
            return BadRequest("Id mismatch");

        await _updateCustomer.ExecuteAsync(dto, ct);
        return NoContent();
    }

    // DELETE /api/customers/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken ct)
    {
        await _deleteCustomer.ExecuteAsync(id, ct);
        return NoContent();
    }
}
