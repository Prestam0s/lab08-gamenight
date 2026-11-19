/*
* Name: Preston Offutt  
* Course: CSCI 1250, Section 001
* Assignment: Lab 8, Game Night
* Date: November 18, 2026
* Description: Keeps a game night leaderboard in three text files. Shows the
               season so far, records tonight's points, saves the new totals,
               and adds tonight's result to a history that grows every week.
*/
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Security;
using System.Transactions;

const string PLAYERS_FILE = "players.txt";
const string POINTS_FILE = "points.txt";
const string HISTORY_FILE = "history.txt";
const int MAX_POINTS = 20;

// Look for the data files next to Program.cs, however the program is started.
string projectFolder = Path.GetFullPath(
    Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
Directory.SetCurrentDirectory(projectFolder);

//Part 3a: The past games
System.Console.WriteLine("=== GAME NIGHT LEADERBOARD ===\n");
string[] records = File.ReadAllLines(HISTORY_FILE);
int tonightNumber = records.Length + 1;
if(records.Length == 0)
{
    System.Console.WriteLine("No nights played yet.\n");
}
else
{
    System.Console.WriteLine($"Nights played: {records.Length}");
}
foreach(string record in records)
    {
        System.Console.WriteLine($"\n  {record}");
    }
System.Console.WriteLine($"\nTonight is Night {tonightNumber}.\n");

//Part 1: The Standings
string[] players =File.ReadAllLines(PLAYERS_FILE);
string[] points = File.ReadAllLines(POINTS_FILE);

System.Console.WriteLine("PLAYER     POINTS\n-----------------");
for(int i = 0; i < players.Length; i++)
{
    System.Console.WriteLine($"{players[i].PadRight(11)} |{points[i]}"); 
}
System.Console.WriteLine($"\nLeader: {players[LeaderIndex(points)]} with {points[LeaderIndex(points)]} points.");
System.Console.WriteLine($"\nSeason total: {TotalPoints(points)}");

//Part 2: Game Night
string winner = " ";
int winnerPoints = 0;
//Begins the point entry for tonights game
for(int i = 0; i < players.Length; i++)
{
    System.Console.Write($"\nPoints tonight for {players[i]}: ");
    //Enters Program into while loop to verify point entries
    int tonight = -1;
    while(tonight < 0 || tonight > MAX_POINTS)
    {
        tonight = Convert.ToInt32(Console.ReadLine());
        if(tonight < 0 || tonight > MAX_POINTS)
        {
            System.Console.WriteLine("Points must be from 0 to 20");
            System.Console.Write($"Points tonight for {players[i]}: ");
        }
        else
        {
            int season = Convert.ToInt32(points[i]) + tonight;
            points[i] = season.ToString();
            //Tracks index of highest score of the night
            if (tonight > winnerPoints)
            {
                winnerPoints = tonight;
                winner = players[i];
            }
        }
        
    } 
}
System.Console.WriteLine($"\nTonight's winner: {winner} with {winnerPoints}");
File.WriteAllLines(POINTS_FILE, points);

//Part 3: The History

string[] entry = { HistoryEntry(tonightNumber, winner, winnerPoints)};
File.AppendAllLines(HISTORY_FILE, entry);

System.Console.WriteLine("\n\nSaved points.txt");
System.Console.WriteLine($"\n Added to history.txt: {entry}");


static int LeaderIndex(string[] points)
{ 
    return points.IndexOf(points.Max());
}
static int TotalPoints(string[] points)
{
    int total = 0;
    foreach(string point in points)
    {
        total += Convert.ToInt32(point);
    }
    return total;
}
static string HistoryEntry(int tonightNumber, string winner, int winnerPoints)
{
    return $"Night {tonightNumber}: {winner} won with {winnerPoints}";
}