using System.Security.Cryptography.X509Certificates;

namespace EmpProjectManagement.Models;

public class ProjectDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Employees { get; set; }
    public decimal TotalCost { get; set; }
}
