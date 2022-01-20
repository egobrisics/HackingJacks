using HackingJacks.Abstract.Repositories;
using HackingJacks.Audio.Domain.Repositories.Abstract;
using HackingJacks.DTOs;
using HackingJacks.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HackingJacks.Audio.Domain.Repositories.Interfaces
{
    public class PatientRepository : IPatientRepository
    {
        public Result<Patient> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Result Save(Patient patient)
        {
            throw new NotImplementedException();
        }
    }
}
