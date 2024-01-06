<Query Kind="Program">
  <Connection>
    <ID>4a229117-44ea-4cae-9f11-ba20949ca533</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <Server>COOKIES-AND-CRE</Server>
    <DisplayName>WorkSchedule-Entity</DisplayName>
    <Database>WorkSchedule</Database>
    <DriverData>
      <EncryptSqlTraffic>False</EncryptSqlTraffic>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
      <TrustServerCertificate>False</TrustServerCertificate>
    </DriverData>
  </Connection>
</Query>

void Main()
{
	//  YOUR NAME HERE: Reonel Duque

	#region Driver  //  3 Marks
	try
	{
		//  The driver must, at minimum perform three different task. 
		#region  Task 1
		//  -   Add a new employee and register their skills (minimun of two skills). 
		EmployeeRegistrationView addEmployee = new EmployeeRegistrationView();
		addEmployee.FirstName = GenerateName(5);
		addEmployee.LastName = GenerateName(5);
		addEmployee.HomePhone = RandomPhoneNumber();
		addEmployee.Active = true;
		
		EmployeeSkillView skill = new EmployeeSkillView();
		skill.SkillID = 1;
		skill.Level = 1;
		skill.HourlyWage = 100;

		addEmployee.EmployeeSkills.Add(skill);
		
		skill = new EmployeeSkillView();
		skill.SkillID = 2;
		skill.Level = 1;
		skill.HourlyWage = 20;
		addEmployee.EmployeeSkills.Add(skill);
		

		addEmployee.Dump("Before Add");
		
		EmployeeRegistrationView afterAdd = AddEditEmployeeRegistration(addEmployee);
		afterAdd.Dump("After Add");
		#endregion
		
		
		#region  Task 2 update an employee and their skill list. 
		 
		// setup Edit
		// before action
		#region Updating their first or last name
		int employeeID = Employees
							.OrderByDescending(x => x.EmployeeID)
							.Select(x => x.EmployeeID).FirstOrDefault();
		
		EmployeeRegistrationView beforeEditName = GetEmployee(employeeID);

		// showing results
		beforeEditName.Dump("Before Edit Name");
		
		//change first name
		beforeEditName.FirstName = GenerateName(5);
		
		//change last name
		beforeEditName.LastName = GenerateName(5);
		
		// show the result of the update of changing the first or last name
		EmployeeRegistrationView afterEditName = AddEditEmployeeRegistration(beforeEditName);
		
		afterEditName.Dump("After Edit Name");
		#endregion
		
		#region Updating one existing skill					
		EmployeeRegistrationView beforeEditSkill = GetEmployee(employeeID);
		beforeEditSkill.Dump("Before Edit Skill");
		
		EmployeeSkillView skillEdit = new EmployeeSkillView();
		skillEdit.EmployeeSkillID = beforeEditSkill.EmployeeSkills.FirstOrDefault().EmployeeSkillID;
		skillEdit.SkillID = 3;
		skillEdit.HourlyWage = 100;
		
		beforeEditSkill.EmployeeSkills.Add(skillEdit);
		EmployeeRegistrationView afterEditSkill = AddEditEmployeeRegistration(beforeEditSkill);
		//show result
		afterEditSkill.Dump("After Edit Skill");
		
		#endregion
		
		#region adding a minimum of one new skill
		EmployeeRegistrationView beforeAddSkill = GetEmployee(employeeID);
		beforeAddSkill.Dump("Before Add Skill");

		//adding one skill
		EmployeeSkillView skillAdd = new EmployeeSkillView();
		skillAdd.SkillID = 6;
		skillAdd.Level = 3;
		skillAdd.HourlyWage = 100;
		skillAdd.YearsOfExperience = 50;
		
		beforeAddSkill.EmployeeSkills.Add(skillAdd);
		
		//adding a second skill
		skillAdd = new EmployeeSkillView();
		skillAdd.SkillID = 5;
		skillAdd.Level = 2;
		skillAdd.HourlyWage = 35;
		
		beforeAddSkill.EmployeeSkills.Add(skillAdd);

		EmployeeRegistrationView afterAddSkill = AddEditEmployeeRegistration(beforeAddSkill);
		//show result
		afterAddSkill.Dump("After Add Skill");

		#endregion

		#endregion

		
		#region Task 3 attempts to register new skills with invalid data that will trigger all the business in this exercise
		
		EmployeeRegistrationView addTestCase = new EmployeeRegistrationView();
		
		addTestCase.FirstName = GenerateName(5);
		addTestCase.LastName = GenerateName(5);
		addTestCase.HomePhone = RandomPhoneNumber();
		addTestCase.Active = true;

		//Test 1 - First Name Empty
		//addTestCase.FirstName = "";
		
		//Test 2 - Last Name Empty
		//addTestCase.LastName = "";
		
		//Test 3 - HomePhone Empty
		//addTestCase.HomePhone = "";
		
		//Test 4 - Active is false
		//addTestCase.Active = false;
		
		//Testing EmployeeSkill Validations
		EmployeeSkillView skillAddFail = new EmployeeSkillView();
		
		//Test 5 - adding 0 skills
		//addTestCase.EmployeeSkills.Add(skillAddFail);
		
		skillAddFail.SkillID = 12;
		skillAddFail.Level = 0;
		skillAddFail.HourlyWage = 110;
		
		//Test 6 - adding one invalid skill
		//skillAddFail.SkillID = 13;
		
		//Test 7 - invalid level
		//skillAddFail.Level = 4;
		
		//Test 8 - negative hourlywage
		//skillAddFail.HourlyWage = -15;
		
		//Test 9 - > 100 hourly wage
		//skillAddFail.HourlyWage = 101;
		
		//Test 10 - negative years of experience
		//skillAddFail.YearsOfExperience = -49;
		
		//Test 11 - YOE > 50
		//skillAddFail.YearsOfExperience = 51;
		
		addTestCase.EmployeeSkills.Add(skillAddFail);
		
		//errors will show up here
		EmployeeRegistrationView testingAdd = AddEditEmployeeRegistration(addTestCase);
		
		#endregion

	}
	#endregion

	#region catch all exceptions 
	catch (AggregateException ex)
	{
		foreach (var error in ex.InnerExceptions)
		{
			error.Message.Dump();
		}
	}
	catch (ArgumentNullException ex)
	{
		GetInnerException(ex).Message.Dump();
	}
	catch (Exception ex)
	{
		GetInnerException(ex).Message.Dump();
	}
	#endregion
}
private Exception GetInnerException(Exception ex)
{
	while (ex.InnerException != null)
		ex = ex.InnerException;
	return ex;
}

