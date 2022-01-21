namespace HackingJacks.Models
{
    public class PatientFieldModel
    {
        public PatientFieldModel()
        {
        }

        public PatientFieldModel(string text, double score, bool isApproved = false)
        {
            Text = text;
            Score = score;
            IsApproved = isApproved;
        }

        public bool IsApproved { get; set; }
        public double Score { get; set; }
        public string Text { get; set; }
    }
}