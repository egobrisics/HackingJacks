using Amazon;
using Amazon.ComprehendMedical;
using Amazon.ComprehendMedical.Model;
using AutoMapper;
using HackingJacks.Abstract.Services;
using HackingJacks.DTOs;
using HackingJacks.General;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace HackingJacks.MedicalEntities.Services
{
    public class MedicalEntityService : IMedicalEntityService
    {
        private readonly string _publicKey = "AKIA43NW6BIWSSVPQTGW";
        private readonly string _privateKey = "HXRmaoJSz1VLwHLgQM/L7XMINo1VkHQCxt4UEgHF";
        private readonly RegionEndpoint _regionEndPoint = RegionEndpoint.USEast1;
        private IMapper _mapper;

        public MedicalEntityService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<Result<List<Entity>>> ProcessTextAsync(Guid id, string text)
        {
            try
            {
                var client = new AmazonComprehendMedicalClient(_publicKey, _privateKey, _regionEndPoint);

                var request = new DetectEntitiesV2Request()
                {
                    Text = text,
                };

                var response = await client.DetectEntitiesV2Async(request);
                
                if (response.HttpStatusCode != HttpStatusCode.OK)
                {
                    return new Result<List<Entity>>(new Exception("An error occured while detecting entities: " + response.ToString()));
                }

                return new Result<List<Entity>>(response.Entities);
            }
            catch (Exception ex)
            {
                return new Result<List<Entity>>(ex);
            }
        }
    }
}
