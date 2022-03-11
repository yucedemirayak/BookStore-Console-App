using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace BookStore
{
    public class ManagementConsole
    {
        int autoTaxPercentage = 8;
        int autoProfitMargin = 10;
        int autoQuantity = 1;
        public void Start()
        {


            string mainTitle = "BookStore - Management Console";
            Console.Title = mainTitle;
            RunMainMenu();
        }

        private void RunMainMenu()
        {
            string prompt = "Welcome to the BookStore Management Console\n" + "Use the arrow keys to cycle through the options and press enter to select an option. \n";
            string[] options = { "Book Operations", "Sales", "Book-List", "Options", "Exit" };
            MenuHorizontal mainMenu = new MenuHorizontal(prompt, options);
            int selectedIndex = mainMenu.Run();
            switch (selectedIndex)
            {
                case 0:
                    BookOperations();
                    break;
                case 1:
                    TransactionList();
                    break;
                case 2:
                    BookList();
                    break;
                case 3:
                    Options();
                    break;
                case 4:
                    Exit();
                    break;
            }
        }
        private void BookOperations()
        {
            string prompt = "--Book Operations-- \n";
            string[] options = { "Register", "Remove", "Update", "Back" };
            MenuHorizontal bookOperationsMenu = new MenuHorizontal(prompt, options);
            int selectedIndex = bookOperationsMenu.Run();
            switch (selectedIndex)
            {
                case 0:
                    Register();
                    break;
                case 1:
                    Remove();
                    break;
                case 2:
                    Update();
                    break;
                case 3:
                    RunMainMenu();
                    break;
            }
        }
        private void Register()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("--Register New Book-- \n");
                string promptContinue = "Do you want to continue ?";
                string[] options = { "Yes", "No" };
                LineMenu wantToContinue = new LineMenu(promptContinue, options);

                /* INPUT NAME */
                string inputName = null;
                do
                {
                    Console.Write("Name : ");
                    inputName = Console.ReadLine();
                    if (inputName == "")
                    {
                        Console.WriteLine("The name of the book was not entered.");
                        int selectedIndex = wantToContinue.Run();
                        if (selectedIndex == 0)
                        {
                            continue;
                        }
                        else
                        {
                            BookOperations();
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                } while (true);

                /* INPUT AUTHOR */
                string inputAuthor = null;
                do
                {
                    Console.Write("Author : ");
                    inputAuthor = Console.ReadLine();
                    if (inputAuthor == "")
                    {
                        Console.WriteLine("The author of the book was not entered.");
                        int selectedIndex = wantToContinue.Run();
                        if (selectedIndex == 0)
                        {
                            continue;
                        }
                        else
                        {
                            BookOperations();
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                } while (true);

                /* INPUT TYPE */
                string prompt = "Type : ";
                var getBookTypes = Enum.GetValues(typeof(BookTypeEnums));
                string[] bookTypeOptions = getBookTypes.OfType<object>().Select(x => x.ToString()).ToArray();
                LineMenu BookTypeMenu = new LineMenu(prompt, bookTypeOptions);
                int newBookType = BookTypeMenu.Run();

                /* INPUT COST PRICE */
                double inputCostPrice = 0;
                do
                {
                    Console.Write("Cost Price : ");
                    inputCostPrice = ConsoleOnly.TypeDouble();
                    if (inputCostPrice == 0)
                    {
                        Console.WriteLine("The cost price of the book was not entered.");
                        int selectedIndex = wantToContinue.Run();
                        if (selectedIndex == 0)
                        {
                            continue;
                        }
                        else
                        {
                            BookOperations();
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                while (true);

                /* INPUT TAX */
                int inputTax;
                do
                {
                    Console.Write("Tax Percentage : ");
                    inputTax = ConsoleOnly.TypeInt();
                    if (inputTax == 0)
                    {
                        Console.WriteLine($"Tax percentage automatically taken as {autoTaxPercentage}");
                        inputTax = autoTaxPercentage;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                while (true);

                /* INPUT PROFIT MARGIN */
                int inputProfitMargin;
                do
                {
                    Console.Write("Profit Margin");
                    inputProfitMargin = ConsoleOnly.TypeInt();
                    if (inputProfitMargin == 0)
                    {
                        Console.WriteLine($"Profit margin automaticall taken as {autoProfitMargin}");
                        inputProfitMargin = autoProfitMargin;
                        break;
                    }
                    else
                    {
                        break;
                    }
                } while (true);

                /* INPUT QUANTITY */
                int inputQuantity;
                do
                {
                    Console.Write("Quantity : ");
                    inputQuantity = ConsoleOnly.TypeInt();
                    if (inputQuantity == 0)
                    {
                        Console.WriteLine($"Quantity automatically taken as {autoQuantity}");
                        inputQuantity = autoQuantity;
                        break;
                    }
                    else
                    {
                        break;
                    }
                } while (true);

                Book book = new Book(inputName, inputAuthor, (BookTypeEnums)newBookType, Convert.ToInt32(inputCostPrice), Convert.ToInt32(inputTax), Convert.ToInt32(inputProfitMargin), Convert.ToInt32(inputQuantity));
                Book.AddBook(book);
                Console.WriteLine("--Registered Book Info--");
                Console.WriteLine(book + "\n");

                string YesOrNoRegisterPrompt = "Do you want to register another book ?";
                string[] YesOrNoOptions = { "Yes", "No" };
                LineMenu YesorNoMenu = new LineMenu(YesOrNoRegisterPrompt, YesOrNoOptions);
                int selectedOptionIndex = YesorNoMenu.Run();

                if (selectedOptionIndex == 0)
                {
                    continue;
                }
                else if (selectedOptionIndex == 1)
                {
                    break;
                }
            } while (true);
            BookOperations();
        }
        private void Remove()
        {
            string promptContinue = "Do you want to continue ?";
            string[] options = { "Yes", "No" };
            LineMenu wantToContinue = new LineMenu(promptContinue, options);
            do
            {
                Console.Clear();
                Console.WriteLine("--Remove Book--\n");
                int bookId;
                do
                {
                    Console.Write("Book Id : ");
                    bookId = ConsoleOnly.TypeInt();
                    if (bookId == 0)
                    {
                        Console.WriteLine("Id was not entered. Please enter the id of the book to be deleted.");

                    }
                    else if (!(bookId >= Book.MIN_ID && bookId <= Book.MAX_ID))
                    {
                        Console.WriteLine("The entered Id was not valid. Book Id must be 6 digits.");
                    }
                    else
                    {
                        Book.RemoveBookFromId(bookId);
                        string YesOrNoPrompt = "Do you want to remove another book ?";
                        string[] YesOrNoOptions = { "Yes", "No" };
                        LineMenu YesorNoMenu = new LineMenu(YesOrNoPrompt, YesOrNoOptions);
                        int selectedYesOrNoOptionIndex = YesorNoMenu.Run();
                        if (selectedYesOrNoOptionIndex == 0)
                        {
                            continue;
                        }
                        else if (selectedYesOrNoOptionIndex == 1)
                        {
                            BookOperations();
                            break;
                        }
                    }
                    int selectedIndex = wantToContinue.Run();
                    if (selectedIndex == 0)
                    {
                        continue;
                    }
                    else
                    {
                        BookOperations();
                        break;
                    }
                } while (true);

            } while (true);
        }
        private void Update()
        {
            do
            {
                string promptContinue = "Do you want to continue ?";
                string[] options = { "Yes", "No" };
                LineMenu wantToContinue = new LineMenu(promptContinue, options);
                Console.Clear(); ;
                Console.WriteLine("--Update Book--");
                int bookId;
                do
                {
                    Console.Write("Book Id : ");
                    bookId = ConsoleOnly.TypeInt();
                    if (bookId == 0)
                    {
                        Console.WriteLine("Id was not entered. Please enter the id of the book to be updated.");
                    }
                    else if (!(bookId >= Book.MIN_ID && bookId <= Book.MAX_ID))
                    {
                        Console.WriteLine("The entered Id was not valid. Book Id must be 6 digits.");
                    }
                    else
                    {
                        var bookToUpdateId = Book.books.SingleOrDefault(x => x.Id == Convert.ToInt32(bookId));
                        if (bookToUpdateId != null)
                        {
                            Console.WriteLine("Book found.");
                            Console.WriteLine($"\n{bookToUpdateId}\n");
                            string promptUpdate = "Which variable would you like to update ?";
                            var getBookProperties = Enum.GetValues(typeof(BookProperyEnums));
                            string[] optionsUpdate = getBookProperties.OfType<object>().Select(x => x.ToString()).ToArray();
                            LineMenu bookPropertyMenu = new LineMenu(promptUpdate, optionsUpdate);
                            int selectedPropertyIndex = bookPropertyMenu.Run();
                            switch (selectedPropertyIndex)
                            {
                                case 0:
                                    int newBookId;
                                    do
                                    {
                                        Console.Write("New Book Id : ");
                                        newBookId = ConsoleOnly.TypeInt();
                                        if (newBookId == 0)
                                        {
                                            Console.WriteLine("Id was not entered. Please enter the new id of the book to be updated.");
                                        }
                                        else if (!(bookId >= Book.MIN_ID && bookId <= Book.MAX_ID))
                                        {
                                            Console.WriteLine("The entered Id was not valid. Book Id must be 6 digits.");
                                        }
                                        else
                                        {
                                            Book.UpdateBookFromId(bookId, BookProperyEnums.BookId, newBookId.ToString());
                                            break;
                                        }
                                        int selectedIndexId = wantToContinue.Run();
                                        if (selectedIndexId == 0)
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            BookOperations();
                                            break;
                                        }

                                    } while (true);
                                    break;
                                case 1:
                                    string newBookName;
                                    do
                                    {
                                        Console.Write("New Name : ");
                                        newBookName = Console.ReadLine();
                                        if (newBookName == "")
                                        {
                                            Console.WriteLine("The new name of the book was not entered.");
                                            int selectedIndexName = wantToContinue.Run();
                                            if (selectedIndexName == 0)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                BookOperations();
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            Book.UpdateBookFromId(bookId, BookProperyEnums.BookName, newBookName);
                                            break;
                                        }
                                    } while (true);
                                    break;
                                case 2:
                                    string newBookAuthor;
                                    do
                                    {
                                        Console.Write("Author : ");
                                        newBookAuthor = Console.ReadLine();
                                        if (newBookAuthor == "")
                                        {
                                            Console.WriteLine("The new author of the book was not entered.");
                                            int selectedIndexAuthor = wantToContinue.Run();
                                            if (selectedIndexAuthor == 0)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                BookOperations();
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            Book.UpdateBookFromId(bookId, BookProperyEnums.Author, newBookAuthor);
                                            break;
                                        }
                                    } while (true);
                                    break;
                                case 3:
                                    string bookTypePrompt = "New Book Type : ";
                                    var getBookTypes = Enum.GetValues(typeof(BookTypeEnums));
                                    string[] bookTypeOptions = getBookTypes.OfType<object>().Select(x => x.ToString()).ToArray();
                                    LineMenu BookTypeMenu = new LineMenu(bookTypePrompt, bookTypeOptions);
                                    int newBookType = BookTypeMenu.Run();
                                    Book.UpdateBookFromId(bookId, BookProperyEnums.BookType, null, (BookTypeEnums)newBookType);
                                    break;
                                case 4:
                                    double newCostPrice;
                                    do
                                    {
                                        Console.Write("New Cost Price : ");
                                        newCostPrice = ConsoleOnly.TypeDouble();
                                        if (newCostPrice >= 0)
                                        {
                                            Console.WriteLine("The entered cost price was not valid. Please enter the cost price more than zero.");
                                        }
                                        else
                                        {
                                            Book.UpdateBookFromId(bookId, BookProperyEnums.CostPrice, newCostPrice.ToString());
                                            break;
                                        }
                                        int selectedIndexCostPrice = wantToContinue.Run();
                                        if (selectedIndexCostPrice == 0)
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            BookOperations();
                                            break;
                                        }
                                    } while (true);
                                    break;
                                case 5:
                                    int newTaxPercentage;
                                    do
                                    {
                                        Console.Write("New Tax Percentage : ");
                                        newTaxPercentage = ConsoleOnly.TypeInt();
                                        if (newTaxPercentage > 0)
                                        {
                                            Console.WriteLine("The entered new tax percentage was not valid.Tax percentage cannot be negative number.");
                                            int selectedIndexTax = wantToContinue.Run();
                                            if (selectedIndexTax == 0)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                BookOperations();
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            Book.UpdateBookFromId(bookId, BookProperyEnums.TaxPercentage, newTaxPercentage.ToString());
                                            break;
                                        }
                                    } while (true);
                                    break;
                                case 6:
                                    int newProfitMargin;
                                    do
                                    {
                                        Console.Write("New Profit Margin : ");
                                        newProfitMargin = ConsoleOnly.TypeInt();
                                        if (newProfitMargin > 0)
                                        {
                                            Console.WriteLine("The entered new profit margin was not valid.Profit margin cannot be negative number.");
                                            int selectedIndexProfitMargin = wantToContinue.Run();
                                            if (selectedIndexProfitMargin == 0)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                BookOperations();
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            Book.UpdateBookFromId(bookId, BookProperyEnums.ProfitMargin, newProfitMargin.ToString());
                                            break;
                                        }
                                    } while (true);
                                    break;
                                case 7:
                                    int newQuantity;
                                    do
                                    {
                                        Console.Write("New Profit Margin : ");
                                        newQuantity = ConsoleOnly.TypeInt();
                                        if (newQuantity > 0)
                                        {
                                            Console.WriteLine("The entered new quantity was not valid.Quantity cannot be negative number.");
                                            int selectedIndexQuantity = wantToContinue.Run();
                                            if (selectedIndexQuantity == 0)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                BookOperations();
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            Book.UpdateBookFromId(bookId, BookProperyEnums.Quantitiy, newQuantity.ToString());
                                            break;
                                        }
                                    } while (true);
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"\nBook Id : {bookToUpdateId} was not found.\n");
                        }
                    }
                    int selectedIndex = wantToContinue.Run();
                    if (selectedIndex == 0)
                    {
                        continue;
                    }
                    else
                    {
                        BookOperations();
                        break;
                    }
                } while (true);
                string YesOrNoPrompt = "Do you want to update another book ?";
                string[] YesOrNoOptions = { "Yes", "No" };
                LineMenu YesorNoMenu = new LineMenu(YesOrNoPrompt, YesOrNoOptions);
                int selectedOptionIndex = YesorNoMenu.Run();
                if (selectedOptionIndex == 0)
                {
                    continue;
                }
                else if (selectedOptionIndex == 1)
                {
                    BookOperations();
                    break;
                }
            } while (true);
        }
        private void Sales()
        {
            string promptContinue = "Do you want to continue ?";
            string[] options = { "Yes", "No" };
            LineMenu wantToContinue = new LineMenu(promptContinue, options);
            Console.Clear();
            Console.WriteLine("--Sales--\n");
            do
            {
                int bookId;
                do
                {
                    Console.WriteLine("Enter the Id of the book to be sold.");
                    Console.Write("Book Id : ");
                    bookId = ConsoleOnly.TypeInt();
                    Book itemToSell = Book.books.SingleOrDefault(x => x.Id == bookId);
                    if (bookId == 0)
                    {
                        Console.WriteLine("Id was not entered. Please enter the id of the book to be sold.");
                    }
                    else if (!(bookId >= Book.MIN_ID && bookId <= Book.MAX_ID))
                    {
                        Console.WriteLine("The entered Id was not valid. Book Id must be 6 digits.");
                    }
                    else if (itemToSell == null)
                    {
                        Console.WriteLine($"Book Id : {bookId} not found.");
                    }
                    else
                    {
                        break;
                    }
                    int selectedIndex = wantToContinue.Run();
                    if (selectedIndex == 0)
                    {
                        continue;
                    }
                    else
                    {
                        RunMainMenu();

                        break;
                    }
                } while (true);
                int quantity;
                do
                {
                    Console.WriteLine("How many books to sell ?");
                    Console.Write("Quantity : ");
                    quantity = ConsoleOnly.TypeInt();
                    if (quantity == 0)
                    {
                        Console.WriteLine("Quantity must be more than zero");
                        int selectedIndex = wantToContinue.Run();
                        if (selectedIndex == 0)
                        {
                            continue;
                        }
                        else
                        {
                            RunMainMenu();
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                } while (true);
                Book.SellBook(bookId, quantity);
                string YesOrNoPrompt = "Do you want to sell another book ?";
                string[] YesOrNoOptions = { "Yes", "No" };
                LineMenu YesorNoMenu = new LineMenu(YesOrNoPrompt, YesOrNoOptions);
                int selectedYesOrNoOptionIndex = YesorNoMenu.Run();
                if (selectedYesOrNoOptionIndex == 0)
                {
                    continue;
                }
                else if (selectedYesOrNoOptionIndex == 1)
                {
                    RunMainMenu();
                    break;
                }
            } while (true);
        }
        private void TransactionList()
        {
            Console.Clear();
            Console.WriteLine("--Transaction List--");
            TransactionClass.ListTransactions();
            string prompt = "Do you want to sell book ?";
            string[] options = { "Yes", "No" };
            LineMenu menu = new LineMenu(prompt, options);
            int index = menu.Run();
            switch (index)
            {
                case 0:
                    Sales();
                    break;
                case 1:
                    RunMainMenu();
                    break;
            }
        }

        private void BookList()
        {
            Console.Clear();
            Console.WriteLine("--Book List--");
            Book.ListBook();

            do
            {
                string promptContinue = "Do you want to continue ?";
                string[] options = { "Yes", "No" };
                LineMenu wantToContinue = new LineMenu(promptContinue, options);
                string YesOrNoPrompt = "Do you want to search book ?";
                string[] YesOrNoOptions = { "Yes", "No" };
                LineMenu YesorNoMenu = new LineMenu(YesOrNoPrompt, YesOrNoOptions);
                int selectedYesOrNoOptionIndex = YesorNoMenu.Run();
                if (selectedYesOrNoOptionIndex == 0)
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("--Book Search--");
                        string promptSearch = "Which variable do you want to search with ? :";
                        string[] optionsSearch = { "Id", "Name", "Type" };
                        LineMenu searchOptionsMenu = new LineMenu(promptSearch, optionsSearch);
                        int selectedSearchOption = searchOptionsMenu.Run();
                        switch (selectedSearchOption)
                        {
                            case 0:
                                int bookId;
                                do
                                {
                                    Console.Write("Book Id : ");
                                    bookId = ConsoleOnly.TypeInt();
                                    if (bookId == 0)
                                    {
                                        Console.WriteLine("Id was not entered. Please enter the new id of the book to be searched.");
                                        break;
                                    }
                                    else if (!(bookId >= Book.MIN_ID && bookId <= Book.MAX_ID))
                                    {
                                        Console.WriteLine("The entered Id was not valid. Book Id must be 6 digits.");
                                        break;
                                    }
                                    else
                                    {
                                        Book.SearchBook(BookProperyEnums.BookId, bookId.ToString());
                                        break;
                                    }

                                } while (true);
                                break;
                            case 1:
                                string searchName;
                                do
                                {
                                    Console.Write("Book Name : ");
                                    searchName = Console.ReadLine();
                                    if (searchName == "")
                                    {
                                        Console.WriteLine("The new name of the book was not entered.");
                                        break;
                                    }
                                    else
                                    {
                                        Book.SearchBook(BookProperyEnums.BookName, searchName);
                                        break;
                                    }
                                } while (true);
                                break;
                            case 2:
                                string bookTypePrompt = "Book Type : ";
                                var getBookTypes = Enum.GetValues(typeof(BookTypeEnums));
                                string[] bookTypeOptions = getBookTypes.OfType<object>().Select(x => x.ToString()).ToArray();
                                LineMenu BookTypeMenu = new LineMenu(bookTypePrompt, bookTypeOptions);
                                int searchBookType = BookTypeMenu.Run();
                                switch (searchBookType)
                                {
                                    case 0:
                                        Book.SearchBook(BookProperyEnums.BookType, null, BookTypeEnums.Other);
                                        break;
                                    case 1:
                                        Book.SearchBook(BookProperyEnums.BookType, null, BookTypeEnums.Novel);
                                        break;
                                    case 2:
                                        Book.SearchBook(BookProperyEnums.BookType, null, BookTypeEnums.Education);
                                        break;
                                    case 3:
                                        Book.SearchBook(BookProperyEnums.BookType, null, BookTypeEnums.Historical);
                                        break;
                                    case 4:
                                        Book.SearchBook(BookProperyEnums.BookType, null, BookTypeEnums.Political);
                                        break;
                                }
                                break;
                        }
                        int selectedIndex = wantToContinue.Run();
                        if (selectedIndex == 0)
                        {
                            continue;
                        }
                        else
                        {
                            RunMainMenu();
                            break;
                        }
                    } while (true);
                }

                else if (selectedYesOrNoOptionIndex == 1)
                {
                    RunMainMenu();
                }
            } while (true);
        }

        private void Options()
        {
            Console.Clear();
            Console.WriteLine("--Options--");
            string promptContinue = "Do you want to continue ?";
            string[] optionContinue = { "Yes", "No" };
            string prompt = "Automatically taken values :";
            string[] options = { $"Tax Percentage = {autoTaxPercentage}" , $"Profit Margin = {autoProfitMargin}" , $"Quantity = {autoQuantity}" , "Back" };
            MenuVertical optionsMenu = new MenuVertical(prompt, options);
            LineMenu wantToContinue = new LineMenu(promptContinue, optionContinue);
            do
            {
                Console.Clear();
                int index = optionsMenu.Run();
                switch (index)
                {
                    case 0:
                        Console.Write("New Auto Tax Percentage : ");
                        autoTaxPercentage = ConsoleOnly.TypeInt();

                        break;
                    case 1:
                        Console.Write("New Auto Profit Margin : ");
                        autoProfitMargin = ConsoleOnly.TypeInt();
                        break;
                    case 2:
                        Console.Write("New Auto Quantity : ");
                        autoQuantity = ConsoleOnly.TypeInt();
                        break;
                    case 3:
                        RunMainMenu();
                        break;
                }
                switch (wantToContinue.Run())
                {
                    case 0:
                        continue;
                    case 1:
                        RunMainMenu();
                        break;
                }
            } while (true);            
        }
        private void Exit()
        {
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey(true);
            Environment.Exit(0);
        }
    }
}
