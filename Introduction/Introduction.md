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

> Instance: TODO: Given that a class is a blueprint or template the instance is the thing or object created from that template. And represents the features and behaviors of something from the real world.

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
