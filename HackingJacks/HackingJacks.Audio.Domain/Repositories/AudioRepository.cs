using HackingJacks.Audio.Domain.Repositories.Abstract;
using HackingJacks.DTOs;
using HackingJacks.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HackingJacks.Audio.Domain.Repositories.Interfaces
{
    public class AudioRepository : IAudioRepository
    {
        public Result<MedicalAudio> Get(Guid id)
        {
            //get item from dynamno db
            throw new NotImplementedException();
        }

        public Result<MedicalAudio> Save(Stream stream)
        {
            //save stream to s3 and then save new medical audio in dynamno
            throw new NotImplementedException();
        }

        public Result<MedicalAudio> Save(MedicalAudio medicalAudio)
        {
            //save medicalAudio in dynamno
            throw new NotImplementedException();
        }
    }
}
