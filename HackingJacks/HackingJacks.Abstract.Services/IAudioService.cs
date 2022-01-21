using HackingJacks.DTOs;
using HackingJacks.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HackingJacks.Audio.Services.Abstract
{
    public interface IAudioService
    {
        Result<MedicalAudio> Get(Guid id);
        Task<Result<MedicalAudio>> SaveAsync(Stream stream);
        Result<MedicalAudio> Save(MedicalAudio medicalAudio);
    }
}
