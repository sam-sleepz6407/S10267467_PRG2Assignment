//========================================================== 

// Student Number : S10267467D

// Student Name	: Chee Hsiao En Samuela (feature 1, 4, 7 & 8)

// Partner Name	: Valerie Soh Jia Qi (feature 2, 3, 5, 6, 9)

//==========================================================
using PRG_2_Assignment;
using PRG2_Assignment;

Dictionary<string, Flight> flightDict = new Dictionary<string, Flight>();

//feature 1
Dictionary<string, Airline> airlinedict = new Dictionary<string, Airline>();
Console.WriteLine("Loading Airlines...");
loadairlines(airlinedict);
Console.WriteLine($"{airlinedict.Count} Airlines Loaded!");

Dictionary<string, BoardingGate> boardinggatedict = new Dictionary<string, BoardingGate>();
Console.WriteLine("Loading Boarding Gates...");
loadboardinggates(boardinggatedict);
Console.WriteLine($"{boardinggatedict.Count} Boarding Gates Loaded!");


Console.WriteLine("Loading Flights...");
LoadFlights(flightDict);
Console.WriteLine($"{flightDict.Count} Flights Loaded!");

Console.WriteLine("\n\n\n");
//main
while (true)
{
    displaymainmenu();
    Console.WriteLine("Please select your option: ");
    string option = Console.ReadLine();
    if (option == "1")
    {
        ListFlights(flightDict);
    }
    else if (option == "2")
    {
        displayboardinggate(boardinggatedict);
    }
    else if (option == "3")
    {
        AssBoardingGate(flightDict);
    }
    else if (option == "4")
    {
        CreateNewFlight(flightDict);
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

void LoadFlights(Dictionary<string, Flight> flightDict) //feature 2
{
    try
    {
        using (StreamReader sr = new StreamReader("flights.csv"))
        {
            string line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] data = line.Split(",");
                string fn = data[0];
                string ori = data[1];
                string dest = data[2];
                DateTime et = DateTime.Parse(data[3]);
                string status = data[4];
                string src = data.Length > 5 ? data[5] : null;

                if (flightDict.ContainsKey(fn))
                {
                    Console.WriteLine($"Duplicate flight number {fn} found! Skipping...");
                    continue;
                }

                Flight flight = null;
                if (status.Contains("DDJB"))
                {
                    flight = new DDJBFlight(fn, ori, dest, et, status, src);
                }
                else if (status.Contains("LWTT"))
                {
                    flight = new LWTTFlight(fn, ori, dest, et, status, src);
                }
                else if (status.Contains("CFFT"))
                {
                    flight = new CFFTFlight(fn, ori, dest, et, status, src);
                }
                else
                {
                    flight = new NORMFlight(fn, ori, dest, et, status);
                }

                flightDict.Add(fn, flight);
                foreach (KeyValuePair<string,Airline> kvp in airlinedict)
                {
                    if (fn[0..2] == kvp.Key)
                    {
                        kvp.Value.Flights[fn]= flight;
                    }
                }
            }
        }
    }
    catch (FileNotFoundException ex)
    {
        Console.WriteLine($"Error: File not found - {ex.Message}");
    }
    catch (FormatException ex)
    {
        Console.WriteLine($"Error: Data format issue - {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An unexpected error occurred while loading flights: {ex.Message}");
    }
}

void ListFlights(Dictionary<string, Flight> flightDict) //feature 3
{
    Console.WriteLine("=============================================\r\n" +
        "List of Flights for Changi Airport Terminal 5\r\n=============================================" +
        "\r\nFlight Number  Origin                   Destination              Expected Time     Status\r\n" +
        "-------------------------------------------------------------------------------------------");

    foreach (var flight in flightDict.Values)
    {
        string formattedTime = flight.ExpectedTime.ToString("hh:mm tt");
        Console.WriteLine($"{flight.FlightNumber,-15}{flight.Origin,-25}{flight.Destination,-25}{formattedTime,-18}{flight.Status}");
    }
}

//feature 4 (option 2)
//displayboardinggate(boardinggatedict);

