using Data;
using Models;
using System;
using System.Collections.Generic;

namespace Services
{
    public class ProductService
    {
        public static void Purchase(ref ICollection<Product> products)
        {
            Console.Write("Введите Артикул необходимого товара: ");
            Guid.TryParse(Console.ReadLine(), out var item);
            foreach (var product in products)
            {
                if (item == product.Id)
                {
                    Console.Clear();
                    var creator = "";
                    if (product.Category == "Игры") { creator = "Разработчик"; }
                    else if(product.Category == "Фильмы") { creator = "Режиссер"; }
                    else if (product.Category == "Музыка") { creator = "Исполнитель"; }
                    else
                    {
                        creator = "Автор";
                    }                   
                    Console.WriteLine($"\nКатегория: {product.Category}\nЖанр: {product.GenreName.Name}\nНазвание: {product.Name}\n{creator}: {product.AuthorName.Name}\nЦена: {product.Price}\nКол-во на складе: {product.Quantity}\nРейтинг: {product.Rating}");
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
                                            Console.ReadLine();
                                        }
                                        break;
                                    case "n":
                                        break;
                                }                            
                                Console.ReadLine();
                            }
                            else
                            {
                                Console.WriteLine("Что-то пошло не так!");
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

        public static void ShowProductsList(ref ICollection<Product> products, string category)
        {
            var productInPage = 0;
            foreach (var product in products)
            {
                if (product.Category == category)
                {
                    Console.WriteLine($"{product.AuthorName.Name} \"{product.Name}\" - Артикул: {product.Id}");
                    productInPage++;
                }

                if (productInPage % 10 == 0 && productInPage > 0)
                {
                    productInPage /= 10;
                    Console.WriteLine("\nДля перехода на след.страницу нажмите любую клавишу\nЕсли хотите купить товар, нажмите ENTER (для покупки будет необходим артикул)");
                    if (Console.ReadKey().Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                    Console.Clear();
                }
            }
        }
    }
}
