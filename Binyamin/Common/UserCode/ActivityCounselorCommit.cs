using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace LightSwitchApplication
{
    public partial class ActivityCounselorCommit
    {
        partial void ApprovedSummary_Compute(ref string result)
        {
            result = "ממתין לאישור";
            for (int i = 0; i < this.Activity.ActivityCounselors.Count(); i++)
            {
                ActivityCounselor actCons = this.Activity.ActivityCounselors.ElementAt<ActivityCounselor>(i);
                if (actCons.Counselor.Id == this.Counselor.Id)
                {
                    if (actCons.Assigned)
                        result = "אושר";
                }

            }

        }
    }
}
