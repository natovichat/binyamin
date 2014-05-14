using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace LightSwitchApplication
{
    public partial class ActivityView
    {
        partial void HebrewDate_Compute(ref string result)
        {
            result = ConvertToHebrewDate(this.ActivityDate);
        }

        private string ConvertToHebrewDate(DateTime dateTime)
        {
            
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("he-IL");

            ci.DateTimeFormat.Calendar = new System.Globalization.HebrewCalendar();

            return dateTime.ToString("dd MMMM", ci);
        }

        partial void DayInWeek_Compute(ref string result)
        {
            if (this.ActivityDate != null)
            {
                result = Convert(this.ActivityDate.DayOfWeek);
            }

        }

        public string Convert(DayOfWeek day)
        {
            string retVal = "";

            switch (day)
            {
                case DayOfWeek.Friday:
                    return "ו";

                case DayOfWeek.Monday:
                    return "ב";

                case DayOfWeek.Saturday:
                    return "ז";

                case DayOfWeek.Sunday:
                    return "א";

                case DayOfWeek.Thursday:
                    return "ה";

                case DayOfWeek.Tuesday:
                    return "ג";

                case DayOfWeek.Wednesday:
                    return "ד";

                default:
                    break;
            }




            return retVal;
        }
        public static  string CSVHeader()
        {
            string CSV = "";
               CSV += "סטטוס" + ",";
            CSV += "סוג פעילות" + ",";
            CSV += "תאריך" + ",";
            CSV += "תאריך עברי" + ",";
            
            CSV += " בית ספר"+",";
            CSV += "שם תוכנית" + ",";
            CSV += "נושא" + ",";
            CSV += "מקום" + ",";
            CSV += "מחוז" + ",";
            CSV += "סוג בית ספר" + ",";
            CSV += "קהל" + ",";
            
            CSV += "שעת פעילות" + ",";
            CSV += "סבבים" + ",";
            CSV += "מדריכים" + ",";
            CSV += "כיתות" + ",";
            CSV += "איש קשר" + ",";
            CSV += "טלפון איש קשר" + ",";
            CSV += "הערות" + ",";
            CSV += "מדריכים משובצים"+ ",";
            return CSV;
        }
        public string ToCSV()
        {
            string CSV="";

            CSV += ActivityStatusId + ",";
            CSV += ActivityType + ",";
            CSV += ActivityDate.ToShortDateString() + ",";
            CSV += ConvertToHebrewDate(ActivityDate) + ",";
            CSV += SchoolName +" "+SchoolPartName+ ",";
            CSV += TopicCollection + ",";
            CSV += Topic + ",";
            CSV += ActivityPlace + ",";
            CSV += District + ",";
            CSV += SchoolType + ",";
            CSV += Audience + ",";
            CSV += ActivityTime + ",";
            CSV += TotalNumberOfRounds + ",";
            CSV += TotalNumberOfCounselor + ",";
            CSV += TotalNumberOfClasses + ",";
            CSV += Contact + ",";
            CSV += ContactPhone + ",";
            CSV += Comments + ",";
            CSV += CounselorInActivity + ",";


            
            return CSV;

        }

        partial void ActivityTime_Compute(ref string result)
        {
            result = StartTime.ToShortTimeString() + "-" + EndTime.ToShortTimeString();

        }
    }

}
