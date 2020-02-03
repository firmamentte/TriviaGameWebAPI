
using System;
using TriviaGameWebAPI.Core;

namespace TriviaGameWebAPI.Data
{
    public static partial class TriviaGameWebAPIDAL
    {
        public static Genre GetGenreByGenreName(string genreName)
        {
            try
            {
                return
                NHibernateSessionManager.Instance.GetSession().
                QueryOver<Genre>().
                Where(genre => genre.GenreName == genreName).
                SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool IsQuestionAnswered(Guid gameId, Guid questionId)
        {
            try
            {
                return
                NHibernateSessionManager.Instance.GetSession().
                QueryOver<Answer>().
                Where(answer => answer.Question == GetQuestionById(questionId)).
                JoinQueryOver(answer => answer.Game).
                Where(game => game.GameId == gameId).
                SingleOrDefault() != null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
