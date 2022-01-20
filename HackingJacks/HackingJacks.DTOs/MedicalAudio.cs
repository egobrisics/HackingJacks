using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackingJacks.DTOs
{
    public class MedicalAudio
    {
        public enum MedicalAudioStatuses
        {
            Created = 0,
            TranscriptCreated = 1,
            MedicalEntitiesProcessed = 2,
            PatientCreated = 3
        }

        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }
        public string AudioMediaUri { get; set; }
        public string TranscriptFileUri { get; set; }
        public Guid MedicalEntitiesId { get; set; }
        public Guid PatientId { get; set; }

        public MedicalAudioStatuses Status { get; set; }
        [JsonProperty("results")]
        public Results Results { get; set; }

    }
}
