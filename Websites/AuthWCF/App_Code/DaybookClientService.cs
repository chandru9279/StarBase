using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Security.Authentication;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Security;

/// <summary>
/// Implementation of the service contract IDaybookClientService
/// <remarks>If you change the class name "DaybookClientService" here, you must also update the reference to "DaybookClientService" in Web.config.</remarks>
/// </summary> 
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
public class DaybookClientService : IDaybookClientService
{
    /// <summary>
    /// Constructor that performs the essential Thread hookup for declarative PrincipalPermission to work.
    /// <remarks>Called automatically by ASP Runtime when a WCF Request comes in</remarks>
    /// </summary>
    public DaybookClientService()
    {   
        Thread.CurrentPrincipal = HttpContext.Current.User;
    }

    /// <summary>
    /// Ping service to check if DaybookServices are active 
    /// <returns>A value indicating if its OK to call the other services</returns>
    /// </summary>
    [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
    public bool PingService()
	{
        return true;
	}

    /// <summary>
    /// Gets a list of contacts for the current principal
    /// </summary>
    [PrincipalPermission(SecurityAction.Demand , Authenticated=true)]
    public List<Contact> GetContacts()
    {
        List<Contact> contacts = new List<Contact>();
        for (int i = 0; i < 5; i++)
        {
            Contact c = new Contact();
            Summa.SContact(out c);
            contacts.Add(c);            
        }
        return contacts;
    }

    /// <summary>
    /// Gets a list of scheduled events for the current principal
    /// </summary>
    [PrincipalPermission(SecurityAction.Demand , Authenticated=true)]
    public List<ScheduledEvent> GetScheduledEvents()
    {
        List<ScheduledEvent> events = new List<ScheduledEvent>();
        for (int i = 0; i < 5; i++)
        {
            ScheduledEvent s = new ScheduledEvent();
            Summa.SScheduledEvent(out s);
            events.Add(s);
        }
        return events;
    }

    /// <summary>
    /// Gets a list of tasks for the current principal
    /// </summary>
    [PrincipalPermission(SecurityAction.Demand , Authenticated=true)]
    public List<Task> GetTasks()
    {
        List<Task> tasks = new List<Task>();
        for (int i = 0; i < 5; i++)
        {
            Task t = new Task();
            Summa.STask(out t);
            tasks.Add(t);
        }
        return tasks;
    }

}
