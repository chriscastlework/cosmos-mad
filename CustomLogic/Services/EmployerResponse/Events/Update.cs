using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.EmployerResponse.Mappers;
using CustomLogic.Services.EmployerResponse.Models;

namespace CustomLogic.Services.EmployerResponse.Events
{
    public class Update : IUpdateEvent<EmployerResponseViewModel, Database.EmployerResponse>
    {

        public int priority()
        {
            return 0;
        }
    
        public bool Run(EmployerResponseViewModel model, ref IQueryable<Database.EmployerResponse> repository, IUnitOfWork unitOfWork, Response<EmployerResponseViewModel> result, ICoreUser EmployerResponse)
        {
            var dbModel =  repository.Single(c => c.JobId == model.JobId && c.CandidateId == model.CandidateId); // you need to be using the primary key could be composit
            var updatedDbModel = EmployerResponseMapper.MapInsertModelToDbModel(model, dbModel);
            unitOfWork.With<Database.EmployerResponse>().Update(updatedDbModel);
            unitOfWork.SaveChanges();
            var newCustomResult = EmployerResponseMapper.MapDbModelToViewModel(updatedDbModel);
            result.Body = newCustomResult;
            return true;
        }
    }
}

