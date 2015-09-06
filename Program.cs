using System;

namespace NetPresentValue
{
    class Program
    {
        enum InputType {integer, real};

        static void Main(string[] args)
        {
            int term;
            double interestRate;
            double[] cashFlows;

            Console.WriteLine("This is a simple Net Present Value calculator.");
            Console.WriteLine("Please note that:");
            Console.WriteLine("The term is the number of years (at least 1 year).");
            Console.WriteLine("The interest rate is the percentage (e.g. 8% must be entered as 0.08).");
            Console.WriteLine("(Press Enter to continue)");
            Console.ReadKey();

            Console.WriteLine();
            do
                Console.Write("Enter the term in number of years: ");
            while (ParseTerm(InputType.integer, Console.ReadLine(), out term) == false);

            do
                Console.Write("Enter the interest rate as a percentage (e.g. 8% => 0.08): ");
            while (ParseInterestRate(InputType.real, Console.ReadLine(), out interestRate) == false);

            cashFlows = new double[term];
            for (int i = 0; i < term; i++)
            {
                do
                    Console.Write("Enter the cash flow #{0}: ", i + 1);
                while (ParseCashFlow(InputType.real, Console.ReadLine(), out cashFlows[i]) == false);
            }

            Console.WriteLine("The Net Present Value is {0}", NetPresentValue(term, interestRate, cashFlows));
            Console.WriteLine("(Press Enter to exit)");
            Console.ReadKey();
        }

        static bool ParseTerm(InputType inputType, string inputValue, out int returnValue)
        {
            bool ok;
            try
            {
                returnValue = Convert.ToInt32(inputValue);
                if (returnValue > 0)
                {
                    ok = true;
                }
                else
                {
                    ok = false;
                    Console.WriteLine("The term must be at least 1 year.");
                }
            }
            catch
            {
                ok = false;
                returnValue = 0;
                Console.WriteLine("The term must be an integer value.");
            }
            return ok;
        }

        static bool ParseInterestRate(InputType inputType, string inputValue, out double returnValue)
        {
            bool ok;
            try
            {
                returnValue = Convert.ToDouble(inputValue);
                if (returnValue >= 0)
                {
                    ok = true;
                }
                else
                {
                    ok = false;
                    Console.WriteLine("The interest rate must be a non negative real value.");
                }
            }
            catch
            {
                ok = false;
                returnValue = 0.0;
                Console.WriteLine("The interest rate must be a non negative real value.");
            }
            return ok;
        }

        static bool ParseCashFlow(InputType inputType, string inputValue, out double returnValue)
        {
            bool ok;
            try
            {
                returnValue = Convert.ToDouble(inputValue);
                ok = true;
            }
            catch
            {
                ok = false;
                returnValue = 0.0;
                Console.WriteLine("The cash flow must be a non negative real value.");
            }
            return ok;
        }

        static double NetPresentValue(int term, double interestRate, double[] cashFlows)
        {
            double netPresentValue = 0.0;
            for (int i = 0; i < term; i++)
            {
                netPresentValue += cashFlows[i] / Math.Pow(1 + interestRate, i);
            }
            return netPresentValue;
        }
    }
}
