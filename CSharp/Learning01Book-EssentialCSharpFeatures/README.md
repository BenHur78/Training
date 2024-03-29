# Introduction

In this learning we will follow Pro ASP.NET Core 6 9th edition Adam Freeman book, Chapter 05, Essential C# Features. You can find the book github repository [here.](https://github.com/apress/pro-asp.net-core-6). The book was written in 2022, so the last version of C# in 2022 year was C# 11. This means I think we will see some C# 10 features.

# List of Learning's

## Reducing duplication in using statements
- To reduce duplicated using statements we can use _global using statements_ in a file _GlobalUsing.cs_
```
global using LanguageFeatures.Models;
global using Microsoft.AspNetCore.Mvc;
```
- There are also implicit using statements. ASP.NET core defines global using statements for the commonly required namespaces and that is why we do not need to define using statements for those namespaces in _Program.cs_ file
```
System
System.Collections.Generic
System.IO
System.Linq
System.Net.Http
System.Net.Http.Json
System.Threading
System.Threading.Tasks
Microsoft.AspNetCore.Builder
Microsoft.AspNetCore.Hosting
Microsoft.AspNetCore.Http
Microsoft.AspNetCore.Routing
Microsoft.Extensions.Configuration
Microsoft.Extensions.DependencyInjection
Microsoft.Extensions.Hosting
Microsoft.Extensions.Logging
```


## Managing null values using nullable and non-nullable types
1. Using default values for non-nullable properties. This is before not having initialization:

```
public string Name { get; set; }
```
2. Here we provide a default value for the non-nullable property, fixing the warning:
```
public string Name { get; set; } = string.Empty;
```

3. We have a warning here, Warning CS8625 Cannot convert null literal to non-nullable reference type.

```
public static Product[] GetProducts() {

...
    return new Product[] { kayak, lifejacket, null };

}
```

4. Using _?_ we can turn a non-nullable type to nullable type

```
public static Product?[] GetProducts() {

...
    return new Product?[] { kayak, lifejacket, null };

}
```

5. Some care when using the question mark when defining array's or collection's
```

Product?[] arr1 = new Product?[] { kayak, lifejacket, null }; //OK because a Product? can be null.
Product?[] arr2 = null; //Not Ok because the array of products cannot be null itself!

Product[]? arr1 = new Product?[] { kayak, lifejacket, null }; //Not Ok because the array of products contain a null reference but we are declaring a Product[] and not a Product?[]!

Product[]? arr2 = null; //Ok because the array of products itself is allowed to be null.

Product?[]? arr1 = new Product?[] { kayak, lifejacket, null }; //Ok because the array of products at the right hand side is not null and this is allowed and at the same time is also allowed for the array to contain a null reference. 

Product?[]? arr2 = null; //Ok because the array of products is allowed to be null.

```

6. Guarding against null in a verbose way. The compiler understand that after an if/else block the variable cannot be null, in this way a warning will be not generated.
```
...
public ViewResult Index() {

    Product?[] products = Product.GetProducts();
    
    Product? product= products[0];
    string val;

    if(product != null)
    {
        val= product.Name;
    }
    else
    {
        val = "No value";
    }

    return View(new string[] { val });
}
```

7. Using the null conditional operator ?. Be aware that the null conditional operator can return null, so the result need to be assigned to a nullable type like _string?_.

```
public ViewResult Index() {

    Product?[] products = Product.GetProducts();
    
    string? val = products[0]?.Name;

    if (val != null)
    {
        return View(new string[] { val });
    }
    
    return View(new string[] { "No Value" });
}
```

8. Using the null coalescing operator _??_ in conjunction with the null conditional operator _?_. There are cases where the ?? operator cannot be used with ? like await/async scenarios.

```
public ViewResult Index() {

    Product?[] products = Product.GetProducts();
        
    return View(new string[] { products[0]?.Name ?? "No Value" });
}
```

9. Using the null-forgiving _!_ operator to override null state analysis. The C# compiler has a sophisticated understanding of when a variable can be null, but it doesn’t always get it right, and there are times when you have a better understanding of whether a null value can arise than the compiler. In these situations, the null-forgiving operator can be used to tell the compiler that a variable isn’t null, regardless of what the null state analysis suggests:

