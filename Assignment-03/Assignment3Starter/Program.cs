
using System.Diagnostics;

/// <summary>
/// Assignment 3
/// 
/// Author: Youfang Yao
/// Date: July 20, 2024
/// Purpose: Allows user to enter/save/load/edit/view weekly pay slip values
///          from a file. 
/// </summary>

string mainMenuChoice = "";
string filename = "";
bool displayMainMenu = true;
bool proceed = false;
bool quit = true;
int logicalSize = 0;

// TODO: declare a constant to represent the max size of the names, wages
// and hours arrays. The arrays must be large enough to store
// pay data for an entire employee list.
const int MAX_EMPLOYEE_COUNT = 25;


// TODO: create a string array named 'names', use the max size constant you declared
// above to specify the physical size of the array.
string[] names = new string[MAX_EMPLOYEE_COUNT];

// TODO: create a double array named 'wages', use the max size constant you declared
// above to specify the physical size of the array.
double[] wages = new double[MAX_EMPLOYEE_COUNT];


// TODO: create a double array named 'hours', use the max size constant you declared
// above to specify the physical size of the array.
double[] hours = new double[MAX_EMPLOYEE_COUNT];

// TODO: create any additional variable you need for your program to work. These
// variable cannot be accessed directly within any of your methods. They must only
// be used in the driver logic.

DisplayProgramIntro();

// To show the main menu
DisplayMainMenu();

while (displayMainMenu)
{
    // Get user-entered option
    mainMenuChoice = Prompt("Enter MAIN MENU option ('D' to display menu): ").ToUpper();

    Console.WriteLine();

    //MAIN MENU Switch statement
    switch (mainMenuChoice)
    {
        case "N": // [N]ew Weekly Hour Setup Entry

            proceed = NewEntrySetupDisclaimer();

            if (proceed)
            {
                string filepath = $"../../../EmployeeList.csv";
                if (File.Exists(filepath))
                {
                    // TODO: call the LoadEmployees method to load employees
                    //       for a new weekly pay setup; returns the number of records loaded to the arrays
                    logicalSize = LoadEmployees(names, wages, hours, filepath);
                    Console.WriteLine();
                    Console.WriteLine($"Employees loaded. {logicalSize} records in temporary memory.");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"\nFile EmployeeList.csv does not exist\n");
                }
            }
            else
            {
                Console.WriteLine("Cancelling new weekly pay setup. Returning to MAIN MENU.");
            }
            break;
        case "L": //[L]oad Weekly Hour Data File
            proceed = LoadEntryDisclaimer();

            if (proceed)
            {
                filename = PromptForFilename();
                filename = filename.Trim();
                string filepath = $"../../../{filename}";
                if (File.Exists(filepath))
                {
                    // TODO: call the LoadWeeklyFile method to load an existing weekly pay file;
                    //  returns the number of records loaded to the arrays
                    logicalSize = LoadWeeklyFile(names, wages,hours,filepath);

                    Console.WriteLine();
                    Console.WriteLine($"{logicalSize} records were loaded.");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"\nFile {filename} does not exist\n");
                }
            }
            else
            {
                Console.WriteLine("Cancelling LOAD operation. Returning to MAIN MENU.");
            }
            break;
        
        case "E": // [E]dit Currently Loaded Weekly Hours Data
            if (logicalSize == 0)
            {
                Console.WriteLine("Sorry, LOAD data (for new hours or existing hours)before EDITING.");
            }
            else
            {
                proceed = EditEntryDisclaimer();

                if (proceed)
                {
                    // TODO: call the EditWeeklyEntries method here to manage the hours in the current arrays
                    EditWeeklyEntries(names, hours, logicalSize);
                }
                else
                {
                    Console.WriteLine("Cancelling EDIT operation. Returning to MAIN MENU.");
                }
            }
            break;
        case "S": // [S]ave Weekly Hour Data to File
            if (logicalSize == 0)
            {
                Console.WriteLine("Sorry, LOAD data or enter NEW data before SAVING.");
            }
            else
            {
                proceed = SaveEntryDisclaimer();

                if (proceed)
                {
                    filename = PromptForFilename();
                    string filepath = $"../../../{filename}";

                    // TODO: call the SaveWeeklyFile method here to save the weekly pay slip data in the current arrays
                    SaveWeeklyFile(names, wages, hours, filename, logicalSize);
                    Console.WriteLine("");
                }
                else
                {
                    Console.WriteLine("Cancelling save operation. Returning to MAIN MENU.");
                }
            }
            break;
        case "V": // [V]iew Currently Loaded Weekly Hour Data
            if (logicalSize == 0)
            {
                Console.WriteLine("Sorry, LOAD data or enter NEW data before VIEWING.");
            }
            else
            {
                // TODO: call the DisplayWeeklyHours method here to view the weekly pay slip data in the current arrays
                DisplayWeeklyHours(names, wages, hours, logicalSize);
            }
            break;
        case "C": // [C]alculate/Display Weekly Pay Slip

            if (logicalSize == 0)
            {
                Console.WriteLine("Sorry, LOAD data or enter NEW data before calculating weekly pay slips.");
            }
            else
            {
                //TODO: call the EmployeePaySlips method here to calculate a weekly pay slip data from the current arrays 
                EmployeePaySlips(names, wages, hours, logicalSize);
            }
            break;
        case "D": //[D]isplay Main Menu
            DisplayMainMenu();
            break;
        case "Q": //[Q]uit Program
            // TODO: uncomment the following line after you have coded the Prompt method
            quit = Prompt("Are you sure you want to quit (Y/N)? ").ToUpper().Equals("Y");
            Console.WriteLine();
            if (quit)
            {
                displayMainMenu = false;
            }
            break;
        default: //invalid entry. Reprompt.
            Console.WriteLine("Invalid reponse. Enter one of the letters to choose a menu option.");
            break;
    }
}


