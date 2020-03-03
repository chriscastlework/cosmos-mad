using CustomLogic.Core.BaseClasses;
using CustomLogic.Core.Interfaces;
using CustomLogic.Services.CandidateResponse.Models;

namespace CustomLogic.Services.CandidateResponse
{
    /// <summary>
    /// This is the wrapper for the IService Please add your custom services here insert/update/get/list are already handled should be enough for rest api
    ///
    ///</summary>
    public class CandidateResponseService : ServiceBase<CandidateResponseViewModel, Database.CandidateResponse>
    {
        public CandidateResponseService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

