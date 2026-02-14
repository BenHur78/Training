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

    interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }

    // low-level module
    class Relationshipts : IRelationshipBrowser
    {
        //the high level module depends on how we structure data
        private List<(Person, Relationship, Person)> relations = new List<(Person, Relationship, Person)>();

        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add((parent, Relationship.Parent, child));
            relations.Add((child, Relationship.Child, parent));
        }

        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            return relations.Where(x => x.Item1.Name == name && x.Item2 == Relationship.Parent).Select(r => r.Item3);
        }

        

        //public List<(Person, Relationship, Person)> Relations => relations; //bad, this is a private thing being exposed.
    }

    class Research //the high module
    {
        //public Research(Relationshipts relationships)
        //{
        //    //var relations = relationshipts.Relations; // this is bad, we are accessing a low level detail, a datastore.
        //    //foreach (var r in relations.Where(x => x.Item1.Name == "John" && x.Item2 == Relationship.Parent))
        //    //{
        //    //    Console.WriteLine($"John has a child called {r.Item3.Name}");
        //    //}
        //}

        public Research(IRelationshipBrowser browser)
        {
            foreach (var p in browser.FindAllChildrenOf("John"))
            {
                Console.WriteLine($"John has a child called {p.Name}");
            }
        }

        static void Main(string[] args)
        {
            var parent = new Person { Name = "John" };
            var child1 = new Person { Name = "Chris" };
            var child2 = new Person { Name = "Mary" };

            //low level module
            var relationships = new Relationshipts();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);

            new Research(relationships);

            Console.WriteLine($"");
        }
    }

}