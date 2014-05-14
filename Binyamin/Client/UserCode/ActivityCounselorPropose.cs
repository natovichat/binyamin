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
    public partial class ActivityCounselorPropose
    {
        // add activity button
        partial void AddActivity_Execute()
        {
            if (this.Activities.SelectedItem != null)
            {
                int actId = this.Activities.SelectedItem.Id;

                for (int j = 0; j < this.ActivityCounselorCommits.Count(); j++)
                {
                    if (actId.Equals(this.ActivityCounselorCommits.ElementAt<ActivityCounselorCommit>(j).Activity.Id))
                    {
                        this.ShowMessageBox("הפעילות כבר נוספה ");
                        return;
                    }
                }

                ActivityCounselorCommit activityCounselorCommit = ActivityCounselorCommits.AddNew();
                activityCounselorCommit.Activity = this.Activities.SelectedItem;
                activityCounselorCommit.Counselor = this.Counselor;
            }
        }

        partial void RemoveActivity_Execute()
        {
            if (this.ActivityCounselorCommits.SelectedItem != null)
            {
                int actId = this.ActivityCounselorCommits.SelectedItem.Activity.Id;
                bool flag = true;

                for (int i = 0; i < this.ActivityCounselorCommits.SelectedItem.Activity.ActivityCounselors.Count(); i++)
                {
                    ActivityCounselor actCons = this.ActivityCounselorCommits.SelectedItem.Activity.ActivityCounselors.ElementAt<ActivityCounselor>(i);
                    if (actCons.Activity.Id == actId)
                    {
                        if (actCons.Assigned)
                        {
                            this.ShowMessageBox("לא ניתן להסיר עקב שיבוץ. נא לפנות לגורם המתאים");
                            flag = false;
                        }
                        else
                        {
                            this.ActivityCounselorCommits.SelectedItem.Activity.ActivityCounselors.ElementAt<ActivityCounselor>(i).Delete();
                        }
                    }
                }

                if (flag)
                {
                    ActivityCounselorCommits.DeleteSelected();
                }

                //Activity act = Activities.AddNew();
                //act = ActivityCounselorCommits.SelectedItem.Activity;

                //ActivityCounselorCommits.RemoveSelected
            }
        }
    }
}
