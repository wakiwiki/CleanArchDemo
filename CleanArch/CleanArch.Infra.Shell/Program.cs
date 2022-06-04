using CleanArch.Infra.Shell;

Console.WriteLine(ConnectionHelper.GetDbConnection("UniversityIdentityDBConnection"));
Console.WriteLine(ConnectionHelper.GetDbConnection("UniversityDBConnection"));