using System;
using CustomLogic.Core.Interfaces;

namespace CustomLogic.Services.UserService.Models
{
    public class UserViewModel : ICoreUser 
    {
        public Guid Id {get;set;}
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid? CandidateId { get; set; }
    }
}
