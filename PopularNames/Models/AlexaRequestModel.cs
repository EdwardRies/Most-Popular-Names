using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PopularNames.Models
{
    [JsonObject]
    public class AlexaRequestModel
    {
        public AlexaRequestModel()
        {
            this.Session = new AlexaSession();
            this.Request = new AlexaRequest();
            this.Version = "1.0";
        }

        [JsonProperty("session")]
        public AlexaSession Session { get; set; }

        [JsonProperty("request")]
        public AlexaRequest Request { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonObject("session")]
        public class AlexaSession
        {
            public AlexaSession()
            {
                this.Application = new AlexaApplication();
                this.Attributes = new AlexaAttributes();
                this.User = new AlexaUser();
            }

            [JsonProperty("sessionId")]
            public string SessionId { get; set; }

            [JsonProperty("application")]
            public AlexaApplication Application { get; set; }

            [JsonProperty("attributes")]
            public AlexaAttributes Attributes { get; set; }

            [JsonProperty("user")]
            public AlexaUser User { get; set; }

            [JsonProperty("new")]
            public bool New { get; set; }

            #region Sub Classes
            [JsonObject("application")]
            public class AlexaApplication
            {
                [JsonProperty("applicationId")]
                public string ApplicationId { get; set; }
            }

            [JsonObject("attributes")]
            public class AlexaAttributes
            {
            }

            [JsonObject("user")]
            public class AlexaUser
            {
                [JsonProperty("userId")]
                public string UserId { get; set; }

                //[JsonProperty("accessToken")]
                //public string AccessToken { get; set; }
            }
            #endregion
        }

        [JsonObject("request")]
        public class AlexaRequest
        {
            public AlexaRequest()
            {
                this.Intent = new AlexaIntent();
            }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("requestId")]
            public string RequestId { get; set; }

            [JsonProperty("locale")]
            public string Locale { get; set; }

            private string _timestampEpoch;
            private double _timestamp;
            [JsonProperty("timestamp")]
            public string TimestampEpoch
            {
                get
                {
                    return _timestampEpoch;
                }
                set
                {
                    _timestampEpoch = value;

                    if (Double.TryParse(value, out _timestamp) && _timestamp > 0)
                        Timestamp = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(_timestamp);
                    else
                    {
                        var timeStamp = DateTime.MinValue;
                        if (DateTime.TryParse(_timestampEpoch, out timeStamp))
                            Timestamp = timeStamp.ToUniversalTime();
                    }
                }
            }

            [JsonIgnore]
            public DateTime Timestamp { get; set; }

            [JsonProperty("intent")]
            public AlexaIntent Intent { get; set; }

            #region Sub Classes
            [JsonObject("intent")]
            public class AlexaIntent
            {
                [JsonProperty("name")]
                public string Name { get; set; }

                [JsonProperty("slots")]
                public dynamic DynamicSlots { get; set; }

                [JsonIgnore]
                public List<KeyValuePair<string, string>> Slots
                {
                    get
                    {
                        var slotList = new List<KeyValuePair<string, string>>();
                        if (this.DynamicSlots == null)
                        {
                            return slotList;
                        }

                        foreach (var slot in DynamicSlots.Children())
                        {
                            if (slot.First.value != null)
                                slotList.Add(new KeyValuePair<string, string>(slot.First.name.ToString(), slot.First.value.ToString()));
                        }

                        return slotList;
                    }
                }

            }
            #endregion
        }

    }
}