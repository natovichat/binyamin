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
    public partial class Application
    {
        partial void Application_Initialize()
        {
            this.Details.ClientTimeout = 1000 * 60 * 30;
        }

        /*

        partial void SearchActivityViews_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
           {
               result = true;
           }
           else
           {
               result = false;
           }
            // Set result to the desired field value

        }

        partial void CounselorsListDetail_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            // Set result to the desired field value

        }

        partial void PartnerTypesListDetail_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
           {
               result = true;
           }
           else
           {
               result = false;
           }
            // Set result to the desired field value
        }

        partial void MebersEnter_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiCounselor))
            {
                result = true;
            }
            else
            {
                result = false;
            }

        }

        partial void AssignCounselorTemp_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            // Set result to the desired field value

        }


        partial void AssigningProcess_CanRun(ref bool result, int activityId, int CounselorDistrictId)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            // Set result to the desired field value

        }

        partial void CreateNewOrderScreen_CanRun(ref bool result, string email)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager) || Current.User.HasPermission(Permissions.ElAmiClient))
            {
                result = true;
            }
            else
            {
                result = false;
            }

        }

        partial void EditableActivitiesGrid_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
           {
               result = true;
           }
           else
           {
               result = false;
           }
            // Set result to the desired field value

        }

        partial void EditableAudiencesGrid_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            // Set result to the desired field value
        }

        partial void SearchGetCounselorData_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            // Set result to the desired field value

        }

        partial void SearchGetfrequencyByAud_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            // Set result to the desired field value

        }

        partial void SchoolPartsListDetail_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            // Set result to the desired field value

        }

        partial void SchoolsListDetail_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            // Set result to the desired field value

        }

        partial void EditableContactsGrid_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            // Set result to the desired field value

        }

        partial void EditableCounselorsGrid_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
             {
                 result = true;
             }
             else
             {
                 result = false;
             }
            // Set result to the desired field value

        }

        partial void CreateNewOrEditActivity_CanRun(ref bool result, int? ActivityId)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            // Set result to the desired field value

        }

        partial void EditableDistrictsGrid_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
           {
               result = true;
           }
           else
           {
               result = false;
           }
            // Set result to the desired field value

        }

        partial void EditableCitiesGrid_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            // Set result to the desired field value
        }

        partial void EditableActivityTypesGrid_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
             {
                 result = true;
             }
             else
             {
                 result = false;
             }
            // Set result to the desired field value

        }

        partial void TopicCollectionsListDetail_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
              {
                  result = true;
              }
              else
              {
                  result = false;
              }
            // Set result to the desired field value
            // Set result to the desired field value

        }

        partial void EditableSchoolTypesGrid_CanRun(ref bool result)
        {
            // Set result to the desired field value
            if (Current.User.HasPermission(Permissions.ElAmiManager))
             {
                 result = true;
             }
             else
             {
                 result = false;
             }
            // Set result to the desired field value

        }

        partial void EditableRolesGrid_CanRun(ref bool result)
        {
            // Set result to the desired field value
            if (Current.User.HasPermission(Permissions.ElAmiManager))
             {
                 result = true;
             }
             else
             {
                 result = false;
             }
            // Set result to the desired field value
        }

        partial void EditableActivityStatusGrid_CanRun(ref bool result)
        {
            // Set result to the desired field value
            if (Current.User.HasPermission(Permissions.ElAmiManager))
           {
               result = true;
           }
           else
           {
               result = false;
           }
            // Set result to the desired field value

        }

        partial void EditableCRsSetGrid_CanRun(ref bool result)
        {
            // Set result to the desired field value
            if (Current.User.HasPermission(Permissions.ElAmiManager))
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            // Set result to the desired field value
        }

        partial void NewDevelopment_CanRun(ref bool result)
        {
            // Set result to the desired field value
            if (Current.User.HasPermission(Permissions.ElAmiManager))
             {
                 result = true;
             }
             else
             {
                 result = false;
             }
            // Set result to the desired field value

        }

        partial void CreateNewUserPrefs_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
             {
                 result = true;
             }
             else
             {
                 result = false;
             }
            // Set result to the desired field value

        }

        partial void ReportSchools_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            // Set result to the desired field value
            // Set result to the desired field value

        }

        partial void SearchReportLectureAllData_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
           {
               result = true;
           }
           else
           {
               result = false;
           }
            // Set result to the desired field value
            // Set result to the desired field value

        }

        partial void SearchLectureViewsByDateAll_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
           {
               result = true;
           }
           else
           {
               result = false;
           }
            // Set result to the desired field value

        }

        partial void SearchGetfrequencyViewBYDate_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            // Set result to the desired field value

        }

        partial void SearchGetSummaryYearViewBYDate_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            // Set result to the desired field value

        }

        partial void SearchGetSummaryYearViewBYYear_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            // Set result to the desired field value

        }

        partial void SearchGetSummaryYearViewBYYearAndMonth_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            // Set result to the desired field value

        }

        partial void PartnersListDetail_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            // Set result to the desired field value

        }

        partial void PartnersGroupsListDetail_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            // Set result to the desired field value

        }

        partial void PartnersReportListDetail_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            // Set result to the desired field value

        }

        partial void OrdersListDetail_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            // Set result to the desired field value

        }

        partial void OrderShowWaitingForApprovedListDetail_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            // Set result to the desired field value

        }

        partial void OrderShowApprovedListDetail_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            // Set result to the desired field value

        }

        partial void OrdersShowCanceledListDetail_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            // Set result to the desired field value

        }

        partial void OrderPropertiesWrappersListDetail_CanRun(ref bool result)
        {
            if (Current.User.HasPermission(Permissions.ElAmiManager))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            // Set result to the desired field value

        }

        partial void ActivityCounselorPropose_CanRun(ref bool result, int CounselorID, int DistrictID)
        {
            // Set result to the desired field value

        }

       */ 
    }
}
