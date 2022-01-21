using Amazon.DynamoDBv2;
using Amazon.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackingJacks.Abstract.Repositories
{
    public static class AmazonDbUtilityMethods
    {
        private static string _accessKey = "AKIA43NW6BIWSSVPQTGW";
        private static string _SharedKey = "HXRmaoJSz1VLwHLgQM/L7XMINo1VkHQCxt4UEgHF";

        public static string GetAccessKey()
        {
            return _accessKey;
        }

        public static string GetSecretKey()
        {
            return _SharedKey;
        }

        public static IAmazonDynamoDB CreateAmazonDynamoDB(string accessKey, string secretKey)
        {
            var credentials = new BasicAWSCredentials(accessKey, secretKey);

            var config = new AmazonDynamoDBConfig
            {
                RegionEndpoint = Amazon.RegionEndpoint.USEast1
            };

            var amazonDynamoDB = new AmazonDynamoDBClient(credentials, config);

            return amazonDynamoDB;
        }

        public static IAmazonDynamoDB CreateAmazonDynamoDB()
        {
            var credentials = new BasicAWSCredentials(GetAccessKey(), GetSecretKey());

            var config = new AmazonDynamoDBConfig
            {
                RegionEndpoint = Amazon.RegionEndpoint.USEast1
            };

            var amazonDynamoDB = new AmazonDynamoDBClient(credentials, config);

            return amazonDynamoDB;
        }

    }
}