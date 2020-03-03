using System.Linq;
using CustomLogic.Services.EmployerResponse.Models;

namespace CustomLogic.Services.EmployerResponse.Mappers
{
    public static class EmployerResponseMapper
    {

        public static Database.EmployerResponse MapInsertModelToDbModel(EmployerResponseViewModel model, Database.EmployerResponse newDomainModel = null)
        {
            if (newDomainModel == null)
            {
                newDomainModel = new Database.EmployerResponse();
            }

            newDomainModel.CandidateId = model.CandidateId;
            newDomainModel.JobId = model.JobId;
            newDomainModel.ResponseType = model.ResponseType;
            return newDomainModel;
        }



        public static EmployerResponseViewModel MapDbModelToViewModel(Database.EmployerResponse dbModel)
        {
            var viewModel = new EmployerResponseViewModel();

            viewModel.CandidateId = dbModel.CandidateId;
            viewModel.JobId = dbModel.JobId;
            viewModel.ResponseType = dbModel.ResponseType;
            return viewModel;
        }


        public static IQueryable<EmployerResponseViewModel> MapDbModelQueryToViewModelQuery(IQueryable<Database.EmployerResponse> databaseQuery)
        {

            return databaseQuery.OrderByDescending(c => c.JobId).Select(c => new EmployerResponseViewModel()
            {
                CandidateId = c.CandidateId,
                JobId = c.JobId,
                ResponseType = c.ResponseType
        });
        }
    }
}


