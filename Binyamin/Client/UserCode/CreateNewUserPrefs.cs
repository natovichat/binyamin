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
    public partial class CreateNewUserPrefs
    {
        partial void CreateNewUserPrefs_InitializeDataWorkspace(global::System.Collections.Generic.List<global::Microsoft.LightSwitch.IDataService> saveChangesTo)
        {
            // Write your code here.
            this.UserPrefsProperty = new UserPrefs();

            IContentItemProxy proxyPassword = this.FindControl("Password");
            proxyPassword.SetBinding(System.Windows.Controls.PasswordBox.PasswordProperty, "Value", System.Windows.Data.BindingMode.TwoWay);

            IContentItemProxy proxyPassword2 = this.FindControl("rePassword");
            proxyPassword2.SetBinding(System.Windows.Controls.PasswordBox.PasswordProperty, "Value", System.Windows.Data.BindingMode.TwoWay);
        }

        partial void CreateNewUserPrefs_Saved()
        {
            // Write your code here.
            this.Close(false);
            //Application.Current.ShowDefaultScreen(this.UserPrefsProperty);
        }

        partial void CreateNewUserPrefs_Created()
        {
            UserPrefs latest = null;
            foreach (UserPrefs prefs in this.DataWorkspace.ApplicationData.UserPrefs)
            {
                if (null == prefs)
                {
                    continue;
                }
               
                if (latest == null)
                    latest = prefs;
                else if ( latest.CreatedDate < prefs.CreatedDate )
                    latest = prefs;
           }

            if (latest != null)
            {
                this.UserPrefsProperty.MailAddress = latest.MailAddress;
                this.UserPrefsProperty.Password = latest.Password;
                this.UserPrefsProperty.rePassword = latest.rePassword;
                this.UserPrefsProperty.Port = latest.Port;
                this.UserPrefsProperty.Server = latest.Server;
                this.UserPrefsProperty.SSL = latest.SSL;
                this.UserPrefsProperty.DisaplayName = latest.DisaplayName;
            }
            else
            {
                this.UserPrefsProperty.Port = "587";
                this.UserPrefsProperty.Server = "smtp.gmail.com";
                this.UserPrefsProperty.SSL = "true";
            }
       }

        partial void CreateNewUserPrefs_Saving(ref bool handled)
        {
            this.UserPrefsProperty.CreatedDate = DateTime.Now;

        }

        //partial void rePassword_Validate(ScreenValidationResultsBuilder results)
        //{
        //    if (this.UserPrefsProperty.rePassword != this.UserPrefsProperty.Password)
        //    {
        //        results.AddPropertyError("הסיסמאות אינן תואמותץ אנא חזור שנית");
        //    }

        //}
        
    }
}