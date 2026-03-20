namespace TowelBox.Domain.Entities;

public class Towel
{
    public Guid Id { get; set; }

    public string ItemCode { get; set; } = string.Empty;

    public string ProductCode { get; set; } = string.Empty;

    public string Status { get; set; } = "LOOSE"; 

    public Guid? BoxId { get; set; }

    public bool IsActive { get; set; } = true;
}