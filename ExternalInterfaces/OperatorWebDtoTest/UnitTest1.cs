using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using NetworkConsistency.ExternalInterfaces.OperatorWebDto.ValueObjects;
using Xunit;

namespace OperatorWebDtoTest
{
    public class FailureReportDtoTest
    {
        private static readonly Guid[] TestUids = {
            Guid.Parse("08e4ef7c-74ba-417e-a74e-c06b6a42718d"),
            Guid.Parse("0f3b4e6e-19ea-437b-a632-71627bbd0d10"),
            Guid.Parse("69ba27e4-bbe5-4115-b02e-e84eee0b2fed"),
            Guid.Parse("7c8d1751-8605-4491-a9b8-949d9ced1034"),
            Guid.Parse("cd5cce61-d889-47e8-94f9-4e01082d885c"),
            Guid.Parse("09cc9f3d-c98c-47cd-a434-2d90e2c39aa5")
        };
        
        private static string GetSerializedReports()
        {
            return @$"[
                {{""uid"":""{TestUids[0]}"",""creationDate"":""2021-01-01T20:10:59"",""inWorkDate"":null,""finishDate"":null,""failedSensors"":[null,null,null,null,null]}},
                {{""uid"":""{TestUids[1]}"",""creationDate"":""2021-02-01T00:59:57"",""inWorkDate"":null,""finishDate"":null,""failedSensors"":[null,null,null]}},
                {{""uid"":""{TestUids[2]}"",""creationDate"":""2021-01-03T10:37:18"",""inWorkDate"":null,""finishDate"":null,""failedSensors"":[null,null]}},
                {{""uid"":""{TestUids[3]}"",""creationDate"":""2021-01-04T16:15:09"",""inWorkDate"":null,""finishDate"":null,""failedSensors"":[null,null,null,null,null,null,null,null,null,null,null,null,null,null]}},
                {{""uid"":""{TestUids[4]}"",""creationDate"":""2021-05-01T09:42:14"",""inWorkDate"":null,""finishDate"":null,""failedSensors"":[null,null,null,null,null,null,null,null]}},
                {{""uid"":""{TestUids[5]}"",""creationDate"":""2021-06-07T12:00:09"",""inWorkDate"":null,""finishDate"":null,""failedSensors"":[null,null,null,null,null,null,null,null,null]}}
           ]";
        }
        
        [Fact]
        public void TestDeserialization()
        {
            var source = GetSerializedReports();
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var serializedData = JsonSerializer.Deserialize<FailureReportDto[]>(source, options);

            Assert.NotNull(serializedData);
            for (var i = 0; i < TestUids.Length; i++)
            {
                Assert.Equal(TestUids[i], serializedData[i].UID);
            }
        }
    }
}