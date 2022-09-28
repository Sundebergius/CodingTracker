using System;

internal class GetUserInput
{
    CodingController codingController = new();
    internal void MainMenu()
    {
        
    }

    private void ProcessAdd()
    {

    }

    private void ProcessDelete()
    {

    }

    private void ProcessUpdate()
    {

    }

    private void ProcessReport()
    {

    }

    internal string GetDateInput()
    {
        Console.WriteLine("\n\nPlease insert the duration: (Format: hh:mm). Type 0 to return to the main menu.\n\n");

        string durationInput = Console.ReadLine();
        
        return durationInput;
    }
}