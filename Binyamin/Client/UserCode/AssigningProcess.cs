using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;
using Microsoft.LightSwitch.Client;

namespace LightSwitchApplication
{
    public partial class AssigningProcess
    {
        partial void AssigningProcess_InitializeDataWorkspace(global::System.Collections.Generic.List<global::Microsoft.LightSwitch.IDataService> saveChangesTo)
        {

            this.ActivityProperty = this.Activity;
 
            DateTime startDateOfThisYear;
            int month = DateTime.Now.Month;
            if (month == 09 || month == 10 || month == 11 || month == 12)
            {
                startDateOfThisYear = new DateTime(DateTime.Now.Year, 09, 1);
            }
            else
            {
                startDateOfThisYear = new DateTime(DateTime.Now.Year - 1, 09, 1);
            }

            //for the check if the counselor pass the number of activities this year
            this.startYearDay = startDateOfThisYear;

            this.counselorId = -1;
           
        }

        partial void AssigningProcess_Saved()
        {
            //Application.ShowSearchActivityViews();
            // Write your code here.
            //  this.Close(false);
            //Application.Current.ShowDefaultScreen(this.ActivityProperty);
        }

        partial void AddCounselor_Execute()
        {
            
            if (Counselors.SelectedItem != null)
            {
                    /*    int LastWeekActivityCount = 0, i =0;
                        int NextWeekActivityCount = 0;
                        //  Define the dates for 1 week from the date of the selected activity and 1 week before the selected activity
                        DateTime LastWeek = this.Activity.ActivityDate.Subtract(new TimeSpan(7, 0, 0, 0));
                        DateTime NextWeek;
                        DateTime TodaysDate = this.Activity.ActivityDate;
                        // Extract a list of activities that are assigned to the selected counselor
                        List<ActivityCounselor> ActivitiesList= Counselors.SelectedItem.ActivityCounselorsQuery.Execute().ToList();
                        ActivityCounselor indexx = ActivitiesList.FirstOrDefault(p => p.ActivityDate.Equals(TodaysDate));
                        if (indexx!=null)
                            // if the counselor already has an activity today: show a message
                            this.ShowMessageBox(Counselors.SelectedItem.FullName.ToString() + "\n" + "כבר מעביר היום פעילות במקום אחר!  ");
                        // count the number of activities that the counselor has in the weeks before and after today
                        for (i = 1; i <= 6; i++)
                        {
                            indexx = ActivitiesList.FirstOrDefault(p => p.ActivityDate.Equals(LastWeek.AddDays(i)));
                            if (indexx != null)
                            {   
                                ++LastWeekActivityCount;
                            }
                        }
                        NextWeek = LastWeek.AddDays(7);
                        //calculating next week activities
                        for (i = 1; i <= 6; i++)
                        {
                            indexx = ActivitiesList.FirstOrDefault(p => p.ActivityDate.Equals(NextWeek.AddDays(i)));
                            if (indexx != null)
                            {
                                ++NextWeekActivityCount;
                            }
                        }
                        this.ShowMessageBox(Counselors.SelectedItem.FullName.ToString() + "\n" + "מספר הפעילויות שהעביר בשבוע האחרון-  " + LastWeekActivityCount + " מספר הפעילויות בשבוע הבא-  " + NextWeekActivityCount);
                       */

                //////////////// get the selected counselor info /////////////////// 

                Counselor cons = this.Counselors.SelectedItem;
                int consId = cons.Id;

                // for the query ActivityCounselorByCounselor
                this.counselorId = consId;

                /////////////////////////////////////////////////////////////////////////

                //////////////// בדיקה אם כבר נוסף לפעילות //////////////////////////

                
                for (int j = 0; j < this.ActivityCounselors.Count(); j++)
                {
                    ActivityCounselor act = this.ActivityCounselors.ElementAt<ActivityCounselor>(j);

                    if (consId.Equals(act.Counselor.Id))
                    {
                        this.ShowMessageBox("המדריך כבר נוסף לפעילות!" );
                        return;
                    }

                }
                

                //////////////////////////////////////////////////////////////
 
                /* SLOW CODE */
                /*
                DateTime actDate = this.Activity.ActivityDate;

                for (int j = 0; j < cons.ActivityCounselors.Count(); j++)
                {
                    DateTime currentActDate = cons.ActivityCounselors.ElementAt<ActivityCounselor>(j).Activity.ActivityDate;
                    if (currentActDate.Equals(actDate))
                    {
                        System.Windows.MessageBoxResult result = this.ShowMessageBox("המריך כבר משובץ או מוצע לשיבוץ לפעילות ביום זה האם עדיין לשבצו?",
                                                                                     "פעילויות באותו היום", MessageBoxOption.YesNo);                      
                        if (result.Equals(System.Windows.MessageBoxResult.No))
                        {
                            return;
                        }
                        break;
                    }
                }
                */

                for (int j = 0; j < this.ActivityByDate.Count; j++)
                {
                    Activity tempAct = this.ActivityByDate.ElementAt<Activity>(j);
                    for (int i = 0; i < tempAct.ActivityCounselors.Count(); i++)
                    {
                        if (tempAct.ActivityCounselors.ElementAt<ActivityCounselor>(i).Counselor.Id == cons.Id)                         
                        {
                            System.Windows.MessageBoxResult result = this.ShowMessageBox("המדריך כבר משובץ או מוצע לפעילות ביום זה האם עדיין לשבצו?",
                                                                                    "פעילויות באותו היום", MessageBoxOption.YesNo);
                            if (result.Equals(System.Windows.MessageBoxResult.No))
                            {
                                return;
                            }
                            break;

                        }

                    }
                }
 
          

                //////////////// בדיקה אם עבר את המכסה השנתית ///////////////////


                if (cons.MaxAcitivitiesPerYear < this.ActivityCounselorsByCounselor.Count + 1)
                {
                    System.Windows.MessageBoxResult result = this.ShowMessageBox("המדריך עבר את מכסת הפעילויות השנתית שלו: "
                                                            + cons.MaxAcitivitiesPerYear.ToString() + "\n" 
                                                            + "האם עדיין לשבצו לפעילות?",
                                                            "חריגה ממספר פעילויות", MessageBoxOption.YesNo);
                    if (result.Equals(System.Windows.MessageBoxResult.No))
                    {
                        return;
                    }
                }
                 


                /////////////////////////////////////////////////////////////////////////


                //////////////////////////////////////////////////////////

             // assigning the counselor to the selected activity
                ActivityCounselor activityCounselor = ActivityCounselors.AddNew();
                activityCounselor.Counselor = Counselors.SelectedItem;
                activityCounselor.Activity = this.Activity;
            }
        }

