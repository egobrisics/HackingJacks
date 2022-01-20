using System;

namespace HackingJacks.DTOs
{
    public class Patient
    {
        public Guid ID { get; set; }


        public PatientField Name { get; set; }

        public PatientField Age { get; set; }
    }
}
