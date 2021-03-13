using Data;
using Models;
using System;
using System.Collections.Generic;

namespace Services
{
    public class ProductPurchaseService
    {
        public static void Purchase(ICollection<Product> products)
        {
            Console.Write("Введите Артикул необходимого товара: ");
            Guid.TryParse(Console.ReadLine(), out var item);
            foreach (var product in products)
            {
                if (item == product.Id)
                {
                    Console.Clear();
                    Console.WriteLine($"\nКатегория: {product.Category}\nЖанр: {product.GenreName.Name}\nНазвание: {product.Name}\nАвтор: {product.AuthorName.Name}\nЦена: {product.Price}\nКол-во на складе: {product.Quantity}\nРейтинг: {product.Rating}");
                    Console.WriteLine("\nЕсли Вы хотите купить товар, нажмите Enter. Иначе нажмите Escape");
                    if (Console.ReadKey().Key == ConsoleKey.Enter)
                    {
                        if (product.Quantity > 0)
                        {
                            var qiwi = new QiwiPaymentService();
                            //Закомментил для теста рейтинга
                            if (/*qiwi.Purchase(decimal.Parse(product.Price.ToString()))*/ true)
                            {
                               /* using (var productDataAccess = new ProductDataAccess())
                                {
                                    productDataAccess.Update(product);
                                    products = productDataAccess.Select();
                                }*/
                                Console.WriteLine("Спасибо за покупку!");
                                Console.WriteLine("Поставить оценку товару? (y/n)");
                                switch (Console.ReadLine())
                                {
                                    case "y":
                                        var rating = 0.0;
                                        Console.WriteLine("Ваша оценка: (1-10)");
                                        var str = Console.ReadLine();
                                        rating = double.Parse(str);                                                                            
                                        if (rating > 0 && rating <= 10)
                                        {
                                            using (var productDataAccess = new ProductDataAccess())
                                            {
                                                productDataAccess.InsertRating(product, rating);
                                                productDataAccess.UpdateRating(product);
                                                products = productDataAccess.Select();
                                            }
                                            Console.WriteLine("Спасибо за оценку!");
                                            Console.ReadLine();
                                        } else
                                        {
                                            Console.WriteLine("Рейтинг не может быть ниже нуля или больше 10!");
                                        }
                                        break;
                                    case "n":
                                        break;
                                }                            
                                Console.ReadLine();
                            }
                            else
                            {
                                Console.WriteLine("Что-то пошло не так! Ожидайте");
                                Console.ReadLine();
                            };
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("К сожалению товара нет на складе");
                            Console.ReadLine();
                        }
                    }
                }
            }
        }
    }
}
