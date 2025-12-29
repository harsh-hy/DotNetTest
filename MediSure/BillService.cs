namespace MediSure
{
    public class BillingService // service to handle billing
    {
        // static fields to hold last bill details
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
        public void ViewBill() // print last bill details
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
        private void CalculateBill(PatientBill bill) // method to calculate bill amounts
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
        private void PrintCalculation(PatientBill bill) // method to print bill details
        {
            //printing the bill details after calculation
            Console.WriteLine($"Gross Amount: {bill.GrossAmount:F2}");
            Console.WriteLine($"Discount: {bill.DiscountAmount:F2}");
            Console.WriteLine($"Final Payable: {bill.FinalPayable:F2}");
            Console.WriteLine("------------------------------------------------------------");
        }
    }

}