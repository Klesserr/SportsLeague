using System;
using System.Text.Json;

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

			int numbers2 = 8;
			int[][] games2 = new int[][] { new int[] { 0, 7, 2, 1 } };
			var rank = ComputeRanks(numbers2, games2);
			var json = JsonSerializer.Serialize(rank);
			Console.WriteLine(json);

		}
		public static int[] ComputeRanks(int number, int[][] games)
		{
			List<Teams> detailedTeams = new List<Teams>();
			List<int> teams = new List<int>();
			List<int> emptyValues = new List<int>();
			int[] selectRanking = new int[number];

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

			if (teams.Count == 0)
			{
				for (int i = 0; i < number; i++)
				{
					emptyValues.Add(1);
				}
				return emptyValues.ToArray();
			}

			foreach (var team in teams)
			{
				detailedTeams.Add(
					new Teams()
					{
						Name = team
					}
				);
			}
			while (number > 0)
			{
				for (int i = 0; i < games.GetLength(0); i++)
				{
					Teams teamA = detailedTeams.FirstOrDefault(t => t.Name == games[i][0]);
					Teams teamB = detailedTeams.FirstOrDefault(t => t.Name == games[i][1]);

					var goalsScoredByA = games[i][2];
					var goalsScoredByB = games[i][3];

					teamA.GoalsFavor += goalsScoredByA;
					teamB.GoalsAgainst += goalsScoredByA;

					teamB.GoalsFavor += goalsScoredByB;
					teamA.GoalsAgainst += goalsScoredByB;


					if (goalsScoredByA > goalsScoredByB)
					{
						teamA.Points += 2;
					}
					else if (goalsScoredByA < goalsScoredByB)
					{
						teamB.Points += 2; ;
					}
					else
					{
						teamA.Points += 1;
						teamB.Points += 1;
					}
				}

				foreach (var team in detailedTeams)
				{
					team.DifferenceGoals = team.GoalsFavor - team.GoalsAgainst;
				}

				var teamsOrderByPoints = detailedTeams.OrderByDescending(t => t.Points).ToList();

				var ranking = 1;
				foreach (var team in teamsOrderByPoints)
				{
					team.Ranking = ranking;
					ranking++;
				}

				for (int i = 0; i < teamsOrderByPoints.Count - 1; i++)
				{
					if (teamsOrderByPoints[i].Points == teamsOrderByPoints[i + 1].Points)
					{
						if (teamsOrderByPoints[i].DifferenceGoals == teamsOrderByPoints[i + 1].DifferenceGoals)
						{
							if (teamsOrderByPoints[i].GoalsFavor == teamsOrderByPoints[i + 1].GoalsFavor)
							{
								teamsOrderByPoints[i + 1].Ranking = teamsOrderByPoints[i].Ranking;
							}
							else if (teamsOrderByPoints[i].GoalsFavor < teamsOrderByPoints[i + 1].GoalsFavor)
							{
								var actualPosition = teamsOrderByPoints[i].Ranking;
								teamsOrderByPoints[i].Ranking = teamsOrderByPoints[i + 1].Ranking;
								teamsOrderByPoints[i + 1].Ranking = actualPosition;
							}
						}
						else if (teamsOrderByPoints[i].DifferenceGoals < teamsOrderByPoints[i + 1].DifferenceGoals)
						{
							var actualPosition = teamsOrderByPoints[i].Ranking;
							teamsOrderByPoints[i].Ranking = teamsOrderByPoints[i + 1].Ranking;
							teamsOrderByPoints[i + 1].Ranking = actualPosition;
						}
					}
				}

				var teamsOrderByName = teamsOrderByPoints.OrderBy(t => t.Name).ToList();

				selectRanking = teamsOrderByName.Select(t => t.Ranking).ToArray();

				number--;
			}

			return selectRanking;
		}
		public class Teams
		{
			public int Name { get; set; }
			public int GoalsFavor { get; set; }
			public int GoalsAgainst { get; set; }
			public int DifferenceGoals { get; set; }
			public int Points { get; set; }
			public int Ranking { get; set; }
		}

	}
}