using System;
using System.Text;
using System.ComponentModel;
using System.Collections.Specialized;

namespace Thon.ZaszBlog.Support.CodedRepresentations
{
	
	//This is the base class from which most business objects will be derived. 	
    //TYPE:The type of the derived class.
    //KEY:The type of the Id property.(Usually a GUID) 
    //"where" allows instantiating objects of type "TYPE" from here ....
	[Serializable]
	public abstract class BusinessBase<TYPE, KEY> : IDataErrorInfo, INotifyPropertyChanged, IChangeTracking, IDisposable where TYPE : BusinessBase<TYPE, KEY>, new()
	{

		#region Properties

		private KEY _Id;		
		public KEY Id
		{
			get { return _Id; }
			set { _Id = value; }
		}

		private DateTime _DateCreated = DateTime.MinValue;		
		public DateTime DateCreated
		{
			get
			{ return _DateCreated; }
			set 
			{
				if (_DateCreated != value) MarkChanged("DateCreated");
				_DateCreated = value; 
			}
		}

		private DateTime _DateModified = DateTime.MinValue;		
		public DateTime DateModified
		{
			get { return _DateModified; }
			set { _DateModified = value; }
		}

		#endregion

		#region IsNew, IsDeleted, IsChanged

		private bool _IsNew = true;
		public bool IsNew
		{
			get { return _IsNew; }
		}

		private bool _IsDeleted;
		public bool IsDeleted
		{
			get { return _IsDeleted; }
		}

		private bool _IsChanged = true;
		public virtual bool IsChanged
		{
			get { return _IsChanged; }
		}

		public void Delete()
		{
			_IsDeleted = true;
			_IsChanged = true;
		}

		private StringCollection _ChangedProperties = new StringCollection();
		protected virtual StringCollection ChangedProperties
		{
			get { return _ChangedProperties; }
		}

		protected virtual void MarkChanged(string propertyName)
		{
			_IsChanged = true;
			if (!_ChangedProperties.Contains(propertyName))
			{
				_ChangedProperties.Add(propertyName);
			}
			OnPropertyChanged(propertyName);
		}

		public virtual void MarkOld()
		{
			_IsChanged = false;
			_IsNew = false;
			_ChangedProperties.Clear();
		}

		#endregion

		#region Validation
                
        
		private StringDictionary _BrokenRules = new StringDictionary();

        // only for use from subclasses 
        protected abstract void ValidationRules();
		protected void AddRule(string propertyName, string errorMessage, bool isBroken)
		{
			if (isBroken)
			{
				_BrokenRules[propertyName] = errorMessage;
			}
			else
			{
				if (_BrokenRules.ContainsKey(propertyName))
				{
					_BrokenRules.Remove(propertyName);
				}
			}
		}
        //end
        
        public bool IsValid
        {
            get
            {
                ValidationRules();
                return this._BrokenRules.Count == 0;
            }
        }
		
		public string ValidationMessage
		{
			get
			{
				if (!IsValid)
				{
					StringBuilder sb = new StringBuilder();
					foreach (string message in this._BrokenRules.Values)
					{
						sb.AppendLine(message);
					}

					return sb.ToString();
				}
				return string.Empty;
			}
		}

		#endregion

		#region Methods
		
		// Loads an instance of the object based on the Id.
		public static TYPE Load(KEY id)
		{
			TYPE instance = new TYPE();            
			instance = instance.DataSelect(id);
			instance.Id = id;

			if (instance != null)
			{
				instance.MarkOld();
				return instance;
			}

			return null;
		}

        public virtual SaveAction Save()
		{
			if (!IsValid && !IsDeleted)
				throw new InvalidOperationException(ValidationMessage);

			if (IsDisposed && !IsDeleted)
				throw new InvalidOperationException("You cannot save a disposed " + this.GetType().Name);

			if (IsChanged)
			{
				return Update();
			}

			return SaveAction.None;
		}

		private SaveAction Update()
		{
			SaveAction action = SaveAction.None;

			if (this.IsDeleted)
			{
				action = SaveAction.Delete;
				OnSaving(this, action);
				DataDelete();
			}
			else
			{
				if (this.IsNew)
				{
					if (_DateCreated == DateTime.MinValue)
						_DateCreated = DateTime.Now;

					_DateModified = DateTime.Now;
					action = SaveAction.Insert;
					OnSaving(this, action);
					DataInsert();
				}
				else
				{
					this._DateModified = DateTime.Now; ;
					action = SaveAction.Update;
					OnSaving(this, action);
					DataUpdate();
				}
				MarkOld();
			}

			OnSaved(this, action);
			return action;
		}

		#endregion

		#region Data access		
		
        // all of these are to be implemented by subclasses
		protected abstract TYPE DataSelect(KEY id);
		protected abstract void DataUpdate();		
		protected abstract void DataInsert();        
		protected abstract void DataDelete();

		#endregion

		#region Equality overrides
        
        // we need to override equality or the entire 2 objects and all values will be compared 
        public override int GetHashCode()
        {   
            return this.Id.GetHashCode();
        }
        // hash code comparison is used coz we don't kno the type of KEY here...
		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			if (obj.GetType() == this.GetType()) return obj.GetHashCode() == this.GetHashCode();
			return false;
		}

		public static bool operator ==(BusinessBase<TYPE, KEY> first, BusinessBase<TYPE, KEY> second)
		{
            if (Object.ReferenceEquals(first, second)) return true;
            if ((object)first == null || (object)second == null) return false;
			return first.GetHashCode() == second.GetHashCode();
		}

		public static bool operator !=(BusinessBase<TYPE, KEY> first, BusinessBase<TYPE, KEY> second)
		{
			return !(first == second);
		}

		#endregion

		#region Events

		public static event EventHandler<SavedEventArgs> Saved;
		protected static void OnSaved(BusinessBase<TYPE, KEY> businessObject, SaveAction action)
		{
			if (Saved != null)
			{
				Saved(businessObject, new SavedEventArgs(action));
			}
		}
		public static event EventHandler<SavedEventArgs> Saving;
		protected static void OnSaving(BusinessBase<TYPE, KEY> businessObject, SaveAction action)
		{
			if (Saving != null)
			{
				Saving(businessObject, new SavedEventArgs(action));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;		
		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}// this is not static, so it is different for different sub classes and so 
        // the event raiser function is virtual

		#endregion

		#region IDisposable

        // very important : an object must not be disposed more'n once
		private bool _IsDisposed;        
		protected bool IsDisposed
		{
			get { return _IsDisposed; }
		}
        // virtual becoz subclasses may need to free more resources
        // protected becoz this must not be called from objects themselves but from Dispose() only
		protected virtual void Dispose(bool disposing)
		{
			if (this.IsDisposed)
				return;

			if (disposing)
			{
				_ChangedProperties.Clear();
				_BrokenRules.Clear();
				_IsDisposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion

		#region IDataErrorInfo Members
		public string Error
		{
			get { return ValidationMessage; }
		}

		public string this[string columnName]
		{
			get
			{
				if (_BrokenRules.ContainsKey(columnName))
					return _BrokenRules[columnName];

				return string.Empty;
			}
		}

		#endregion

		#region IChangeTracking Members
		void IChangeTracking.AcceptChanges()
		{
			Save();
		}
		#endregion
	}
}