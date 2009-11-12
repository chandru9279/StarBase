namespace Thon.ZaszBlog.Support.CodedRepresentations
{

  //A generic collection with the ability to check if it has been changed.
  [System.Serializable]
  public class StateList<T> : System.Collections.Generic.List<T>
  {
    #region Base overrides

      public override int GetHashCode()
		{
			string hash = string.Empty;
			foreach (T item in this) hash += item.GetHashCode().ToString();
			return hash.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			if (obj.GetType() == this.GetType()) return obj.GetHashCode() == this.GetHashCode();
			return false;
		}

    #endregion

		private int _HasCode = 0;

        public virtual bool IsChanged
        {
          get{return this.GetHashCode() != _HasCode;}
        }

		public virtual void MarkOld()
		{
			_HasCode = this.GetHashCode();
			base.TrimExcess();
		}
  }
}