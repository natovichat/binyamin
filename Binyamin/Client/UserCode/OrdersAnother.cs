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
    public partial class OrdersAnother
    {
        partial void Orders_Loaded(bool succeeded)
        {
            // Write your code here.
            this.SetDisplayNameFromEntity(this.Orders);
        }

        partial void Orders_Changed()
        {
            // Write your code here.
            this.SetDisplayNameFromEntity(this.Orders);
        }

        partial void OrdersAnother_Saved()
        {
            // Write your code here.
            this.SetDisplayNameFromEntity(this.Orders);
        }
    }
}