using HackingJacks.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackingJacks.Abstract.Services
{
    public interface IMedicalTextService
    {
        Result<Guid> ProcessAudioFile(Guid guid);
    }
}
