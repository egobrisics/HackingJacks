using HackingJacks.Abstract.Services;
using HackingJacks.Audio.Services.Abstract;
using HackingJacks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HackingJacks.Patient.Controllers
{
    [Route("patient")]
    public class PatientController : Controller
    {
        private IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public IActionResult Index()
        {
            var patient = new PatientModel();
            patient.Name = "Erik Gobris";

            return View(patient);
        }

        [HttpGet("{id}")]
        public JsonResult Get(Guid id)
        {
            var result = _patientService.Get(id);

            if (!result.Success)
            {
                return Json(result.Error.ToString());
            }

            return Json(result.Item);
        }
      
    }
}
