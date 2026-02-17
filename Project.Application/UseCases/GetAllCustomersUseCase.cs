using Project.Application.DTOs;
using Project.Domain.Repositories;

namespace Project.Application.UseCases;

public class GetAllCustomersUseCase
{
    private readonly ICustomerRepository _repository;

    public GetAllCustomersUseCase(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CustomerDto>> ExecuteAsync(
        CancellationToken ct = default)
    {
        var customers = await _repository.GetAllAsync(ct);

        return customers.Select(c =>
            new CustomerDto(c.Id, c.Name, c.Email));
    }
}
