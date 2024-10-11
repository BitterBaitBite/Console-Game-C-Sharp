using Guillermo_Concepcion_Entrega_C_Sharp.Types;
using System.Collections.Generic;

namespace Guillermo_Concepcion_Entrega_C_Sharp.Localization {
	static class CharacterStatLocalization {
		public static readonly Dictionary<CharacterStatEnum, string> es_ES = new Dictionary<CharacterStatEnum, string> {
			{CharacterStatEnum.Strength, "fuerza" },
			{CharacterStatEnum.Constitution, "constitución" },
			{CharacterStatEnum.Dexterity, "destreza" },
			{CharacterStatEnum.Intelligence, "inteligencia" },
		};
	}
}
