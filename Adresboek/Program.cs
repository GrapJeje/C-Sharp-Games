using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using static Address;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class program 
{
    public static void Main(string[] args)
    {

        Dictionary<Person, Address> addressList = new Dictionary<Person, Address>();

        addressList.Add(
            new Person("Berta van Duiserwijk", "BertavDW@outlook.com", "123456789"),
            new Address("Volkstraat", "1", "Amsterdam", "3104YD")
            );

        addressList.Add(
            new Person("Kees de Jong", "KeesDeJong@gmail.com", "987654321"),
            new Address("Volkstraat", "2", "Amsterdam", "3104HD")
            );

        addressList.Add(
            new Person("Sophie van Rijn", "SophieVR@gmail.com", "555123456"),
            new Address("Kerkstraat", "10", "Rotterdam", "3021DE")
            );

        addressList.Add(
            new Person("Jan Meijer", "JanMeijer@hotmail.com", "444987123"),
            new Address("Lindelaan", "15", "Utrecht", "3512BP")
            );

        addressList.Add(
            new Person("Emma van Leeuwen", "EmmaVL@yahoo.com", "222555888"),
            new Address("Hoogstraat", "22", "Den Haag", "2512GN")
            );

        addressList.Add(
            new Person("Peter van den Berg", "PeterVB@hotmail.com", "666333222"),
            new Address("Marktstraat", "5", "Groningen", "9711HM")
            );

        addressList.Add(
            new Person("Anouk Visser", "AnoukV@gmail.com", "999111333"),
            new Address("Nieuwstraat", "8", "Eindhoven", "5611AX")
            );

        AddressBook addressBook = new AddressBook(addressList);

        while (true)
        {
            ChoiceMenu();
            Console.Write("Voer jouw keuze in: \n");
            string input = Console.ReadLine();

            switch(menuInput(input, 5))
            {
                case 1:
                    {
                        // Share the whole List
                        foreach (var entry in addressList)
                        {
                            Person person = entry.Key;
                            Address address = entry.Value;

                            AddressBook.AdressList(address, person);
                            Thread.Sleep(200);
                        }
                        Thread.Sleep(1000);
                        break;
                    }
                case 2:
                    {
                        // Filter menu
                        FilterOn();
                        string FilterInput = Console.ReadLine();

                        Filter(menuInput(input, 6), addressBook);
                        break;
                    }
                case 3:
                    {
                        // Add someone
                        Console.WriteLine("\nVoeg hier een naam in:");
                        string Name = Console.ReadLine();

                        AddUser(addressBook, Name);
                        break;
                    }
                case 4:
                    {
                        // Edit someone
                        EditUser(addressBook, addressList);
                        break;
                    }
                case 5:
                    {
                        // Remove someone
                        Console.WriteLine("Geef een naam op die je wilt verwijderen:");
                        string Name = Console.ReadLine();
                        RemoveUser(addressBook, Name);
                        break;
                    }
                default:
                    break;
            }
        }
    }

    static void AddUser(AddressBook addressBook, string Name)
    {
        addressBook.setName(Name, Name);

        Console.WriteLine("\nVoeg hier een email in:");
        addressBook.setEmail(Name, Console.ReadLine());

        Console.WriteLine("\nVoeg hier een telefoon nummer in:");
        addressBook.setPhoneNumber(Name, Console.ReadLine());

        Console.WriteLine("\nVoeg hier een straat naam in:");
        addressBook.setStreetName(Name, Console.ReadLine());

        Console.WriteLine("\nVoeg hier een huisnummer in:");
        addressBook.setHouseNumber(Name, Console.ReadLine());

        Console.WriteLine("\nVoeg hier een dorp/stad naam in:");
        addressBook.setCity(Name, Console.ReadLine());

        Console.WriteLine("\nVoeg hier een postcode in:");
        addressBook.setPostCode(Name, Console.ReadLine());

        Thread.Sleep(200);
        Console.WriteLine("\nContact is toegevoegd!");
        Thread.Sleep(1000);

        Dictionary<Person, Address> foundAddresses = addressBook.GetNames(Name);

        if (foundAddresses.Count == 0)
        {
            Console.WriteLine("\nEr is niemand gevonden met deze naam.\n");
            return;
        }
        else if (foundAddresses.Count > 1)
        {
            Console.WriteLine("\nDe volgende mensen zijn gevonden met deze naam. Specificeer beter:");
            foreach (var entry in foundAddresses)
            {
                    Person person = entry.Key;
                    Address address = entry.Value;

                AddressBook.AdressList(address, person);
                Thread.Sleep(200);
            }
            Console.WriteLine("");
            return;
        }
        else
        {
            foreach (var entry in foundAddresses)
            {
                Person person = entry.Key;
                Address address = entry.Value;

                AddressBook.AdressList(address, person);
            }
        }

        Thread.Sleep(1000);
    }

    static void EditUser(AddressBook addressBook, Dictionary<Person, Address> addressList)
    {
        EditAdress();
        string EditInput = Console.ReadLine();

        Console.WriteLine("Geef graag een (oude)naam op om aan te passen!");
        string name = Console.ReadLine();

        foreach (var entry in addressList)
        {
            Person person = entry.Key;
            Address address = entry.Value;

            if (person.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                switch (menuInput(EditInput, 6))
                {
                    case 1:
                        {
                            Console.WriteLine("Wat wordt de nieuwe naam?:");
                            addressBook.setName(Console.ReadLine(), name);
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Wat word de nieuwe E-mail adres?:");
                            addressBook.setEmail(name, Console.ReadLine());
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Wat word het nieuwe telefoon nummer?:");
                            addressBook.setPhoneNumber(name, Console.ReadLine());
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("Wat word de nieuwe straatnaam?:");
                            addressBook.setStreetName(name, Console.ReadLine());
                            break;
                        }
                    case 5:
                        {
                            Console.WriteLine("Wat word het nieuwe huisnummer?:");
                            addressBook.setHouseNumber(name, Console.ReadLine());
                            break;
                        }
                    case 6:
                        {
                            Console.WriteLine("Wat word het nieuwe stad/dorp?:");
                            addressBook.setCity(name, Console.ReadLine());
                            break;
                        }
                    case 7:
                        {
                            Console.WriteLine("Wat word de nieuwe postcode?:");
                            addressBook.setPostCode(name, Console.ReadLine());
                            break;
                        }
                }

                AddressBook.AdressList(address, person);
                Thread.Sleep(200);
                break;
            }
        }
    }

    static void RemoveUser(AddressBook addressBook, string Name)
    {
        Dictionary<Person, Address> foundAddresses = addressBook.GetNames(Name);

        if (foundAddresses.Count == 0)
        {
            Console.WriteLine("\nEr is niemand gevonden met deze naam.\n");
            return;
        }
        else if (foundAddresses.Count > 1)
        {
            Console.WriteLine("\nDe volgende mensen zijn gevonden met deze naam. Specificeer beter:");
            foreach (var entry in foundAddresses)
            {
                Person person = entry.Key;
                Address address = entry.Value;

                AddressBook.AdressList(address, person);
            }
            Console.WriteLine("");
            return;
        }
        else
        {
            addressBook.RemoveByName(Name);
            Thread.Sleep(1000);
        }
    }

    static void Filter(int input, AddressBook addressBook)
    {

        Console.WriteLine("Voer een zoekterm in: ");
        string FilterInput = Console.ReadLine();
        Dictionary<Person, Address> filteredAddresses = new Dictionary<Person, Address>();

        switch (input)
        {
            case 1:
                filteredAddresses = addressBook.GetNames(FilterInput);
                break;
            case 2:
                {
                    filteredAddresses = addressBook.GetByEmail(FilterInput);
                    break;
                }
            case 3:
                {
                    filteredAddresses = addressBook.GetByPhoneNumber(FilterInput);
                    break;
                }
            case 4:
                {
                    filteredAddresses = addressBook.GetByStreetName(FilterInput);
                    break;
                }
            case 5:
                {
                    filteredAddresses = addressBook.GetByHouseNumber(FilterInput);
                    break;
                }
            case 6:
                {
                    filteredAddresses = addressBook.GetByCity(FilterInput);
                    break;
                }
            case 7:
                {
                    filteredAddresses = addressBook.GetByPostCode(FilterInput);
                    break;
                } 
        }

        if (filteredAddresses.Count > 0)
        {
            foreach (var entry in filteredAddresses)
            {
                Person person = entry.Key;
                Address address = entry.Value;

                AddressBook.AdressList(address, person);
                Thread.Sleep(200);
            }
        }
        else
        {
            Console.WriteLine("Geen zoekresultaten gevonden!\n");
            Thread.Sleep(1000);
        }
    }

    static void ChoiceMenu()
    {
        Console.WriteLine("-------------------------------------------\n"
            + "Welkom in het adresboek!\n"
            + "Maak een keuze. \n"
            + "\n1 - Bekijk huidige lijst.\n"
            + "2 - Filteren.\n"
            + "3 - Voeg iemand toe.\n"
            + "4 - Pas iemand aan.\n"
            + "5 - Verwijder iemand.\n"
            + "\nx - Voer 'x' in om te stoppen.\n"
            + "\n-------------------------------------------\n");
    }

    static void FilterOn()
    {
        Console.WriteLine("-------------------------------------------\n"
            + "Filteren op:\n"
            + "\n1 - Naam\n"
            + "2 - E-mail \n"
            + "3 - Telefoon Nummer\n"
            + "4 - Straatnaam\n"
            + "5 - Huisnummer\n"
            + "6 - Stad/Dorp\n"
            + "7 - Postcode\n"
            + "\nx - Voer 'x' in om terug te gaan.\n"
            + "\n-------------------------------------------\n");
    }

    static void EditAdress()
    {
        Console.WriteLine("-------------------------------------------\n"
            + "Wat wil je editen?:\n"
            + "\n1 - Naam\n"
            + "2 - E-mail \n"
            + "3 - Telefoon Nummer\n"
            + "4 - Straatnaam\n"
            + "5 - Huisnummer\n"
            + "6 - Stad/Dorp\n"
            + "7 - Postcode\n"
            + "\nx - Voer 'x' in om terug te gaan.\n"
            + "\n-------------------------------------------\n");
    }

    static int menuInput(string input, int max)
    {
        // Stop the program if user put in 'x'
        if (input.ToLower() == "x")
        {
            Environment.Exit(0);
        }

        // Check if input is a number
        if (!int.TryParse(input, out int user_choice))
        {
            Console.WriteLine("Ongeldige invoer, voer een getal in.\n");
            return 0;
        }

        // Check if there is an option with that number
        if (!(user_choice >= 1 && user_choice <= max))
        {
            Console.WriteLine($"Ongeldige keuze, kies uit 1 t/m {max}.\n");
            return 0;
        }

        return user_choice;
    }
}
public class Person
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public Person(string name, string email, string phoneNumber)
    {
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
    }
}

