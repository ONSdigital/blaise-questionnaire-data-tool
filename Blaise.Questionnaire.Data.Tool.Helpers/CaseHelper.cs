namespace Blaise.Questionnaire.Data.Tool.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Blaise.Nuget.Api.Api;
    using Blaise.Nuget.Api.Contracts.Interfaces;
    using Blaise.Nuget.Api.Contracts.Models;
    using Blaise.Questionnaire.Data.Tool.Helpers.Models;
    using Newtonsoft.Json;

    public class CaseHelper
    {
        private readonly IBlaiseCaseApi _blaiseCaseApi;

        public CaseHelper(ConnectionModel connectionModel)
        {
            _blaiseCaseApi = new BlaiseCaseApi(connectionModel);
        }

        public static CaseHelper GetInstance(ConnectionModel connectionModel)
        {
            return new CaseHelper(connectionModel);
        }

        public void CreateCasesInBlaise(int numberOfCases, string questionnaireName, string serverParkName, int primaryKey)
        {
            _blaiseCaseApi.RemoveCases(questionnaireName, serverParkName);

            var caseModels = new List<CaseModel>();
            try
            {
                for (var count = 1; count <= numberOfCases; count++)
                {
                    var caseDataModel = new CaseDataModel(primaryKey);
                    caseModels.Add(caseDataModel.ToCaseModel());
                    primaryKey++;

                    if (MaxChunkSizeOrMaxCountReached(count, numberOfCases))
                    {
                        _blaiseCaseApi.CreateCases(caseModels, questionnaireName, serverParkName);
                        caseModels = new List<CaseModel>();
                        Console.WriteLine($"Total cases written {count}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an error {ex} writing {caseModels.Count} cases for questionnaire '{questionnaireName}'");
            }

            Console.WriteLine($"Completed creating cases for questionnaire '{questionnaireName}'");
        }

        public void CreateCasesInBlaise(string questionnaireName, string serverParkName, string caseSampleFile)
        {
            _blaiseCaseApi.RemoveCases(questionnaireName, serverParkName);

            var caseModels = new List<CaseModel>();
            var sampleCaseList = GetSampleDataFields(caseSampleFile);
            var count = 1;

            if (string.IsNullOrWhiteSpace(caseSampleFile))
            {
                Console.WriteLine("No sample file provided. Using default data.");
            }
            else
            {
                Console.WriteLine($"Using data from sample file: {caseSampleFile}");
                Console.WriteLine($"Loaded {sampleCaseList.Count} sample cases from file '{caseSampleFile}'.");
            }

            try
            {
                foreach (var sampleCase in sampleCaseList)
                {
                    if (!sampleCase.ContainsKey("qiD.Serial_Number"))
                    {
                        Console.WriteLine($"Sample case missing 'qiD.Serial_Number': {JsonConvert.SerializeObject(sampleCase)}");
                        continue;
                    }

                    if (!int.TryParse(sampleCase["qiD.Serial_Number"], out var primaryKey))
                    {
                        Console.WriteLine($"Invalid 'qiD.Serial_Number': {sampleCase["qiD.Serial_Number"]}");
                        continue;
                    }

                    var caseDataModel = new CaseDataModel(primaryKey, sampleCase);
                    var caseModel = caseDataModel.ToCaseModel();

                    Console.WriteLine($"Creating case model: {JsonConvert.SerializeObject(caseModel)}");

                    caseModels.Add(caseModel);

                    if (MaxChunkSizeOrMaxCountReached(count, sampleCaseList.Count))
                    {
                        Console.WriteLine($"Writing {caseModels.Count} cases to Blaise.");
                        _blaiseCaseApi.CreateCases(caseModels, questionnaireName, serverParkName);
                        caseModels = new List<CaseModel>();
                        Console.WriteLine($"Total cases written {count}");
                    }

                    count++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an error {ex} writing {caseModels.Count} cases for questionnaire '{questionnaireName}'");
            }

            Console.WriteLine($"Completed creating cases for questionnaire '{questionnaireName}'");
        }

        private static bool MaxChunkSizeOrMaxCountReached(int count, int maxCount)
        {
            const int maxChunkSize = 500;
            return count % maxChunkSize == 0 || count == maxCount;
        }

        private List<Dictionary<string, string>> GetSampleDataFields(string caseSampleFile)
        {
            if (string.IsNullOrWhiteSpace(caseSampleFile))
            {
                return new List<Dictionary<string, string>>();
            }

            var json = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(File.ReadAllText(caseSampleFile));
            return json;
        }
    }
}