        partial void RemoveCounselor_Execute()
        {
            ActivityCounselors.DeleteSelected();

        }

        partial void AddCommitedCounselor_Execute()
        {
            if (ActivityCounselorCommits.SelectedItem != null)
            {
                ///////////////////////////////////////////////////////////////////////

                Counselor cons1 = this.ActivityCounselorCommits.SelectedItem.Counselor;
                int consId = cons1.Id;
                // for the query ActivityCounselorByCounselor
                this.counselorId = consId;

                
                for (int j = 0; j < this.ActivityCounselors.Count(); j++ )
                 {
                     ActivityCounselor act = this.ActivityCounselors.ElementAt<ActivityCounselor>(j);

                     if (consId.Equals(act.Counselor.Id))
                     {
                         this.ShowMessageBox("המדריך כבר נוסף לפעילות!");
                         return;
                     }

                 }

                
                Counselor cons = this.ActivityCounselorCommits.SelectedItem.Counselor;

                for (int j = 0; j < this.ActivityByDate.Count; j++)
                {
                    Activity tempAct = this.ActivityByDate.ElementAt<Activity>(j);
                    for (int i = 0; i < tempAct.ActivityCounselors.Count(); i++)
                    {
                        if (tempAct.ActivityCounselors.ElementAt<ActivityCounselor>(i).Counselor.Id == cons.Id)
                        {
                            System.Windows.MessageBoxResult result = this.ShowMessageBox("המדריך כבר משובץ או מוצע לפעילות ביום זה האם עדיין לשבצו?",
                                                                                    "פעילויות באותו היום", MessageBoxOption.YesNo);
                            if (result.Equals(System.Windows.MessageBoxResult.No))
                            {
                                return;
                            }
                            break;
                        }

                    }
                }

                /* SLOW CODE */
                /*                
                //////////////////////////////////////////////////////////
                
                DateTime actDate = this.ActivityCounselorCommits.SelectedItem.Activity.ActivityDate;
                for (int j = 0; j < cons.ActivityCounselors.Count(); j++)
                {
                    DateTime currentActDate = cons.ActivityCounselors.ElementAt<ActivityCounselor>(j).Activity.ActivityDate;
                    if (currentActDate.Equals(actDate))
                    {
                        System.Windows.MessageBoxResult result = this.ShowMessageBox("המריך כבר משובץ או מוצע לפעילות ביום זה האם עדיין לשבצו?",
                                                                                     "פעילויות באותו היום", MessageBoxOption.YesNo);
                        if (result.Equals(System.Windows.MessageBoxResult.No))
                        {
                            return;
                        }
                        break;
                    }
                }
                */

                //////////////////////////////////////////////////////////


                //////////////// בדיקה אם עבר את המכסה השנתית ///////////////////


                if (cons1.MaxAcitivitiesPerYear < this.ActivityCounselorsByCounselor.Count + 1)
                {
                    System.Windows.MessageBoxResult result = this.ShowMessageBox("המדריך עבר את מכסת הפעילויות השנתית שלו: "
                                                            + cons.MaxAcitivitiesPerYear.ToString() + "\n"
                                                            + "האם עדיין לשבצו לפעילות?",
                                                            "חריגה ממספר פעילויות", MessageBoxOption.YesNo);
                    if (result.Equals(System.Windows.MessageBoxResult.No))
                    {
                        return;
                    }
                }
                
                

                /////////////////////////////////////////////////////////////////////////



                ActivityCounselor activityCounselor = ActivityCounselors.AddNew();
                activityCounselor.Counselor = ActivityCounselorCommits.SelectedItem.Counselor;
                activityCounselor.Activity = this.Activity;

            }

        }
       
