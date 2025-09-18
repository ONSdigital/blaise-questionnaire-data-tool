namespace Blaise.Questionnaire.Data.Tool.Helpers.Models
{
    using System;
    using System.Collections.Generic;
    using Blaise.Nuget.Api.Contracts.Models;

    public class CaseDataModel
    {
        private static readonly Random _random = new Random();

        public CaseDataModel(int primaryKey, Dictionary<string, string> sampleDataFields = null)
        {
            PrimaryKey = primaryKey.ToString();
            DataFields = InitialiseCaseDataFields(sampleDataFields);
        }

        public string PrimaryKey { get; set; }

        public string Uac1 => GenerateRandomUac();

        public string Uac2 => GenerateRandomUac();

        public string Uac3 => GenerateRandomUac();

        public Dictionary<string, string> DataFields { get; }

        public CaseModel ToCaseModel(string primaryKeyFieldName = "QID.Serial_Number")
        {
            var primaryKeyDict = new Dictionary<string, string>
            {
                { primaryKeyFieldName, PrimaryKey },
            };
            return new CaseModel(primaryKeyDict, DataFields);
        }

        public Dictionary<string, string> InitialiseCaseDataFields(Dictionary<string, string> dataFields)
        {
            if (dataFields == null)
            {
                dataFields = new Dictionary<string, string>();
                AddDefaultValues(dataFields); // Add default values only if no sample data is provided
            }
            else
            {
                dataFields["qid.serial_number"] = PrimaryKey; // Ensure the primary key is always set
            }

            return dataFields;
        }

        private static string GenerateRandomUac()
        {
            return _random.Next(1000, 9999).ToString();
        }

        private void AddDefaultValues(Dictionary<string, string> dataFields)
        {
            dataFields["qid.serial_number"] = PrimaryKey;
            dataFields["qdatabag.tla"] = "tla";
            dataFields["qdatabag.wave"] = "1";
            dataFields["qdatabag.telno"] = "07000000000";
            dataFields["qdatabag.telno2"] = "07000000000";
            dataFields["qdatabag.telnoappt"] = "07000000000";
            dataFields["qdatabag.prem1"] = "prem1";
            dataFields["qdatabag.prem2"] = "prem2";
            dataFields["qdatabag.prem3"] = "prem3";
            dataFields["qdatabag.district"] = "district";
            dataFields["qdatabag.posttown"] = "posttown";
            dataFields["qdatabag.postcode"] = "np10 8xg";
            dataFields["qdatabag.uprn"] = "100100963961";
            dataFields["qdatabag.uprn_latitude"] = "51.566";
            dataFields["qdatabag.uprn_longitude"] = "-3.026";
            dataFields["qdatabag.samptitle"] = "samptitle";
            dataFields["qdatabag.sampfname"] = "sampfname";
            dataFields["qdatabag.sampsname"] = "sampsname";
            dataFields["qdatabag.name"] = "name";
            dataFields["qdatabag.fieldcase"] = "y";
            dataFields["qdatabag.priority"] = "1";
            dataFields["qdatabag.fieldteam"] = "fieldteam";
            dataFields["qdatabag.fieldregion"] = "Region 1";
            dataFields["qdatabag.wavecomdte"] = "01-01-2099";
            dataFields["qsample.orgname"] = "orgname";
            dataFields["qsample.address1"] = "address1";
            dataFields["qsample.address2"] = "address2";
            dataFields["qsample.locality"] = "locality";
            dataFields["qsample.townname"] = "townname";
            dataFields["qsample.postcode"] = "postcode";
            dataFields["qsample.telno"] = "07000000000";
            dataFields["qsample.telno2"] = "07000000000";
            dataFields["qhadmin.hout"] = string.Empty;
            dataFields["hout"] = string.Empty;
            dataFields["qhadmin.interviewer[1]"] = "interviewer1";
            dataFields["dmhsize"] = "2";
            dataFields["dmname[1]"] = "dmname1";
            dataFields["dmdteofbth[1]"] = "01-01-2000";
            dataFields["dmname[2]"] = "dmname2";
            dataFields["dmdteofbth[2]"] = "01-01-2000";
            dataFields["qhousehold.qhhold.person[1].title"] = "title";
            dataFields["qhousehold.qhhold.person[1].fstnme"] = "fstnme";
            dataFields["qhousehold.qhhold.person[1].surnme"] = "surnme";
            dataFields["qrotate.rdmktnind"] = string.Empty;
            dataFields["qrotate.rhout"] = string.Empty;
            dataFields["catimana.caticall.regscalls[1].whomade"] = "whomade";
            dataFields["catimana.caticall.regscalls[5].dialresult"] = "1";
            dataFields["catimana.caticall.regscalls[1].whomade"] = "whomade";
            dataFields["catimana.caticall.regscalls[5].dialresult"] = "1";
            dataFields["datamodelname"] = "datamodelname";
            dataFields["offlinecapi.towhom"] = "rich";
        }
    }
}
