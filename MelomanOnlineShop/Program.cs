﻿using Models;
using Models.Abstract;
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
            var books = new List<Book>();
            books.Add(new Book
            {
                Id=1,
                Name="Три Мушкетера",
                AuthorName = new Author { Id=1,Name="А. Дюма"},
                GenreName = new Genre { Id=1,Name="Роман"},
                Price=1000                
            });
            ConfigurationService.Init();
            Console.WriteLine(@"*********************************************
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
                                    foreach (var product in books)
                                    {
                                        // 
                                        Console.WriteLine($"Артикул {product.Id} - {product.AuthorName.Name} \"{product.Name}\" ({product.GenreName.Name})");
                                        Console.ReadLine();
                                    }
                                    Console.WriteLine("Хотите выбрать товар? (y/n)");
                                    switch (Console.ReadLine())
                                    {
                                        case "y":
                                            Console.Write("Введите Артикул необходимого товара: ");
                                            int.TryParse(Console.ReadLine(),out var item);
                                            foreach(var book in books)
                                            {
                                                if (item == book.Id)
                                                {
                                                    var qiwi = new QiwiPaymentService();
                                                    if (qiwi.Purchase(1))
                                                    {
                                                        Console.WriteLine("Спасибо за покупку!");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Что-то пошло не так! Ожидайте");
                                                    };
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

