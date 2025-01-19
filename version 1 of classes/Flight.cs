//========================================================== 

// Student Number	:

// Student Name	: 

// Partner Name	: Valerie Soh Jia Qi (Valerie wrote this code)

//==========================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG_2_Assignment
{
    abstract class Flight
    {
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }
        public Flight(string fn, string ori, string dest, DateTime et, string stat) 
        {
            FlightNumber = fn;
            Origin = ori;
            Destination = dest;
            ExpectedTime = et;
            Status = stat;
        }
        public double CalculateFees()
        {
            double fees = 0;
            return fees;
        }
        public override string ToString()
        {
            return $"Flight Number: {FlightNumber}, Origin: {Origin}, Destination: {Destination}, Expected Time: {ExpectedTime}, Status: {Status}";
        }
    }
}
