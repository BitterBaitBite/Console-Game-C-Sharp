using Guillermo_Concepcion_Entrega_C_Sharp.Types;
using System.Collections.Generic;

namespace Guillermo_Concepcion_Entrega_C_Sharp.Localization {
	static class EnemyHintLocalization {
		public static readonly Dictionary<EnemyHintEnum, string> es_ES = new Dictionary<EnemyHintEnum, string> {
			{EnemyHintEnum.Swift, "veloz"},
			{EnemyHintEnum.Large, "grande"},
			{EnemyHintEnum.Clever, "hábil"},
			{EnemyHintEnum.Unstoppable, "imparable"},
			{EnemyHintEnum.Lethal, "letal"},
			{EnemyHintEnum.Brutal, "brutal"},
			{EnemyHintEnum.Legendary, "legendario/a"},
		};
	}
}
