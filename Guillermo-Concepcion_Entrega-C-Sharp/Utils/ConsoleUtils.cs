using System;

namespace Guillermo_Concepcion_Entrega_C_Sharp.Utils {
	static class ConsoleUtils {
		public static void Print(string message, params string[] args) {
			Console.WriteLine(message, args);
		}

		public static void Print(string message, ConsoleColor color, params string[] args) {
			Console.ForegroundColor = color;
			Console.WriteLine(message, args);
			Console.ResetColor();
		}

		// TODO hacer que los colores de los prints estén asociados a tipos de mensajes:
		// Ej.: Sistema -> Gris || Combate -> Morado || Acciones jugador -> Azul || etc.
		public static void Narrator(string message, params string[] args) {
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine(message, args);
			Console.ResetColor();
		}

		public static void System(string message, params string[] args) {
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine(message, args);
			Console.ResetColor();
		}

		public static void SystemOut(string message, params string[] args) {
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine(message, args);
			Console.ResetColor();
		}

		public static void Info(string message, params string[] args) {
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine(message, args);
			Console.ResetColor();
		}
		public static void BlockedInfo(string message, params string[] args) {
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine(message, args);
			Console.ResetColor();
		}

		public static void Error(string message, params string[] args) {
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(message, args);
			Console.ResetColor();
		}

		public static void Success(string message, params string[] args) {
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(message, args);
			Console.ResetColor();
		}

		public static void Combat(string message, params string[] args) {
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine(message, args);
			Console.ResetColor();
		}

		public static void Notification(string message, params string[] args) {
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(message, args);
			Console.ResetColor();
		}

		public static void Player(string message, params string[] args) {
			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine(message, args);
			Console.ResetColor();
		}

		public static void Enemy(string message, params string[] args) {
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine(message, args);
			Console.ResetColor();
		}


		/*
		 * TODO testear el delay con el for (index out of bound exceptions), el error probablemente se debe al Format
		 * 
		 * public static void Print(string message, ushort delay, params string[] args) {
			string formattedMessage = string.Format(message, args);
			for (int i = 0; i < message.Length; i++) {
				Console.Write(formattedMessage[i]);
				Task.Delay(delay).Wait();
			}
			Console.Write('\n');
		}
		*/
	}
}
