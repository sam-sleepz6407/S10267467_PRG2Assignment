using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//========================================================== 

// Student Number : S10267467D

// Student Name	: Chee Hsiao En Samuela (samuela wrote this)

// Partner Name	: Valerie Soh Jia Qi 

//==========================================================

namespace PRG2_Assignment
{
    internal class Airline
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Dictionary<string, Flight> Flights { get; set; }
        public Airline(string name, string code)
        {
            Name = name;
            Code = code;
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
            return $"Name: {Name}, Code: {Code}";
        }
    }
}
