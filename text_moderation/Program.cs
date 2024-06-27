using System;
using Azure.AI.ContentSafety;

namespace Azure.AI.ContentSafety.Csharp.Demo
{
  class ContentSafetyAnalyzeText
  {
    public static void AnalyzeText()
    {
      // retrieve the endpoint and key from the environment variables created earlier
      string endpoint = "INSERT_ENDPOINT_HERE";
      string key = "INSERT_KEY_HERE";

      ContentSafetyClient client = new ContentSafetyClient(new Uri(endpoint), new AzureKeyCredential(key));

      string text = "INSERT_TEXT_TO_MODERATE.";

      var request = new AnalyzeTextOptions(text);

      Response<AnalyzeTextResult> response;
      try
      {
          response = client.AnalyzeText(request);
      }
      catch (RequestFailedException ex)
      {
          Console.WriteLine("Analyze text failed.\nStatus code: {0}, Error code: {1}, Error message: {2}", ex.Status, ex.ErrorCode, ex.Message);
          throw;
      }

      Console.WriteLine("\nAnalyze text succeeded:");
      Console.WriteLine("Detected Hate severity: {0}", response.Value.CategoriesAnalysis.FirstOrDefault(a => a.Category == TextCategory.Hate)?.Severity ?? 0);
      Console.WriteLine("Detected SelfHarm severity: {0}", response.Value.CategoriesAnalysis.FirstOrDefault(a => a.Category == TextCategory.SelfHarm)?.Severity ?? 0);
      Console.WriteLine("Detected Sexual severity: {0}", response.Value.CategoriesAnalysis.FirstOrDefault(a => a.Category == TextCategory.Sexual)?.Severity ?? 0);
      Console.WriteLine("Detected Violence severity: {0}", response.Value.CategoriesAnalysis.FirstOrDefault(a => a.Category == TextCategory.Violence)?.Severity ?? 0);

    }
    static void Main()
    {
        AnalyzeText();
    }
  }
}