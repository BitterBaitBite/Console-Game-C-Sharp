using Guillermo_Concepcion_Entrega_C_Sharp.Database;
using Guillermo_Concepcion_Entrega_C_Sharp.Localization;
using Guillermo_Concepcion_Entrega_C_Sharp.Types;
using Guillermo_Concepcion_Entrega_C_Sharp.Utils;
using System;

namespace Guillermo_Concepcion_Entrega_C_Sharp.Classes {
	class CombatRoom : Room {
		protected int Round { get; set; }

		public int Level { get; protected set; }

		public EnemyHintEnum DescriptionHint { get; protected set; }

		public Enemy EnemyCharacter { get; protected set; }

		public CombatRoom(int level = 1) {
			Round = 1;
			Level = level;
			EnemyCharacter = new Enemy(EnemyDatabase.GetRandomEnemy(), Level);
			DescriptionHint = GetDescriptionHint();
			Description = $"Entras en una habitación con manchas de sangre en el suelo y las paredes. Frente a tí aparece un/a {EnemyCharacter.Name} {EnemyHintLocalization.es_ES[DescriptionHint]}. ¡Prepárate para el combate!\n";
		}

		// Start room actions and return a boolean indicating if the room actions are finished
		public override bool StartRoom(PlayerCharacter player) {
			ConsoleUtils.Narrator(Description);

			return RoomAction(player);
		}

		// Combat actions, operations and notifications
		protected override bool RoomAction(PlayerCharacter player) {
			AttackTypeEnum previousEnemyAttack = new AttackTypeEnum();
			AttackTypeEnum enemyAttack;

			while (player.IsAlive() && EnemyCharacter.IsAlive()) {
				bool firstRound = Round == 1;

				ConsoleUtils.Combat("Ronda - {0}", Round.ToString());
				ConsoleUtils.Combat("{0} vs '{1} {2}'", player.Name, EnemyCharacter.Name, EnemyHintLocalization.es_ES[DescriptionHint]);
				ConsoleUtils.Combat("________________________________________\n");

				Console.Write("{0}: ", player.Name);
				ConsoleUtils.Player("{0}/{1} ", player.CurrentHealth.ToString(), player.MaxHealth.ToString());
				player.PrintHealthBar();
				Console.WriteLine();

				Console.Write("{0} {1}: ", EnemyCharacter.Name, EnemyHintLocalization.es_ES[DescriptionHint]);
				ConsoleUtils.Enemy("{0}/{1} ", EnemyCharacter.CurrentHealth.ToString(), EnemyCharacter.MaxHealth.ToString());
				EnemyCharacter.PrintHealthBar();
				Console.WriteLine();

				// Player turn
				AttackTypeEnum playerAttack = player.GetAttackType();
				ConsoleUtils.Info("Realizas un ataque de tipo {0}", AttackTypeLocalization.es_ES[playerAttack]);

				// Enemy turn
				do {
					enemyAttack = EnemyCharacter.GetAttackType();
				} while (!firstRound && EnemyCharacter.Level != 1 && previousEnemyAttack == enemyAttack);

				ConsoleUtils.Info("'{0} {1}' usa un ataque de tipo {2}\n", EnemyCharacter.Name, EnemyHintLocalization.es_ES[DescriptionHint], AttackTypeLocalization.es_ES[enemyAttack]);

				CalcDamage(GetCombatResult(playerAttack, enemyAttack), player);

				// Round end and increment
				previousEnemyAttack = enemyAttack;

				ConsoleUtils.Combat("Resultado ronda {0}:\n", Round.ToString());

				Console.Write("{0}: ", player.Name);
				ConsoleUtils.Player("{0}/{1} ", player.CurrentHealth.ToString(), player.MaxHealth.ToString());
				player.PrintHealthBar();
				Console.WriteLine();

				Console.Write("{0} {1}: ", EnemyCharacter.Name, EnemyHintLocalization.es_ES[DescriptionHint]);
				ConsoleUtils.Enemy("{0}/{1} ", EnemyCharacter.CurrentHealth.ToString(), EnemyCharacter.MaxHealth.ToString());
				EnemyCharacter.PrintHealthBar();
				Console.WriteLine();

				ConsoleUtils.SystemOut("Pulse cualquier tecla para continuar...\n");
				Console.ReadKey();
				Console.Clear();
				Round++;
			}

			return player.IsAlive();
		}

