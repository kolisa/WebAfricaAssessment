// See https://aka.ms/new-console-template for more information
using EmpProjectManagement.Console;
using EmpProjectManagement.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;


string name = string.Empty;
string username = string.Empty;
string jobTitle = string.Empty;
string dateOfBith = string.Empty;
int jobTitleId = 0;
Start:
nameV:
Console.Write("Please enter your name: ");
name = Console.ReadLine();
if (string.IsNullOrEmpty(name))
{
    goto nameV;
}
usernameV:
Console.Write("Please enter your username: ");
username = Console.ReadLine();
if (string.IsNullOrEmpty(username))
{

    goto usernameV;
}
Console.WriteLine("Type of Job title");
Console.WriteLine("");
Console.WriteLine($"id\tJobTitle\r\n1\tDeveloper\r\n2\tDBA\r\n3\tTester\r\n4\tBusiness Analyst");
Console.WriteLine("");
jobTitleV:
Console.Write("Please select your Job Title Id from above list: ");
jobTitle = Console.ReadLine();
if (string.IsNullOrEmpty(jobTitle))
{
    goto jobTitleV;
}
if (!int.TryParse(jobTitle, out int Id))
{
    goto jobTitleV;
}
if (int.TryParse(jobTitle, out jobTitleId))
{
    if (jobTitleId <= 0 || jobTitleId > 4)
    {
        goto jobTitleV;
    }
}
dateOfBithV:
Console.Write("Please enter your Date of Birth ({0}): ", DateTime.Now.AddYears(-20).GetDateTimeFormats());
dateOfBith = Console.ReadLine();
if (!string.IsNullOrEmpty(dateOfBith))
{
    if (!DateTime.TryParse(dateOfBith, out DateTime date))
    {
        goto dateOfBithV;
    }
}



//validate and add data to te employee object
AddEmployeeDto employee = new AddEmployeeDto();

if (!string.IsNullOrEmpty(dateOfBith))
    employee.DateOfBirth = Convert.ToDateTime(dateOfBith);
employee.JobTitleId = jobTitleId;
employee.Name = name;
employee.Surname = username;



var services = new ServiceCollection();
services.AddHttpClient<EmployeeClient>(c => c.BaseAddress = new Uri("https://localhost:7243/"));
var serviceProvider = services.BuildServiceProvider();

var client = serviceProvider.GetService<EmployeeClient>();
var response = await client.AddemployeeAsync(employee);

if (response != null)
{
    var message = await response.Content.ReadFromJsonAsync<Response>();
    Console.WriteLine("");
    Console.WriteLine();
    Console.WriteLine(message.Message);
    Console.WriteLine();
}


Console.WriteLine("Do you want to add another employee  y/n:");
string anotherEmp = Console.ReadLine();
if (anotherEmp.ToLower() == "y")
{
    Console.Clear();
    goto Start;
}
else { Console.WriteLine("Thank you!! good bye"); Environment.Exit(1); }
Console.ReadLine();