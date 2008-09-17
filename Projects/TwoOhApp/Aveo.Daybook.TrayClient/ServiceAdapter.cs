using System;
using System.Collections.Generic;
using System.Text;

namespace Aveo.Daybook.TrayClient
{
    public class ServiceAdapter
    {
        #region Private Members

        private bool IsLoggedIn = false;
        private System.Net.CookieContainer cookiecontainer;
        private string Username, Password;        
        private AuthenticationService authServ;
        private ProfileService proServ;
        private DaybookClientService dbcServ;
        private bool ContactsCompleted = false, ScheduledEventsCompleted = false, TasksCompleted = false;        
        private Guid ContactsGuid = new Guid("{38BD1F1E-1F3C-4a68-8CF5-447243D7BFD4}");
        private Guid TasksGuid = new Guid("{BEE1506C-276E-4b41-9E17-8F56C0AA8166}");
        private Guid EventsGuid = new Guid("{62B09F1E-8CE4-4013-867C-25B42CEA4C8D}");        
        private Guid ProfileGuid = new Guid("{FDCD4598-E986-4984-8FF0-6923645BF325}");

        #endregion

        #region Public Members

        public static ServiceAdapter Instance;
        public static List<Contact> contacts;
        public static List<ScheduledEvent> events;
        public static List<Task> tasks;
        public static ArrayOfKeyValueOfstringanyTypeKeyValueOfstringanyType[] profiledata;
        public static ProfilePropertyMetadata[] profileMD;
        public delegate void GotAllDataHandler(bool success);
        public delegate void LoginCompletedHandler(bool success);
        public delegate void GotProfileHandler(bool success); 
        public static event GotAllDataHandler GotAllData;
        public static event LoginCompletedHandler LoginCompleted;
        public static event GotProfileHandler GotProfile;
        /// <summary>
        /// Readonly
        /// Gets a value indicating if the user is logged in.
        /// </summary>        
        public bool IsAuthenticated
        {
            get { return IsLoggedIn; }
        }
        /// <summary>
        /// Readonly
        /// Returns the entered username
        /// </summary>
        public string username
        {
            get { return Username; }
        }

        #endregion

        public ServiceAdapter()
        {
            authServ = new AuthenticationService();
            proServ = new ProfileService();
            dbcServ = new DaybookClientService();
            authServ.CookieContainer = new System.Net.CookieContainer();
            Instance = this;            
        }

        #region Logging In
        
        /// <summary>
        /// Will try to login to the web service asynchronously
        /// Results are known only when authServ_LoginCompleted is completed
        /// </summary>
        /// <param name="username">Username entered by the user from UI</param>
        /// <param name="password">Password entered by the user from UI</param>
        public void TryLogin(string username,string password)
        {
            try
            {
                Username = username;
                Password = password;
                authServ.LoginAsync(Username, Password, "", false, false);
                authServ.LoginCompleted += new LoginCompletedEventHandler(authServ_LoginCompleted);                
            }
            catch (Exception e)
            {
                Console.WriteLine("TryLogin : " + e.ToString());                
            }
        }

