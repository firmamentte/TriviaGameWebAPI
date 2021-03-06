﻿using System;
using System.Collections.Generic;

namespace TriviaGameWebAPI.BLL.DataContract
{
    public class GameResultResp
    {
        public Guid GameId { get; set; }
        public string CreationDate { get; set; }
        public string GenreName { get; set; }
        public int TotalScore { get; set; }
        public List<AnswerResp> Answers { get; set; } = new List<AnswerResp>();
    }
}
