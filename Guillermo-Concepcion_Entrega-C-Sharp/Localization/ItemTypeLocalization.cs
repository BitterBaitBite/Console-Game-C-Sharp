using Guillermo_Concepcion_Entrega_C_Sharp.Types;
using System.Collections.Generic;

namespace Guillermo_Concepcion_Entrega_C_Sharp.Localization {
	static class ItemTypeLocalization {
		public static readonly Dictionary<ItemTypeEnum, string> es_ES = new Dictionary<ItemTypeEnum, string> {
			{ItemTypeEnum.Armor, "armadura" },
			{ItemTypeEnum.Weapon, "arma" },
		};
	}
}
