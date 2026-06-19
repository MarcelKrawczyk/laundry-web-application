using System.ComponentModel.DataAnnotations;

namespace LaundryWebApplication.Models;

public class WashingMachine
{
    public int WashingMachineId { get; set; }
    
    public decimal MaxWeight { get; set; }

    [MaxLength(100)]
    public string SerialNumber { get; set; } = null!;
    
    public ICollection<AvailableProgram> AvailablePrograms { get; set; } = new List<AvailableProgram>();
}