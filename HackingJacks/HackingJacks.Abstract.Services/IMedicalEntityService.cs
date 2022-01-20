using HackingJacks.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackingJacks.Abstract.Services
{
    public interface IMedicalEntityService
    {
        Task<Result> ProcessTextAsync(Guid id, string text);
    }
}
