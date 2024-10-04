using System.Text.Json;

namespace Classes_and_interfaces.Classes
{
    public class Player : Person, IComparable<Player>
    {
        private int _age; // Поле для хранения возраста

        public override int Age
        {
            get => _age;
            set
            {
                if (value < 18)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Возраст не может быть меньше 18 лет.");
                }
                _age = value;
            }
        }

        public string Position { get; set; }
        public int SkillLevel { get; set; }

        public Player(int age, string name, string lastName, string position, int skillLevel)
            : base(name, lastName, age)
        {
            Position = position;
            SkillLevel = skillLevel;
            Age = age; // Устанавливаем возраст через свойство
        }

        public int CompareTo(Player other)
        {
            if (other == null) return 1;
            return SkillLevel.CompareTo(other.SkillLevel); // Сравнение по skillLevel
        }

        // Переопределение операторов сравнения

        public static bool operator <(Player left, Player right)
        {
            if (ReferenceEquals(left, null)) return true; // Если левый объект null, он меньше
            if (ReferenceEquals(right, null)) return false; // Если правый объект null, он больше

            return left.SkillLevel < right.SkillLevel;
        }

        public static bool operator >(Player left, Player right)
        {
            if (ReferenceEquals(left, null)) return false; // Если левый объект null, он не больше
            if (ReferenceEquals(right, null)) return true; // Если правый объект null, левый больше

            return left.SkillLevel > right.SkillLevel;
        }

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        }

        public override string ToString()
        {
            return $"Имя: {Name} {LastName}, Возраст: {Age}, Позиция: {Position}, Уровень навыков: {SkillLevel}";
        }
    }
}
