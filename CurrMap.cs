using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FinalProject___LeeShapira
{
	public class CurrMap
	{
		private char[,] map;
		private int startX;
		private int startY;
		private int trapX;
		private int trapY;
		private ArrayList monsters;
		public static int counter = 0;
		public int id = 0;

		public CurrMap(char[,] map, string[] mapInfo)
		{
			this.map = map;
			this.startX = int.Parse(mapInfo[0]);
			this.startY = int.Parse(mapInfo[1]);
			this.trapY = int.Parse(mapInfo[2]);
			this.trapX = int.Parse(mapInfo[3]);
			monsters = new ArrayList();
			counter++;
			id = counter;
		}
	
		public CurrMap(char[,] map)
		{
			this.map = map;
			monsters = new ArrayList();
			counter++;
	        id = counter;
		}

		public Monster GetMonster(char ch)
		{
			switch (ch)
			{
				case 'h':
					return new Monster(MonsterType.Hopy,this);
				case 's':
					return new Monster(MonsterType.Skeleton, this);
				case '£':
					return new Monster(MonsterType.Scorpion, this);
				case 'B':
					return new Monster(MonsterType.Bargon, this);
				case 'W':
					return new Monster(MonsterType.Witch, this);
				case 'K':
					return new Monster(MonsterType.King_Hopy, this);
				case 'H':
					return new Monster(MonsterType.Hulk, this);
				case 'ƒ':
					return new Monster(MonsterType.Figon, this);
				case 'T':
					return new Monster(MonsterType.Titan, this);
				case '©':
					return new Monster(MonsterType.Chaos_Cara, this);
				default:
					return null;
			}
		}

		public Monster GetExistingMonster(int x, int y)
		{
			foreach (Monster mon in monsters)
			{
				if(mon._coord.X == x && mon._coord.Y == y) return mon;
			}
			return null;
		}

		public void PrintMap()
		{
			Monster mon;
			for (int i = 0; i < map.GetLength(0); i++)
			{
				for (int j = 0; j < map.GetLength(1); j++)
				{
					mon = GetMonster(map[i, j]);
					if (mon != null)
					{
						Console.ForegroundColor = ConsoleColor.Green;
						Console.Write(map[i, j]);
						Console.ForegroundColor = ConsoleColor.White;
						mon.SetMonsterPosition(j, i);
						monsters.Add(mon);
					}
					else Console.Write(map[i, j]);

				}
				Console.WriteLine("");
			}
			Trap.DeactivateTrap();
			Trap.Action(trapX, trapY);
			PrintElementDictionary();
		}

		public char[,] GetMap() 
		{
			return map;
		}

		public int StartX
		{
			get { return startX; }
			set { startX = value; }
		}
		
		public int StartY
		{
			get { return startY; }
			set { startY = value; }
		}

		public void MonstersMovement(Player player)
		{
			int step,x,y;
			x = player.Coord.X;
			y = player.Coord.Y;
			for (int i = 0; i < monsters.Count; i++)
			{
				Monster mon = monsters[i] as Monster;
				step = mon.CheckRangeAtX(x, y);
				if (step == -999)
				{
					step = mon.CheckRangeAtY(x, y);
					if (step == 0)
					{
						CombatSystem.Combat_System(player, mon);

					}
				}
				else if(step == 0)
				{
					CombatSystem.Combat_System(player, mon);
				}
			}
		}

		public void KillMonster(Monster mon)
		{
			monsters.Remove(mon);
		}

		public void PrintElementDictionary()
		{
			int i = 0;
			string[] lines = System.IO.File.ReadAllLines("Maps\\Dictionary.txt");
			foreach (string line in lines)
			{
				Console.SetCursorPosition(40, i++);
				Console.WriteLine(line + "\n");
			}
		}
	}
}
