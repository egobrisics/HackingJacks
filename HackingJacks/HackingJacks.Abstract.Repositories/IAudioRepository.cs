﻿using HackingJacks.DTOs;
using HackingJacks.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HackingJacks.Audio.Domain.Repositories.Abstract
{
    public interface IAudioRepository
    {
        Result<MedicalAudio> Get(Guid id);

        Result<MedicalAudio> Save(Stream stream);
        Result<MedicalAudio> Save(MedicalAudio medicalAudio);
    }
}