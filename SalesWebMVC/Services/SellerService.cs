using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Models;
using SalesWebMVC.Services.Exceptions;

namespace SalesWebMVC.Services;

public class SellerService
{
    private readonly SalesWebMVCContext _context;

    public SellerService(SalesWebMVCContext context)
    {
        _context = context;
    }

    public async Task<List<Seller>> FindAllAsync()
    {
        return await _context.Seller.ToListAsync();
    }

    public async Task InsertAsync(Seller obj)
    {
        _context.Add(obj);
        await _context.SaveChangesAsync();
    }

    public async Task<Seller> FindByIdAsync(int id)
    {
        return await _context.Seller
            .Include(seller => seller.Department)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task RemoveAsync(int id)
    {
        try
        {
            var seller = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(seller);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            throw new IntegrityException("Can't delete seller because he/she has sales");
        }
    }

    public async Task UpdateAsync(Seller seller)
    {
        bool hasAny = await _context.Seller.AnyAsync(s => s.Id == seller.Id);
        if (!hasAny)
        {
            throw new NotFoundException("Id not found");
        }

        try
        {
            _context.Update(seller);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {
            throw new DbConcurrencyException(e.Message);
        }
    }
}