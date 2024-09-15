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
		}
		public static int[] ComputeRanks(int number, int[][] games)
		{
			int[] ranking = new int[number];
			List<int> teams = new List<int>();
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
				for (int j = 0; j < games[i].Length - 3; j++)
				{
					int scoreTeamA = games[i][j + 2];
					int scoreTeamB = games[i][j + 3];

					if (scoreTeamA > scoreTeamB)
					{
						goalsFavor[teamA] += scoreTeamA;
						goalsAgainst[teamA] += scoreTeamB;

						goalsFavor[teamB] += scoreTeamB;
						goalsAgainst[teamB] += scoreTeamA;

						differenceGoals[teamA] = goalsFavor[teamA] - goalsAgainst[teamA];
						differenceGoals[teamB] = goalsFavor[teamB] - goalsAgainst[teamB];

						points[teamA] += 2;
					}
					else if (scoreTeamB > scoreTeamA)
					{
						goalsFavor[teamB] += scoreTeamB;
						goalsAgainst[teamB] += scoreTeamA;

						goalsFavor[teamA] += scoreTeamA;
						goalsAgainst[teamA] += scoreTeamB;

						differenceGoals[teamA] = goalsFavor[teamA] - goalsAgainst[teamA];
						differenceGoals[teamB] = goalsFavor[teamB] - goalsAgainst[teamB];

						points[teamB] += 2;
					}
					else
					{
						goalsFavor[teamA] += scoreTeamA;
						goalsAgainst[teamA] += scoreTeamB;

						goalsFavor[teamB] += scoreTeamB;
						goalsAgainst[teamB] += scoreTeamA;


						points[teamA] += 1;
						points[teamB] += 1;
					}

				}
			}
			for (int i = 0; i < teams.Count; i++)
			{
				for (int j = teams.Count - 1; j > i; j--)
				{
					if (points[j] > points[i])
					{
                        var auxPoints = points[j];
						points[j] = points[i];
						points[i] = auxPoints;

						var auxTeams= teams[j];
						teams[j] = teams[i];
						teams[i] = auxTeams;

						var auxDifGoals = differenceGoals[j];
						differenceGoals[j] = differenceGoals[i];
						differenceGoals[i] = auxDifGoals;

						var auxGoalFav = goalsFavor[j];
						goalsFavor[j] = goalsFavor[i];
						goalsFavor[i] = auxGoalFav;

						var auxGoalAgainst = goalsAgainst[j];
						goalsAgainst[j] = goalsAgainst[i];
						goalsAgainst[i] = auxGoalAgainst;
						
					} else if(points[j] == points[i] && differenceGoals[i] == differenceGoals[j] && goalsFavor[i] == goalsFavor[j])
					{
                       
                    }
				}
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