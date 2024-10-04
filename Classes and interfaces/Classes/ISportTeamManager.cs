using Classes_and_interfaces.Classes;
using Classes_and_interfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes_and_interfaces.Interfaces
{
    public interface ISportTeamManager
    {
        void SetName();
        void AddPlayer();
        void RemovePlayer();
        void AddManager();
        void ShowStatistics();
        void SortPlayers();
        void FindPlayer();
        void DisplayTeamInfo();
        void SaveToFile();
        void LoadFromFile();
        void FilterPlayers();
    }
}

namespace Classes_and_interfaces.Managers
{
    public class SportTeamManager : ISportTeamManager
    {
        private SportTeam team;

        public SportTeamManager()
        {
            team = new SportTeam();
        }

        public void SetName()
        {
            Console.Write("Введите название команды: ");
            string teamName = Console.ReadLine();

            team.TeamName = teamName;
        }

        public void AddPlayer()
        {
            Console.Write("Введите имя игрока: ");
            string name = Console.ReadLine();

            Console.Write("Введите фамилию игрока: ");
            string lastName = Console.ReadLine();

            Console.Write("Введите возраст игрока: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Введите позицию игрока: ");
            string position = Console.ReadLine();

            Console.Write("Введите уровень мастерства игрока: ");
            int skillLevel = int.Parse(Console.ReadLine());

            Player player = new Player(age, name, lastName, position, skillLevel);
            team.AddPlayer(player);
            Console.WriteLine("Игрок добавлен.");
        }

        public void RemovePlayer()
        {
            Console.Write("Введите имя игрока для удаления: ");
            string name = Console.ReadLine();

            Console.Write("Введите фамилию игрока для удаления: ");
            string lastName = Console.ReadLine();

            Player player = team[name, lastName];
            if (player != null)
            {
                team.RemovePlayer(player);
                Console.WriteLine("Игрок удален.");
            }
            else
            {
                Console.WriteLine("Игрок не найден.");
            }
        }

        public void ShowStatistics()
        {
            team.PrintStatistics();
        }

        public void SortPlayers()
        {
            Console.Write("Введите критерий сортировки (age, name, lastname, skilllevel): ");
            string criterion = Console.ReadLine();

            Console.Write("Введите порядок сортировки (true - возрастание, false - убывание): ");
            bool ascending = bool.Parse(Console.ReadLine());

            switch (criterion.ToLower())
            {
                case "age":
                    team.SortPlayers(p => p.Age, ascending);
                    break;
                case "name":
                    team.SortPlayers(p => p.Name, ascending);
                    break;
                case "lastname":
                    team.SortPlayers(p => p.LastName, ascending);
                    break;
                case "skilllevel":
                    team.SortPlayers(p => p.SkillLevel, ascending);
                    break;
                default:
                    Console.WriteLine("Неверный критерий сортировки.");
                    return;
            }

            Console.WriteLine("Игроки отсортированы.");
        }

        public void FindPlayer()
        {
            Console.Write("Введите имя игрока для поиска (или нажмите Enter для пропуска): ");
            string name = Console.ReadLine();
            name = string.IsNullOrWhiteSpace(name) ? null : name;

            Console.Write("Введите фамилию игрока для поиска (или нажмите Enter для пропуска): ");
            string lastName = Console.ReadLine();
            lastName = string.IsNullOrWhiteSpace(lastName) ? null : lastName;

            Console.Write("Введите возраст игрока для поиска (или нажмите Enter для пропуска): ");
            string ageInput = Console.ReadLine();
            int? age = string.IsNullOrWhiteSpace(ageInput) ? (int?)null : int.Parse(ageInput);

            Console.Write("Введите уровень мастерства игрока для поиска (или нажмите Enter для пропуска): ");
            string skillLevelInput = Console.ReadLine();
            int? skillLevel = string.IsNullOrWhiteSpace(skillLevelInput) ? (int?)null : int.Parse(skillLevelInput);

            Console.Write("Введите позицию игрока для поиска (или нажмите Enter для пропуска): ");
            string position = Console.ReadLine();
            position = string.IsNullOrWhiteSpace(position) ? null : position;

            var foundPlayers = team.FindPlayers(name, lastName, age, skillLevel, position);

            if (foundPlayers.Any())
            {
                Console.WriteLine("Найденные игроки:");
                foreach (var player in foundPlayers)
                {
                    Console.WriteLine(player.ToString());
                }
            }
            else
            {
                Console.WriteLine("Игроки не найдены.");
            }
        }

        public void DisplayTeamInfo()
        {
            if (team.GetPlayerCount() == 0)
                Console.WriteLine("В команде нет игроков.");

            Console.WriteLine(team.ToString());
        }

        public void AddManager()
        {
            Console.Write("Введите имя менеджера: ");
            string name = Console.ReadLine();

            Console.Write("Введите фамилию менеджера: ");
            string lastName = Console.ReadLine();

            Console.Write("Введите возраст менеджера: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Введите стаж менеджера в годах: ");
            int experience = int.Parse(Console.ReadLine());

            Manager manager = new Manager(age, name, lastName, experience);
            team.TeamManager = manager; // Установка менеджера команды

            Console.WriteLine($"Менеджер {manager} добавлен.");
        }

        public void SaveToFile()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(team.TeamName))
                    throw new InvalidOperationException("Название команды не должно быть пустым.");

                string fileName = $"{team.TeamName.Replace(' ', '_')}.json";

                team.SaveToFile(fileName);

                Console.WriteLine($"Данные команды сохранены в файл {fileName}.");
            }
            catch 
            {
                Console.WriteLine("Перед сохранением данных в файл задайте навзание для команды!");
            }
            
        }

        public void LoadFromFile()
        {
            Console.Write("Введите название файла для загрузки: ");
            string fileName = Console.ReadLine();

            try
            {
                var loadedTeam = SportTeam.LoadFromFile(fileName);
                this.team = loadedTeam; // Обновляем текущую команду данными из файла

                Console.WriteLine($"Данные команды загружены из файла {fileName}.\n");
                ShowStatistics();
                Console.WriteLine();
                DisplayTeamInfo();

                if (loadedTeam.TeamManager != null)
                    Console.WriteLine($"Менеджер команды: {loadedTeam.TeamManager}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        public void FilterPlayers()
        {
            // Пример фильтрации по уровню мастерства
            Console.Write("Введите минимальный уровень мастерства для фильтрации: ");
            int minSkillLevel = int.Parse(Console.ReadLine());

            var filteredPlayers = team.FilterPlayers(p => p.SkillLevel >= minSkillLevel);

            if (!filteredPlayers.Any())
            {
                Console.WriteLine("Игроки не найдены по заданным критериям.");
                return;
            }

            foreach (var player in filteredPlayers)
            {
                Console.WriteLine(player);
            }
        }
    }
}