namespace TowelBox.Domain.Entities;

public class Box
{
    public Guid Id { get; set; }

    public string BoxCode { get; set; } = string.Empty;

    public string ProductCode { get; set; } = string.Empty;

    public int Capacity { get; set; }

    public string Status { get; set; } = "OPEN"; // OPEN / CLOSED

    public bool IsActive { get; set; } = true;
}