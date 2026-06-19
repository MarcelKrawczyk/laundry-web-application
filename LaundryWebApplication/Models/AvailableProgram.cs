namespace LaundryWebApplication.Models;

public class AvailableProgram
{
    public int AvailableProgramId { get; set; }
    
    public int WashingMachineId { get; set; }
    public WashingMachine WashingMachine { get; set; } = null!;
    
    public int WashProgramId { get; set; }
    public WashProgram WashProgram { get; set; } = null!;
    
    public decimal Price { get; set; }
    
    public ICollection<PurchaseHistory> PurchaseHistories { get; set; } = new List<PurchaseHistory>();
}