using System;
//Summary
// This is the main program file for QuickMart Traders application.
// It provides a console-based interface for users to create transactions,
// view the last transaction, and calculate profit or loss on transactions.
/// The application runs in a loop until the user chooses to exit.
namespace Quick
{
    class Program
    {
        static void Main(string[] args)
        {
            TransactionService service = new TransactionService(); // create service object
            bool running = true; // running till option 4 is chosen
            // main loop for the application
            while (running)
            {
                Console.WriteLine("================== QuickMart Traders ==================");
                Console.WriteLine("1. Create New Transaction (Enter Purchase & Selling Details)");
                Console.WriteLine("2. View Last Transaction");
                Console.WriteLine("3. Calculate Profit/Loss (Recompute & Print)");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your option: ");
                int choice= int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:// create new transaction
                        service.CreateTransaction();
                        break;
                    case 2:// view last transaction
                        service.ViewTransaction();
                        break;
                    case 3:// calculate profit or loss
                        service.CalculateProfitOrLoss();
                        break;
                    case 4:// exit application
                        Console.WriteLine("Thank you. Application closed normally.");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}