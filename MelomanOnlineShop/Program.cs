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
                        Console.WriteLine("Пожалуйста, введите ваш номер телефона: (пример: 7XXXXXXXXXXX)");
                        try
                        {
                            if (AuthUtil.Authorization(Console.ReadLine()) == true)
                            {
                                Console.WriteLine("Вы успешно авторизованы! Для продолжения нажмите любую клавишу");
                                Console.ReadLine();
                            }
                            else
                            {
                                Console.WriteLine("Введены не верные данные, повторите попытку");
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        while (true)
                        {
                            Console.Clear();
                            Console.Write("\nВыберите категорию товара:\n1. Книги\n2. Музыка\n3. Фильмы\n4. Игры\n0. Выход\nВыбор: ");
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    Console.Clear();
                                    ProductService.ShowProductsList(products, "Книги");
                                    Console.WriteLine("\nХотите выбрать товар? (y/n)");
                                    switch (Console.ReadLine())
                                    {
                                        case "y":
                                            ProductService.Purchase(ref products);
                                            break;
                                        case "n":
                                            break;
                                    }
                                    break;
                                case "2":
                                    Console.Clear();
                                    ProductService.ShowProductsList(products, "Музыка");
                                    Console.WriteLine("\nХотите выбрать товар? (y/n)");
                                    switch (Console.ReadLine())
                                    {
                                        case "y":
                                            ProductService.Purchase(ref products);
                                            break;
                                        case "n":
                                            break;
                                    }
                                    break;
                                case "3":
                                    Console.Clear();
                                    ProductService.ShowProductsList(products, "Фильмы");
                                    Console.WriteLine("\nХотите выбрать товар? (y/n)");
                                    switch (Console.ReadLine())
                                    {
                                        case "y":
                                            ProductService.Purchase(ref products);
                                            break;
                                        case "n":
                                            break;
                                    }
                                    break;
                                case "4":
                                    Console.Clear();
                                    ProductService.ShowProductsList(products, "Игры");
                                    Console.WriteLine("\nХотите выбрать товар? (y/n)");
                                    switch (Console.ReadLine())
                                    {
                                        case "y":
                                            ProductService.Purchase(ref products);
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