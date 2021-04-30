using System;
using UnityEngine;

namespace Core.StatSystem
{
	public enum StatType
	{
		FullHealth,
		Speed,
		Damage,
		Agility,
		Fortune,
		Armor,
		Spikes,
		Vampirism,
		Regeneration
	}
	
	public enum StatModifierType
	{
		EarlyFlat = 100,
		PercentAdd = 200,
		PercentMultiply = 300,
		LateFlat = 400
	}

	[Serializable]
	public class StatModifier
	{
		public StatType StatType;
		
		[Space]
		public float Value;
		public StatModifierType Type;
		
		public int Order { get; private set; }
		public object Source { get; private set; }
	}
}
