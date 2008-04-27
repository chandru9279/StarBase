using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

/// <summary>
/// Summary description for ScheduledEvents
/// </summary>


[DataContract]
public struct ScheduledEvent
{
    [DataMember]
    public string ScheduleName;
    [DataMember]
    public string ScheduleDescription;
    [DataMember]
    public string Repository;
    [DataMember]
    public string TargetGroupOrUser;
    [DataMember]
    public string CreatedUser;
    [DataMember]
    public DateTime ScheduleStartTime;
    [DataMember]
    public DateTime ScheduleEndTime;
    [DataMember]
    public DateTime ScheduleReminderTime;
}