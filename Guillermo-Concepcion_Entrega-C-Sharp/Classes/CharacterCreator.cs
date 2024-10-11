using Guillermo_Concepcion_Entrega_C_Sharp.Database;
using Guillermo_Concepcion_Entrega_C_Sharp.Localization;
using Guillermo_Concepcion_Entrega_C_Sharp.Types;
using Guillermo_Concepcion_Entrega_C_Sharp.Utils;
using System;
using System.Collections.Generic;

namespace Guillermo_Concepcion_Entrega_C_Sharp.Classes {
	class CharacterCreator {

		/*
		 * We want to create the player character once the user has introduced all the data correctly, 
		 * so he can go back and forth until its finished. For that, we have a simple temporal character 
		 * struct to save the data until character creation is done and we can call the Player constructor.
		 */
		private struct TempPlayer {
			public static int MAX_STAT_POINTS = 40;
			public int CurrentStatPoints;

			public CharacterClassEnum CharacterClass;
			public string Name;
			public int Money;
			public Armor CharacterArmor;
			public Weapon CharacterWeapon;
			public Dictionary<CharacterStatEnum, int> Stats;
		}
		private TempPlayer tempPlayer;

		// We initialize the basic data for character creation into our temp character struct
		public CharacterCreator(out PlayerCharacter playerRef, out bool continueGame) {
			tempPlayer = new TempPlayer {
				CurrentStatPoints = 0,
				Stats = new Dictionary<CharacterStatEnum, int>(),
				Money = 100
			};

			continueGame = StartCreator(out playerRef);
		}

		// Starts the character creation and controls the exit input from the user as well as the final creation
		private bool StartCreator(out PlayerCharacter playerRef) {
			Console.Clear();
			Console.WriteLine();

			int numberOfSteps = Enum.GetValues(typeof(CharacterCreationStepEnum)).Length;
			int creatorStep = 1;

			do {
				creatorStep = PlayerSteps(creatorStep);
			} while (creatorStep > 0 && creatorStep <= numberOfSteps);

			if (creatorStep <= 0) {
				playerRef = null;
				return false;
			}

			switch (tempPlayer.CharacterClass) {
				case CharacterClassEnum.Warrior:
					playerRef = new WarriorCharacter(tempPlayer.Name, tempPlayer.CharacterArmor, tempPlayer.CharacterWeapon, tempPlayer.Money, tempPlayer.Stats);
					ConsoleUtils.Success("Tu guerrero se ha creado correctamente.\n");
					break;
				case CharacterClassEnum.Mage:
					playerRef = new MageCharacter(tempPlayer.Name, tempPlayer.CharacterArmor, tempPlayer.CharacterWeapon, tempPlayer.Money, tempPlayer.Stats);
					ConsoleUtils.Success("Tu mago se ha creado correctamente.\n");
					break;
				default:
					playerRef = null;
					ConsoleUtils.Error("Ha habido un error en la creación del personaje.\n");
					break;
			}

			ConsoleUtils.SystemOut("Presiona cualquier tecla para continuar...\n");
			Console.ReadKey();
			Console.Clear();

			return playerRef != null;
		}

