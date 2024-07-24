/**
 *  Introduction: This a task program for DMIT CPSC-1012
 *  
 *  Task Name: Spring 2024 Assignment 02 - Control Structures and Error Handling
 *  
 *  Student Name: Youfang Yao
 *  Student ID: 200582794
 *  
 *  Date: 2024/07/05
 *  
 *  Version: 1.0
 *
 */
namespace LimoService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***************************************");
            Console.WriteLine("*            Limo Service             *");
            Console.WriteLine("***************************************");
            Console.WriteLine();

            // Try to get user inputing
            int NumberOfPeople = 0;
            int NumberOfHours = 0;
            int TotalDistanceInKm = 0;
            try
            {
                // Get the number of people and check it is acceptable or not
                NumberOfPeople = int.Parse(PromptForUserInput("Please enter the number of people (from 1 to 12): "));
                if (NumberOfPeople < 1)
                {
                    Console.WriteLine("The number of people can't be less than 1!");
                    return;
                }
                if (NumberOfPeople > 12)
                {
                    Console.WriteLine("To many people. Booking rejected!");
                    return;
                }

                // Get the number of hours and check it is acceptable or not
                NumberOfHours = int.Parse(PromptForUserInput("Please enter how many hours are booked: "));
                if (NumberOfHours <= 0)
                {
                    Console.WriteLine("The booked hours should be postive whole number!");
                    return;
                }

                // Get the total distance and check it is acceptable or not
                TotalDistanceInKm = int.Parse(PromptForUserInput("Please enter the total distance in km: "));
                if (TotalDistanceInKm < 0)
                {
                    Console.WriteLine("The total distance should be positive whole number!");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid input! Error: " + ex.ToString());
                return;
            }

            Console.WriteLine("\nCharges Information:");

            // Output numbers of people and charges
            double ChargesOfPeople = CalcChargesOfPeople(NumberOfPeople);
            Console.WriteLine("Number of People:    {0}\t{1}", 
                    string.Format("{0, 5}", NumberOfPeople), 
                    string.Format("{0, 10}", string.Format("${0:f2}", ChargesOfPeople)));

            // Output booked hours and charges
            double ChargesOfHours = CalcChargesOfHours(NumberOfHours);
            Console.WriteLine("Booked Hours:        {0}\t{1}", 
                    string.Format("{0, 5}", NumberOfHours),
                    string.Format("{0, 10}", string.Format("${0:f2}", ChargesOfHours)));

            // Output totals distance and charges
            double ChargesOfDistance = CalcChargesOfDistance(TotalDistanceInKm);
            Console.WriteLine("Total Distance:      {0}\t{1}", 
                    string.Format("{0, 5}", TotalDistanceInKm),
                    string.Format("{0, 10}", string.Format("${0:f2}", ChargesOfDistance)));

            //Output the totals
            double TotalCharges = ChargesOfPeople + ChargesOfHours + ChargesOfDistance;
            Console.WriteLine("Total                   \t{0}",
                    string.Format("{0, 10}", string.Format("${0:f2}", TotalCharges)));
        }

        /*
         *  To prompt a message and ask user to input a value
         *  
         *  Return user inputed value as string
         */
        static string PromptForUserInput(string promptText)
        {
            Console.Write(promptText);
            return Console.ReadLine() ?? "0";
        }

        /*
         * To calculate the charges of people, as below:
         * 1. 1st person (regardless of the number of people) charged 25.00
         * 2. for next 3 people, charge 10.00 each
         * 3. for next 4 people, charge 7.50 each
         * 4. for the next 4 people, charge 6.00 each
         * 
         */
        static double CalcChargesOfPeople(int NumberOfPeople)
        {
            double Charges = 0.0;
            if (NumberOfPeople == 1)
            {
                Charges = 25;
            }
            else if (NumberOfPeople > 1 && NumberOfPeople <= 4) 
            {
                Charges = 25;
                Charges += 10 * (NumberOfPeople - 1);
            }
            else if (NumberOfPeople > 4 && NumberOfPeople <= 8)
            {
                Charges = 25;
                Charges += 3 * 10;
                Charges += 7.5 * (NumberOfPeople - (1 + 3));
            }
            else if (NumberOfPeople > 8 && NumberOfPeople <= 12)
            {
                Charges = 25;
                Charges += 3 * 10;
                Charges += 4 * 7.5;
                Charges += 6 * (NumberOfPeople - (1 + 3 + 4));
            }
            else
            {
                Charges = 0.0;
            }

            return Charges;
        }

        /*
         *  To calculate the charges of booked hours, as below:
         *  1. a minimum of 2 hours is charged; $80 * 2 = 160
         *  2. over 2 hours till 4 hours; all hours charged at $70 (including first 2)
         *  3. over 4 hours till 8 hours; all hours charged at $65
         *  4. 9 or more hours; all hours charged at $60
         *
         */
        static double CalcChargesOfHours(int NumberOfHours)
        {
            double Charges = 0;

            if (NumberOfHours <= 2)
            {
                Charges = 2 * 80;
            }
            else if (NumberOfHours > 2 && NumberOfHours <= 4)
            {
                Charges = NumberOfHours * 70;
            }
            else if (NumberOfHours > 4 && NumberOfHours <= 8)
            {
                Charges = NumberOfHours * 65;
            }
            else
            {
                Charges = NumberOfHours * 60;
            }

            return Charges;
        }

        /*
         *  To calculate the charges of total distance in km, as below:
         *  1. under 100; all mileage charged at 0.10
         *  2. over 100 till 250; all mileage charged at 0.08
         *  3. over 250 till 400; all mileage charged at 0.07
         *  4. over 400; all mileage charged at 0.06
         * 
         */
        static double CalcChargesOfDistance(int DistanceInKm)
        {
            double Charges = 0;

            if (DistanceInKm <= 100)
            {
                Charges = DistanceInKm * 0.1;
            }
            else if (DistanceInKm > 100 && DistanceInKm <= 250)
            {
                Charges = DistanceInKm * 0.08;
            }
            else if (DistanceInKm > 250 && DistanceInKm <= 400)
            {
                Charges = DistanceInKm * 0.07;
            }
            else if (DistanceInKm > 400)
            {
                Charges = DistanceInKm * 0.06;
            }

            return Charges;
        }
    }
}
