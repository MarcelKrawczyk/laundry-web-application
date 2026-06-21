using LaundryWebApplication.DTOs;

namespace LaundryWebApplication.Services;

public interface ILaundryService
{
    Task<GetCustomerDTO?> GetCustomerPurchasesAsync(int customerId);

    Task<string?> PostWashingMachineAsync(PostRequestDTO dto);
}