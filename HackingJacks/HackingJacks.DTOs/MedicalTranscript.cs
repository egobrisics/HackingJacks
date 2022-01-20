using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HackingJacks.DTOs
{
    public class MedicalTranscript
    {
        [JsonProperty("jobName")]
        public string JobName { get; set; }

        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        [JsonProperty("results")]
        public Results Results { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class Transcript
    {
        [JsonProperty("transcript")]
        public string Text { get; set; }
    }

    public class Alternative
    {
        [JsonProperty("confidence")]
        public string Confidence { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }

    public class Item
    {
        [JsonProperty("start_time")]
        public string StartTime { get; set; }

        [JsonProperty("end_time")]
        public string EndTime { get; set; }

        [JsonProperty("alternatives")]
        public List<Alternative> Alternatives { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Results
    {
        [JsonProperty("transcripts")]
        public List<Transcript> Transcripts { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }


}
