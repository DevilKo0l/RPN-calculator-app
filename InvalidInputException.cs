using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_4_Trung_Nguyen
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException(){ }
        public InvalidInputException(string messenge) : base(string.Format("Wrong equation: {0}",messenge))
        {
        }
    }
}
