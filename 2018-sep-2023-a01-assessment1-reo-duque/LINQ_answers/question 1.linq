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

//1. retrieve data of all players goal > 1 in a single game
PlayerStats
	.Where(x => x.Goals > 1)
	.Select(x => new {
		Name = x.Player.FirstName + " " + x.Player.LastName,
		Date = x.Game.GameDate,
		Goals = x.Goals
	})
	.OrderByDescending(x => x.Goals)
	.ThenBy(x => x.Name)
	.Dump();