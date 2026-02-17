using Project.Domain.Repositories;

namespace Project.Application.UseCases;

public class DeleteCustomerUseCase
{
    private readonly ICustomerRepository _repository;

    public DeleteCustomerUseCase(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(
        Guid customerId,
        CancellationToken ct = default)
    {
        await _repository.DeleteAsync(customerId, ct);
    }
}
