namespace EmpProjectManagement.Models;

public class EmployeeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string JobTitle { get; set; }
    public DateTime? DateOfBirth { get; set; }
}
