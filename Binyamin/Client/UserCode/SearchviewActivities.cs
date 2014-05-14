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
    public partial class SearchviewActivities
    {


        partial void ActivityAddAndEditNew_Execute()
        {
            if (viewActivities.SelectedItem != null)
            {
                Application.ShowCreateNewOrEditActivity(viewActivities.SelectedItem.Id);
            }

        }

        partial void DuplicateActivity_Execute()
        {
            Activity duplicateFrom = null;
            if (viewActivities.SelectedItem != null)
            {
                duplicateFrom = this.DataWorkspace.ApplicationData.Activities_Single(viewActivities.SelectedItem.Id);

            }
            if (duplicateFrom != null)
            {
                Activity duplicateTo = this.DataWorkspace.ApplicationData.Activities.AddNew();

                duplicateFrom.DuplicateActivity(duplicateTo);
            }

        }
    

        partial void AssignProcess_Execute()
        {
            if (this.viewActivities.SelectedItem != null)
            {
                Application.ShowAssigningProcess(this.viewActivities.SelectedItem.Id);
            }

        }

        partial void FinishAssign_Execute()
        {
            if (viewActivities.SelectedItem != null)
            {
                Application.ShowRoundsListDetail(viewActivities.SelectedItem.Id);
            }


        }


        partial void ActivityEditSelected_Execute()
        {
            if (viewActivities.SelectedItem != null)
            {
                Application.ShowCreateNewOrEditActivity(viewActivities.SelectedItem.Id);
            }

        }

        partial void SearchviewActivities_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            this.viewActivityFromDate = DateTime.Now;

            this.viewActivityToDate = DateTime.Now.AddMonths(1);

        }

    
       

    }
}
