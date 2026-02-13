using System.Diagnostics;

new Demo().Run();

public class Demo
{
    private List<Entity> _entities = new List<Entity>();
    private Dictionary<Entity, string> _factoryMap = new Dictionary<Entity, string>();

    public void Run()
    {
        // Setup: 2 entities
        var nominal = new Entity("Nominal", onSolve: () =>
        {
            Console.WriteLine("  [Solve] Nominal.Solve() called - triggering re-association");

            // Simulate: Remove old Actual, add new Actual
            var oldActual = _entities.FirstOrDefault(e => e.Name == "OldActual");
            var newActual = new Entity("NewActual");

            _entities.Remove(oldActual);
            _entities.Add(newActual);

            Console.WriteLine($"  [Solve] Removed {oldActual?.Name}, Added {newActual.Name}");
            Console.WriteLine($"  [Solve] _entities now has {_entities.Count} items");

            _factoryMap.Clear();

            //_factoryMap[nominal] = "NominalProvider";
            _factoryMap[newActual] = "ActualProvider";

            Console.WriteLine($"  [Solve] _factoryMap now has {_factoryMap.Count} items");
        });

        var oldActual = new Entity("OldActual");

        _entities.Add(nominal);
        _entities.Add(oldActual);

        _factoryMap[nominal] = "NominalProvider";
        _factoryMap[oldActual] = "ActualProvider";

        Console.WriteLine($"Initial state: {_entities.Count} entities, {_factoryMap.Count} in map");
        Console.WriteLine($"  - {_entities[0].Name}");
        Console.WriteLine($"  - {_entities[1].Name}\n");

        // THE CRITICAL LINE: Deferred LINQ query
        var entitiesToEdit = _entities.Where(e => _factoryMap.ContainsKey(e));

        Console.WriteLine("Starting foreach iteration...\n");

        int iteration = 1;
        try
        {
            foreach (var entity in entitiesToEdit)
            {
                Console.WriteLine($"Iteration {iteration}: Processing {entity.Name}");

                if (entity.Name == "Nominal")
                {
                    entity.Solve(); // This modifies _entities during iteration!
                }

                Console.WriteLine($"Iteration {iteration}: Completed\n");
                iteration++;
            }

            Console.WriteLine("✅ No exception thrown!");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"❌ Exception thrown: {ex.Message}");
        }

        Console.WriteLine($"\nFinal state: {_entities.Count} entities");
        foreach (var e in _entities)
        {
            Console.WriteLine($"  - {e.Name}");
        }
    }

    [DebuggerDisplay("Name = {Name}")]
    class Entity
    {
        public string Name { get; }
        private Action _onSolve;

        public Entity(string name, Action onSolve = null)
        {
            Name = name;
            _onSolve = onSolve;
        }

        public void Solve()
        {
            _onSolve?.Invoke();
        }
    }

    //static void Main()
    //{
    //    new Demo().Run();
    //}
}