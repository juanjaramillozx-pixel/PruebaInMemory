using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;
using Project.Domain.Repositories;
using Project.Infrastructure.Persistence;

namespace Project.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync(
        CancellationToken ct = default)
    {
        return await _context.Customers
            .Include(c => c.Orders)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<Customer?> GetByIdAsync(
        Guid id,
        CancellationToken ct = default)
    {
        return await _context.Customers
            .Include(c => c.Orders)
            .FirstOrDefaultAsync(c => c.Id == id, ct);
    }

    public async Task AddAsync(
        Customer customer,
        CancellationToken ct = default)
    {
        await _context.Customers.AddAsync(customer, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(
        Customer customer,
        CancellationToken ct = default)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(
        Guid id,
        CancellationToken ct = default)
    {
        var customer = await _context.Customers.FindAsync(
            new object[] { id }, ct);

        if (customer is null)
            return;

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync(ct);
    }
}
