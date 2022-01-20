using Amazon;
using Amazon.TranscribeService;
using Amazon.TranscribeService.Model;
using HackingJacks.Audio.Services.Abstract;
using HackingJacks.General;
using System;
using System.Threading.Tasks;

namespace HackingJacks.MedicalText.Services
{
    public class MedicalTextService
    {
        private readonly string _publicKey = "";
        private readonly string _privateKey = "";
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
        public async Task<Result<string>> TranscribeAsync(Guid guid)
        {
            try
            {
                var audioResult = _audioService.Get(guid);
                if (audioResult.Success)
                {
                    return new Result<string>(audioResult.Error);
                }

                var medicalAudio = audioResult.Item;

                var client = new AmazonTranscribeServiceClient(_publicKey, _privateKey, _regionEndPoint);

                var request = new StartMedicalTranscriptionJobRequest()
                {
                    MedicalTranscriptionJobName = guid.ToString(),
                    LanguageCode = LanguageCode.EnUS,
                    MediaFormat = MediaFormat.Mp4,
                    MediaSampleRateHertz = 16_000,
                    Media = new Media()
                    {
                        MediaFileUri = medicalAudio.AudioMediaUri
                    }
                };

                var response = await client.StartMedicalTranscriptionJobAsync(request);

                //wait for job to be completed

                medicalAudio.TranscriptFileUri = response.MedicalTranscriptionJob.Transcript.TranscriptFileUri;
                medicalAudio.Status = DTOs.MedicalAudio.MedicalAudioStatuses.TranscriptCreated;
                medicalAudio.DateUpdated = DateTime.UtcNow;
                _audioService.Save(medicalAudio);

                return new Result<string>(medicalAudio.TranscriptFileUri);
            }
            catch (Exception ex)
            {
                return new Result<string>(ex);
            }
        }
    }
}
