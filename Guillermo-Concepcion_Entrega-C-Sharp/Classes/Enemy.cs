using Guillermo_Concepcion_Entrega_C_Sharp.Interfaces;
using Guillermo_Concepcion_Entrega_C_Sharp.Types;
using Guillermo_Concepcion_Entrega_C_Sharp.Utils;
using System;
using System.Collections.Generic;

namespace Guillermo_Concepcion_Entrega_C_Sharp.Classes {
	class Enemy : Character {

		public ushort Id { get; protected set; }

		public int Level { get; protected set; }

		public AttackTypeEnum[] AvailableAttacks { get; protected set; }

		public Enemy(ushort id, string name, int level, Dictionary<CharacterStatEnum, int> stats) : base(name, stats) {
			Id = id;
			Level = level;

			InitializeHealth();
			InitializeAttackTypes();
		}

		public Enemy(Enemy enemyCopy, int level) : base(enemyCopy.Name, enemyCopy.Stats) {
			Id = enemyCopy.Id;
			Level = level;

			InitializeHealth();
			InitializeAttackTypes();
		}

		protected override void InitializeHealth() {
			int constitutionModifier = Stats.Find(el => el.Type == CharacterStatEnum.Constitution).Modifier;
			MaxHealth = (6 + (int) RandomUtils.GetRandom(2, 4 + 1) * Level) + (constitutionModifier * Level);
			CurrentHealth = MaxHealth;
		}

		protected void InitializeAttackTypes() {
			AvailableAttacks = new AttackTypeEnum[Level];
			int attackTypesCounter = Enum.GetValues(typeof(AttackTypeEnum)).Length;

			List<AttackTypeEnum> attackList = new List<AttackTypeEnum>();
			foreach (AttackTypeEnum attackType in Enum.GetValues(typeof(AttackTypeEnum))) {
				attackList.Add(attackType);
			}

			int index = 0;
			while (index < AvailableAttacks.Length && attackList.Count > 0) {
				int randomIndex = (int) RandomUtils.GetRandom(attackList.Count);

				AvailableAttacks[index] = attackList[randomIndex];

				attackList.Remove(attackList[randomIndex]);

				index++;
			}
		}

		protected bool CheckForAttackType(AttackTypeEnum attack) {
			for (int i = 0; i < AvailableAttacks.Length; i++) {
				if (AvailableAttacks[i] == attack) {
					return true;
				}
			}

			return false;
		}

		// Calculate total damage to inflict on a damageable object, based on strength, level and critical boolean
		public override int DoDamage(IDamageable damageable, bool criticalAttack = false) {
			int enemyDamage = (int) RandomUtils.GetRandom(3, 6 + 1);
			int strengthModifier = Stats.Find(el => el.Type == CharacterStatEnum.Strength).Modifier;
			float totalDamage = (enemyDamage * Level) + (strengthModifier * Level);

			if (criticalAttack) {
				totalDamage *= 1.2f;
			}

			return damageable.ReceiveDamage((int) Math.Floor(totalDamage));
		}

		// Get a random attack from enemy available attacks
		public override AttackTypeEnum GetAttackType() {
			int randomAttackIndex = (int) RandomUtils.GetRandom(AvailableAttacks.Length);

			return AvailableAttacks[randomAttackIndex];
		}

		// Prints the player health bar with red color by default
		public override void PrintHealthBar(ConsoleColor color = ConsoleColor.DarkRed) {
			base.PrintHealthBar(color);
		}
	}
}
