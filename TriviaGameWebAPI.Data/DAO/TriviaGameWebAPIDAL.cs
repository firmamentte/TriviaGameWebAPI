/*
using MyGeneration/Template/NHibernate (c) by Firmament
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriviaGameWebAPI.Core;

namespace TriviaGameWebAPI.Data
{
    /// <summary>
     /// Exposes access to NHibernate DAO classes.  Motivation for this DAO
    /// framework can be found at on the web
    /// AutoGenerated
    /// </summary>

 public static partial class TriviaGameWebAPIDAL 
     {

		public static async Task RefreshAnswer(Answer inParam)
		{
			await NHibernateSessionManager.Instance.GetSession().RefreshAsync(inParam);
		}
		public static async Task DeleteAnswer(Answer inParam)
		{
			await NHibernateSessionManager.Instance.GetSession().DeleteAsync(inParam);
		}
		public static async Task<List<Answer>> GetAllAnswer()
		{
			return
			await NHibernateSessionManager.Instance.GetSession().
			QueryOver<Answer>().
			ListAsync().ContinueWith(result => result.Result.ToList());
		}
		public static async Task<Answer> GetAnswerById(Guid id)
		{
			return
			await NHibernateSessionManager.Instance.GetSession().
			QueryOver<Answer>().
			Where(entity => entity.AnswerId == id).
			SingleOrDefaultAsync();
		}
		public static async Task SaveAnswer(Answer inParam)
		{
			await NHibernateSessionManager.Instance.GetSession().SaveOrUpdateAsync(inParam);
		}
		public static async Task MergeAnswer(Answer inParam)
		{
			await NHibernateSessionManager.Instance.GetSession().MergeAsync(inParam);
		}
		public static async Task SaveAnswer(List<Answer> inParam)
		{
			foreach (Answer _Answer in inParam)
			{
				await NHibernateSessionManager.Instance.GetSession().SaveOrUpdateAsync(_Answer);
			}
		}
		public static async Task RefreshChoice(Choice inParam)
		{
			await NHibernateSessionManager.Instance.GetSession().RefreshAsync(inParam);
		}
		public static async Task DeleteChoice(Choice inParam)
		{
			await NHibernateSessionManager.Instance.GetSession().DeleteAsync(inParam);
		}
		public static async Task<List<Choice>> GetAllChoice()
		{
			return
			await NHibernateSessionManager.Instance.GetSession().
			QueryOver<Choice>().
			ListAsync().ContinueWith(result => result.Result.ToList());
		}
		public static async Task<Choice> GetChoiceById(Guid id)
		{
			return
			await NHibernateSessionManager.Instance.GetSession().
			QueryOver<Choice>().
			Where(entity => entity.ChoiceId == id).
			SingleOrDefaultAsync();
		}
		public static async Task SaveChoice(Choice inParam)
		{
			await NHibernateSessionManager.Instance.GetSession().SaveOrUpdateAsync(inParam);
		}
		public static async Task MergeChoice(Choice inParam)
		{
			await NHibernateSessionManager.Instance.GetSession().MergeAsync(inParam);
		}
		public static async Task SaveChoice(List<Choice> inParam)
		{
			foreach (Choice _Choice in inParam)
			{
				await NHibernateSessionManager.Instance.GetSession().SaveOrUpdateAsync(_Choice);
			}
		}
		public static async Task RefreshGame(Game inParam)
		{
			await NHibernateSessionManager.Instance.GetSession().RefreshAsync(inParam);
		}
		public static async Task DeleteGame(Game inParam)
		{
			await NHibernateSessionManager.Instance.GetSession().DeleteAsync(inParam);
		}
		public static async Task<List<Game>> GetAllGame()
		{
			return
			await NHibernateSessionManager.Instance.GetSession().
			QueryOver<Game>().
			ListAsync().ContinueWith(result => result.Result.ToList());
		}
		public static async Task<Game> GetGameById(Guid id)
		{
			return
			await NHibernateSessionManager.Instance.GetSession().
			QueryOver<Game>().
			Where(entity => entity.GameId == id).
			SingleOrDefaultAsync();
		}
		public static async Task SaveGame(Game inParam)
		{
			await NHibernateSessionManager.Instance.GetSession().SaveOrUpdateAsync(inParam);
		}
		public static async Task MergeGame(Game inParam)
		{
			await NHibernateSessionManager.Instance.GetSession().MergeAsync(inParam);
		}
		public static async Task SaveGame(List<Game> inParam)
		{
			foreach (Game _Game in inParam)
			{
				await NHibernateSessionManager.Instance.GetSession().SaveOrUpdateAsync(_Game);
			}
		}
		public static async Task RefreshGenre(Genre inParam)
		{
			await NHibernateSessionManager.Instance.GetSession().RefreshAsync(inParam);
		}
		public static async Task DeleteGenre(Genre inParam)
		{
			await NHibernateSessionManager.Instance.GetSession().DeleteAsync(inParam);
		}
		public static async Task<List<Genre>> GetAllGenre()
		{
			return
			await NHibernateSessionManager.Instance.GetSession().
			QueryOver<Genre>().
			ListAsync().ContinueWith(result => result.Result.ToList());
		}
		public static async Task<Genre> GetGenreById(Guid id)
		{
			return
			await NHibernateSessionManager.Instance.GetSession().
			QueryOver<Genre>().
			Where(entity => entity.GenreId == id).
			SingleOrDefaultAsync();
		}
		public static async Task SaveGenre(Genre inParam)
		{
			await NHibernateSessionManager.Instance.GetSession().SaveOrUpdateAsync(inParam);
		}
		public static async Task MergeGenre(Genre inParam)
		{
			await NHibernateSessionManager.Instance.GetSession().MergeAsync(inParam);
		}
		public static async Task SaveGenre(List<Genre> inParam)
		{
			foreach (Genre _Genre in inParam)
			{
				await NHibernateSessionManager.Instance.GetSession().SaveOrUpdateAsync(_Genre);
			}
		}
		public static async Task RefreshQuestion(Question inParam)
		{
			await NHibernateSessionManager.Instance.GetSession().RefreshAsync(inParam);
		}
		public static async Task DeleteQuestion(Question inParam)
		{
			await NHibernateSessionManager.Instance.GetSession().DeleteAsync(inParam);
		}
		public static async Task<List<Question>> GetAllQuestion()
		{
			return
			await NHibernateSessionManager.Instance.GetSession().
			QueryOver<Question>().
			ListAsync().ContinueWith(result => result.Result.ToList());
		}
		public static async Task<Question> GetQuestionById(Guid id)
		{
			return
			await NHibernateSessionManager.Instance.GetSession().
			QueryOver<Question>().
			Where(entity => entity.QuestionId == id).
			SingleOrDefaultAsync();
		}
		public static async Task SaveQuestion(Question inParam)
		{
			await NHibernateSessionManager.Instance.GetSession().SaveOrUpdateAsync(inParam);
		}
		public static async Task MergeQuestion(Question inParam)
		{
			await NHibernateSessionManager.Instance.GetSession().MergeAsync(inParam);
		}
		public static async Task SaveQuestion(List<Question> inParam)
		{
			foreach (Question _Question in inParam)
			{
				await NHibernateSessionManager.Instance.GetSession().SaveOrUpdateAsync(_Question);
			}
		}


}
}