		// Get a descriptive hint of the enemy available attack types
		protected EnemyHintEnum GetDescriptionHint() {
			if (EnemyCharacter.AvailableAttacks.Length == 1) {
				switch (EnemyCharacter.AvailableAttacks[0]) {
					case AttackTypeEnum.Fast:
						return EnemyHintEnum.Swift;

					case AttackTypeEnum.Strong:
						return EnemyHintEnum.Large;

					case AttackTypeEnum.Skilled:
						return EnemyHintEnum.Clever;
				}
			}
			else if (EnemyCharacter.AvailableAttacks.Length == 2) {
				switch (EnemyCharacter.AvailableAttacks[0]) {
					case AttackTypeEnum.Fast:
						switch (EnemyCharacter.AvailableAttacks[1]) {
							case AttackTypeEnum.Strong:
								return EnemyHintEnum.Unstoppable;

							case AttackTypeEnum.Skilled:
								return EnemyHintEnum.Lethal;
						}
						break;

					case AttackTypeEnum.Strong:
						switch (EnemyCharacter.AvailableAttacks[1]) {
							case AttackTypeEnum.Fast:
								return EnemyHintEnum.Unstoppable;

							case AttackTypeEnum.Skilled:
								return EnemyHintEnum.Brutal;
						}
						break;

					case AttackTypeEnum.Skilled:
						switch (EnemyCharacter.AvailableAttacks[1]) {
							case AttackTypeEnum.Strong:
								return EnemyHintEnum.Brutal;

							case AttackTypeEnum.Fast:
								return EnemyHintEnum.Lethal;
						}
						break;
				}
			}

			return EnemyHintEnum.Legendary;
		}

		// Get the combat result (win, lose, draw) depending of player and enemy attack types
		protected int GetCombatResult(AttackTypeEnum playerAttack, AttackTypeEnum enemyAttack) {
			if (playerAttack == enemyAttack) {
				return 0;
			}

			switch (playerAttack) {
				case AttackTypeEnum.Fast:
					switch (enemyAttack) {
						case AttackTypeEnum.Strong:
							return 1;
						case AttackTypeEnum.Skilled:
							return -1;
					}
					break;

				case AttackTypeEnum.Strong:
					switch (enemyAttack) {
						case AttackTypeEnum.Fast:
							return -1;
						case AttackTypeEnum.Skilled:
							return 1;
					}
					break;

				case AttackTypeEnum.Skilled:
					switch (enemyAttack) {
						case AttackTypeEnum.Fast:
							return 1;
						case AttackTypeEnum.Strong:
							return -1;
					}
					break;

			}

			return 0;
		}

		// Call the player and enemy damage functions depending of combat result and notify the user
		protected void CalcDamage(int combatResult, PlayerCharacter player) {
			switch (combatResult) {
				case 0:
					ConsoleUtils.Info("Ambos conseguís atacar a vuestro adversario:\n");
					ConsoleUtils.Player("Consigues hacer {0} de daño a '{1} {2}'\n", player.DoDamage(EnemyCharacter).ToString(), EnemyCharacter.Name, EnemyHintLocalization.es_ES[DescriptionHint]);
					ConsoleUtils.Enemy("'{0} {1}' te hace {2} de daño.\n", EnemyCharacter.Name, EnemyHintLocalization.es_ES[DescriptionHint], EnemyCharacter.DoDamage(player, true).ToString());
					break;

				case 1:
					ConsoleUtils.Player("Te adelantas a '{0} {1}' y le atacas.\n", EnemyCharacter.Name, EnemyHintLocalization.es_ES[DescriptionHint]);
					ConsoleUtils.Player("Haces {0} de daño a '{1} {2}'.\n", player.DoDamage(EnemyCharacter, true).ToString(), EnemyCharacter.Name, EnemyHintLocalization.es_ES[DescriptionHint]);
					break;

				case -1:
					ConsoleUtils.Enemy("Tu enemigo se adelanta a tus movimientos y ataca.\n");
					ConsoleUtils.Enemy("'{0} {1}' te hace {2} de daño.\n", EnemyCharacter.Name, EnemyHintLocalization.es_ES[DescriptionHint], EnemyCharacter.DoDamage(player, true).ToString());
					break;
			}
		}
	}
}
