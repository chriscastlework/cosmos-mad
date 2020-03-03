using System;
using System.Linq;
using CustomLogic.Services.Candidate.Models;

namespace CustomLogic.Services.Candidate.Mappers
{
    public static class CandidateMapper
    {

        public static Database.Candidate MapInsertModelToDbModel(CandidateViewModel model, Database.Candidate newDomainModel = null)
        {
            if (newDomainModel == null)
            {
                newDomainModel = new Database.Candidate();
                newDomainModel.Id = Guid.NewGuid();
            }

            newDomainModel.UserId = model.UserId;
            return newDomainModel;
        }



        public static CandidateViewModel MapDbModelToViewModel(Database.Candidate dbModel)
        {
            var viewModel = new CandidateViewModel();

            viewModel.Id = dbModel.Id;
            viewModel.UserId = dbModel.UserId;

            return viewModel;
        }


        public static IQueryable<CandidateViewModel> MapDbModelQueryToViewModelQuery(IQueryable<Database.Candidate> databaseQuery)
        {

            return databaseQuery.OrderByDescending(c => c.Id).Select(c => new CandidateViewModel()
            {
                Id = c.Id,
                UserId = c.UserId
            });
        }
    }
}


