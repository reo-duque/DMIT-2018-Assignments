<Query Kind="Program">
  <Connection>
    <ID>a341a3d1-4d12-4949-93b4-883d855721a2</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <Server>COOKIES-AND-CRE</Server>
    <Database>FSIS_2018</Database>
    <DisplayName>FSIS_2018-Entity</DisplayName>
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
	try
	{
		//YOUR CODE HERE

		//Driver
		//display teams method
		DisplayTeams();

		//create instance of GameStat with test data
		GameStat newGameStat = new GameStat();
		newGameStat.GameDate = DateTime.Today;
		newGameStat.HomeTeamId = 11;
		newGameStat.HomeTeamScore = 2;
		newGameStat.VisitingTeamId = 1;
		newGameStat.VisitingTeamScore = 1;
		
		//Test 1 - team IDs must be different
		//newGameStat.VisitingTeamId = 4;
		
		//Test 2 - Team IDs must exist
		//newGameStat.VisitingTeamId = 11;
		
		//Test 3 - The score is not tied
		//newGameStat.VisitingTeamScore = 2;
		
		//Test 4 - game cannot exist
		//just run it twice
		
		//call Game_RecordGame(xxxxx)
		Game_RecordGame(newGameStat);
		//display teams method
		DisplayTeams();
		//display games method
		DisplayGames();
	}
	catch (ArgumentNullException ex)
	{
		GetInnerException(ex).Message.Dump();
	}
	catch (ArgumentException ex)
	{

		GetInnerException(ex).Message.Dump();
	}
	catch (AggregateException ex)
	{
		//having collected a number of errors
		//	each error should be dumped to a separate line
		foreach (var error in ex.InnerExceptions)
		{
			error.Message.Dump();
		}
	}
	catch (Exception ex)
	{
		GetInnerException(ex).Message.Dump();
	}
}

// You can define other methods, fields, classes and namespaces here

private Exception GetInnerException(Exception ex)
{
	while (ex.InnerException != null)
		ex = ex.InnerException;
	return ex;
}

public class GameStat
{
	public DateTime GameDate { get; set; }
	public int HomeTeamId { get; set; }
	public int HomeTeamScore { get; set; }
	public int VisitingTeamId { get; set; }
	public int VisitingTeamScore { get; set; }
}

public void Game_RecordGame(GameStat game)
{
	//local variables
	Teams homeTeam = new Teams();

	Teams visitingTeam = new Teams();

	Games gameexists = Games
					.Where(x => x.GameDate == game.GameDate && x.HomeTeamID == game.HomeTeamId && x.VisitingTeamID == game.VisitingTeamId)
					.Select(x => x)
					.FirstOrDefault();

	//we need a container to hold x number of Exception messages
	List<Exception> errorlist = new List<Exception>();
	#region Business Rules
	// Team IDs must be different
	if (game.HomeTeamId == game.VisitingTeamId)
	{
		errorlist.Add(new Exception("Team IDs must be different"));
	}
	if (game.HomeTeamId < 3 || game.HomeTeamId > 10)
	{
		errorlist.Add(new Exception("Home Team must exist"));
	}
	else
	{
		homeTeam = Teams
						.Where(x => x.TeamID == game.HomeTeamId)
						.Select(x => x)
						.FirstOrDefault();
	}
	if (game.VisitingTeamId < 3 || game.VisitingTeamId > 10)
	{
		errorlist.Add(new Exception("Visiting Team must exist"));
	}
	else
	{
		visitingTeam = Teams
							.Where(x => x.TeamID == game.VisitingTeamId)
							.Select(x => x)
							.FirstOrDefault();
	}
	if (game.HomeTeamScore == game.VisitingTeamScore)
	{
		errorlist.Add(new Exception("The score cannot be tied"));
	}
	if (game.GameDate > DateTime.Today)
	{
		errorlist.Add(new Exception("The date cannot be a future date"));
	}

	if (gameexists != null)
	{
		errorlist.Add(new Exception("The game already exists"));
	}
	#endregion
	#region Method Code

	if (gameexists == null)
	{
		gameexists = new Games();
	}
	
	//update all entity
	gameexists.GameDate = game.GameDate;
	gameexists.HomeTeamID = game.HomeTeamId;
	gameexists.VisitingTeamID = game.VisitingTeamId;
	gameexists.HomeTeamScore = game.HomeTeamScore;
	gameexists.VisitingTeamScore = game.VisitingTeamScore;
	
	//process the game
	if (gameexists.HomeTeamScore > gameexists.VisitingTeamScore)
	{
		homeTeam.Wins = homeTeam.Wins + 1;
		visitingTeam.Losses = visitingTeam.Losses + 1;
		Teams.Update(homeTeam);
		Teams.Update(visitingTeam);
	}
	else
	{
		visitingTeam.Wins = visitingTeam.Wins + 1;
		homeTeam.Losses = homeTeam.Losses + 1;
		Teams.Update(homeTeam);
		Teams.Update(visitingTeam);
	}
	//adding to database
	if (gameexists.GameID == 0)
	{
		Games.Add(gameexists);
	}
	#endregion
	
	//check for any errors
	if (errorlist.Count() > 0)
	{
		ChangeTracker.Clear();
		
		throw new AggregateException("Unable to proceed! Check concerns", errorlist);
	}
	else
	{
		SaveChanges();	
	}
}

public void DisplayTeams()
{
	Teams
		.Select(x => new {
			TeamId = x.TeamID,
			Name = x.TeamName,
			Wins = x.Wins,
			Losses = x.Losses
		})
		.Dump();
}

public void DisplayGames()
{
	//display games records. display teamname, order games in descending game date
	Games
		.Select(x => new {
			ID = x.GameID,
			Date = x.GameDate,
			HomeTeamID = x.HomeTeamID,
			HomeName = x.Home.TeamName,
			HomeScore = x.HomeTeamScore,
			VisitingTeamID = x.VisitingTeamID,
			VisitingName = x.Visiting.TeamName,
			VisitingScore = x.VisitingTeamScore			
		})
		.OrderByDescending(x => x.Date)
		.Dump();
}