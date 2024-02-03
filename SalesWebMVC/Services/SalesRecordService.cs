using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Models;

namespace SalesWebMVC.Services;

public class SalesRecordService
{
    private readonly SalesWebMVCContext _context;

    public SalesRecordService(SalesWebMVCContext context)
    {
        _context = context;
    }

    public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
    {
        var result = _context.SalesRecords.Select(s => s);
        if (minDate.HasValue)
        {
            result = result.Where(x => x.Date >= minDate.Value);
        }

        if (maxDate.HasValue)
        {
            result = result.Where(x => x.Date <= maxDate.Value);
        }

        return await result
            .Include(record => record.Seller)
            .Include(record => record.Seller.Department)
            .OrderByDescending(record => record.Date)
            .ToListAsync();
    }

    public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate,
        DateTime? maxDate)
    {
        var result = _context.SalesRecords.Select(s => s);
        if (minDate.HasValue)
        {
            result = result.Where(x => x.Date >= minDate.Value);
        }

        if (maxDate.HasValue)
        {
            result = result.Where(x => x.Date <= maxDate.Value);
        }

        return await result
            .Include(record => record.Seller)
            .Include(record => record.Seller.Department)
            .OrderByDescending(record => record.Date)
            .GroupBy(record => record.Seller.Department)
            .ToListAsync();
    }
}