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
    public partial class MembersEnter2
    {
        partial void MembersEnter2_InitializeDataWorkspace(global::System.Collections.Generic.List<global::Microsoft.LightSwitch.IDataService> saveChangesTo)
        {
            // Write your code here.
            this.AuthenticationDetailProperty = new AuthenticationDetail();
        }

        partial void MembersEnter2_Saved()
        {
            // Write your code here.
            this.Close(false);
            Application.Current.ShowDefaultScreen(this.AuthenticationDetailProperty);
        }

        partial void Enter_Execute()
        {
            int id=12,num=0;
            bool flag = false;

            Counselor cons = null;
           //  cons = this.DataWorkspace.ApplicationData.CounselorByUserPass(this.AuthenticationDetailProperty.UserName.ToString(), this.AuthenticationDetailProperty.Password.ToString()).First<Counselor>();
            foreach (Counselor cons1 in this.DataWorkspace.ApplicationData.CounselorByUserPass(this.AuthenticationDetailProperty.UserName.ToString(), this.AuthenticationDetailProperty.Password.ToString()))
            {
              num++;
            }
            if (num > 0)
            {
                cons = this.DataWorkspace.ApplicationData.CounselorByUserPass(this.AuthenticationDetailProperty.UserName.ToString(), this.AuthenticationDetailProperty.Password.ToString()).First<Counselor>();
                id = cons.Id;
                flag = true;
            }
            else
            {
                this.ShowMessageBox("שם משתמש ו/או ססמא לא נכונים");
                flag = false;
                    
            }







            if (flag != false)
            {
                this.Application.ShowPastActivitysOfCounselor(id);
            }


        }
    }
}