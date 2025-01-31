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

namespace PRG2_Assignment
{
    class LWTTFlight : Flight
    {
        public double RequestFee { get; set; }
        public string SpecialRequestCode { get; set; }
        public double CalculateFees()
        {
            double fee = 0;

            if (SpecialRequestCode == "LWTT")
            {
                fee += 500;
            }
            return fee;
        }
        public LWTTFlight(string fn, string ori, string dest, DateTime et, string stat) : base(fn, ori, dest, et, stat) { }
        public override string ToString()
        {
            return $"Flight Number: {FlightNumber}, Origin: {Origin}, Destination: {Destination}, Expected Time: {ExpectedTime:hh:mm tt}, Status: {Status}, Special Request Code: {SpecialRequestCode}";
        }
    }
}
