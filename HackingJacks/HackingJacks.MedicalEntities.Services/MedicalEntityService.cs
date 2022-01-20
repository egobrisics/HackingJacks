using HackingJacks.Abstract.Services;
using HackingJacks.General;
using System;
using System.Threading.Tasks;

namespace HackingJacks.MedicalEntities.Services
{
    public class MedicalEntityService : IMedicalEntityService
    {
        public Task<Result> ProcessTextAsync(Guid id, string text)
        {
            throw new NotImplementedException();
        }
    }
}