public class Address
{
    public string StreetName { get; set; }
    public string HouseNumber { get; set; }
    public string City { get; set; }
    public string PostCode { get; set; }

    public Address(string streetName, string houseNumber, string city, string postCode)
    {
        StreetName = streetName;
        HouseNumber = houseNumber;
        City = city;
        PostCode = postCode;
    }
}

public class AddressBook
{
    private Dictionary<Person, Address> addresses;

    public AddressBook(Dictionary<Person, Address> initialAddresses)
    {
        addresses = initialAddresses;
    }
    public static void AdressList(Address address, Person person)
    {
        Console.WriteLine("-------------------------------------------\n"
                + $"Naam: {person.Name}\n"
                + $"E-mail: {person.Email}\n"
                + $"Telefoon Nummer: {person.PhoneNumber}\n"
                + $"Adres: {address.StreetName} {address.HouseNumber}\n"
                + $"Stad: {address.City}\n"
                + $"Postcode: {address.PostCode}\n"
                + "-------------------------------------------\n");
    }

    public bool CheckPerson(List<Person> foundPersons)
    {
        if (foundPersons.Count == 0)
        {
            Console.WriteLine("Er is niemand gevonden met deze naam.");
            return true;
        }

        if (foundPersons.Count > 1)
        {
            Console.WriteLine("De volgende mensen zijn gevonden met deze naam. Specificeer beter:");
            foreach (var person in foundPersons)
            {
                Address associatedAddress = addresses[person];
                AdressList(associatedAddress, person);
            }
            return true;
        }

        return false;
    }

