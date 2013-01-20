using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace PedagogyWorld.FileStorage
{
    public class AwsHandle
    {
        private readonly AmazonS3 _s3Client = AWSClientFactory.CreateAmazonS3Client("AKIAIJNFMB4HFNKTXNXA", "KCEE3e9Ac90LdGfF6QqyjMQNbKV6MCeQHdZACcAc", RegionEndpoint.USWest2);

        public IEnumerable<string> ListBuckets()
        {
            IEnumerable<string> output=null;
            try
            {
                var response = _s3Client.ListBuckets();
                if (response.Buckets != null && response.Buckets.Count > 0)
                {
                    output = from b in response.Buckets
                             select b.BucketName;
                }
            }
            catch (AmazonS3Exception ex)
            {
                if (ex.ErrorCode != null && (ex.ErrorCode.Equals("InvalidAccessKeyId") ||
                    ex.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Please check the provided AWS Credentials.");
                    Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");
                }
                else
                {
                    Console.WriteLine("Caught Exception: " + ex.Message);
                    Console.WriteLine("Response Status Code: " + ex.StatusCode);
                    Console.WriteLine("Error Code: " + ex.ErrorCode);
                    Console.WriteLine("Request ID: " + ex.RequestId);
                    Console.WriteLine("XML: " + ex.XML);
                }
            }
            return output;
        }

        public void NewBuckets(string name)
        {
            try
            {
                var request = new PutBucketRequest
                {
                    BucketRegion = S3Region.USW2,
                    BucketName = name
                };
                _s3Client.PutBucket(request);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteBucket(string name)
        {
            try
            {
                var request = new DeleteBucketRequest { BucketName = name };
                _s3Client.DeleteBucket(request);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public IEnumerable<string> GetDocs(string bucket)
        {
            IEnumerable<string> objects = null;
            try
            {
                var request = new ListObjectsRequest
                {
                    BucketName = bucket
                };
                var response = _s3Client.ListObjects(request);
                objects = from o in response.S3Objects
                          select o.Key;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return objects;
        }

        public bool NewFile(string bucket, string fileName, Stream uploadFileStream, string contentType)
        {
            var output = false;
            try
            {
                var request = new PutObjectRequest
                {
                    BucketName = bucket,
                    Key = fileName,
                    InputStream = uploadFileStream,
                    ContentType = contentType
                };
                _s3Client.PutObject(request);
                output = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return output;
        }

        public void DownloadObject(string bucket, string fileName)
        {
            try
            {
                var request = new GetObjectRequest
                {
                    BucketName = bucket,
                    Key = fileName
                };
                var response = _s3Client.GetObject(request);
                response.WriteResponseStreamToFile("C:\\Users\\larry\\Documents\\perl_poetry.pdf");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteObject(string bucket, string fileNmae)
        {
            try
            {
                var request = new DeleteObjectRequest
                {
                    BucketName = bucket,
                    Key = fileNmae
                };
                _s3Client.DeleteObject(request);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}