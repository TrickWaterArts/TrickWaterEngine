using System;

namespace TrickWaterEngine {
	public static class Debug {

		public static void Print(object msg) {
			string s = (msg == null) ? "null" : msg.ToString();
			Console.WriteLine(s);
		}

	}
}