using Project.Application.DTOs;
using Project.Domain.Repositories;

namespace Project.Application.UseCases;

public class UpdateCustomerUseCase
{
    private readonly ICustomerRepository _repository;

    public UpdateCustomerUseCase(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(
        UpdateCustomerDto dto,
        CancellationToken ct = default)
    {
        var customer = await _repository.GetByIdAsync(dto.Id, ct);

        if (customer is null)
            throw new InvalidOperationException("Customer not found");

        customer.Update(dto.Name, dto.Email);

        await _repository.UpdateAsync(customer, ct);
    }
}
