using Guillermo_Concepcion_Entrega_C_Sharp.Types;
using System;

namespace Guillermo_Concepcion_Entrega_C_Sharp.Classes {
	class Armor : Item {
		public int HealthBonus { get; protected set; }

		public Tuple<CharacterStatEnum, int> StatBonus { get; protected set; }

		public Tuple<CharacterStatEnum, int> StatRestriction { get; protected set; }

		public Armor(
			ushort id,
			string name,
			string description,
			int buyValue,
			int healthBonus,
			Tuple<CharacterStatEnum, int> statBonus,
			Tuple<CharacterStatEnum, int> statRestriction = null
		) : base(id, name, description, buyValue) {
			HealthBonus = healthBonus;
			StatBonus = statBonus;

			if (statRestriction != null) {
				StatRestriction = statRestriction;
			}
		}

		public override string ToString() {
			string healthBonus = $"Vida +{HealthBonus}".PadRight(12).PadLeft(16);
			string statBonus = $"{StatBonus.Item1} +{StatBonus.Item2}".PadRight(16).PadLeft(20);

			string statRestriction = "".PadRight(16).PadLeft(20);
			if (StatRestriction != null) {
				statRestriction = $"{StatRestriction.Item1} >= {StatRestriction.Item2}".PadRight(16).PadLeft(20);
			}

			return $" {Id.ToString().PadRight(6).PadLeft(8)} | {Name.PadRight(12).PadLeft(14)} | {BuyValue.ToString().PadRight(6).PadLeft(8)} | {healthBonus} | {statBonus} | {statRestriction} ";
		}
	}
}
