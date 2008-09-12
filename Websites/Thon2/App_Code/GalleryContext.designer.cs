﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Thon.Gallery
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[System.Data.Linq.Mapping.DatabaseAttribute(Name="ThonBase")]
	public partial class GalleryContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertPhoto(Photo instance);
    partial void UpdatePhoto(Photo instance);
    partial void DeletePhoto(Photo instance);
    partial void InsertPhotoCategoryJoin(PhotoCategoryJoin instance);
    partial void UpdatePhotoCategoryJoin(PhotoCategoryJoin instance);
    partial void DeletePhotoCategoryJoin(PhotoCategoryJoin instance);
    partial void InsertFlickrCommand(FlickrCommand instance);
    partial void UpdateFlickrCommand(FlickrCommand instance);
    partial void DeleteFlickrCommand(FlickrCommand instance);
    partial void InsertCategory(Category instance);
    partial void UpdateCategory(Category instance);
    partial void DeleteCategory(Category instance);
    #endregion
		
		public GalleryContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["ThonSqlServer"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public GalleryContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public GalleryContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public GalleryContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public GalleryContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Photo> Photos
		{
			get
			{
				return this.GetTable<Photo>();
			}
		}
		
		public System.Data.Linq.Table<PhotoCategoryJoin> PhotoCategoryJoins
		{
			get
			{
				return this.GetTable<PhotoCategoryJoin>();
			}
		}
		
		public System.Data.Linq.Table<FlickrCommand> FlickrCommands
		{
			get
			{
				return this.GetTable<FlickrCommand>();
			}
		}
		
		public System.Data.Linq.Table<Category> Categories
		{
			get
			{
				return this.GetTable<Category>();
			}
		}
	}
	
	[Table(Name="dbo.Photos")]
	public partial class Photo : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _PhotoID;
		
		private System.Nullable<System.DateTime> _PhotoDate;
		
		private string _PhotoDescription;
		
		private System.Nullable<short> _PhotoWidth;
		
		private System.Nullable<short> _PhotoHeight;
		
		private System.Nullable<short> _PhotoResolution;
		
		private bool _PhotoVisible;
		
		private string _PhotoOwner;
		
		private string _PhotoFlickrID;
		
		private EntitySet<PhotoCategoryJoin> _PhotoCategoryJoins;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnPhotoIDChanging(int value);
    partial void OnPhotoIDChanged();
    partial void OnPhotoDateChanging(System.Nullable<System.DateTime> value);
    partial void OnPhotoDateChanged();
    partial void OnPhotoDescriptionChanging(string value);
    partial void OnPhotoDescriptionChanged();
    partial void OnPhotoWidthChanging(System.Nullable<short> value);
    partial void OnPhotoWidthChanged();
    partial void OnPhotoHeightChanging(System.Nullable<short> value);
    partial void OnPhotoHeightChanged();
    partial void OnPhotoResolutionChanging(System.Nullable<short> value);
    partial void OnPhotoResolutionChanged();
    partial void OnPhotoVisibleChanging(bool value);
    partial void OnPhotoVisibleChanged();
    partial void OnPhotoOwnerChanging(string value);
    partial void OnPhotoOwnerChanged();
    partial void OnPhotoFlickrIDChanging(string value);
    partial void OnPhotoFlickrIDChanged();
    #endregion
		
		public Photo()
		{
			this._PhotoCategoryJoins = new EntitySet<PhotoCategoryJoin>(new Action<PhotoCategoryJoin>(this.attach_PhotoCategoryJoins), new Action<PhotoCategoryJoin>(this.detach_PhotoCategoryJoins));
			OnCreated();
		}
		
		[Column(Storage="_PhotoID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int PhotoID
		{
			get
			{
				return this._PhotoID;
			}
			set
			{
				if ((this._PhotoID != value))
				{
					this.OnPhotoIDChanging(value);
					this.SendPropertyChanging();
					this._PhotoID = value;
					this.SendPropertyChanged("PhotoID");
					this.OnPhotoIDChanged();
				}
			}
		}
		
		[Column(Storage="_PhotoDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> PhotoDate
		{
			get
			{
				return this._PhotoDate;
			}
			set
			{
				if ((this._PhotoDate != value))
				{
					this.OnPhotoDateChanging(value);
					this.SendPropertyChanging();
					this._PhotoDate = value;
					this.SendPropertyChanged("PhotoDate");
					this.OnPhotoDateChanged();
				}
			}
		}
		
		[Column(Storage="_PhotoDescription", DbType="VarChar(255)")]
		public string PhotoDescription
		{
			get
			{
				return this._PhotoDescription;
			}
			set
			{
				if ((this._PhotoDescription != value))
				{
					this.OnPhotoDescriptionChanging(value);
					this.SendPropertyChanging();
					this._PhotoDescription = value;
					this.SendPropertyChanged("PhotoDescription");
					this.OnPhotoDescriptionChanged();
				}
			}
		}
		
		[Column(Storage="_PhotoWidth", DbType="SmallInt")]
		public System.Nullable<short> PhotoWidth
		{
			get
			{
				return this._PhotoWidth;
			}
			set
			{
				if ((this._PhotoWidth != value))
				{
					this.OnPhotoWidthChanging(value);
					this.SendPropertyChanging();
					this._PhotoWidth = value;
					this.SendPropertyChanged("PhotoWidth");
					this.OnPhotoWidthChanged();
				}
			}
		}
		
		[Column(Storage="_PhotoHeight", DbType="SmallInt")]
		public System.Nullable<short> PhotoHeight
		{
			get
			{
				return this._PhotoHeight;
			}
			set
			{
				if ((this._PhotoHeight != value))
				{
					this.OnPhotoHeightChanging(value);
					this.SendPropertyChanging();
					this._PhotoHeight = value;
					this.SendPropertyChanged("PhotoHeight");
					this.OnPhotoHeightChanged();
				}
			}
		}
		
		[Column(Storage="_PhotoResolution", DbType="SmallInt")]
		public System.Nullable<short> PhotoResolution
		{
			get
			{
				return this._PhotoResolution;
			}
			set
			{
				if ((this._PhotoResolution != value))
				{
					this.OnPhotoResolutionChanging(value);
					this.SendPropertyChanging();
					this._PhotoResolution = value;
					this.SendPropertyChanged("PhotoResolution");
					this.OnPhotoResolutionChanged();
				}
			}
		}
		
		[Column(Storage="_PhotoVisible", DbType="Bit NOT NULL")]
		public bool PhotoVisible
		{
			get
			{
				return this._PhotoVisible;
			}
			set
			{
				if ((this._PhotoVisible != value))
				{
					this.OnPhotoVisibleChanging(value);
					this.SendPropertyChanging();
					this._PhotoVisible = value;
					this.SendPropertyChanged("PhotoVisible");
					this.OnPhotoVisibleChanged();
				}
			}
		}
		
		[Column(Storage="_PhotoOwner", DbType="VarChar(50)")]
		public string PhotoOwner
		{
			get
			{
				return this._PhotoOwner;
			}
			set
			{
				if ((this._PhotoOwner != value))
				{
					this.OnPhotoOwnerChanging(value);
					this.SendPropertyChanging();
					this._PhotoOwner = value;
					this.SendPropertyChanged("PhotoOwner");
					this.OnPhotoOwnerChanged();
				}
			}
		}
		
		[Column(Storage="_PhotoFlickrID", DbType="VarChar(50)")]
		public string PhotoFlickrID
		{
			get
			{
				return this._PhotoFlickrID;
			}
			set
			{
				if ((this._PhotoFlickrID != value))
				{
					this.OnPhotoFlickrIDChanging(value);
					this.SendPropertyChanging();
					this._PhotoFlickrID = value;
					this.SendPropertyChanged("PhotoFlickrID");
					this.OnPhotoFlickrIDChanged();
				}
			}
		}
		
		[Association(Name="Photo_PhotoCategoryJoin", Storage="_PhotoCategoryJoins", OtherKey="PhotoNumber")]
		public EntitySet<PhotoCategoryJoin> PhotoCategoryJoins
		{
			get
			{
				return this._PhotoCategoryJoins;
			}
			set
			{
				this._PhotoCategoryJoins.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_PhotoCategoryJoins(PhotoCategoryJoin entity)
		{
			this.SendPropertyChanging();
			entity.Photo = this;
		}
		
		private void detach_PhotoCategoryJoins(PhotoCategoryJoin entity)
		{
			this.SendPropertyChanging();
			entity.Photo = null;
		}
	}
	
	[Table(Name="dbo.PhotoCategoryJoin")]
	public partial class PhotoCategoryJoin : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _PhotoNumber;
		
		private int _CategoryNumber;
		
		private EntityRef<Photo> _Photo;
		
		private EntityRef<Category> _Category;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnPhotoNumberChanging(int value);
    partial void OnPhotoNumberChanged();
    partial void OnCategoryNumberChanging(int value);
    partial void OnCategoryNumberChanged();
    #endregion
		
		public PhotoCategoryJoin()
		{
			this._Photo = default(EntityRef<Photo>);
			this._Category = default(EntityRef<Category>);
			OnCreated();
		}
		
		[Column(Storage="_PhotoNumber", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int PhotoNumber
		{
			get
			{
				return this._PhotoNumber;
			}
			set
			{
				if ((this._PhotoNumber != value))
				{
					if (this._Photo.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnPhotoNumberChanging(value);
					this.SendPropertyChanging();
					this._PhotoNumber = value;
					this.SendPropertyChanged("PhotoNumber");
					this.OnPhotoNumberChanged();
				}
			}
		}
		
		[Column(Storage="_CategoryNumber", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int CategoryNumber
		{
			get
			{
				return this._CategoryNumber;
			}
			set
			{
				if ((this._CategoryNumber != value))
				{
					if (this._Category.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCategoryNumberChanging(value);
					this.SendPropertyChanging();
					this._CategoryNumber = value;
					this.SendPropertyChanged("CategoryNumber");
					this.OnCategoryNumberChanged();
				}
			}
		}
		
		[Association(Name="Photo_PhotoCategoryJoin", Storage="_Photo", ThisKey="PhotoNumber", IsForeignKey=true)]
		public Photo Photo
		{
			get
			{
				return this._Photo.Entity;
			}
			set
			{
				Photo previousValue = this._Photo.Entity;
				if (((previousValue != value) 
							|| (this._Photo.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Photo.Entity = null;
						previousValue.PhotoCategoryJoins.Remove(this);
					}
					this._Photo.Entity = value;
					if ((value != null))
					{
						value.PhotoCategoryJoins.Add(this);
						this._PhotoNumber = value.PhotoID;
					}
					else
					{
						this._PhotoNumber = default(int);
					}
					this.SendPropertyChanged("Photo");
				}
			}
		}
		
		[Association(Name="Category_PhotoCategoryJoin", Storage="_Category", ThisKey="CategoryNumber", IsForeignKey=true)]
		public Category Category
		{
			get
			{
				return this._Category.Entity;
			}
			set
			{
				Category previousValue = this._Category.Entity;
				if (((previousValue != value) 
							|| (this._Category.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Category.Entity = null;
						previousValue.PhotoCategoryJoins.Remove(this);
					}
					this._Category.Entity = value;
					if ((value != null))
					{
						value.PhotoCategoryJoins.Add(this);
						this._CategoryNumber = value.CategoryID;
					}
					else
					{
						this._CategoryNumber = default(int);
					}
					this.SendPropertyChanged("Category");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="dbo.FlickrCommands")]
	public partial class FlickrCommand : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _FlickrCommandID;
		
		private string _FlickrCommandType;
		
		private System.Nullable<int> _FlickrCommandPhoto;
		
		private string _FlickrCommandParameter;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnFlickrCommandIDChanging(int value);
    partial void OnFlickrCommandIDChanged();
    partial void OnFlickrCommandTypeChanging(string value);
    partial void OnFlickrCommandTypeChanged();
    partial void OnFlickrCommandPhotoChanging(System.Nullable<int> value);
    partial void OnFlickrCommandPhotoChanged();
    partial void OnFlickrCommandParameterChanging(string value);
    partial void OnFlickrCommandParameterChanged();
    #endregion
		
		public FlickrCommand()
		{
			OnCreated();
		}
		
		[Column(Storage="_FlickrCommandID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int FlickrCommandID
		{
			get
			{
				return this._FlickrCommandID;
			}
			set
			{
				if ((this._FlickrCommandID != value))
				{
					this.OnFlickrCommandIDChanging(value);
					this.SendPropertyChanging();
					this._FlickrCommandID = value;
					this.SendPropertyChanged("FlickrCommandID");
					this.OnFlickrCommandIDChanged();
				}
			}
		}
		
		[Column(Storage="_FlickrCommandType", DbType="VarChar(50)")]
		public string FlickrCommandType
		{
			get
			{
				return this._FlickrCommandType;
			}
			set
			{
				if ((this._FlickrCommandType != value))
				{
					this.OnFlickrCommandTypeChanging(value);
					this.SendPropertyChanging();
					this._FlickrCommandType = value;
					this.SendPropertyChanged("FlickrCommandType");
					this.OnFlickrCommandTypeChanged();
				}
			}
		}
		
		[Column(Storage="_FlickrCommandPhoto", DbType="Int")]
		public System.Nullable<int> FlickrCommandPhoto
		{
			get
			{
				return this._FlickrCommandPhoto;
			}
			set
			{
				if ((this._FlickrCommandPhoto != value))
				{
					this.OnFlickrCommandPhotoChanging(value);
					this.SendPropertyChanging();
					this._FlickrCommandPhoto = value;
					this.SendPropertyChanged("FlickrCommandPhoto");
					this.OnFlickrCommandPhotoChanged();
				}
			}
		}
		
		[Column(Storage="_FlickrCommandParameter", DbType="VarChar(50)")]
		public string FlickrCommandParameter
		{
			get
			{
				return this._FlickrCommandParameter;
			}
			set
			{
				if ((this._FlickrCommandParameter != value))
				{
					this.OnFlickrCommandParameterChanging(value);
					this.SendPropertyChanging();
					this._FlickrCommandParameter = value;
					this.SendPropertyChanged("FlickrCommandParameter");
					this.OnFlickrCommandParameterChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="dbo.Categories")]
	public partial class Category : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _CategoryID;
		
		private string _CategoryOwner;
		
		private string _CategoryName;
		
		private string _CategoryFlickrID;
		
		private EntitySet<PhotoCategoryJoin> _PhotoCategoryJoins;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCategoryIDChanging(int value);
    partial void OnCategoryIDChanged();
    partial void OnCategoryOwnerChanging(string value);
    partial void OnCategoryOwnerChanged();
    partial void OnCategoryNameChanging(string value);
    partial void OnCategoryNameChanged();
    partial void OnCategoryFlickrIDChanging(string value);
    partial void OnCategoryFlickrIDChanged();
    #endregion
		
		public Category()
		{
			this._PhotoCategoryJoins = new EntitySet<PhotoCategoryJoin>(new Action<PhotoCategoryJoin>(this.attach_PhotoCategoryJoins), new Action<PhotoCategoryJoin>(this.detach_PhotoCategoryJoins));
			OnCreated();
		}
		
		[Column(Storage="_CategoryID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int CategoryID
		{
			get
			{
				return this._CategoryID;
			}
			set
			{
				if ((this._CategoryID != value))
				{
					this.OnCategoryIDChanging(value);
					this.SendPropertyChanging();
					this._CategoryID = value;
					this.SendPropertyChanged("CategoryID");
					this.OnCategoryIDChanged();
				}
			}
		}
		
		[Column(Storage="_CategoryOwner", DbType="VarChar(50)")]
		public string CategoryOwner
		{
			get
			{
				return this._CategoryOwner;
			}
			set
			{
				if ((this._CategoryOwner != value))
				{
					this.OnCategoryOwnerChanging(value);
					this.SendPropertyChanging();
					this._CategoryOwner = value;
					this.SendPropertyChanged("CategoryOwner");
					this.OnCategoryOwnerChanged();
				}
			}
		}
		
		[Column(Storage="_CategoryName", DbType="VarChar(50)")]
		public string CategoryName
		{
			get
			{
				return this._CategoryName;
			}
			set
			{
				if ((this._CategoryName != value))
				{
					this.OnCategoryNameChanging(value);
					this.SendPropertyChanging();
					this._CategoryName = value;
					this.SendPropertyChanged("CategoryName");
					this.OnCategoryNameChanged();
				}
			}
		}
		
		[Column(Storage="_CategoryFlickrID", DbType="VarChar(50)")]
		public string CategoryFlickrID
		{
			get
			{
				return this._CategoryFlickrID;
			}
			set
			{
				if ((this._CategoryFlickrID != value))
				{
					this.OnCategoryFlickrIDChanging(value);
					this.SendPropertyChanging();
					this._CategoryFlickrID = value;
					this.SendPropertyChanged("CategoryFlickrID");
					this.OnCategoryFlickrIDChanged();
				}
			}
		}
		
		[Association(Name="Category_PhotoCategoryJoin", Storage="_PhotoCategoryJoins", OtherKey="CategoryNumber")]
		public EntitySet<PhotoCategoryJoin> PhotoCategoryJoins
		{
			get
			{
				return this._PhotoCategoryJoins;
			}
			set
			{
				this._PhotoCategoryJoins.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_PhotoCategoryJoins(PhotoCategoryJoin entity)
		{
			this.SendPropertyChanging();
			entity.Category = this;
		}
		
		private void detach_PhotoCategoryJoins(PhotoCategoryJoin entity)
		{
			this.SendPropertyChanging();
			entity.Category = null;
		}
	}
}
#pragma warning restore 1591
