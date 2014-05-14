using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Security.Server;
namespace LightSwitchApplication
{
    public partial class BinyaminReportDataService
    {
        partial void ByDistrict_PreprocessQuery(int? DistrictId, ref IQueryable<ActivityView> query)
        {
            if (DistrictId.HasValue)
            {
                query = query.Where(a => a.DistrictId == DistrictId);
            }
        }
      
        partial void ActivityViews_All_Executed(IEnumerable<ActivityView> result)
        {

            
            foreach (ActivityView view in result)
            {

                int activtyId = view.ActivityId;
                Activity g = this.DataWorkspace.ApplicationData.Activities_Single(activtyId);

                int numberOfMissing = view.TotalNumberOfCounselor - view.NumberOfAssigedCounselor;

                string counselors = view.TotalNumberOfCounselor + @"\" + view.NumberOfAssigedCounselor;
                foreach (ActivityCounselor counselorsInActivity in g.ActivityCounselors.Where(ac => ac.Assigned))
                {
                    counselors += counselorsInActivity.Counselor.FullName;

                    int counselorNumberOfRound = counselorsInActivity.NumberOfRound;
                    int totalRoundInActivity = view.TotalNumberOfRounds;

                    counselors += counselorNumberOfRound < totalRoundInActivity ? counselorNumberOfRound.ToString() : "";
                    
                    counselors += '\n';
                }
                
                view.CounselorInActivity = counselors;
            }
        }

        partial void MissingCounselors_Executed(int? DistrictId, DateTime? FromDate, DateTime? ToDate, bool? ShowOnlyMissingCounselors, IEnumerable<ActivityView> result)
        {

        }

        partial void MissingCounselors_PreprocessQuery(int? DistrictId, DateTime? FromDate, DateTime? ToDate, bool? ShowOnlyMissingCounselors, ref IQueryable<ActivityView> query)
        {

            if (ShowOnlyMissingCounselors.HasValue && ShowOnlyMissingCounselors.Value)
            {
                query = query.Where(a => a.IsMissingCounselors);
            }

        }

        partial void ActivitiesByCounselor_PreprocessQuery(int? CounselorId, ref IQueryable<ActivityView> query)
        {
            if (CounselorId.HasValue)
            {
            query = query.Where(a=>DataWorkspace.ApplicationData.Activities_Single(a.ActivityId).ActivityCounselors.Select(c=>c.Counselor.Id).ToList().Contains(CounselorId.Value));
            }
        }

        partial void SortedByDate_PreprocessQuery(ref IQueryable<ActivityView> query)
        {
            
        }

        partial void GetLectureViewByDate_PreprocessQuery(DateTime? start, DateTime? c_end, ref IQueryable<LectureView> query)
        {
          
        }

     


     
     
        

        
    }
}
