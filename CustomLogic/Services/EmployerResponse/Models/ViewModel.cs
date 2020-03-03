using System;

namespace CustomLogic.Services.EmployerResponse.Models
{
    public class EmployerResponseViewModel
    {
        public byte ResponseType { get; set; }
        public Guid CandidateId { get; set; }
        public Guid JobId { get; set; }

    }
}
