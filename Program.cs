using System;

namespace SportsLeague // Note: actual namespace depends on the project name.
{
	internal class Program
	{
		static void Main(string[] args)
		{
			int number = 6;
			int[][] games =
			{
				new int[]{0,5,2,2}, // Team 0 - Team 5 => 2:2 ->Draw 1p for team.
				new int[]{1,4,0,2},
				new int[]{2,3,1,2},
				new int[]{1,5,2,2},
				new int[]{2,0,1,1},
				new int[]{3,4,1,1},
				new int[]{2,5,0,2},
				new int[]{3,1,1,1},
				new int[]{4,0,2,0},
			};
			int[] rank = ComputeRanks(number, games);
			foreach (var r in rank)
			{
				Console.Write(r);
				Console.WriteLine();
			}
		}
		public static int[] ComputeRanks(int number, int[][] games)
		{
			List<int> teams = new List<int>();
			int[] ranking = new int[number];
			int[] points = new int[number];
			int[] goalsFavor = new int[number];
			int[] goalsAgainst = new int[number];
			int[] differenceGoals = new int[number];
			int[] position = new int[number];

			for (int i = 0; i < games.GetLength(0); i++)
			{
				int teamsA = games[i][0];
				int teamsB = games[i][1];
				teams.Add(teamsA);
				teams.Add(teamsB);
			}

			for (int i = 0; i < teams.Count; i++)
			{
				int value = teams[i];
				for (int j = teams.Count - 1; j > i; j--)
				{
					int nextValue = teams[j];
					if (value == nextValue)
					{
						teams.RemoveAt(j);
					}
				}
			}

			teams.Sort();
			for (int i = 0; i < games.GetLength(0); i++)
			{
				int teamA = games[i][0];
				int teamB = games[i][1];

				int scoreTeamA = games[i][2];
				int scoreTeamB = games[i][3];

				goalsFavor[teamA] += scoreTeamA;
				goalsAgainst[teamA] += scoreTeamB;
				goalsFavor[teamB] += scoreTeamB;
				goalsAgainst[teamB] += scoreTeamA;

				if (scoreTeamA > scoreTeamB)
				{
					points[teamA] += 2;
				}
				else if (scoreTeamB > scoreTeamA)
				{
					points[teamB] += 2;
				}
				else
				{
					points[teamA] += 1;
					points[teamB] += 1;
				}
			}

			for (int i = 0; i < teams.Count; i++)
			{
				differenceGoals[i] = goalsFavor[i] - goalsAgainst[i];
			}

			for (int i = 0; i < teams.Count; i++)
			{
				
			}
			for (int i = 0; i < teams.Count; i++)
			{

				Console.Write($"Position: {position[i]} |");
				Console.Write($"Team : {teams[i]} | ");
				Console.Write($"GF:GC {goalsFavor[i]} : {goalsAgainst[i]} | ");
				Console.Write($"Difference Goals: {differenceGoals[i]} |");
				Console.Write($"Points: {points[i]} ");
				Console.WriteLine();
			}
			return ranking;
		}
	}
}