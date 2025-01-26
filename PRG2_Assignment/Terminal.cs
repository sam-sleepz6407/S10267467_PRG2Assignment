using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using PRG_2_Assignment;
//========================================================== 

// Student Number	: S10267467D

// Student Name	: Chee Hsiao En Samuela (samuela wrote this)

// Partner Name	: Valerie Soh Jia Qi 

//==========================================================

namespace PRG2_Assignment
{
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
}
