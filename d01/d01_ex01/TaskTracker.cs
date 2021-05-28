#nullable enable
using System;
using System.Collections.Generic;
using System.Globalization;
using d01_ex01.Tasks;

namespace d01_ex01
{
    class TaskTracker
    {
        private List<Task> _tasks;

        TaskTracker()
        {
            _tasks = new List<Task>();
        }
        private static void OnError(string message)
        {
            Console.WriteLine(message + Environment.NewLine);
        }

        private void AddTask()
        {
            bool parseResult;
            
            Console.Write("Введите заголовок: ");
            string? title = Console.ReadLine();

            if (string.IsNullOrEmpty(title))
            {
                OnError("Ошибка ввода. Проверьте входные данные и повторите запрос.");
                return;
            }
            
            Console.Write("Введите описание: ");
            string? summary = Console.ReadLine();

            Console.Write("Введите срок: ");
            DateTime? dueDate = null;
            parseResult = DateTime.TryParseExact(Console.ReadLine(), "M/d/yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out var tmpDueDate);
            if (parseResult)
                dueDate = tmpDueDate;

            Console.Write("Введите тип: ");
            parseResult = Enum.TryParse(Console.ReadLine(), true, out TaskType type);
            if (!parseResult)
            {
                OnError("Ошибка ввода. Проверьте входные данные и повторите запрос.");
                return;
            }


            Console.Write("Установите приоритет: ");
            parseResult = Enum.TryParse(Console.ReadLine(), true, out TaskPirority priority);
            if (!parseResult)
                priority = TaskPirority.Normal;

            var task = new Task(title, summary, dueDate, type, priority);
            _tasks.Add(task);
            
            Console.WriteLine("\nЗадача создана: \n" + task.ToString() + Environment.NewLine);
        }

        private void ShowTasks()
        {
            foreach (var task in _tasks)
            {
                Console.WriteLine(task + Environment.NewLine);
            }
        }

        private void ShowTasksList()
        {
            var serial = 0;

            foreach (var task in _tasks)
            {
                Console.WriteLine(serial + ". " + task.GetTitle());
                serial++;
            }
        }
        private Task? ChoiceTask()
        {
            Task? selected = null;

            ShowTasksList();
            Console.Write("Введите заголовок или id: ");

            string? input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input))
                return selected;
            
            foreach (var task in _tasks)
            {
                if (task.GetTitle() == input)
                    selected = task;
            }

            if (selected == null)
            {
                bool ret = int.TryParse(input, out int index);

                if (ret && index < _tasks.Count)
                    selected = _tasks[index];
            }

            return selected;
        }

        private bool TrackerIsEmpty()
        {
            if (_tasks.Count == 0)
            {
                Console.WriteLine("Список задач пока пуст.\n");
                return true;
            }

            return false;
        }
        
        private void DoneTasks()
        {
            Task? task = ChoiceTask();

            if (task == null)
            {
                OnError("Ошибка ввода. Задача с таким заголовком не найдена");
                return;
            }
            
            task.Done();
           
            Console.WriteLine($"Задача [{task.GetTitle()}] выполнена!\n");
        }

        private void WontDoTask()
        {
            Task? task = ChoiceTask();

            if (task == null)
            {
                OnError("Задача не найдена");
                return;
            }
            
            task.Close();
           
            Console.WriteLine($"Задача [{task.GetTitle()}] более не актуальна!\n");
        }

        static int Main(string[] args)
        {
            string? input;
            var errors = 0;
            var taskTracker = new TaskTracker();

            do
            {
                input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    errors++;
                    continue;
                }

                errors = 0;

                input = input.Trim().ToLower();

                switch (input)
                {
                    case "add": taskTracker.AddTask(); break;
                    case "list": if (!taskTracker.TrackerIsEmpty()) taskTracker.ShowTasks(); break;
                    case "done": if (!taskTracker.TrackerIsEmpty()) taskTracker.DoneTasks(); break;
                    case "wontdo": if (!taskTracker.TrackerIsEmpty()) taskTracker.WontDoTask(); break;
                    case "q":
                    case"quit": break;
                    default: Console.WriteLine("Доступные комманды: add, list, done, wontdo, quit\n"); break;
                };
            } while (input != "q" && input != "quit" && errors < 4);

            return 0;
        }
    }
}