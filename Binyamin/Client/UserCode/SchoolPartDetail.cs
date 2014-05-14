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
    public partial class SchoolPartDetail
    {
        partial void SchoolPart_Loaded(bool succeeded)
        {
            // Write your code here.
            this.SetDisplayNameFromEntity(this.SchoolPart);
        }

        partial void SchoolPart_Changed()
        {
            // Write your code here.
            this.SetDisplayNameFromEntity(this.SchoolPart);
        }

        partial void SchoolPartDetail_Saved()
        {
            // Write your code here.
            this.SetDisplayNameFromEntity(this.SchoolPart);
        }
    }
}