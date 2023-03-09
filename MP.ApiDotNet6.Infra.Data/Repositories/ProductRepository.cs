using Microsoft.EntityFrameworkCore;
using MP.ApiDotNet6.Domain.Entities;
using MP.ApiDotNet6.Domain.Repositories;
using MP.ApiDotNet6.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP.ApiDotNet6.Infra.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _db;
    public ProductRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Product> CreateAsync(Product product)
    {
        _db.Add(product);
        await _db.SaveChangesAsync();
        return product; 
    }

    public async Task DeleteAsync(Product product)
    {
        _db.Remove(product);
        await _db.SaveChangesAsync();
    }

    public async Task EditAsync(Product product)
    {
        _db.Update(product);
        await _db.SaveChangesAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<int> GetIdByCodeErpAsync(string codeErp)
    {
        // Buscou o CodeErp,se encontrar retorne o id, se não encontrar o CodeErp retorne 0
        return (await _db.Products.FirstOrDefaultAsync(x => x.CodeErp == codeErp))?.Id ?? 0;
    }

    public async Task<ICollection<Product>> GetProductsAsync()
    {
        return await _db.Products.ToListAsync();
    }
}
