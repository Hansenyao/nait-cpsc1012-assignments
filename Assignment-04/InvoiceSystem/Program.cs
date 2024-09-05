/**
 *  Introduction: This a task program for DMIT CPSC-1012
 *  
 *  Task Name: Spring 2024 Assignment 04 - Working with Custom Objects
 *  
 *  Student Name: Youfang Yao
 *  Student ID: 200582794
 *  
 *  Date: 2024/07/25
 *  
 *  Version: 1.0
 *
 */
namespace InvoiceSystem
{
    internal class Program
    {
 /*
        // Tester for Part A
        static void Main(string[] args)
        {
            Console.WriteLine("Assignment 4 Part A");
            Console.WriteLine("");

            // Get invoice detail by user's entering
            string productId = Prompt("Enter the product id:  ");
            productId = productId.Trim();
            if (productId.Length <= 0)
            {
                Console.WriteLine("Invalid product ID.");
                return;
            }
            Console.WriteLine("");
            //
            string description = Prompt("Enter the product description:  ");
            description = description.Trim();
            if (description.Length <= 0)
            {
                Console.WriteLine("Invalid product description.");
                return;
            }
            Console.WriteLine("");
            //
            int quantity = PromptInteger("Enter the number of the item purchased:  ");
            if (quantity <= 0)
            {
                Console.WriteLine("Invalid quantity number.");
                return;
            }
            Console.WriteLine("");
            //
            double price = PromptDouble("Enter the price of an item:  ");
            if (price < 0)
            {
                Console.WriteLine("Involid product price.");
                return;
            }

            // Construct a InvoiceDetail instance
            InvoiceDetail detail = new InvoiceDetail(productId, description, quantity, price);

            // Show invoice detail
            Console.WriteLine("");
            Console.WriteLine("Invoice Deetail:");
            Console.WriteLine("");
            Console.WriteLine("     Product ID: {0}", detail.ProductId);
            Console.WriteLine("    Description: {0}", detail.Description);
            Console.WriteLine("       Quantity: {0}", detail.Quantity);
            Console.WriteLine("          Price: {0}", detail.Price);
            Console.WriteLine("Exteneded Price: {0}", detail.ExtendedPrice);
            Console.WriteLine("");
        }
 */


        // Main entry of Part B
        static void Main(string[] args)
        {
            bool displayMainMenu = true;
            string mainMenuChoice = "";
            List<Invoice> invoices = new List<Invoice>();

            // To show the main menu
            DisplayMainMenu();

            while (displayMainMenu)
            {
                // Get user-entered option
                mainMenuChoice = Prompt("Enter your menu choice: ").ToLower();

                //MAIN MENU Switch statement
                switch (mainMenuChoice)
                {
                    case "a": // List all invoices
                        {
                            string filepath = $"../../../InvoiceTestData.csv";
                            if (File.Exists(filepath))
                            {
                                // Load invoices data from file to the list
                                bool success = LoadInvoicesFromFile(filepath, invoices);
                                if (success)
                                {
                                    // Show all invoice times in the list
                                    ListAllInvoices(invoices);
                                }
                                else
                                {
                                    Console.WriteLine("\nLoad invoices data file: {0} failed.\n", filepath);
                                }
                            }
                            else
                            {
                                Console.WriteLine($"\nFile InvoiceTestData.csv does not exist\n");
                            }
                        }
                        break;
                    case "b":   // Show one invoice information and details
                        {
                            if (invoices.Count == 0)
                            {
                                Console.WriteLine("\nNo any invoices can be display, please choice 'a) List all Invoices' and try again.\n");
                            }
                            else
                            {
                                // Get the invoice id which we want to display
                                int selectedId = PromptInteger("\nEnter invoice id to view:   ");

                                // Display the detail information
                                DisplayInvoice(invoices, selectedId);
                            }
                        }
                        break;
                    case "x":
                        displayMainMenu = false;
                        break;
                    default:
                        Console.WriteLine("Invalid menu choice. Try again!");
                        break;
                }

                Console.WriteLine();
            }
        }

