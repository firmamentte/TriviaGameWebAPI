using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriviaGameWebAPI.BLL.DataContract;
using TriviaGameWebAPI.Core;
using TriviaGameWebAPI.Data;

namespace TriviaGameWebAPI.BLL
{
    public static class TriviaGameWebAPIBLL
    {
        private static void BeginTransaction()
        {
            NHibernateSessionManager.Instance.BeginTransaction();
        }

        private static async Task CommitTransaction()
        {
            await NHibernateSessionManager.Instance.CommitTransaction();
        }

        public static void InitialiseAppSettings(IConfiguration configuration)
        {
            try
            {
                if (configuration != null)
                {
                    if (string.IsNullOrWhiteSpace(FirmamentUtilities.DatabaseHelper.ConnectionString))
                    {
                        FirmamentUtilities.DatabaseHelper.ConnectionString = configuration.GetConnectionString("DatabasePath");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static class GenreHelper
        {
            public static async Task<List<GenreResp>> GetGenres()
            {
                try
                {
                    List<GenreResp> _genreResps = new List<GenreResp>();

                    foreach (var genre in await TriviaGameWebAPIDAL.GetAllGenre())
                    {
                        _genreResps.Add(FillGenreResp(genre));
                    }

                    return _genreResps;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            private static GenreResp FillGenreResp(Genre genre)
            {
                try
                {
                    if (genre != null)
                    {
                        return new GenreResp()
                        {
                            GenreName = genre.GenreName
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public static class GameHelper
        {
            public static async Task<GameResp> CreateGame(string genreName)
            {
                try
                {
                    Genre _genre = await TriviaGameWebAPIDAL.GetGenreByGenreName(genreName);

                    if (_genre is null)
                    {
                        throw new Exception("Invalid Genre Name. The resource has been removed, had its name changed, or is unavailable.");
                    }

                    Game _game = new Game()
                    {
                        Genre = _genre,
                        CreationDate = DateTime.Now,
                        Answers = new List<Answer>()
                    };

                    BeginTransaction();

                    await TriviaGameWebAPIDAL.SaveGame(_game);

                    await CommitTransaction();

                    return FillGameResp(_game);
                }
                catch (Exception)
                {

                    throw;
                }
            }

            public static async Task<bool> AnswerQuestion(AnswerQuestionReq answerQuestionReq)
            {
                try
                {
                    Game _game = await TriviaGameWebAPIDAL.GetGameById(answerQuestionReq.GameId);

                    if (_game is null)
                    {
                        throw new Exception("Invalid Game Id. The resource has been removed, had its name changed, or is unavailable.");
                    }

                    Question _question = await TriviaGameWebAPIDAL.GetQuestionById(answerQuestionReq.QuestionId);

                    if (_question is null)
                    {
                        throw new Exception("Invalid Question Id. The resource has been removed, had its name changed, or is unavailable.");
                    }

                    if (await TriviaGameWebAPIDAL.IsQuestionAnswered(answerQuestionReq.GameId, answerQuestionReq.QuestionId))
                    {
                        throw new Exception("Question already answered");
                    }

                    Choice _choice = null;

                    if (answerQuestionReq.ChoiceId != null)
                    {
                        _choice = await TriviaGameWebAPIDAL.GetChoiceById((Guid)answerQuestionReq.ChoiceId);

                        if (_choice is null)
                        {
                            throw new Exception("Invalid Choice Id. The resource has been removed, had its name changed, or is unavailable.");
                        }
                    }

                    answerQuestionReq.AnswerDuration = answerQuestionReq.AnswerDuration > _question.QuestionDuration ?
                                                       _question.QuestionDuration :
                                                       answerQuestionReq.AnswerDuration;

                    Answer _answer = new Answer()
                    {
                        Game = _game,
                        Question = _question,
                        Choice = _choice,
                        AnswerDuration = answerQuestionReq.ChoiceId != null ? answerQuestionReq.AnswerDuration : _question.QuestionDuration
                    };

                    BeginTransaction();

                    await TriviaGameWebAPIDAL.SaveAnswer(_answer);

                    await CommitTransaction();

                    _game.Answers.Add(_answer);

                    return _choice is null ? false : _choice.IsCorrect;
                }
                catch (Exception)
                {

                    throw;
                }
            }

            public static async Task<GameResultResp> ViewGame(Guid gameId)
            {
                try
                {
                    Game _game = await TriviaGameWebAPIDAL.GetGameById(gameId);

                    if (_game is null)
                    {
                        throw new Exception("Invalid Game Id. The resource has been removed, had its name changed, or is unavailable.");
                    }

                    return FillGameResultResp(_game);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            private static GameResp FillGameResp(Game game)
            {
                try
                {
                    if (game != null)
                    {
                        GameResp _gameResp = new GameResp()
                        {
                            GameId = game.GameId,
                            CreationDate = game.CreationDate.ToString("dd-MMMM-yyyy hh:mm:ss tt"),
                            GenreName = game.GenreName
                        };

                        foreach (var question in game.Questions)
                        {
                            _gameResp.Questions.Add(FillQuestionResp(question));
                        }

                        return _gameResp;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            private static QuestionResp FillQuestionResp(Question question)
            {
                try
                {
                    if (question != null)
                    {
                        QuestionResp _questionResp = new QuestionResp()
                        {
                            QuestionId = question.QuestionId,
                            QuestionNumber = question.QuestionNumber,
                            QuestionDescription = question.QuestionDescription,
                            QuestionDuration = question.QuestionDuration
                        };

                        foreach (var choice in question.Choices)
                        {
                            _questionResp.Choices.Add(FillChoiceResp(choice));
                        }

                        return _questionResp;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            private static ChoiceResp FillChoiceResp(Choice choice)
            {
                try
                {
                    if (choice != null)
                    {
                        return new ChoiceResp()
                        {
                            ChoiceId = choice.ChoiceId,
                            ChoiceName = choice.ChoiceName,
                            IsCorrect = choice.IsCorrect
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            private static GameResultResp FillGameResultResp(Game game)
            {
                try
                {
                    if (game != null)
                    {
                        GameResultResp _gameResultResp = new GameResultResp()
                        {
                            GameId = game.GameId,
                            GenreName = game.GenreName,
                            CreationDate = game.CreationDate.ToString("dd-MMMM-yyyy hh:mm:ss tt"),
                            TotalScore = game.TotalScore
                        };

                        foreach (var answer in game.Answers)
                        {
                            _gameResultResp.Answers.Add(FillAnswerResp(answer));
                        }

                        return _gameResultResp;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            private static AnswerResp FillAnswerResp(Answer answer)
            {
                try
                {
                    if (answer != null)
                    {
                        return new AnswerResp()
                        {
                            QuestionDescription = answer.QuestionDescription,
                            QuestionNumber = answer.QuestionNumber,
                            QuestionDuration = answer.QuestionDuration,
                            AnswerDescription = answer.AnswerDescription,
                            AnswerDuration = answer.AnswerDuration,
                            IsAnswerCorrect = answer.IsAnswerCorrect,
                            IsQuestionAnswered = answer.IsQuestionAnswered,
                            Score = answer.Score
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
