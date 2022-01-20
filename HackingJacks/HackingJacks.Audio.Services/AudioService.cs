using HackingJacks.Audio.Domain.Repositories.Abstract;
using HackingJacks.Audio.Services.Abstract;
using HackingJacks.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HackingJacks.Audio.Services
{
    public class AudioService : IAudioService
    {
        private IAudioRepository _audioRepository;

        public AudioService(IAudioRepository audioRepository)
        {
            _audioRepository = audioRepository;
        }

        public Result<Stream> Get(Guid id)
        {
           return _audioRepository.Get(id);
        }

        public Result<Guid> Save(Stream stream)
        {
            return _audioRepository.Save(stream);
        }
    }
}
