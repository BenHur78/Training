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
    }

    public class Research //the high module
    {
        static void Main(string[] args)
        {
            var parent = new Person { Name = "John" };
            var child1 = new Person { Name = "Chris" };
            var child2 = new Person { Name = "Mary" };

            //low level module
            var relationships = new Relationshipts();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);

            Console.WriteLine($"");
        }
    }

}