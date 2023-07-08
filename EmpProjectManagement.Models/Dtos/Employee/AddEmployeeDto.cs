using System.ComponentModel.DataAnnotations;

namespace EmpProjectManagement.Models;

public class AddEmployeeDto
{
    [Required(ErrorMessage = "Please enter a name")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Please enter a surname")]
    public string Surname { get; set; }
    [Required(ErrorMessage = "Please select a Job Title")]
    public int JobTitleId { get; set; }
    public DateTime? DateOfBirth { get; set; }
}
