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
    public partial class MembersEnter
    {
        partial void AuthenticationDetail_Loaded(bool succeeded)
        {
            // Write your code here.
            this.SetDisplayNameFromEntity(this.AuthenticationDetail);
        }

        partial void AuthenticationDetail_Changed()
        {
            // Write your code here.
            this.SetDisplayNameFromEntity(this.AuthenticationDetail);
        }

        partial void MembersEnter_Saved()
        {
            // Write your code here.
            this.SetDisplayNameFromEntity(this.AuthenticationDetail);
        }
    }
}