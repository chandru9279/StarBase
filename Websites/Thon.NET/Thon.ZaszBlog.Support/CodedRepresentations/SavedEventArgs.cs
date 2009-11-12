using System;
using System.Collections.Generic;
using System.Text;

namespace Thon.ZaszBlog.Support.CodedRepresentations
{

    public enum SaveAction
    {  
        None,  
        Insert,    
        Update,    
        Delete
    }

    // Arguments for the two custom events onSaving & onSaved
    public class SavedEventArgs : EventArgs
    {
        public SavedEventArgs(SaveAction action)
        {
          Action = action;
        }

        private SaveAction _Action;
        public SaveAction Action
        {
          get { return _Action; }
          set { _Action = value; }
        }
    }
}
