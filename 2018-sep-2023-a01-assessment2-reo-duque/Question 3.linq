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

//list the teams and all their players. sort by team name, and players by last name, then first name
Players
	.GroupBy(x => x.Team)
	.Select(x => new {
		Team = x.Key.TeamName,
		Coach = x.Key.Coach,
		Players = x.Key.Players.Select(x => new {
			LastName = x.LastName,
			FirstName = x.FirstName,
			Gender = x.Gender == "F" ? "Female" : "Male",
			Age = x.Age
		}).OrderBy(x => x.LastName)
		.ThenBy(x => x.FirstName)
	}).OrderBy(x => x.Team)
	.Dump()