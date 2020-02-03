/*
 ThoTH Technology (c) 2008 Firmament
*/
using System;
using System.Collections.Generic;

namespace TriviaGameWebAPI.Core
{
	/// <summary>
	/// Question object for NHibernate mapped table 'Question'.
	/// </summary>
	[Serializable]
	public partial class Question
	{
		#region Member Variables@name
		
		protected Guid _questionid;
		protected Genre _genre;
		protected int _questionnumber;
		protected string _questiondescription;
		protected int _questionduration;
		protected IList<Answer> _answers;
		protected IList<Choice> _choices;
		#endregion
		#region Constructors
			
		public Question() {}
					
		public Question(Guid questionid, Genre genre, int questionnumber, string questiondescription, int questionduration) 
		{
			this._questionid= questionid;
			this._genre= genre;
			this._questionnumber= questionnumber;
			this._questiondescription= questiondescription;
			this._questionduration= questionduration;
		}

		#endregion
		#region Public Properties
		
		public  virtual Guid QuestionId
		{
			get 
				{
				return _questionid; 
				}
			
			set {if (value != this._questionid){_questionid= value;}}
				
		}
		
		public  virtual Genre Genre
		{
			get 
				{
				return _genre; 
				}
			
			set {_genre= value; }
		}
		
		public  virtual int QuestionNumber
		{
			get 
				{
				return _questionnumber; 
				}
			
			set {if (value != this._questionnumber){_questionnumber= value;}}
				
		}
		
		public  virtual string QuestionDescription
		{
			get 
				{
				return _questiondescription; 
				}
			
			set {
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("value", value.ToString(), "QuestionDescription cannot contain more than 500 characters");
				if (value != this._questiondescription){_questiondescription= value;}}
				
		}
		
		public  virtual int QuestionDuration
		{
			get 
				{
				return _questionduration; 
				}
			
			set {if (value != this._questionduration){_questionduration= value;}}
				
		}
		
		public  virtual IList<Answer> Answers
		{
			get 
				{
				return _answers; 
				}
			
			set {_answers= value; }
		}
		
		public  virtual IList<Choice> Choices
		{
			get 
				{
				return _choices; 
				}
			
			set {_choices= value; }
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
			Question castObj = (Question)obj;
			return ( castObj != null ) &&
			this._questionid == castObj.QuestionId;
		}
		/// <summary>
		/// local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			int hash = 57;
			hash = 27 * hash * _questionid.GetHashCode();
			return hash;
		}
		#endregion
		
	}
}
