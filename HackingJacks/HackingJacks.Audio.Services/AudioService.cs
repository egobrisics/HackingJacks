using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using HackingJacks.Audio.Domain.Repositories.Abstract;
using HackingJacks.Audio.Services.Abstract;
using HackingJacks.DTOs;
using HackingJacks.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackingJacks.Audio.Services
{
    public class AudioService : IAudioService
    {
        private readonly string _publicKey = "AKIA43NW6BIWSSVPQTGW";
        private readonly string _privateKey = "HXRmaoJSz1VLwHLgQM/L7XMINo1VkHQCxt4UEgHF";
        private readonly string _BucketName = "audiofilesinput";
        private readonly RegionEndpoint _regionEndPoint = RegionEndpoint.USEast1;
        private IAudioRepository _audioRepository;

        public AudioService(IAudioRepository audioRepository)
        {
            _audioRepository = audioRepository;
        }

        public Result<MedicalAudio> Get(Guid id)
        {
           return _audioRepository.Get(id);
        }

        public async Task<Result<MedicalAudio>> SaveAsync(Stream stream)
        {
            //save stream to s3 and then save new medical audio in dynamno
            string fileName = sendAudioFileToS3(_BucketName, stream);
            var client = new AmazonS3Client(_publicKey, _privateKey, _regionEndPoint);            
            var response = await client.GetObjectAsync(_BucketName, fileName);
            //using (var streamReader = new StreamReader(response.ResponseStream))
            //{
            //    var data = streamReader.ReadToEnd();
            //    var medicalTranscript = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOs.MedicalAudio>(data);

            //    //return new Result<string>(medicalTranscript.Results.Items.FirstOrDefault().ToString());
            //}
            //string audioUrl = response.Metadata.;
            SendMedicalReordToDatabase(fileName);
            return _audioRepository.Save(stream);
        }

        public Result<MedicalAudio> Save(MedicalAudio medicalAudio)
        {
            return _audioRepository.Save(medicalAudio);
        }

        public string sendAudioFileToS3(string bucketName, Stream stream)
        {            
            IAmazonS3 client = new AmazonS3Client(_publicKey, _privateKey, _regionEndPoint);
            TransferUtility utility = new TransferUtility(client);
            TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();

            request.BucketName = bucketName;
            string fileNameInS3 = "File" + DateTime.Now.Millisecond + "";

            request.Key = fileNameInS3;  
            request.InputStream = stream;
            utility.Upload(request); 

            return fileNameInS3;
        }

        public void SendMedicalReordToDatabase(string fileId)
        {

            var medicalAudio = new MedicalAudio
            {
                Id = new Guid(),
                AudioMediaUri = "",
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                Status = MedicalAudio.MedicalAudioStatuses.Created,
                PatientId = new Guid(),
                MedicalEntitiesId = new Guid(),
                TranscriptFileUri = ""
            };

            //Save this record to DynamoDB 
        }

        Result<MedicalAudio> IAudioService.SaveAsync(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
