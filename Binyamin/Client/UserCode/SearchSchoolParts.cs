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
    public partial class SearchSchoolParts
    {
        partial void SearchSchoolParts_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            foreach (var current_scholl in SchoolPart)
            {
                foreach (var current_order in Orders)
                {
                    if (current_scholl.FullName == current_order.SchoolPart.FullName)
                    {

                    }
                }
            }

        }
    }
}
