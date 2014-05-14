using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace LightSwitchApplication
{
    public partial class CounselorView
    {
        public static string CSVHeader()
        {
            string CSV = "";
            CSV += "שם המדריך" + ",";
            CSV += "נושא" + ",";
            CSV += "שם תוכנית" + ",";
            CSV += " בית ספר" + ",";
            CSV += "עיר" + ",";
            CSV += "תאריך" + ",";
            CSV += "חודש" + ",";
            CSV += "קהל" + ",";
        
           
            return CSV;
        }
        public string ToCSV()
        {
   

            string CSV = "";
            CSV += CounselorInActivity + ",";
       
            CSV += TopicCollection + ",";
            CSV += Topic + ",";
            CSV +=SchoolName + ",";
            CSV+= ActivityPlace +  ",";
            CSV += ActivityDate.ToShortDateString() + ",";
            CSV += Month + ",";
            CSV += Audience + ",";
        


            return CSV;

        }
    }
}
