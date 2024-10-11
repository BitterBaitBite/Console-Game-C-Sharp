using Guillermo_Concepcion_Entrega_C_Sharp.Types;
using System.Collections.Generic;

namespace Guillermo_Concepcion_Entrega_C_Sharp.Localization {
	static class EnemyLocalization {
		public static readonly Dictionary<EnemyEnum, string> es_ES = new Dictionary<EnemyEnum, string> {
			{EnemyEnum.Mimic, "Mímico" },
			{EnemyEnum.Goblin, "Trasgo" },
			{EnemyEnum.Orc, "Orco" },
			{EnemyEnum.Serpent, "Sierpe" },
			{EnemyEnum.Dragon, "Dragón" },
		};
	}
}
