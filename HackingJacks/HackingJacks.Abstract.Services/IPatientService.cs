using HackingJacks.DTOs;
using HackingJacks.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackingJacks.Abstract.Services
{
    public interface IPatientService
    {
        Result<Patient> Get(Guid id);
        Result Save(Patient patient);
    }
}
