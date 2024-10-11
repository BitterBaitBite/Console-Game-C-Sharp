using Guillermo_Concepcion_Entrega_C_Sharp.Types;
using System.Collections.Generic;

namespace Guillermo_Concepcion_Entrega_C_Sharp.Localization {
	static class CharacterClassLocalization {
		public static readonly Dictionary<CharacterClassEnum, string> es_ES = new Dictionary<CharacterClassEnum, string> {
			{CharacterClassEnum.Warrior, "Guerrero" },
			{CharacterClassEnum.Mage, "Mago" },
			{CharacterClassEnum.Rogue, "Pícaro" },
			{CharacterClassEnum.Druid, "Druida" }
		};
	}
}
