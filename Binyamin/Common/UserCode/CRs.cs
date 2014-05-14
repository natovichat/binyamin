using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace LightSwitchApplication
{
    public partial class CRs
    {
        partial void CRs_Created()
        {
            this.Status = "Open"; 

        }

        partial void Status_Changed()
        {
            if (this.Status == "Close")
            {
                this.DateOfReolved = DateTime.Today;
            }
            else
            {
                this.DateOfReolved = null;
            }
        }
    }
}
