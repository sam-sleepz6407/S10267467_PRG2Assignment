//========================================================== 

// Student Number : S10267467D

// Student Name	: Chee Hsiao En Samuela (feature 1, 4, 7 & 8)

// Partner Name	: Valerie Soh Jia Qi (feature 2, 3, 5, 6, 9)

//==========================================================
using Microsoft.VisualBasic;
using PRG_2_Assignment;
using System.Numerics;
using System.Xml.Linq;

Dictionary<string, Flight> flightDict = new Dictionary<string, Flight>();

InitData(flightDict);
LoadFlights(flightDict);

//feature 1
Dictionary<string, Airline> airlinedict = new Dictionary<string, Airline>();
Console.WriteLine("Loading Airlines...");
loadairlines(airlinedict);
Console.WriteLine($"{airlinedict.Count} Airlines Loaded!");

Dictionary<string, BoardingGate> boardinggatedict = new Dictionary<string, BoardingGate>();
Console.WriteLine("Loading Boarding Gates...");
loadboardinggates(boardinggatedict);
Console.WriteLine($"{boardinggatedict.Count} Boarding Gates Loaded!");


Console.WriteLine("\n\n\n");
//main
while (true)
{
    displaymainmenu();
    Console.WriteLine("Please select your option: ");
    string option = Console.ReadLine();
    if (option == "1")
    {

    }
    else if (option == "2")
    {
        displayboardinggate(boardinggatedict);
    }
    else if (option == "3")
    {

    }
    else if (option == "4")
    {

    }
    else if (option == "5")
    {
        feature7();
    }
    else if (option == "6")
    {

    }
    else if (option == "7")
    {

    }
    else if (option == "0") break;
    else
    {
        Console.WriteLine("Invalid option! ");
    }
}

void LoadFlights(Dictionary<string, Flight> flightDict)  //feature 2 
{
    using (StreamReader sr = new StreamReader("flights.csv"))
    {
        string line = sr.ReadLine();
        while ((line = sr.ReadLine()) != null)
        {
            string[] data = line.Split(",");
        }
    }
}

void InitData(Dictionary<string, Flight> flightDict)
{
    flightDict.Add("SQ 693", new NORMFlight("SQ 693", "Tokyo", "Singapore", DateTime.Today.AddHours(10).AddMinutes(30), "On Time"));
    flightDict.Add("MH 722", new NORMFlight("MH 722", "Kuala Lumpur", "Singapore", DateTime.Today.AddHours(11).AddMinutes(00), "Delayed"));
    flightDict.Add("JL 122", new NORMFlight("JL 122", "Tokyo", "Singapore", DateTime.Today.AddHours(12).AddMinutes(15), "On Time"));
    flightDict.Add("CX 312", new NORMFlight("CX 312", "Singapore", "Hong Kong", DateTime.Today.AddHours(13).AddMinutes(00), "Boarding"));
    flightDict.Add("QF 981", new NORMFlight("QF 981", "Singapore", "Sydney", DateTime.Today.AddHours(14).AddMinutes(45), "Delayed"));

    foreach (var flight in flightDict)
    {
        Console.WriteLine(flight.Value);  //feature 3
    }
}

//feature 4 (option 2)
//displayboardinggate(boardinggatedict);


//feature 7 (option 5)
void feature7()
{
    displayairline(airlinedict);
    bool valid = false;
    string code="";
    while (!valid)
    {
        valid = true;
        Console.Write("Enter Airline Code: ");
        code = Console.ReadLine();
        if (code == null)
        {
            valid = false;
            Console.WriteLine("Code must not be empty. ");
        }
        if (code.Length != 2)
        {
            valid = false;
            Console.WriteLine("Length of the code must be 2. ");
        }
        if (!code.All(char.IsLetter))
        {
            valid = false;
            Console.WriteLine("Code must only contain letters. ");
        }
    }
    if (searchairline(code) != null)
    {
        Airline airline = searchairline(code);
    }
    else
    {
        Console.WriteLine("Airline not found. ");
    }
}



//methods
void displaymainmenu()
{
    Console.WriteLine();
    Console.WriteLine("=============================================");
    Console.WriteLine("Welcome to Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("1. List All Flights");
    Console.WriteLine("2. List Boarding Gates");
    Console.WriteLine("3. Assign a Boarding Gate to a Flight");
    Console.WriteLine("4. Create Flight");
    Console.WriteLine("5. Display Airline Flights");
    Console.WriteLine("6. Modify Flight Details");
    Console.WriteLine("7. Display Flight Schedule");
    Console.WriteLine("0. Exit");
    Console.WriteLine("\n");
}

void loadairlines(Dictionary<string, Airline> airlinedict)
{
    using (StreamReader sr = new StreamReader("airlines.csv"))
    {
        string s = sr.ReadLine();
        while ((s = sr.ReadLine()) != null)
        {
            string[] sdeets = s.Split(',');
            string name = sdeets[0];
            string code = sdeets[1];
            Airline airline = new Airline(name, code);
            airlinedict[code] = airline;
        }
    }
}
void loadboardinggates(Dictionary<string, BoardingGate> boardinggatedict)
{
    using (StreamReader sr = new StreamReader("boardinggates.csv"))
    {
        string s = sr.ReadLine();
        while ((s = sr.ReadLine()) != null)
        {
            string[] sdeets = s.Split(',');
            string gate = sdeets[0];
            bool ddjb = Convert.ToBoolean(sdeets[1]);
            bool cfft = Convert.ToBoolean(sdeets[2]);
            bool lwtt = Convert.ToBoolean(sdeets[3]);
            BoardingGate boardinggate = new BoardingGate(gate, ddjb,cfft,lwtt);
            boardinggatedict[gate] = boardinggate;
        }
    }
}

void displayboardinggate(Dictionary<string, BoardingGate> boardinggatedict)
{
    Console.WriteLine("All Boarding Gates in Terminal 5 with its Special Request Codes and Flight Numbers:\n");
    foreach (KeyValuePair<string, BoardingGate> kvp in boardinggatedict)
    {
        Console.Write($"{kvp.Key}\t");
        if (kvp.Value.SupportsCFFT)
        {
            Console.Write("Supports CFFT\t");
        }
        if (kvp.Value.SupportsDDJB)
        {
            Console.Write("Supports DDJB\t");
        }
        if (kvp.Value.SupportsLWTT)
        {
            Console.Write("Supports LWTT\t");
        }
        Console.WriteLine();
    }
}

void displayairline(Dictionary<string, Airline> airlinedict)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Airline Code",-16}{"Airline Name"}");
    foreach (KeyValuePair<string, Airline> kvp in airlinedict)
    {
        Console.WriteLine($"{kvp.Key,-16}{kvp.Value.Name}");
    }
}
Airline? searchairline(string code)
{
    Airline airline;
    foreach (KeyValuePair<string, Airline> kvp in airlinedict)
    {
        if (kvp.Key == code)
        {
            airline = kvp.Value;
            return airline;
        }
    }
    return null;
}
//do for each flight show airline num origin and dest once valerie done w loading flights
