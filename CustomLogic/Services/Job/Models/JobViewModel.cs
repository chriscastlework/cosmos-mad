using System;

namespace CustomLogic.Services.Job.Models
{
    public class JobViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int JobType { get; set; }
        public Guid EmployerId { get; set; }
    }
}