        partial void RemoveCommitedCounselor_Execute()
        {
            ActivityCounselorCommits.DeleteSelected();

        }

        partial void SendEmailOfAssignDetail_Execute()
        {
            bool flag = false;
            
            for (int j = 0; j < this.ActivityCounselors.Count(); j++)
            {

                if (this.ActivityCounselors.ElementAt<ActivityCounselor>(j).Assigned)
                {
                    flag = true;                   
                }
            }

            if (flag == false)
                return;

            string comment = this.ShowInputBox("הוסף הערה למייל", "הערה");

            for (int j = 0; j < this.ActivityCounselors.Count(); j++)
            {
                
                if (this.ActivityCounselors.ElementAt<ActivityCounselor>(j).Assigned)
                {
                    string counselorEmail = this.ActivityCounselors.ElementAt<ActivityCounselor>(j).Counselor.Email;
                    if (counselorEmail != null && !counselorEmail.Equals(""))
                    {
                        this.SendMail(counselorEmail, comment);
                    }
                    else 
                    {
                        string consName = this.ActivityCounselors.ElementAt<ActivityCounselor>(j).Counselor.FullName;
                        this.ShowInputBox("המייל לא נשלח, מחסור בפרטים עבור:  " + consName, "שגיאה");
                    }
                }
            }
        }


