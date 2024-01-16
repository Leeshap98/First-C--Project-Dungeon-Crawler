using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FinalProject___LeeShapira
{
	enum TreasureElements
	{
		IncreaseDamage = 15,
		IncreaseMaxHP = 20,
		Heal
	}
	class TreasureChest
	{
		public static void Action(char element,Player player)
		{
			switch (element)
			{
				case '¶':
					IncreaseDamageChest(player);
					break;

				case '▀':
					IncreaseMaxHPChest(player);
					break;

				case '#':
					HealChest(player);
					break;
				default:
					Console.WriteLine("there is no chest buff like this");
					break;
			}
		}

		static void IncreaseDamageChest(Player player)
		{
			int d = (int)TreasureElements.IncreaseDamage;
			player.Damage += d;
			string message = "You opened an extra damage chest! Your damage is increased by "+d;
			Data.DataViewExtra(player.GameManager,message, 2000);
		}

		static void IncreaseMaxHPChest(Player player)
		{
			int hp = (int)TreasureElements.IncreaseMaxHP;
			player.MaxHP += hp;
			string message = "You opened an extra HP chest! Your max hp is increased by " + hp;
			Data.DataViewExtra(player.GameManager, message, 2000);
		}

		static void HealChest(Player player)
		{
			player.Heal();
			string message = "You opened a healing chest! You are completely healed!";
			Data.DataViewExtra(player.GameManager, message, 2000);
		}
	}
}
