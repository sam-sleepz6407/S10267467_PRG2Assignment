using System;
using System.Collections.Generic;
using System.Linq;
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
    internal class BoardingGate
    {
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight flight { get; set; }
        public BoardingGate(string gatename, bool supportcfft, bool supportDDJB, bool supportlwtt)
        {
            GateName = gatename;
            SupportsCFFT = supportcfft;
            SupportsDDJB = supportDDJB;
            SupportsLWTT = supportlwtt;
        }
        //public double CalculateFees()
        //{

        //}
        public override string ToString()
        {
            return $"Gate name: {GateName}, Supports CFFT: {SupportsCFFT}, Supports DDJB: {SupportsDDJB}, Supports LWTT: {SupportsLWTT}";
        }
    }
}
