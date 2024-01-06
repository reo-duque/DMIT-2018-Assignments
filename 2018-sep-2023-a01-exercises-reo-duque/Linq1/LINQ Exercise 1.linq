<Query Kind="Statements">
  <Connection>
    <ID>32cc976e-1b8c-44a4-b188-61982169f93a</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>WorkSchedule</Database>
    <DriverData>
      <LegacyMFA>false</LegacyMFA>
    </DriverData>
  </Connection>
</Query>

//1. List all employees. Order by "Last Name"
Employees
	.OrderBy(x => x.LastName)
	.Select(x => new
	{
		Name = x.FirstName + " " + x.LastName,
		Phone = x.HomePhone,
		Available = x.Active == true ? "Yes" : "No"
	})
	.Dump();
	
//2. Find skills required for contracts with more than 1 employees. order by "facility" then "skills"
ContractSkills
	.Where(x => x.NumberOfEmployees > 1)
	.OrderBy(x => x.Contract.Location.Name)
	.ThenBy(x => x.Skill.Description)
	.Select(x => new
	{
		Start = x.Contract.StartDate,
		Finish = x.Contract.EndDate,
		Facility = x.Contract.Location.Name,
		Phone = x.Contract.Location.Phone,
		Skill = x.Skill.Description,
		Employees = x.NumberOfEmployees	
	})
	.Dump();
	
//3. skills required sorted alphabetically by "LastName", "Level*(experct -> novice), and "Skill". only show employees who are active.
EmployeeSkills
	.Where(x => x.Employee.Active == true)
	.OrderBy(x => x.Employee.LastName)
	.ThenBy(x => x.Level)
	.ThenBy(x => x.Skill.Description)
	.Select(x => new
	{
		FirstName = x.Employee.FirstName,
		LastName = x.Employee.LastName,
		Skill = x.Skill.Description,
		Level = x.Level == 1 ? "Novice" : x.Level == 2 ? "Proficient" : "Expert",
		TicketRequired = x.Skill.RequiresTicket == true ? "Yes" : "No"
	})
	.Dump();

////4. manager info, order by "maximum skill years" then "lastname"
Employees
	.Select(x => new
	{
		LastName = x.LastName,
		FirstName = x.FirstName,
		SkillCount = x.EmployeeSkills.Count(),
		MinSkillYear = x.EmployeeSkills.Min(x => x.YearsOfExperience) == null ? 0 : x.EmployeeSkills.Min(x => x.YearsOfExperience),
		MaxSkillYear = x.EmployeeSkills.Max(x => x.YearsOfExperience) == null ? 0 : x.EmployeeSkills.Max(x => x.YearsOfExperience),
		MinSkillWages = x.EmployeeSkills.Min(x => x.HourlyWage),
		MaxSkillWages = x.EmployeeSkills.Max(x => x.HourlyWage),
	})
	.OrderByDescending(x => x.MaxSkillYear)
	.ThenBy(x => x.LastName)
	.Dump();
	
//5. List skills and the number of novice, proficient and experts, order by skills
Skills
	.Select(x => new
	{
		Description = x.Description,
		Novice = x.EmployeeSkills.Count(x => x.Level == 1),
		Proficient = x.EmployeeSkills.Count(x => x.Level == 2),
		Expert = x.EmployeeSkills.Count(x => x.Level == 3),
	})
	.OrderBy(x => x.Description)
	.Dump();

	





	