DisplayProgramOutro();

// ================================================================================================ //
//                                                                                                  //
//                                          METHODS to Code                                         //
//                                                                                                  //
// ================================================================================================ //

// ++++++++++++++++++++++++++++++++++++ Difficulty 1 ++++++++++++++++++++++++++++++++++++
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
        try
        {
            Console.Write(promptString);
            input = Console.ReadLine() ?? "";
            result = double.Parse(input);
            hasValidResult = true;
        }
 
        catch (Exception e)
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
        try
        {
            Console.Write(promptString);
            input = Console.ReadLine() ?? "";
            result = int.Parse(input);
            hasValidResult = true;
        }
        catch (Exception e)
        {
            Console.WriteLine("");
            Console.WriteLine("Your value >{0}< is invalid. Enter a whole number (eg 5)", input);
            Console.WriteLine("");
        }
    } while (!hasValidResult);

    return result;
}

/*
 * To display the main menu.
 * 
 * The menu must consist of the following options:
 * 
 * [N]ew Weekly Pay Setup Entry
 * [L]oad Weekly Pay Data from File
 * [E]dit Currently Loaded Weekly Pay Data
 * [S]ave Weekly Pay Data to File
 * [V]iew Currently Loaded Weekly Pay Data
 * [C]alculate Weekly Pay Slip
 * [D]isplay Main Menu
 * [Q]uit Program
 * 
 */
static void DisplayMainMenu()
{
    Console.WriteLine("[N]ew Weekly Pay Setup Entry");
    Console.WriteLine("[L]oad Weekly Pay Data from File");
    Console.WriteLine("[E]dit Currently Loaded Weekly Pay Data");
    Console.WriteLine("[S]ave Weekly Pay Data to File");
    Console.WriteLine("[V]iew Currently Loaded Weekly Pay Data");
    Console.WriteLine("[C]alculate Weekly Pay Slip");
    Console.WriteLine("[D]isplay Main Menu");
    Console.WriteLine("[Q]uit Program");
    Console.WriteLine("");
}

