using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;
using System.Windows.Browser;

namespace LightSwitchApplication
{
    public partial class WebLink
    {
        partial void WebLink_Activated()
        {
            this.FindControl("prpWebPage").ControlAvailable += webControlAvailable;
        }

        private void webControlAvailable(object sender, ControlAvailableEventArgs e)
        {
            ((System.Windows.Controls.WebBrowser)e.Control).Navigate(new Uri("http://news.bbc.co.uk"));
            //string url = "http://www.google.com";
            //HtmlPage.Window.Navigate(new Uri(url), "", "toolbar=yes,location=no,status=no,menubar=yes,resizable=yes");

            
        }
    }
}
