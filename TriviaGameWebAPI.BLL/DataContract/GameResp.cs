﻿using System;
using System.Collections.Generic;

namespace TriviaGameWebAPI.BLL.DataContract
{
    public class GameResp
    {
        public Guid GameId { get; set; }
        public string GenreName { get; set; }
        public string CreationDate { get; set; }
        public List<QuestionResp> Questions { get; set; } = new List<QuestionResp>();
    }
}
