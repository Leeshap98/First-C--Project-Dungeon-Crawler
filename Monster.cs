using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject___LeeShapira
{
	public enum MonsterType
	{
		Hopy = 2,
		Skeleton = 10,
		Scorpion = 15,
		Bargon = 20,
		Witch = 25,
		King_Hopy = 30,
		Hulk = 35,
		Figon = 40,
		Titan = 45,
		Chaos_Cara = 60
	};

	public struct MCoord
	{
		public int X { get; set; }
		public int Y { get; set; }


		public MCoord(int x, int y)
		{
			X = x;
			Y = y;
		}

		public void Add(MCoord coord)
		{
			this.X += coord.X;
			this.Y += coord.Y;
		}

	}

	public class Monster
	{
		public string Name { get;  set; }
		public int Damage { get; private set; }
		public int MaxHp { get; private set; }
		public int CurrentHP { get; private set; }
		public char Symbol { get; private set; }

		public MCoord _coord;

		public CurrMap currMap;

		public void SetMonsterPosition(int x, int y)
		{
			currMap.GetMap()[_coord.Y, _coord.X] = ' ';
			currMap.GetMap()[y, x] = Symbol;
			_coord.X = x;
			_coord.Y = y;
		}

		public Monster(MonsterType type, CurrMap currMap)
		{
			switch (type)
			{
				case MonsterType.Hopy:
					Name = "Hopy";
					Damage = (int)MonsterType.Hopy;
					MaxHp = 10;
					Symbol = 'h';
					break;
				case MonsterType.Skeleton:
					Name = "Skeleton";
					Damage = (int)MonsterType.Skeleton;
					MaxHp = 20;
					Symbol = 's';
					break;
				case MonsterType.Scorpion:
					Name = "Scorpion";
					Damage = (int)MonsterType.Scorpion;
					MaxHp = 30;
					Symbol = '£';
					break;
				case MonsterType.Bargon:
					Name = "Bargon";
					Damage = (int)MonsterType.Bargon;
					MaxHp = 40;
					Symbol = 'B';
					break;
				case MonsterType.Witch:
					Name = "Witch";
					Damage = (int)MonsterType.Witch;
					MaxHp = 50;
					Symbol = 'W';
					break;
				case MonsterType.King_Hopy:
					Name = "King Hopy";
					Damage = (int)MonsterType.King_Hopy;
					MaxHp = 60;
					Symbol = 'K';
					break;
				case MonsterType.Hulk:
					Name = "Hulk";
					Damage = (int)MonsterType.Hulk;
					MaxHp = 70;
					Symbol = 'H';
					break;
				case MonsterType.Figon:
					Name = "Figon";
					Damage = (int)MonsterType.Figon;
					MaxHp = 80;
					Symbol = 'ƒ';
					break;
				case MonsterType.Titan:
					Name = "Titan";
					Damage = (int)MonsterType.Titan;
					MaxHp = 90;
					Symbol = 'T';
					break;
				case MonsterType.Chaos_Cara:
					Name = "Chaos Cara";
					Damage = (int)MonsterType.Chaos_Cara;
					MaxHp = 120;
					Symbol = '©';
					break;
				default:
					throw new ArgumentException("there is no monster like that");
			}
			CurrentHP = MaxHp;
			this.currMap = currMap;
		}
		
		public bool IsDead()
		{
			return CurrentHP <= 0;
		}

		public void Death()
		{
			if (IsDead())
			{
				currMap.GetMap()[_coord.Y, _coord.X] = ' ';
				Console.SetCursorPosition(_coord.X, _coord.Y);
				Console.Write(" ");
			}
		}


		public int DamageInflicted()
		{
			if (IsDead() || HitChance())
				return 0;
			else
				return Damage;
		}

		public void TakeDamage(int damageTaken)
		{
			if (!IsDead() || Evasion())
			{
				CurrentHP -= damageTaken;
				if (CurrentHP < 0)
					CurrentHP = 0;
			}

		}

		public bool HitChance()
		{
			Random random = new Random();
			int hitChance = random.Next(0, 10);
			return hitChance < 5;
		}

		public bool Evasion()
		{
			Random random = new Random();
			int evasion = random.Next(0, 10);
			return evasion >= 3;
		}


		public int CheckRangeAtX(int x,int y)
		{
			int distX = Math.Abs(this._coord.X - x);
			if (this._coord.Y == y)
			{
				if (distX == 1) return 0;
				else if (distX <= 4)
				{
					if (x < this._coord.X)
					{
						Console.SetCursorPosition(_coord.X, _coord.Y);
						MoveLogic(_coord.X - 1, _coord.Y);
						return -1;
					}
					else if (x > this._coord.X)
					{
						Console.SetCursorPosition(_coord.X, _coord.Y);
						MoveLogic(_coord.X + 1, _coord.Y);
						return 1;
					}
					else{return -999; }//לא יגיע לפה
				}
				else { return -999; }//לא יגיע לפה
			}
			else { return -999; }
		}

		public int CheckRangeAtY(int x, int y)
		{
			int distY = Math.Abs(this._coord.Y - y);
			if (this._coord.X == x)
			{
				if (distY == 1) return 0;
				else if (distY <= 4)
				{
					if (y < this._coord.Y)
					{
						Console.SetCursorPosition(_coord.X, _coord.Y);
						MoveLogic(_coord.X, _coord.Y-1);
						return -1;
					}
					else if (y > this._coord.Y)
					{
						Console.SetCursorPosition(_coord.X, _coord.Y);
						MoveLogic(_coord.X, _coord.Y+1);
						return 1;
					}
					else { return -999; }//לא יגיע לפה
				}
				else { return -999; }//לא יגיע לפה
			}
			else { return -999; }
		}

		void MoveLogic(int x, int y)
		{
			char place = currMap.GetMap()[y, x];
			if (place == ' ')
			{
				Console.WriteLine(" ");
				Console.SetCursorPosition(x, y);
				SetMonsterPosition(x, y);
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine(Symbol);
				Console.ForegroundColor = ConsoleColor.White;




			}
		}

		public override string ToString()
		{
			return Name+ " at pos ("+ _coord.Y+","+ _coord.X+")";
		}

	}
}
