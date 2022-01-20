using HackingJacks.Abstract.Services;
using HackingJacks.Audio.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HackingJacks.MedicalText.Controllers
{
    [Route("medicaltext")]
    public class MedicalTextController : Controller
    {
        private IMedicalTextService _medicalTextService;

        public MedicalTextController(IMedicalTextService medicalTextService)
        {
            _medicalTextService = medicalTextService;
        }
        
        [HttpGet("Transcribe/{id}")]
        public async Task<JsonResult> Transcribe(Guid id)
        {
            var result = await _medicalTextService.TranscribeAsync(id);

            if (!result.Success)
            {
                return Json(result.Error.ToString());
            }

            return Json(result.Item);
        }
      
    }
}
