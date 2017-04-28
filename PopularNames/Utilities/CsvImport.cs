using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using PopularNames.Models;
using static PopularNames.Utilities.CsvFileReader;

namespace PopularNames.Utilities
{
    public class CsvImport
    {
        public static List<Entry> GetData()
        {
            var data = new List<Entry>();
            var filePath = $"{Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory)}/App_Data/PopularNames.csv";

            using (var reader = new CsvFileReader(filePath))
            {
                var row = new CsvRow();
                while (reader.ReadRow(row))
                {
                    var entry = new Entry() { Year = row[0] };
                    entry.Girls.Add(row[1]);
                    entry.Girls.Add(row[2]);
                    entry.Girls.Add(row[3]);
                    entry.Girls.Add(row[4]);
                    entry.Girls.Add(row[5]);
                    entry.Boys.Add(row[6]);
                    entry.Boys.Add(row[7]);
                    entry.Boys.Add(row[8]);
                    entry.Boys.Add(row[9]);
                    entry.Boys.Add(row[10]);
                    data.Add(entry);
                }
            }

            return data;
        }
    }
}

