using System;

namespace MT.PersonService.API.Dtos
{
    public enum FileStatus
    {
        Creating,
        Completed
    }
    public class ReportDto
    {
        public int UUID { get; set; }

        public DateTime RequestDate { get; set; }


        public FileStatus ReportState { get; set; }
    }
}
