****************************************
Pro C# with .NET and .NET Core
****************************************

1. Where I am? Page 88.

checked/unchecked keyword

dynamic keyword p 96, chapter 16

pattern matching in Switch p106, p246

ref locals and returns p125

local functions p133

C# nullable types p150
Null conditional operator p150

Tuples p154

Default class constructor p165 

Partial classes, partial keyword p211

Understanding Member Shadowing p240, using new keyword to hide a parent method.

Exception filters p279

nameof keyword p1144

.NET Core Chapter 31, p1245 
	Entity Framework Core
	ASP.NET Core

Operators
---------
Conditional operator ?: p102 

****************************************
Doubts
****************************************
1. What is the value of using default constructor?
Example: bool b = new bool(); Why not using bool b = default; ?


****************************************
Concepts
****************************************

BigInteger p76
	Defined in System.Numerics. Integer's not constrained by a upper or lower limit.

Boxing/unboxing p330, 332
	a. The process of representing a value type (eg int) as a reference type. In other words is the process of converting
	a value from the stack to the heap.
	
	Boxing can be formally defined as the process of explicitly assigning a value type to a System.Object
	variable. When you box a value, the CLR allocates a new object on the heap and copies the value type’s value
	(25, in this case) into that instance. What is returned to you is a reference to the newly allocated heap-based
	object.
	// Box the int into an object reference.
	object boxedInt = myInt;
	
	b. boxing/unboxxing has performance issues (transfer of data between the heap and stack). It also not type safe
	(when you unbox it, CLR need to check if the variable that will store the value type is of the correct type; if you
	are using int and you are unboxing to long, an exception will be thrown).
	
	The heap objects needs to be garbage-collected, remember also of that.
	
	c. type safety. I touched on the issue of type safety when covering unboxing operations. Recall that you must unbox your
	data into the same data type it was declared as before boxing.

Case-sensitive p56 
	C# is a case-sensitive programming language. All C# keywords are lowercase.
	
Code library p22
	the concept of assembly and namespace help to create libraries

Code snippet's
	a. prop, create a property p200

Collections
	a.Hashtable, Dictionary p327. Dictionary is faster than Hastable and Dic is type safe in the sense a Dic is
	a generic and you specify the type at compile time. Hashtable is not type safe and you can store as key or value the
	type you want. Hashtable does not support null values but dic support it.
	
	b. collection initialization sintax p343
	List<Point> myListOfPoints = new List<Point>
	{
		new Point { X = 2, Y = 2 },
		new Point { X = 3, Y = 3 },
		new Point(PointColor.BloodRed){ X = 4, Y = 4 }
	};
	
	c. ObservableCollection<T>, ReadOnlyObservableCollection<T> p351
	The ObservableCollection<T> class is useful in that it has the ability to inform external objects when
	its contents have changed in some way (as you might guess, working with ReadOnlyObservableCollection
	<T> is similar but read-only in nature).


Curly bracket's sintax p87
	Example:
	// Using curly bracket syntax.
	string greeting = string.Format("Hello {0} you are {1} years old.", name, age);

Data type class hierarchy p72
	a. Notice that each type ultimately derives from System.Object
	b. Also note that many numerical data types (eg int, decimal, etc) derive from a class named System.ValueType. 
	Descendants of ValueType are automatically allocated on the stack and, therefore, have a predictable 
	lifetime and are quite efficient.
	c. On the other hand, types that do not have System.ValueType in their inheritance chain
	(such as System.Type, System.String, System.Array, System.Exception, and System.Delegate) are not
	allocated on the stack but on the garbage-collected heap.
	
	d. Example: int is only a shorthand notation for System.Int32. It derives from System.Object.
	This is also valid: 12.GetHastCode()! 12 as int is a System.ValueType and from "12" you have access to its
	members.


Default constructor p70
	It relates to Default literal concept. All intrinsic data types support a default constructor.
	This means a bool variable is set to false, a string is set to null, numeric data types are set to 0.
	Example of using a default constructor: bool b = new bool();