#region Methods

#region AddEditEmployeeRegistration Method   //  6 Marks
public EmployeeRegistrationView AddEditEmployeeRegistration(EmployeeRegistrationView employeeRegistration)
{
	// --- Business Logic and Parameter Exception Section --- 
	#region Business Logic and Parameter Exception  //  2 
	// List initialization to capture potential errors during processing.
	List<Exception> errorList = new List<Exception>();

	//Business Rules
	/*
	
	You may update the skill of an existing employee. However, all employee information is required.
	
	*/
	
	//validate incoming parameters
	//First name, last name, and phone number are mandatory fields.
	if (string.IsNullOrWhiteSpace(employeeRegistration.FirstName))
	{
		errorList.Add(new ArgumentNullException("First Name is required and cannot be empty"));
	}
	if (string.IsNullOrWhiteSpace(employeeRegistration.LastName))
	{
		errorList.Add(new ArgumentNullException("Last Name is required and cannot be empty"));
	}
	if (string.IsNullOrWhiteSpace(employeeRegistration.HomePhone))
	{
		errorList.Add(new ArgumentNullException("Home Phone is required and cannot be empty"));
	}
	// Active must be true
	if (!employeeRegistration.Active)
	{
		errorList.Add(new Exception("Active must be true"));
	}
	
	//For a new employee, at least one new valid skill must be added.
	
	//Checks if the employee does not exist
	if (employeeRegistration.EmployeeID == 0)
	{
		//if employee doesn't exist, check if there's a skill selected
		if (employeeRegistration.EmployeeSkills.Count() == 0)
		{
			errorList.Add(new Exception("Skill must be added"));
		}
		//check to see if the skill selected is valid
		else
		{
			foreach (var newSkill in employeeRegistration.EmployeeSkills)
			{
				//checks for valid skillID
				if (Skills.Where(x => x.SkillID == newSkill.SkillID).Count() == 0)
				{
					errorList.Add(new Exception($"{newSkill.SkillID} is not a valid skill. A valid skill must be selected"));
				}
			
				//A valid "Level" is required.
				if (EmployeeSkills.Where(x => x.Level == newSkill.Level).Count() == 0)
				{
					errorList.Add(new Exception($"{newSkill.Level} is not a valid level. A valid level must be selected"));
				}

				/*"Years of Experience" (YOE) is optional but must meet specific criteria:
					-YOE must be a positive, non-zero integer or null.
					-If YOE is provided, it must fall within the range of 1 to 50 (inclusive).*/
				if (newSkill.YearsOfExperience != null)
				{
					if (newSkill.YearsOfExperience <= 0)
					{
						errorList.Add(new Exception("Years of Experience must be a positive non-zero integer"));
					}
					else if (newSkill.YearsOfExperience < 1 || newSkill.YearsOfExperience > 50)
					{
						errorList.Add(new Exception("Years of Experience must be within the range of 1 to 50"));
					}
				}
				/*"Hourly Wage" is required and must meet these conditions:
					- Hourly Wage should be a positive, non-zero decimal.
					- Hourly Wage must be within the range of $15.00 to $100.00(inclusive).*/
				if (newSkill.HourlyWage <= 0)
				{
					errorList.Add(new Exception("Hourly Wage must be a positive non-zero integer"));
				}
				else if (newSkill.HourlyWage < 15.00m || newSkill.HourlyWage > 100.00m)
				{
					errorList.Add(new Exception("Hourly Wage must be within the range of $15.00 to $100.00"));
				}
			}
		}
	}
	else
	{
		foreach (var newSkill in employeeRegistration.EmployeeSkills)
		{
			//checks for valid skillID
			if (Skills.Where(x => x.SkillID == newSkill.SkillID).Count() == 0)
			{
				errorList.Add(new Exception($"{newSkill.SkillID} is not a valid skill. A valid skill must be selected"));
			}

			//A valid "Level" is required.
			if (EmployeeSkills.Where(x => x.Level == newSkill.Level).Count() == 0)
			{
				errorList.Add(new Exception($"{newSkill.Level} is not a valid level. A valid level must be selected"));
			}

			/*"Years of Experience" (YOE) is optional but must meet specific criteria:
				-YOE must be a positive, non-zero integer or null.
				-If YOE is provided, it must fall within the range of 1 to 50 (inclusive).*/
			if (newSkill.YearsOfExperience != null)
			{
				if (newSkill.YearsOfExperience <= 0)
				{
					errorList.Add(new Exception("Years of Experience must be a positive non-zero integer"));
				}
				else if (newSkill.YearsOfExperience < 1 || newSkill.YearsOfExperience > 50)
				{
					errorList.Add(new Exception("Years of Experience must be within the range of 1 to 50"));
				}
			}
			/*"Hourly Wage" is required and must meet these conditions:
				- Hourly Wage should be a positive, non-zero decimal.
				- Hourly Wage must be within the range of $15.00 to $100.00(inclusive).*/
			if (newSkill.HourlyWage <= 0)
			{
				errorList.Add(new Exception("Hourly Wage must be a positive non-zero integer"));
			}
			else if (newSkill.HourlyWage < 15.00m || newSkill.HourlyWage > 100.00m)
			{
				errorList.Add(new Exception("Hourly Wage must be within the range of $15.00 to $100.00"));
			}
		}
	}

	#endregion

	// --- Main Method Logic Section --- 
	#region Method Code //  3 Marks
	//retrieve the employee from the database
	Employees employee = Employees
							.Where(x => x.EmployeeID == employeeRegistration.EmployeeID)
							.FirstOrDefault();
							
	//if employee doesn't exist, initialize it
	if (employee == null)
	{
		employee = new Employees();
	}
	
	//update all entity
	employee.FirstName = employeeRegistration.FirstName;
	employee.LastName = employeeRegistration.LastName;
	employee.HomePhone = employeeRegistration.HomePhone;
	employee.Active = employeeRegistration.Active;
	
	//process each skill provided
	foreach (var employeeSkillView in employeeRegistration.EmployeeSkills)
	{
		EmployeeSkills employeeSkill = EmployeeSkills
											.Where(x => x.EmployeeSkillID == employeeSkillView.EmployeeSkillID)
											.FirstOrDefault();
		
		//if the employeeskill doesn't exist, initialize it
		if (employeeSkill == null)
		{
			employeeSkill = new EmployeeSkills();
		}
		
		//map fields from the employeeskills item to the data model
		employeeSkill.SkillID = employeeSkillView.SkillID;
		employeeSkill.Level = employeeSkillView.Level;
		employeeSkill.HourlyWage = employeeSkillView.HourlyWage;
		employeeSkill.YearsOfExperience = employeeSkillView.YearsOfExperience;
		
		//Handle new or existing skill items
		if (employeeSkill.EmployeeSkillID == 0)
		{
			employee.EmployeeSkills.Add(employeeSkill);
		}
		else
		{
			EmployeeSkills.Update(employeeSkill);
		}
		
	}
	
	//adding to database
	if (employee.EmployeeID == 0)
	{
		Employees.Add(employee);
	}

	#endregion

	#region Check for errors and saving of data //  1 Marks

	// Check for the presence of any errors.
	if (errorList.Count() > 0)
	{
		// If errors are present, clear any changes tracked
		ChangeTracker.Clear();
		
		// Throw an aggregate exception containing all errors found during processing.
		throw new AggregateException("Unable to proceed!  Check concerns", errorList);
	}
	else
	{
		// If no errors are present, commit changes to the database.
		SaveChanges();
	}

	#endregion
	return GetEmployee(employee.EmployeeID);
}
#endregion

