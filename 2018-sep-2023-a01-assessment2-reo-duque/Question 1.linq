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

//list all guardians with child > 1. sort by number of guardians' children, descending. sort the children age
Players
	.Where(x => x.Guardian.Players.Count() > 1)
	.GroupBy(x => x.Guardian)
	.Select(x => new {
		Name = x.Key.FirstName + ' ' + x.Key.LastName,
		Children = x.Select(ch => new {
						Name = ch.FirstName,
						Age = ch.Age,
						Gender = ch.Gender,
						Team = ch.Team.TeamName
					}).OrderBy(ch => ch.Age)
	}).OrderByDescending(x => x.Children.Count())
	.Dump();