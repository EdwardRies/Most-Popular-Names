using System;
using Newtonsoft.Json;

namespace PopularNames.Models
{
    [JsonObject]
    public class AlexaResponseModel
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("sessionAttributes")]
        public ResponseSessionAttributes Session { get; set; }

        [JsonProperty("response")]
        public ResponseAttributes Response { get; set; }

        public AlexaResponseModel()
        {
            Version = "1.0";
            Session = new ResponseSessionAttributes();
            Response = new ResponseAttributes();
        }

        public AlexaResponseModel(string outputSpeechText)
            : this()
        {
            Response.OutputSpeech.Text = outputSpeechText;
            Response.Card.Content = outputSpeechText;
        }

        public AlexaResponseModel(string outputSpeechText, bool shouldEndSession)
            : this()
        {
            Response.OutputSpeech.Text = outputSpeechText;
            Response.ShouldEndSession = shouldEndSession;

            if (shouldEndSession)
            {
                Response.Card.Content = outputSpeechText;
            }
            else
            {
                Response.Card = null;
            }
        }

        public AlexaResponseModel(string outputSpeechText, string cardContent)
            : this()
        {
            Response.OutputSpeech.Text = outputSpeechText;
            Response.Card.Content = cardContent;
        }

        /* Sub Classes */

        [JsonObject("sessionAttributes")]
        public class ResponseSessionAttributes
        {
            //[JsonProperty("customProperty")]
            //public int customProperty { get; set; }
        }

        [JsonObject("response")]
        public class ResponseAttributes
        {
            [JsonProperty("shouldEndSession")]
            public bool ShouldEndSession { get; set; }

            [JsonProperty("outputSpeech")]
            public OutputSpeechAttributes OutputSpeech { get; set; }

            [JsonProperty("card")]
            public CardAttributes Card { get; set; }

            [JsonProperty("reprompt")]
            public RepromptAttributes Reprompt { get; set; }

            public ResponseAttributes()
            {
                ShouldEndSession = false;
                OutputSpeech = new OutputSpeechAttributes();
                Card = new CardAttributes();
                Reprompt = new RepromptAttributes();
            }

            [JsonObject("outputSpeech")]
            public class OutputSpeechAttributes
            {
                [JsonProperty("type")]
                public string Type { get; set; }

                [JsonProperty("text")]
                public string Text { get; set; }

                [JsonProperty("ssml")]
                public string Ssml { get; set; }

                public OutputSpeechAttributes()
                {
                    Type = "PlainText";
                }
            }

            [JsonObject("card")]
            public class CardAttributes
            {
                [JsonProperty("type")]
                public string Type { get; set; }

                [JsonProperty("title")]
                public string Title { get; set; }

                [JsonProperty("content")]
                public string Content { get; set; }

                public CardAttributes()
                {
                    Type = "Simple";
                }
            }

            [JsonObject("reprompt")]
            public class RepromptAttributes
            {
                [JsonProperty("outputSpeech")]
                public OutputSpeechAttributes OutputSpeech { get; set; }

                public RepromptAttributes()
                {
                    OutputSpeech = new OutputSpeechAttributes();
                }
            }
        }

    }
}