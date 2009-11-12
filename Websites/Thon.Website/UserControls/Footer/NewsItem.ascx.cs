using System;

public partial class NewsItemAscx : System.Web.UI.UserControl
{
    private string _title;
    public string Title
    {
        get { return _title; }
        set { _title = value; }
    }

    private string _link;
    public string Link
    {
        get { return _link; }
        set { _link = value; }
    }

    private string _date;
    public string Date
    {
        get { return _date; }
        set { _date = value; }
    }

    private string _content;
    public string Content
    {
        get { return _content; }
        set { _content = value; }
    }
}
