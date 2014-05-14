using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace LightSwitchApplication
{
    public partial class viewActivity
    {
        partial void HebrewDate_Compute(ref string result)
        {
            DateTime date = (DateTime)this.ActivityDate;
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("he-IL");

            ci.DateTimeFormat.Calendar = new System.Globalization.HebrewCalendar();

            result = date.ToString("dd MMMM", ci);

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
                    return "שבת";

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

        partial void Status_Compute(ref string result)
        {
            if (this.ActivityStatus.HasValue)
            {
                ActivityStatusEnum status = (ActivityStatusEnum)this.ActivityStatus;

                result = Convert(status);
            }

        }

        private string Convert(ActivityStatusEnum status)
        {
            switch (status)
            {
                case ActivityStatusEnum.Create:
                    return "חדשה";
                case ActivityStatusEnum.Assigned:
                    return "משובצת";
                case ActivityStatusEnum.Finished:
                    return "הסתיימה";
                case ActivityStatusEnum.Cancel:
                    return "בוטלה";
                case ActivityStatusEnum.Postponed:
                    return "נדחתה";
                default:
                    throw new NotSupportedException();
            }
        }

        public enum ActivityStatusEnum
        {
            Create=1,
            Assigned=2,
            Finished=3,
            Cancel=4,
            Postponed=5
        }
    }
}
