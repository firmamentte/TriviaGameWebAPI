using System.Collections.Generic;

namespace TriviaGameWebAPI.Core
{
    public partial class Game
    {
        public virtual string GenreName
        {
            get
            {
                return Genre.GenreName;
            }
        }

        public virtual List<Question> Questions
        {
            get
            {
                List<Question> _questions = new List<Question>();

                foreach (var question in Genre.Questions)
                {
                    _questions.Add(question);
                }

                return _questions;
            }
        }

        public virtual int TotalScore
        {
            get
            {
                int _totalScore = 0;

                foreach (var answer in Answers)
                {
                    _totalScore += answer.Score;
                }

                return _totalScore;
            }
        }
    }
}
