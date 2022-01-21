using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using HackingJacks.Audio.Domain.Repositories.Abstract;
using HackingJacks.DTOs;
using HackingJacks.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace HackingJacks.Audio.Domain.Repositories.Interfaces
{
    public class AudioRepository : IAudioRepository
    {
        public Result<MedicalAudio> Get(Guid id)
        {
            var medicalAudio = new MedicalAudio()
            {
                Id = id,
                AudioMediaUri = "s3://audiofilesinput/Script 1 - Back Pain.mp3",
            };

            //get item from dynamno db
            return new Result<MedicalAudio>(medicalAudio);
        }

        public async Task<Result<MedicalAudio>> SaveAsync(MedicalAudio medicalAudio)
        {
            IAmazonDynamoDB client = HackingJacks.Abstract.Repositories.AmazonDbUtilityMethods.CreateAmazonDynamoDB();

            Document document = serialize(medicalAudio);

            await client.PutItemAsync(
                "MedicalAudioFile",
                document.ToAttributeMap(),
                CancellationToken.None).ConfigureAwait(false);

            return new Result<MedicalAudio>(medicalAudio);
        }        

        public Result<MedicalAudio> Save(MedicalAudio medicalAudio)
        {
            //save medicalAudio in dynamno
            return new Result<MedicalAudio>(medicalAudio);
        }

        private Document serialize(MedicalAudio medicalAudio)
        {
            return Document.FromJson(
                JsonSerializer.Serialize(
                    medicalAudio,
                    new JsonSerializerOptions
                    {
                        AllowTrailingCommas = false,
                        IgnoreNullValues = true,
                        IgnoreReadOnlyProperties = true
                    }));
        }
    }
}