// ++++++++++++++++++++++++++++++++++++ Difficulty 2 ++++++++++++++++++++++++++++++++++++
/*
 * To reads the current employee list file and loads the employee name, 
 * status and wage into their appropriate arrays; 
 * set the hours array to zero.
 * 
 * Employee list file consist of employee's full name and the corresponsing wage,
 * they are splitted by ',', like below:
 * 
 * Shirley Ujest,25.25
 * Lowand Behold,18.50
 * Pat Downe,18.50
 * Ima Stew-Dent,16.50
 * Charity Kase,18.50
 * 
 * Return: returns the number of employees loaded
 * 
 */
static int LoadEmployees(string[] names, double[] wages, double[] hours, string filename)
{
    int readCount = 0;

    try
    {
        string line;
        StreamReader fileReader = new StreamReader(filename);
        while (((line = fileReader.ReadLine() ?? "") != null) && 
                (line.Length > 0) &&
                (readCount < MAX_EMPLOYEE_COUNT))
        {
            string[] lineValues = line.Split(',');
            names[readCount] = lineValues[0];
            wages[readCount] = double.Parse(lineValues[1]);
            hours[readCount] = 0;
            readCount++;
        }
        fileReader.Close();
    }
    catch (FileNotFoundException e)
    {
        readCount = 0;
        Console.WriteLine("File:{0} does not exist!", filename);
    }
    catch (Exception e)
    {
        readCount = 0;
        Console.WriteLine("Failed to read file:{0}, error: {1}", filename, e.Message);
    }

    return readCount;
}
/*
 *  To load the records from an existing weekly file (filename) into 
 *  the associative arrays used by the program.
 *  
 * Employee list file consist of employee's full name,wage and weekly hours,
 * they are splitted by ',', like below:
 * 
 * Shirley Ujest,25.25,38.2
 * Lowand Behold,18.50,43.5
 * Pat Downe,18.50,38.2
 * Ima Stew-Dent,16.50,18.0
 * Charity Kase,18.50,33.6
 * 
 * Return: returns the record count (i.e. how many employees were loaded)
 * 
 */
static int LoadWeeklyFile(string[] names, double[] wages, double[] hours, string filename)
{
    int readCount = 0;

    try
    {
        string line;
        StreamReader fileReader = new StreamReader(filename);
        while (((line = fileReader.ReadLine() ?? "") != null) &&
                (line.Length > 0) &&
                (readCount < MAX_EMPLOYEE_COUNT))
        {
            string[] lineValues = line.Split(',');
            names[readCount] = lineValues[0];
            wages[readCount] = double.Parse(lineValues[1]);
            hours[readCount] = double.Parse(lineValues[2]);
            readCount++;
        }
        fileReader.Close();
    }
    catch (FileNotFoundException e)
    {
        readCount = 0;
        Console.WriteLine("File:{0} does not exist!", filename);
    }
    catch (Exception e)
    {
        readCount = 0;
        Console.WriteLine("Failed to read file:{0}, error: {1}", filename, e.Message);
    }

    return readCount;
}
/*
 *  To  writes the associative array data to a weekly file (filename) in 
 *  the correct format.
 * 
 *  Return: void
 */
static void SaveWeeklyFile(string[] names, double[] wages, double[] hours, string filename, int countOfEntries)
{
    if (filename.Length == 0)
    {
        Console.WriteLine("Invalid file name!");
        return;
    }
    if (countOfEntries <= 0 || countOfEntries > MAX_EMPLOYEE_COUNT)
    {
        Console.WriteLine("Invalid count of entries!");
        return;
    }

    try
    {
        StreamWriter fileWriter = new StreamWriter(filename, false);
        for (int i = 0; i < countOfEntries; i++)
        {
            string lineValue = names[i];
            lineValue += ",";
            lineValue += wages[i];
            lineValue += ",";
            lineValue += hours[i];
            fileWriter.WriteLine(lineValue);
        }
        fileWriter.Close();
    }
    catch (Exception e)
    {
        Console.WriteLine("Failed to write file:{0}, error: {1}", filename, e.Message);
    }
}
/*
 *  To displas the current entered/loaded employee entries in a 
 *  formatted table (i.e. ensure that proper columns and alignment are used). 
 *
 *  Return: void
 */
