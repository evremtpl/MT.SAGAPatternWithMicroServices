using System;


namespace MT.ReportService.API.Dtos
{
    public enum FileStatus
    {
        Creating,
        Completed
    }
    public class ReportDto
    {
        public Guid ReportId { get; set; }
        public int UUID { get; set; }


        public FileStatus ReportState { get; set; }
    }
}
