using Guillermo_Concepcion_Entrega_C_Sharp.Types;
using System.Collections.Generic;

namespace Guillermo_Concepcion_Entrega_C_Sharp.Localization {
	static class AttackTypeLocalization {
		public static readonly Dictionary<AttackTypeEnum, string> es_ES = new Dictionary<AttackTypeEnum, string> {
			{AttackTypeEnum.Fast, "Rápido" },
			{AttackTypeEnum.Strong, "Fuerte" },
			{AttackTypeEnum.Skilled, "Técnico" },
		};
	}
}
