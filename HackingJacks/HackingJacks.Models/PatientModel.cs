namespace HackingJacks.Models
{
    public class PatientModel
    {
        public PatientAllergyModel[] Allergies { get; set; }
        public PatientDemographicsModel Demographics { get; set; }
        public PatientMedicalConditionModel[] ExistingConditions { get; set; }
        public PatientMedicationModel[] Medications { get; set; }
        public PatientProcedureModel[] Procedures { get; set; }
        public PatientSocialHistoryModel SocialHistory { get; set; }

        public PatientDiagnosisModel[] Diagnoses { get; set; }
    }
}