// MediSure Clinic Billing System - A console application to manage patient billing.
// Features:
// 1. Create New Bill: Input bill ID, patient name, insurance status, consultation fee, lab charges, and medicine charges.
// 2. View Last Bill: Display details of the most recent bill.
// 3. Clear Last Bill: Remove the last stored bill details.
// 4. Exit: Close the application.
namespace MediSure
{
    class Program
    {
        // main method to run the application
        static void Main(string[] args)
        {
            // creating service object
            BillingService service = new BillingService();
            bool running = true;

            while (running) // main menu loop running till user presses 4 for exit
            {
                // displaying menu options
                Console.WriteLine("================== MediSure Clinic Billing ==================");
                Console.WriteLine("1.Create New Bill (Enter Patient Details)");
                Console.WriteLine("2.View Last Bill");
                Console.WriteLine("3 Clear Last Bill");
                Console.WriteLine("4.Exit");
                Console.Write("Enter Your Option: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": // create new bill
                        service.CreateBill();
                        break;
                    case "2":// view last bill  
                        service.ViewBill();
                        break;
                    case "3":// clear last bill
                        service.ClearBill();
                        break;
                    case "4":// exit
                        running = false;
                        Console.WriteLine("Thank you. Application closed normally.");
                        break;
                    default:// invalid option
                        Console.WriteLine("Invalid Option !! Try Again ");
                        break;
                }
            }
        }
    }
}