using System;
using System.Linq;



namespace BookStore
{
    internal class Program
    {
        /* 
         * Kitap Mağazası Uygulaması
         * Kitap, Kasa
         * 1 - Kitap Kayıt Edebilmeli
         *      -Kayıt Esnasında Kitap Adı, Adedi, Maliyet fiyatı, Vergisi, kazanç miktarı vs.
         *      -Ürün fiyatı maliyet fiyatı , vergi ve kazanç miktarına bağlı olarak hesaplanır.
         * 2 - Kitap Silebilmeli
         *      -Kitap silme fonksiyonu seçildiğinde girilen adet kadar kitap silinecektir.
         * 3 - Kitap Güncellemeli
         * 4 - Kitap Satış
         *      -Satılan Kitap fiyatı kadar kasaya gelir olarak giriş yapılır
         *      -Satılan kitap kitap listesinden eksiltilir.
         * 5 - Kitap Listesi
         * 6 - Kitap Listesinden arama kabiliyeti
         * 
         * Kitap -> id, adi, türü (enum kullan), maliyet fiyatı, toplam vergi, stok adedi, kayıt tarihi, güncelleme tarihi
         * Kasa  -> işlem id'si , türü (gelir , gider // enum kullan), tutar, kayıt tarihi
         */
        static void Main(string[] args)
        {
            Console.SetWindowSize(160, 50);
            GenerateRandomBooks(100);
            ManagementConsole managementConsole = new ManagementConsole();
            managementConsole.Start();
        }
        static void GenerateRandomBooks(int _qty)
        {
            int sayac = 0;
            Random random = new Random();
            while (sayac < _qty)
            {
                Book book = new Book("Book " + (sayac + 1), "Author" + (sayac + 1), (BookTypeEnums)random.Next(1, 4), random.Next(10, 200), random.Next(1, 10), random.Next(1, 5), random.Next(1, 10));
                Book.AddBook(book);
                sayac++;
            }
        }
    }
}
