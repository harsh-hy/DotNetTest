namespace Quick
{
    // service class to handle transaction operations
    // create, view, calculate profit/loss
    public class TransactionService
    {
        public static SaleTransaction LastTransaction; // last transaction saved
        public static bool HasLastTransaction = false;// flag to check if last transaction exists

        public void CreateTransaction() // create new transaction
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
            if (!int.TryParse(Console.ReadLine(), out int quat) || quat <= 0)
            {
                Console.WriteLine("Quantity must be greater than zero.");
                return;
            }
            t.Quantity = quat;
            Console.Write("Enter Purchase Amount (total): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal purchase) || purchase <= 0)
            {
                Console.WriteLine("Purchase Amount must be greater than zero.");
                return;
            }
            t.PurchaseAmount = purchase;
            Console.Write("Enter Selling Amount (total): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal selling) || selling < 0)
            {
                Console.WriteLine("Selling Amount cannot be negative.");
                return;
            }
            t.SellingAmount = selling;

            CalculateProfitLoss(t);
            LastTransaction = t;
            HasLastTransaction = true; // transaction created successfully and saved
            Console.WriteLine("Transaction saved successfully.");
            PrintCalculation(t); // printing the succesful transaction 
        }
        public void ViewTransaction() // print last transaction details
        {
            if (!HasLastTransaction) // check if transaction exists
            {
                Console.WriteLine("No transaction available. Please create a new transaction first.");
                return;
            }

            SaleTransaction t = LastTransaction; // get last transaction
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
        public void CalculateProfitOrLoss() // recalculate profit/loss for last transaction
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
        private void CalculateProfitLoss(SaleTransaction t) // calculate profit/loss for a transaction
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

}