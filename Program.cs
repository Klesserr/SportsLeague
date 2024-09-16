using System;
using System.Runtime;

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
			int[] points = new int[number];
			int[] goalsFavor = new int[number];
			int[] goalsAgainst = new int[number];
			int[] differenceGoals = new int[number];
			int[] position = new int[number];
			int countJump = 0;
			var getTeams = GetTeams(games);
			getTeams.Sort();

			var orderPoints = OrderPoints(differenceGoals, games,points,goalsFavor,goalsAgainst);
			var orderFavorGoals = OrderByGoalsFavor(goalsFavor, games, points);
			var orderAgainstGoals = OrderByGoalsAgaints(goalsAgainst, games, points);
			var getDifferenceGoals = OrderByDiffGoals(differenceGoals, games, points, orderFavorGoals, orderAgainstGoals);
			var GetThePoints = GetPoints(games, goalsFavor, goalsAgainst, differenceGoals, points);

			Array.Sort(orderPoints);
			Array.Reverse(orderPoints);

			Array.Sort(getDifferenceGoals);
			Array.Reverse(getDifferenceGoals);

			for(int i = 0; i < orderPoints.Length; i++)
			{
                Console.Write($"GF: {goalsFavor[i]} | ");
                Console.Write($"GC: {goalsAgainst[i]} | ");
                Console.Write($"DifGoals: {getDifferenceGoals[i]} | ");
                Console.Write($"Points: {orderPoints[i]} | ");
                Console.WriteLine();
            }
			
			Console.WriteLine($"RESULTADO DEL RANKING:");
			foreach (var r in ranking)
			{
				Console.Write($"[{r}]");
			}
			return ranking;
		}
		private static int[] OrderPoints(int[] differenceGoals, int[][] games, int[] points, int[] goalsFavor, int[] goalsAgainst)
		{
			var teams = GetTeams(games);
			for (int i = 0; i < teams.Count; i++)
			{
				for (int j = i + 1; j < teams.Count; j++)
				{
					if (points[j] > points[i])
					{
						var auxPoints = points[j];
						points[j] = points[i];
						points[i] = auxPoints;

						var auxTeams = teams[j];
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

					}
					else if (points[j] == points[i] && goalsFavor[i] == goalsFavor[j])
					{
						var auxPoints = points[j];
						points[j] = points[i];
						points[i] = auxPoints;

						var auxTeams = teams[j];
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
					}
					else if (points[j] == points[i] && goalsFavor[i] == goalsFavor[j] &&
						differenceGoals[i] == differenceGoals[j])
					{
						var auxPoints = points[j];
						points[j] = points[i];
						points[i] = auxPoints;

						var auxTeams = teams[j];
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
					}
				}
			}
			return points;
		}
		private static int[] OrderByDiffGoals(int[] differenceGoals, int[][] games, int[] points, int[] goalsFavor, int[] goalsAgainst)
		{
			var teams = GetTeams(games);
			for (int i = 0; i < teams.Count; i++)
			{
				for (int j = i + 1; j < teams.Count; j++)
				{
					if (points[j] > points[i])
					{
						var auxPoints = points[j];
						points[j] = points[i];
						points[i] = auxPoints;

						var auxTeams = teams[j];
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

					}
					else if (points[j] == points[i] && goalsAgainst[i] == goalsAgainst[j])
					{
						var auxPoints = points[j];
						points[j] = points[i];
						points[i] = auxPoints;

						var auxTeams = teams[j];
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
					}
					else if (points[j] == points[i] && goalsAgainst[i] == goalsAgainst[j] &&
						goalsFavor[i] == goalsFavor[j])
					{
						var auxPoints = points[j];
						points[j] = points[i];
						points[i] = auxPoints;

						var auxTeams = teams[j];
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
					}
				}
			}
			return differenceGoals;
		}
		private static int[] OrderByGoalsAgaints(int[] goalsAgainst, int[][] games, int[] points)
		{
			
			return goalsAgainst;
		}
		private static int[] OrderByGoalsFavor(int[] goalsFavor, int[][] games, int[] points)
		{
			var teams = GetTeams(games);
			for (int i = 0; i < teams.Count; i++)
			{
				for (int j = i + 1; j < teams.Count; j++)
				{
					if (points[j] > points[i])
					{
						var auxGoalFav = goalsFavor[j];
						goalsFavor[j] = goalsFavor[i];
						goalsFavor[i] = auxGoalFav;

					}else if(points[j] == points[i] && goalsFavor[j] > goalsFavor[i])
					{
						var auxGoalFav = goalsFavor[j];
						goalsFavor[j] = goalsFavor[i];
						goalsFavor[i] = auxGoalFav;
					}

				}
			}
			return goalsFavor;

		}

		public static int[][] GetPoints(int[][] games, int[] goalsFavor, int[] goalsAgainst, int[] differenceGoals, int[] points)
		{
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
			return games;
		}
		public static int[] OrderByPoints(int[] points, int[][] games)
		{
			var teams = GetTeams(games);
			teams.Sort();
			for (int i = 0; i < teams.Count; i++)
			{
				for (int j = i + 1; j < teams.Count; j++)
				{
					if (points[j] > points[i])
					{
						var auxPoints = points[j];
						points[j] = points[i];
						points[i] = auxPoints;

					}

				}
			}
			return points;
		}
		public static List<int> GetTeams(int[][] games)
		{
			var teams = new List<int>();
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
			return teams;

		}

	}
}