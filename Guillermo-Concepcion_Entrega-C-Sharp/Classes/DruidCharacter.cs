using Guillermo_Concepcion_Entrega_C_Sharp.Interfaces;
using Guillermo_Concepcion_Entrega_C_Sharp.Types;
using Guillermo_Concepcion_Entrega_C_Sharp.Utils;
using System;
using System.Collections.Generic;

namespace Guillermo_Concepcion_Entrega_C_Sharp.Classes {
	class DruidCharacter : PlayerCharacter {

		public DruidCharacter(string name, Armor armor, Weapon weapon, int money, Dictionary<CharacterStatEnum, int> stats) : base(name, armor, weapon, money, stats) {
			InitializeHealth();
		}

		// Health based on higher base + constitution modifier + armor bonus
		protected override void InitializeHealth() {
			int constitutionModifier = Stats.Find(el => el.Type == CharacterStatEnum.Constitution).Modifier;
			int strengthModifier = Stats.Find(el => el.Type == CharacterStatEnum.Strength).Modifier;

			MaxHealth = (int) Math.Floor(RandomUtils.GetRandom(30, 38 + 1) + constitutionModifier / 2 + strengthModifier / 2);
			MaxHealth += EquippedArmor.HealthBonus;
			CurrentHealth = MaxHealth;
		}

		// Calculate damage to inflict on a damageable object: low base damage + strength mod + constitution mod + weapon damage + can heal on crit plus intelligence mod
		public override int DoDamage(IDamageable damageable, bool criticalAttack) {
			int weaponDamage = (int) RandomUtils.GetRandom(EquippedWeapon.MinDamage, EquippedWeapon.MaxDamage);
			int constitutionModifier = Stats.Find(el => el.Type == CharacterStatEnum.Constitution).Modifier;
			int strengthModifier = Stats.Find(el => el.Type == CharacterStatEnum.Strength).Modifier;
			int intelligenceModifier = Stats.Find(el => el.Type == CharacterStatEnum.Intelligence).Modifier;

			float totalDamage = (int) Math.Floor(RandomUtils.GetRandom(2, 4 + 1) + weaponDamage + constitutionModifier / 2 + strengthModifier / 2);

			if (criticalAttack) {
				int healAmount = (int) Math.Floor(weaponDamage * EquippedWeapon.CriticalBonus + intelligenceModifier);

				ConsoleUtils.Player("Te has curado {0}. Ahora tienes {1}/{2} de vida.\n", healAmount.ToString(), Heal(healAmount).ToString(), MaxHealth.ToString());
			}

			return damageable.ReceiveDamage((int) Math.Floor(totalDamage));
		}
	}
}