static void DisplayWeeklyHours(string[] names, double[] wages, double[] hours, int countOfEntries)
{
    if (countOfEntries <= 0 || countOfEntries > MAX_EMPLOYEE_COUNT)
    {
        Console.WriteLine("Invalid count of entries!");
        return;
    }

    Console.WriteLine("Name\t\t\tWage\tHours");
    Console.WriteLine("----------------------------------------");
    for (int i = 0;i < countOfEntries; i++)
    {
        Console.WriteLine("{0}\t\t{1}\t{2}", 
                            names[i], 
                            string.Format("{0:f2}", wages[i]),
                            string.Format("{0:f1}", hours[i]));
    }
    Console.WriteLine("");
}

// ++++++++++++++++++++++++++++++++++++ Difficulty 3 ++++++++++++++++++++++++++++++++++++
/*
 * To edit employee weekly entry
 * 
 * Allows the user to view all current weekly entries and choose one to edit 
 * the employee weekly hours (i.e. overwrite hours). 
 * 
 * An employee may only work up to 75 hours per week.
 *  
 * 
 */
static void EditWeeklyEntries(string[] names, double[] hours, int countOfEntries)
{
    if (countOfEntries <= 0 || countOfEntries > MAX_EMPLOYEE_COUNT)
    {
        Console.WriteLine("Invalid count of entries!");
        return;
    }

    bool cancelByUser = false;
    do
    {
        // Ouptut current employees weekly hours
        Console.WriteLine("");
        Console.WriteLine("Current Weekly Hour Data\n");
        Console.WriteLine("\tIndex\tName\t\t\tHours");
        for (int i = 0; i < countOfEntries; i++)
        {
            Console.WriteLine("\t{0}\t{1}\t\t{2}",
                               i + 1,
                               names[i],
                               string.Format("{0:f1}", hours[i]));
        }
        Console.WriteLine("");

        // Choice an index to edit
        int selectIndex = PromptInteger("Select an employee to maintain hours by number:  ");
        if (selectIndex < 1 || selectIndex > countOfEntries)
        {
            // User inputed an invalid number
            Console.WriteLine("");
            Console.WriteLine("You have entered an invalid number to select employee: {0}", selectIndex);
            Console.WriteLine("");
            if (!Prompt("Would you like to edit other employee? (Y/N):  ").ToLower().Equals("y"))
            {
                cancelByUser = true;
            }
        }
        else
        {
            // Ask user to confirm
            Console.WriteLine("");
            if (Prompt(string.Format("You selected {0} to edit. Continue? (Y/N):  ", names[selectIndex - 1])).ToLower().Equals("y"))
            {
                // Ask user enter the nuber of hours
                Console.WriteLine("");
                double newHours = PromptDouble("Enter the number of hours the employee worked this week (between 1 and 75):  ");
                if (newHours < 1 || newHours > 75)
                {
                    // User entered an invalid number
                    Console.WriteLine("");
                    Console.WriteLine("You have entered an invalid number of hours: {0}", newHours);
                }
                else
                {
                    // Update hours in arry
                    hours[selectIndex - 1] = newHours;
                }
                Console.WriteLine("");
                if (!Prompt("Would you like to edit other employee? (Y/N):  ").ToLower().Equals("y"))
                {
                    cancelByUser = true;
                }
            }
            else
            {
                Console.WriteLine("You have decided not to edit the hours of {0}.", names[selectIndex - 1]);
                Console.WriteLine("");

                if (!Prompt("Would you like to edit other employee? (Y/N):  ").ToLower().Equals("y"))
                {
                    cancelByUser = true;
                }
            }
        }
    } while (!cancelByUser);

    Console.WriteLine("");
}

