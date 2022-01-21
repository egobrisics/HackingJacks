using HackingJacks.DTOs;
using HackingJacks.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HackingJacks.Audio.Domain.Repositories.Abstract
{
    public interface IAudioRepository
    {
        Result<MedicalAudio> Get(Guid id);

        Task<Result<MedicalAudio>> SaveAsync(MedicalAudio medicalAudio);
        Result<MedicalAudio> Save(MedicalAudio medicalAudio);
    }
}
