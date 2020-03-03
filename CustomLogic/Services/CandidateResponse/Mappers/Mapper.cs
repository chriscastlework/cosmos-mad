using System.Linq;
using CustomLogic.Services.CandidateResponse.Models;

namespace CustomLogic.Services.CandidateResponse.Mappers
{
    public static class CandidateResponseMapper
    {

        public static Database.CandidateResponse MapInsertModelToDbModel(CandidateResponseViewModel model, Database.CandidateResponse newDomainModel = null)
        {
            if (newDomainModel == null)
            {
                newDomainModel = new Database.CandidateResponse();
            }

            newDomainModel.CandidateId = model.CandidateId;
            newDomainModel.JobId = model.JobId;
            newDomainModel.ResponseType = model.ResponseType;
            return newDomainModel;
        }



        public static CandidateResponseViewModel MapDbModelToViewModel(Database.CandidateResponse dbModel)
        {
            var viewModel = new CandidateResponseViewModel();

            viewModel.CandidateId = dbModel.CandidateId;
            viewModel.JobId = dbModel.JobId;
            viewModel.ResponseType = dbModel.ResponseType;
            return viewModel;
        }


        public static IQueryable<CandidateResponseViewModel> MapDbModelQueryToViewModelQuery(IQueryable<Database.CandidateResponse> databaseQuery)
        {

            return databaseQuery.OrderByDescending(c => c.JobId).Select(c => new CandidateResponseViewModel()
            {
                CandidateId = c.CandidateId,
                JobId = c.JobId,
                ResponseType = c.ResponseType,
        });
        }
    }
}


