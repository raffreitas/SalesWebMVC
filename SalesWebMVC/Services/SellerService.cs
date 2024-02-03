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

    public List<Seller> FindAll()
    {
        return _context.Seller.ToList();
    }

    public void Insert(Seller obj)
    {
        _context.Add(obj);
        _context.SaveChanges();
    }

    public Seller FindById(int id)
    {
        return _context.Seller
            .Include(seller => seller.Department)
            .FirstOrDefault(s => s.Id == id);
    }

    public void Remove(int id)
    {
        var seller = _context.Seller.Find(id);
        _context.Seller.Remove(seller);
        _context.SaveChanges();
    }

    public void Update(Seller seller)
    {
        if (!_context.Seller.Any(s => s.Id == seller.Id))
        {
            throw new NotFoundException("Id not found");
        }

        try
        {
            _context.Update(seller);
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException e)
        {
            throw new DbConcurrencyException(e.Message);
        }
    }
}