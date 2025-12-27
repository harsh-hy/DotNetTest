using System;

namespace QuickMart
{
    public class SaleTransaction
    {
        public string InvoiceNo { get; set; }
        public string CustomerName { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal PurchaseAmount { get; set; }
        public decimal SellingAmount { get; set; }
        public string ProfitOrLossStatus { get; set; }
        public decimal ProfitOrLossAmount { get; set; }
        public decimal ProfitMarginPercent { get; set; }
    }

    public class TransactionService
    {
        public static SaleTransaction LastTransaction;
        public static bool HasLastTransaction = false;

        public void CreateTransaction()
        {
            SaleTransaction t = new SaleTransaction(); // new transaction object

            // takink inputs from the user in the given  format 
            Console.Write("Enter Invoice No: ");
            t.InvoiceNo = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(t.InvoiceNo))
            {
                Console.WriteLine("Invoice No cannot be empty.");
                return;
            }
            Console.Write("Enter Customer Name: ");
            t.CustomerName = Console.ReadLine();

            Console.Write("Enter Item Name: ");
            t.ItemName = Console.ReadLine();

            Console.Write("Enter Quantity: ");
            t.Quantity = Console.ReadLine();
            if (t.Quantity <= 0)
            {
                Console.WriteLine("Quantity must be greater than zero.");
                return;
            }
            Console.Write("Enter Purchase Amount (total): ");
            t.PurchaseAmount= Console.ReadLine();
            if (t.PurchaseAmount <= 0)
            {
                Console.WriteLine("Purchase Amount must be greater than zero.");
                return;
            }
            Console.Write("Enter Selling Amount (total): ");
            if (t.SellingAmount < 0)
            {
                Console.WriteLine("Selling Amount cannot be negative.");
                return;
            }
            
            CalculateProfitLoss(t);
            LastTransaction = t;
            HasLastTransaction = true; // transaction created successfully and saved
            Console.WriteLine("Transaction saved successfully.");
            PrintCalculation(t); // printing the succesful transaction
        }
        public void ViewTransaction() // print last transaction details
        {
            if (!HasLastTransaction)
            {
                Console.WriteLine("No transaction available. Please create a new transaction first.");
                return;
            }

            SaleTransaction t = LastTransaction;
            Console.WriteLine("-------------- Last Transaction --------------");
            Console.WriteLine($"Invoice No: {t.InvoiceNo}");
            Console.WriteLine($"Customer: {t.CustomerName}");
            Console.WriteLine($"Item: {t.ItemName}");
            Console.WriteLine($"Quantity: {t.Quantity}");
            Console.WriteLine($"Purchase Amount: {t.PurchaseAmount:F2}");
            Console.WriteLine($"Selling Amount: {t.SellingAmount:F2}");
            Console.WriteLine($"Status: {t.ProfitOrLossStatus}");
            Console.WriteLine($"Profit/Loss Amount: {t.ProfitOrLossAmount:F2}");
            Console.WriteLine($"Profit Margin (%): {t.ProfitMarginPercent:F2}");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("------------------------------------------------------");
        }
        public void CalculateProfitOrLoss()
        {
            if (!HasLastTransaction)
            {
                Console.WriteLine("No transaction available. Please create a new transaction first.");
                return;
            }
            CalculateProfitLoss(LastTransaction);

            Console.WriteLine("\nRecalculation completed successfully.");
            PrintCalculation(LastTransaction);
        }
        private void CalculateProfitLoss(SaleTransaction t)
        {
            if (t.SellingAmount > t.PurchaseAmount)// profit
            {
                t.ProfitOrLossStatus = "PROFIT";
                t.ProfitOrLossAmount = t.SellingAmount - t.PurchaseAmount;
            }
            else if (t.SellingAmount < t.PurchaseAmount)//loss
            {
                t.ProfitOrLossStatus = "LOSS";
                t.ProfitOrLossAmount = t.PurchaseAmount - t.SellingAmount;
            }
            else // neither
            {
                t.ProfitOrLossStatus = "BREAK-EVEN";
                t.ProfitOrLossAmount = 0;
            }
            t.ProfitMarginPercent = (t.ProfitOrLossAmount / t.PurchaseAmount) * 100;  // calculating the profit percentage
        }

        private void PrintCalculation(SaleTransaction t) // print profit/loss calculation transcation
        {
            Console.WriteLine($"Status               : {t.ProfitOrLossStatus}");
            Console.WriteLine($"Profit/Loss Amount   : {t.ProfitOrLossAmount:F2}");
            Console.WriteLine($"Profit Margin (%)    : {t.ProfitMarginPercent:F2}");
            Console.WriteLine("------------------------------------------------------");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TransactionService service = new TransactionService(); // create service object
            bool running = true; // running till option 4 is chosen

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
                    case 1:
                        service.CreateTransaction();
                        break;
                    case 2:
                        service.ViewTransaction();
                        break;
                    case 3:
                        service.CalculateProfitOrLoss();
                        break;
                    case 4:
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