        /// <summary>
        /// Results of the TryLogin method. Called by proxy class when attempt was completed
        /// <seealso cref="TryLogin"/>
        /// </summary>
        /// <param name="sender">Proxy class reference</param>
        /// <param name="e">LoginCompletedEventArgs sent from the proxy object</param>
        private void authServ_LoginCompleted(object sender, LoginCompletedEventArgs e)
        {
            try
            {
                if (e.LoginResult == true)
                {
                    cookiecontainer = authServ.CookieContainer;                    
                    IsLoggedIn = true;
                }
                LoginCompleted(e.LoginResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine("authServ_LoginCompleted : " + ex.ToString());
                LoginCompleted(false);
            }
        }

        #endregion

        #region Getting business Information

        /// <summary>
        /// Will try to Get Information from the web service asynchronously
        /// Results are known only when dbcServ_GetContactsCompleted,
        /// dbcServ_GetScheduledEventsCompleted, dbcServ_GetTasksCompleted
        /// are completed
        /// </summary>
        /// <remarks>
        /// Multiple Asyc calls simultaneously require an object as userstate(Here a Guid is used).
        /// </remarks>
        public void TryGetInformation()
        {   
            dbcServ.GetTasksCompleted += new GetTasksCompletedEventHandler(dbcServ_GetTasksCompleted);
            dbcServ.GetScheduledEventsCompleted += new GetScheduledEventsCompletedEventHandler(dbcServ_GetScheduledEventsCompleted);
            dbcServ.GetContactsCompleted += new GetContactsCompletedEventHandler(dbcServ_GetContactsCompleted);
            dbcServ.CookieContainer = cookiecontainer;
            dbcServ.GetContactsAsync(ContactsGuid);
            dbcServ.GetScheduledEventsAsync(EventsGuid);
            dbcServ.GetTasksAsync(TasksGuid);
        }

        /// <summary>
        /// Contacts Results of the TryGetInformation method. Called by proxy class when attempt was completed
        /// </summary>
        /// <param name="sender">Proxy class reference</param>
        /// <param name="e">GetContactsCompletedEventArgs sent from the proxy object</param>
        private void dbcServ_GetContactsCompleted(object sender, GetContactsCompletedEventArgs e)
        {
            try
            {
                if (e.Error == null)
                {
                    contacts = new List<Contact>(e.Result);
                    ContactsCompleted = true;
                    if (TasksCompleted && ScheduledEventsCompleted)
                        ResetAndCall();
                }
                else
                    throw e.Error;
            }
            catch (Exception ex) 
            {
                Console.WriteLine("dbcServ_GetContactsCompleted : " + ex.ToString());
                GotAllData(false);
            }
        }

        /// <summary>
        /// ScheduledEvents Results of the TryGetInformation method. Called by proxy class when attempt was completed
        /// </summary>
        /// <param name="sender">Proxy class reference</param>
        /// <param name="e">GetScheduledEventsCompletedEventArgs sent from the proxy object</param>
        private void dbcServ_GetScheduledEventsCompleted(object sender, GetScheduledEventsCompletedEventArgs e)
        {
            try
            {
                if (e.Error == null)
                {
                    events = new List<ScheduledEvent>(e.Result);
                    ScheduledEventsCompleted = true;
                    if (TasksCompleted && ContactsCompleted)
                        ResetAndCall();
                }
                else
                    throw e.Error;
            }
            catch (Exception ex)
            {
                Console.WriteLine("dbcServ_GetScheduledEventsCompleted : " + ex.ToString());
                GotAllData(false);
            }
        }

        /// <summary>
        /// Tasks Results of the TryGetInformation method. Called by proxy class when attempt was completed
        /// </summary>
        /// <param name="sender">Proxy class reference</param>
        /// <param name="e">dbcServ_GetTasksCompleted sent from the proxy object</param>
        private void dbcServ_GetTasksCompleted(object sender, GetTasksCompletedEventArgs e)
        {
            try
            {
                if (e.Error == null)
                {
                    tasks = new List<Task>(e.Result);
                    TasksCompleted = true;
                    if (ContactsCompleted && ScheduledEventsCompleted)
                        ResetAndCall();
                }
                else
                    throw e.Error;
            }
            catch (Exception ex)
            {
                Console.WriteLine("dbcServ_GetTasksCompleted : " + ex.ToString());
                GotAllData(false);
            }
        }

        /// <summary>
        /// ResetAndCall is a mechanism that uses 3 boolean variables to see if 3 async function
        /// calls have completed and if so calls the UI back to inform static data variables in this
        /// class are now initialized and ready to be used.
        /// </summary>
        private void ResetAndCall()
        {
            ContactsCompleted = false;
            TasksCompleted = false;
            ScheduledEventsCompleted = false;
            GotAllData(true);
        }

        #endregion

        #region Getting Profile Information

        /// <summary>
        /// Tries to contact profile service and gets profile data of the current user.
        /// </summary>
        /// <remarks>
        /// Needs authentication cookie provided by login function. So this must be called only after login.
        /// </remarks>
        public void TryGetProfile()
        {
            proServ.CookieContainer = cookiecontainer;
            proServ.GetAllPropertiesForCurrentUserCompleted += new GetAllPropertiesForCurrentUserCompletedEventHandler(proServ_GetAllPropertiesForCurrentUserCompleted);            
            proServ.GetAllPropertiesForCurrentUserAsync(true, true, ProfileGuid);
        }

        /// <summary>
        /// Profile info from the TryGetProfile method. Called by proxy class when attempt was completed.
        /// </summary>
        /// <param name="sender">Proxy class reference</param>
        /// <param name="e">proServ_GetAllPropertiesForCurrentUserCompleted sent from the proxy object</param>
        private void proServ_GetAllPropertiesForCurrentUserCompleted(object sender, GetAllPropertiesForCurrentUserCompletedEventArgs e)
        {
            try
            {
                if (e.Error == null)
                {
                    profiledata = e.Result;
                    GotProfile(true);
                }
                else
                    throw e.Error;
            }
            catch (Exception ex)
            {
                Console.WriteLine("proServ_GetAllPropertiesForCurrentUserCompleted : " + ex.ToString());
                GotProfile(false);
            }
        }

        #endregion
    }
}
