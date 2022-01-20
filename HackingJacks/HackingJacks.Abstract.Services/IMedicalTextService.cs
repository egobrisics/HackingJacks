using HackingJacks.DTOs;
using HackingJacks.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackingJacks.Abstract.Services
{
    public interface IMedicalTextService
    {
        Task<Result<MedicalAudio>> TranscribeAsync(string mediaUri);
        Task<Result<MedicalAudio>> TranscribeAsync(Guid guid);
        Task<Result<string>> GetTranscribedTextAsync(Guid guid);
    }
}
