/*
 ThoTH Technology (c) 2008 Firmament
*/
using System;
using System.Collections.Generic;

namespace TriviaGameWebAPI.Core
{
	/// <summary>
	/// Game object for NHibernate mapped table 'Game'.
	/// </summary>
	[Serializable]
	public partial class Game
	{
		#region Member Variables@name
		
		protected Guid _gameid;
		protected Genre _genre;
		protected DateTime _creationdate;
		protected IList<Answer> _answers;
		#endregion
		#region Constructors
			
		public Game() {}
					
		public Game(Guid gameid, Genre genre, DateTime creationdate) 
		{
			this._gameid= gameid;
			this._genre= genre;
			this._creationdate= creationdate;
		}

		#endregion
		#region Public Properties
		
		public  virtual Guid GameId
		{
			get 
				{
				return _gameid; 
				}
			
			set {if (value != this._gameid){_gameid= value;}}
				
		}
		
		public  virtual Genre Genre
		{
			get 
				{
				return _genre; 
				}
			
			set {_genre= value; }
		}
		
		public  virtual DateTime CreationDate
		{
			get 
				{
				return _creationdate; 
				}
			
			set {if (value != this._creationdate){_creationdate= value;}}
				
		}
		
		public  virtual IList<Answer> Answers
		{
			get 
				{
				return _answers; 
				}
			
			set {_answers= value; }
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
			Game castObj = (Game)obj;
			return ( castObj != null ) &&
			this._gameid == castObj.GameId;
		}
		/// <summary>
		/// local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			int hash = 57;
			hash = 27 * hash * _gameid.GetHashCode();
			return hash;
		}
		#endregion
		
	}
}