Default literal p70
	A new feature of C# 7.1. Example: int myInt = default; Assigning a default value to a variable.

Delegate p17
	a. Delegates are the .NET equivalent of a type-safe, C-style function pointer. The key difference is that 
		a .NET delegate is a class that derives from System.MulticastDelegate, rather than a simple pointer to a raw
		memory address.
		
	Delete is a class that inherits from MulticastDelegate, p71 
	
	b. Essentially, the .NET delegate type is a type-safe object that “points to” a
	method or a list of methods that can be invoked at a later time.
	
	Unlike a traditional C++ function pointer,
	however, .NET delegates are classes that have built-in support for multicasting and asynchronous method
	invocation.
	
	p365 - As you will see, a lambda expression is little more than an anonymous method in
	disguise and provides a simplified approach to working with delegates.
	
	c. Understand that you can never directly derive from these base classes (MulticastDelegate, Delegate) in your code (it is a
	compiler error to do so) p369
	
	d. Combine() is a method of System.MulticastDelegate/Delegate and is the same as using += operator, p370
	
	e. method group conversion p379
	//Rather than creating a delegate object where the only purpose is to pass a reference to OnCarEngineEvent method
	//Note: RegisterWithCarEngine receives as parameter a delegate.
	c1.RegisterWithCarEngine(new Car.CarEngineHandler(OnCarEngineEvent));
	
	f. delegate's can be generic p380
	// This generic delegate can represnet any method
	// returning void and taking a single parameter of type T.
	public delegate void MyGenericDelegate<T>(T arg);
	
	g. When the name of the delegate is not important we have built in delegate types like Action<>, Func<>, p382
	The Action<> delegate type can point only to methods that take a void return value. That is the difference with Func<>, Func<>
	can return a type.
	
	Be aware that the final type parameter of Func<> is always the return value of the method:
	//static string SumToString(int x, int y) {...}
	Func<int, int, string> funcTarget2 = new Func<int, int, string>(SumToString);
	
	h.the event keyword. The purpose of this is that you no longer need to have methods to do the registration and unregistration p386
	The registration/unregistration methods have the role to add/remove items from the delegate invocation list.
	
	//CarEngineHandler is a delegate. The delegate stores the invocation list. Remember, a delegate is a class and it contain a member that
	//is called the invocation list. The invocation list stores the delegate method's that will be invoked.
	//When Exploded event is fired, the CarEngineHandler invocation list will be called.
	public event CarEngineHandler Exploded;


Digit Separators p78
	You want to type a numeric type that has a huge number of digits than the eye can keep track of.
	Example: 123_456, using underscore as a digit separator.
	It also workks for binary data: 0b0010_0000

Dispose
	a. IDisposable p495.
	you can implement IDisposable to provide a way for the object user to clean up the object as soon as it is finished. 
	However, if the caller forgets to call Dispose(), the unmanaged resources may be held in memory indefinitely.
	
	b. using keyword is the same as using try/finally logic where Dispose() is invoked within finally block p497
	// Dispose() is called automatically when the using scope exits.
	using(MyResourceWrapper rw = new MyResourceWrapper())
	{
	// Use rw object.
	}
	
	//be aware that it is possible to declare multiple objects of the same type within a using scope
	// Use a comma-delimited list to declare multiple objects to dispose.
	using(MyResourceWrapper rw = new MyResourceWrapper(), rw2 = new MyResourceWrapper())
	{
	// Use rw and rw2 objects.
	}
	
	c. p498 While this syntax does remove the need to manually wrap disposable objects within try/finally logic,
	the C# using keyword unfortunately now has a double meaning (importing namespaces and invoking a
	Dispose() method).
	
	d. p499 a pattern on how to call Dispose()


