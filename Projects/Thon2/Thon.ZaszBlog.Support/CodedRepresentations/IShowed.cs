using System;
using System.Collections.Generic;

namespace Thon.ZaszBlog.Support.CodedRepresentations
{
  public interface IShowed
  {
    String Title { get; }
    String Content { get;}
    DateTime DateCreated { get; }
    DateTime DateModified { get; }
    Guid Id { get;  }
    String RelativeLink { get;}
    Uri AbsoluteLink { get;}
    String Description { get;}
    String Author { get;}
    void OnServing(ServingEventArgs eventArgs);
    StateList<Category> Categories { get;}
    bool IsVisible { get;}
  }
}