        /*
         * Displays the prompt string and returns user-entered string
         * 
         * promptString: The prompt string
         * 
         * Return: An user entered string
         */
        static string Prompt(string promptString)
        {
            Console.Write(promptString);
            return Console.ReadLine() ?? "";
        }
        /*
         * Displays the prompt string and returns user-entered double value
         * 
         * promptString: The prompt string
         * 
         * Return: An user entered double value
         *         Return 0.0 if an error occured.
         */
        static double PromptDouble(string promptString)
        {
            double result = 0.0d;
            string input = "";
            bool hasValidResult = false;

            do
            {
                Console.Write(promptString);
                input = Console.ReadLine() ?? "";
                if (double.TryParse(input, out result))
                {
                    hasValidResult = true;
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Your value >{0}< is invalid. Enter a decimal number (eg 20.5)", input);
                    Console.WriteLine("");
                }
            } while (!hasValidResult);

            return result;
        }
        /*
         * Displays the prompt string and returns user-entered integer value
         * 
         * promptString: The prompt string
         * 
         * Return: An user entered integer value
         *         Return 0 if an error occured.
         */
        static int PromptInteger(string promptString)
        {
            int result = 0;
            string input = "";
            bool hasValidResult = false;

            do
            {
                Console.Write(promptString);
                input = Console.ReadLine() ?? "";
                if (int.TryParse(input, out result))
                {
                    hasValidResult = true;
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Your value >{0}< is invalid. Enter a whole number (eg 5)", input);
                    Console.WriteLine("");
                }
            } while (!hasValidResult);

            return result;
        }
        /*
         *  To display the main menu for Part B 
         * 
         */
        static void DisplayMainMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("Main Menu");
            Console.WriteLine("a) List all Invoices");
            Console.WriteLine("b) Display Invoice");
            Console.WriteLine("x) Exit");
            Console.WriteLine("");
        }
        /*
         *  Load all invoices data from file, the file content like below:
         *  
         *  Invoice,InvoiceId,InvoiceDate,Name
         *  InvoiceDetail,ProductId,Description,Quantity,Price
         *  InvoiceDetail,ProductId,Description,Quantity,Price
         *  InvoiceDetail,ProductId,Description,Quantity,Price
         *  InvoiceDetail,ProductId,Description,Quantity,Price
         * 
         *  fileName: csv file name which storages all invoices information
         *  
         *  return: return true if success, otherwise return false
         */
        static bool LoadInvoicesFromFile(string fileName, List<Invoice> invoices)
        {
            // Check filename is valid or not
            if (fileName == null || fileName.Length <= 0) 
            {
                Console.WriteLine("Parameter fileName is involid.");
                return false;
            }

            // Clear old item in list
            invoices.Clear();

            // Load all invoices data from file
            try
            {
                StreamReader reader = new StreamReader(fileName);
                while (!reader.EndOfStream)
                {
                    // Skip space lines
                    string line  = reader.ReadLine() ?? "";
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }
                    line = line.Trim();

                    // The begin line should be 'Invoice'
                    if (line.StartsWith("Invoice,"))
                    {
                        LoadInvoiceLine(line, invoices);
                    }
                    else if (line.StartsWith("InvoiceDetail,"))
                    {
                        Invoice invoice = invoices[invoices.Count - 1];
                        LoadInvoiceDetailLine(line, invoice);
                    }
                    else
                    {
                        continue;
                    }
                }
                reader.Close();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File: {0} does not exist.", fileName);
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Encounter an exception! Error: {0}.", e.Message);
                return false;
            }

