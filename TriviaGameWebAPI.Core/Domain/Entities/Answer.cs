/*
 ThoTH Technology (c) 2008 Firmament
*/
using System;
using System.Collections.Generic;

namespace TriviaGameWebAPI.Core
{
	/// <summary>
	/// Answer object for NHibernate mapped table 'Answer'.
	/// </summary>
	[Serializable]
	public partial class Answer
	{
		#region Member Variables@name
		
		protected Guid _answerid;
		protected Game _game;
		protected Question _question;
		protected Choice _choice;
		protected int _answerduration;
		#endregion
		#region Constructors
			
		public Answer() {}
					
		public Answer(Guid answerid, Game game, Question question, int answerduration) 
		{
			this._answerid= answerid;
			this._game= game;
			this._question= question;
			this._answerduration= answerduration;
		}

		#endregion
		#region Public Properties
		
		public  virtual Guid AnswerId
		{
			get 
				{
				return _answerid; 
				}
			
			set {if (value != this._answerid){_answerid= value;}}
				
		}
		
		public  virtual Game Game
		{
			get 
				{
				return _game; 
				}
			
			set {_game= value; }
		}
		
		public  virtual Question Question
		{
			get 
				{
				return _question; 
				}
			
			set {_question= value; }
		}
		
		public  virtual Choice Choice
		{
			get 
				{
				return _choice; 
				}
			
			set {_choice= value; }
		}
		
		public  virtual int AnswerDuration
		{
			get 
				{
				return _answerduration; 
				}
			
			set {if (value != this._answerduration){_answerduration= value;}}
				
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
			Answer castObj = (Answer)obj;
			return ( castObj != null ) &&
			this._answerid == castObj.AnswerId;
		}
		/// <summary>
		/// local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			int hash = 57;
			hash = 27 * hash * _answerid.GetHashCode();
			return hash;
		}
		#endregion
		
	}
}
