// See https://aka.ms/new-console-template for more information
using EmpProjectManagement.BulkFileProcess;
using HtmlAgilityPack;

using System.Data;
using System.Data.SqlClient;
using System.Text.Json;

Console.WriteLine("Large File Processing & Bulk Insertion!");

var data = DownloadJsonData();
BulkInsertion(data);
Console.WriteLine();
Console.WriteLine("File Processing Completed!!!");

Console.ReadLine();

DataTable DownloadJsonData()
{
    DataTable dt = new DataTable("Data");
    try
    {
        var urlLink = "https://www.webafrica.co.za/includes/fibregeolocation.handler.php?cmd=sources&polygon=1";

        var web = new HtmlWeb();

        var htmlDoc = web.Load(urlLink);

        if (htmlDoc.ParseErrors != null && htmlDoc.ParseErrors.Count() > 0)
        {
            return dt;
        }
        else
        {
            if (htmlDoc.DocumentNode != null)
            {
               
                var projectLocation = JsonSerializer.Deserialize<ProjectLocation>(htmlDoc.DocumentNode.OuterHtml);
             
                 dt = UtilExtensions.ToDataTable<Data>(projectLocation.Data);
            }
        }

        return dt;
    }
    catch (Exception)
    {

        throw;
    }


}



void BulkInsertion(DataTable data)
{
    try
    {
        string csDestination = "Data Source=c0070230725-1\\MSSQLSERVER2019;Initial Catalog=CodeWorks;User ID=kolisa;Password=L@gin3;Encrypt=false;TrustServerCertificate=true";

        using (SqlConnection connection = new SqlConnection(csDestination))
        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
        {
            connection.Open();
            bulkCopy.DestinationTableName = "ProjectLocations";
            bulkCopy.WriteToServer(data);
        }
    }
    catch (Exception)
    {

        throw;
    }

}