//========================================================== 

// Student Number : S10267467D

// Student Name	: Chee Hsiao En Samuela

// Partner Name	: Valerie Soh Jia Qi

//==========================================================
//features 1, 4, 7 & 8
using PRG2_Assignment;

//feature 1
Dictionary<string, Airline> airlinedict = new Dictionary<string, Airline>();
loadairlines(airlinedict);
//foreach (KeyValuePair<string, Airline> kvp in airlinedict)
//{
//    Console.WriteLine(kvp.Key);
//    Console.WriteLine(kvp.Value.ToString());
//    Console.WriteLine();
//}
Dictionary<string, BoardingGate> boardinggatedict = new Dictionary<string, BoardingGate>();
loadboardinggates(boardinggatedict);
//foreach (KeyValuePair<string, BoardingGate> kvp in boardinggatedict)
//{
//    Console.WriteLine(kvp.Key);
//    Console.WriteLine(kvp.Value.ToString());
//    Console.WriteLine();
//}

//main
while (true)
{
    displaymainmenu();
    Console.Write("Your choice: ");
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

    }
    else if (option == "6")
    {

    }
    else if (option == "0") break;
    else
    {
        Console.WriteLine("Invalid option! ");
    }
}



//feature 4 (option 2)
//displayboardinggate(boardinggatedict);


//feature 7 (option 5)
//displayairline(airlinedict);



//methods
void displaymainmenu()
{
    Console.WriteLine();
    Console.WriteLine("-------------------------------------------------------");
    Console.WriteLine("MAIN MENU");
    Console.WriteLine("[1] List all flights with their basic information");
    Console.WriteLine("[2] List all boarding gates");
    Console.WriteLine("[3] Assign a boarding gate to a flight");
    Console.WriteLine("[4] Create a new flight");
    Console.WriteLine("[5] Display full flight details from an airline");
    Console.WriteLine("[6] Modify flight details");
    Console.WriteLine("[0] Exit");
    Console.WriteLine("-------------------------------------------------------");
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
            airlinedict[name] = airline;
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
    foreach (KeyValuePair<string, Airline> kvp in airlinedict)
    {
        Console.WriteLine(kvp.Key);
        Console.WriteLine(kvp.Value.ToString());
        Console.WriteLine();
    }
}
void searchairline()
{

}