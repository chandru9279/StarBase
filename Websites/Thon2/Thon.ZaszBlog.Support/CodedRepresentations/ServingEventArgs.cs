using System;
using System.Collections.Generic;
using System.Text;

namespace Thon.ZaszBlog.Support.CodedRepresentations
{
  
  // Used when a IShowed is served to the output stream.  
  public class ServingEventArgs : EventArgs
  {
    public ServingEventArgs(string body, ServingLocation location)
    {
      _Body = body;
      _Location = location;
    }

    private string _Body;
    public string Body
    {
      get { return _Body; }
      set { _Body = value; }
    }

    private ServingLocation _Location;
    public ServingLocation Location
    {
      get { return _Location; }
      set { _Location = value; }
    }

    private bool _Cancel;
    public bool Cancel
    {
      get { return _Cancel; }
      set { _Cancel = value; }
    }

  }

  
  // The location where the serving takes place
  public enum ServingLocation
  {
      None, // Is used to indicate that a location hasn't been chosen.
      SinglePost, //Is used when a single post is served from post.aspx.
      PostList, //Is used when multiple posts are served using postlist.ascx.
      SinglePage, //Is used when a single page is displayed on page.aspx.
      Feed, //Is used when content is served from a feed (RSS or ATOM) for future use.
      Other //Is used when content is served on a custom location.
  }
}
