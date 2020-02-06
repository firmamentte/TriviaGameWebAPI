
using System;
using System.Threading.Tasks;
using TriviaGameWebAPI.Core;

namespace TriviaGameWebAPI.Data
{
    public static partial class TriviaGameWebAPIDAL
    {
        public static async Task<Genre> GetGenreByGenreName(string genreName)
        {
            try
            {
                return
                await NHibernateSessionManager.Instance.GetSession().
                QueryOver<Genre>().
                Where(genre => genre.GenreName == genreName).
                SingleOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<bool> IsQuestionAnswered(Guid gameId, Guid questionId)
        {
            try
            {
                Question _question = await GetQuestionById(questionId);

                return
                await NHibernateSessionManager.Instance.GetSession().
                QueryOver<Answer>().
                Where(answer => answer.Question == _question).
                JoinQueryOver(answer => answer.Game).
                Where(game => game.GameId == gameId).
                SingleOrDefaultAsync() != null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
