namespace MediSure
{
    public class PatientBill
    {
        public string BillId { get; set; }
        public string PatientName { get; set; }
        public bool HasInsurance { get; set; }
        public decimal ConsultationFee { get; set; }
        public decimal LabCharges { get; set; }
        public decimal MedicineCharges { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalPayable { get; set; }
    }
    public class BillingService
    {
        public static PatientBill LastBill;
        public static bool HasLastBill = false;
        public void CreateBill()
        {
            PatientBill bill=new PatientBill(); // creating a new bill object
            //Taking inputs from the user in the given format ans saving to the bill object
            Console.Write("Enter Bill ID: ");
            bill.BillId = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(bill.BillId))
            {
                Console.WriteLine("Bill ID cannot be empty.");
                return;
            }
            Console.Write("Enter Patient Name: ");
            bill.PatientName = Console.ReadLine();
            Console.Write("Is the patient insured? (Y/N): ");
            string insuranceInput = Console.ReadLine();
            bill.HasInsurance = (insuranceInput == "Y" || insuranceInput == "y");
            Console.Write("Enter Consultation Fee: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal consultation) || consultation <= 0)
            {
                Console.WriteLine("Consultation Fee must be greater than zero.");
                return;
            }
            bill.ConsultationFee = consultation;

            Console.Write("Enter Lab Charges: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal lab) || lab < 0)
            {
                Console.WriteLine("Lab Charges cannot be negative.");
                return;
            }
            bill.LabCharges = lab;

            Console.Write("Enter Medicine Charges: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal medicine) || medicine < 0)
            {
                Console.WriteLine("Medicine Charges cannot be negative.");
                return;
            }
            bill.MedicineCharges = medicine;
            CalculateBill(bill);
            LastBill = bill; // saving the bill
            HasLastBill = true;// bill created successfully 
            Console.WriteLine("Bill created successfully.");
            PrintCalculation(bill); // forwarded to print tthe details
        }
        public void ViewBill()
        {
            if (!HasLastBill)// no biils are yet created
            {
                Console.WriteLine("No bill available. Please create a new bill first."); 
                return;
            }
            //bill is there 
            PatientBill b = LastBill;
            Console.WriteLine("----------- Last Bill -----------");
            Console.WriteLine($"BillId: {b.BillId}");
            Console.WriteLine($"Patient: {b.PatientName}");
            Console.WriteLine($"Insured: {(b.HasInsurance ? "Yes" : "No")}");
            Console.WriteLine($"Consultation Fee: {b.ConsultationFee:F2}");
            Console.WriteLine($"Lab Charges: {b.LabCharges:F2}");
            Console.WriteLine($"Medicine Charges: {b.MedicineCharges:F2}");
            Console.WriteLine($"Gross Amount: {b.GrossAmount:F2}");
            Console.WriteLine($"Discount Amount: {b.DiscountAmount:F2}");
            Console.WriteLine($"Final Payable: {b.FinalPayable:F2}");
            Console.WriteLine("--------------------------------");
        }
        // CLEAR LAST BILL
        public void ClearBill() // method to clear last bill
        {
            LastBill = null;
            HasLastBill = false;
            Console.WriteLine("Last bill cleared.");
        }
        private void CalculateBill(PatientBill bill)
        {
            bill.GrossAmount=bill.ConsultationFee+bill.LabCharges+bill.MedicineCharges; // calculating gross amount to be paid
            if (bill.HasInsurance)
            {
                bill.DiscountAmount = bill.GrossAmount * 0.10m;
            }
            else
            {
                bill.DiscountAmount = 0;
            }
            bill.FinalPayable = bill.GrossAmount - bill.DiscountAmount; // final amount after discount
        }
        private void PrintCalculation(PatientBill bill)
        {
            //printing the bill details after calculation
            Console.WriteLine($"Gross Amount: {bill.GrossAmount:F2}");
            Console.WriteLine($"Discount: {bill.DiscountAmount:F2}");
            Console.WriteLine($"Final Payable: {bill.FinalPayable:F2}");
            Console.WriteLine("------------------------------------------------------------");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            BillingService service = new BillingService();
            bool running = true;

            while (running)
            {
                Console.WriteLine("================== MediSure Clinic Billing ==================");
                Console.WriteLine("1.Create New Bill (Enter Patient Details)");
                Console.WriteLine("2.View Last Bill");
                Console.WriteLine("3 Clear Last Bill");
                Console.WriteLine("4.Exit");
                Console.Write("Enter Your Option: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        service.CreateBill();
                        break;
                    case "2":
                        service.ViewBill();
                        break;
                    case "3":
                        service.ClearBill();
                        break;
                    case "4":
                        running = false;
                        Console.WriteLine("Thank you. Application closed normally.");
                        break;
                    default:
                        Console.WriteLine("Invalid Option !! Try Again ");
                        break;
                }
            }
        }
    }
}