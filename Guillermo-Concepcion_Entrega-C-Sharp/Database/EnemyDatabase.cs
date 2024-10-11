using Guillermo_Concepcion_Entrega_C_Sharp.Classes;
using Guillermo_Concepcion_Entrega_C_Sharp.Localization;
using Guillermo_Concepcion_Entrega_C_Sharp.Types;
using Guillermo_Concepcion_Entrega_C_Sharp.Utils;
using System.Collections.Generic;

namespace Guillermo_Concepcion_Entrega_C_Sharp.Database {
	class EnemyDatabase {
		public static readonly int FIRST_INDEX = 500;

		public static readonly Dictionary<int, Enemy> EnemyDB = new Dictionary<int, Enemy>() {
			{500, new Enemy(500, EnemyLocalization.es_ES[EnemyEnum.Mimic], 1, GetEnemyStats(EnemyEnum.Mimic))},
			{501, new Enemy(501, EnemyLocalization.es_ES[EnemyEnum.Goblin], 1, GetEnemyStats(EnemyEnum.Goblin))},
			{502, new Enemy(502, EnemyLocalization.es_ES[EnemyEnum.Orc], 1, GetEnemyStats(EnemyEnum.Orc))},
			{503, new Enemy(503, EnemyLocalization.es_ES[EnemyEnum.Serpent], 1, GetEnemyStats(EnemyEnum.Serpent))},
			{504, new Enemy(504, EnemyLocalization.es_ES[EnemyEnum.Dragon], 1, GetEnemyStats(EnemyEnum.Dragon))},
		};

		private static Dictionary<CharacterStatEnum, int> GetEnemyStats(EnemyEnum enemyType) {
			Dictionary<CharacterStatEnum, int> characterStats = new Dictionary<CharacterStatEnum, int>();

			switch (enemyType) {
				case EnemyEnum.Mimic:
					characterStats.Add(CharacterStatEnum.Strength, 5);
					characterStats.Add(CharacterStatEnum.Constitution, 10);
					characterStats.Add(CharacterStatEnum.Dexterity, 6);
					break;

				case EnemyEnum.Goblin:
					characterStats.Add(CharacterStatEnum.Strength, 8);
					characterStats.Add(CharacterStatEnum.Constitution, 8);
					characterStats.Add(CharacterStatEnum.Dexterity, 8);
					break;

				case EnemyEnum.Orc:
					characterStats.Add(CharacterStatEnum.Strength, 10);
					characterStats.Add(CharacterStatEnum.Constitution, 10);
					characterStats.Add(CharacterStatEnum.Dexterity, 7);
					break;

				case EnemyEnum.Serpent:
					characterStats.Add(CharacterStatEnum.Strength, 10);
					characterStats.Add(CharacterStatEnum.Constitution, 12);
					characterStats.Add(CharacterStatEnum.Dexterity, 8);
					break;

				case EnemyEnum.Dragon:
					characterStats.Add(CharacterStatEnum.Strength, 15);
					characterStats.Add(CharacterStatEnum.Constitution, 15);
					characterStats.Add(CharacterStatEnum.Dexterity, 5);
					break;
			}

			return characterStats;
		}

		public static Enemy GetRandomEnemy() {
			int DBFirst = 500;
			int DBLength = 5;
			int enemyRandomId = (int) RandomUtils.GetRandom(DBFirst, DBFirst + DBLength);

			Enemy enemy = null;
			bool hasEnemy = false;

			while (!hasEnemy) {
				hasEnemy = EnemyDB.TryGetValue(enemyRandomId, out enemy);
			}

			return enemy;
		}
	}
}
