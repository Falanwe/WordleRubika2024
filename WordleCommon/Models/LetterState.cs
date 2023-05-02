﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Wordle.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum LetterState
    {
        Missing,
        Misplaced,
        Placed
    };
}
