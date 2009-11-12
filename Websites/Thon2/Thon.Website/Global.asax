<%@ Application Language="C#" %>
<%@ Import Namespace="System.Threading" %>
<%@ Import Namespace="System.Reflection" %>
<%@ Import Namespace="Thon.ZaszBlog.Support" %>
<%@ Import Namespace="Thon.ZaszBlog.Support.Web.Controls" %>

<script runat="server">

    /// <summary>
    /// Hooks up the available extensions located in the App_Code folder.
    /// An extension must be decorated with the ExtensionAttribute to work.
    /// <example>
    /// <code>
    /// [Extension("Description of the SomeExtension class")]
    /// public class SomeExtension
    /// {
    ///   //There must be a parameterless default constructor.
    ///   public SomeExtension()
    ///   {
    ///     //Hook up to the events.
    ///   }
    /// }
    /// </code>
    /// </example>
    /// </summary>
    void Application_Start(object sender, EventArgs e)
    {
        try
        {
            Assembly a = Assembly.Load("App_Code");
            Type[] types = a.GetTypes();
           
            foreach (Type type in types)
            {
                object[] attributes = type.GetCustomAttributes(typeof(ExtensionAttribute), false);
                foreach (object attribute in attributes)
                {
                    if (ExtensionManager.ExtensionEnabled(type.Name))
                    {
                        a.CreateInstance(type.FullName);
                    }
                }
            }
        }
        catch (System.IO.FileNotFoundException)
        {
            // There are no extensions in the App_Code folder, this error can be safely ignored
        }
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
