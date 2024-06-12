using Microsoft.EntityFrameworkCore.Internal;
using Sales.Models;
using Sales.Models.Enuns;

namespace Sales.Data
{
    public class SeedingService
    {
        private SalesContext _context;

        public SeedingService(SalesContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Department.Any() || _context.Seller.Any() || _context.SalesRecord.Any())
            {
                return; // DB has been seeded
            }
            Department d1 = new Department(1, "Computers");

            
            Seller s1 = new Seller(1, "Bob Brown", "bob@gmail.com", new System.DateTime(1998, 4, 21), 1000.0,d1) ;

            SalesRecord r1 = new SalesRecord(1, new System.DateTime(2018, 09, 25), 11000.0, SaleStatus.Billed, s1);
            SalesRecord r2 = new SalesRecord(1, new System.DateTime(2018, 10, 25), 22000.0, SaleStatus.Billed, s1);
            _context.Department.AddRange(d1);
            _context.Seller.AddRange(s1);
            _context.SalesRecord.AddRange(r1,r2);
            _context.SaveChanges();
        }
    }
}
