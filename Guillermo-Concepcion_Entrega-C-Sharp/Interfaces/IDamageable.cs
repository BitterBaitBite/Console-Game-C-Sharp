using System;

namespace Guillermo_Concepcion_Entrega_C_Sharp.Interfaces {
	interface IDamageable {
		int MaxHealth { get; }

		int CurrentHealth { get; }

		int ReceiveDamage(int rawDamage);

		bool IsAlive();

		void PrintHealthBar(ConsoleColor color);
	}
}
