using Amazon;
using Amazon.TranscribeService;
using Amazon.TranscribeService.Model;
using HackingJacks.Abstract.Services;
using HackingJacks.Audio.Services.Abstract;
using HackingJacks.DTOs;
using HackingJacks.General;
using System;
using System.Threading.Tasks;

namespace HackingJacks.MedicalText.Services
{
    public class MedicalTextService : IMedicalTextService
    {
        private readonly string _publicKey = "AKIA43NW6BIWSSVPQTGW";
        private readonly string _privateKey = "HXRmaoJSz1VLwHLgQM/L7XMINo1VkHQCxt4UEgHF";
        private readonly string _outputBucketName = "transcribedaudiofile";
        private readonly RegionEndpoint _regionEndPoint = RegionEndpoint.USEast1;
        private IAudioService _audioService;

        public MedicalTextService(IAudioService audioService)
        {
            _audioService = audioService;
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

                var medicalAudio = audioResult.Item;

                var client = new AmazonTranscribeServiceClient(_publicKey, _privateKey, _regionEndPoint);

                var startRequest = new StartMedicalTranscriptionJobRequest()
                {
                    MedicalTranscriptionJobName = guid.ToString(),
                    LanguageCode = LanguageCode.EnUS,
                    MediaFormat = MediaFormat.Mp3,
                    Media = new Media()
                    {
                        MediaFileUri = medicalAudio.AudioMediaUri,
                    },
                    OutputBucketName = _outputBucketName,
                    OutputKey = guid.ToString() + ".txt",
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
                        MedicalTranscriptionJobName = guid.ToString()
                    };

                    // code block to be executed
                    jobResponse = await client.GetMedicalTranscriptionJobAsync(jobRequest);
               
                }
                while (jobResponse.MedicalTranscriptionJob.TranscriptionJobStatus != TranscriptionJobStatus.COMPLETED ||
                        jobResponse.MedicalTranscriptionJob.TranscriptionJobStatus != TranscriptionJobStatus.FAILED);

                if (jobResponse.MedicalTranscriptionJob.TranscriptionJobStatus == TranscriptionJobStatus.FAILED)
                {
                    return new Result<MedicalAudio>(new Exception(jobResponse.MedicalTranscriptionJob.FailureReason));
                }

                medicalAudio.TranscriptFileUri = response.MedicalTranscriptionJob.Transcript.TranscriptFileUri;
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
    }
}
