using SalesWebMVC.Models;

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
        return _context.Seller.FirstOrDefault(s => s.Id == id);
    }

    public void Remove(int id)
    {
        var seller = _context.Seller.Find(id);
        _context.Seller.Remove(seller);
        _context.SaveChanges();
    }
}