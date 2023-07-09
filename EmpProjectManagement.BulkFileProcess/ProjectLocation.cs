using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpProjectManagement.BulkFileProcess;
public class ProjectLocation
{
    public string Message { get; set; }
    public List<Data> Data { get; set; }

}
public class Data
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
}
