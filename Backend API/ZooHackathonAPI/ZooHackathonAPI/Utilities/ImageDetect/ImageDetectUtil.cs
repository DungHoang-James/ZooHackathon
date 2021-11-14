using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooHackathonAPI.Utilities.ImageDetect
{
    public class ImageDetectUtil
    {
        public static List<ObjectDetect> DetectImage(string url)
        {
            Image image = Image.FetchFromUri(url);
            ImageAnnotatorClient client = ImageAnnotatorClient.Create();
            IReadOnlyList<EntityAnnotation> labels = client.DetectLabels(image);
            var results = new List<ObjectDetect>();
            foreach (EntityAnnotation label in labels)
            {
                if (label != null)
                {
                    ObjectDetect ob = new ObjectDetect();
                    ob.Probability = label.Score;
                    ob.Label = label.Description;
                    results.Add(ob);
                }
            }
            return results;
        }
    }
}
