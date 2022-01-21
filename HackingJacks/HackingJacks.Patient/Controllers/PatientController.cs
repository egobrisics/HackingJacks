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
            var patient = new PatientModel()
            {
                Demographics = new PatientDemographicsModel()
                {
                    Name = new PatientFieldModel()
                    {
                        Text = "Erik Gobris",
                        Score = 99.9,
                        IsApproved = true,
                    },
                    Age = new PatientFieldModel()
                    {
                        Text = "32",
                        Score = 30.1,
                        IsApproved = false,
                    },
                    Occupation = new PatientFieldModel()
                    {
                        Text = "Programmer",
                        Score = 80.5,
                        IsApproved = false,
                    },
                },
                SocialHistory = new PatientSocialHistoryModel()
                {
                    AlcoholUse = new PatientFieldModel()
                    {
                        Text = "Social",
                        IsApproved = false,
                        Score = 72.8,
                    },
                },
                Medications = new PatientMedicationModel[]
                {
                    new PatientMedicationModel()
                    {
                        Name = new PatientFieldModel("Advil", 90, false),
                        Dosage = new PatientFieldModel("1 tablet", 87, false),
                    },
                    new PatientMedicationModel()
                    {
                        Name = new PatientFieldModel("Claritin D", 72.0, false),
                        Frequency = new PatientFieldModel("Daily", 47, false),
                    },
                },
                ExistingConditions = new PatientMedicalConditionModel[]
                {
                    new PatientMedicalConditionModel()
                    {
                        Description = new PatientFieldModel("Rash", 90),
                        Sites = new PatientFieldModel[]
                        {
                            new PatientFieldModel("Hand", 87),
                            new PatientFieldModel("Arm", 70),
                        },
                    },
                    new PatientMedicalConditionModel()
                    {
                        Description = new PatientFieldModel("Pain", 88),
                        Sites = new PatientFieldModel[]
                        {
                            new PatientFieldModel("Back", 49),
                            new PatientFieldModel("Neck", 47),
                        },
                    },
                },
            };
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
