namespace Core.StatSystem
{
	public enum StatModifierType
	{
		Flat = 100,
		PercentAdd = 200,
		PercentMultiply = 300
	}

	public class StatModifier
	{
		public readonly float Value;
		public readonly StatModifierType Type;
		public readonly int Order;
		public readonly object Source;

		public StatModifier(float value, StatModifierType type, int order, object source = null)
		{
			Value = value;
			Type = type;
			Order = order;
			Source = source;
		}

		public StatModifier(float value, StatModifierType type) : this(value, type, (int) type)
		{
		}

		public StatModifier(float value, StatModifierType type, object source) : this(value, type, (int) type, source)
		{
		}
	}
}