		private int PlayerSteps(int creatorStep) {
			switch (creatorStep) {
				case 0:
					Console.Clear();
					return 0;

				// STEP 1: CLASS ELECTION
				case 1:
					int numberOfClasses = Enum.GetValues(typeof(CharacterClassEnum)).Length;

					int selectedClass = 1;
					ConsoleUtils.Notification("Elige una clase para tu personaje:");
					foreach (CharacterClassEnum className in Enum.GetValues(typeof(CharacterClassEnum))) {
						ConsoleUtils.System("\t{0}.- {1}", (selectedClass++).ToString(), CharacterClassLocalization.es_ES[className]);
					}
					ConsoleUtils.Print("\t0.- Salir", ConsoleColor.DarkRed);

					// Reads the user input and checks if it's an int input
					if (!int.TryParse(Console.ReadLine(), out selectedClass)) {
						Console.Clear();
						ConsoleUtils.Error("¡El valor introducido no es válido!\n");
						return creatorStep;
					}

					// Checks if user input is below the max range
					if (selectedClass > numberOfClasses || selectedClass < 0) {
						Console.Clear();
						ConsoleUtils.Error("¡El valor introducido no es válido!\n");
						return creatorStep;
					}

					// Checks if user input is above the min range
					if (selectedClass == 0) {
						Console.Clear();
						return creatorStep - 1;
					}

					tempPlayer.CharacterClass = (CharacterClassEnum) selectedClass - 1;

					Console.Clear();
					return creatorStep + 1;

				// STEP 2: CHARACTER NAME
				case 2:
					ConsoleUtils.Notification("Introduce el nombre de tu personaje:");
					ConsoleUtils.SystemOut("(0 para ir atrás)");
					string userInput = Console.ReadLine();

					if (int.TryParse(userInput, out int numberInput)) {
						return numberInput == 0 ? creatorStep - 1 : creatorStep;
					}

					tempPlayer.Name = userInput;

					Console.Clear();
					return creatorStep + 1;

				// STEP 3: STAT INITIALIZATION
				case 3:
					int numberOfStats = Enum.GetValues(typeof(CharacterStatEnum)).Length;
					int statStep = 1;
					do {
						statStep = PlayerStatSteps(statStep);
					} while (statStep > 0 && statStep <= numberOfStats);

					if (statStep == 0) {
						return creatorStep - 1;
					}

					Console.Clear();
					return creatorStep + 1;

				// STEP 4: ARMOR SELECTION
				case 4:
					ResetCharacterItem(ItemTypeEnum.Armor);

					int selectedArmor = SelectItemFromTable(ItemTypeEnum.Armor);

					if (selectedArmor == -1) {
						Console.Clear();
						ConsoleUtils.Error("El valor introducido no es válido, inténtelo de nuevo\n");
						return creatorStep;
					}

					if (selectedArmor == 0) {
						Console.Clear();
						return creatorStep - 1;
					}

					if (!ItemDatabase.ArmorDB.TryGetValue(selectedArmor, out Armor armor)) {
						Console.Clear();
						ConsoleUtils.Error("El id {0} no existe, inténtelo de nuevo.", selectedArmor.ToString());
						return creatorStep;
					}

					if (armor.BuyValue > tempPlayer.Money) {
						Console.Clear();
						ConsoleUtils.Error("No tienes dinero suficiente para comprar la armadura '{0}'.\n", armor.Name);
						return creatorStep;
					}

					if (armor.StatRestriction != null && tempPlayer.Stats[armor.StatRestriction.Item1] < armor.StatRestriction.Item2) {
						Console.Clear();
						ConsoleUtils.Error("No tienes suficiente {0} para usar la armadura '{1}'.\n", armor.StatRestriction.Item1.ToString(), armor.Name);
						return creatorStep;
					}

					tempPlayer.CharacterArmor = armor;
					tempPlayer.Money -= tempPlayer.CharacterArmor.BuyValue;

					Console.Clear();
					return creatorStep + 1;

				// STEP 5: WEAPON SELECTION
				case 5:
					ResetCharacterItem(ItemTypeEnum.Weapon);

					int weaponId = SelectItemFromTable(ItemTypeEnum.Weapon);

					if (weaponId == -1) {
						Console.Clear();
						ConsoleUtils.Error("El valor introducido no es válido, inténtelo de nuevo\n");
						return creatorStep;
					}

					if (weaponId == 0) {
						Console.Clear();
						return creatorStep - 1;
					}

					if (!ItemDatabase.WeaponDB.TryGetValue(weaponId, out Weapon weapon)) {
						Console.Clear();
						ConsoleUtils.Error("El id {0} no existe, inténtelo de nuevo.\n", weaponId.ToString());
						return creatorStep;
					}

					if (weapon.BuyValue > tempPlayer.Money) {
						Console.Clear();
						ConsoleUtils.Error("No tienes dinero suficiente para comprar el arma '{0}'.\n", weapon.Name);
						return creatorStep;
					}

					tempPlayer.CharacterWeapon = weapon;
					tempPlayer.Money -= tempPlayer.CharacterWeapon.BuyValue;

					Console.Clear();
					return creatorStep + 1;

				default:
					return creatorStep;
			}
		}

		// Restarts the temp player item and money
		private void ResetCharacterItem(ItemTypeEnum itemType) {
			switch (itemType) {
				case ItemTypeEnum.Armor:
					if (tempPlayer.CharacterArmor != null) {
						tempPlayer.Money += tempPlayer.CharacterArmor.BuyValue;
						tempPlayer.CharacterArmor = null;
					}
					break;
				case ItemTypeEnum.Weapon:
					if (tempPlayer.CharacterWeapon != null) {
						tempPlayer.Money += tempPlayer.CharacterWeapon.BuyValue;
						tempPlayer.CharacterWeapon = null;
					}
					break;
			}
		}

		// Asks the user for every character stat, giving him the option to go back at any moment
		private int PlayerStatSteps(int statStep) {
			switch (statStep) {
				case 0:
					return 0;

				// STRENGTH 
				case 1:
					return AskForStat(CharacterStatEnum.Strength, statStep);


				// CONSTITUTION 
				case 2:
					return AskForStat(CharacterStatEnum.Constitution, statStep);


				// DEXTERITY 
				case 3:
					return AskForStat(CharacterStatEnum.Dexterity, statStep);


				// INTELLIGENCE
				case 4:
					return AskForStat(CharacterStatEnum.Intelligence, statStep, true);

				default:
					return statStep;
			}
		}

