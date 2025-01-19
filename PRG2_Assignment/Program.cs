//features 1, 4, 7 & 8
using PRG2_Assignment

//feature 1
Dictionary<string, Airline> airlinedict = new Dictionary<string, Airline>();
loadairlines(airlinedict);
foreach (KeyValuePair<string, Airline> kvp in airlinedict)
{
    Console.WriteLine(kvp.Key);
    Console.WriteLine(kvp.Value.ToString());
    Console.WriteLine();
}

//methods
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