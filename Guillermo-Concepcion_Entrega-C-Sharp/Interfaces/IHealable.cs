namespace Guillermo_Concepcion_Entrega_C_Sharp.Interfaces {
	interface IHealable {
		int MaxHealth { get; }

		int CurrentHealth { get; }

		int Heal();

		int Heal(int amount);
	}
}
