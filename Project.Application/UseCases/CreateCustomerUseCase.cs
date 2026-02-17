using Project.Application.DTOs;
using Project.Domain.Entities;
using Project.Domain.Repositories;

namespace Project.Application.UseCases;

public class CreateCustomerUseCase
{
    private readonly ICustomerRepository _repository;

    public CreateCustomerUseCase(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(
        CreateCustomerDto dto,
        CancellationToken ct = default)
    {
        var customer = new Customer(dto.Name, dto.Email);
        await _repository.AddAsync(customer, ct);
    }
}
