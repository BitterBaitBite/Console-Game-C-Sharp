namespace Guillermo_Concepcion_Entrega_C_Sharp.Classes {
	abstract class Room {

		public string Description { get; protected set; }

		// Starts room actions and returns a boolean indicating if the room actions are finished
		public abstract bool StartRoom(PlayerCharacter player);

		protected abstract bool RoomAction(PlayerCharacter player);
	}
}
