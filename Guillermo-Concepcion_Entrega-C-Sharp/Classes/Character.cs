using Guillermo_Concepcion_Entrega_C_Sharp.Interfaces;
using Guillermo_Concepcion_Entrega_C_Sharp.Types;
using Guillermo_Concepcion_Entrega_C_Sharp.Utils;
using System;
using System.Collections.Generic;

namespace Guillermo_Concepcion_Entrega_C_Sharp.Classes {
	abstract class Character : IDamageable, IDamageSource, IHealable {
		public string Name { get; protected set; }

		public List<Stat> Stats { get; protected set; } = new List<Stat>();

		public int MaxHealth { get; protected set; }

		public int CurrentHealth { get; protected set; }

		public Character(string name, Dictionary<CharacterStatEnum, int> stats) {
			Name = name;

			InitializeStats(stats);
		}

		public Character(string name, List<Stat> stats) {
			Name = name;

			Stats = stats;
		}

		protected void InitializeStats(Dictionary<CharacterStatEnum, int> stats) {
			foreach (KeyValuePair<CharacterStatEnum, int> pair in stats) {
				Stats.Add(new Stat(pair.Key, pair.Value));
			}
		}

		protected abstract void InitializeHealth();

		// Check if this character is alive (current health > 0)
		public bool IsAlive() {
			return CurrentHealth > 0;
		}

		// Heal added to character class instead of player for scaling purposes, so enemies could heal too
		public int Heal() {
			CurrentHealth = MaxHealth;

			return CurrentHealth;
		}

		// Heals by amount added for scaling purposes, so we could add other healing options for any character
		public int Heal(int amount) {
			if (amount > 0) {
				CurrentHealth = Math.Min(CurrentHealth + amount, MaxHealth);
			}

			return CurrentHealth;
		}

		public int GetCurrentStatPoints() {
			int currentPoints = 0;
			foreach (Stat stat in Stats) {
				currentPoints += stat.Value;
			}

			return currentPoints;
		}

		// Calculates the result damage with a dodge chance based on dexterity stat
		public int ReceiveDamage(int rawDamage) {
			if (rawDamage <= 0) {
				return 0;
			}

			int dexterityModifier = Stats.Find(stat => stat.Type == CharacterStatEnum.Dexterity).Modifier;

			// Chance to dodge based on character dexterity
			// dex.: -5 -> chance: 0% | dex.: 5 -> chance: 50%
			float dodgeChance = dexterityModifier * 5 + 25;
			bool hasDodgedAttack = RandomUtils.HasChance(dodgeChance);

			if (hasDodgedAttack) {
				ConsoleUtils.Narrator("¡{0} consigue esquivar el ataque!\n", Name);
				return 0;
			}

			CurrentHealth = Math.Max(0, CurrentHealth - rawDamage);
			return rawDamage;
		}

		public abstract int DoDamage(IDamageable damageable, bool criticalAttack);

		public abstract AttackTypeEnum GetAttackType();

		// Prints the player health bar with white color by default
		public virtual void PrintHealthBar(ConsoleColor color = ConsoleColor.White) {
			Console.ForegroundColor = color;
			Console.Write(" |");

			Console.BackgroundColor = color;
			for (int i = 1; i < CurrentHealth; i++) {
				Console.Write(' ');
			}

			Console.ResetColor();
			for (int i = 0; i < MaxHealth - CurrentHealth; i++) {
				Console.Write(' ');
			}
			Console.ForegroundColor = color;
			Console.WriteLine('|');

			Console.ResetColor();
		}
	}
}
