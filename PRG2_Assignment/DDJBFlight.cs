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
    class DDJBFlight : Flight
    {
        public double RequestFee { get; set; }
        public string SpecialRequestCode { get; set; }
        public DDJBFlight(string fn, string ori, string dest, DateTime et, string stat, string src) : base(fn, ori, dest, et, stat)
        {
            SpecialRequestCode = src;
        }
        public double CalculateFees()
        {
            double fee = base.CalculateFees();
            fee += 300;
            if (Origin == "SIN") fee += 800;
            if (Destination == "SIN") fee += 500;
            if (SpecialRequestCode == "DDJB") fee += 300;
            return fee;
        }
        public override string ToString()
        {
            return base.ToString() + $", Special Request Code: {SpecialRequestCode}";
        }
    }
}
