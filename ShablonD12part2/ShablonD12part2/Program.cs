using System;
using System.Collections.Generic;

public class Venue
{
    public string Name { get; set; }
    public List<DateTime> BusyDates { get; set; } = new List<DateTime>();
}
public class BookingSystem
{
    private List<Venue> venues;

    public BookingSystem()
    {
        venues = new List<Venue>
        {
            new Venue { Name = "Большой зал", BusyDates = { new DateTime(2025, 1, 12) } },
            new Venue { Name = "Малый зал" }
        };
    }
    public Venue CheckAvailability(DateTime date)
    {
        foreach (var venue in venues)
        {
            if (!venue.BusyDates.Contains(date))
                return venue;
        }
        return null;
    }
    public bool ProcessPayment(string card)
    {
        return card == "VALID";
    }
    public void ConfirmBooking(Venue venue, DateTime date)
    {
        venue.BusyDates.Add(date);
        Console.WriteLine($"Бронирование подтверждено! Площадка: {venue.Name}, дата: {date.ToShortDateString()}");
    }
}
public class Program
{
    public static void Main(string[] args)
    {
        BookingSystem system = new BookingSystem();
        DateTime eventDate = new DateTime(2025, 1, 12);
        Console.WriteLine($"Запрос доступности на {eventDate.ToShortDateString()}...");
        Venue venue = system.CheckAvailability(eventDate);
        if (venue == null)
        {
            Console.WriteLine("Площадка недоступна. Выберите другую дату или место.");
            return;
        }
        Console.WriteLine($"Доступная площадка найдена: {venue.Name}");

        Console.Write("Введите номер карты (VALID или INVALID): ");
        string cardNumber = Console.ReadLine();

        bool paymentSuccess = system.ProcessPayment(cardNumber);

        if (!paymentSuccess)
        {
            Console.WriteLine("Платёж отклонён. Повторите попытку.");
            return;
        }
        Console.WriteLine("Платёж успешен!");
        system.ConfirmBooking(venue, eventDate);
        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}