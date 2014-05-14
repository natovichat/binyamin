using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;
using Microsoft.LightSwitch.Client;

namespace LightSwitchApplication
{
    public partial class CreateNewOrEditActivity
    {
        partial void CreateNewOrEditActivity_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            if (ActivityId.HasValue)
            {
                DateTime start = DateTime.Now;
                this.ActivityProperty = Activity;

                DateTime stop = DateTime.Now;

                TimeSpan diff = stop.Subtract(start);
             
            }
            else
            { 
                this.ActivityProperty = new Activity();
            }

        }

     

        partial void SelectedSchool_Changed()
        {
            if (this.SelectedSchool != null)
            {
                this.ActivityProperty.SchoolPart = this.DataWorkspace.ApplicationData.SchoolParts_Single(this.SelectedSchool.SchoolId);
            }
        }

      

      
       
    }
}