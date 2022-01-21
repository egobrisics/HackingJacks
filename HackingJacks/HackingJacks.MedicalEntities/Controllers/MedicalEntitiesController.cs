using HackingJacks.Abstract.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HackingJacks.MedicalEntities.Controllers
{
    [Route("medicalEntities")]
    public class MedicalEntitiesController : Controller
    {
        private IMedicalEntityService _medicalEntityService;
        private IMedicalTextService _medicalTextService;

        public MedicalEntitiesController(IMedicalEntityService medicalEntityService, 
                                        IMedicalTextService medicalTextService)
        {
            _medicalEntityService = medicalEntityService;
            _medicalTextService = medicalTextService;
        }

        [HttpGet("processAudioFile/{fileName}")]
        public async Task<ViewResult> ProcessAudioFile(string fileName)
        {
            var resultAudio = await _medicalTextService.TranscribeAsync(fileName);
            if (!resultAudio.Success)
            {
                return null;
            }

            var resultText = await _medicalTextService.GetTranscribedTextAsync(resultAudio.Item.Id);
            if (!resultText.Success)
            {
                return null;
            }

            var resultEntities = await _medicalEntityService.ProcessTextAsync(resultAudio.Item.Id, resultText.Item);

            if (!resultEntities.Success)
            {
                return null;
            }

            var patientModel = _medicalEntityService.MapPatient(resultEntities.Item);

            return View("~/Views/Patient/Index.cshtml", patientModel);
        }

        [HttpGet("processTranscript/{id}")]
        public async Task<JsonResult> ProcessTranscript(Guid id)
        {
            var resultText = await _medicalTextService.GetTranscribedTextAsync(id);
            if (!resultText.Success)
            {
                return Json(resultText.Error.ToString());
            }

            var resultEntities  = await _medicalEntityService.ProcessTextAsync(id, resultText.Item);

            if (!resultEntities.Success)
            {
                return Json(resultEntities.Error.ToString());
            }

            return Json(resultEntities.Item);
        }
    }
}
