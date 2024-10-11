using Guillermo_Concepcion_Entrega_C_Sharp.Interfaces;
using Guillermo_Concepcion_Entrega_C_Sharp.Types;
using Guillermo_Concepcion_Entrega_C_Sharp.Utils;
using System;
using System.Collections.Generic;

namespace Guillermo_Concepcion_Entrega_C_Sharp.Classes {

	class MageCharacter : PlayerCharacter {
		public MageCharacter(string name, Armor armor, Weapon weapon, int money, Dictionary<CharacterStatEnum, int> stats) : base(name, armor, weapon, money, stats) {
			InitializeHealth();
		}

		// Health based on lower base + constitution modifier + armor health
		protected override void InitializeHealth() {
			int constitutionModifier = Stats.Find(el => el.Type == CharacterStatEnum.Constitution).Modifier;
			MaxHealth = (int) RandomUtils.GetRandom(15, 20 + 1) + constitutionModifier;
			MaxHealth += EquippedArmor.HealthBonus;
			CurrentHealth = MaxHealth;
		}

		// Calculate damage to inflict on a damageable object based on higher base damage + intelligence modifier + weapon damage
		public override int DoDamage(IDamageable damageable, bool criticalAttack) {
			int weaponDamage = (int) RandomUtils.GetRandom(EquippedWeapon.MinDamage, EquippedWeapon.MaxDamage);
			int intelligenceModifier = Stats.Find(el => el.Type == CharacterStatEnum.Intelligence).Modifier;
			float totalDamage = (int) RandomUtils.GetRandom(5, 10 + 1) + weaponDamage + intelligenceModifier;


			if (criticalAttack) {
				totalDamage *= EquippedWeapon.CriticalBonus;
			}

			return damageable.ReceiveDamage((int) Math.Floor(totalDamage));
		}
	}
}
