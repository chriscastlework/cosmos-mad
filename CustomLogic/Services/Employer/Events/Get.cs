using System.Linq;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using CustomLogic.Services.Employer.Mappers;
using CustomLogic.Services.Employer.Models;

namespace CustomLogic.Services.Employer.Events
{
    public class View : IViewEvent<EmployerViewModel, Database.Employer>
    {
        public int CreatedId = 0;

        public Response<EmployerViewModel> Run(EmployerViewModel model, ref IQueryable<Database.Employer> repository, IUnitOfWork unitOfWork, Response<EmployerViewModel> result, ICoreUser Employer)
        {
            var itemToUpdate = repository.SingleOrDefault(c => c.Id == model.Id);

            if (itemToUpdate != null)
            {
                var newCustomResult = EmployerMapper.MapDbModelToViewModel(itemToUpdate);
                result.Body = newCustomResult;
                result.Success = true;
            }
            else
            {
                result.Success = false;
                result.LogError("Error viewing Employers");
            }

            return result;
        }

    }
}
