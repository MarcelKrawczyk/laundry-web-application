using LaundryWebApplication.Data;
using LaundryWebApplication.DTOs;
using LaundryWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<string?> PostWashingMachineAsync(PostRequestDTO dto)
    {
        var washingMachine = await _context.WashingMachines
            .AnyAsync(x => x.SerialNumber == dto.WashingMachine.SerialNumber);
        if (washingMachine)
            return "Pralka o takim numerze seryjnym już istnieje.";

        var names = dto.availablePrograms.Select(x => x.ProgramName).Distinct().ToList();
        var nameExists = await _context.WashPrograms
            .Where(p => names.Contains(p.Name)).ToListAsync();
        if (nameExists.Count != names.Count)
            return "Któryś z podanych programów nie istnieje";
        
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var machine = new WashingMachine
            {
                SerialNumber = dto.WashingMachine.SerialNumber,
                MaxWeight = dto.WashingMachine.MaxWeight
            };
            foreach (var ap in dto.availablePrograms)
            {
                var program = nameExists.First(p => p.Name == ap.ProgramName);
                machine.AvailablePrograms.Add(new AvailableProgram
                {
                    WashProgram = program,   // dopinamy ISTNIEJĄCY program bez tworzenia nowego
                    Price = ap.Price
                });
            }
            _context.WashingMachines.Add(machine);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return null;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}