Event keyword
	a. The events are related to delegates. 
	//What this means? CarEngineHandler is a delegate definition. It represents a list of methods that can be invoked. p386
	public event CarEngineHandler Exploded;
	
	b. EventHandler<T> is a generic delegate type. This is an improvement because we tend to repetitively define delegates that receives
	EventArgs as argument.
	//EventHandler<CarEventArgs> is a delegate that receives as arguments an object and an instance of CardEventArgs
	public event EventHandler<CarEventArgs> Exploded;
	public event EventHandler<CarEventArgs> AboutToBlow;
	
	public static void CarAboutToBlow(object sender, CarEventArgs e)...p393
	
	// Register event handlers.
	c1.AboutToBlow += CarAboutToBlow;
	
	// Anotehr way of registering event handlers.
	EventHandler<CarEventArgs> d = new EventHandler<CarEventArgs>(CarExploded); p394
	c1.Exploded += d;
	
	Note: the delegate type name is "EventHandler<CarEventArgs>"! Yes, it includes the generic parameter type CarEventArgs!.
	The definition of EventHandler<T> type is:
	public delegate void EventHandler<TEventArgs>(object? sender, TEventArgs e);

Expression-bodied members
	a. We can use "=>" operator to simplify the definitions of constructors, method bodies and properties p405
	
	public int Add(int x, int y)
	{
		return x + y;
	}
	
	//Note that you are not using curly brackets "{...}"!
	public int Add(int x, int y) => x + y;

Generics
	a. Related to boxing/unboxing. p337
	b. only classes, structures, interfaces, and delegates can be written generically; enum types cannot
	c. Constraining Type Parameters using where keyword p360
	Using this keyword, you can add a set of constraints to a given type parameter, which the C# compiler
	will check at compile time

	//Examples
	where T : class
	where T : new()

Interfaces
	a. When two interfaces declares the same method name, possible name clash, p304
	b. IEnumerable (GetEnumerator), IEnumerator (MoveNext, Current, Reset) p306
	c. How to hide GetEnumerator using explicit interface implementation p307
	d. ICloneable p313
	e. IComparable p316; IComparer p319

Iterators
	a. an iterator is a member that specifies how a container’s internal items should be returned when processed by foreach.
	b. Using yield return keyword p308. When the yield return statement is reached, the current location in the container is stored, 
	and execution is restarted from this location the next time the iterator is called.

Lambda expressions
	a. The main reason of using lambda expressions is to provide a clean, concise manner to define an
	anonymous method and therefore indirectly a manner to simplify working with delegates.
	
	b. Using the C# lambda operator (=>), you can
	specify a block of code statements (and the parameters to pass to those code statements) wherever a strongly
	typed delegate is required p365
	
	c. The lambda expression can be single statement or multiple statement's.
	//Single statement p401
	List<int> evenNumbers = list.FindAll((i) => ((i % 2) == 0));
	
	//Multiple statements using curly brackets
	List<int> evenNumbers = list.FindAll((i) =>
	{
		Console.WriteLine("value of i is currently: {0}", i);
		bool isEven = ((i % 2) == 0);
		return isEven;
	});
	
	d. Type inference is when you do not specify the parameters type of the lambda expression p102
	// msg, result type are infered by the compiler
	m.SetMathHandler( (msg, result) => {Console.WriteLine("Message: {0}, Result: {1}", msg, result);} );
	
	//Explicitly defining the parameter types
	m.SetMathHandler( (string msg, int result) => {Console.WriteLine("Message: {0}, Result: {1}", msg, result);} );
	
	e. LINQ programming model makes substancial uses of lambda expression's p405

Lazy
	a. p501 Lazy Object Instantiation
	Lazy object instantiation is useful not only to decrease allocation of unnecessary objects. You can
	also use this technique if a given member has expensive creation code, such as invoking a remote method,
	communication with a relational database, or whatnot.
	//
	private Lazy<AllTracks> allSongs = new Lazy<AllTracks>();	
	

Local functions
	a. p309 - one of the main uses of location functions is explained in this page.
	//
	private Lazy<AllTracks> allSongs = new Lazy<AllTracks>();	

