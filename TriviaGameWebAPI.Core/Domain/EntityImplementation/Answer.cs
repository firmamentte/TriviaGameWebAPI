
namespace TriviaGameWebAPI.Core
{
    public partial class Answer
    {
        public virtual bool IsQuestionAnswered
        {
            get
            {
                return Choice != null;
            }
        }

        public virtual string QuestionDescription
        {
            get
            {
                return Question.QuestionDescription;
            }
        }

        public virtual int QuestionNumber
        {
            get
            {
                return Question.QuestionNumber;
            }
        }

        public virtual int QuestionDuration
        {
            get
            {
                return Question.QuestionDuration;
            }
        }

        public virtual bool IsAnswerCorrect
        {
            get
            {
                if (Choice != null)
                    return Choice.IsCorrect;
                else
                    return false;
            }
        }

        public virtual string AnswerDescription
        {
            get
            {
                if (Choice != null)
                    return Choice.ChoiceName;
                else
                    return null;
            }
        }

        public virtual int Score
        {
            get
            {
                if (IsAnswerCorrect)
                    return QuestionDuration - AnswerDuration;
                else
                    return 0;
            }
        }
    }
}
