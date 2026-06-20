using LaundryWebApplication.Models;

namespace LaundryWebApplication.DTOs;

public class GetWashingMachineDTO
{
    public decimal MaxWeight { get; set; }
    public string Serial { get; set; } = null!;
}

public class GetProgramDTO
{
    public string Name { get; set; } = null!;
    public int Duration { get; set; }
}

public class GetPurchasesDTO
{
    public DateTime Date { get; set; }
    public int? Rating { get; set; }
    public decimal Price { get; set; }
    public GetWashingMachineDTO WashingMachine { get; set; } = null!;
    public GetProgramDTO Program { get; set; } = null!;
}

public class GetCustomerDTO
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public List<GetPurchasesDTO> Purchases { get; set; } = new();
}