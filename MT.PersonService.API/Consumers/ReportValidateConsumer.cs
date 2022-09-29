using ClosedXML.Excel;
using MassTransit;
using Microsoft.Extensions.Logging;
using MT.PersonService.Core.Entity;
using MT.PersonService.Core.Interfaces.Services;
using MT.RabbitMqMessage.Event;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MT.PersonService.API.Consumers
{
    public class ReportValidateConsumer :
      IConsumer<IReportValidateEvent>
    {
        private readonly ILogger<ReportValidateConsumer> _logger;
        private readonly IGenericService<ContactInfo> _personService;

        

        public ReportValidateConsumer(ILogger<ReportValidateConsumer> logger, IGenericService<ContactInfo> personService)
        {
            _logger = logger;
            _personService = personService;
        }

        public async Task Consume(ConsumeContext<IReportValidateEvent> context)
        {
            var data = context.Message;

            if (data.UUId==2)
            {
                await context.Publish<IReportCancelledEvent>(
          new { ReportId = context.Message.ReportId, UUId = context.Message.UUId });
            }
            else
            {
                try
                {
                  
                    using var ms = new MemoryStream();
                    var wb = new XLWorkbook();
                    var ds = new DataSet();
                    ds.Tables.Add(await GetTable("persons"));

                    wb.Worksheets.Add(ds);
                    wb.SaveAs(ms);

                    MultipartFormDataContent multipartFormDataContent = new();
                    multipartFormDataContent.Add(new ByteArrayContent(ms.ToArray()), "file", Guid.NewGuid().ToString() + ".xlsx");

                    var baseUrl = "http://localhost:5003/api/files/";
                    using (var httpClient = new HttpClient())
                    {
                        var reponse = await httpClient.PostAsync($"{baseUrl}?fileId={context.Message.ReportId}", multipartFormDataContent);
                        if (reponse.IsSuccessStatusCode)
                        {
                            _logger.LogInformation($" File (Id={context.Message.ReportId} ) was created by successful");
                           
                        }

                    }
                }
                catch (Exception ex)
                {

                    _logger.LogInformation(ex.Message);
                }
            }
        }



        private async Task<DataTable> GetTable(string tableName)
        {
            DataTable table = new DataTable { TableName = tableName };
            table.Columns.Add("Location", typeof(string));
            table.Columns.Add("PersonCount", typeof(int));
            table.Columns.Add("PhoneNumber", typeof(int));
            
            var contactInfos= await _personService.GetAllAsync();


                var qry = from cont in contactInfos
                          where cont.UUID != 0
                          group cont by cont.Location
                 into grp
                          select new
                          {
                              Location = grp.Key,
                              PersonCount = grp.Select(x => x.UUID).Distinct().Count(),
                              PhoneNumber = grp.Select(x => x.PhoneNumber).Distinct().Count()
                          };

                foreach (var row in qry.OrderBy(x => x.PersonCount))
                {
                    table.Rows.Add(row.Location, row.PersonCount, row.PhoneNumber);
                    Console.WriteLine("{0}: {1} : {2}", row.Location, row.PersonCount, row.PhoneNumber);
                }

            




            return table;
        }
    }
}
