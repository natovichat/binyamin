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
    public partial class CreateNewActivity
    {
        partial void CreateNewActivity_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            if (activityId.HasValue)
            {
                this.ActivityProperty = DataWorkspace.ApplicationData.Activities_Single(activityId);
            }
            else
            {
                // Write your code here.
                this.ActivityProperty = new Activity();
            }
        }

        partial void CreateNewActivity_Saved()
        {
            // Write your code here.
            this.Close(false);
            Application.Current.ShowDefaultScreen(this.ActivityProperty);
        }
    }
}