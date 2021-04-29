using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Core.StatSystem
{
	[Serializable]
	public class Stat
	{
		public float BaseValue;

		public virtual float Value
		{
			get
			{
				if (_isDirty || _lastBaseValue != BaseValue)
				{
					_lastBaseValue = BaseValue;
					_value = CalculateFinalValue();
					_isDirty = false;
				}

				return _value;
			}
		}

		public readonly ReadOnlyCollection<StatModifier> StatModifiers;

		protected bool _isDirty = true;

		protected float _lastBaseValue;

		protected float _value;

		protected readonly List<StatModifier> _statModifiers;

		public Stat()
		{
			_statModifiers = new List<StatModifier>();
			StatModifiers = _statModifiers.AsReadOnly();
		}

		public Stat(float baseValue) : this()
		{
			BaseValue = baseValue;
		}

		public virtual void AddModifier(StatModifier modifier)
		{
			_isDirty = true;
			_statModifiers.Add(modifier);
		}

		public virtual bool RemoveModifier(StatModifier modifier)
		{
			if (_statModifiers.Remove(modifier))
			{
				_isDirty = true;
				return true;
			}
			return false;
		}

		public virtual bool RemoveAllModifiersFromSource(object source)
		{
			var removedCount = _statModifiers.RemoveAll(modifier => modifier.Source == source);

			if (removedCount > 0)
			{
				_isDirty = true;
				return true;
			}
			return false;
		}

		protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
		{
			if (a.Order < b.Order)
				return -1;
			if (a.Order > b.Order)
				return 1;
			return 0;
		}
		
		protected virtual float CalculateFinalValue()
		{
			var finalValue = BaseValue;
			var percentAddSum = 0f;

			_statModifiers.Sort(CompareModifierOrder);

			for (var i = 0; i < _statModifiers.Count; i++)
			{
				var modifier = _statModifiers[i];

				if (modifier.Type == StatModifierType.Flat)
				{
					finalValue += modifier.Value;
				}
				else if (modifier.Type == StatModifierType.PercentAdd)
				{
					percentAddSum += modifier.Value;

					if (i + 1 >= _statModifiers.Count || _statModifiers[i + 1].Type != StatModifierType.PercentAdd)
					{
						finalValue *= 1 + percentAddSum;
						percentAddSum = 0;
					}
				}
				else if (modifier.Type == StatModifierType.PercentMultiply)
				{
					finalValue *= 1 + modifier.Value;
				}
			}

			// Workaround for float calculation errors, like displaying 12.00001 instead of 12
			return (float)Math.Round(finalValue, 4);
		}
	}
}
