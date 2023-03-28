using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FinalProject___LeeShapira
{
	public class CombatSystem
	{
		public static GameManager gameManager;
		public static void Combat_System(Player player, Monster monster)
		{
			if (!monster.IsDead())
			{
				string extra = "You encountered a " + monster.Name;
				DataView(extra, 2000);
				while (!player.IsDead() && !monster.IsDead())
				{
					monster.TakeDamage(player.DamageInflicted());
					player.TakeDamage(monster.DamageInflicted());

					extra = "The " + monster.Name + " hit you with " + monster.Damage + " damage!\n";
					extra += "You hit the " + monster.Name + "! it's current hp is " + monster.CurrentHP;
					DataView(extra, 3500);
				}

				if (player.IsDead())
				{
					Console.Clear();
					Console.WriteLine("You Died!");
					Thread.Sleep(2000);
					gameManager.mapManager.LoadFinish();
				}
				else
				{
					extra = "You beat the " + monster.Name;
					DataView(extra, 2000);
					monster.Death();
					gameManager.mapManager.CurrMap.KillMonster(monster);
				}
			}
		}

		public static void DataView(string extra,int time)
		{
			Data.DataView(gameManager);
			Data.DataViewExtra(gameManager, extra,time);
		}

	}
}
