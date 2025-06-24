using System;

enum TypeOfWork
{
    Home,
    Business,
    Server
}

class Device
{
    public string Name { get; set; }
    public double Price { get; set; }
    public DateTime ReleaseDate { get; set; }

    public Device() { }

    public Device(string name, double price, DateTime releaseDate)
    {
        Name = name;
        Price = price;
        ReleaseDate = releaseDate;
    }

    public override string ToString()
    {
        return $"Name: {Name}, Price: {Price}, ReleaseDate: {ReleaseDate.ToShortDateString()}";
    }
}

class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Person(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }
}

class Computer
{
    private Person owner;
    private TypeOfWork workType;
    private string ipAddress;
    private Device[] devices;

    public Computer() { }

    public Computer(Person owner, TypeOfWork workType, string ipAddress, Device[] devices)
    {
        this.owner = owner;
        this.workType = workType;
        this.ipAddress = ipAddress;
        this.devices = devices;
    }

    public Person Owner { get => owner; set => owner = value; }
    public TypeOfWork WorkType { get => workType; set => workType = value; }
    public string IpAddress { get => ipAddress; set => ipAddress = value; }
    public Device[] Devices { get => devices; set => devices = value; }

    public double TotalPrice
    {
        get
        {
            double sum = 0;
            if (devices != null)
                foreach (var d in devices)
                    sum += d.Price;
            return sum;
        }
    }

    public bool this[TypeOfWork type]
    {
        get => workType == type;
    }

    public void AddDevices(params Device[] newDevices)
    {
        if (devices == null)
        {
            devices = newDevices;
            return;
        }

        Device[] temp = new Device[devices.Length + newDevices.Length];
        devices.CopyTo(temp, 0);
        newDevices.CopyTo(temp, devices.Length);
        devices = temp;
    }

    public override string ToString()
    {
        string result = $"Owner: {owner}, WorkType: {workType}, IP: {ipAddress}, TotalPrice: {TotalPrice}\nDevices:\n";
        if (devices != null)
            foreach (var d in devices)
                result += d + "\n";
        return result;
    }

    public string ToShortString()
    {
        return $"Owner: {owner}, WorkType: {workType}, IP: {ipAddress}, TotalPrice: {TotalPrice}";
    }
}

class Program
{
    static void Main()
    {
        var owner = new Person("Ivan", "Ivanov");
        var comp = new Computer(owner, TypeOfWork.Home, "192.168.0.1", null);

        Console.WriteLine(comp.ToShortString());

        Console.WriteLine($"TypeOfWork.Home: {comp[TypeOfWork.Home]}");
        Console.WriteLine($"TypeOfWork.Business: {comp[TypeOfWork.Business]}");
        Console.WriteLine($"TypeOfWork.Server: {comp[TypeOfWork.Server]}");

        comp.Owner = new Person("Petr", "Petrov");
        comp.WorkType = TypeOfWork.Server;
        comp.IpAddress = "10.0.0.5";

        Console.WriteLine(comp.ToString());

        var dev1 = new Device("Printer", 250, new DateTime(2023, 5, 10));
        var dev2 = new Device("Scanner", 350, new DateTime(2022, 11, 5));
        comp.AddDevices(dev1, dev2);

        Console.WriteLine(comp.ToString());

        Computer[] computers = new Computer[5];
        for (int i = 0; i < computers.Length; i++)
        {
            computers[i] = new Computer(
                new Person($"User{i}", $"Surname{i}"),
                (TypeOfWork)(i % 3),
                $"192.168.1.{i + 1}",
                new Device[] {
                    new Device($"Device{i}_1", i * 100 + 50, DateTime.Now.AddYears(-i)),
                    new Device($"Device{i}_2", i * 200 + 100, DateTime.Now.AddYears(-i - 1))
                }
            );
        }
        foreach (var c in computers)
            Console.WriteLine($"{c.IpAddress} - {c.ToShortString()}");

        Computer maxDevicesComp = computers[0];
        foreach (var c in computers)
            if (c.Devices.Length > maxDevicesComp.Devices.Length)
                maxDevicesComp = c;

        Console.WriteLine("Computer with most devices:");
        Console.WriteLine(maxDevicesComp.ToShortString());
    }
}