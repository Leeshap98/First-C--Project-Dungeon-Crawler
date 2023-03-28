using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FinalProject___LeeShapira
{
	public struct Coord
	{
		public int X { get; set; }
		public int Y { get; set; }


		public Coord(int x, int y)
		{
			X = x;
			Y = y;
		}

		public void Add(Coord coord)
		{
			this.X += coord.X;
			this.Y += coord.Y;
		}

	}

	public class Player
	{
		private string _name;
		private char _playerSymbol;
		private int _currentHP;
		private int _maxHP;
		private int _damage;


		public Coord Coord;

		public string Name
		{
			get { return _name; }
			set 
			{
				if(value.Length >= 3)_name = value; 
			}
		}

		public char PlayerSymbol
		{
			get { return _playerSymbol; }
			set { _playerSymbol = value; }
		}

		public int CurrentHP
		{
			get { return _currentHP; }
			set 
			{
				if (value <= MaxHP) _currentHP = value;
				else _currentHP = MaxHP;
			}
		}

		public int MaxHP
		{
			get { return _maxHP; }
			set 
			{
				if(value >= 300)_maxHP = value; 
			}
		}

		public int Damage
		{
			get { return _damage; }
			set 
			{
				if(value > 0)_damage = value; 
			}
		}

		public ConsoleColor Color { get; set; }

		public GameManager GameManager { get; set; }

		public int Potions { get; set; }

		public Player(string name , char playerSymbol , ConsoleColor color, GameManager gameManager)
		{
			Name = name;
			PlayerSymbol = playerSymbol;
			Color = color;
			MaxHP = 300;
			CurrentHP = MaxHP;
			Damage = 20;
			Potions = 4;
			Coord = new Coord();
			GameManager = gameManager;
		}

		public Player(string name, char playerSymbol, ConsoleColor color,int maxHP, int curr, int damage, int potions, GameManager gameManager)
		{
			Name = name;
			PlayerSymbol = playerSymbol;
			Color = color;
			MaxHP = maxHP;
			CurrentHP = curr;
			Damage = damage;
			Potions = potions;
			Coord = new Coord();
			GameManager = gameManager;
		}

		public void SetPlayerPosition(int x, int y)
		{
			GameManager.mapManager.CurrMap.GetMap()[Coord.Y, Coord.X] = ' ';
			GameManager.mapManager.CurrMap.GetMap()[y, x] = PlayerSymbol;
			Coord.X = x;
			Coord.Y = y;
		}

		public void PlayerMovement()
		{
			int level = GameManager.mapManager.CurrentMapID;
			ConsoleKeyInfo keyInfo;
			do
			{
				keyInfo = Console.ReadKey(true);//כל עוד המקש לחוץ
				Console.SetCursorPosition(Coord.X, Coord.Y);
				switch (keyInfo.Key)
				{
					case ConsoleKey.D:
						SubMovement(Coord.Y, Coord.X + 1);
						break;

					case ConsoleKey.A:
						SubMovement(Coord.Y, Coord.X - 1);
						break;

					case ConsoleKey.W:
						SubMovement(Coord.Y - 1, Coord.X);
						break;

					case ConsoleKey.S:
						SubMovement(Coord.Y + 1, Coord.X);
						break;
					case ConsoleKey.P:
						PotionUse();
						break;
					case ConsoleKey.E:
						GameManager.SaveGame(this);
						Data.DataViewExtra(GameManager,"Game Saved", 2000);
						break;


				}
				GameManager.mapManager.CurrMap.MonstersMovement(this);
				level = GameManager.mapManager.CurrentMapID;
			}
			while (level<11);
		}

		public void SubMovement(int y , int x)
		{
			char[,] map = GameManager.mapManager.CurrMap.GetMap();
			char place = map[y, x];
			if (place == ' ')
			{
				MoveLogic(x, y);
			}

			else if (place == '«')
			{
				GameManager.IncrementLvl();
			}

			else if(place == '§')
			{
				if (Trap.ActivateTrap())
				{
					Trap.Action(x, y);
					TakeDamage(Trap.TrapDamage);
					Data.DataView(GameManager);
					string message = "You stepped on a trap! You lost "+ Trap.TrapDamage + " hp.";
					Data.DataViewExtra(GameManager, message, 2000);
				}
			}
			else if(place == '¶' || place == '▀' || place == '#')
			{
				map[y, x] = '‼';
				TreasureChest.Action(place, this);
				Data.DataView(GameManager);
				int cursorX = Console.CursorLeft;
				int cursorY = Console.CursorTop; 
				Console.SetCursorPosition(x, y);
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.Write(place);
				Console.ForegroundColor = ConsoleColor.White;
				Console.SetCursorPosition(cursorX, cursorY);
			}
			else
			{
				Monster monster = GameManager.mapManager.CurrMap.GetExistingMonster(x, y);
				if (monster != null)
				{
					CombatSystem.Combat_System(this, monster);
				}
			}
		}

		void MoveLogic(int x, int y)
		{
            Console.WriteLine(" ");
			Console.SetCursorPosition(x, y);
			SetPlayerPosition(x, y);
			Console.ForegroundColor = this.Color;
			Console.WriteLine(PlayerSymbol);
			Console.ForegroundColor = ConsoleColor.White;
		}

		public bool IsDead()
		{
			return CurrentHP <= 0;
		}
		
		//המכה שאני נותן
		public int DamageInflicted()
		{
			//אם השחקן מת או שלא הצליח לפגוע
			if (IsDead() || HitChance())
				return 0;
			//האם אני נותן מכה חזקה יותר או רגילה
			else if (CriticalHit())
				return Damage + 5;
			else
				return Damage;
		}


		//המכה שאני מקבל
		public void TakeDamage(int damageTaken)
		{
			//אם הוא לא מת או שהצלחתי להתחמק
			if (!IsDead() || Evasion())
			{
				CurrentHP -= damageTaken;
				if (CurrentHP < 0)
					CurrentHP = 0;
			}
		}

		public void Heal()
		{
			if (!IsDead())
			{
				if (CurrentHP < MaxHP)
					CurrentHP = MaxHP;
			}
		}
	
		public bool HitChance()
		{
			Random random = new Random();
			int hitChance = random.Next(0, 10);
			return hitChance < 3;
		}

		public bool Evasion()
		{
			Random random = new Random();
			int evasion = random.Next(0, 10);
			return evasion >= 5;
		}

		public bool CriticalHit()
		{
			Random random = new Random();
			int crit = random.Next(0, 10);
			return crit >= 7;
		}

		public void PotionUse()
		{
			if(Potions > 0)
			{
				Potions--;
				CurrentHP += 40;
				if (CurrentHP > MaxHP) 
					CurrentHP = MaxHP;
				Data.DataView(GameManager);
				Data.DataViewExtra(GameManager, "You took a potion!", 2000);
			}
		}
	}
}