Parameters
	a. To summarize, parameters are variables defined in a method declaration that are used to pass values into the method when it is called, 
	while arguments are the actual values that are passed into a method when it is called.

Null checking
	a. Delegate scenario, we need to check if its not nul. Here you cannot use the "!" operator because we are not accessing a member p375
	//listOfHandlers is a delegate type
	if (listOfHandlers != null)
	listOfHandlers("Sorry, this car is dead...");
	
	So yeah, there are certain scenarios you need to use an if to check if the reference is null or not.
	
	Also note that in this p375 example, the Car class does not have control, in the sense that, is the exterior environment that
	sets the delegate. When the class does not have control, you need to check for null reference's.
	
	b. Wait...in point "a." I said we need to check for null. But it seems we can use the null-conditional operator "?." p391
	//Instead of having this
	if (listOfHandlers != null)
	listOfHandlers("Sorry, this car is dead...");
	
	//We can have this
	Exploded?.Invoke("Sorry, this car is dead...");

Scope
	a. Curly bracket's defines scope's p404

Reference type p82
	a. A reference type is an object allocated on the garbage-collected managed heap.
	b. The equality operator ==, return true if the reference are pointing to the same object in memory.
	But this is not true for string's, where the operator compare the value of the string!

Sealed class  p15
	a. C# supplies another keyword, sealed, that prevents inheritance from occurring p126
	b. In C#, the sealed keyword is used to prevent a class or a method from being further derived or overridden by any derived classes.

String
	a. Strings are immutable p85
	b. Use StringBuilder to increase performance p86	

String comparision p83
	a. Using StringComparison enumeration
	Example:
	s1.Equals(s2, StringComparison.OrdinalIgnoreCase);
	//Above line is better than
	if (firstString.ToUpper() == secondString.ToUpper())
	{
	//Do something	
	}

String format character p66
	Example: Console.WriteLine("c format: {0:c}", 99999);

String interpolation p87
	a1. The curly bracket syntax illustrated within this chapter ({0}, {1}, and so on) has existed within the .NET
	platform since version 1.0. Starting with the release of C# 6, C# programmers can use an alternative syntax to
	build string literals that contain placeholders for variables. Formally, this is called string interpolation
	
	a2. This new approach allows you to directly embed the variables themselves, rather than tacking them on as a comma-delimited list
	
	Example:
	// Using curly bracket syntax.
	string greeting = string.Format("Hello {0} you are {1} years old.", name, age);
	
	// Using string interpolation
	string greeting2 = $"Hello {name} you are {age} years old.";

System.ValueType p72
	Example: int or System.Int32. System.Void or void are also value type.
	System.DateTime does not have a keyword like System.Int32 (int).
	System.TimeSpan is also a value type.
	Enumerations and structure's are also value type's.
	BigInteger is not a value type.

TryParse p75
	Example: bool.TryParse("True", out bool b); TryParse return true if the parsing was successful.
	If it was sucessful, the result of the parsing is b, a out parameter.

Verbatim strings p82
	Example: Using "@" to disable escaping, @"C:\MyApp\bin\Debug"
	// White space is preserved with verbatim strings.
	string myLongString = @"This is a very
		very
			very
				long string";
	Console.WriteLine(myLongString);
	//example where double quotes are preserved
	Console.WriteLine(@"Cerebus said ""Darrr! Pret-ty sun-sets""");

.csproj p40
	<LangVersion>7.1</LangVersion>, to specify C# language version.

.NET Framework, .NET Core, ,NET p31
	Mono project, Xamarin, .NET Core are implementations of CLI (Common Language Infrastructure) specification
	.NET Core distribution is not a complete carbon copy of the .NET Framework, is a subset.



****************************************
Tools
****************************************


ildasm.exe p28
	Using Microsoft Visual Studio 2022 (VS), Tools -> Command Line ->  Developer Command Prompt