void AssBoardingGate(Dictionary<string, Flight> flightDict) //feature 5
{
    while (true)
    {
        try
        {
            Console.Write("Enter your flight number: ");
            string fn = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(fn))
            {
                throw new ArgumentException("Invalid input. Please enter a valid flight number.");
            }

            if (!flightDict.ContainsKey(fn))
            {
                Console.WriteLine("Flight does not exist. Do you want to add a new flight? [Y/N]");
                string response = Console.ReadLine()?.ToUpper();

                if (response == "Y")
                {
                    CreateNewFlight(flightDict);
                    break;
                }
                else
                {
                    Console.WriteLine("Returning to main menu.");
                    break;
                }
            }

            Flight flight = flightDict[fn];
            Console.WriteLine($"Flight {fn} found! Details:\n{flight}");

            string specialRequestCode = "None";
            if (flight is DDJBFlight ddjbFlight) specialRequestCode = ddjbFlight.SpecialRequestCode ?? "None";
            else if (flight is LWTTFlight lwttFlight) specialRequestCode = lwttFlight.SpecialRequestCode ?? "None";
            else if (flight is CFFTFlight cfftFlight) specialRequestCode = cfftFlight.SpecialRequestCode ?? "None";

            Console.WriteLine($"Special Request Code: {specialRequestCode}");
            Console.Write("Enter the boarding gate: ");
            string bg = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(bg))
            {
                throw new ArgumentException("Invalid input. Boarding gate cannot be empty.");
            }

            if (flightDict.Values.Any(f => f.BoardingGate == bg))
            {
                Console.WriteLine($"Boarding gate {bg} is already assigned to another flight. Please choose a different one.");
                continue;
            }

            flight.BoardingGate = bg;
            Console.WriteLine($"Boarding gate {bg} has been successfully assigned to flight {fn}.");

            Console.Write("Would you like to update the flight status? [Y/N]: ");
            string statusResponse = Console.ReadLine()?.ToUpper();

            if (statusResponse == "Y")
            {
                Console.WriteLine("Enter the new status (Delayed, Boarding, On Time): ");
                string newStatus = Console.ReadLine();
                if (newStatus == "Delayed" || newStatus == "Boarding" || newStatus == "On Time")
                {
                    flight.Status = newStatus;
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

            break;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}

void CreateNewFlight(Dictionary<string, Flight> flightDict) //feature 6
{
    string fn = "", ori = "", dest = "", stat = "On Time", flightType = "";
    DateTime et;

    try
    {
        Console.Write("Enter Flight Number: ");
        fn = Console.ReadLine()?.Trim();
        while (string.IsNullOrEmpty(fn))
        {
            Console.Write("Flight number cannot be empty. Please enter a valid flight number: ");
            fn = Console.ReadLine()?.Trim();
        }

        Console.Write("Enter Origin: ");
        ori = Console.ReadLine()?.Trim();
        while (string.IsNullOrEmpty(ori))
        {
            Console.Write("Origin cannot be empty. Please enter a valid origin: ");
            ori = Console.ReadLine()?.Trim();
        }

        Console.Write("Enter Destination: ");
        dest = Console.ReadLine()?.Trim();
        while (string.IsNullOrEmpty(dest))
        {
            Console.Write("Destination cannot be empty. Please enter a valid destination: ");
            dest = Console.ReadLine()?.Trim();
        }

        Console.Write("Enter Expected Departure Time (yyyy-mm-dd hh:mm): ");
        while (!DateTime.TryParse(Console.ReadLine(), out et))
        {
            Console.Write("Invalid format. Please enter the Expected Departure Time (yyyy-mm-dd hh:mm): ");
        }

        Console.Write("Enter Status (On Time, Delayed, Boarding): ");
        string inputStat = Console.ReadLine()?.Trim();
        if (!string.IsNullOrEmpty(inputStat))
        {
            stat = inputStat;
        }

        Console.Write("Enter Special Request Code (or press Enter to skip): ");
        string src = Console.ReadLine()?.Trim();

        Console.WriteLine("\nSelect the Flight Type:");
        Console.WriteLine("1. DDJBFlight (Special Request Code required)");
        Console.WriteLine("2. LWTTFlight (Special Request Code required)");
        Console.WriteLine("3. CFFTFlight (Special Request Code required)");
        Console.WriteLine("4. NORMFlight (No Special Request Code)");

        Console.Write("\nEnter Flight Type (1, 2, 3, or 4): ");
        flightType = Console.ReadLine()?.Trim();
        while (string.IsNullOrEmpty(flightType) || !"1234".Contains(flightType))
        {
            Console.Write("Invalid flight type. Please enter 1, 2, 3, or 4: ");
            flightType = Console.ReadLine()?.Trim();
        }

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

        if (flightDict.ContainsKey(fn))
        {
            Console.WriteLine($"Flight number {fn} already exists. Please choose a different flight number.");
            return;
        }

        flightDict.Add(newFlight.FlightNumber, newFlight);
        foreach (KeyValuePair<string, Airline> kvp in airlinedict)
        {
            if (fn[0..2] == kvp.Key)
            {
                kvp.Value.Flights[fn] = newFlight;
            }
        }
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
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
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
        Airline searchedairline = searchairline(code);
        Console.WriteLine($"=============================================\n" + $"List of Flights for {searchedairline.Name}\n=============================================");
        ListAirlineFlights(searchedairline);
    }
    else
    {
        Console.WriteLine("Airline not found. ");
    }
    
}

//feature 8 (option 6)
void feature8()
{
    
}

void DisFlightsChron(Dictionary<string, Flight> flightDict) //feature 9
{
    var sortedFlights = flightDict.Values.OrderBy(f => f.ExpectedTime).ToList();
    Console.WriteLine("Flights for the day (ordered by earliest departure time):\n");

    foreach (var flight in sortedFlights)
    {
        Console.WriteLine($"Flight Number: {flight.FlightNumber}");
        Console.WriteLine($"Airline Name: {flight.Airline}");
        Console.WriteLine($"Origin: {flight.Origin}");
        Console.WriteLine($"Destination: {flight.Destination}");
        Console.WriteLine($"Expected Departure Time: {flight.ExpectedTime:yyyy-MM-dd HH:mm}");
        Console.WriteLine($"Status: {flight.Status}");

        if (flight is DDJBFlight ddjb)
        {
            Console.WriteLine($"Special Request Code: {ddjb.SpecialRequestCode}");
        }
        else if (flight is LWTTFlight lwtt)
        {
            Console.WriteLine($"Special Request Code: {lwtt.SpecialRequestCode}");
        }
        else if (flight is CFFTFlight cfft)
        {
            Console.WriteLine($"Special Request Code: {cfft.SpecialRequestCode}");
        }
        else
        {
            Console.WriteLine("Special Request Code: None");
        }

        if (!string.IsNullOrEmpty(flight.BoardingGate))
        {
            Console.WriteLine($"Boarding Gate: {flight.BoardingGate}");
        }
        else
        {
            Console.WriteLine("Boarding Gate: Not assigned");
        }

        Console.WriteLine();
    }
}

void DisTotalFeePerAirline(Dictionary<string, Flight> flightDict, Dictionary<string, BoardingGate> boardinggatedict) //advanced feature (b) by Valerie
{
    foreach (KeyValuePair<string, Flight> kvp in flightDict)
    {
        if (string.IsNullOrEmpty(kvp.Value.BoardingGate))
        {
            Console.WriteLine($"Flight {kvp.Value.FlightNumber} is not assigned a Boarding Gate. Please assign before proceeding.");
            return;
        }
    }

    Dictionary<string, double> airlineFees = new Dictionary<string, double>();

    foreach (KeyValuePair<string, Flight> kvp in flightDict)
    {
        Flight flight = kvp.Value;
        double flightFee = 0;

        if (flight.Origin == "SIN" || flight.Destination == "SIN")
        {
            flightFee += flight.Origin == "SIN" || flight.Destination == "SIN" ? 800 : 500;
        }

        flightFee += 300;

        if (flight.Status == "SpecialRequest")
        {
            flightFee += 50;
        }

        string airlineCode = flight.Airline.Code;
        if (!airlineFees.ContainsKey(airlineCode))
        {
            airlineFees[airlineCode] = 0;
        }

        airlineFees[airlineCode] += flightFee;
    }

    foreach (var airlineFee in airlineFees)
    {
        string airlineCode = airlineFee.Key;
        double subtotalFee = airlineFee.Value;
        double discount = 0;
        double finalFee = subtotalFee - discount;

        Console.WriteLine($"Airline {airlineCode}:");
        Console.WriteLine($"  Subtotal of Fees: ${subtotalFee}");
        Console.WriteLine($"  Discount Applied: ${discount}");
        Console.WriteLine($"  Final Total Fee: ${finalFee}\n");
    }

    double totalFees = airlineFees.Values.Sum();
    Console.WriteLine($"Total Fees for All Airlines: ${totalFees}");
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
void ListAirlineFlights(Airline airline)
{
    Console.WriteLine($"{"Flight Number",-16}{"Airline Name",-23}{"Origin",-23}{"Destination",-23}Expected \nDeparture/Arrival Time");

    foreach (KeyValuePair<string,Flight> kpv in airline.Flights)
    {

        Console.WriteLine($"{kpv.Value.FlightNumber,-16}{airline.Name,-23}{kpv.Value.Origin,-23}{kpv.Value.Destination,-23}{kpv.Value.ExpectedTime}");

    }
}


//void 
