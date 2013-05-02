﻿using EFChangeNotify;
using System;
using System.Linq;
using TestConsole.Models;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string productName = "Lamp";

            //using (var cache = new EntityCache<Product, StoreDbContext>(p => p.Name == "Lamp"))
            //{
            //    Console.WriteLine("Press any key to stop listening for changes...");

            //    while (true)
            //    {
            //        Console.WriteLine(cache.Results.Count());
            //        Thread.Sleep(1000);

            //        if (Console.KeyAvailable)
            //            break;
            //    }
            //}

            //return;

            using (var notifer = new EntityChangeNotifier<Product, StoreDbContext>(p => p.Name == productName))
            {
                notifer.Error += (sender, e) =>
                {
                    Console.WriteLine("[{0}, {1}, {2}]:\n{3}", e.Reason.Info, e.Reason.Source, e.Reason.Type, e.Sql);
                };

                notifer.Changed += (sender, e) =>
                {
                    Console.WriteLine(e.Results.Count());
                    foreach (var p in e.Results)
                    {
                        Console.WriteLine("  {0}", p.Name);
                    }
                };

                using (var otherNotifier = new EntityChangeNotifier<Product, StoreDbContext>(x => x.Name == "Desk"))
                {
                    otherNotifier.Changed += (sender, e) =>
                    {
                        Console.WriteLine(e.Results.Count());
                    };

                    Console.WriteLine("Press any key to stop listening for changes...");
                    Console.ReadKey(true);
                }

                Console.WriteLine("Press any key to stop...");
                Console.ReadKey(true);
            }
        }
    }
}
