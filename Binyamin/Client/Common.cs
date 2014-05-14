using System;
using System.Net;

using System.Runtime.Serialization;
using System.Collections.Generic;
using System.ComponentModel;
using Common;

namespace Common
{
    #region Enums

    [DataContract]
    public enum ActivityType
    {
        [EnumMember]
        Unknown = 0,
        [EnumMember]
        Lecture = 1,
        [EnumMember]
        WorkshopRegular = 2,
        [EnumMember]
        WorkshopHoleGrade = 3,
        [EnumMember]
        WorkshopEvening = 5,
        [EnumMember]
        ParentsAndChildren = 4,
        [EnumMember]
        Bitmidrash = 6,
        [EnumMember]
        SoundAndLightShow = 7,
        [EnumMember]
        Trip = 8,
        [EnumMember]
        Show = 9 //מופע

    }

    [DataContract]
    public enum ActivityStatus
    {
        [EnumMember]
        Create,
        [EnumMember]
        Assigned,
        [EnumMember]
        Finished,
        [EnumMember]
        Cancel,
        [EnumMember]
        Postponed
    }

    [DataContract]
    public enum Grade
    {
        [EnumMember]
        ז =1 ,
        [EnumMember]
        ח=2,
        [EnumMember]
        ט=3 ,
        [EnumMember]
        י=4 ,
        [EnumMember]
        יא=5,
        [EnumMember]
        יב=6,
        [EnumMember]
        הורים=7,
        [EnumMember]
        מורים=8,
        [EnumMember]
        אחר=9


    }

    [DataContract]
    public enum CounselorAnswer
    {
        [EnumMember]
        UnKnown,
        [EnumMember]
        Approve,
        [EnumMember]
        Reject,
        [EnumMember]
        Tentetiv

    }

    #endregion Enums

    #region Types

    #region Activity and Orders

    [DataContract]
    public class Order
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public bool IsSchoolApprove { get; set; }

