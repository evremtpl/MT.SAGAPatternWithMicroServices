using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using MT.RabbitMq;
using MT.RabbitMqMessage;
using MT.ReportService.API.Dtos;
using MT.ReportService.Core.Entity;
using MT.ReportService.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MT.ReportService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {

        private readonly IGenericService<Report> _reportService;
        private readonly IMapper _mapper;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public ReportController(IGenericService<Report> reportService, IMapper mapper, ISendEndpointProvider sendEndpointProvider)
        {

            _reportService = reportService;
            _mapper = mapper;
           _sendEndpointProvider= sendEndpointProvider;


        }
        [HttpPost]
        [Route("createreport")]
        public async Task<IActionResult> CreateReportUsingStateMachine([FromBody] ReportDto reportModel)
        {
            reportModel.ReportId = Guid.NewGuid();
            reportModel.ReportState = Dtos.FileStatus.Creating;
            reportModel.RequestDate = DateTime.Now; 
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:" + BusConstants.StartReportQueue));

           await _reportService.AddAsync(_mapper.Map<Report>(reportModel));

            await endpoint.Send<IStartReport>(new
            {
                ReportId = reportModel.ReportId,
                UUId = reportModel.UUID,
                ReportCreatedDate = DateTime.Now,
            });

            return Ok("Success");
        }






















        [HttpGet("{id}")]
        public async Task<IActionResult> GetReport(string id) //db de Id var mı yok mu diye bir kontrol yapılmadı! Bir filter yazılabilir kod tekrarı olmaması için
        {
            var report = await _reportService.GetByIdAsync(id);
            if (report != null) { return Ok(_mapper.Map<ReportDto>(report)); }
            return BadRequest("Gönderdiğiniz id ye ait rapor bulunmuyor");

        }
        






        [HttpGet]
        public async Task<IActionResult> GetAllReports()
        {
            var reports = await _reportService.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<ReportDto>>(reports));
        }
    }
}
