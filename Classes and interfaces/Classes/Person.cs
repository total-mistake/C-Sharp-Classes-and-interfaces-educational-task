
namespace Classes_and_interfaces.Classes
{
    public abstract class Person : IEquatable<Person>
    {
        public virtual int Age { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public Person(string Name, string LastName, int Age)
        {
            this.Name = Name;
            this.LastName = LastName;
            this.Age = Age;
        }

        // Реализация метода Equals для сравнения по имени, фамилии и возрасту
        public bool Equals(Person other)
        {
            if (other == null) return false;

            return this.Name == other.Name &&
                   this.LastName == other.LastName &&
                   this.Age == other.Age;
        }

        // Переопределение метода Equals для object
        public override bool Equals(object obj)
        {
            if (obj is Person other)
                return Equals(other); // Используем реализацию IEquatable

            return false;
        }

        // Переопределение метода GetHashCode
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, LastName, Age);
        }

        public abstract string ToJson();

        public override abstract string ToString();

        // Переопределение операторов сравнения
        public static bool operator ==(Person left, Person right)
        {
            // Проверка на null
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Name == right.Name && left.LastName == right.LastName && left.Age == right.Age;
        }

        public static bool operator !=(Person left, Person right)
        {
            return !(left == right);
        }
    }
}
