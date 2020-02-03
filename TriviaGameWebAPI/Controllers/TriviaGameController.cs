using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TriviaGameWebAPI.BLL;
using TriviaGameWebAPI.BLL.DataContract;

namespace TriviaGameWebAPI.Controllers
{
    [Route("api/TriviaGame")]
    [ApiController]
    public class TriviaGameController : ControllerBase
    {
        public TriviaGameController(IConfiguration configuration)
        {
            TriviaGameWebAPIBLL.InitialiseAppSettings(configuration);
        }

        [Route("V1/GetGenres")]
        [HttpGet]
        public ActionResult<IEnumerable<GenreResp>> GetGenres()
        {
            try
            {
                #region RequestValidation

                #endregion

                return Ok(TriviaGameWebAPIBLL.GenreHelper.GetGenres());
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("V1/CreateGame")]
        [HttpPost]
        public ActionResult<GameResp> CreateGame([FromBody]string genreName)
        {
            try
            {
                #region RequestValidation

                ModelState.Clear();

                if (string.IsNullOrWhiteSpace(genreName))
                {
                    ModelState.AddModelError("GenreName", "Genre Name required");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                #endregion

                GameResp _gameResp = TriviaGameWebAPIBLL.GameHelper.CreateGame(genreName);

                return Created($"{Request.Scheme}://{Request.Host.Value}/api/TriviaGame/V1/ViewGame?gameId={ _gameResp.GameId }", _gameResp);

            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("V1/AnswerQuestion")]
        [HttpPut]
        public ActionResult<bool> AnswerQuestion([FromBody]AnswerQuestionReq answerQuestionReq)
        {
            try
            {
                #region RequestValidation

                ModelState.Clear();

                if (answerQuestionReq is null)
                {
                    ModelState.AddModelError("AnswerQuestionReq", "Answer Question request can not be null");
                }
                else
                {
                    if (answerQuestionReq.GameId == Guid.Empty)
                    {
                        ModelState.AddModelError("GameId", "Game Id must be a globally unique identifier and not empty");
                    }

                    if (answerQuestionReq.QuestionId == Guid.Empty)
                    {
                        ModelState.AddModelError("QuestionId", "Question Id must be a globally unique identifier and not empty");
                    }

                    if (answerQuestionReq.ChoiceId != null)
                    {
                        if (answerQuestionReq.ChoiceId == Guid.Empty)
                        {
                            ModelState.AddModelError("ChoiceId", "Choice Id must be a globally unique identifier and not empty");
                        }
                    }

                    if (answerQuestionReq.AnswerDuration < 0)
                    {
                        ModelState.AddModelError("AnswerDuration", "Answer Duration can not be less than zero");
                    }
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                #endregion

                return Ok(TriviaGameWebAPIBLL.GameHelper.AnswerQuestion(answerQuestionReq));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("V1/ViewGame")]
        [HttpGet]
        public ActionResult<GameResultResp> ViewGame([FromQuery]Guid gameId)
        {
            try
            {
                #region RequestValidation

                ModelState.Clear();

                if (gameId == Guid.Empty)
                {
                    ModelState.AddModelError("GameId", "Game Id must be a globally unique identifier and not empty");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                #endregion

                return Ok(TriviaGameWebAPIBLL.GameHelper.ViewGame(gameId));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}