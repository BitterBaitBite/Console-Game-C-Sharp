using Guillermo_Concepcion_Entrega_C_Sharp.Interfaces;
using Guillermo_Concepcion_Entrega_C_Sharp.Types;
using Guillermo_Concepcion_Entrega_C_Sharp.Utils;
using System;
using System.Collections.Generic;

namespace Guillermo_Concepcion_Entrega_C_Sharp.Classes {
	class WarriorCharacter : PlayerCharacter {

		public WarriorCharacter(string name, Armor armor, Weapon weapon, int money, Dictionary<CharacterStatEnum, int> stats) : base(name, armor, weapon, money, stats) {
			InitializeHealth();
		}

		// Health based on higher base + constitution modifier + armor bonus
		protected override void InitializeHealth() {
			int constitutionModifier = Stats.Find(el => el.Type == CharacterStatEnum.Constitution).Modifier;
			MaxHealth = (int) RandomUtils.GetRandom(25, 35 + 1) + constitutionModifier;
			MaxHealth += EquippedArmor.HealthBonus;
			CurrentHealth = MaxHealth;
		}

		// Calculate damage to inflict on a damageable object based on lower base damage + strength modifier + weapon damage
		public override int DoDamage(IDamageable damageable, bool criticalAttack) {
			int weaponDamage = (int) RandomUtils.GetRandom(EquippedWeapon.MinDamage, EquippedWeapon.MaxDamage);
			int strengthModifier = Stats.Find(el => el.Type == CharacterStatEnum.Strength).Modifier;
			float totalDamage = (int) RandomUtils.GetRandom(3, 6 + 1) + weaponDamage + strengthModifier;

			if (criticalAttack) {
				totalDamage *= EquippedWeapon.CriticalBonus;
			}

			return damageable.ReceiveDamage((int) Math.Floor(totalDamage));
		}
	}
}
