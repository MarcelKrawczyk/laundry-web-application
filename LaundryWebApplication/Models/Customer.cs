using System.ComponentModel.DataAnnotations;

namespace LaundryWebApplication.Models;

public class Customer
{
    public int CustomerId { get; set; }

    [MaxLength(50)] 
    public string FirstName { get; set; } = null!;

    [MaxLength(100)] 
    public string LastName { get; set; } = null!;

    [MaxLength(100)] 
    public string? PhoneNumber { get; set; }
    
    public ICollection<PurchaseHistory> PurchaseHistories { get; set; } = new List<PurchaseHistory>();
}