        [DataMember]
        public List<Activity> Activities { get; set; }

    }

    [DataContract]
    public class Activity
    {
        public Activity(int id)
        {

            Date = DateTime.Now;
            Comments = "";
            Id = id;
            NumberOfRequireCounselors = 0;
            Rounds = new List<Round>();
            ProposalsToCounselors = new List<Proposal>();
            CounselorsAssigned = new List<Counselor>();
            CounselorsApproved = new List<Counselor>();
            CounselorsApprovedIds = new List<int>();
            CounselorsAssignedIds = new List<int>();
            ActivityStatus = ActivityStatus.Create;
            Topic = Topic;
            OrderId = -1;

            List<Round> rounds = new List<Round> { new Round(), new Round(), new Round() };

            Rounds = rounds;

            EnterTime = DateTime.Now;

        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public List<Round> Rounds { get; set; }
        [DataMember]
        public int NumberOfRequireCounselors { get; set; }
        [DataMember]
        public string Comments { get; set; }
        [DataMember]
        public List<Proposal> ProposalsToCounselors { get; set; }
        private ActivityStatus activityStatus;
        [DataMember]
        public ActivityStatus ActivityStatus
        {
            get
            {
                //switch (activityStatus)
                //{
                //    case ActivityStatus.Create:
                //        if (NumberOfMissingCounselors == 0)
                //        {
                //            activityStatus =  ActivityStatus.Assigned;
                //        }
                //        break;
                //    case ActivityStatus.Assigned:
                //        if (DateTime.Now > Date)
                //        {
                //            activityStatus =  ActivityStatus.Finished;
                //        }
                //        else if (NumberOfMissingCounselors > 0)
                //        {
                //            activityStatus = ActivityStatus.Create;
                //        }
                //        break;
                //    case ActivityStatus.Finished:
                //        if (Date > DateTime.Now)
                //        {
                //            activityStatus = ActivityStatus.Create;
                //            activityStatus = ActivityStatus;
                //        }
                //        break;
                //    case ActivityStatus.Cancel:
                //    case ActivityStatus.Deleted:
                //    case ActivityStatus.Postponed:
                //        break;

                //    default:
                //        throw new NotSupportedException();
                //        break;
                //}


                return activityStatus;
            }
            set
            {
                activityStatus = value;
            }
        }

        [DataMember]
        public List<Counselor> CounselorsAssigned { get; set; }
        [DataMember]
        public List<Counselor> CounselorsApproved { get; set; }

        [DataMember]
        public List<int> CounselorsAssignedIds { get; set; }
        [DataMember]
        public List<int> CounselorsApprovedIds { get; set; }

        [DataMember]
        public School School { get; set; }
        [DataMember]
        public int SchoolId { get; set; }
        [DataMember]
        public int OrderId { get; set; }

        public int NumberOfMissingCounselors
        {
            get
            {
                return NumberOfRequireCounselors - CounselorsAssigned.Count;
            }
            private set
            {

            }
        }
        
        private Topic topic;
        [DataMember]
        public Topic Topic
        {
            set
            {
                //set the value only if the topic changed or the topic is null
                if (value == null || topic == null || value.Id != topic.Id)
                {
                    foreach (Round r in Rounds)
                    {
                        foreach (Meeting m in r.Meetings)
                        {
                            m.MeetingSubject = value;
                        }
                    }
                    topic = value;
                }
            }
            get
            {
                return topic;
            }

        }

        [DataMember]
        public int TopicId { get; set; }
        [DataMember]
        public Contacte Contacte { get; set; }
        [DataMember]
        public int ContacteId { get; set; }

        private Grade grade;
        [DataMember]
        public Grade Grade
        {
            set
            {
                grade = value;
            }
            get
            {
                return grade;

            }

        }
        [DataMember]
        public ActivityType ActivityType { get; set; }
        private int totalNumberOfClass;
        [DataMember]
        public int TotalNumberOfClass
        {
            set
            {
                totalNumberOfClass = value;
            }
            get
            {
                return totalNumberOfClass;

            }

        }
        private PaymentInfo paymentInfo = new PaymentInfo();
        [DataMember]
        public PaymentInfo PaymentInfo
        {
            get
            {
                return paymentInfo;
            }
            set
            {
                paymentInfo = value;
            }
        }
        private string orderCommnet;
        [DataMember]
        public string OrderCommnet
        {
            get
            {
                return orderCommnet;
            }
            set
            {
                orderCommnet = value;
            }

        }
        [DataMember]
        public DateTime EnterTime { get; set; }

        [DataMember]
        public DateTime ModifyTime { get; set; }

        #region Calculate Property

        public string ActivityTime
        {
            get
            {
                return "";
                //TimeSpan time = Rounds[0].StratTime;
                //string startTime = string.Format("{0:D2}:{1:D2}", time.Hours, time.Minutes);
                //time = Rounds.Last().StopTime;
                //string endTime = string.Format("{0:D2}:{1:D2}", time.Hours, time.Minutes);

                //return startTime + "-" + endTime;
            }
        }

        public DayOfWeek DayInWeek
        {
            get
            {
                return Date.DayOfWeek;
            }
        }

        public string Grades
        {
            get
            {
                List<Grade> allGrades = new List<Grade>();
                foreach (Round round in Rounds)
                {
                    foreach (Meeting meeting in round.Meetings)
                    {
                        if (meeting.Grade.HasValue && !allGrades.Contains(meeting.Grade.Value))
                        {
                            allGrades.Add(meeting.Grade.Value);
                        }
                    }
                }
                if (allGrades.Count == 0) allGrades.Add(grade);


                string gradeInString = "";
                foreach (Grade g in allGrades)
                {
                    gradeInString += g.ToString() + ",";
                }
                gradeInString = gradeInString.TrimEnd(new char[] { ',' });
                return gradeInString;
            }

        }

        #endregion

        #region Methods

        public void ReCalculatePayment()
        {
            //if (this.ActivityStatus == ActivityStatus.Create || this.ActivityStatus == ActivityStatus.Assigned)
            //{
            //    List<Proposal> approvedProposal = (from Proposal p in this.ProposalsToCounselors
            //                                       where p.IsApprovedByManager
            //                                       select p).ToList();
            //    ReCalculatePayment(approvedProposal, this.Rounds.ToList(), this.ActivityType);
            //}
        }

        private void ReCalculatePayment(List<Proposal> approvedProposal, List<Round> rounds, ActivityType activityType)
        {
            Dictionary<int, int> numOfRoundsByCounselorId = CreateNumberOfRoundsPerCounselor(rounds);

            paymentInfo.CounselorPaymentInfos = CalculateCounselorsPayment(approvedProposal, numOfRoundsByCounselorId, ActivityType);

            List<int> numberOfClassPerRound = new List<int>();

            foreach (Round r in Rounds)
            {
                numberOfClassPerRound.Add(r.NumberOfMeeting);
            }
            //paymentInfo.SchoolPayment.Cost = Calculator.CalculateCost(Calculator.CalculateAssignment(numberOfClassPerRound));

        }

        private List<CounselorPaymentInfo> CalculateCounselorsPayment(List<Proposal> approvedProposal, Dictionary<int, int> NumOfRoundsByCounselorId, ActivityType activityType)
        {
            List<CounselorPaymentInfo> counselorsPayment = new List<CounselorPaymentInfo>();

            foreach (Proposal proposal in approvedProposal)
            {
                if (NumOfRoundsByCounselorId.ContainsKey(proposal.Counselor.Id))
                {
                    CounselorPaymentInfo info = CalculateCounselorPayment(proposal.Counselor, NumOfRoundsByCounselorId[proposal.Counselor.Id], activityType);
                    info.PartOfAgreement = proposal.IsAssingedAsPermenet;
                    counselorsPayment.Add(info);
                }
            }

            return counselorsPayment;
        }

        private CounselorPaymentInfo CalculateCounselorPayment(Counselor counselor, int numberOfRounds, ActivityType activityType)
        {
            CounselorPaymentInfo paymentInfo = new CounselorPaymentInfo();

            paymentInfo.NumberOfRounds = numberOfRounds;
            paymentInfo.Counselor = counselor;
            //  paymentInfo.PayForActivity = Calculator.CalculateCounselorCost(counselor.PermanentInfo.IsPermanent, activityType, numberOfRounds);

            return paymentInfo;

        }

        public static Dictionary<int, int> CreateNumberOfRoundsPerCounselor(List<Round> rounds)
        {
            Dictionary<int, int> NumOfRoundsByCounselorId = new Dictionary<int, int>();


            foreach (Round r in rounds)
            {
                List<int> counselorInRound = new List<int>();
                foreach (Meeting m in r.Meetings)
                {
                    if (m.Counselor != null && !counselorInRound.Contains(m.Counselor.Id)) //for each round we count the counselor only once.
                    {
                        if (!NumOfRoundsByCounselorId.ContainsKey(m.Counselor.Id))
                        {
                            NumOfRoundsByCounselorId.Add(m.Counselor.Id, 0);
                        }
                        NumOfRoundsByCounselorId[m.Counselor.Id]++;
                        counselorInRound.Add(m.Counselor.Id);
                    }
                }
            }
            return NumOfRoundsByCounselorId;
        }

        public List<Counselor> ProposedCounselors
        {
            get
            {
                List<Counselor> proposedCounselors = new List<Counselor>();

                foreach (Proposal p in ProposalsToCounselors)
                {
                    proposedCounselors.Add(p.Counselor);
                }
                return proposedCounselors;

            }
        }

        public List<Round> GetRoundByCounselor(Counselor counselor)
        {
            List<Round> rounds = new List<Round>();

            foreach (Round round in Rounds)
            {
                if (round.Contain(counselor))
                {
                    rounds.Add(round);
                }

            }


            return rounds;
        }

        #endregion


    }

    [DataContract]
    public class Round
    {
        public Round()
        {
            Meetings = new List<Meeting>();
            NumberOfStudentInMeeting = 35;
            StratTime = new TimeSpan();
            StopTime = new TimeSpan();
        }
        [DataMember]
        public TimeSpan StratTime { get; set; }
        [DataMember]
        public TimeSpan StopTime { get; set; }
        private int numberOfMeeting;
        [DataMember]
        public int NumberOfMeeting
        {
            get
            {
                return numberOfMeeting;
            }
            set
            {
                numberOfMeeting = value;
                int diff = numberOfMeeting - meetings.Count;

                if (diff > 0) //need to add
                {
                    for (int i = 0; i < diff; i++)
                    {
                        Meeting newMetting = new Meeting();
                        meetings.Add(newMetting);
                    }
                }
            }


        }
        [DataMember]
        public int NumberOfStudentInMeeting { get; set; }


        public List<Meeting> meetings;
        [DataMember]
        public List<Meeting> Meetings
        {
            get
            {
                List<Meeting> currentMetting = new List<Meeting>();
                for (int i = 0; i < numberOfMeeting; i++)
                {
                    currentMetting.Add(meetings[i]);
                }
                return currentMetting;
            }
            set
            {
                meetings = value;
            }
        }


        internal bool Contain(Counselor counselor)
        {
            return true;
            //return (from Meeting m in meetings
            //        where m.Counselor != null && m.Counselor.Id == counselor.Id
            //        select m).Count() > 0;
        }
    }

    [DataContract]
    public class Meeting
    {
        [DataMember]
        public Counselor Counselor { get; set; }
        [DataMember]
        public Grade? Grade { get; set; }
        [DataMember]
        public int NumOfClass { get; set; }
        [DataMember]
        public Topic MeetingSubject { get; set; }
    }

    #endregion

    #region Topics
    [DataContract]
    public class Topic
    {
        public override bool Equals(object obj)
        {
            if (obj is Topic)
            {
                Topic topic = (Topic)obj;
                return this.Id == topic.Id;
            }
            return false;
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public int TopicsCollectionId { get; set; }

        [DataMember]
        public ActivityType TopicType { get; set; }


    }

    [DataContract]
    public class TopicsCollection
    {
        public TopicsCollection()
        {
            Topics = new List<Topic>();
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public List<Topic> Topics { get; set; }
        [DataMember]
        public ActivityType CollectionType { get; set; }

    }

    #endregion

    #region Assigning Process

    [DataContract]
    public class Proposal
    {
        public Proposal(Counselor c)
        {
            Counselor = c;
            CounselorId = c.Id;
        }
        public Counselor Counselor { get; set; }
        [DataMember]
        public int CounselorId { get; set; }
        [DataMember]
        public int NumOfRoundPropose { get; set; }
        [DataMember]
        public CounselorAnswer CounselorAnswer { get; set; }
        [DataMember]
        public bool IsApprovedByManager { get; set; }
        [DataMember]
        public int NumOfRoundApproved { get; set; }
        [DataMember]
        public bool IsAssingedAsPermenet { get; set; }
        public override bool Equals(object obj)
        {
            Proposal p = (Proposal)obj;

            return p.Counselor.Id == this.Counselor.Id;
        }

    }

    #endregion

    #region Counselors

    [DataContract]
    public class User
    {
        public User()
        {
            City = new City();
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string PhoneNumber1 { get; set; }
        [DataMember]
        public string PhoneNumber2 { get; set; }
        [DataMember]
        public string Mail { get; set; }
        [DataMember]
        public string Adress { get; set; }

        public City City { get; set; }
        [DataMember]
        public int CityId { get; set; }
        [DataMember]
        public District District { get; set; }

    }

    [DataContract]
    public class Counselor : User
    {
        public Counselor()
        {
            PermanentInfo = new PermanentInfo();
            Id = -1;
        }
        [DataMember]
        public bool HasCar { get; set; }
        [DataMember]
        public DayOfWeek[] FreeDays { get; set; }
        [DataMember]
        public DayOfWeek[] FlexDays { get; set; }
        [DataMember]
        public PermanentInfo PermanentInfo { get; set; }
        [DataMember]
        public string FullName
        {
            set
            {

            }
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }

        public override bool Equals(object obj)
        {
            if (obj is Counselor)
            {
                return Id == ((Counselor)obj).Id;
            }
            else
            {
                return false;
            }
        }
    }

    public class PermanentInfo
    {
        public bool IsPermanent { get; set; }
        public DayOfWeek PermanentDay { get; set; }
    }

    #endregion

    #region Schools

    [DataContract]
    public class SchoolCollection
    {
        public SchoolCollection()
        {
            Schools = new List<School>();
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public List<School> Schools { get; set; }


    }

    [DataContract]
    public class School
    {
        public School()
        {
            Conactees = new List<Contacte>();
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string SchoolCollectionName { get; set; }
        public string Title
        {
            get
            {
                if (SchoolCollectionName != "")
                {
                    return SchoolCollectionName + " " + SchoolName;
                }
                else
                {
                    return SchoolName;
                }
            }
        }

        [DataMember]
        public City City { get; set; }
        [DataMember]
        public int CityId { get; set; }
        [DataMember]
        public District District { get; set; }
        [DataMember]
        public string Adress { get; set; }
        [DataMember]
        public string PhoneNumber1 { get; set; }
        [DataMember]
        public string PhoneNumber2 { get; set; }
        [DataMember]
        public string FaxNumber { get; set; }
        [DataMember]
        public SchoolType SchoolType { get; set; }
        [DataMember]
        public string Comments { get; set; }
        [DataMember]
        public List<Contacte> Conactees { get; set; }

        //must
        [DataMember]
        public int SchoolCollectionId { get; set; }
        [DataMember]
        public string SchoolName { get; set; }





    }

    /// <summary>
    /// Contactee Role
    /// </summary>
    [DataContract]
    public class Role
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Title { get; set; }
    }

    [DataContract]
    public class Contacte
    {
        public override bool Equals(object obj)
        {
            if (obj is Contacte)
            {
                Contacte topic = (Contacte)obj;
                return this.Id == topic.Id;
            }
            return false;
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string PhoneNumber1 { get; set; }
        [DataMember]
        public string PhoneNumber2 { get; set; }
        [DataMember]
        public Role Role { get; set; }
        [DataMember]
        public bool IsCenteral { get; set; }
        [DataMember]
        public string Mail { get; set; }
    }

    [DataContract]
    public class SchoolType
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Title { get; set; }
    }

    #endregion

    #region General

    [DataContract]
    public class District
    {
        public override bool Equals(object obj)
        {
            if (obj is District)
            {
                return ((District)obj).Id == this.Id;
            }
            return false;
        }
        [DataMember]
        public int Id { set; get; }
        [DataMember]
        public string Title { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
    [DataContract]
    public class City : IComparable
    {
        public City()
        {
        }
        public City(int id, string title)
        {
            Id = id;
            Title = title;
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public District DefaultDistrict { get; set; }

        public override string ToString()
        {
            return Title;
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            return this.Title.CompareTo(((City)obj).Title);
        }

        #endregion
    }

    #endregion

    #region Payment

    public class LecturerPaymentInfo
    {
        //public Lecturer Lecturer { get; set; }
        public int PayForLecture { get; set; }
    }

    public class CounselorPaymentInfo
    {
        public Counselor Counselor { get; set; }
        public int PayForActivity { get; set; }
        public int PayForRide { get; set; }
        public bool PartOfAgreement { get; set; }
        public int NumberOfRounds { get; set; }
        public int ModifyPayment { get; set; }
        public string ModifyPaymentCause { get; set; }
    }

    public class SchoolPayment
    {
        public int Cost { get; set; } //עלות מלאה
        public int Subsidization { get; set; } //סיבסוד
    }

    public class PaymentInfo
    {
        public PaymentInfo()
        {
            CounselorPaymentInfos = new List<CounselorPaymentInfo>();
            SchoolPayment = new SchoolPayment();
        }
        public List<CounselorPaymentInfo> CounselorPaymentInfos { get; set; }
        public SchoolPayment SchoolPayment { get; set; }
    }

    #endregion

    #region OrderOfEquiment

    public class OrderOfEquiment
    {
        public DateTime Date { get; set; }
        public School School { get; set; }
        public Topic Topic { get; set; }
        public int Count { get; set; }
        public int NumberOfClasses { get; set; }

    }

    #endregion



    #endregion


}
public class Utils
{
    public static bool IsInRange(DateTime date, DateTime from, DateTime to)
    {
        TimeSpan span = new TimeSpan(1, 0, 0, 0);

        DateTime cellDate = CellDate(date, span);
        DateTime cellfrom = CellDate(from, span);
        DateTime cellTo = CellDate(to, span);

        bool res = cellDate >= cellfrom && cellDate <= cellTo;

        return res;


    }

    private static DateTime CellDate(DateTime date, TimeSpan span)
    {
        long ticks = date.Ticks / span.Ticks;
        DateTime cellDate = new DateTime(ticks * span.Ticks);
        return cellDate;
    }

}

public class OpenToAssign : INotifyPropertyChanged
{
    public OpenToAssign(Counselor counselor, bool isOpen)
    {
        Counselor = counselor;
        IsOpen = isOpen;
    }
    public Counselor counselor;
    public Counselor Counselor
    {
        get
        {
            return counselor;
        }
        set
        {
            counselor = value;
            RaisePropertyChanged("Counselor");
        }
    }

    private void RaisePropertyChanged(string p)
    {

        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(p));
        }
    }
    private bool isOpen;

    public bool IsOpen
    {
        get
        {
            return isOpen;
        }
        set
        {
            isOpen = value;
            RaisePropertyChanged("IsOpen");

        }
    }

    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion
}


public class CounselorFilter : INotifyPropertyChanged
{
    public event EventHandler FilterChanged;

    public CounselorFilter()
    {

    }
    public bool ToFilter
    {
        get
        {
            return permanent || freeDay || flexDay;
        }
    }
    private bool permanent;
    private bool freeDay;
    private bool flexDay;


    public bool Permanent
    {

        get
        {
            return permanent;
        }
        set
        {
            permanent = value;
            FireFilterChanged();
        }
    }
    public bool FreeDay
    {

        get
        {
            return freeDay;
        }
        set
        {
            freeDay = value;
            FireFilterChanged();
        }
    }
    public bool FlexDay
    {

        get
        {
            return flexDay;
        }
        set
        {
            flexDay = value;
            FireFilterChanged();
        }
    }


    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;
    private void RaisePropertyChanged(string p)
    {

        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(p));
        }
    }
    #endregion

    void FireFilterChanged()
    {
        if (FilterChanged != null)
        {
            FilterChanged(this, null);
        }
    }
}