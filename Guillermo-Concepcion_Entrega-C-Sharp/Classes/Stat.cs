using Guillermo_Concepcion_Entrega_C_Sharp.Types;
using System;

namespace Guillermo_Concepcion_Entrega_C_Sharp.Classes {
	class Stat {
		public CharacterStatEnum Type { get; }
		public int Value { get; }
		public int Modifier { get; }

		public Stat(CharacterStatEnum type, int stat) {
			this.Type = type;
			this.Value = stat;

			this.Modifier = CalcModifier(stat);
		}

		private static int CalcModifier(int stat) {
			return (int) Math.Floor((stat - 10f) / 2f);
		}
	}
}
