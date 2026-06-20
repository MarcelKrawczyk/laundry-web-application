using System.ComponentModel.DataAnnotations;

namespace LaundryWebApplication.DTOs;

public class PostRequestDTO
{
    [Required] public WashingMachineDTO WashingMachine { get; set; } = null!;
    
    [Required]
    public List<PostAvailableProgramsDTO> availablePrograms { get; set; } = new();
}
public class PostAvailableProgramsDTO
{
    [Required]
    [MaxLength(50)]
    public string ProgramName { get; set; } = null!;
    
    [Range(0, 25)]
    public decimal Price { get; set; }
}

public class WashingMachineDTO
{
    [Range(8, double.MaxValue)] 
    public decimal MaxWeight { get; set; }

    [MaxLength(100)] 
    public string SerialNumber { get; set; } = null!;
}