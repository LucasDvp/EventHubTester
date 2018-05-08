using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHubTester.FileDealing
{
    /*
    "ID": "201803070726530203-TFS261959-webclienturl",
    "ComponentId": "234761",
    "TimeStamp": "2018-03-06 23:27:56.000",
    "Type": "Ended",
    "DataUpdatedAt": "2018-03-06 23:28:16.000",
    "CreatedBy": "jswymer@microsoft.com",
    "RepositoryAccount": "MicrosoftDocs",
    "RepositoryName": "dynamics365smb-devitpro",
    "RepositoryId": "9c70e7fc-a0c3-250f-2ec7-0fea6867c40f",
    "BranchName": "TFS261959-webclienturl",
    "Status": "SucceededWithWarning",
    "Target": "Publish",
    "SourceBranchName": "",
    "TargetBranchName": ""
     */
    class BuildMsgTemplate
    {
        [JsonProperty("ID")]
        public string id { get; set; }
        [JsonProperty("ComponentId")]
        public string componentId { get; set; }
        [JsonProperty("TimeStamp")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime timeStamp { get; set; }
        [JsonProperty("Type")]
        public string type { get; set; }
        [JsonProperty("DataUpdatedAt")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime updateTime { get; set; }
        [JsonProperty("CreatedBy")]
        public string createdBy { get; set; }
        [JsonProperty("RepositoryAccount")]
        public string repoAccount { get; set; }
        [JsonProperty("RepositoryName")]
        public string repoName { get; set; }
        [JsonProperty("RepositoryId")]
        public string repoId { get; set; }
        [JsonProperty("BranchName")]
        public string branchName { get; set; }
        [JsonProperty("Status")]
        public string status { get; set; }
        [JsonProperty("Target")]
        public string target { get; set; }
        [JsonProperty("SourceBranchName")]
        public string sourceBranchName { get; set; }
        [JsonProperty("TargetBranchName")]
        public string targetBranchName { get; set; }
        public BuildMsgTemplate() { }
    }
}
