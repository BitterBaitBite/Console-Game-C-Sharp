using Guillermo_Concepcion_Entrega_C_Sharp.Classes;
using Guillermo_Concepcion_Entrega_C_Sharp.Types;
using System;
using System.Collections.Generic;

namespace Guillermo_Concepcion_Entrega_C_Sharp.Database {
	// TODO SECONDARY descriptions, +items
	class ItemDatabase {
		public static readonly Dictionary<int, Armor> ArmorDB = new Dictionary<int, Armor>() {
			{100, new Armor(100, "Arm. pesada", "", 50, 10, new Tuple<CharacterStatEnum, int>(CharacterStatEnum.Constitution, 2), new Tuple<CharacterStatEnum, int>(CharacterStatEnum.Strength, 15))},
			{101, new Armor(101, "Arm. cuero", "", 30, 3, new Tuple<CharacterStatEnum, int>(CharacterStatEnum.Dexterity, 2))},
			{102, new Armor(102, "Toga mágica", "", 40, 5, new Tuple<CharacterStatEnum, int>(CharacterStatEnum.Intelligence, 2), new Tuple<CharacterStatEnum, int>(CharacterStatEnum.Intelligence, 15))},
		};

		public static readonly Dictionary<int, Weapon> WeaponDB = new Dictionary<int, Weapon>() {
			{200, new Weapon(200, "Espada corta", "", 40, 2, 5, 1.2f)},
			{201, new Weapon(201, "Hacha de guerra", "", 60, 6, 10, 1.5f)},
			{202, new Weapon(202, "Bastón mágico", "", 50, 1, 5, 2f)}
		};
	}
}
