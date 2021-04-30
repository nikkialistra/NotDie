using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UniRx;

namespace Core.StatSystem
{
	[Serializable]
	public class Stat
	{
		public IReadOnlyReactiveProperty<float> Value;
		public readonly ReadOnlyCollection<StatModifier> StatModifiers;
		
		private float BaseValue;
		private ReactiveProperty<bool> IsDirty = new ReactiveProperty<bool>();
		
		private readonly List<StatModifier> _statModifiers;

		public Stat(float baseValue)
		{
			_statModifiers = new List<StatModifier>();
			StatModifiers = _statModifiers.AsReadOnly();

			Value = IsDirty.ObserveEveryValueChanged(x => x.Value)
				.Where(x => x == true)
				.Select(_ =>
				{
					IsDirty.Value = false;
					return CalculateFinalValue();
				})
				.ToReactiveProperty();

			BaseValue = baseValue;
			IsDirty.Value = true;
		}

		public virtual void AddModifier(StatModifier modifier)
		{
			_statModifiers.Add(modifier);
			IsDirty.Value = true;
		}

		public virtual bool RemoveModifier(StatModifier modifier)
		{
			if (_statModifiers.Remove(modifier))
			{
				IsDirty.Value = true;
				return true;
			}
			return false;
		}

		public virtual bool RemoveAllModifiersFromSource(object source)
		{
			var removedCount = _statModifiers.RemoveAll(modifier => modifier.Source == source);

			if (removedCount > 0)
			{
				IsDirty.Value = true;
				return true;
			}
			return false;
		}

		private int CompareModifierOrder(StatModifier a, StatModifier b)
		{
			if (a.Order < b.Order)
				return -1;
			if (a.Order > b.Order)
				return 1;
			return 0;
		}
		
		private float CalculateFinalValue()
		{
			var finalValue = BaseValue;
			var percentAddSum = 0f;

			_statModifiers.Sort(CompareModifierOrder);

			for (var i = 0; i < _statModifiers.Count; i++)
			{
				var modifier = _statModifiers[i];

				switch (modifier.Type)
				{
					case StatModifierType.EarlyFlat:
						finalValue += modifier.Value;
						break;
					case StatModifierType.PercentAdd:
					{
						percentAddSum += modifier.Value;

						if (i + 1 >= _statModifiers.Count || _statModifiers[i + 1].Type != StatModifierType.PercentAdd)
						{
							finalValue *= 1 + percentAddSum;
							percentAddSum = 0;
						}

						break;
					}
					case StatModifierType.PercentMultiply:
						finalValue *= 1 + modifier.Value;
						break;
					case StatModifierType.LateFlat:
						finalValue += modifier.Value;
						break;
				}
			}

			// Workaround for float calculation errors, like displaying 12.00001 instead of 12
			return (float)Math.Round(finalValue, 4);
		}
	}
}