#region GetEmployeeRegistration Method    //  1 Marks
public EmployeeRegistrationView GetEmployee(int employeeID)
{
	if (employeeID == 0)
	{
		throw new ArgumentNullException("Please provide a invoice id");
	}
	return Employees
			.Where(x => x.EmployeeID == employeeID)
			.Select(x => new EmployeeRegistrationView
			{
				EmployeeID = x.EmployeeID,
				FirstName = x.FirstName,
				LastName = x.LastName,
				HomePhone = x.HomePhone,
				Active = x.Active,
				EmployeeSkills = x.EmployeeSkills
									.Select(es => new EmployeeSkillView
									{
										EmployeeSkillID = es.EmployeeSkillID,
										EmployeeID = es.EmployeeID,
										SkillID = es.SkillID,
										Level = es.Level,
										YearsOfExperience = es.YearsOfExperience,
										HourlyWage = es.HourlyWage.Value
									}).ToList()
			}).FirstOrDefault();
}
#endregion

#endregion

/// <summary> 
/// Contains class definitions that are referenced in the current LINQ file. 
/// </summary> 
/// <remarks> 
/// It's crucial to highlight that in standard development practices, code and class definitions  
/// should not be mixed in the same file. Proper separation of concerns dictates that classes  
/// should have their own dedicated files, promoting modularity and maintainability. 
/// </remarks> 
#region Class/View Model   

