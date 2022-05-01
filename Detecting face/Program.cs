using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Rekognition.Model;

namespace Detecting_face
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DetectFace();
        }
        static void DetectFace()
        {
            var awsCredentials = new Amazon.Runtime.BasicAWSCredentials("AKIA3BZMVBVKD2IHGKEZ", "rlTB6Ne7BJxDgX1nCOrMU98uDVdP8ux/VZkrgPqe");

            var rekognition = new Amazon.Rekognition.AmazonRekognitionClient(awsCreden‌tials, Amazon.RegionEndpoint.USEast1);

            var res = rekognition.StartFaceDetectionAsync(new Amazon.Rekognition.Model.StartFaceDetectionRequest() { Video = new Amazon.Rekognition.Model.Video() { S3Object = new Amazon.Rekognition.Model.S3Object() { Bucket = "mynewbuckeeett", Name = "video.mp4" } } }).Result;
            GetFaceDetectionResponse result;
            do
            {
                Thread.Sleep(4000);
                result = rekognition.GetFaceDetectionAsync(new Amazon.Rekognition.Model.GetFaceDetectionRequest() { JobId = res.JobId }).Result;
                if (result.JobStatus == Amazon.Rekognition.VideoJobStatus.SUCCEEDED)
                {
                    break;
                }
            } while (true);

            foreach (var faceDetection in result.Faces)
            {
                Console.WriteLine($"Brightness: {faceDetection.Face.Quality.Brightness}\n Sharpness: {faceDetection.Face.Quality.Sharpness}\n Confidence: {faceDetection.Face.Confidence}\n\n");
            }
            Console.ReadLine();
        }
    }
}
