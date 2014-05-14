using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace LightSwitchApplication
{
    public partial class Activity
    {
        partial void HebrewDate_Compute(ref string result)
        {
            DateTime date = (DateTime)this.ActivityDate;
            System.Globalization.CultureInfo ci = new  System.Globalization.CultureInfo("he-IL");
            
            ci.DateTimeFormat.Calendar = new System.Globalization.HebrewCalendar();

            result =  date.ToString("dd MMMM", ci);

        }

        partial void SchoolType_Compute(ref string result)
        {
            if (this.SchoolPart != null && this.SchoolPart.SchoolType!=null)
            {
                result = this.SchoolPart.SchoolType.Title;
            }

        }

        partial void DayInWeek_Compute(ref string result)
        {
            if (this.ActivityDate != null)
            {
                result = Convert(this.ActivityDate.DayOfWeek);
            }

        }

        public string Convert(DayOfWeek day)
        {
            string retVal = "";
          
                switch (day)
                {
                    case DayOfWeek.Friday:
                        return "שישי";

                    case DayOfWeek.Monday:
                        return "שני";

                    case DayOfWeek.Saturday:
                        return "שבת";

                    case DayOfWeek.Sunday:
                        return "ראשון";

                    case DayOfWeek.Thursday:
                        return "חמישי";

                    case DayOfWeek.Tuesday:
                        return "שלישי";

                    case DayOfWeek.Wednesday:
                        return "רביעי";

                    default:
                        break;
                }



            
            return retVal;
        }
 
        partial void ActivityPlace_Compute( ref string result)
        {
            if (this.SchoolPart != null && this.SchoolPart.City != null)
            {
                result = this.SchoolPart.City.Title;
            }

        }

        

        public void DuplicateActivity(Activity duplicateTo)
        {
            duplicateTo.ActivityDate = ActivityDate;
            duplicateTo.ActivityStatus = ActivityStatus;
            duplicateTo.ActivityType = ActivityType;
            duplicateTo.Audience = Audience;
            duplicateTo.Contact = Contact;
            duplicateTo.District = District;
            foreach (Round r in Rounds)
            {
                Round newRound = duplicateTo.Rounds.AddNew();
                newRound.NumberOfMeetingsInRound = r.NumberOfMeetingsInRound;
                newRound.StartTime = r.StartTime;
                newRound.StopTime = r.StopTime;
                newRound.NumberOfStudentInMeeting = r.NumberOfStudentInMeeting;
            }

            duplicateTo.NumberOfRounds = NumberOfRounds;
            duplicateTo.SchoolPart = SchoolPart;
            duplicateTo.Topic = Topic;
            duplicateTo.TotalNumberOfClasses = TotalNumberOfClasses;
            duplicateTo.Comments = Comments;
            duplicateTo.StartTime = StartTime;
            duplicateTo.EndTime = EndTime;
            duplicateTo.NumberOfCounselor = NumberOfCounselor;
            


            this.DataWorkspace.ApplicationData.SaveChanges();
        }

        partial void FullTopicTitle_Compute(ref string result)
        {
            if (Topic != null)
            {
                result = this.Topic.TopicCollection.Title + '\n' + this.Topic.Title;
            }
        }

        partial void SchoolPart_Changed()
        {
            if (Contact != null)
            {
                if (SchoolPart == null || this.Contact.SchoolPart.Id != this.SchoolPart.Id)
                {
                    this.Contact = null;
                }
            }
            
        }
       

        public enum ActivityStatusEnum
        {
            Create = 1,
            Assigned = 2,
            Finished = 3,
            Cancel = 4,
            Postponed = 5
        }

        partial void Activity_Created()
        {
            this.ActivityStatus = this.DataWorkspace.ApplicationData.ActivityByInternalId((int)ActivityStatusEnum.Create).Execute().ToList()[0];
            this.StartTime = this.ActivityDate;
            this.EndTime = this.ActivityDate;
            this.IsOpenForCounselorsCommit = true;
        }

        partial void Contact_Changed()
        {
            if (Contact != null)
            {
                this.SchoolPart = this.Contact.SchoolPart;
            }
        }

        partial void NumberOfRounds_Changed()
        {
            #region add or remove new Round object to activity according to the int numberOfRound
            int currentNumberOfRound = this.Rounds.Count();

            if (NumberOfRounds > currentNumberOfRound)
            {
                AddRound(NumberOfRounds - currentNumberOfRound);
            }
            else if (NumberOfRounds < currentNumberOfRound)
            {
                RemoveRound(currentNumberOfRound - NumberOfRounds);
            }
            #endregion

            CalculateNumberOfMeetingInRound();

            if (NumberOfRounds > 0)
            {
                this.Rounds.First().StartTime = this.StartTime;
                this.Rounds.Last().StopTime = this.EndTime;    
            }
            
            foreach (ActivityCounselor activityCounselor in this.ActivityCounselors)
            {
                activityCounselor.NumberOfRound = this.NumberOfRounds;
            }

        }

        private void CalculateNumberOfMeetingInRound()
        {
            if (this.NumberOfRounds > 0)
            {
                int meetingLeft = TotalNumberOfClasses;

                foreach (Round round in Rounds)
                {
                    round.NumberOfMeetingsInRound = 0;
                }

                while (meetingLeft > 0)
                {
                    foreach (Round round in Rounds)
                    {
                        if (meetingLeft > 0)
                        {
                            round.NumberOfMeetingsInRound++;
                            meetingLeft--;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
               
            }
        }

        private void AddRound(int toAdd)
        {
            for (int i = 0; i < toAdd; i++)
            {
                Round r = this.Rounds.AddNew();
                r.Activity = this;
            }
        }

        private void RemoveRound(int toRemove)
        {
            for (int i = 0; i < toRemove;i++ )
            {
                this.Rounds.Last().Delete();
            }
        }

        partial void StartTime_Changed()
        {
            if (Rounds.Count() > 0)
            {
                this.Rounds.First().StartTime = this.StartTime;
            }
        }

        partial void EndTime_Changed()
        {
            if (Rounds.Count() > 0)
            {
                this.Rounds.Last().StopTime = this.EndTime;
            }
        }

        partial void Summary_Compute(ref string result)
        {
            result = this.HebrewDate + " " + this.ActivityDate.ToShortDateString();

        }

        partial void TotalNumberOfClasses_Changed()
        {
            CalculateNumberOfMeetingInRound();
        }

        partial void StartTimeToShow_Compute(ref string result)
        {
            result = this.StartTime.TimeOfDay.ToString().Substring(0, 5);
        }

        partial void EndTimeToShow_Compute(ref string result)
        {
            result = this.EndTime.TimeOfDay.ToString().Substring(0, 5);
        }


        partial void NumberOfAssignedConselours_Compute(ref int result)
        {
            result = 0;
            for (int j = 0; j < this.ActivityCounselors.Count(); j++)
            {
                if (this.ActivityCounselors.ElementAt<ActivityCounselor>(j).Assigned)
                {
                    result++;
                }
            }
        }
       

        
    }
}
