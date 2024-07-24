/**
 *  Introduction: This a task program for DMIT CPSC-1012
 *  
 *  Task Name: Spring 2024 Assignment 01 - Arithmetic Expressions
 *  
 *  Student Name: Youfang Yao
 *  Student ID: 200582794
 *  
 *  Date: 2024/05/27
 *  
 *  Version: 1.0
 *
 */
namespace Task_270524
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***************************************");
            Console.WriteLine("*           Super SuperMarket         *");
            Console.WriteLine("***************************************");
            Console.WriteLine();

            float OVERTIME_PAY_TIMES = 1.5f;
            float TAX_RATE = 0.3f;

            // Input employee id
            Console.Write("Enter employee id: ");
            string EmployeeId = Console.ReadLine() ?? "";
            if (EmployeeId.Length <= 0)
            {
                Console.WriteLine("Error: Employee id can not be null!");
                return;
            }

            // Input pay rate
            Console.Write("Enter play rate: ");
            float PayRate = float.Parse(Console.ReadLine() ?? "0");
            if (PayRate <= 0)
            {
                Console.WriteLine("Error: Pay rate can not be less or equal 0!");
                return;
            }

            // Input regular hours worked
            Console.Write("Enter regular hours worked: ");
            float RegularHours = float.Parse(Console.ReadLine() ?? "0");
            if (RegularHours <= 0)
            {
                Console.WriteLine("Error: Regular hours worked can not be less or equal 0!");
                return;
            }

            // Input overtime hours worked
            Console.Write("Enter overtime hours worked (my be 0): ");
            float OvertimeHours = float.Parse(Console.ReadLine() ?? "0");
            if (OvertimeHours < 0)
            {
                Console.WriteLine("Error: Overtime hours worked can not be less 0!");
                return;
            }

            // Input total deductions
            Console.Write("Enter total deductions for pay period: ");
            float TotalDeductions = float.Parse(Console.ReadLine() ?? "0");

            // Calculate gross pay
            float GrossPay = PayRate * RegularHours + OVERTIME_PAY_TIMES * PayRate * OvertimeHours;

            // Calculate tax and net pay
            float Tax = GrossPay * TAX_RATE;
            float NetPay = GrossPay - (Tax + TotalDeductions);

            // Output result
            Console.WriteLine();
            Console.WriteLine("Pay information for employee: {0}", EmployeeId);
            Console.WriteLine();
            Console.WriteLine("Gross Pay:                    {0}", string.Format("{0:f2}", GrossPay));
            Console.WriteLine("Tax:                          {0}", string.Format("{0:f2}", Tax));
            Console.WriteLine("Deductions:                   {0}", string.Format("{0:f2}", TotalDeductions));
            Console.WriteLine("Net Pay:                      {0}", string.Format("{0:f2}", NetPay));

        }
    }
}
