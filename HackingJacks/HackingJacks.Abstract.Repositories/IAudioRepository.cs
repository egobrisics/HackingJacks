using HackingJacks.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HackingJacks.Audio.Domain.Repositories.Abstract
{
    public interface IAudioRepository
    {
        Result<Stream> Get(Guid id);

        Result<Guid> Save(Stream stream);
    }
}
