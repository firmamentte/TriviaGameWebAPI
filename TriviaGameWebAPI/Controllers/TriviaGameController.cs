using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
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
        public async Task<ActionResult> GetGenres()
        {
            try
            {
                #region RequestValidation

                #endregion

                List<GenreResp> _genreResps = await TriviaGameWebAPIBLL.GenreHelper.GetGenres();

                return Ok(new
                {
                    meta = new { code = HttpStatusCode.OK },
                    data = _genreResps
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    meta = new
                    {
                        code = HttpStatusCode.BadRequest,
                        message = ex.Message
                    }
                });
            }
        }

        [Route("V1/CreateGame")]
        [HttpPost]
        public async Task<ActionResult> CreateGame([FromBody]string genreName)
        {
            try
            {
                #region RequestValidation

                ModelState.Clear();

                if (string.IsNullOrWhiteSpace(genreName))
                {
                    ModelState.AddModelError("GenreName", "genreName required");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        meta = new
                        {
                            code = HttpStatusCode.BadRequest,
                            message = ControllerHelper.GetModelStateErrors(ModelState)
                        }
                    });
                }

                #endregion

                GameResp _gameResp = await TriviaGameWebAPIBLL.GameHelper.CreateGame(genreName);

                return Created(string.Empty, new
                {
                    meta = new
                    {
                        code = HttpStatusCode.Created
                    },
                    data = _gameResp
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    meta = new
                    {
                        code = HttpStatusCode.BadRequest,
                        message = ex.Message
                    }
                });
            }
        }

        [Route("V1/AnswerQuestion")]
        [HttpPut]
        public async Task<ActionResult> AnswerQuestion([FromBody]AnswerQuestionReq answerQuestionReq)
        {
            try
            {
                #region RequestValidation

                ModelState.Clear();

                if (answerQuestionReq is null)
                {
                    ModelState.AddModelError("AnswerQuestionReq", "answerQuestionReq can not be null");
                }
                else
                {
                    if (answerQuestionReq.GameId == Guid.Empty)
                    {
                        ModelState.AddModelError("GameId", "GameId must be a globally unique identifier and not empty");
                    }

                    if (answerQuestionReq.QuestionId == Guid.Empty)
                    {
                        ModelState.AddModelError("QuestionId", "QuestionId must be a globally unique identifier and not empty");
                    }

                    if (answerQuestionReq.ChoiceId != null)
                    {
                        if (answerQuestionReq.ChoiceId == Guid.Empty)
                        {
                            ModelState.AddModelError("ChoiceId", "ChoiceId must be a globally unique identifier and not empty");
                        }
                    }

                    if (answerQuestionReq.AnswerDuration < 0)
                    {
                        ModelState.AddModelError("AnswerDuration", "AnswerDuration can not be less than zero");
                    }
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        meta = new
                        {
                            code = HttpStatusCode.BadRequest,
                            message = ControllerHelper.GetModelStateErrors(ModelState)
                        }
                    });
                }

                #endregion

                bool _isCorrect = await TriviaGameWebAPIBLL.GameHelper.AnswerQuestion(answerQuestionReq);

                return Ok(new
                {
                    meta = new { code = HttpStatusCode.OK },
                    data = _isCorrect
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    meta = new
                    {
                        code = HttpStatusCode.BadRequest,
                        message = ex.Message
                    }
                });
            }
        }

        [Route("V1/ViewGame")]
        [HttpGet]
        public async Task<ActionResult> ViewGame([FromQuery]Guid gameId)
        {
            try
            {
                #region RequestValidation

                ModelState.Clear();

                if (gameId == Guid.Empty)
                {
                    ModelState.AddModelError("GameId", "gameId must be a globally unique identifier and not empty");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        meta = new
                        {
                            code = HttpStatusCode.BadRequest,
                            message = ControllerHelper.GetModelStateErrors(ModelState)
                        }
                    });
                }

                #endregion

                GameResultResp _gameResultResp = await TriviaGameWebAPIBLL.GameHelper.ViewGame(gameId);

                return Ok(new
                {
                    meta = new { code = HttpStatusCode.OK },
                    data = _gameResultResp
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    meta = new
                    {
                        code = HttpStatusCode.BadRequest,
                        message = ex.Message
                    }
                });
            }
        }
    }
}