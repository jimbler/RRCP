using RRCP;

TestInputs();

static void TestInputs()
{
    Console.WriteLine("Red Rover Coding Puzzle ..............\n");
    Console.WriteLine("Use test input? (id, name, email, type(id, name, customFields(c1, c2, c3)), externalId)");
    Console.WriteLine("y/n");
    var useTestInput = Console.ReadLine();

    if (useTestInput == "y")
    {
        var testInput = "(id, name, email, type(id, name, customFields(c1, c2, c3)), externalId)";
        var props = Property.CreatePropertiesFromString(testInput);
        var output1 = Property.FormatProps(props);
        var output2 = Property.FormatProps(Property.SortProps(props));
        Console.WriteLine(output1);
        Console.WriteLine(output2);
    }
    else
    {
        Console.WriteLine("Enter your input:");
        var testInput = Console.ReadLine();
        var props = Property.CreatePropertiesFromString(testInput);
        var output1 = Property.FormatProps(props);
        var output2 = Property.FormatProps(Property.SortProps(props));
        Console.WriteLine(output1);
        Console.WriteLine(output2);
    }

    Console.WriteLine("Try again? y/n");
    var repeat = Console.ReadLine();
    if (repeat == "y")
    {
        TestInputs();
    }
    else
    {
        Console.WriteLine("Goodbye!");
    }
}