public class EmployeeRegistrationView
{
	public int EmployeeID { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string HomePhone { get; set; }
	public bool Active { get; set; }
	public List<EmployeeSkillView> EmployeeSkills { get; set; } = new();
}

public class EmployeeSkillView
{
	public int EmployeeSkillID { get; set; }
	public int EmployeeID { get; set; }
	public int SkillID { get; set; }
	public int Level { get; set; }
	public int? YearsOfExperience { get; set; }
	public decimal HourlyWage { get; set; }
}

#endregion

#region Supporting Methods
/// <summary>
/// Generates a random phone number.
/// The generated phone number ensures the first digit is not 0 or 1.
/// </summary>
/// <returns>A random phone number.</returns>
public static string RandomPhoneNumber()
{
	var random = new Random();
	string phoneNumber = string.Empty;

	// Ensure the first digit isn't 0 or 1.
	int firstDigit = random.Next(2, 10); // Generates a random digit between 2 and 9.
	phoneNumber = $"{firstDigit}";

	// Generate the rest of the digits.
	for (int i = 1; i < 10; i++)
	{
		int currentDigit = random.Next(10);
		phoneNumber = $"{phoneNumber}{currentDigit}";

		// Add periods after every third digit (except for the last period).
		if (i % 3 == 2 && i != 8)
		{
			phoneNumber = $"{phoneNumber}.";
		}
	}

	return phoneNumber;
}

/// <summary>
/// Generates a random name of a given length.
/// The generated name follows a pattern of alternating consonants and vowels.
/// </summary>
/// <param name="len">The desired length of the generated name.</param>
/// <returns>A random name of the specified length.</returns>
public static string GenerateName(int len)
{
	// Create a new Random instance.
	Random r = new Random();

	// Define consonants and vowels to use in the name generation.
	string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
	string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };

	string Name = "";

	// Start the name with an uppercase consonant and a vowel.
	Name += consonants[r.Next(consonants.Length)].ToUpper();
	Name += vowels[r.Next(vowels.Length)];

	// Counter for tracking the number of characters added.
	int b = 2;

	// Add alternating consonants and vowels until we reach the desired length.
	while (b < len)
	{
		Name += consonants[r.Next(consonants.Length)];
		b++;
		Name += vowels[r.Next(vowels.Length)];
		b++;
	}

	return Name;
}
#endregion
