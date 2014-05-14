using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace LightSwitchApplication
{
    public partial class UserPrefs
    {
        partial void rePassword_Validate(EntityValidationResultsBuilder results)
        {
            if (rePassword != Password)
            {
                results.AddPropertyError("הסיסמאות אינן תואמות, אנא חזור שנית");
            }

        }
    }
}
