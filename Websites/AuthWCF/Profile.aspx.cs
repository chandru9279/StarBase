using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class ProfileInformation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            Label1.Text = HttpContext.Current.User.Identity.Name;
            Label2.Text = Profile.Calendar.View.ToString();
            Label3.Text = Profile.Calendar.CustomViewDays.ToString();
            Label4.Text = Profile.Calendar.TimeFormat.ToString();
            Label5.Text = Profile.Calendar.ShowNonBusinessHours.ToString();
            
            Label6.Text = "Count : " + Profile.Calendar.ExcludeRepositories.Count.ToString() + Environment.NewLine;
            for (int i = 0; i < Profile.Calendar.ExcludeRepositories.Count-1; i++)
                Label6.Text += Environment.NewLine + Profile.Calendar.ExcludeRepositories[i].ToString();

            Label7.Text = Profile.Calendar.ExcludeMyDaybook.ToString();




            Label10.Text = "Count : " + Profile.Tasks.ExcludeRepositories.Count.ToString() + Environment.NewLine;
            for (int i = 0; i < Profile.Tasks.ExcludeRepositories.Count - 1; i++)
                Label10.Text += Environment.NewLine + Profile.Tasks.ExcludeRepositories[i].ToString();
            Label8.Text = Profile.Tasks.HideCompletedTasks.ToString();
            Label9.Text = Profile.Tasks.HideTasksNotDueRecently.ToString();
            Label11.Text = Profile.Calendar.ExcludeMyDaybook.ToString();
        }
        else
        {
            Label1.Text = "User is not Authenticated";
            Label1.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            Label1.Text = HttpContext.Current.User.Identity.Name;
            Label2.Text = Profile.Calendar.View.ToString();
            Label3.Text = Profile.Calendar.CustomViewDays.ToString();
            Label4.Text = Profile.Calendar.TimeFormat.ToString();
            Label5.Text = Profile.Calendar.ShowNonBusinessHours.ToString();

            Label6.Text = "Count : " + Profile.Calendar.ExcludeRepositories.Count.ToString() + Environment.NewLine;
            for (int i = 0; i < Profile.Calendar.ExcludeRepositories.Count; i++)
                Label6.Text += Environment.NewLine + Profile.Calendar.ExcludeRepositories[i].ToString();

            Label7.Text = Profile.Calendar.ExcludeMyDaybook.ToString();




            Label10.Text = "Count : " + Profile.Tasks.ExcludeRepositories.Count.ToString() + Environment.NewLine;
            for (int i = 0; i < Profile.Tasks.ExcludeRepositories.Count; i++)
                Label10.Text += Environment.NewLine + Profile.Tasks.ExcludeRepositories[i].ToString();
            Label8.Text = Profile.Tasks.HideCompletedTasks.ToString();
            Label9.Text = Profile.Tasks.HideTasksNotDueRecently.ToString();
            Label11.Text = Profile.Calendar.ExcludeMyDaybook.ToString();
            
        }
        else
        {
            Label1.Text = "User is not Authenticated";
            Label1.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {   
            Profile.Calendar.View = Aveo.Daybook.Utils.ProfileCalendarView.Week;
            Profile.Calendar.CustomViewDays = 3;
            Profile.Calendar.TimeFormat = Aveo.Daybook.Utils.ProfileCalendarTimeFormat.TwentyFourHour;
            Profile.Calendar.ShowNonBusinessHours = true;
            Profile.Calendar.ExcludeRepositories = new ArrayList();
            Profile.Calendar.ExcludeRepositories.Add("Repository A");
            Profile.Calendar.ExcludeRepositories.Add("Repository B");
            Profile.Calendar.ExcludeRepositories.Add("Repository C");
            Profile.Calendar.ExcludeMyDaybook = true;


            Profile.Tasks.ExcludeRepositories = new ArrayList();
            Profile.Tasks.ExcludeRepositories.Add("Repository A");
            Profile.Tasks.ExcludeRepositories.Add("Repository B");
            Profile.Tasks.ExcludeRepositories.Add("Repository C");
            Profile.Tasks.HideCompletedTasks = true;
            Profile.Tasks.HideTasksNotDueRecently = true;
        }
    }
}