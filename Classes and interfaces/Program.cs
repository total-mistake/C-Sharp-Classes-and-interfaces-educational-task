using Classes_and_interfaces.Managers;
using Classes_and_interfaces.Classes;
using Classes_and_interfaces.Interfaces;


int V = ('S' + 'E') % 10;
Console.WriteLine("Вариант №" + V);

ISportTeamManager sportTeamManager = new SportTeamManager();

while (true)
{
    Console.WriteLine("\nВыберите действие:");
    Console.WriteLine("1. Задать имя команды");
    Console.WriteLine("2. Добавить игрока");
    Console.WriteLine("3. Удалить игрока");
    Console.WriteLine("4. Назначить менеджера");
    Console.WriteLine("5. Показать информацию о команде");
    Console.WriteLine("6. Показать статистику");
    Console.WriteLine("7. Сортировать игроков");
    Console.WriteLine("8. Найти игрока");
    Console.WriteLine("9. Сохранить информацию о команде в файл");
    Console.WriteLine("10. Загрузить информацию о команде из файла");
    Console.WriteLine("0. Выход\n");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            sportTeamManager.SetName();
            Clear();
            break;
        case "2":
            sportTeamManager.AddPlayer();
            Clear();
            break;
        case "3":
            sportTeamManager.RemovePlayer();
            Clear();
            break;
        case "4":
            sportTeamManager.AddManager();
            Clear();
            break;
        case "5":
            sportTeamManager.DisplayTeamInfo();
            Clear();
            break;
        case "6":
            sportTeamManager.ShowStatistics();
            Clear();
            break;
        case "7":
            sportTeamManager.SortPlayers();
            Clear();
            break;
        case "8":
            sportTeamManager.FindPlayer();
            Clear();
            break;
        case "9":
            sportTeamManager.SaveToFile();
            Clear();
            break;
        case "10":
            sportTeamManager.LoadFromFile();
            Clear();
            break;
        case "0":
            return;
        default:
            Console.WriteLine("Неверный выбор. Попробуйте снова.");
            Clear();
            break;
    }
}


static void Clear()
{
    Console.WriteLine("\nНажмите любую кнопку для продолжения");
    Console.ReadKey();
    Console.Clear();
}


