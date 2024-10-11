using Guillermo_Concepcion_Entrega_C_Sharp.Classes;
using Guillermo_Concepcion_Entrega_C_Sharp.Utils;
using System;

namespace Guillermo_Concepcion_Entrega_C_Sharp {
	class TheGame {
		static void Main() {

			/*
			 * CHARACTER CREATION
			 */
			new CharacterCreator(out PlayerCharacter player, out bool continueGame);

			if (!continueGame || player == null) {
				return;
			}

			/*
			 * DUNGEON START AND END CONTROL
			 */
			Dungeon dungeon = new Dungeon(12);

			if (!dungeon.StartDungeon(player)) {
				ConsoleUtils.Enemy("\t GAME OVER \t\n");
			}
			else {
				ConsoleUtils.Success("\t ¡ENHORABUENA! \t\n");
			}

			ConsoleUtils.SystemOut("Pulsa cualquier tecla para finalizar la partida");
			Console.ReadKey();
		}
	}
}
