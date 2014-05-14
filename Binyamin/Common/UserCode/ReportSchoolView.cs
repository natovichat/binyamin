using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace LightSwitchApplication
{
    public partial class ReportSchoolView
    {
        public static string CSVHeader()
        {
            string CSV = "";
            CSV += "תאריך" + ",";
            CSV += "שם תוכנית" + ",";
            CSV += "נושא" + ",";
            CSV += "סוג פעילות" + ",";
            CSV += "קהל" + ",";
            CSV += "מספר כיתות" + ",";
            CSV += "מספר מדריכים" + ",";
            CSV += "סבבים" + ",";
            CSV += "סוג בית ספר" + ",";
          
            CSV += "איש קשר" + ",";
            CSV += "טלפון איש קשר" + ",";
    
            return CSV;
        }
        public string ToCSV()
        {
            string CSV = "";
            CSV += ActivityDate.ToShortDateString() + ",";
            CSV += TopicCollection + ",";
            CSV += Topic + ",";
            CSV += ActivityType + ",";
            CSV += Audience + ",";
            CSV += TotalNumberOfClasses + ",";
            CSV += TotalNumberOfCounselor + ",";
            CSV += TotalNumberOfRounds + ",";
            CSV += SchoolType + ",";
          
            CSV += Contact + ",";
            CSV += ContactPhone + ",";


            return CSV;

        }
    
    }
}
