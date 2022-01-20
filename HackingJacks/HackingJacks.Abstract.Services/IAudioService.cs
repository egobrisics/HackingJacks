using HackingJacks.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HackingJacks.Audio.Services.Abstract
{
    public interface IAudioService
    {
        Result<Stream> Get(Guid id);
        Result<Guid> Save(Stream stream);
    }
}
