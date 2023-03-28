using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject___LeeShapira
{
	class Trap
	{
		public static char TrapSymbol { get; } = '§';
		public static int TrapDamage { get; private set; } = 10;
		public static bool IsActivated { get; set; } = false;
		public static ConsoleColor TrapColor { get; set; } = ConsoleColor.Black;


		public static bool ActivateTrap()
		{
			if (!IsActivated)
			{
				IsActivated = true;
                TrapColor = ConsoleColor.Red;
				return true;
            }
			return false;
		}

		public static void DeactivateTrap()
		{
			IsActivated = false;
			TrapColor = ConsoleColor.Black;
		}

		public static void Action(int x, int y)
		{
			int cursorX = Console.CursorLeft;
			int cursorY = Console.CursorTop;
			Console.SetCursorPosition(x, y);
			Console.ForegroundColor = TrapColor;
			Console.Write('§');
			Console.ForegroundColor = ConsoleColor.White;
			Console.SetCursorPosition(cursorX, cursorY);
		}







	}
}
