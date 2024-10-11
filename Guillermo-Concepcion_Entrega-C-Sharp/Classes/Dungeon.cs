using Guillermo_Concepcion_Entrega_C_Sharp.Utils;

namespace Guillermo_Concepcion_Entrega_C_Sharp.Classes {
	class Dungeon {

		public Room[] DungeonMap { get; protected set; }

		public int CurrentRoom { get; protected set; }

		public Dungeon() {
			DungeonMap = new Room[10];
			int roomLevel = 1;
			CurrentRoom = 1;

			for (int i = CurrentRoom; i <= DungeonMap.Length; i++) {
				if (i % 4 == 0) {
					DungeonMap[i - 1] = new HealingRoom();
					continue;
				}
				else if ((i - 1) != 0 && (i - 1) % 4 == 0 && roomLevel < 3) {
					roomLevel++;
				}

				DungeonMap[i - 1] = new CombatRoom(roomLevel);
			}
		}

		public bool StartDungeon(PlayerCharacter player) {
			ConsoleUtils.Narrator("Encuentras una oscura estructura, oculta entre la espesa vegetación de la salvaje selva que te rodea. La piedra de sus muros parece impenetrables, pero encuentras una gran y majestuosa entrada, ahora olvidada tras décadas de abandono. Entras en aquella mazmorra con los ojos bien abiertos, alerta a cualquier sonido, pues algo te dice que encontrarás numerosos enemigos, cuando presionas una losa a tus pies, accionando una verja de hierro forjado que cae a tus espaldas cubriendo la entrada. Solo puedes avanzar. Ya no hay vuelta atrás... \n");

			while (CurrentRoom <= DungeonMap.Length) {
				ConsoleUtils.Info("Habitación {0}\n", CurrentRoom.ToString());

				if (DungeonMap[CurrentRoom - 1].StartRoom(player)) {
					ConsoleUtils.Narrator("Has conseguido superar la sala {0}.\n", CurrentRoom.ToString());
				}
				else {
					ConsoleUtils.Narrator("Has caido combatiendo en la mazmorra, y tu cadáver yace frío en la sala {0}.\n", CurrentRoom.ToString());
					break;
				}

				if (++CurrentRoom <= DungeonMap.Length) {
					ConsoleUtils.Narrator("Avanzas triunfante a la siguiente habitación, al siguiente desafío\n");
				}
				else {
					ConsoleUtils.Success("¡Has conseguido derrotar a todos tus enemigos y completar la mazmorra!\n");
				}
			}

			return player.IsAlive();
		}
	}
}
