<Query Kind="Expression">
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

//list the players' stats. order by full player name
PlayerStats
	.GroupBy(x => x.Player)
	.Select(x => new {
		name = x.Key.FirstName + ' ' + x.Key.LastName,
		teamname = x.Key.Team.TeamName,
		goals = x.Sum(x => x.Goals),
		assists = x.Sum(x => x.Assists),
		redcards = x.Count(x => x.RedCard),
		yellowcards = x.Count(x => x.YellowCard)
	}).OrderBy(x => x.name)