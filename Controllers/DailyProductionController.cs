using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DailyProduction.Model;
using Azure.Data.Tables;
using Azure;
namespace IbasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DailyProductionController : ControllerBase
    {

        private List<DailyProductionDTO> _productionRepo;
        private readonly ILogger<DailyProductionController> _logger;

        private readonly string serviceUri = "DefaultEndpointsProtocol=https;AccountName=jacob360;AccountKey=LK7AJjQBghV7mJPDR7XGD0w6Phc5tcssfaSQA3B+u/T5tDQKN7JsF4ClIX6xwBWIwobH+p5iy15H+AStOgeCKQ==;BlobEndpoint=https://jacob360.blob.core.windows.net/;QueueEndpoint=https://jacob360.queue.core.windows.net/;TableEndpoint=https://jacob360.table.core.windows.net/;FileEndpoint=https://jacob360.file.core.windows.net";
        private TableClient tableClient;
        public DailyProductionController(ILogger<DailyProductionController> logger)
        {
            _logger = logger;

            //this.tableClient = new TableClient("DefaultEndpointsProtocol = https; AccountName = jacob360; AccountKey = LK7AJjQBghV7mJPDR7XGD0w6Phc5tcssfaSQA3B + u / T5tDQKN7JsF4ClIX6xwBWIwobH + p5iy15H + AStOgeCKQ ==; BlobEndpoint = https://jacob360.blob.core.windows.net/;QueueEndpoint=https://jacob360.queue.core.windows.net/;TableEndpoint=https://jacob360.table.core.windows.net/;FileEndpoint=https://jacob360.file.core.windows.net/;");
            this.tableClient = new TableClient(new Uri("https://jacob360.table.core.windows.net"), "IBASProduktion2022Jacob", new TableSharedKeyCredential("jacob360", "ksz2+P/Ljz5IaWK/tSZX8TROrciUFnipECk1SWJ1hr1scY+z6acHYoRZu7zTgBd6on72huQkkp5I+ASt1h4mOw=="));
            Pageable <TableEntity> entities = this.tableClient.Query<TableEntity>();

        }
        
        [HttpGet]
        public IEnumerable<DailyProductionDTO> Get()
        {
            var production = new List<DailyProductionDTO>();
            Pageable<TableEntity> entities = this.tableClient.Query<TableEntity>();
            foreach (TableEntity entity in entities)
            {
                var dto = new DailyProductionDTO
                {
                    Date = DateTime.Parse(entity.RowKey),
                    Model = (BikeModel)Enum.ToObject(typeof(BikeModel), Int32.Parse(entity.PartitionKey)),
                    ItemsProduced = (int)entity.GetInt32("itemsProduced")
                };
                production.Add(dto);
            }
            return production;
        }
    }
}
