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
    public partial class webpage
    {

        private void webControlAvailable(object sender, ControlAvailableEventArgs e)
        {
            ((System.Windows.Controls.WebBrowser)e.Control).Navigate(new Uri("http://news.bbc.co.uk"));
        }

        partial void webpage_Activated()
        {
            this.FindControl("prpWebPage").ControlAvailable += webControlAvailable;

        }

    }
}