		// Interacts with the user for him to introduce a given stat value
		private int AskForStat(CharacterStatEnum statName, int statStep, bool lastStep = false) {
			// In case we go back from any step to this stat step, we reset the stat and it's spent stat points
			if (tempPlayer.Stats.ContainsKey(statName)) {
				tempPlayer.CurrentStatPoints -= tempPlayer.Stats[statName];
				tempPlayer.Stats.Remove(statName);
			}

			int statValue;
			do {
				ConsoleUtils.Notification("\nIntroduzca el valor de la \"{0}\" del personaje (1 - 20):", CharacterStatLocalization.es_ES[statName]);
				ConsoleUtils.SystemOut("(0 para ir atrás)");

				int pointsLeft = TempPlayer.MAX_STAT_POINTS - tempPlayer.CurrentStatPoints;
				if (tempPlayer.CurrentStatPoints < TempPlayer.MAX_STAT_POINTS) {
					ConsoleUtils.Info("[{0}/{1}]", pointsLeft.ToString(), TempPlayer.MAX_STAT_POINTS.ToString());
				}
				else {
					ConsoleUtils.BlockedInfo("[{0}/{1}]", pointsLeft.ToString(), TempPlayer.MAX_STAT_POINTS.ToString());
				}

				if (!int.TryParse(Console.ReadLine(), out statValue)) {
					Console.Clear();
					ConsoleUtils.Error("¡El valor introducido no es válido!\n");
					statValue = -1;
				}
			} while (IsInvalidStatInput(statValue, lastStep));

			if (statValue == 0) {
				Console.Clear();
				return statStep - 1;
			}

			if (tempPlayer.Stats.ContainsKey(statName)) {
				tempPlayer.CurrentStatPoints -= tempPlayer.Stats[statName];
				tempPlayer.Stats[statName] = statValue;
			}
			else {
				tempPlayer.Stats.Add(statName, statValue);
			}

			tempPlayer.CurrentStatPoints += statValue;

			Console.Clear();
			return statStep + 1;
		}

		// Checks if the user stat input is valid or not
		private bool IsInvalidStatInput(int statValue, bool lastStep) {
			if (statValue > 20) {
				Console.Clear();
				ConsoleUtils.Error("El valor no puede ser superior a 20");
				return true;
			}

			if (statValue < 0) {
				Console.Clear();
				ConsoleUtils.Error("El valor no puede ser inferior a 0");
				return true;
			}

			if (statValue == 0) {
				return false;
			}

			if (statValue > TempPlayer.MAX_STAT_POINTS - tempPlayer.CurrentStatPoints) {
				Console.Clear();
				ConsoleUtils.Error("El valor no puede ser superior a los puntos restantes");
				return true;
			}

			if (lastStep && statValue < TempPlayer.MAX_STAT_POINTS - tempPlayer.CurrentStatPoints) {
				Console.Clear();
				ConsoleUtils.Error("Debes gastar todos los puntos de estadística disponibles");
				return true;
			}

			return false;
		}

		// Prints an item table depending on item type, and manage user selection input
		private int SelectItemFromTable(ItemTypeEnum itemType) {
			ConsoleUtils.Notification("Elija un objeto de tipo {0} para su personaje:", ItemTypeLocalization.es_ES[itemType]);
			ConsoleUtils.SystemOut("(0 para ir atrás)\n");

			Console.Write("Dinero restante: ");
			if (tempPlayer.Money > 0) {
				ConsoleUtils.Info("{0}", tempPlayer.Money.ToString());
			}
			else {
				ConsoleUtils.BlockedInfo("{0}", tempPlayer.Money.ToString());
			}

			switch (itemType) {
				case ItemTypeEnum.Armor:
					Console.WriteLine("---------------------------------------------------------------------------------------------------------");
					foreach (KeyValuePair<int, Armor> armorOption in ItemDatabase.ArmorDB) {
						if (tempPlayer.Money < armorOption.Value.BuyValue) {
							Console.ForegroundColor = ConsoleColor.DarkRed;
						}

						Console.WriteLine("|{0}|", armorOption.Value.ToString());
						Console.ResetColor();
					}
					Console.WriteLine("---------------------------------------------------------------------------------------------------------");


					bool isValidInput = int.TryParse(Console.ReadLine(), out int armorId);

					if (!isValidInput) {
						return -1;
					}

					return armorId;

				case ItemTypeEnum.Weapon:
					Console.WriteLine("--------------------------------------------------------------------------------------------------------");
					foreach (KeyValuePair<int, Weapon> weaponOption in ItemDatabase.WeaponDB) {
						if (tempPlayer.Money < weaponOption.Value.BuyValue) {
							Console.ForegroundColor = ConsoleColor.DarkRed;
						}

						Console.WriteLine("|{0}|", weaponOption.Value.ToString());
						Console.ResetColor();
					}
					Console.WriteLine("--------------------------------------------------------------------------------------------------------");


					isValidInput = int.TryParse(Console.ReadLine(), out int weaponId);

					if (!isValidInput) {
						return -1;
					}

					return weaponId;

				default:
					return -1;
			}
		}
	}
}
