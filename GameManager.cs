using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FinalProject___LeeShapira
{
	public class GameManager
	{
		public Player player;
		public MapManager mapManager;
		public CombatSystem combatSystem;
		public Data data;
		public string level;

		public GameManager()
		{
			level = "0";
			Menu();
			CombatSystem.gameManager = this;
			if(level== "0" )mapManager = new MapManager();
			else mapManager = new MapManager(level);
			Data.DataView(this);
			int x = mapManager.CurrMap.StartX;
			int y = mapManager.CurrMap.StartY;
			Console.SetCursorPosition(x, y);
			player.SetPlayerPosition(x, y);
			player.PlayerMovement();
		}

		public void Menu()
		{
			string name, avatarColor, option;
			int maxHP, currHP, damage,potion;
			char symbol;
			string gameData = System.IO.File.ReadAllText("GameData.txt").Trim();
			if (gameData.Length > 0)
			{
				string[] parts = gameData.Split(",");
				name = parts[0];
				symbol = char.Parse(parts[1]);
				avatarColor = parts[2];
				level = parts[3];
				maxHP = int.Parse(parts[4]);
				currHP = int.Parse(parts[5]);
				damage = int.Parse(parts[6]);
				potion = int.Parse(parts[7]);
				Console.WriteLine("Greetings {0}!\nWelcome back Hero", name);
				Console.WriteLine("Do you want to continue your last journey?");
				Console.WriteLine("To continue press 1\nFor starting a new journey press 2\nFor exit Press any key");
				option = Console.ReadLine();
				if (option == "1")
				{
					player = new Player(name, symbol, ColorGetColor(avatarColor), maxHP, currHP, damage, potion, this);
					//Thread.Sleep(2000);
					Console.Clear();
				}
				else if (option == "2")
				{
					MenuNewGame();
				}
				else
				{
					Console.Clear();
					System.Environment.Exit(0);
				}
			}
			else MenuNewGame();
		}

		public void MenuNewGameIntro(string name, char symbol, char color)
		{
			const string UNDERLINE = "\x1B[4m";
			const string RESET = "\x1B[0m";
			Console.Clear();
			Console.WriteLine("Greetings Hero!");
			Console.WriteLine(UNDERLINE+"Main Menu"+ RESET+" ");
			Console.WriteLine("To enter your name, press 1");
			Console.WriteLine("To pick your avatar, press 2");
			Console.WriteLine("To pick your avatar's color, press 3");
			Console.WriteLine("To see the credits, press 4");
			Console.WriteLine("To exit the game, Press 5");
			Console.WriteLine("\n\n\n\n\n");
			if(name.Trim() != "" || symbol != ' ' || color != ' ')
				Console.WriteLine("So far chosen:");
			if(name.Trim() != "")
				Console.WriteLine("name: "+name);
			if (symbol != ' ')
				Console.WriteLine("avatar: " + symbol);
			if (color != ' ')
				Console.WriteLine("color: " + ColorGetColorName(color));
		}

		public void MenuNewGame()
		{
			System.IO.File.WriteAllText("GameData.txt", "");
			string name = "";
			char symbol = ' ',option,color = ' ',choice;
			level = "0";
			do
			{
				Console.Clear();
				MenuNewGameIntro(name, symbol, color);
				Console.SetCursorPosition(0, 7);
				choice = char.Parse(Console.ReadLine());

				if (choice == '1')
				{
					Console.WriteLine("Please enter your name (min of 3 letters):");
					name = Console.ReadLine();
					while(name.Length < 3)
					{
						Console.WriteLine("Please enter your name (min of 3 letters):");
						name = Console.ReadLine();
					}
					Thread.Sleep(500);
					Console.Clear();
					Console.WriteLine("{0} what an elegant name, ", name);
				}

				else if (choice == '2')
				{
					do
					{
						Console.Clear();
						Console.WriteLine("Please choose your avatar\n");
						Console.WriteLine("Press 1 for ☺️");
						Console.WriteLine("Press 2 for ☻");
						Console.WriteLine("Press 3 for ♥️");
						option = char.Parse(Console.ReadLine());
					}
					while (option < '1' || option > '3');
					symbol = (char)(option - '0');
					Thread.Sleep(1000);
					Console.Clear();
					Console.WriteLine("you've choosen {0}", symbol);//הורדת נקודת היחס מתו אסקי למספר
				}

				else if (choice == '3')
				{
					do
					{
						Console.Clear();
						Console.WriteLine("Please choose your avatar color\n");
						Console.WriteLine("Press 1 for green");
						Console.WriteLine("Press 2 for blue");
						Console.WriteLine("Press 3 for red");
						Console.WriteLine("Press 4 for magenta");
						Console.WriteLine("Press 5 for yellow");
						color = char.Parse(Console.ReadLine());
					}
					while (color < '1' || color > '5');
				}

				else if (choice == '4')
				{
					Credits();
				}

				else if (choice == '5')
				{
					Environment.Exit(1);
				}

			}
			while (name.Trim() == "" || symbol == ' ' || color == ' ');
			Console.Clear();
			player = new Player(name, symbol, ColorGetColor(color), this);          
        }

		public void IncrementLvl()
		{
			Console.Clear();
			mapManager.IncrementLvl();
			int x = mapManager.CurrMap.StartX;
			int y = mapManager.CurrMap.StartY;
			player.SetPlayerPosition(x, y);
			Data.DataView(this);
		}

		public ConsoleColor ColorGetColor(string color)
		{
			switch (color)
			{
				case "green":
					return ConsoleColor.Green;
				case "blue":
					return ConsoleColor.Blue;
				case "red":
					return ConsoleColor.Red;
				case "magenta":
					return ConsoleColor.Magenta;
				case "yellow":
					return ConsoleColor.Yellow;
				default: return ConsoleColor.White;
			}
		}
		public string ColorGetColor(ConsoleColor color)
		{
			switch (color)
			{
				case ConsoleColor.Green:
					return "green";
				case ConsoleColor.Blue:
					return "blue";
				case ConsoleColor.Red:
					return "red";
				case ConsoleColor.Magenta:
					return "magenta";
				case ConsoleColor.Yellow:
					return "yellow";
				default: return "white";
			}
		}

		public ConsoleColor ColorGetColor(int color)
		{
			switch (color)
			{
				case '1':
					return ConsoleColor.Green;
				case '2':
					return ConsoleColor.Blue;
				case '3':
					return ConsoleColor.Red;
				case '4':
					return ConsoleColor.Magenta;
				case '5':
					return ConsoleColor.Yellow;
				default: return ConsoleColor.White;
			}
		}
		
		public string ColorGetColorName(char color)
		{
			switch (color)
			{
				case '1':
					return "Green";
				case '2':
					return "Blue";
				case '3':
					return "Red";
				case '4':
					return "Magenta";
				case '5':
					return "Yellow";

				default: return "White";
			}
		}
	
		public override string ToString()
		{
			return "GameManger";
		}

		public void Credits()
		{
			Console.Clear();
			Console.WriteLine("The creator of all of this,");
			Thread.Sleep(1000);
			Console.WriteLine("the walls,");
			Thread.Sleep(1000);
			Console.WriteLine("the monsters,");
			Thread.Sleep(1000);
			Console.WriteLine("the chests,");
			Thread.Sleep(1000);
			Console.WriteLine("the traps..");
			Thread.Sleep(1000);
			Console.WriteLine("and even you,");
			Thread.Sleep(1500);
			Console.WriteLine("is God!");
			Thread.Sleep(3000);
			Console.WriteLine();
			Console.WriteLine("JK LOL\nit's me - Lee (BADASS) Shapira");
			Thread.Sleep(3000);
			Console.WriteLine();
			Console.WriteLine("Press Enter go back to the Main Menu");
			Console.ReadLine();
			Console.Clear();
        }

		public void SaveGame(Player p)
		{
			string name = p.Name;
			string symbol = p.PlayerSymbol.ToString();
			string avatarColor = ColorGetColor(p.Color);
			string level = mapManager.CurrentMapID.ToString();
			string maxHP = p.MaxHP.ToString();
			string currHP = p.CurrentHP.ToString();
			string damage = p.Damage.ToString();
			string Potion = p.Potions.ToString();
			string text = name + "," + symbol + "," + avatarColor + "," + level + "," + maxHP + "," + currHP + "," + damage + "," + Potion;
			System.IO.File.WriteAllText("GameData.txt",text);
		}

		//public GameManager(Player player)
		//{
		//    this.player = player;
		//}
	}
}
