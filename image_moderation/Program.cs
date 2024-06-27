using System;
using Azure.AI.ContentSafety;

namespace Azure.AI.ContentSafety.Dotnet.Sample
{
  class ContentSafetySampleAnalyzeImage
  {
    public static void AnalyzeImage()
    {
      // retrieve the endpoint and key from the environment variables created earlier
      string endpoint = "INSERT_ENDPOINT_HERE";
      string key = "INSERT_KEY_HERE";

      ContentSafetyClient client = new ContentSafetyClient(new Uri(endpoint), new AzureKeyCredential(key));

      // Example: analyze image

      string imagePath = @"INSERT_IMAGE_PATH_HERE";
      ContentSafetyImageData image = new ContentSafetyImageData(BinaryData.FromBytes(File.ReadAllBytes(imagePath)));

      var request = new AnalyzeImageOptions(image);

      Response<AnalyzeImageResult> response;
      try
      {
          response = client.AnalyzeImage(request);
      }
      catch (RequestFailedException ex)
      {
          Console.WriteLine("Analyze image failed.\nStatus code: {0}, Error code: {1}, Error message: {2}", ex.Status, ex.ErrorCode, ex.Message);
          throw;
      }

      Console.WriteLine("Detected Hate severity: {0}", response.Value.CategoriesAnalysis.FirstOrDefault(a => a.Category == ImageCategory.Hate)?.Severity ?? 0);
      Console.WriteLine("Detected SelfHarm severity: {0}", response.Value.CategoriesAnalysis.FirstOrDefault(a => a.Category == ImageCategory.SelfHarm)?.Severity ?? 0);
      Console.WriteLine("Detected Sexual severity: {0}", response.Value.CategoriesAnalysis.FirstOrDefault(a => a.Category == ImageCategory.Sexual)?.Severity ?? 0);
      Console.WriteLine("Detected Violence severity: {0}", response.Value.CategoriesAnalysis.FirstOrDefault(a => a.Category == ImageCategory.Violence)?.Severity ?? 0);
    }
    static void Main()
    {
      AnalyzeImage();
    }
  }
}