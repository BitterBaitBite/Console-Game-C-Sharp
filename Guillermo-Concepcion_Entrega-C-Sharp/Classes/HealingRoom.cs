using Guillermo_Concepcion_Entrega_C_Sharp.Utils;
using System;

namespace Guillermo_Concepcion_Entrega_C_Sharp.Classes {
	class HealingRoom : Room {

		public HealingRoom() {
			Description = "Entras en una habitación cubierta de maleza, y en el centro ves una elegante fuente con motivos que representan plantas y flores de lo más exóticas.\n" +
			"Te acercas y, en la superficie del agua, ves tu reflejo. Te miras directamente a los ojos, y en un destello parece que te intercambies con el reflejo y que el \n" +
			"mundo se diese la vuelta. Sigues en el mismo lugar, pero te sientes renovado. \n" +
			"¿Habrá sido imaginación tuya?\n";
		}

		// Starts room actions and returns a boolean indicating if the room actions are finished
		public override bool StartRoom(PlayerCharacter player) {
			ConsoleUtils.Narrator(Description);

			return RoomAction(player);
		}

		// Heals and notifies the player, then returns if it's character is alive (always true)
		protected override bool RoomAction(PlayerCharacter player) {
			player.Heal();

			ConsoleUtils.Combat("Resultado:\n");

			ConsoleUtils.Narrator("!Te has recuperado de todas tus heridas!\n");

			Console.Write("{0}: ", player.Name);
			ConsoleUtils.Player("{0}/{1} ", player.CurrentHealth.ToString(), player.MaxHealth.ToString());
			player.PrintHealthBar();
			Console.WriteLine();

			ConsoleUtils.SystemOut("Pulse cualquier tecla para continuar...\n");
			Console.ReadKey();
			Console.Clear();

			return player.IsAlive();
		}
	}
}
