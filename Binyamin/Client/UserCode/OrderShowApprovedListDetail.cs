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
    public partial class OrderShowApprovedListDetail
    {
        partial void ChangeStatus_Execute()
        {
            if (this.OrderShowApproved.SelectedItem == null)
                return;

            this.OrderShowApproved.SelectedItem.Approved = null;
            this.OrderShowApproved.SelectedItem.ChosenDate = null;
            this.DataWorkspace.ApplicationData.SaveChanges();
            this.Refresh();
        }
    }
}
