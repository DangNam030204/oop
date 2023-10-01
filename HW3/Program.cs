using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HW3
{
    public class Employee
    {
        public string Name { get; set; }      
    }

    public class Item
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }       
    }
    public class BillLine
    {
        public Item Item { get; set; }
        public int Quantity { get; set; }
       
    }

    public class GroceryBill
    {
        private static int nextReceiptNumber = 1;
        private int receiptNumber;
        private Employee clerk;
        protected List<BillLine> billLines;
        public GroceryBill(Employee clerk )
        {
            this.receiptNumber = nextReceiptNumber++;
            this.clerk = clerk;
            billLines = new List<BillLine>();
        }
        public int ReceiptNumber
        {
            get { return receiptNumber; }
        }
        public void Add(BillLine billLine)
        {
            billLines.Add(billLine);
        }
        public double GetTotal()
        {
            double total = 0;
            foreach (var billLine in billLines)
            {
                total += billLine.Quantity * (billLine.Item.Price - (billLine.Item.Price* billLine.Item.Discount/100));
            }
            return total;
        }
        public void PrintReceipt()
        {
            DateTime now = DateTime.Now;
            Console.WriteLine("\t\t\t\t\t\t\tHÓA ĐƠN THANH TOÁN");
            Console.Write("\t\t\t\t\tMã hóa đơn : " + ReceiptNumber);
            Console.WriteLine("\t\tThời gian: " + now);
            Console.WriteLine("\t\t\t\tHóa đơn:");
            Console.WriteLine();
            Console.WriteLine($"\t\t\t\t|Tên Hàng Hóa | Đơn Giá($) | Số Lượng | Chiết khấu(%) |   Tổng   |");
            Console.WriteLine("\t\t\t\t-------------------------------------------------------------------");            
            foreach (var billLine in billLines)
            {
                double sum = 0;
                sum += billLine.Quantity * (billLine.Item.Price - (billLine.Item.Price * billLine.Item.Discount / 100));
                Console.WriteLine($"\t\t\t\t|{billLine.Item.Name,12} | {billLine.Item.Price,9}  | {billLine.Quantity,8} | {billLine.Item.Discount,13} | {sum,7}  |");
                Console.WriteLine("\t\t\t\t------------------------------------------------------------------");
            }           
            GetTotal();
            Console.WriteLine($"\t\t\t\t|Tổng số tiền\t\t\t\t\t      | {GetTotal(),7}  | \n\n");          
        }
    }
    public class DiscountBill : GroceryBill
    {
        private bool isPreferredCustomer;
        private int discountCount;
        private double discountAmount;
        private double discountPercent;
        public DiscountBill(Employee clerk, bool isPreferredCustomer) : base(clerk)
        {
            this.isPreferredCustomer = isPreferredCustomer;
        }
        public int GetDiscountCount()
        {
            return discountCount;
        }
        public double GetDiscountAmount()
        {
            return discountAmount;
        }
        public double GetDiscountPercent()
        {
            return discountPercent;
        }
        public new double GetTotal()
        {
            double total = base.GetTotal();
            if (isPreferredCustomer)
            {
                foreach (var billLine in billLines)
                {
                    if (billLine.Item.Discount > 0)
                    {
                        discountCount += billLine.Quantity;
                        discountAmount += billLine.Quantity * billLine.Item.Discount;
                    }
                }
                discountPercent = (discountAmount / total) * 100;
                total -= discountAmount;
            }
            return total;
        }
    }
    public class Program
    {
       static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            Employee clerk = new Employee { Name = "Dang Nam" };
            GroceryBill groceryBill = new GroceryBill(clerk);       
            while (true)
            {
                Console.WriteLine("Nhập thông tin hàng hóa:");
                Console.Write("Tên hàng hóa <ENTER để ngừng>: ");
                string itemName = Console.ReadLine();
                if (itemName == "") break;                                
                double itemPrice;
                do
                {
                    Console.Write("Đơn giá: ");
                    string inputPrice = Console.ReadLine();
                    if (double.TryParse(inputPrice, out itemPrice) && itemPrice > 0)
                    {
                        break;
                    }
                    Console.WriteLine("Vui lòng nhập lại đơn giá (số nguyên lớn hơn 0).");
                } while (true);
                double itemDiscount;
                do
                {
                    Console.Write("Chiết khấu (%): ");
                    string inputDiscount = Console.ReadLine();
                    if (double.TryParse(inputDiscount, out itemDiscount) && itemDiscount >= 0 && itemDiscount <= 100)
                    {
                        break;
                    }
                    Console.WriteLine("Vui lòng nhập lại chiết khấu (số nguyên từ 0 đến 100).");
                } while (true);
                int itemQuantity;
                do
                {
                    Console.Write("Số lượng: ");
                    string inputQuantity = Console.ReadLine();
                    if (int.TryParse(inputQuantity, out itemQuantity) && itemQuantity > 0)
                    {
                        break;
                    }
                    Console.WriteLine("Vui lòng nhập lại số lượng (số nguyên lớn hơn 0).");
                } while (true);
                Item newItem = new Item { Name = itemName, Price = itemPrice, Discount = itemDiscount };
                BillLine newBillLine = new BillLine { Item = newItem, Quantity = itemQuantity };
                groceryBill.Add(newBillLine);
                Console.Clear();
            }
            Console.Clear();
            groceryBill.PrintReceipt();
            Console.ReadKey();
        }
    }
}
    