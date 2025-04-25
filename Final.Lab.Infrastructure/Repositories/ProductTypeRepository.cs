using Final.Lab.Domain.Models;
using Final.Lab.Domain.Repositories;
using Final.Lab.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Final.Lab.Infrastructure.Repositories;

public class ProductTypeRepository(DataContext _context) : IProductTypeRepository
{
    public async Task<List<ProductType>> GetAllActive()
    {
        var result = await _context.ProductTypes
                          .Where(p => p.IsDeleted == false)
                          .ToListAsync();

        return result;
    }

    public async Task<ProductType?> GetByCodeActive(string code)
    {
        var result = await _context.ProductTypes
                           .Where(p => p.IsDeleted == false)
                           .FirstOrDefaultAsync(p => p.Code == code);

        return result;
    }
}