```  
    return View(new string[] { products[0]!.Name });
```

You need to be right, otherwise you will get a NullReferenceException.

10. Disabling null state analysis warnings
```  
#pragma warning disable CS8602
...
#pragma warning restore CS8602
```


## Mixing static and dynamic values in strings using string interpolation
1. String interpolation is the process of replacing placeholder's with value's
```  
return View(new string[] { $"Name: {products[0]?.Name}, Price: {products[0]?.Price:C2}" } );
```


## Initializing and populate objects using object and collection initializers and target-typed new expressions

1. Using object initializer. Instead of:
```  
Product kayak = new Product();
kayak.Name = "Kayak";
kayak.Price = 275M;
```

We can have

```  
Product kayak = new Product
{
    Name = "Kayak",
    Price = 275M
};
```

2. Using collection initializer, allow us to specify the contents of the collection during the construction. Instead of:
```  
string[] names = new string[3];
names[0] = "Bob";
names[1] = "Joe";
names[2] = "Alice";
```

We can have

```  
new string[] { "Bob", "Joe", "Alice" }
```

3. There is a new feature that simplifies the initialization of indexed collections like dictionaries. This is the traditional C# approach:
```  
public ViewResult Index() {

    Dictionary<string, Product> products = new Dictionary<string, Product>
    {
        { "Kayak", new Product { Name = "Kayak", Price = 275M} },
        { "Lifejacket", new Product { Name = "Lifejacket", Price = 48.95M} }
    };
    
    return View("Index", products.Keys);
}
```

With the new feature we reduce the number of _{_ and _}_ character's

```  
Dictionary<string, Product> products = new Dictionary<string, Product>
{
        ["Kayak"] = new Product { Name = "Kayak", Price = 275M} ,
        ["Lifejacket"] = new Product { Name = "Lifejacket", Price = 48.95M}
};
```

4. We can simplify further the previous example using target-type new expression. We use "target-type" because the context is being used as a target for the compiler in order for it to infer the type.

```  
Dictionary<string, Product> products = new ()
{
        ["Kayak"] = new Product { Name = "Kayak", Price = 275M} ,
        ["Lifejacket"] = new Product { Name = "Lifejacket", Price = 48.95M}
};
```


## Assigning a value for specific types using pattern matching

1. Testing if an object is of a specific type (in this example a decimal) and if yes, we assign the value to a variable, is a form of pattern matching
```  
public ViewResult Index() {

            object[] data = new object[] { 275M, 29.95M, "apple", "orange", 100, 10 };
            decimal total = 0;

            for(int i = 0; i < data.Length; i++)
            {
                if (data[i] is decimal d)
                { 
                    total += d;
                }
            }
            
            return View("Index", new string[] { $"Total: {total:C2}" });
        }
```

2. We can use pattern matching in switch statement's:

```  
object[] data = new object[] { 275M, 29.95M, "apple", "orange", 100, 10 };
decimal total = 0;

for(int i = 0; i < data.Length; i++)
{
    switch(data[i])
    {
        case decimal decimalValue:
            total+= decimalValue;
            break;
        case int intValue when intValue > 50:
            total+= intValue;
            break;
    }

}
```

## Extending the functionality of a class without modifying it using extension methods

## Expressing functions and methods concisely using lambda expressions

## Defining a variable without explicitly declaring its type using var keyword

1. Its possible to create an anonymous type that represents a view model object without to have to define a class or a struct. We create it using object initializers.
```  
public ViewResult Index()
{
    var products = new[]
    {
        new { MyName = "Kayak", MyPrice = 275M },
        new { MyName = "Lifejacket", MyPrice = 48.95M },
        new { MyName = "Soccer ball", MyPrice = 19.50M },
        new { MyName = "Corner flag", MyPrice = 34.95M },
    };
    
    return View(products.Select(p => p.GetType().ToString()));
}
```


## Modifying an interface without requiring changes in ints implementation classes using default implementation

## Performing work asynchronously using tashs and async/await keywords

## Producing a sequence of values over time using an asynchronous enumerable

## Getting the name of a class or member using nameof expression
