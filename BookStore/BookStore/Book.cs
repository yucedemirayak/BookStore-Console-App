using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BookStore
{
    public class Book : BaseClass
    {
        public static List<Book> books = new List<Book>();
        public string Name { get; set; }

        public string Author { get; set; } // Sonradan ekle !!
        public BookTypeEnums BookType { get; set; }
        public double CostPrice { get; set; }
        public int TaxPercantage { get; set; } = 8;
        public int ProfitMargin { get; set; }
        public int QTY { get; set; }
        public double Price { get; set; }



        public Book(string _name, string _Author, BookTypeEnums _bookType, double _costPrice, int _taxPercentage, int _profitMargin, int _qty)
        {
            Name = _name;
            Author = _Author;
            BookType = _bookType;
            CostPrice = Math.Round(_costPrice, 2, MidpointRounding.ToEven);
            TaxPercantage = _taxPercentage;
            ProfitMargin = _profitMargin;
            QTY = _qty;
            Price = calculateBookPrice(_costPrice, _taxPercentage, _profitMargin);
        }

        public static void AddBook(Book item)
        {
            try
            {
                books.Add(item);
                double amount = TransactionClass.CalculateAmount(item.CostPrice, item.QTY);
                TransactionClass buyTransaction = new TransactionClass(amount, TransactionTypeEnums.OUTCOME);
                TransactionClass.AddTransaction(buyTransaction);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata :" + ex.Message);
            }
        }
        public static void RemoveBookFromId(int _Id)
        {
            var itemToRemove = books.SingleOrDefault(x => x.Id == _Id);
            if (itemToRemove != null)
            {
                books.Remove(itemToRemove);
                Console.WriteLine($"\nId : {_Id} Deleted.");
            }
            else
            {
                Console.WriteLine($"\nBook Id : {_Id} was not found.");
            }
        }

        public static void UpdateBookFromId(int _Id, BookProperyEnums _Property, string _Value, BookTypeEnums _booktype = BookTypeEnums.Other)
        {
            var itemToUpdate = books.SingleOrDefault(x => x.Id == _Id);
            if (itemToUpdate != null)
            {
                books.Remove(itemToUpdate);
                switch (_Property)
                {
                    case BookProperyEnums.BookId:
                        itemToUpdate.Id = Convert.ToInt32(_Value);
                        break;
                    case BookProperyEnums.BookName:
                        itemToUpdate.Name = _Value;
                        break;
                    case BookProperyEnums.Author:
                        itemToUpdate.Author = _Value;
                        break;
                    case BookProperyEnums.BookType:
                        switch (_booktype)
                        {
                            case BookTypeEnums.Other:
                                itemToUpdate.BookType = BookTypeEnums.Other;
                                break;
                            case BookTypeEnums.Novel:
                                itemToUpdate.BookType = BookTypeEnums.Novel;
                                break;
                            case BookTypeEnums.Education:
                                itemToUpdate.BookType = BookTypeEnums.Education;
                                break;
                            case BookTypeEnums.Historical:
                                itemToUpdate.BookType = BookTypeEnums.Historical;
                                break;
                            case BookTypeEnums.Political:
                                itemToUpdate.BookType = BookTypeEnums.Political;
                                break;
                        }
                        break;
                    case BookProperyEnums.CostPrice:
                        itemToUpdate.CostPrice = Convert.ToDouble(_Value);
                        break;
                    case BookProperyEnums.TaxPercentage:
                        itemToUpdate.TaxPercantage = Convert.ToInt32(_Value);
                        break;
                    case BookProperyEnums.ProfitMargin:
                        itemToUpdate.ProfitMargin = Convert.ToInt32(_Value);
                        break;
                    case BookProperyEnums.Quantitiy:
                        itemToUpdate.QTY = Convert.ToInt32(_Value);
                        break;
                }
                books.Add(itemToUpdate);
            }
            else
            {
                Console.WriteLine("\nBook Id was not found.\n");
            }
        }
        public static void SellBook(int _Id, int _Quantity)
        {
            Book itemToSell = books.SingleOrDefault(x => x.Id == _Id);
            if ((itemToSell.QTY - _Quantity) >= 0)
            {
                books.Remove(itemToSell);
                itemToSell.QTY = itemToSell.QTY - _Quantity;
                books.Add(itemToSell);
                double salesAmount = TransactionClass.CalculateAmount(itemToSell.Price, _Quantity);
                TransactionClass sellTransaction = new TransactionClass(salesAmount, TransactionTypeEnums.INCOME);
                TransactionClass.AddTransaction(sellTransaction);
                Console.WriteLine($"Book Sold => Id : {itemToSell.Id} , Name : {itemToSell.Name} , The Remaning Quantity : {itemToSell.QTY}");
            }
            else if ((itemToSell.QTY - _Quantity) < 0)
            {
                Console.WriteLine("There are not enough books to sell.");
                Console.WriteLine($"Book Id : {itemToSell.Id} , Book Name : {itemToSell.Name} , Book Quantity : {itemToSell.QTY} , Quantity wanted to sell : {_Quantity}");
            }

        }
        public static void ListBook()
        {
            var table = new ConsoleTable();
            string[] bookTypeOptions = { "Book Id", "Name", "Author", "Book Type", "Cost Price", "Price", "Tax Percentage", "Profit Margin", "Quantity", "Updated Time" };
            table.SetHeaders(bookTypeOptions);
            foreach (Book book in books)
            {
                table.AddRow(book.CreateArray());
            }
            Console.WriteLine(table.ToString());
        }
        


        public static void SearchBook(BookProperyEnums _Property, string _Value, BookTypeEnums _BookType = BookTypeEnums.Other)
        {
            string[] bookTypeOptions = { "Book Id", "Name", "Author", "Book Type", "Cost Price", "Price", "Tax Percentage", "Profit Margin", "Quantity", "Updated Time" };
            var table = new ConsoleTable();
            table.SetHeaders(bookTypeOptions);
            switch (_Property)
            {
                case BookProperyEnums.BookId:
                    int searchId = Convert.ToInt32(_Value);
                    var itemToSearch = books.SingleOrDefault(x => x.Id == searchId);
                    if (itemToSearch != null)
                    {
                        var tableId = new ConsoleTable();
                        table.AddRow(itemToSearch.CreateArray());
                        Console.WriteLine(tableId.ToString());
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Book Id : {searchId} not found.");
                        break;
                    }
                case BookProperyEnums.BookName:
                    foreach (Book book in books)
                    {
                        if (book.Name.Contains(_Value))
                        {
                            table.AddRow(book.CreateArray());
                        }
                    }
                    itemToSearch = books.SingleOrDefault(x => x.Name == _Value);
                    if (itemToSearch != null)
                    {
                        Console.WriteLine(table.ToString());
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Book Name : {_Value} not found.");
                        break;
                    }
                case BookProperyEnums.BookType:
                    foreach (Book book in books)
                    {
                        if (book.BookType == _BookType)
                        {
                            table.AddRow(book.CreateArray());
                        }
                    }
                    if (table.ToString().Length <= ConsoleTable.MinTableLength)
                    {
                        Console.WriteLine($"Book Type : {_BookType} not found.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine(table.ToString());
                    }
                    break;
            }
        }

        private double calculateBookPrice(double costPrice, int tax, int profitMargin)
        {
            double taxPrice = (costPrice * tax) / 100;
            double profitPrice = (costPrice * profitMargin) / 100;
            double price = costPrice + taxPrice + profitPrice;
            return Math.Round(price, 2, MidpointRounding.ToEven);
        }
        public override string ToString()
        {
            return String.Format($"Id : {Id} , Name : {Name} , Author : {Author} , Type : {BookType} , Cost Price : {CostPrice} , Price : {Price} , Tax Percentage : {TaxPercantage} , Profit Margin : {ProfitMargin} , Quantity : {QTY} , Updated Time : {UpdatedTime}");
        }

        public string[] CreateArray()
        {
            string[] bookArray = { Id.ToString(), Name, Author, BookType.ToString(), CostPrice.ToString(), Price.ToString(), TaxPercantage.ToString(), ProfitMargin.ToString(), QTY.ToString(), UpdatedTime.ToString() };
            return bookArray;
        }
    }
}
