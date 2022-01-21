using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using HackingJacks.Abstract.Repositories;
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

namespace HackingJacks.Patients.Domain.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        public Result<Patient> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Result Save(Patient patient)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Patient>> SaveAsync(Patient patient)
        {
            IAmazonDynamoDB client = AmazonDbUtilityMethods.CreateAmazonDynamoDB();

            Document document = serializePatient(patient);

            await client.PutItemAsync(
                "Patient",
                document.ToAttributeMap(),
                CancellationToken.None).ConfigureAwait(false);

            return new Result<Patient>(patient);
        }

        private Document serializePatient(Patient patient)
        {
            return Document.FromJson(
                JsonSerializer.Serialize(
                    patient,
                    new JsonSerializerOptions
                    {
                        AllowTrailingCommas = false,
                        IgnoreNullValues = true,
                        IgnoreReadOnlyProperties = true
                    }));
        }
    }
}
