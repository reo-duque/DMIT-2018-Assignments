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
	.Select(x => new {
		TeamName = x.Team.TeamName,
		TotalPlayers = x.Team.Players.Count(),
		YoungestPlayerAge = x.Team.Players.Min(x => x.Age),
		OldestPlayerAge = x.Team.Players.Max(x => x.Age)
	}).Distinct()
	.OrderBy(x => x.TeamName)
	.Dump();