/*
 * To calculate employees weekly pay slip
 * 
 * Allows the user to view all current weekly pay slip data for the employees. 
 * Goss Wage is straight time for the first 37.5 hours per week then 1.5 * straight 
 * time for any hours over 37.5. 
 * 
 * Tax rate is according to the following table. 
 * 
 */
static void EmployeePaySlips(string[] names, double[] wages, double[] hours, int countOfEntries)
{
    Console.WriteLine("");
    if (countOfEntries <= 0 || countOfEntries > MAX_EMPLOYEE_COUNT)
    {
        Console.WriteLine("Invalid count of entries!");
        return;
    }

    Console.WriteLine("Name\t\t\tWages\tHours\tGross Wages\t\tTaxes\t\tNet Pay");
    for (int i = 0; i < countOfEntries; i++)
    {
        double gossWages = CalculateGossWages(wages[i], hours[i]);
        double taxes = CalculateTaxes(gossWages);
        double netPay = gossWages - taxes;

        Console.Write("{0}\t\t", names[i]);
        Console.Write("{0}\t", string.Format("{0, 5}", string.Format("{0:f2}", wages[i])));
        Console.Write("{0}\t", string.Format("{0, 5}", string.Format("{0:f1}", hours[i])));
        Console.Write("{0}\t", string.Format("{0, 11}", string.Format("{0:f2}", gossWages)));
        Console.Write("{0}\t", string.Format("{0, 13}", string.Format("{0:f2}", taxes)));
        Console.Write("{0}\t", string.Format("{0, 15}", string.Format("{0:f2}", netPay)));
        Console.Write("\n");
    }
    Console.WriteLine("");
}
/*
 * To calculate goss wages based wage and working hours
 * 
 * wage: the base wage per hour
 * hours: the weekly work hours
 * 
 * Return: the goss wages
 * 
 */
static double CalculateGossWages(double wage, double hours)
{
    double gossWages = 0.0d;
    const double STRAIGHT_HOURS = 37.5d;
    const double OVERTIME_WAGE_SCALE = 1.5d;

    if (hours <= STRAIGHT_HOURS)
    {
        gossWages = hours * wage;
    }
    else
    {
        gossWages = STRAIGHT_HOURS * wage;
        gossWages += OVERTIME_WAGE_SCALE * wage * (hours - STRAIGHT_HOURS);
    }

    return gossWages;
}
/*
 * To calculate taxes based goss wages
 * 
 * gossWages: the goss wages
 * 
 * Return: the taxes
 * 
 */
static double CalculateTaxes(double gossWages)
{
    const int WAGE_LEVEL_1 = 600;
    const int WAGE_LEVEL_2 = 1200;
    const double TAX_RATE_1 = 17.0d / 100;
    const double TAX_RATE_2 = 21.0d / 100;
    const double TAX_RATE_3 = 23.5d / 100;
    double taxes = 0.0d;

    if (gossWages <= WAGE_LEVEL_1)
    {
        taxes = gossWages * TAX_RATE_1;
    }
    else if (gossWages > WAGE_LEVEL_1 && gossWages <= WAGE_LEVEL_2)
    {
        taxes = WAGE_LEVEL_1 * TAX_RATE_1;
        taxes += (gossWages - WAGE_LEVEL_1) * TAX_RATE_2;
    }
    else
    {
        taxes = WAGE_LEVEL_1 * TAX_RATE_1;
        taxes += (WAGE_LEVEL_2 - WAGE_LEVEL_1) * TAX_RATE_2;
        taxes += (gossWages - (WAGE_LEVEL_1 + WAGE_LEVEL_2)) * TAX_RATE_3;
    }

    return taxes;
}

#region Additional Provided Methods DO NOT ALTER
// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// ++++++++++++++++++++++++++++++++++++ Additional Provided Methods ++++++++++++++++++++++++++++++++++++
// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

// NOTE: Many of the following methods depend on the Prompt method and will operate correctly once
// that method has been implemented.

