using System;
using System.Globalization;
using System.Threading;

internal class GetUserInput
{
    CodingController codingController = new();
    internal void MainMenu()
    {
        bool closeApp = false;
        while (closeApp == false)
        {
                Console.WriteLine("\n\nMAIN MENU");
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("\nType 0 to Close Application.");
                Console.WriteLine("Type 1 to View records");
                Console.WriteLine("Type 2 to Add records");
                Console.WriteLine("Type 3 to Delete records");
                Console.WriteLine("Type 4 to Update records");
                Console.WriteLine("Type 5 to start tracking a current session");

                string CommandInput = Console.ReadLine();

                while (string.IsNullOrEmpty(CommandInput))
                {
                    Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4\n");
                    CommandInput = Console.ReadLine();
                }

                switch (CommandInput)
                {
                    case "0":
                        closeApp = true;
                        Environment.Exit(0);
                        break;

                    case "1":
                        codingController.Get();
                        break;

                    case "2":
                        ProcessAdd();
                        break;

                    case "3":
                    ProcessDelete();
                    break;

                    case "4":
                    ProcessUpdate();
                    break;

                    case "5":
                    ProcessSession();
                    break;

                }
        }
    }

    private void ProcessAdd()
    {
        var date = GetDateInput();
        var duration = GetDurationInput();

        Coding coding = new();

        coding.Date = date;
        coding.Duration = duration;

        codingController.Post(coding);
    }

    private void ProcessDelete()
    {
        codingController.Get();
        Console.WriteLine("Please add id of the category you want to delete (or 0 to return to Main Menu).");

        string CommandInput = Console.ReadLine();

        while(!Int32.TryParse(CommandInput, out _) || string.IsNullOrEmpty(CommandInput) || Int32.Parse(CommandInput) < 0)
        {
            Console.WriteLine("\nYou have to tyipe a valid Id (or 0 to return to the Main Menu).\n");
            CommandInput = Console.ReadLine();
        }

        var id = Int32.Parse(CommandInput);

        if (id==0) MainMenu();

        var coding = codingController.GetById(id);

        while (coding.Id == 0)
        {
            Console.WriteLine($"\nRecord with id {id} doesn't exist\n");
            ProcessDelete();
        }
        codingController.Delete(id);
        MainMenu();
    }

    private void ProcessUpdate()
    {
        codingController.Get();

        Console.WriteLine("Please add id of the category you want to update (or 0 to return to Main Menu).");
        string commandInput = Console.ReadLine();

        while (!Int32.TryParse(commandInput, out _) || string.IsNullOrEmpty(commandInput) || Int32.Parse(commandInput) < 0)
        {
            Console.WriteLine("\nYou have to type an Id (or 0 to return to the Main Menu).\n");
            commandInput = Console.ReadLine();
        }

        var id = Int32.Parse(commandInput);

        if (id == 0) MainMenu();

        var coding = codingController.GetById(id);

        while (coding.Id == 0)
        {
            Console.WriteLine($"\nRecord with id {id} doesn't exist\n");
            ProcessUpdate();
        }

        var updateInput = "";

        bool updating = true;
        while (updating == true)
        {
            Console.WriteLine($"\nType 'd' for Date \n");
                Console.WriteLine($"\nType 't' for Duration \n");
                Console.WriteLine($"\nType 's' to save update \n");
                Console.WriteLine($"\nType '0' to Go Back to Main Menu \n");

                updateInput = Console.ReadLine();

                switch (updateInput)
                {
                    case "d":
                        coding.Date = GetDateInput();
                        break;

                    case "t":
                        coding.Duration = GetDurationInput();
                        break;

                    case "0":
                        MainMenu();
                        updating = false;
                        break;

                    case "s":
                        updating = false;
                        break;

                    default:
                        Console.WriteLine($"\nType '0' to Go Back to Main Menu \n");
                        break;
                }
        }
        codingController.Update(coding);
        MainMenu();
    }

    private void ProcessReport()
    {
        Console.WriteLine("Please type a month (MM)");
        string monthInput = Console.ReadLine();

        while (
            !Int32.TryParse(monthInput, out _)
            || string.IsNullOrEmpty(monthInput)
            || Int32.Parse(monthInput) < 0
            || Int32.Parse(monthInput) > 12)
            {
                Console.WriteLine("\nInvalid month\n");
                monthInput = Console.ReadLine();
            }

            var month = Int32.Parse(monthInput);

            Console.WriteLine("Please type year (a number from 2000 to 9999)");

            string yearInput = Console.ReadLine();

            while (
                !Int32.TryParse(yearInput, out _)
                || string.IsNullOrEmpty(yearInput)
                || Int32.Parse(yearInput) < 2000
                || Int32.Parse(yearInput) > 9999)
                {
                    Console.WriteLine("\nInvalid year\n");
                    monthInput = Console.ReadLine();
                }

                var year = Int32.Parse(yearInput);

                var yearMonth = new DateTime(year, month, 01, 00, 00, 00);

                var allRecords = codingController.Get();
    }

    internal string GetDateInput()
    {
        Console.WriteLine("\n\nPlease insert the date: (Format: dd-mm-yy). Type 0 to return to the main menu.\n\n");

        string dateInput = Console.ReadLine();
        
        if (dateInput == "0") MainMenu();

        while (!DateTime.TryParseExact(dateInput, "dd-MM-yy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
        {
            Console.WriteLine("\n\nNot a valid date. Please insert the date with the format: dd-mm-yy.\n\n");
            dateInput = Console.ReadLine();
        }

        return dateInput;
    }

    internal string GetDurationInput()
    {
        Console.WriteLine("\n\nPlease insert the duration: (Format: hh:mm). Type 0 to return to the Main Menu. \n\n");

        string durationInput = Console.ReadLine();

        if (durationInput == "0") MainMenu();

        while (!TimeSpan.TryParseExact(durationInput, "h\\:mm", CultureInfo.InvariantCulture, out _))
        {
            Console.WriteLine("\n\nDuration invalid. Please insert the duration: (Format: hh:mm) or type 0 to return to the Main Menu.\n\n");
            durationInput = Console.ReadLine();
            if (durationInput == "0") MainMenu(); 
        }
        return durationInput;
    }

    internal void ProcessSession()
    {
        Console.WriteLine("\n\nDo you want to start a timer for a new session now? Type Y to confirm. Type 0 to return to the Main Menu. \n\n");

        string sessionInput = Console.ReadLine();

        if (sessionInput == "0") MainMenu();

        Timer timer = new Timer(TimerCallback, null, 0, 2000);
        Console.ReadLine();
    }

    private static void TimerCallback (object o)
    {
        Console.WriteLine("In TimerCallback: " + DateTime.Now);
    }
}