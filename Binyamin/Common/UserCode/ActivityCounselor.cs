using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace LightSwitchApplication
{
    public partial class ActivityCounselor
    {

        partial void CounselorPhoneNumber1_Compute(ref string result)
        {
            result = this.Counselor.PhoneNumber1;
        }

        
        partial void Activity_Changed()
        {
            if (Activity != null)
            {
                this.NumberOfRound = this.Activity.NumberOfRounds;
            }
            else
            {
                this.NumberOfRound = 0;
            }
        }

        partial void ActivityDate_Compute(ref DateTime result)
        {
            result = this.Activity.ActivityDate;

        }

        partial void CounselorPhoneNumber2_Compute(ref string result)
        {
            result = this.Counselor.PhoneNumber2;

        }
    }
}
