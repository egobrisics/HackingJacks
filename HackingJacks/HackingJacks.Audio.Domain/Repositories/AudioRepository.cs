using HackingJacks.Audio.Domain.Repositories.Abstract;
using HackingJacks.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HackingJacks.Audio.Domain.Repositories.Interfaces
{
    public class AudioRepository : IAudioRepository
    {
        public Result<Stream> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Result<Guid> Save(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
