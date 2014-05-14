using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace LightSwitchApplication
{
    public partial class Counselor
    {
        partial void FullName_Compute(ref string result)
        {
            string firstName = this.FirstName!=null ? this.FirstName : "";
            string lastName = this.LastName!=null ? this.LastName : "";

            result = firstName + " " + lastName;
                 

        }

        partial void NumberOfActivitiesThisYear_Compute(ref int result)
        {
            int numOfActivitiesThisYear = 0;
            DateTime startDate;
            int month = DateTime.Now.Month;
            if (month == 09 || month == 10 || month == 11 || month == 12)
            {
                startDate = new DateTime(DateTime.Now.Year, 09, 1);
            }
            else
            {
                startDate = new DateTime(DateTime.Now.Year - 1, 09, 1);
            }


            int actConTaples = this.ActivityCounselors.Count<ActivityCounselor>();
            for (int i = 0; i < actConTaples; i++)
            {
                if (this.ActivityCounselors.ElementAt<ActivityCounselor>(i).Assigned
                    && this.ActivityCounselors.ElementAt<ActivityCounselor>(i).Activity.ActivityDate >= startDate)
                //&& this.ActivityCounselors.ElementAt<ActivityCounselor>(i).Activity.ActivityDate <= DateTime.Now)
                {
                    numOfActivitiesThisYear++;
                }
            }
            result = numOfActivitiesThisYear;
        }

        partial void Counselor_Created()
        {
            this.MaxAcitivitiesPerYear = 50;
        }
    }
}
