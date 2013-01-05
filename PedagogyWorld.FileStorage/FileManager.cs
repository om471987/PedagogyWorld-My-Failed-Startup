using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace PedagogyWorld.FileStorage
{
    public class FileManager 
    {
        public CloudBlobContainer GetCloudBlobContainer(string container)
        {
            //cloudStorageAccount = CloudStorageAccount.DevelopmentStorageAccount;
            var storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=http;AccountName=files;AccountKey=RlY6oc9A0eeAl/mTdBfLcTYuOr2q8KW1z6BQcjYn3xOL9GQjKqAsNS/wmnNk2Cxd0ZTER3jMM/pi2vqK14zhog==");
            var blobClient = storageAccount.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference(container);
            if (blobContainer.CreateIfNotExist()) 
            {

                blobContainer.SetPermissions(
                   new BlobContainerPermissions 
                   { 
                       PublicAccess = BlobContainerPublicAccessType.Blob 
                   }
                );
            }
            return blobContainer;
        }
    }
}
