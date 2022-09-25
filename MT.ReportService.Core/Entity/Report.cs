using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MT.ReportService.Core.Entity
{
    public enum FileStatus
    {
        Creating,
        Completed,
        Cancelled
    }
    public class Report
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ReportId { get; set; }
        public int UUID { get; set; }

        public DateTime RequestDate { get; set; }


        public DateTime CreatedDate { get; set; }

        public FileStatus ReportState { get; set; }

    }
}
