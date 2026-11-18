/*
* Name: Preston Offutt  
* Course: CSCI 1250, Section 001
* Assignment: Lab 8, Game Night
* Date: October 21, 2026
* Description: A Program that tracks the scores of players over a season. Recording the history of each
               nights winner and the total points of the players.
*/
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Security;
using System.Transactions;

const string PLAYERS_FILE = "players.txt";
const string POINTS_FILE = "points.txt";
///const string HISTORY_FILE = "history.txt";
//const int MAX_POINTS = 20;

// Look for the data files next to Program.cs, however the program is started.
string projectFolder = Path.GetFullPath(
    Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
Directory.SetCurrentDirectory(projectFolder);

//Part 1: The Standings
string[] players =File.ReadAllLines(PLAYERS_FILE);
string[] points = File.ReadAllLines(POINTS_FILE);

System.Console.WriteLine("PLAYER     POINTS");
for(int i = 0; i < players.Length; i++)
{
    System.Console.WriteLine($"{players[i].PadRight(11)}  {points[i]}"); 
}
System.Console.WriteLine($"Leader: {players[LeaderIndex(points)]} with {points[LeaderIndex(points)]} points.");
System.Console.WriteLine($"Season total: {TotalPoints(points)}");

//Part 2: Game Night
for(int i = 0; i < players.Length; i++)
{
    System.Console.WriteLine($"Points tonight for {players[i]}: ");
    int tonight = -1;
    while(tonight < 0 || tonight > 20)
    {
        tonight = Convert.ToInt32(Console.ReadLine());
        if(tonight < 0 || tonight > 20)
        {
            System.Console.WriteLine("Points must be from 0 to 20");
        }
        else
        {
            int season = Convert.ToInt32(points[i]) + tonight;
            points[i] = season.ToString
        }
    } 
}
File.WriteAllLines(POINTS_FILE, points);










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
// static string HistoryEntry(int night, string winner, int points)
// {}