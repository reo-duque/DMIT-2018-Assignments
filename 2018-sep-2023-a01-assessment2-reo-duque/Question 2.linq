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

//display the number of male and female players
Players
	.GroupBy(x => x.Gender)
	.Select(x => new {
		Gender = x.Key == "F" ? "Female" : "Male",
		Count = x.Key == "F" ? x.Count(x => x.Gender == "F") : x.Count(x => x.Gender == "M")
	}).Dump();