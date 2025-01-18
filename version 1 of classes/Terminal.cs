using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace version_1_of_classes
{
    internal class Airline
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Dictionary<string, Flight> Flights { get; set; }
        public Airline(string name, string code, Dictionary<string, Flight> flights)
        {
            Name = name;
            Code = code;
            Flights = flights;
        }
        //public bool AddFlight(Flight f)
        //{

        //}
        //public bool CalculateFees()
        //{

        //}
        //public bool RemoveFlight(Flight f)
        //{

        //}
        public override string ToString()
        {
            return $"Name: {Name}, Code: {Code}, Flights: {Flights}";
        }
    }
    internal class Terminal
    {
        public string TerminalName { get; set; }
        public Dictionary<string, Airline> Airlines { get; set; }
        public Dictionary<string, Flight> Flights { get; set; }
        public Dictionary<string, BoardingGate> BoardingGates { get; set; }
        public Dictionary<string, double> GateFees { get; set; }
        public Terminal(string terminalname, Dictionary<string, Airline> airlines, Dictionary<string, Flight> flights, Dictionary<string, BoardingGate> boardinggates, Dictionary<string, double> gatefees)
        {
            TerminalName = terminalname;
            Airlines = airlines;
            Flights = flights;
            BoardingGates = boardinggates;
            GateFees = gatefees;
        }
        //public bool AddAirline(Airline a)
        //{

        //}
        //public bool AddBoardingGate(BoardingGate b)
        //{

        //}
        //public Airline GetAirlineFromFlight(Flight)
        //{

        //}
        //public void PrintAirlineFees()
        //{

        //}
        public override string ToString()
        {
            return $"Terminal name: {TerminalName}, Airlines: {Airlines}, Flights: {Flights}, Boarding Gates: {BoardingGates}, Gate Fees: {GateFees}";
        }
    }
    internal class BoardingGate
    {
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flight { get; set; }
        public BoardingGate(string gatename, bool supportcfft, bool supportDDJB, bool supportlwtt, Flight flight)
        {
            GateName = gatename;
            SupportsCFFT = supportcfft;
            SupportsDDJB = supportDDJB;
            SupportsLWTT = supportlwtt;
            Flight = flight;
        }
        //public double CalculateFees()
        //{

        //}
        public override string ToString()
        {
            return $"Gate name: {GateName}, Supports CFFT: {SupportsCFFT}, Supports DDJB: {SupportsDDJB}, Supports LWTT: {SupportsLWTT}, Flight: {Flight}";
        }
    }
    abstract class Flight
    {
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }
        public abstract double CalculateFees();
        public override string ToString()
        {
            return $"Flight number: {FlightNumber}, Origin: {Origin}, Destination: {Destination}, Expected Time: {ExpectedTime}, Status: {Status}";
        }
    }
}
