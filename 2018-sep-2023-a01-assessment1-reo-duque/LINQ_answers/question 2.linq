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

//all games where team lost, orderby results by spread descending and by date
Games
	.Where(x => x.HomeTeamScore < x.VisitingTeamScore)
	.Select(x => new {
		Date = x.GameDate,
		Home = x.Home.TeamName,
		Visiting = x.Visiting.TeamName,
		HomeScore = x.HomeTeamScore,
		TeamScore = x.VisitingTeamScore,
		Spread = x.VisitingTeamScore - x.HomeTeamScore
	})
	.OrderByDescending(x => x.Spread)
	.ThenBy(x => x.Date)
	.Dump();
