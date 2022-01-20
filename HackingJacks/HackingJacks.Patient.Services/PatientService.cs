using HackingJacks.Abstract.Repositories;
using HackingJacks.Abstract.Services;
using HackingJacks.Audio.Domain.Repositories.Abstract;
using HackingJacks.DTOs;
using HackingJacks.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HackingJacks.Audio.Services
{
    public class PatientService : IPatientService
    {
        private IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

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
