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

//1. show all skills requiring a ticket and which employees have those skills. order employees by years of experience(highest to lowest)
EmployeeSkills
	.Where(x => x.Skill.RequiresTicket == true)
	.GroupBy(x => x.Skill.Description)
	.Select(x => new {
		Description = x.Key,
		Employees = x.Select(a => new {
				Name = a.Employee.FirstName + " " + a.Employee.LastName,
				Level = a.Level == 1 ? "Novice" : a.Level == 2 ? "Proficient" : "Expert",
				YearExperience = a.YearsOfExperience ?? 0
			}).OrderByDescending(x => x.YearExperience)
	}).Dump();

//2. List all employees with multiple skills. ignore employees w/ 1 skill, show name of emplyee and list their skillset, show name of skill, level of competence and years of experience
EmployeeSkills
	.GroupBy(x => x.Employee)
	.Select(x => new {
		Name = x.Key.FirstName + " " + x.Key.LastName,
		SkillSet = x.Select(a => new {
			Description = a.Skill.Description,
			Level = a.Level == 1 ? "Novice" : a.Level == 2 ? "Proficient" : "Expert",
			YearExperience = a.YearsOfExperience ?? 0
		})
	}).Where(x => x.SkillSet.Count() > 1)
	.Dump();
	
//3. show # of employees for each day ordered by day-of-week
Shifts
	.Where(x => x.PlacementContract.Location.LocationID == 4)
	.GroupBy(x => x.DayOfWeek)
	.Select(x => new {
		DayOfWeek = x.Key == 0 ? "Sunday" : x.Key == 1 ? "Monday" : x.Key == 2 ? "Tuesday" : x.Key == 3 ? "Wednesday" :x.Key == 4 ? "Thursday" : x.Key == 5 ? "Friday" : "Saturday",
		EmployeesNeeds = x.Sum(x => x.NumberOfEmployees)
	})
	.Dump();
	
//4. List all employees with most years of experience
EmployeeSkills
	.GroupBy(x => x.Employee)
	.Select(x => new {
		Name = x.Key.FirstName + " " + x.Key.LastName,
		YOE = x.Key.EmployeeSkills.Sum(x => x.YearsOfExperience)
	})
	.Where(x => x.YOE == EmployeeSkills.Select(a => a.Employee.EmployeeSkills.Sum(b=> b.YearsOfExperience)).Max())
	.Dump();
	
//5. For September, list total earnings per employee, number of shifts, regular earnings, overtime earnings.
Schedules
	.ToList()
	.Where(x => x.Day.Month == 9)
	.GroupBy(x => x.Employee)
	.Select(x => new
	{
		Name = x.Key.FirstName + " " + x.Key.LastName,
		RegularEarnings = x.Where(x => x.OverTime == false).Select(x => (x.Shift.EndTime.ToTimeSpan().Subtract(x.Shift.StartTime.ToTimeSpan())).Hours * x.HourlyWage).Sum(),
		OverTimeEarnings = x.Where(x => x.OverTime == true).Select(x => (x.Shift.EndTime.ToTimeSpan().Subtract(x.Shift.StartTime.ToTimeSpan())).Hours * 1.5m * x.HourlyWage).Sum(),
		NumberOfShifts = x.Key.Schedules.Select(x => x).Where(x => x.Day.Month == 9).Count(),
	})
	.OrderBy(x => x.Name)
	.Dump();

