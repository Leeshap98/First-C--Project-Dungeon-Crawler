using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject___LeeShapira
{
	public class MapManager
	{
		private int _currentMapID;
		private CurrMap _currMap;


		public MapManager() 
		{
			_currentMapID = 0;
			IncrementLvl();

		}
		public MapManager(string level)
		{
			_currentMapID = int.Parse(level) - 1;
			IncrementLvl();

		}

		public int CurrentMapID
		{
			get { return _currentMapID; }
			set { _currentMapID = value; }
		}

		public CurrMap CurrMap
		{
			get { return _currMap; }
			set { _currMap = value; }
		}


		public void IncrementLvl()
		{
			_currentMapID++;
			if (_currentMapID >= 1 && _currentMapID < 11)
			{
				LoadMap();
			}
			else if (_currentMapID == 11) LoadFinish();
		}

		public void LoadMap()
		{
			string[] lines = System.IO.File.ReadAllLines("Maps\\level" + _currentMapID + ".txt");
			string[] mapInfo = lines[0].Split(',');
			char[,] map = new char[lines.Length-1, lines[1].Length];
			for (int i = 1; i < lines.Length; i++)
			{
				for (int j = 0; j < lines[i].Length; j++)
				{
					map[i-1, j] = lines[i][j];
				}
			}
			CurrMap = new CurrMap(map, mapInfo);
			CurrMap.PrintMap();
		}


		public void LoadFinish()
		{
			string[] lines = System.IO.File.ReadAllLines("Maps\\gameOver.txt");
			char[,] map = new char[lines.Length, lines[1].Length];
			for (int i = 0; i < lines.Length; i++)
			{
				for (int j = 0; j < lines[i].Length; j++)
				{
					map[i, j] = lines[i][j];
				}
			}
			CurrMap = new CurrMap(map);
			CurrMap.PrintMap();
		}

	}
}
