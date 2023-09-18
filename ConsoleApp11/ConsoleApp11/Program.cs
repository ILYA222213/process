using System;
using System.Diagnostics;
using System.Timers;

class Program
{
    static System.Timers.Timer timer; // Создание объекта Timer для установки интервала и события
    static int counter = 0; // Счетчик запусков блокнота

    static void Main(string[] args)
    {
        // Инициализируем и запускаем таймер
        timer = new System.Timers.Timer(1000); // Установка интервала в 1000 миллисекунд (1 секунда)
        timer.Elapsed += TimerElapsed; // Добавляем обработчик события Elapsed
        timer.AutoReset = true; // Повторное вызывание события Elapsedпосле завершения обработки
        timer.Start(); // Запуск таймера

        // Выводим список времени запуска программы Notepad
        Console.WriteLine("Список времени запуска программы:");
        Console.WriteLine("===============================");

        // Перебираем все процессы с именем "notepad"
        foreach (var process in Process.GetProcessesByName("notepad"))
        {
            Console.WriteLine($"Программа запущена в: {process.StartTime}");
        }

        Console.WriteLine("Нажмите любую клавишу для завершения.");
        Console.ReadKey();

        // Останавливаем таймер перед выходом из программы
        timer.Stop();
        timer.Dispose();
    }

    static void TimerElapsed(object sender, ElapsedEventArgs e)
    {
        if (counter < 10)
        {
            // Запускаем блокнот
            Process.Start("notepad.exe");
            Console.WriteLine($"Блокнот вызван {counter + 1} раз в: {DateTime.Now}");

            counter++;
        }
        else
        {
            // Останавливаем таймер после 10-го запуска блокнота
            timer.Stop();
            timer.Dispose();
        }
    }
}