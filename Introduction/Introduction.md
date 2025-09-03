# 1. An Introduction to Object-Oriented

## 1.1. Class basics and benefits

### What is a class?

A class is like blueprint or a template that defines how objects should look and behave that are from the real-world. 

> They are known as entities

A class defines:

- Attributes (fields/properties): data it holds.
- Methods (functions): actions it can perform.

### What is an object ?

An object is an instance of a class - created based on the bluprint or template.

> Instance: TODO: Define what is an instance in your own words.

An instance is a concrete object that has been created from a class. The class defines the structure and behavior (attributes and methods), while the instance represents a specific realization of that class with its own values stored in the attibutes.
For example, the class `Car` is like a blueprint or a mold that describes what a car is, the brand and color. But a blueprint is not an actual car.
An instance is when you take that blueprint and build a real car: a red Toyota, a blue Honda. Both are cars, both follow the same definition, but each one has its own details.
So, a instance is the materialization of a class.

### Note

At the end of day a class is like a test case template, and objects as the executed test cases with actual data.

### Benefits of classes

- Reusability: Once defined, you can reuse the class in different parts of your code.
- Abstraction: Hide unnecessary details, show only what matters.
- Maintainability: Code is easier to manage and update.
- Scalability: Easy to extend and adapt to future needs.
- Encapsulation: Bundle data & behavior together according to a context.

## 1.2. Creating objects using the constructor

### What is a constructor? (Definition)

A constructor is a special method that is automatically called when an object of a class is created.

- It *usually* initializes fields or properties of the class
- Constructors do not have a return type (not even `void`)
- If you don't explicitly define one, C# provides a default constructor.

## 1.3. Using object reference

- In C#, when we create an object using `new`, what we actually store in a variable is **a reference to that object in memory**, not the object itself.
- A reference is like an "address" or a "pointer" to where the object lives in memory.
- Multiple variables can refer or point to the same object, meaning if you change the object through one reference, it will be reflected when accessed through the other reference.

```csharp
int num1 = 12;
int num2;

num2 = num1;

num1 = 15;

Console.WriteLine(num1); // 15
Console.WriteLine(num2); // 12

var test11 = new LoginTest
{
  Username = "user",
  Password = "password"
};

LoginTest test12;

test12 = test11;

test11.Username = "other user";

Console.WriteLine(test11.Username); // other user
Console.WriteLine(test12.Username); // other user
```
