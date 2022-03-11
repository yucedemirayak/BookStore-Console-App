using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore
{
    public class TransactionClass : BaseClass
    {
        public static List<TransactionClass> TransactionList = new List<TransactionClass>();
        public TransactionTypeEnums TransactionType { get; set; }
        public double Amount { get; set; }
        
        public TransactionClass(double Amount, TransactionTypeEnums TransactionType)
        {
            this.Amount = Amount;
            this.TransactionType = TransactionType;
        }

        public static void AddTransaction(TransactionClass Transaction)
        {
            TransactionList.Add(Transaction);
        }

        public static double CalculateAmount(double Price, int qty)
        {
            return Price * qty;
        }

        public static void ListTransactions()
        {
            var table = new ConsoleTable();
            string[] headers = { "Transaction Id", "Transaction Type", "Amount", "Updated Time" };
            table.SetHeaders(headers);
            foreach (TransactionClass transaction in TransactionList)
            {
                table.AddRow(transaction.CreateArray());
            }
            Console.WriteLine(table.ToString());
        }

        public override string ToString()
        {
            return String.Format($"Id : {Id} , Transaction : {TransactionType} , Amount : {Amount} , Updated Time : {UpdatedTime}");
        }

        public string[] CreateArray()
        {
            string[] transactionArray = { Id.ToString(), TransactionType.ToString() , Amount.ToString() , UpdatedTime.ToString() };
            return transactionArray;
        }
    }
}
