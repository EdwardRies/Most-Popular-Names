using System;
using System.Collections.Generic;
using System.Linq;

namespace PopularNames.Utilities
{
    public static class StaticText
    {
        public static string Title = "Most Popular Names ";
        public static string IntentType = "IntentType";
        public static string PromptForInput = "Please specify gender and year.";
        public static string RepromptForInput = "What do you want to do? ";

        public static string MissingGender = "I didn't hear the gender. Please specify gender and year.";
        public static string UnknownYear = "I do not have information on the requested year. Currently I have information from 1916 to present day.";
        public static string HelpMessage = "When asking questions you need to specify the gender and year. For example, what is the most popular boy name in nineteen seventy.";
    }
}
