using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;

namespace TestSAS
{
    class Program
    {
        //https://docs.microsoft.com/en-us/azure/storage/storage-dotnet-shared-access-signature-part-2#add-a-method-to-try-container-operations-using-a-shared-access-signature
        static void Main(string[] args)
        {
            string containerSAS = "<your container SAS>";
            //string blobSAS = "<your blob SAS>";
            //string containerSASWithAccessPolicy = "<your container SAS with access policy>";
            //string blobSASWithAccessPolicy = "<your blob SAS with access policy>";

            //Call the test methods with the shared access signatures created on the container, with and without the access policy.
            UseContainerSAS(containerSAS);
            //UseContainerSAS(containerSASWithAccessPolicy);

            Console.ReadLine();
        }
        static void UseContainerSAS(string sas)
        {
            //Try performing container operations with the SAS provided.

            //Return a reference to the container using the SAS URI.
            CloudBlobContainer container = new CloudBlobContainer(new Uri(sas));

            //Create a list to store blob URIs returned by a listing operation on the container.
            List<ICloudBlob> blobList = new List<ICloudBlob>();

            //Write operation: write a new blob to the container.
            try
            {
                CloudBlockBlob blob = container.GetBlockBlobReference("testblobCreatedViaSAS.txt");
                string blobContent = "This blob was created with a shared access signature granting write permissions to the container. ";
                MemoryStream msWrite = new MemoryStream(Encoding.UTF8.GetBytes(blobContent));
                msWrite.Position = 0;
                using (msWrite)
                {
                    blob.UploadFromStream(msWrite);
                }
                Console.WriteLine("Write operation succeeded for SAS " + sas);
                Console.WriteLine();
            }
            catch (StorageException e)
            {
                Console.WriteLine("Write operation failed for SAS " + sas);
                Console.WriteLine("Additional error information: " + e.Message);
                Console.WriteLine();
            }

        }

    }
}
