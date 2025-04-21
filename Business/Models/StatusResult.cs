﻿using Domain.Models;

namespace Business.Models;

public class StatusResult
{
    public bool Succeeded { get; set; }
    public int StatusCode { get; set; }
    public string? Error { get; set; }
    public Status? Result { get; set; }
}
