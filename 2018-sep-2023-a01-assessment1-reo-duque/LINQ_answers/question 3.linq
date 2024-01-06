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

//highest combined score, order by combined score and home team score descending
Games
	.Select(x => new
	{
		Date = x.GameDate,
		Home = x.Home.TeamName,
		HomeCoach = x.Home.Coach,
		VisitingTeam = x.Visiting.TeamName,
		VisitCoach = x.Visiting.Coach,
		HomeTeamScore = x.HomeTeamScore,
		VisitingTeamScore = x.VisitingTeamScore,
	})
	.OrderByDescending(x => x.HomeTeamScore + x.VisitingTeamScore)
	.ThenByDescending(x => x.HomeTeamScore)
	.Dump();
