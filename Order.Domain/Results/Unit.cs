﻿namespace Order.Domain.Results;

public sealed class Unit
{
    public static readonly Unit Value = new Unit();
    private Unit() { }
}