/// <summary>
/// Displays the Program intro.
/// </summary>
static void DisplayProgramIntro()
{
    Console.WriteLine("========================================");
    Console.WriteLine("=                                      =");
    Console.WriteLine("=            Weekly Pay Slips          =");
    Console.WriteLine("=                                      =");
    Console.WriteLine("========================================");
    Console.WriteLine();
}

// <summary>
/// Displays the Program outro.
/// </summary>
static void DisplayProgramOutro()
{
    Console.Write("Program terminated. Press ENTER to exit program...");
    Console.ReadLine();
}

/// <summary>
/// Displays a disclaimer for NEW entry option.
/// </summary>
/// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
static bool NewEntrySetupDisclaimer()
{
    bool response;
    Console.WriteLine("Disclaimer: proceeding will overwrite all unsaved data.");
    Console.WriteLine("Hint: This will clear existing weekly data and load with a clean employee list.");
    Console.WriteLine("Hint: You'll need to enter data for the hours only.");
    Console.WriteLine();
    response = Prompt("Do you wish to proceed anyway? (Y/N) ").ToLower().Equals("y");
    Console.WriteLine();
    return response;
}

/// <summary>
/// Displays a disclaimer for LOAD entry option.
/// </summary>
/// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
static bool LoadEntryDisclaimer()
{
    bool response;
    Console.WriteLine("Disclaimer: proceeding will overwrite all unsaved data.");
    Console.WriteLine("Hint: If you currently have weekly pay data entries, save them first!");
    Console.WriteLine();
    response = Prompt("Do you wish to proceed anyway? (Y/N) ").ToLower().Equals("y");
    Console.WriteLine();
    return response;
}

/// <summary>
/// Displays a disclaimer for EDIT entry option.
/// </summary>
/// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
static bool EditEntryDisclaimer()
{
    bool response;
    Console.WriteLine("Disclaimer: editing will overwrite unsaved weekly pay data values.");
    Console.WriteLine("Hint: Save to a file before editing.");
    Console.WriteLine();
    response = Prompt("Do you wish to proceed anyway? (Y/N) ").ToLower().Equals("y");
    Console.WriteLine();
    return response;
}

/// <summary>
/// Displays a disclaimer for SAVE entry option.
/// </summary>
/// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
static bool SaveEntryDisclaimer()
{
    bool response;
    Console.WriteLine("Disclaimer: saving to an EXISTING file will overwrite data currently on that file.");
    Console.WriteLine("Hint: Files will be saved to this program's directory by default.");
    Console.WriteLine("Hint: If the file does not yet exist, it will be created.");
    Console.WriteLine();
    response = Prompt("Do you wish to proceed anyway? (Y/N) ").ToLower().Equals("y");
    Console.WriteLine();
    return response;
}

/// <summary>
/// Displays prompt for a filename, and returns a valid filename. 
/// Includes exception handling.
/// </summary>
/// <returns>User-entered string, representing valid filename (.txt or .csv)</returns>
static string PromptForFilename()
{
    string filename = "";
    bool isValidFilename = true;
    const string CsvFileExtension = ".csv";
    const string TxtFileExtension = ".txt";

    do
    {
        filename = Prompt("Enter name of .csv or .txt file to Load/Save (eg. Jun-16-2024-hours.csv): ");
        if (filename == "")
        {
            isValidFilename = false;
            Console.WriteLine("Please try again. The filename cannot be blank or just spaces.");
        }
        else
        {
            if (!filename.EndsWith(CsvFileExtension) && !filename.EndsWith(TxtFileExtension)) //if filename does not end with .txt or .csv.
            {
                filename = filename + CsvFileExtension; //append .csv to filename
                Console.WriteLine("It looks like your filename does not end in .csv or .txt, so it will be treated as a .csv file.");
                isValidFilename = true;
            }
            else
            {
                Console.WriteLine("It looks like your filename ends in .csv or .txt, which is good!");
                isValidFilename = true;
            }
        }
    } while (!isValidFilename);
    return filename;
}

#endregion