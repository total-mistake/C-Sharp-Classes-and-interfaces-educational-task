using System.Text.Json;

namespace Classes_and_interfaces.Classes
{
    public class SportTeam
    {
        public string TeamName { get; set; }
        public Manager? TeamManager { get; set; }
        public List<Player> Players { get; set; }
        public SportTeam()
        {
            TeamManager = null;
            Players = new List<Player>();
        }

        public SportTeam(string teamName, Manager manager)
        {
            TeamName = teamName;
            TeamManager = manager;
            Players = new List<Player>();
        }

        // Метод для добавления нового игрока
        public void AddPlayer(Player player)
        {
            if (Players.Any(p => p.Equals(player)))
            {
                throw new InvalidOperationException("Игрок уже существует в команде.");
            }
            Players.Add(player);
        }

        // Метод для удаления игрока из команды
        public void RemovePlayer(Player player)
        {
            if (!Players.Remove(player))
            {
                throw new InvalidOperationException("Игрок не найден в команде.");
            }
        }

        // Метод для сортировки игроков по заданному параметру
        public void SortPlayers(Func<Player, object> keySelector, bool ascending = true)
        {
            Players = ascending ? Players.OrderBy(keySelector).ToList() : Players.OrderByDescending(keySelector).ToList();
        }

        // Метод для вывода информации о количестве игроков в команде
        public int GetPlayerCount()
        {
            return Players.Count;
        }

        // Метод для поиска игроков по заданным характеристикам
        public List<Player> FindPlayers(string name = null, string lastName = null, int? age = null, int? skillLevel = null, string position = null)
        {
            return Players.Where(p =>
                (name == null || p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) &&
                (lastName == null || p.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase)) &&
                (!age.HasValue || p.Age == age) &&
                (!skillLevel.HasValue || p.SkillLevel == skillLevel) &&
                (position == null || p.Position.Equals(position, StringComparison.OrdinalIgnoreCase))).ToList();
        }

        // Метод для фильтрации игроков по заданному предикату
        public List<Player> FilterPlayers(Func<Player, bool> predicate)
        {
            return Players.Where(predicate).ToList();
        }

        // Индексатор для доступа к игрокам по индексу
        public Player this[int index]
        {
            get => Players[index];
            set => Players[index] = value;
        }

        // Индексатор для получения игрока по имени и фамилии
        public Player? this[string name, string lastName]
        {
            get
            {
                return Players.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                                                   p.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));
            }
        }

        // Метод для вывода статистики по игрокам
        public void PrintStatistics()
        {
            int playerCount = Players.Count;
            double averageAge = playerCount > 0 ? Players.Average(p => p.Age) : 0;
            double averageSkillLevel = playerCount > 0 ? Players.Average(p => p.SkillLevel) : 0;

            Console.WriteLine($"Количество игроков: {playerCount}");
            Console.WriteLine($"Средний возраст: {averageAge:F0} лет");
            Console.WriteLine($"Средний уровень мастерства: {averageSkillLevel:F2}");
        }

        // Метод ToString для вывода информации о команде
        public sealed override string ToString()
        {
            string managerInfo = TeamManager != null ? TeamManager.ToString() : "Менеджер отсутствует";

            if (Players.Count == 0)
            {
                return $"Команда: {TeamName}\n{managerInfo}\nВ составе команды еще нет игроков.";
            }

            var playerInfo = string.Join(Environment.NewLine, Players.Select(p => p.ToString()));

            return $"Команда: {TeamName}\n{managerInfo}\nКоличество игроков: {GetPlayerCount()}\nИгроки:\n{playerInfo}";
        }

        // Метод для записи данных в файл (включая менеджера и название команды)
        public void SaveToFile(string fileName)
        {
            File.WriteAllText(fileName, this.ToJson());
        }

        public string ToJson()
        {
            var jsonData = new
            {
                TeamName,
                TeamManager,
                Players
            };

            return JsonSerializer.Serialize(jsonData, new JsonSerializerOptions { WriteIndented = true });
        }

        // Метод для загрузки данных из файла с проверкой корректности данных
        public static SportTeam LoadFromFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("Файл не найден.", fileName);
            }

            var json = File.ReadAllText(fileName);
            var data = JsonSerializer.Deserialize<LoadData>(json);

            if (data == null)
            {
                throw new InvalidDataException("Некорректные данные в файле.");
            }

            var team = new SportTeam(data.TeamName, data.TeamManager);
            team.Players = data.Players;

            return team;
        }

        private class LoadData
        {
            public string TeamName { get; set; }
            public Manager TeamManager { get; set; }
            public List<Player> Players { get; set; }
        }
    }
}