    public Dictionary<Person, Address> GetNames(string input)
    {
        return addresses
            .Where(entry => entry.Key.Name.Contains(input, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(entry => entry.Key, entry => entry.Value);
    }

    public Dictionary<Person, Address> GetByEmail(string input)
    {
        return addresses
            .Where(entry => entry.Key.Email.Contains(input, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(entry => entry.Key, entry => entry.Value);
    }

    public Dictionary<Person, Address> GetByPhoneNumber(string input)
    {
        return addresses
            .Where(entry => entry.Key.PhoneNumber.Contains(input, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(entry => entry.Key, entry => entry.Value);
    }

    public Dictionary<Person, Address> GetByStreetName(string input)
    {
        return addresses
            .Where(entry => entry.Value.StreetName.Contains(input, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(entry => entry.Key, entry => entry.Value);
    }

    public Dictionary<Person, Address> GetByHouseNumber(string input)
    {
        return addresses
            .Where(entry => entry.Value.HouseNumber.Contains(input, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(entry => entry.Key, entry => entry.Value);
    }

    public Dictionary<Person, Address> GetByCity(string input)
    {
        return addresses
            .Where(entry => entry.Value.City.Contains(input, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(entry => entry.Key, entry => entry.Value);
    }

    public Dictionary<Person, Address> GetByPostCode(string input)
    {
        return addresses
            .Where(entry => entry.Value.PostCode.Contains(input, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(entry => entry.Key, entry => entry.Value);
    }

    public void setName(string newName, string oldName)
    {
        if (newName == null)
        {
            return;
        }

        var foundPersons = addresses
            .Where(entry => entry.Key.Name.Equals(oldName, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (!foundPersons.Any())
        {
            var newPerson = new Person(newName, "Onbekende Email", "0000000000");
            var newAddress = new Address("Onbekende straatnaam", "0", "Onbekende stad", "0000AA");

            addresses.Add(newPerson, newAddress);
            Console.WriteLine($"Nieuwe contactpersoon '{newName}' is toegevoegd!");
        }
        else
        {
            foreach (var entry in foundPersons)
            {
                Person person = entry.Key;
                Address address = entry.Value;

                addresses.Remove(person);
                person.Name = newName;
                addresses.Add(person, address);

                Console.WriteLine($"{newName} is toegevoegd!");
            }
        }
    }

    public void setEmail(string name, string email)
    {
        if (email == null)
        {
            return;
        }

        var foundPersons = addresses.Keys
            .Where(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (!CheckPerson(foundPersons))
            {
            Person foundPerson = foundPersons.First();
            foundPerson.Email = email;

            Console.WriteLine($"Email van {foundPerson.Name} succesvol aangepast naar {email}.");
        }
    }

    public void setPhoneNumber(string name, string phoneNumber)
    {
        if (phoneNumber == null)
        {
            return;
        }

        var foundPersons = addresses.Keys
            .Where(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (!CheckPerson(foundPersons))
        {
            Person foundPerson = foundPersons.First();
            foundPerson.PhoneNumber = phoneNumber;

            Console.WriteLine($"Telefoon nummer van {foundPerson.Name} succesvol aangepast naar {phoneNumber}.");
        }
    }

    public void setStreetName(string name, string newStreetName)
    {
        if (newStreetName == null)
        {
            return;
        }

        var foundPersons = addresses.Keys
            .Where(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (!CheckPerson(foundPersons))
        {
            Person foundPerson = foundPersons.First();
            Address associatedAddress = addresses[foundPerson];
            associatedAddress.StreetName = newStreetName;

            Console.WriteLine($"Straat naam van {foundPerson.Name} succesvol aangepast naar {newStreetName}.");
        }
    }

    public void setHouseNumber(string name, string houseNumber)
    {
        if (houseNumber == null)
        {
            return;
        }

        var foundPersons = addresses.Keys
            .Where(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (!CheckPerson(foundPersons))
        {
            Person foundPerson = foundPersons.First();
            Address associatedAddress = addresses[foundPerson];
            associatedAddress.HouseNumber = houseNumber;

            Console.WriteLine($"Huis nummer van {foundPerson.Name} succesvol aangepast naar {houseNumber}.");
        }
    }

    public void setCity(string name, string CityName)
    {
        if (CityName == null)
        {
            return;
        }

        var foundPersons = addresses.Keys
            .Where(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (!CheckPerson(foundPersons))
        {
            Person foundPerson = foundPersons.First();
            Address associatedAddress = addresses[foundPerson];
            associatedAddress.City = CityName;

            Console.WriteLine($"Stad/Dorp van {foundPerson.Name} succesvol aangepast naar {CityName}.");
        }
    }

    public void setPostCode(string name, string postCode)
    {
        if (postCode == null)
        {
            return;
        }

        var foundPersons = addresses.Keys
            .Where(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (!CheckPerson(foundPersons))
        {
            Person foundPerson = foundPersons.First();
            Address associatedAddress = addresses[foundPerson];
            associatedAddress.PostCode = postCode;

            Console.WriteLine($"Postcode van {foundPerson.Name} succesvol aangepast naar {postCode}.");
        }
    }

    public void RemoveByName(string name)
    {
        var foundPersons = addresses.Keys
            .Where(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (!CheckPerson(foundPersons))
        {
            Thread.Sleep(300);
            Person foundPerson = foundPersons.First();
            Address associatedAddress = addresses[foundPerson];

            addresses.Remove(foundPerson);

            Console.WriteLine($"{foundPerson.Name} is succesvol verwijderd.\n");
        }
    }
}