using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

/// <summary>
/// Encapsulates a Task which is sent to the rich client application
/// </summary>
[DataContract]
public struct Task
{
    /// <summary>
    /// Name of the Task
    /// </summary>    
    [DataMember]
    public string TaskName;

    /// <summary>
    /// Task Description
    /// </summary>
    [DataMember]
    public string Description;

    /// <summary>
    /// Task Repository
    /// </summary>
    [DataMember]
    public string Repository;

    /// <summary>
    /// Targeted towards this Group or User
    /// </summary>
    [DataMember]
    public string TargetGroupOrUser;

    /// <summary>
    /// The user that created this task
    /// </summary>
    [DataMember]
    public string CreatedUser;

    /// <summary>
    /// Details associated with this task : Task Starting time
    /// </summary>
    [DataMember]
    public DateTime StartTime;

    /// <summary>
    /// Details associated with this task : Task Due Date
    /// </summary>
    [DataMember]
    public DateTime DueDate;

    /// <summary>
    /// Details associated with this task : Task Priority
    /// </summary>
    [DataMember]
    public short Priority;

    /// <summary>
    /// Details associated with this task : Task Completion Percentage
    /// </summary>
    [DataMember]
    public string PercentageComplete;
}