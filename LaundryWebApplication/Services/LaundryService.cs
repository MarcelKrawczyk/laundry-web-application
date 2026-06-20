using LaundryWebApplication.Data;
using LaundryWebApplication.DTOs;
using Microsoft.EntityFrameworkCore;

namespace LaundryWebApplication.Services;

public class LaundryService : ILaundryService
{
    private readonly LaundryContext _context;

    public LaundryService(LaundryContext context)
    {
        _context = context;
    }

    public async Task<GetCustomerDTO?> GetCustomerPurchasesAsync(int customerId)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(x => x.CustomerId == customerId);

        if (customer is null)
        {
            return null;
        }

        var purchases = await _context.PurchaseHistories
            .Where(x => x.CustomerId == customerId)
            .Select(x => new GetPurchasesDTO
            {
                Date = x.PurchaseDate,
                Rating = x.Rating,
                Price = x.AvailableProgram.Price,
                WashingMachine = new GetWashingMachineDTO
                {
                    Serial = x.AvailableProgram.WashingMachine.SerialNumber,
                    MaxWeight =  x.AvailableProgram.WashingMachine.MaxWeight
                },
                Program = new GetProgramDTO
                {
                    Name = x.AvailableProgram.WashProgram.Name,
                    Duration = x.AvailableProgram.WashProgram.DurationMinutes
                }
            }).ToListAsync();
        
        return new GetCustomerDTO
        {
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            PhoneNumber =  customer.PhoneNumber,
            Purchases = purchases
        };
    }
}