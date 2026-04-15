using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicMath
{
    public class BasicMaths
    {
        public double Add(double num1, double num2)
        {
            return num1 + num2;
        }
        public double Subtract(double num1, double num2)
        {
            return num1 - num2;
        }
        public double Divide(double num1, double num2)
        {
            if (num2 == 0)
                throw new DivideByZeroException("Division byzero is not allowed.");
            return num1 / num2;
        }
        public double Multiply(double num1, double num2)
        {
            return num1 * num2;
        }
    }


}

