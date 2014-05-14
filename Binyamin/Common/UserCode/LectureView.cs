using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace LightSwitchApplication
{
    public partial class LectureView
    {
        public static string CSVHeader()
        {
            string CSV = "";
            CSV += "תוכנית" + ",";
            CSV += "נושא פעילות" + ",";
            CSV += "מס' פעילויות" + ",";
            CSV += "מדריכים" + ",";
            CSV += "כיתות" + ",";
            CSV += "סבבים" + ",";
            CSV += "סוג פעילות" + ",";
            return CSV;
        }
        public string ToCSV()
        {
            string CSV = "";

          
            CSV += this.ActivityTopicCollection + ",";
            CSV += this.ActivityTopic + ",";
            CSV += this.NumberOfActivities  + ",";
            CSV += this.NumberOfClasses + ",";
            CSV += this.NumberOfCounselors + ",";
            CSV += this.NumberOfRounds + ",";
            CSV += this.ActivityType + ",";

            return CSV;

        }

    }
}
