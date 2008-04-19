using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Thon.ZaszBlog.Support.DataServicesAndConfiguration;

public partial class AdminPingServices : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (!Page.IsPostBack)
    {
      BindGrid();
    }

    grid.RowEditing += new GridViewEditEventHandler(grid_RowEditing);
    grid.RowUpdating += new GridViewUpdateEventHandler(grid_RowUpdating);
    grid.RowCancelingEdit += delegate { Response.Redirect(Request.RawUrl); };
    grid.RowDeleting += new GridViewDeleteEventHandler(grid_RowDeleting);
    btnAdd.Click += new EventHandler(btnAdd_Click);
    btnAdd.Text = "Add ping service";
  }

  void btnAdd_Click(object sender, EventArgs e)
  {
    StringCollection col = StaticDataService.LoadPingServices();
    string service = txtNewCategory.Text.ToLowerInvariant();
    if (!col.Contains(service))
    {
      col.Add(service);
      StaticDataService.SavePingServices(col);
    }
    Response.Redirect(Request.RawUrl);
  }

  void grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
  {
    string service = grid.DataKeys[e.RowIndex].Value.ToString();
    StringCollection col = StaticDataService.LoadPingServices();
    col.Remove(service);    
    StaticDataService.SavePingServices(col);
    Response.Redirect(Request.RawUrl);
  }

  void grid_RowUpdating(object sender, GridViewUpdateEventArgs e)
  {
    string service = grid.DataKeys[e.RowIndex].Value.ToString();
    TextBox textbox = (TextBox)grid.Rows[e.RowIndex].FindControl("txtName");
    
    StringCollection col = StaticDataService.LoadPingServices();
    col.Remove(service);
    col.Add(textbox.Text.ToLowerInvariant());
    StaticDataService.SavePingServices(col);
    
    Response.Redirect(Request.RawUrl);
  }

  void grid_RowEditing(object sender, GridViewEditEventArgs e)
  {
    grid.EditIndex = e.NewEditIndex;
    BindGrid();
  }

  private void BindGrid()
  {
    StringCollection col = StaticDataService.LoadPingServices();
    StringDictionary dic = new StringDictionary();
    foreach (string services in col)
    {
      dic.Add(services, services);
    }

    grid.DataKeyNames = new string[] { "key" };
    grid.DataSource = dic;
    grid.DataBind();
  }

}
