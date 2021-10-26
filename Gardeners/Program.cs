using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gardeners
{
    class Program
    {
        //Поля для размеров сада.
        static int n1;
        static int n2;

        //Заглушка.
        static object locker = new object();

        //По умолчанию массивы bool заполняются false.
        static bool[,] garden;

        //Настройка метода обрабортки сада слева-направо сверху-вниз.
        public static void GardenWorks1()
        {
            for (int i = 0; i < n1; i++)
            {
                for (int j = 0; j < n2; j++)
                {
                    if (garden[i, j] == false)
                    {
                        garden[i, j] = true;
                        Console.WriteLine("Садовник1 обрабатывает сад {0},{1}", i, j);
                    }
                    else
                    {
                        Console.WriteLine("Садовник1 пропускает сад {0},{1}, его уже обработал Садовник2", i, j);
                    }                    
                    Thread.Sleep(50);
                }
            }
            

        }

        //Настройка метода обработки сада справа-налево снизу-вверх.
        public static void GardenWorks2()
        {
            for (int i = n1-1; i >= 0; i--)
            {
                for (int j = n2-1; j >= 0; j--)
                {
                    if (garden[i, j] == false)
                    {
                        garden[i, j] = true;
                        Console.WriteLine("Садовник2 обрабатывает сад {0},{1}", i, j);
                    }
                    else
                    {
                        Console.WriteLine("Садовник2 пропускает сад {0},{1}, его уже обработал Садовник1", i, j);
                    }
                    
                    Thread.Sleep(50);
                }
            }
            
        }

        static void Main(string[] args)
        {
            //Введение размеров сада.
            Console.WriteLine("Введите длину захватки сада");
            n1 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите ширину захватки сада");
            n2 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("");


            garden = new bool[n1, n2];

            //Считается ли, что тут используются распределяемые ресурсы? Вводила-убирала локер, влияния не увидела, если честно :(
            lock (locker)
            {
                //Первый поток (садовник1).
                ThreadStart gardenerDelegate1 = new ThreadStart(GardenWorks1);
                Thread gardener1 = new Thread(gardenerDelegate1);
                gardener1.Start();
                

                //Второй поток (садовник2).
                ThreadStart gardenerDelegate2 = new ThreadStart(GardenWorks2);
                Thread gardener2 = new Thread(gardenerDelegate2);                
                gardener2.Start();

            }
            Console.ReadKey();
        }
    }
}