            return true;
        }
        /*
         *  To parse a line string for an invoice object
         *  
         *  The line string content format is below:
         *  Invoice,InvoiceId,InvoiceDate,Name
         *  
         *  Construct a new object and add it to list 'invoices'
         * 
         */
        static void LoadInvoiceLine(string invoiceLine, List<Invoice> invoices)
        {
            string[] invoiceData = invoiceLine.Split(",");
            if (invoiceData.Length == 4)
            {
                // Get invoice's properties and check are valid or not
                int invoiceId = int.Parse(invoiceData[1]);
                if (invoiceId <= 0)
                {
                    Console.WriteLine("{0} is not a valid invoice ID, it must be greater than 0.", invoiceId);
                    return;
                }
                DateTime invoiceDate = DateTime.Parse(invoiceData[2]);
                if (invoiceDate > DateTime.Now)
                {
                    Console.WriteLine("{0} is not a valid invoice time, it cannot be in the future.", invoiceDate);
                    return;
                }
                string invoiceName = invoiceData[3];
                invoiceName = invoiceName.Trim();
                if (string.IsNullOrWhiteSpace(invoiceName))
                {
                    Console.WriteLine("Invoice Name cannot be null or empty.");
                    return;
                }

                // Add this invoice object to list
                Invoice invoice = new Invoice(invoiceId, invoiceDate, invoiceName, new List<InvoiceDetail>());
                invoices.Add(invoice);
            }
        }
        /*
         *  To parse a line string for an invoice detail object
         *  
         *  The line string content format is below:
         *  InvoiceDetail,ProductId,Description,Quantity,Price
         *  
         *  Construct a new object and add it to the property Invoice.InvoiceDetals
         * 
         */
        static void LoadInvoiceDetailLine(string detailLine, Invoice invoice)
        {
            string[] invoiceDetail = detailLine.Split(",");
            if (invoiceDetail.Length == 5)
            {
                // Get invoice detail properties and check are valid or not
                string id = invoiceDetail[1];
                id = id.Trim();
                if (string.IsNullOrWhiteSpace(id))
                {
                    Console.WriteLine("Product Id cannot be null or empty.");
                    return;
                }
                string description = invoiceDetail[2];
                description = description.Trim();
                if (string.IsNullOrWhiteSpace(description))
                {
                    Console.WriteLine("Product description cannot be null or empty.");
                    return;
                }
                int quantity = int.Parse(invoiceDetail[3]);
                if (quantity <= 0)
                {
                    Console.WriteLine("{0} is not a valid quantity, it must be greater than 0.", quantity);
                    return;
                }
                double price = double.Parse(invoiceDetail[4]);
                if (price < 0)
                {
                    Console.WriteLine("{0} is not a valid price, it must be greater or equal to 0.", price);
                    return;
                }

                // Add this detail to the property 'InvoiceDetails' in object 'invoice' 
                InvoiceDetail detail = new InvoiceDetail(id, description, quantity, price);
                invoice.InvoiceDetails.Add(detail);
            }
        }
        /*
         *  List all invoices from file 
         * 
         *  fileName: All invoices list
         *  
         *  return: void
         */
        static void ListAllInvoices(List<Invoice> invoices)
        {
            Console.WriteLine("");
            Console.WriteLine("Current Invoices:");
            Console.WriteLine("\t{0,-4} {1,-10}\t{2,-30}\t{3,15}", "ID", "Date", "Sales Person", "Total");
            foreach (Invoice invoice in invoices)
            {
                Console.WriteLine(invoice.ToString());
            }
            Console.WriteLine("");
        }
        /*
         *  To display one invoice information and its detail 
         * 
         *  fileName: All invoices list
         *  invoiceId: The invoice identity of which we want to display
         *  
         */
        static void DisplayInvoice(List<Invoice> invoices, int invoiceId)
        {
            bool hasSelected = false;

            // Find the invoice which we want to display from the list
            foreach (Invoice invoice in invoices)
            {
                if (invoice.InvoiceId == invoiceId)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Current Invoice:");
                    Console.WriteLine("");

                    // Display the selected invoice information
                    Console.WriteLine("\t{0,-4} {1,-10}\t{2,-30}\t{3,15}", "ID", "Date", "Sales Person", "Total");
                    Console.WriteLine(invoice.ToString());
                    Console.WriteLine("");

                    // Display the detail items of this invoice
                    Console.WriteLine("");
                    Console.WriteLine("{0,-10}\t{1,-40}\t{2,10}\t{3,10}\t{4,10}", "Product", "Description", "Qty", "Price", "Total");
                    foreach (InvoiceDetail detail in invoice.InvoiceDetails)
                    {
                        Console.WriteLine(detail.ToString());
                    }
                    Console.WriteLine("");

                    hasSelected = true;
                    break;
                }
            }

            // No any invoice is selected, the invoiceId is involid
            if (!hasSelected)
            {
                Console.WriteLine("There are no invoive for id of {0} on record at this time.", invoiceId);
            }
        }
    }
}
