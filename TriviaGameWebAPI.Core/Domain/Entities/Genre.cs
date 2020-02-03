/*
 ThoTH Technology (c) 2008 Firmament
*/
using System;
using System.Collections.Generic;

namespace TriviaGameWebAPI.Core
{
	/// <summary>
	/// Genre object for NHibernate mapped table 'Genre'.
	/// </summary>
	[Serializable]
	public partial class Genre
	{
		#region Member Variables@name
		
		protected Guid _genreid;
		protected string _genrename;
		protected IList<Game> _games;
		protected IList<Question> _questions;
		#endregion
		#region Constructors
			
		public Genre() {}
					
		public Genre(Guid genreid, string genrename) 
		{
			this._genreid= genreid;
			this._genrename= genrename;
		}

		#endregion
		#region Public Properties
		
		public  virtual Guid GenreId
		{
			get 
				{
				return _genreid; 
				}
			
			set {if (value != this._genreid){_genreid= value;}}
				
		}
		
		public  virtual string GenreName
		{
			get 
				{
				return _genrename; 
				}
			
			set {
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("value", value.ToString(), "GenreName cannot contain more than 50 characters");
				if (value != this._genrename){_genrename= value;}}
				
		}
		
		public  virtual IList<Game> Games
		{
			get 
				{
				return _games; 
				}
			
			set {_games= value; }
		}
		
		public  virtual IList<Question> Questions
		{
			get 
				{
				return _questions; 
				}
			
			set {_questions= value; }
		}
		#endregion
		
		#region Equals And HashCode Overrides
		/// <summary>
		/// local implementation of Equals based on unique value members
		/// </summary>
		public override bool Equals( object obj )
		{
			if( this == obj ) return true;
			if( ( obj == null ) || ( obj.GetType() != this.GetType() ) ) return false;
			Genre castObj = (Genre)obj;
			return ( castObj != null ) &&
			this._genreid == castObj.GenreId;
		}
		/// <summary>
		/// local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			int hash = 57;
			hash = 27 * hash * _genreid.GetHashCode();
			return hash;
		}
		#endregion
		
	}
}
