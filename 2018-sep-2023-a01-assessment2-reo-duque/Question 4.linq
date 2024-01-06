<Query Kind="Statements">
  <Connection>
    <ID>eded0c58-9a34-4a3a-8f09-f895b3035426</ID>
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

//List all the top teams with most games won
Players
	.GroupBy(x => x.Team)
	.Select(x => new {
		Teams = x.Key.TeamName,
		Wins = x.Key.Wins
	}).Where(a => a.Wins == Players.Select(b => b.Team.Wins).Max())
	.Dump();