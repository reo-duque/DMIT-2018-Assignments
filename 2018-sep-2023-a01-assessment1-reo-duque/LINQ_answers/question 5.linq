<Query Kind="Statements">
  <Connection>
    <ID>b451acaa-9548-4446-b053-5539cb0b1f7f</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>FSIS_2018</Database>
    <DriverData>
      <LegacyMFA>false</LegacyMFA>
    </DriverData>
  </Connection>
</Query>

//
Players
	.Select(x => new
	{
		TeamName = x.Team.TeamName,
		TotalGamesAsHomeTeam = x.Team.HomeGames.Count(),
		TotalGamesAsVisitingTeam = x.Team.VisitingGames.Count(),
		HighestScoreAsHomeTeam = Games.Where(a => a.HomeTeamID == x.TeamID).Select(a => a.HomeTeamScore).Max(),
		LowestScoreAsHomeTeam = Games.Where(a => a.HomeTeamID == x.TeamID).Select(a => a.HomeTeamScore).Min(),
		HighestScoreAsVisitingTeam = Games.Where(a => a.VisitingTeamID == x.TeamID).Select(a => a.VisitingTeamScore).Max(),
		LowestScoreAsVisitingTeam = Games.Where(a => a.VisitingTeamID == x.TeamID).Select(a => a.VisitingTeamScore).Min(),
	}).Distinct()
	.OrderBy(x => x.TeamName)
	.Dump();
	