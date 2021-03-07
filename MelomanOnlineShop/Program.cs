using Models;
using Services;
using System;
using Data;
using System.Collections.Generic;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Для проверки меню создал экземпляр
            ConfigurationService.Init();
            ICollection<Product> products;
            using (var productDataAccess = new ProductDataAccess())
            {
                products = productDataAccess.Select();
            }


            Console.WriteLine(@"                                *********************************************
                                *			                    *
                                *			                    *
                                *   Добро пожаловать в онлайн магазин!      *
                                *			                    *
                                *			                    *
                                *********************************************");
            while (true)
            {
                Console.Write("\n1. Зарегистрироваться\n2. Войти\n0. Выход\nВыбор: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Пожалуйста, введите ваш номер телефона: (пример: 7XXXXXXXXXXX)");
                        try
                        {
                            if (AuthUtil.Registration(Console.ReadLine()) == true)
                            {
                                Console.WriteLine("Вы успешно зарегистрированы!");
                            }
                            else
                            {
                                Console.WriteLine("Ошибка регистрации! Введены не верные данные, либо пользователь уже зарегистрирован");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "2":
                        // Временно закомментил, чтобы меню тестить 

                        //Console.WriteLine("Пожалуйста, введите ваш номер телефона: (пример: 7XXXXXXXXXXX)");
                        //try
                        //{
                        //    if (AuthUtil.Authorization(Console.ReadLine()) == true)
                        //    {
                        //        Console.WriteLine("Вы успешно авторизованы! Для продолжения нажмите любую клавишу");
                        //        Console.ReadLine();
                        //    }
                        //    else
                        //    {
                        //        Console.WriteLine("Введены не верные данные, повторите попытку");
                        //        break;
                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    Console.WriteLine(ex.Message);
                        //}
                        while (true)
                        {
                            Console.Clear();
                            Console.Write("\nВыберите категорию товара:\n1. Книги\n2. Музыка\n3. Фильмы\n4. Игры\n0. Выход\nВыбор: ");
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    Console.Clear();
                                    foreach (var product in products)
                                    {
                                        if (product.Category == "Книги")
                                        {
                                            Console.WriteLine($"Артикул {product.Id} - {product.AuthorName.Name} \"{product.Name}\" ({product.GenreName.Name})");
                                        }
                                    }
                                    Console.WriteLine("\nХотите выбрать товар? (y/n)");
                                    switch (Console.ReadLine())
                                    {
                                        case "y":
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
                                                        var qiwi = new QiwiPaymentService();
                                                        if (qiwi.Purchase(decimal.Parse(product.Price.ToString())))
                                                        {
                                                            Console.WriteLine("Спасибо за покупку!");
                                                            Console.ReadLine();
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Что-то пошло не так! Ожидайте");
                                                            Console.ReadLine();
                                                        };
                                                    }
                                                }
                                            }
                                            break;
                                        case "n":
                                            break;
                                    }
                                    break;
                                case "2":
                                    Console.Clear();
                                    foreach (var product in products)
                                    {
                                        if (product.Category == "Музыка")
                                        {
                                            Console.WriteLine($"Артикул {product.Id} - {product.AuthorName.Name} \"{product.Name}\" ({product.GenreName.Name})");
                                        }
                                    }
                                    Console.WriteLine("\nХотите выбрать товар? (y/n)");
                                    switch (Console.ReadLine())
                                    {
                                        case "y":
                                            Console.Write("Введите Артикул необходимого товара: ");
                                            Guid.TryParse(Console.ReadLine(), out var item);
                                            Console.WriteLine("\nЕсли Вы хотите купить товар, нажмите Enter. Иначе нажмите Escape");
                                            if (Console.ReadKey().Key == ConsoleKey.Enter)
                                            {
                                                foreach (var product in products)
                                                {
                                                    if (item == product.Id)
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine($"\nКатегория: {product.Category}\nЖанр: {product.GenreName.Name}\nНазвание: {product.Name}\nАвтор: {product.AuthorName.Name}\nЦена: {product.Price}\nКол-во на складе: {product.Quantity}\nРейтинг: {product.Rating}");
                                                        Console.WriteLine("\nЕсли Вы хотите купить товар, нажмите Enter. Иначе нажмите Escape");
                                                        if (Console.ReadKey().Key == ConsoleKey.Enter)
                                                        {
                                                            var qiwi = new QiwiPaymentService();
                                                            if (qiwi.Purchase(decimal.Parse(product.Price.ToString())))
                                                            {
                                                                Console.WriteLine("Спасибо за покупку!");
                                                                Console.ReadLine();
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Что-то пошло не так! Ожидайте");
                                                                Console.ReadLine();
                                                            };
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case "n":
                                            break;
                                    }
                                    break;
                                case "3":
                                    Console.Clear();
                                    foreach (var product in products)
                                    {
                                        if (product.Category == "Фильмы")
                                        {
                                            Console.WriteLine($"Артикул {product.Id} - {product.AuthorName.Name} \"{product.Name}\" ({product.GenreName.Name})");
                                        }
                                    }
                                    Console.WriteLine("\nХотите выбрать товар? (y/n)");
                                    switch (Console.ReadLine())
                                    {
                                        case "y":
                                            Console.Write("Введите Артикул необходимого товара: ");
                                            Guid.TryParse(Console.ReadLine(), out var item);
                                            foreach (var product in products)
                                            {
                                                if (item == product.Id)
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine($"\nКатегория: {product.Category}\nЖанр: {product.GenreName.Name}\nНазвание: {product.Name}\nАвтор: {product.AuthorName.Name}\nЦена: {product.Price}\nКол-во на складе: {product.Quantity}\nРейтинг: {product.Rating}");
                                                    if (Console.ReadKey().Key == ConsoleKey.Enter)
                                                    {
                                                        var qiwi = new QiwiPaymentService();
                                                        if (qiwi.Purchase(decimal.Parse(product.Price.ToString())))
                                                        {
                                                            Console.WriteLine("Спасибо за покупку!");
                                                            Console.ReadLine();
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Что-то пошло не так! Ожидайте");
                                                            Console.ReadLine();
                                                        };
                                                    }
                                                }
                                            }
                                            break;
                                        case "n":
                                            break;
                                    }
                                    break;
                                case "4":
                                    Console.Clear();
                                    foreach (var product in products)
                                    {
                                        if (product.Category == "Игры")
                                        {
                                            Console.WriteLine($"Артикул {product.Id} - {product.AuthorName.Name} \"{product.Name}\" ({product.GenreName.Name})");
                                        }
                                    }
                                    Console.WriteLine("\nХотите выбрать товар? (y/n)");
                                    switch (Console.ReadLine())
                                    {
                                        case "y":
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
                                                        var qiwi = new QiwiPaymentService();
                                                        if (qiwi.Purchase(decimal.Parse(product.Price.ToString())))
                                                        {
                                                            Console.WriteLine("Спасибо за покупку!");
                                                            Console.ReadLine();
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Что-то пошло не так! Ожидайте");
                                                            Console.ReadLine();
                                                        };
                                                    }
                                                }
                                            }
                                            break;
                                        case "n":
                                            break;
                                    }
                                    break;
                                case "0":
                                    return;
                            }
                        }
                    case "0":
                        return;
                }
            }
        }
    }
}

