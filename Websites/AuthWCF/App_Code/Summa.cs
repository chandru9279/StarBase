using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summa oru class :-)
/// </summary>
public static class Summa
{
    public static void SContact(out Contact c)
    {
        c.ContactEmail = "zasz@aveoinfotech.com";
        c.ContactMobile = "+919486149264";
        c.ContactName = "T Chandirasekar";
        c.ContactPhoneNumber = "0011-33334444";
        c.Tags = "SomeTag";          
    }
    public static void SScheduledEvent(out ScheduledEvent s)
    {
        s.CreatedUser = "T Chandirasekar";
        s.Repository = "Reapository A";
        s.ScheduleDescription = "Sample Schedule";
        s.ScheduleEndTime = DateTime.Now.AddDays(1);
        s.ScheduleName = "Sample schedule one";
        s.ScheduleReminderTime = DateTime.Now;
        s.ScheduleStartTime = DateTime.Now.AddDays(-1);
        s.TargetGroupOrUser = "Arasu Shankar";
    }
    public static void STask(out Task t)
    {
        t.CreatedUser = "T Chandirasekar";
        t.Description = "Sample Task";
        t.DueDate = DateTime.Now.AddDays(1);
        t.PercentageComplete = "68%";
        t.Priority = 5;
        t.Repository = "Reapository A";
        t.StartTime = DateTime.Now.AddDays(-1);
        t.TargetGroupOrUser = "Arasu Shankar";
        t.TaskName = "Sample task one"; 
    }
}
