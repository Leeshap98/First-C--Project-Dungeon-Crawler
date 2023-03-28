using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject___LeeShapira
{
	public class Data
	{
		private GameManager gameManager;
		//מראה את השלב הנוכחי
		public static int LvlTransitions { get; set; }//"you advanced to the * dungeon lvl"

		//מראה את החיים שנשאר לו כרגע
		public static int CurrentHPData { get; set; }//"you advanced to the * dungeon lvl"

		//עוצמת הנזק הנוכחית של השחקן
		public static int DamageData { get; set; }//"you advanced to the * dungeon lvl"

		//הנזק שירד מקרב עם המפלצת,כמה הרווחתי מתיבה,כמה הפסדתי ממלכודת
		public static int PlayerOutput { get; set; }

		public static int MonsterOutput { get; set; }

		// "You hit the 'monsterName' with x damage, you now have x HP"
		// "The 'monsterName' hits you with x damage, it now has x HP/ the 'monsterName' is dead"
		//You found a healling/maxHealf/maxDam treasure box! you now have x HP, x maxHP, x maxDamage!
		//Oh no! You stumbled on a hidden trap! you lost 10 HP. You now have x HP.
		//הוספת שדה של מקרא

		public Data(GameManager gameManager)
		{
			this.gameManager = gameManager;
		}


		public static void DataView(GameManager gameManager)
		{
			int currentMapID = gameManager.mapManager.CurrentMapID;
			int currentHPData = gameManager.player.CurrentHP;
			int maxHPData = gameManager.player.MaxHP;
			int currentDamageData = gameManager.player.Damage;
			int potions = gameManager.player.Potions;
			int cursorX = Console.CursorLeft;
			int cursorY = Console.CursorTop;
			Console.SetCursorPosition(0, 13);
			Console.WriteLine("you advanced to the {0} dungeon lvl", currentMapID);
			Console.WriteLine("HP: {0}\\{1}\tDamage: {2} Potions: {3}", currentHPData, maxHPData, currentDamageData, potions);
			//"You hit the 'monsterName' with x damage, you now have x HP"
			//"The 'monsterName' hits you with x damage, it now has x HP/ the 'monsterName' is dead"
			//You found a healling/maxHealf/maxDam treasure box! you now have x HP, x maxHP, x maxDamage!
			//Oh no! You stumbled on a hidden trap! you lost 10 HP. You now have x HP.
			Console.SetCursorPosition(cursorX, cursorY);
		}
		public static void DataViewExtra(GameManager gameManager, string extra,int time)
		{
			Console.SetCursorPosition(0, 15);
			Console.WriteLine("                                                              ");
			Console.WriteLine("                                                              ");
			Console.SetCursorPosition(0, 15);
			Console.WriteLine(extra);
			Thread.Sleep(time);
			Console.WriteLine("                                                              ");
			Console.WriteLine("                                                              ");
		}
	}
}
