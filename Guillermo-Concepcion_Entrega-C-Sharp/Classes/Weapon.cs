namespace Guillermo_Concepcion_Entrega_C_Sharp.Classes {
	class Weapon : Item {
		public int MinDamage { get; protected set; }

		public int MaxDamage { get; protected set; }

		public float CriticalBonus { get; protected set; }

		public Weapon(
			ushort id,
			string name,
			string description,
			int buyValue,
			int minDamage,
			int maxDamage,
			float criticalBonus
		) : base(id, name, description, buyValue) {
			MinDamage = minDamage;
			MaxDamage = maxDamage;
			CriticalBonus = criticalBonus;
		}

		public override string ToString() {
			string minDamage = $"Daño min. {MinDamage}".PadRight(14).PadLeft(18);
			string maxDamage = $"Daño max. {MaxDamage}".PadRight(14).PadLeft(18);
			string criticalBonus = $"Crítico x{CriticalBonus}".PadRight(14).PadLeft(18);

			return $" {Id.ToString().PadRight(6).PadLeft(8)} | {Name.PadRight(14).PadLeft(16)} | {BuyValue.ToString().PadRight(6).PadLeft(8)} | {minDamage} | {maxDamage} | {criticalBonus} ";
		}
	}
}
