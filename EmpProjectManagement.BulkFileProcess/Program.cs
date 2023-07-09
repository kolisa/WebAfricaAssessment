// See https://aka.ms/new-console-template for more information
using EmpProjectManagement.BulkFileProcess;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


Console.WriteLine("Large File Processing & Bulk Insertion!");

var data = DownloadJsonData();
BulkInsertion(data);

Console.Write("File Processing Completed!!!");

Console.ReadLine();

DataTable DownloadJsonData()
{
    DataTable dt = new DataTable("Data");
    try
    {
        var urlLink = "https://www.webafrica.co.za/includes/fibregeolocation.handler.php?cmd=sources&polygon=1"; // 1. Specify url where the json is to read. 

        var web = new HtmlWeb(); // Init the HTMl Web


        var htmlDoc = web.Load(urlLink); // Load our url

        // ParseErrors is an ArrayList containing any errors from the Load statement
        if (htmlDoc.ParseErrors != null && htmlDoc.ParseErrors.Count() > 0)
        {
            // Handle any parse errors as required
            return dt;

        }
        else
        {

            if (htmlDoc.DocumentNode != null)
            {

                var ProjectLocation = JsonConvert.DeserializeObject<ProjectLocation>(htmlDoc.DocumentNode.OuterHtml);
                dt = UtilExtensions.ToDataTable<Data>(ProjectLocation.Data);
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