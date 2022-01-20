namespace HackingJacks.Models
{
    public class PatientMedicalConditionModel
    {
        public PatientFieldModel Description { get; set; }
        public PatientFieldModel[] Sites { get; set; }
    }
}