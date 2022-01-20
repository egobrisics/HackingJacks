﻿using Amazon.ComprehendMedical.Model;
using HackingJacks.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackingJacks.Abstract.Services
{
    public interface IMedicalEntityService
    {
        Task<Result<List<Entity>>> ProcessTextAsync(Guid id, string text);
    }
}
