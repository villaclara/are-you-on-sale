using Models.Enums;

namespace Core.EventArgs;

public class ProductChangedEventArgs(Guid id, long userId, WhatProductFieldChanged productField, object oldValue, object newValue) : System.EventArgs
{
	public Guid ProductId { get; set; } = id;
	public long UserId { get; set; } = userId;
	public WhatProductFieldChanged WhatFieldChanged { get; set; } = productField;
	public object OldValue { get; set; } = oldValue;
	public object NewValue { get; set; } = newValue;
}
