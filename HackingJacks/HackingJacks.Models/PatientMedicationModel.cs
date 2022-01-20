namespace HackingJacks.Models
{
    public class PatientMedicationModel
    {
        public PatientFieldModel Dosage { get; set; }
        public PatientFieldModel Frequency { get; set; }
        public PatientFieldModel Name { get; set; }
        public PatientFieldModel Route { get; set; }
        public PatientFieldModel Strength { get; set; }
    }
}