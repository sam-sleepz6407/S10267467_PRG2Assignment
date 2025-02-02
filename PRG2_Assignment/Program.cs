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
        feature8();
    }
    else if (option == "7")
    {
        DisFlightsChron(flightDict);
    }
    else if (option == "8")
    {
        advancedfeaturea();
    }
    else if (option == "9")
    {
        DisTotalFeePerAirline(flightDict, boardinggatedict);
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
                string status = "On Time";
                string? src = data[4];

                if (flightDict.ContainsKey(fn))
                {
                    Console.WriteLine($"Duplicate flight number {fn} found! Skipping...");
                    continue;
                }

                Flight? flight = null;
                if (!string.IsNullOrWhiteSpace(src?.Trim()))
                {
                    if (src.Contains("DDJB"))
                    {
                        flight = new DDJBFlight(fn, ori, dest, et, status, src);
                    }
                    else if (src.Contains("LWTT"))
                    {
                        flight = new LWTTFlight(fn, ori, dest, et, status, src);
                    }
                    else if (src.Contains("CFFT"))
                    {
                        flight = new CFFTFlight(fn, ori, dest, et, status, src);
                    }
                }
                else if (string.IsNullOrWhiteSpace(src?.Trim()))
                {
                    flight = new NORMFlight(fn, ori, dest, et, status);
                }
                //Console.WriteLine(flight.ToString());
                flightDict.Add(fn, flight);
                foreach (KeyValuePair<string, Airline> kvp in airlinedict)
                {
                    if (fn[0..2] == kvp.Key)
                    {
                        flight.Airline = kvp.Value;
                        kvp.Value.Flights[fn] = flight;
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
        if (flight.Status == null)
        {
            string newstat = "no status";
            Console.WriteLine($"{flight.FlightNumber,-15}{flight.Origin,-25}{flight.Destination,-25}{formattedTime,-18}{newstat}");
        }
        else
        {
            Console.WriteLine($"{flight.FlightNumber,-15}{flight.Origin,-25}{flight.Destination,-25}{formattedTime,-18}{flight.Status}");
        }
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
        while (true)
        {
            Console.Write("Enter Flight Number: ");
            fn = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(fn))
            {
                Console.Write("Flight number cannot be empty. Please enter a valid flight number: ");
                fn = Console.ReadLine()?.Trim();
            }
            else if (flightDict.ContainsKey(fn))
            {
                Console.Write($"Flight number {fn} already exists. Please choose a different flight number: ");
            }
            else
            {
                break;
            }
        }

        while (true)
        {
            Console.Write("Enter Origin: ");
            ori = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(ori))
            {
                Console.Write("Origin cannot be empty. Please enter a valid origin: ");
                ori = Console.ReadLine()?.Trim();
            }
            else
            {
                break;
            }
        }

        while (true)
        {
            Console.Write("Enter Destination: ");
            dest = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(dest))
            {
                Console.Write("Destination cannot be empty. Please enter a valid destination: ");
                dest = Console.ReadLine()?.Trim();
            }
            else
            {
                break;
            }
        }

        while (true)
        {
            Console.Write("Enter Expected Departure Time (yyyy-mm-dd hh:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out et))
            {
                Console.Write("Invalid format. Please enter the Expected Departure Time (yyyy-mm-dd hh:mm): ");
            }
            else
            {
                break;
            }
        }

        while (true)
        {
            Console.Write("Enter Status (On Time, Delayed, Boarding): ");
            string inputStat = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(inputStat))
            {
                stat = inputStat;
            }
            else
            {
                break;
            }
        }

        Console.Write("Enter Special Request Code (or press Enter to skip): ");
        string src = Console.ReadLine()?.Trim();

        Flight newFlight = null;
        if (!string.IsNullOrEmpty(src))
        {
            if (src.StartsWith("DDJB"))
            {
                newFlight = new DDJBFlight(fn, ori, dest, et, stat, src);
            }
            else if (src.StartsWith("LWTT"))
            {
                newFlight = new LWTTFlight(fn, ori, dest, et, stat, src);
            }
            else if (src.StartsWith("CFFT"))
            {
                newFlight = new CFFTFlight(fn, ori, dest, et, stat, src);
            }
            else
            {
                newFlight = new NORMFlight(fn, ori, dest, et, stat);
            }
        }
        else
        {
            newFlight = new NORMFlight(fn, ori, dest, et, stat); //default
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
        Console.WriteLine("Flight details have been saved!");
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
    //Console.WriteLine("here");
    displayingflightsandairlines();
    //Console.WriteLine("here");
    bool valid = false;
    string flightnum = "";
    while (!valid)
    {
        valid = true;
        Console.WriteLine("Choose an existing Flight to modify or delete:");
        flightnum = Console.ReadLine();
        if (flightnum == null)
        {
            valid = false;
            Console.WriteLine("Flight number must not be empty. Please try again: ");
        }
    }

    Flight? flightfound = searchflights(flightnum);
    if (flightfound != null)
    {
        //Console.WriteLine($"not null {flightfound.Airline.Name}");

        Console.WriteLine($"1. Modify Flight\n2. Delete Flight\nChoose an option:");
        bool validchoice = false;
        string? choice = "";
        while (!validchoice)
        {
            validchoice = true;
            choice = Console.ReadLine();
            if (!(choice == "1" | choice == "2"))
            {
                valid = false;
                Console.Write("Invalid option! Please try again: ");
            }
        }
        if (choice == "1")
        {
            Console.WriteLine("1. Modify Basic Information\n2. Modify Status\n3. Modify Special Request Code\n4. Modify Boarding Gate\nChoose an option:");
            bool optionvalid = false;
            string? option = "";
            while (!optionvalid)
            {
                optionvalid = true;
                option = Console.ReadLine();
                if (!(option == "1" | option == "2" | option == "3" | option == "4"))
                {
                    optionvalid = false;
                    Console.WriteLine("Invalid option! Please try again: ");
                }
            }
            if (option == "1")
            {
                Console.Write("Enter new Origin: ");
                string? neworigin = Console.ReadLine();
                while (neworigin == null)
                {
                    Console.WriteLine("New Origin cannot be blank, please enter again:");
                    neworigin = Console.ReadLine();
                }
                Console.Write("Enter new Destination: ");
                string? newdest = Console.ReadLine();
                while (newdest == null)
                {
                    Console.WriteLine("New Destination cannot be blank, please enter again: ");
                    newdest = Console.ReadLine();
                }
                Console.Write("Enter new Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
                DateTime newexpected = DateTime.Now; //placeholder
                bool datevalid = false;
                while (!datevalid)
                {
                    datevalid = true;
                    try
                    {
                        newexpected = DateTime.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("The date is invalid, try again:");
                        datevalid = false;
                    }
                }
                flightfound.Origin = neworigin;
                flightfound.Destination = newdest;
                flightfound.ExpectedTime = newexpected;
                Console.WriteLine("Flight updated!");
                Console.WriteLine($"Flight Number: {flightnum}\nAirline Name: {flightfound.Airline.Name}\nOrigin: {flightfound.Origin}\nDestination: {flightfound.Destination}\nExpected Departure/Arrival Time: {flightfound.ExpectedTime}");
                Console.WriteLine($"Status: {flightfound.Status}");
                Type type = flightfound.GetType();
                //Console.WriteLine(type);
                if (type == typeof(PRG_2_Assignment.CFFTFlight))
                {
                    CFFTFlight flightfoundbytype = (CFFTFlight)flightfound;
                    Console.WriteLine($"Special Request Code: {flightfoundbytype.SpecialRequestCode}");
                }
                else if (type == typeof(PRG_2_Assignment.LWTTFlight))
                {
                    LWTTFlight flightfoundbytype = (LWTTFlight)flightfound;
                    Console.WriteLine($"Special Request Code: {flightfoundbytype.SpecialRequestCode}");
                }
                else if (type == typeof(PRG_2_Assignment.DDJBFlight))
                {
                    DDJBFlight flightfoundbytype = (DDJBFlight)flightfound;
                    Console.WriteLine($"Special Request Code: {flightfoundbytype.SpecialRequestCode}");
                }
                Console.WriteLine($"Boarding Gate: {flightfound.BoardingGate}");
            }
            else if (option == "2")
            {
                Console.WriteLine("1. Delayed\n2. Boarding\n3. On Time\nPlease select the new status of the flight:");
                bool statusvalid = false;
                string? status = null;
                while (!statusvalid)
                {
                    statusvalid = true;
                    status = Console.ReadLine();
                    if (!(status == "1" | status == "2" | status == "3"))
                    {
                        statusvalid = false;
                        Console.WriteLine("Invalid option. Please select the new status of the flight:");
                    }
                }
                string? actualstatus = null;
                if (status == "1")
                {
                    actualstatus = "Delayed";
                }
                else if (status == "2")
                {
                    actualstatus = "Boarding";
                }
                else
                {
                    actualstatus = "On Time";
                }
                flightfound.Status = actualstatus;
            }
            else if (option == "3")
            {
                Console.WriteLine("1. LWTT\n2. CFFT\n3. DDJB\n4. No Special Request Code");
                Console.WriteLine("Please select the option of the new Special Request Code of the flight:");
                bool srcvalid = false;
                string? src = null;
                while (!srcvalid)
                {
                    srcvalid = true;
                    src = Console.ReadLine();
                    if (!(src == "1" | src == "2" | src == "3" | src == "4"))
                    {
                        srcvalid = false;
                        Console.WriteLine("Invalid option. Please select the option of the new Special Request Code of the flight:");
                    }
                }
                if (src == "1")
                {
                    Flight newsrc = new LWTTFlight(flightfound.FlightNumber, flightfound.Origin, flightfound.Destination, flightfound.ExpectedTime, flightfound.Status, src);
                    newsrc.BoardingGate = flightfound.BoardingGate;
                    newsrc.Airline = flightfound.Airline;
                    flightDict.Remove(flightfound.FlightNumber);
                    flightDict[newsrc.FlightNumber] = newsrc;
                    newsrc.Airline.Flights.Remove(flightfound.FlightNumber);
                    newsrc.Airline.Flights[newsrc.FlightNumber] = newsrc;
                }
                else if (src == "2")
                {
                    Flight newsrc = new CFFTFlight(flightfound.FlightNumber, flightfound.Origin, flightfound.Destination, flightfound.ExpectedTime, flightfound.Status, src);
                    newsrc.BoardingGate = flightfound.BoardingGate;
                    newsrc.Airline = flightfound.Airline;
                    flightDict.Remove(flightfound.FlightNumber);
                    flightDict[newsrc.FlightNumber] = newsrc;
                    newsrc.Airline.Flights.Remove(flightfound.FlightNumber);
                    newsrc.Airline.Flights[newsrc.FlightNumber] = newsrc;
                }
                else if (src == "3")
                {
                    Flight newsrc = new DDJBFlight(flightfound.FlightNumber, flightfound.Origin, flightfound.Destination, flightfound.ExpectedTime, flightfound.Status, src);
                    newsrc.BoardingGate = flightfound.BoardingGate;
                    newsrc.Airline = flightfound.Airline;
                    flightDict.Remove(flightfound.FlightNumber);
                    flightDict[newsrc.FlightNumber] = newsrc;
                    newsrc.Airline.Flights.Remove(flightfound.FlightNumber);
                    newsrc.Airline.Flights[newsrc.FlightNumber] = newsrc;
                }
                else if (src == "4")
                {
                    Flight newsrc = new NORMFlight(flightfound.FlightNumber, flightfound.Origin, flightfound.Destination, flightfound.ExpectedTime, flightfound.Status);
                    newsrc.BoardingGate = flightfound.BoardingGate;
                    newsrc.Airline = flightfound.Airline;
                    flightDict.Remove(flightfound.FlightNumber);
                    flightDict[newsrc.FlightNumber] = newsrc;
                    newsrc.Airline.Flights.Remove(flightfound.FlightNumber);
                    newsrc.Airline.Flights[newsrc.FlightNumber] = newsrc;
                }
            }
            else if (option == "4")
            {
                Console.WriteLine("Enter the Boarding Gate to be changed into: ");
                string? gatename = null;
                bool gatevalid = false;
                while (!gatevalid)
                {
                    gatevalid = true;
                    gatename = Console.ReadLine();
                    if (!boardinggatedict.ContainsKey(gatename))
                    {
                        gatevalid = false;
                        Console.WriteLine("Invalid Boarding Gate. Enter the Boarding Gate to be changed into: ");
                    }
                    if (boardinggatedict[gatename].flight != null)
                    {
                        gatevalid = false;
                        Console.WriteLine("Gate already in use. Enter the Boarding Gate to be changed into: ");
                    }
                }
                Console.WriteLine($"Boarding gate has been changed to {gatename}");
                flightfound.BoardingGate = gatename;
                boardinggatedict[gatename].flight = flightfound;

            }
        }
        //option2 to delete
        else
        {
            Console.WriteLine($"Are you sure you want to delete flight {flightnum}? [Y/N]");
            string? sure = null;
            bool surevalid = false;
            while (!surevalid)
            {
                surevalid = true;
                sure = Console.ReadLine().ToUpper();
                if (!(sure == "Y" | sure == "N"))
                {
                    surevalid = false;
                    Console.WriteLine($"Invalid option. Are you sure you want to delete flight {flightnum}? [Y/N]");
                }
            }
            if (sure == "Y")
            {
                flightDict.Remove(flightfound.FlightNumber);
                flightfound.Airline.Flights.Remove(flightfound.FlightNumber);
                Console.WriteLine("Flight has been removed. ");
            }
            else
            {
                Console.WriteLine("Returning to Main Menu. ");
            }
        }
    }
    else
    {
        Console.WriteLine("Flight not found. ");
    }

}

void DisFlightsChron(Dictionary<string, Flight> flightDict) //feature 9 
{
    var sortedFlights = flightDict.Values.OrderBy(f => f.ExpectedTime).ToList();

    Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------");

    Console.WriteLine("{0,-15} {1,-16} {2,-18} {3,-20} {4,-35} {5,-12} {6,-12} {7,-20}",
        "FlightNumber", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time", "Status", "BoardingGate", "Special Request Code");

    Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------");

    foreach (var flight in sortedFlights)
    {
        string boardingGate = string.IsNullOrEmpty(flight.BoardingGate) ? "Unassigned" : flight.BoardingGate;
        string specialRequestCode = flight is DDJBFlight ? "DDJB" :
                                     flight is LWTTFlight ? "LWTT" :
                                     flight is CFFTFlight ? "CFFT" : "NORM";

        Console.WriteLine("{0,-15} {1,-16} {2,-18} {3,-20} {4,-35} {5,-12} {6,-12} {7,-20}",
            flight.FlightNumber, flight.Airline, flight.Origin, flight.Destination, flight.ExpectedTime.ToString("HH:mm"), flight.Status, boardingGate, specialRequestCode);
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
    Console.WriteLine("8. Process all unassigned Flights to Boarding Gates ");
    Console.WriteLine("9. Display the total fee per airline for the day");
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


void displayingflightsandairlines()
{
    displayairline(airlinedict);
    bool valid = false;
    string code = "";
    while (!valid)
    {
        valid = true;
        Console.WriteLine("Enter Airline Code: ");
        code = Console.ReadLine();
        if (code == null)
        {
            valid = false;
            Console.Write("Code must not be empty. ");
        }
        if (code.Length != 2)
        {
            valid = false;
            Console.Write("Length of the code must be 2. ");
        }
        if (!code.All(char.IsLetter))
        {
            valid = false;
            Console.Write("Code must only contain letters. ");
        }
        if (!valid)
        {
            Console.WriteLine("Please try again: ");
        }
    }
    if (searchairline(code) != null)
    {
        Airline searchedairline = searchairline(code);
        Console.WriteLine($"List of Flights for {searchedairline.Name}");
        ListAirlineFlights(searchedairline);
    }
    else
    {
        Console.WriteLine("Airline not found. ");
    }
}
Flight? searchflights(string flightnum)
{
    foreach (KeyValuePair<string, Flight> kvp in flightDict)
    {
        if (flightnum == kvp.Key)
        {
            return kvp.Value;
        }
    }
    return null;
}

//advanced feature
void advancedfeaturea()
{
    int assigned = 0;
    Queue<Flight> unassignedflights = new Queue<Flight>();
    foreach (KeyValuePair<string, Flight> kvp in flightDict)
    {
        if (kvp.Value.BoardingGate == null)
        {
            unassignedflights.Enqueue(kvp.Value);
        }
        else
        {
            assigned++;
        }
    }
    Console.WriteLine($"Total number of flights that have not yet been assigned a Boarding Gate: {unassignedflights.Count}");
    List<BoardingGate> unassignedboardinggates = new List<BoardingGate>();
    foreach (KeyValuePair<string, BoardingGate> kvp in boardinggatedict)
    {
        if (kvp.Value.flight == null)
        {
            unassignedboardinggates.Add(kvp.Value);
        }
        else
        {
            assigned++;
        }
    }
    int processed = 0;
    Console.WriteLine($"Total number of boarding gates that have not yet been assigned a flight: {unassignedboardinggates.Count}");
    List<BoardingGate> gatesToRemove = new List<BoardingGate>();
    foreach (Flight flight in unassignedflights)
    {
        if (flight.GetType() == typeof(PRG_2_Assignment.CFFTFlight))
        {
            foreach (BoardingGate gate in unassignedboardinggates)
            {
                if (gate.SupportsCFFT)
                {
                    gate.flight = flight;
                    flight.BoardingGate = gate.GateName;
                    gatesToRemove.Add(gate);
                    processed += 2;
                    break;
                }
            }
        }
        if (flight.GetType() == typeof(PRG_2_Assignment.LWTTFlight))
        {
            foreach (BoardingGate gate in unassignedboardinggates)
            {
                if (gate.SupportsLWTT)
                {
                    gate.flight = flight;
                    flight.BoardingGate = gate.GateName;
                    gatesToRemove.Add(gate);
                    processed += 2;
                    break;
                }
            }
        }
        if (flight.GetType() == typeof(PRG_2_Assignment.DDJBFlight))
        {
            foreach (BoardingGate gate in unassignedboardinggates)
            {
                if (gate.SupportsDDJB)
                {
                    gate.flight = flight;
                    flight.BoardingGate = gate.GateName;
                    gatesToRemove.Add(gate);
                    processed += 2;
                    break;
                }
            }
        }
        if (flight.GetType() == typeof(PRG_2_Assignment.NORMFlight))
        {
            foreach (BoardingGate gate in unassignedboardinggates)
            {
                if (!(gate.SupportsDDJB | gate.SupportsCFFT | gate.SupportsLWTT))
                {
                    gate.flight = flight;
                    flight.BoardingGate = gate.GateName;
                    gatesToRemove.Add(gate);
                    processed += 2;
                    break;
                }
            }
        }
    }
    foreach (BoardingGate gate in gatesToRemove)
    {
        unassignedboardinggates.Remove(gate);
    }
    Console.WriteLine($"Total number of flights and boarding gates processed and assigned: {processed}");
    if (assigned == 0)
    {
        Console.WriteLine($"Percentage of flights and boarding gates processed automatically over those that were already assigned: 100%");
    }
    else
    {
        Console.WriteLine($"Percentage of flights and boarding gates processed automatically over those that were already assigned: {(processed / assigned) * 100}%");
    }
}
