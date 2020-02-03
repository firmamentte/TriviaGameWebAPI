/*
 ThoTH Technology (c) 2008 Firmament
*/
using System;
using System.Collections.Generic;

namespace TriviaGameWebAPI.Core
{
	/// <summary>
	/// Choice object for NHibernate mapped table 'Choice'.
	/// </summary>
	[Serializable]
	public partial class Choice
	{
		#region Member Variables@name
		
		protected Guid _choiceid;
		protected Question _question;
		protected string _choicename;
		protected bool _iscorrect;
		protected IList<Answer> _answers;
		#endregion
		#region Constructors
			
		public Choice() {}
					
		public Choice(Guid choiceid, Question question, string choicename, bool iscorrect) 
		{
			this._choiceid= choiceid;
			this._question= question;
			this._choicename= choicename;
			this._iscorrect= iscorrect;
		}

		#endregion
		#region Public Properties
		
		public  virtual Guid ChoiceId
		{
			get 
				{
				return _choiceid; 
				}
			
			set {if (value != this._choiceid){_choiceid= value;}}
				
		}
		
		public  virtual Question Question
		{
			get 
				{
				return _question; 
				}
			
			set {_question= value; }
		}
		
		public  virtual string ChoiceName
		{
			get 
				{
				return _choicename; 
				}
			
			set {
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("value", value.ToString(), "ChoiceName cannot contain more than 50 characters");
				if (value != this._choicename){_choicename= value;}}
				
		}
		
		public  virtual bool IsCorrect
		{
			get 
				{
				return _iscorrect; 
				}
			
			set {if (value != this._iscorrect){_iscorrect= value;}}
				
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
			Choice castObj = (Choice)obj;
			return ( castObj != null ) &&
			this._choiceid == castObj.ChoiceId;
		}
		/// <summary>
		/// local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			int hash = 57;
			hash = 27 * hash * _choiceid.GetHashCode();
			return hash;
		}
		#endregion
		
	}
}
