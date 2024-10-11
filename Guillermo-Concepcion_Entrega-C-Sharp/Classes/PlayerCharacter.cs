using Guillermo_Concepcion_Entrega_C_Sharp.Interfaces;
using Guillermo_Concepcion_Entrega_C_Sharp.Localization;
using Guillermo_Concepcion_Entrega_C_Sharp.Types;
using Guillermo_Concepcion_Entrega_C_Sharp.Utils;
using System;
using System.Collections.Generic;

namespace Guillermo_Concepcion_Entrega_C_Sharp.Classes {
	// TODO ¡IMPORTANTE! en la selección de equipo, elegir según la restricción de los objetos
	class PlayerCharacter : Character {
		public CharacterClassEnum CharacterClass { get; protected set; }
		public int MaxStatPoints { get => 40; }
		public Armor EquippedArmor { get; protected set; }

		public Weapon EquippedWeapon { get; protected set; }

		public int Money { get; protected set; }

		public PlayerCharacter(string name, Armor armor, Weapon weapon, int money, Dictionary<CharacterStatEnum, int> stats) : base(name, stats) {
			EquippedArmor = armor;
			EquippedWeapon = weapon;
			Money = money;
		}

		// Default implementation - not used
		protected override void InitializeHealth() {
			int constitutionModifier = Stats.Find(el => el.Type == CharacterStatEnum.Constitution).Modifier;
			MaxHealth = 20 + constitutionModifier;
			MaxHealth += EquippedArmor.HealthBonus;
			CurrentHealth = MaxHealth;
		}

		// Default implementation - not used
		public override int DoDamage(IDamageable damageable, bool criticalAttack = false) {
			int weaponDamage = (int) RandomUtils.GetRandom(EquippedWeapon.MinDamage, EquippedWeapon.MaxDamage);
			float totalDamage = (int) RandomUtils.GetRandom(3, 6 + 1) + weaponDamage;

			if (criticalAttack) {
				totalDamage *= EquippedWeapon.CriticalBonus;
			}

			return damageable.ReceiveDamage((int) Math.Floor(totalDamage));
		}

		// TODO SECONDARY añadir localización de los tipos de ataque
		public override AttackTypeEnum GetAttackType() {
			int attackCount = Enum.GetValues(typeof(AttackTypeEnum)).Length;
			ConsoleUtils.Notification("Elige un tipo de ataque:");

			int attackIndex = 1;
			foreach (AttackTypeEnum attackType in Enum.GetValues(typeof(AttackTypeEnum))) {
				ConsoleUtils.System("\t{0}.- {1}", attackIndex.ToString(), AttackTypeLocalization.es_ES[attackType]);
				attackIndex++;
			}

			if (!int.TryParse(Console.ReadLine(), out attackIndex)) {
				ConsoleUtils.Error("El valor introducido no es válido, inténtelo de nuevo.");

				return GetAttackType();
			}

			if (attackIndex < 1 || attackIndex > attackCount) {
				ConsoleUtils.Error("La opción introducida no es válida, inténtelo de nuevo.\n");

				return GetAttackType();
			}

			Console.WriteLine();
			return (AttackTypeEnum) (attackIndex - 1);
		}

		// Prints the player health bar with green color by default
		public override void PrintHealthBar(ConsoleColor color = ConsoleColor.DarkGreen) {
			base.PrintHealthBar(color);
		}
	}
}