        private void SendMail(string email,string comment)
        {

            string strMessage = "<html><body>" + "<font size=4>" +
                                "<p align=\"right\">" + ".מדריך יקר, שובצה עבורך פעילות" + "</p>" +
                                "<p align=\"right\">" + "<u>" + "<b>" + "פרטי הפעילות" + "</u>" + "</b>" + "</p>";

            if (this.Activity.Summary != null)
            {
                strMessage += "<p align=\"right\">" + "<u>" + "תאריך: " + "</u>" + this.Activity.Summary + "</p>" +
                              "<p align=\"right\">" + "<u>" + "יום: " + "</u>" + this.Activity.DayInWeek + "</p>";
            }

            if (this.Activity.ActivityType != null)
            {
                strMessage += "<p align=\"right\">" + "<u>" + "סוג פעילות: " + "</u>" + this.Activity.ActivityType.ToString() + "</p>";
            }

            if (this.Activity.FullTopicTitle != null)
            {
                strMessage+= "<p align=\"right\">" + "<u>" + "נושא: " + "</u>" + this.Activity.FullTopicTitle.ToString() + "</p>" ;
            }

            if (this.Activity.SchoolPart != null)
            {
                strMessage += "<p align=\"right\">" + "<u>" + "בית ספר: " + "</u>" + this.Activity.SchoolPart.ToString() + "</p>" +
                               "<p align=\"right\">" + "<u>" + "עיר: " + "</u>" + this.Activity.ActivityPlace.ToString() + "</p>" ;
            }

            if (this.Activity.SchoolPart.SchoolMapLink != null)
            {
                strMessage += "<p align=\"right\">" + "<u>" + ":קישור למפת מיקום בית הספר " + "</u>" + "</p>";
                strMessage += "<p align=\"right\">" + this.Activity.SchoolPart.SchoolMapLink + "</p>";
            }

            if (this.Activity.StartTime !=null  && this.Activity.EndTime != null)
            {
                strMessage+=  "<p align=\"right\">" + "<u>" + "בין השעות: " + "</u>" + this.Activity.StartTimeToShow.Substring(0,5) + " - "
                              + this.Activity.EndTimeToShow.Substring(0, 5) + "</p>";
            }


            string roundsInfo = null;
            for (int i = 0; i < this.Activity.NumberOfRounds; i++)
            {
                Round round = this.Activity.Rounds.ElementAt<Round>(i);
                if (round.StartTime == null || round.StopTime == null)
                {
                    roundsInfo = null;
                    break;
                }
                roundsInfo += "<p align=\"right\">" + "סבב מספר " + (i + 1) + " בין השעות " + round.Start_Stop + "</p>";
            }

            if (roundsInfo != null)
            {
                strMessage += "<p align=\"right\">" + "<u>" + ":סבבי פעילות" + "</u>" + roundsInfo + "</p>";
            }

            if (comment != null && !comment.Equals(""))
            {
                strMessage += "<p align=\"right\">" + "<u>" + "הערות: " + "</u>" + comment +"</p>";
            }

            strMessage += "<p align=\"right\">" + "<u>" + ":קישור לאתר השיבוץ " + "</u>" + "</p>";
            strMessage += "<p align=\"right\">" + "http://binyamin.info/binyamin/" + "</p>";


            string strSubject = "אל עמי - הודעה בדבר שיבוץ לפעילות";
            string strFrom = "yonidvirami@gmail.com";


            // Create the MailHelper class created in the Server project.
            Emails new_entry = new Emails();
            new_entry.Sender = strFrom;
            new_entry.Receiver = "EL-AMI";
            new_entry.Subject = strSubject;
            new_entry.ReceiverMail = email;
            new_entry.Message = strMessage;
            this.DataWorkspace.ApplicationData.SaveChanges();
            //this.ShowMessageBox(strMessage);

        }

     
    }
}

