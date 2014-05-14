using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace LightSwitchApplication
{
    public partial class Round
    {
     
        partial void Start_Stop_Compute(ref string result)
        {
            string start_time = this.StartTime.HasValue ? this.StartTime.Value.ToShortTimeString() :  "";
            string stop_time = this.StopTime.HasValue ? this.StopTime.Value.ToShortTimeString() :  "";

            result = start_time + "-" + stop_time; 

        }

        partial void Round_Created()
        {
            this.NumberOfStudentInMeeting = 35;
        }
    }
}
