using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using PRG_2_Assignment;
//========================================================== 

// Student Number : S10267467D

// Student Name	: Chee Hsiao En Samuela (samuela wrote this)

// Partner Name	: Valerie Soh Jia Qi 

//==========================================================

namespace PRG2_Assignment
{
    internal class Terminal
    {
        public string TerminalName { get; set; }
        public Dictionary<string, Airline> Airlines { get; set; } = new Dictionary<string, Airline>();
        public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();
        public Dictionary<string, BoardingGate> BoardingGates { get; set; } = new Dictionary<string, BoardingGate>();
        public Dictionary<string, double> GateFees { get; set; } = new Dictionary<string, double>();
        public Terminal(string terminalname)
        {
            TerminalName = terminalname;

        }
        public bool AddAirline(Airline a)
        {
            if (Airlines.ContainsKey(a.Code))
            {
                return false;
            }
            else
            {
                Airlines[a.Code] = a;
                return true;
            }
        }
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
