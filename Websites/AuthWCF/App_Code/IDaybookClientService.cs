using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

/// <summary>
/// Service contract for the DaybookClientService class. 
/// <remarks>If you change the class name "IDaybookClientService" here, you must also update the reference to "IDaybookClientService" in Web.config.</remarks>
/// </summary>
[ServiceContract, XmlSerializerFormat]
public interface IDaybookClientService
{
    /// <summary>
    /// Ping service to check if DaybookServices are active. 
    /// <returns>A value indicating if its OK to call the other services</returns>
    /// </summary>
	[OperationContract]
	bool PingService();

    /// <summary>
    /// Gets a list of contacts for the current principal
    /// </summary>
    [OperationContract]    
    List<Contact> GetContacts();

    /// <summary>
    /// Gets a list of scheduled events for the current principal
    /// </summary>
    [OperationContract]
    List<ScheduledEvent> GetScheduledEvents();

    /// <summary>
    /// Gets a list of tasks for the current principal
    /// </summary>
    [OperationContract]
    List<Task> GetTasks();
}