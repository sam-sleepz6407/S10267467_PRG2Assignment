//========================================================== 

// Student Number : S10267467D

// Student Name	: Chee Hsiao En Samuela (feature 1, 4, 7 & 8)

// Partner Name	: Valerie Soh Jia Qi (feature 2, 3, 5, 6, 9)

//==========================================================
using PRG_2_Assignment;

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

void AssBoardingGate(Dictionary<string, Flight> flightDict)  //feature 5
{
    while (true)
    {
        Console.Write("Enter your flight number: ");
        string fn = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(fn))
        {
            Console.WriteLine("Invalid input. Please enter a valid flight number.");
            continue;
        }

        if (!flightDict.ContainsKey(fn))
        {
            Console.WriteLine("Flight does not exist. Do you want to add a new flight? [Y/N]");
            string response = Console.ReadLine()?.ToUpper();
            if (response != "Y")
            {
                Console.WriteLine("Returning to main menu.");
                break;
            }
            else if (response == "N")
            {
                continue;
            }
        }

        Flight flight = flightDict[fn];
        Console.WriteLine($"Flight {fn} found! Details:\n{flight}");

        string specialRequestCode = "None";
        if (flight is DDJBFlight ddjbFlight)
        {
            specialRequestCode = ddjbFlight.SpecialRequestCode ?? "None";
        }
        else if (flight is LWTTFlight lwttFlight)
        {
            specialRequestCode = lwttFlight.SpecialRequestCode ?? "None";
        }
        else if (flight is CFFTFlight cfftFlight)
        {
            specialRequestCode = cfftFlight.SpecialRequestCode ?? "None";
        }

        Console.WriteLine($"Special Request Code: {specialRequestCode}");

        Console.Write("Enter the boarding gate: ");
        string bg = Console.ReadLine();

        if (flightDict.Values.Any(f => f.BoardingGate == bg))
        {
            Console.WriteLine($"Boarding gate {bg} is already assigned to another flight. Please choose a different one.");
            continue;
        }

        flight.BoardingGate = bg;

        Console.WriteLine($"Boarding gate {bg} has been successfully assigned to flight {fn}.");
        Console.WriteLine($"Flight Details: \nFlight Number: {fn}\nBoarding Gate: {bg}\nSpecial Request Code: {specialRequestCode}");

        Console.Write("Would you like to update the flight status? [Y/N]: ");
        string statusResponse = Console.ReadLine()?.ToUpper();

        if (statusResponse == "Y")
        {
            Console.WriteLine("Enter the new status (Delayed, Boarding, On Time): ");
            string newStatus = Console.ReadLine();

            if (newStatus == "Delayed" || newStatus == "Boarding" || newStatus == "On Time")
            {
                flight.Status = newStatus;
                Console.WriteLine($"Flight status updated to {newStatus}.");
            }
            else
            {
                Console.WriteLine("Invalid status. Setting to default status 'On Time'.");
                flight.Status = "On Time";
            }
        }
        else
        {
            flight.Status = "On Time";
            Console.WriteLine("Flight status set to default: 'On Time'.");
        }

        Console.WriteLine($"Boarding gate assignment complete for flight {fn}.\n");
        break;
    }
}

void CreateNewFlight(Dictionary<string, Flight> flightDict) //feature 6
{
    Console.Write("Enter Flight Number: ");
    string fn = Console.ReadLine()?.Trim();

    Console.Write("Enter Origin: ");
    string ori = Console.ReadLine()?.Trim();

    Console.Write("Enter Destination: ");
    string dest = Console.ReadLine()?.Trim();

    Console.Write("Enter Expected Departure Time (yyyy-mm-dd hh:mm): ");
    DateTime et;
    while (!DateTime.TryParse(Console.ReadLine(), out et))
    {
        Console.Write("Invalid format. Please enter the Expected Departure Time (yyyy-mm-dd hh:mm): ");
    }

    Console.Write("Enter Status (On Time, Delayed, Boarding): ");
    string stat = Console.ReadLine()?.Trim() ?? "On Time";

    string src = null;
    Console.Write("Enter Special Request Code (or press Enter to skip): ");
    src = Console.ReadLine()?.Trim();

    Console.WriteLine("\nSelect the Flight Type:");
    Console.WriteLine("1. DDJBFlight (Special Request Code required)");
    Console.WriteLine("2. LWTTFlight (Special Request Code required)");
    Console.WriteLine("3. CFFTFlight (Special Request Code required)");
    Console.WriteLine("4. NORMFlight (No Special Request Code)");

    Console.Write("\nEnter Flight Type (1, 2, 3, or 4): ");
    string flightType = Console.ReadLine()?.Trim();

    Flight newFlight = null;

    if (flightType == "1")
    {
        newFlight = new DDJBFlight(fn, ori, dest, et, stat, src);
    }
    else if (flightType == "2")
    {
        newFlight = new LWTTFlight(fn, ori, dest, et, stat, src);
    }
    else if (flightType == "3")
    {
        newFlight = new CFFTFlight(fn, ori, dest, et, stat, src);
    }
    else if (flightType == "4")
    {
        newFlight = new NORMFlight(fn, ori, dest, et, stat);
    }
    else
    {
        Console.WriteLine("Invalid flight type entered. Please try again.");
        return;
    }

    if (newFlight != null)
    {
        if (flightDict.ContainsKey(fn))
        {
            Console.WriteLine($"Flight number {fn} already exists. Please choose a different flight number.");
            return;
        }

        flightDict.Add(newFlight.FlightNumber, newFlight);
        Console.WriteLine("New flight has been successfully added!");

        using (StreamWriter sw = new StreamWriter("flights.csv", true))
        {
            if (newFlight is DDJBFlight ddjb)
            {
                sw.WriteLine($"{newFlight.FlightNumber},{newFlight.Origin},{newFlight.Destination},{newFlight.ExpectedTime:yyyy-MM-dd HH:mm},{newFlight.Status},{ddjb.SpecialRequestCode}");
            }
            else if (newFlight is LWTTFlight lwtt)
            {
                sw.WriteLine($"{newFlight.FlightNumber},{newFlight.Origin},{newFlight.Destination},{newFlight.ExpectedTime:yyyy-MM-dd HH:mm},{newFlight.Status},{lwtt.SpecialRequestCode}");
            }
            else if (newFlight is CFFTFlight cfft)
            {
                sw.WriteLine($"{newFlight.FlightNumber},{newFlight.Origin},{newFlight.Destination},{newFlight.ExpectedTime:yyyy-MM-dd HH:mm},{newFlight.Status},{cfft.SpecialRequestCode}");
            }
            else if (newFlight is NORMFlight norm)
            {
                sw.WriteLine($"{newFlight.FlightNumber},{newFlight.Origin},{newFlight.Destination},{newFlight.ExpectedTime:yyyy-MM-dd HH:mm},{newFlight.Status}");
            }
        }
        Console.WriteLine("Flight details have been saved to flights.csv!");
    }
}

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
