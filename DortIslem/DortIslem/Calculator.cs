﻿




namespace DortIslem
{
    public class Calculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }

        public int Divide(int a, int b)
        {
            if(b==0)
            {
                throw new DivideByZeroException("Devider can not be zero");
            }
            return a / b;
        }

        public int Multiply(int a, int b)
        {
            return a * b;
        }

        public int Substract(int a, int b)
        {
            return a - b;
        }
    }
}