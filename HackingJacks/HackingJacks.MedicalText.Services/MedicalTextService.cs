using Amazon;
using Amazon.S3;
using Amazon.TranscribeService;
using Amazon.TranscribeService.Model;
using HackingJacks.Abstract.Services;
using HackingJacks.Audio.Services.Abstract;
using HackingJacks.DTOs;
using HackingJacks.General;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HackingJacks.MedicalText.Services
{
    public class MedicalTextService : IMedicalTextService
    {
        private readonly string _publicKey = "AKIA43NW6BIWSSVPQTGW";
        private readonly string _privateKey = "HXRmaoJSz1VLwHLgQM/L7XMINo1VkHQCxt4UEgHF";

        private readonly string _inputBucketName = "audiofilesinput";
        private readonly string _outputBucketName = "transcribedaudiofile";
        private readonly RegionEndpoint _regionEndPoint = RegionEndpoint.USEast1;
        private IAudioService _audioService;

        public MedicalTextService(IAudioService audioService)
        {
            _audioService = audioService;
        }

        public async Task<Result<MedicalAudio>> TranscribeAsync(string fileName)
        {
            try
            {
                var medicalAudio = new MedicalAudio()
                {
                    Id = Guid.NewGuid(),
                    AudioMediaUri = "s3://" + _inputBucketName + "/" + fileName,
                };

                var result = await TranscribeAsync(medicalAudio);
                return result;
            }
            catch (Exception ex)
            {
                return new Result<MedicalAudio>(ex);
            }
        }

        /// <summary>
        /// retuns the s3 file to the processed audio file
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public async Task<Result<MedicalAudio>> TranscribeAsync(Guid guid)
        {
            try
            {
                var audioResult = _audioService.Get(guid);
                if (!audioResult.Success)
                {
                    return new Result<MedicalAudio>(audioResult.Error);
                }

                var result = await TranscribeAsync(audioResult.Item);

                return result;

            }
            catch (Exception ex)
            {
                return new Result<MedicalAudio>(ex);
            }
        }

        private async Task<Result<MedicalAudio>> TranscribeAsync(MedicalAudio medicalAudio)
        {
            try
            {
                var client = new AmazonTranscribeServiceClient(_publicKey, _privateKey, _regionEndPoint);

                var startRequest = new StartMedicalTranscriptionJobRequest()
                {
                    MedicalTranscriptionJobName = medicalAudio.Id.ToString(),
                    LanguageCode = LanguageCode.EnUS,
                    MediaFormat = MediaFormat.Mp3,
                    Media = new Media()
                    {
                        MediaFileUri = medicalAudio.AudioMediaUri,
                    },
                    OutputBucketName = _outputBucketName,
                    OutputKey = medicalAudio.Id.ToString(),
                    Specialty = Specialty.PRIMARYCARE,
                    Type = Amazon.TranscribeService.Type.CONVERSATION
                };

                var response = await client.StartMedicalTranscriptionJobAsync(startRequest);

                //wait for job to be completed
                GetMedicalTranscriptionJobResponse jobResponse = null;
                do
                {
                    var jobRequest = new GetMedicalTranscriptionJobRequest()
                    {
                        MedicalTranscriptionJobName = medicalAudio.Id.ToString()
                    };

                    jobResponse = await client.GetMedicalTranscriptionJobAsync(jobRequest);
                }
                while (jobResponse.MedicalTranscriptionJob.TranscriptionJobStatus == TranscriptionJobStatus.IN_PROGRESS ||
                        jobResponse.MedicalTranscriptionJob.TranscriptionJobStatus == TranscriptionJobStatus.QUEUED);

                if (jobResponse.MedicalTranscriptionJob.TranscriptionJobStatus == TranscriptionJobStatus.FAILED)
                {
                    return new Result<MedicalAudio>(new Exception(jobResponse.MedicalTranscriptionJob.FailureReason));
                }

                medicalAudio.TranscriptFileUri = jobResponse.MedicalTranscriptionJob.Transcript.TranscriptFileUri;
                medicalAudio.Status = DTOs.MedicalAudio.MedicalAudioStatuses.TranscriptCreated;
                medicalAudio.DateUpdated = DateTime.UtcNow;
                _audioService.Save(medicalAudio);

                return new Result<MedicalAudio>(medicalAudio);
            }
            catch (Exception ex)
            {
                return new Result<MedicalAudio>(ex);
            }
        }

        public async Task<Result<string>> GetTranscribedTextAsync(Guid guid)
        {
            try
            {
                var client = new AmazonS3Client(_publicKey, _privateKey, _regionEndPoint);
                var objectResponse = await client.GetObjectAsync(_outputBucketName, guid.ToString());

                using (var streamReader = new StreamReader(objectResponse.ResponseStream))
                {
                    var transcribedData = streamReader.ReadToEnd();
                    var medicalTranscript = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOs.MedicalTranscript>(transcribedData);

                    return new Result<string>(medicalTranscript.Results.Transcripts.FirstOrDefault()?.Text);
                }

            }
            catch (Exception ex)
            {
                return new Result<string>(ex);
            }
        }

 
    }
}
