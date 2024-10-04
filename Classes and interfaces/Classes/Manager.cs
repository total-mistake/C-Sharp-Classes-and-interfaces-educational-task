using System.Text.Json;

namespace Classes_and_interfaces.Classes
{
    public class Manager : Person, IComparable<Manager>
    {
        public int Experience { get; set; } //Стаж работы в годах
        public Manager(int Age, string Name, string LastName, int Experience)
            : base(Name, LastName, Age)
        {
            this.Experience = Experience;
        }

        public int CompareTo(Manager other)
        {
            if (other == null) return 1;
            return Experience.CompareTo(other.Experience); // Сравнение по skillLevel
        }

        // Переопределение операторов сравнения

        public static bool operator <(Manager left, Manager right)
        {
            if (ReferenceEquals(left, null)) return true; // Если левый объект null, он меньше
            if (ReferenceEquals(right, null)) return false; // Если правый объект null, он больше

            return left.Experience < right.Experience;
        }

        public static bool operator >(Manager left, Manager right)
        {
            if (ReferenceEquals(left, null)) return false; // Если левый объект null, он не больше
            if (ReferenceEquals(right, null)) return true; // Если правый объект null, левый больше

            return left.Experience > right.Experience;
        }

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        }

        public override string ToString()
        {
            return $"Менеджер: \nИмя: {Name} {LastName}, Возраст: {Age}, Опыт: {Experience}";
        }
    }
}
