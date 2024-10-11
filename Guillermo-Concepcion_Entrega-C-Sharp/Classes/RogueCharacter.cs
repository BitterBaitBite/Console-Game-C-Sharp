using Guillermo_Concepcion_Entrega_C_Sharp.Interfaces;
using Guillermo_Concepcion_Entrega_C_Sharp.Types;
using Guillermo_Concepcion_Entrega_C_Sharp.Utils;
using System;
using System.Collections.Generic;

namespace Guillermo_Concepcion_Entrega_C_Sharp.Classes {

	class RogueCharacter : PlayerCharacter {
		public RogueCharacter(string name, Armor armor, Weapon weapon, int money, Dictionary<CharacterStatEnum, int> stats) : base(name, armor, weapon, money, stats) {
			InitializeHealth();
		}

		// Health based on lower base + constitution modifier + armor health
		protected override void InitializeHealth() {
			int constitutionModifier = Stats.Find(el => el.Type == CharacterStatEnum.Constitution).Modifier;
			int strengthModifier = Stats.Find(el => el.Type == CharacterStatEnum.Strength).Modifier;

			MaxHealth = (int) RandomUtils.GetRandom(18, 25 + 1) + constitutionModifier / 2 + strengthModifier / 2;
			MaxHealth += EquippedArmor.HealthBonus;
			CurrentHealth = MaxHealth;
		}

		// Calculate damage to inflict on a damageable object based on lowest base damage + dexterity modifier + weapon damage + highest crit
		public override int DoDamage(IDamageable damageable, bool criticalAttack) {
			int weaponDamage = (int) RandomUtils.GetRandom(EquippedWeapon.MinDamage, EquippedWeapon.MaxDamage);

			int dexterityModifier = Stats.Find(el => el.Type == CharacterStatEnum.Dexterity).Modifier;
			int constitutionModifier = Stats.Find(el => el.Type == CharacterStatEnum.Constitution).Modifier;

			float totalDamage = (int) RandomUtils.GetRandom(1, 4 + 1) + weaponDamage + dexterityModifier;

			if (criticalAttack) {
				totalDamage = totalDamage * EquippedWeapon.CriticalBonus + (dexterityModifier - Math.Max(0, constitutionModifier));
			}

			return damageable.ReceiveDamage((int) Math.Floor(totalDamage));
		}
	}
}
