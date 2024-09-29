namespace Models.Enums;

/// <summary>
/// Describes what field of Product was changed.
/// </summary>
public enum WhatProductFieldChanged
{
	None = 0,
	CurrentPrice = 1,
	OriginPrice = 2,
	All = 999
}
