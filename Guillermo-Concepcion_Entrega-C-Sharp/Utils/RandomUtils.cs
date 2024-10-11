using System;

namespace Guillermo_Concepcion_Entrega_C_Sharp.Utils {
	static class RandomUtils {
		public static readonly Random random = new Random();

		public static double GetRandom() { return random.NextDouble(); }

		public static double GetRandom(double max) { return Math.Floor(random.NextDouble() * max); }

		public static double GetRandom(double min, double max) { return Math.Floor((random.NextDouble() * (max - min)) + min); }

		public static bool GetTrueOrFalse() {
			return (random.NextDouble() - 0.5) > 0;
		}

		public static bool GetTrueOrFalse(double chancePercent) {
			return (random.NextDouble() * 100 - chancePercent) > 0;
		}
	}
}
