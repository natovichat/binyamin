using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;
namespace LightSwitchApplication
{
    public partial class AssignCounselorTemp
    {
        partial void OpenActivityCounselorPropose_Execute()
        {

            this.Application.ShowActivityCounselorPropose(this.Counselors.SelectedItem.Id, this.Counselors.SelectedItem.District.Id);

        }

        partial void OpenActivitysOfCounselor_Execute()
        {
            this.Application.ShowPastActivitysOfCounselor(this.Counselors.SelectedItem.Id);

        }

        partial void OpenFutureActivitysOfCounselor_Execute()
        {
            this.Application.ShowFutureActivitysOfCounselor(this.Counselors.SelectedItem.Id);
        }
    }
}