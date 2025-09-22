using System;
using System.Collections.Generic;
using DaysBetweenCounter;

// Создать структуру, хранящую информацию о заказах, принимаемых
// швейным ателье: номер заказа, заказчик, вид пошива (перечисление),
// дата приема (число, месяц, год как вложенная структура), стоимость
// заказа. Создать несколько таких структур и заполнить их. Вывести на
// экран все заказы, принятые в прошлом месяце. Если таковых нет, то
// выдать сообщение.

struct Date
{
    public int Day;
    public int Month;
    public int Year;

    public Date(int day, int month, int year)
    {
        this.Day = day;
        this.Month = month;
        this.Year = year;
    }
}

enum SewingType : byte { Dress, Suit, Skirt, Trousers, Other }

struct Order
{
    public int Id;
    public string Customer;
    public SewingType Type;
    public Date AcceptDate;
    public double Cost;
}

class Program
{
    static void Main()
    {
        List<Order> orders = new List<Order>();
        int id = 0;

        while (true)
        {
            Console.Write("Создать заказ? (0 - нет, 1 - да): ");
            bool isContinue = Console.ReadLine() == "1";
            if (isContinue == false) break;

            Order order = new Order();

            Console.WriteLine($"\nЗаказ #{id + 1}");
            order.Id = id;

            Console.Write("Заказчик: ");
            order.Customer = Console.ReadLine();

            Console.Write("Вид пошива (0 - платье, 1 - костюм, 2 - юбка, 3 - брюки, 4 - другое): ");
            int chosenType = int.Parse(Console.ReadLine());
            order.Type = (SewingType)chosenType;

            Console.Write("Дата приёма (дд мм гггг): ");
            string[] parts = Console.ReadLine().Split(' ');
            order.AcceptDate = new Date
            {
                Day = int.Parse(parts[0]),
                Month = int.Parse(parts[1]),
                Year = int.Parse(parts[2])
            };

            Console.Write("Стоимость заказа: ");
            order.Cost = double.Parse(Console.ReadLine());

            Console.WriteLine();
            orders.Add(order);
            id++;
        }

        Console.WriteLine("\n---ЗАКАЗЫ ЗА ПОСЛЕДНИЙ МЕСЯЦ---\n");

        int ordersLastMonthCounter = 0;
        DaysBetweenCounter.DaysBetweenCounter daysBetweenCounter = new DaysBetweenCounter.DaysBetweenCounter();
        Date today = new Date(DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year);

        foreach (Order order in orders)
        {
            Date orderStart = new Date(order.AcceptDate.Day, order.AcceptDate.Month, order.AcceptDate.Year);
            double daysDifference = daysBetweenCounter.daysBetweenDates(orderStart, today);

            if (daysDifference < 0 || daysDifference > 30) continue;

            Console.Write($"Заказ #{order.Id + 1}\nЗаказчик: {order.Customer}\nВид пошива: ");
            switch (order.Type)
            {
                case SewingType.Dress: Console.Write("Платье"); break;
                case SewingType.Suit: Console.Write("Костюм"); break;
                case SewingType.Skirt: Console.Write("Юбка"); break;
                case SewingType.Trousers: Console.Write("Брюки"); break;
                default: Console.Write("Другое"); break;
            }
            Console.WriteLine($"\nДата: {order.AcceptDate.Day}.{order.AcceptDate.Month}.{order.AcceptDate.Year}\nСтоимость: {order.Cost} руб.\n");
            ordersLastMonthCounter++;
        }

        if (ordersLastMonthCounter == 0)
            Console.WriteLine("Таких заказов нет!");
    }
}
