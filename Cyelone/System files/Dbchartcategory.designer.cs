﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sales_and_Inventory_System__Gadgets_Shop_
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Ceylone_Moter_DB")]
	public partial class DbchartcategoryDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public DbchartcategoryDataContext() : 
				base(global::Sales_and_Inventory_System__Gadgets_Shop_.Properties.Settings.Default.Ceylone_Moter_DBConnectionString1, mappingSource)
		{
			OnCreated();
		}
		
		public DbchartcategoryDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DbchartcategoryDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DbchartcategoryDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DbchartcategoryDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<viewcateg> viewcategs
		{
			get
			{
				return this.GetTable<viewcateg>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.viewcateg")]
	public partial class viewcateg
	{
		
		private string _CategoryName;
		
		private System.Nullable<int> _TotalQuantity;
		
		public viewcateg()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CategoryName", DbType="VarChar(250) NOT NULL", CanBeNull=false)]
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
					this._CategoryName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TotalQuantity", DbType="Int")]
		public System.Nullable<int> TotalQuantity
		{
			get
			{
				return this._TotalQuantity;
			}
			set
			{
				if ((this._TotalQuantity != value))
				{
					this._TotalQuantity = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
