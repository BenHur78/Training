namespace DesignPatterns
{
    enum Relationship
    {
        Parent,
        Child,
        Sibling
    }

    class Person
    {
        public string Name;
    }

    // low-level module
    class Relationshipts
    {
        private List<(Person, Relationship, Person)> relations = new List<(Person, Relationship, Person)>();

        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add((parent, Relationship.Parent, child));
            relations.Add((child, Relationship.Child, parent));
        }

    public class Demo
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"");
        }
    }

}