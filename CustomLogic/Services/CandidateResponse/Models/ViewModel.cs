using System;

namespace CustomLogic.Services.CandidateResponse.Models
{
    public class CandidateResponseViewModel 
    {
        public byte ResponseType { get; set; }
        public Guid JobId { get; set; }
        public Guid CandidateId { get; set; }
    }
}
