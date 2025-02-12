//========================================================== 

// Student Number : S10267467D

// Student Name	: Chee Hsiao En Samuela

// Partner Name	: Valerie Soh Jia Qi (Valerie wrote this code)

//========================================================== 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG_2_Assignment
{
    class NORMFlight : Flight
    {
        public double CalculateFees()
        {
            double fees = 0;
            if (Destination == "Singapore")
            {
                fees = 500;
            }
            else if (Origin == "Singapore")
            {
                fees = 800;
            }
            return fees;
        }
        public NORMFlight(string fn, string ori, string dest, DateTime et, string stat) : base(fn, ori, dest, et, stat) { }
        public override string ToString()
        {
            return $"Flight Number: {FlightNumber}, Origin: {Origin}, Destination: {Destination}, Expected Time: {ExpectedTime:hh:mm tt}, Status: {Status}";
        }
    }
}
