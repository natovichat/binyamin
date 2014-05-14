using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace LightSwitchApplication
{
    public partial class SummaryYear
    {
        public static string CSVHeader()
        {
            string CSV = "";

            CSV += " תאריך" + ",";
            CSV += "מספר פעילויות" + ",";
            CSV += "מספר כיתות" + ",";
            CSV += "מספר מדריכים" + ",";
            return CSV;
        }
        public string ToCSV()
        {
            string CSV = "";
            CSV += ActivityDate + ",";
            CSV += NumOfLectures + ",";
            CSV += TotalNumberOfClasses + ",";
            CSV += TotalNumberOfCounselor + ",";



            return CSV;

        }
    }
}
