using System.Runtime.CompilerServices;
using System.Xml.Linq;
using static Address;

public class program 
{
    public static void Main(string[] args)
    {

        List<Address> addressList = new List<Address>()
        {
            new Address("Berta van Duiserwijk", "Volkstraat", 1, "Amsterdam", "3104YD", "BertavDW@outlook.com"),
            new Address("Kees de Jong", "Volkstraat", 2, "Amsterdam", "3104HD", "KeesDeJong@gmail.com"),
            new Address("Sophie van Rijn", "Kerkstraat", 10, "Rotterdam", "3021DE", "SophieVR@gmail.com"),
            new Address("Jan Meijer", "Lindelaan", 15, "Utrecht", "3512BP", "JanMeijer@hotmail.com"),
            new Address("Emma van Leeuwen", "Hoogstraat", 22, "Den Haag", "2512GN", "EmmaVL@yahoo.com"),
            new Address("Peter van den Berg", "Marktstraat", 5, "Groningen", "9711HM", "PeterVB@hotmail.com"),
            new Address("Anouk Visser", "Nieuwstraat", 8, "Eindhoven", "5611AX", "AnoukV@gmail.com")
        };

        AddressBook addressBook = new AddressBook(addressList);

        while (true)
        {
            ChoiceMenu();
            Console.Write("Voer jouw keuze in: \n");
            string input = Console.ReadLine();

            // Stop the program if user put in 'x'
            if (input.ToLower() == "x")
            {
                break;
            }

            if (!int.TryParse(input, out int user_choice))
            {
                Console.WriteLine("Ongeldige invoer, voer een getal in.\n");
                continue;
            }

            if (!(user_choice >= 1 && user_choice <= 5))
            {
                Console.WriteLine("Ongeldige keuze, kies uit 1 t/m 5.\n");
                continue;
            }

            // Share the whole List
            if (user_choice == 1)
            {
                foreach (Address address in addressList)
                {
                    Console.WriteLine("-------------------------------------------\n"
                    + $"Naam: {address.Name}\n"
                    + $"Adres: {address.StreetName} {address.HouseNumber}\n"
                    + $"Stad: {address.City}\n"
                    + $"Postcode: {address.PostCode}\n"
                    + $"E-mail: {address.Email}\n"
                    + "-------------------------------------------\n");
                    Thread.Sleep(200);
                }
                Thread.Sleep(1000);
            }

            // Filter menu
            if (user_choice == 2)
            {
                FilterOn();
                string FilterInput = Console.ReadLine();

                if (FilterInput.ToLower() == "x")
                {
                    continue;
                }
                else if (!int.TryParse(FilterInput, out int Filter_User_Choice))
                {
                    Console.WriteLine("Ongeldige invoer, voer een getal in.\n");
                    continue;
                }
                else if (!(Filter_User_Choice >= 1 && Filter_User_Choice <= 6))
                {
                    Console.WriteLine("Ongeldige keuze, kies uit 1 t/m 6.\n");
                    continue;
                }
                else
                {
                    Filter(Filter_User_Choice, addressBook);
                }
            }

            // Add someone
            if (user_choice == 3)
            {
                Console.WriteLine("\nVoeg hier een naam in:");
                String Name = Console.ReadLine();
                addressBook.setName(Name, Name);

                Console.WriteLine("\nVoeg hier een straat naam in:");
                addressBook.setStreetName(Name, Console.ReadLine());

                Console.WriteLine("\nVoeg hier een huisnummer in:");
                string HouseNumber = Console.ReadLine();

                if (!int.TryParse(HouseNumber, out int HouseNumber_User_Choice))
                {
                    Console.WriteLine("Ongeldige invoer, voer een getal in.");
                    continue;
                }
                else
                {
                    addressBook.setHouseNumber(Name, HouseNumber_User_Choice);
                }

                Console.WriteLine("\nVoeg hier een dorp/stad naam in:");
                addressBook.setCity(Name, Console.ReadLine());

                Console.WriteLine("\nVoeg hier een postcode in:");
                addressBook.setPostCode(Name, Console.ReadLine());

                Console.WriteLine("\nVoeg hier een email in:");
                addressBook.setEmail(Name, Console.ReadLine());

                Thread.Sleep(200);
                Console.WriteLine("\nContact is toegevoegd!");
                Thread.Sleep(1000);

                List<Address> foundAddresses = addressBook.GetNames(Name);

                if (foundAddresses.Count == 0)
                {
                    Console.WriteLine("\nEr is niemand gevonden met deze naam.\n");
                    return;
                }
                else if (foundAddresses.Count > 1)
                {
                    Console.WriteLine("\nDe volgende mensen zijn gevonden met deze naam. Specificeer beter:");
                    foreach (var address in foundAddresses)
                    {
                        Console.WriteLine($"Naam: {address.Name}, Straat: {address.StreetName}, Huisnummer: {address.HouseNumber}, Stad: {address.City}");
                    }
                    Console.WriteLine("");
                    return;
                }
                else
                {
                    Address person = foundAddresses.First();
                    Console.WriteLine("\n-------------------------------------------");
                    Console.WriteLine($"Naam: {person.Name}");
                    Console.WriteLine($"Adres: {person.StreetName} {person.HouseNumber}");
                    Console.WriteLine($"Stad: {person.City}");
                    Console.WriteLine($"Postcode: {person.PostCode}");
                    Console.WriteLine($"E-mail: {person.Email}");
                    Console.WriteLine("-------------------------------------------\n");
                }

                Thread.Sleep(1000);

            }

            // Edit someone
            if (user_choice == 4)
            {
                editAdress();
                string EditInput = Console.ReadLine();

                if (EditInput.ToLower() == "x")
                {
                    continue;
                }
                else if (!int.TryParse(EditInput, out int Edit_User_Choice))
                {
                    Console.WriteLine("Ongeldige invoer, voer een getal in.\n");
                    continue;
                }
                else if (!(Edit_User_Choice >= 1 && Edit_User_Choice <= 6))
                {
                    Console.WriteLine("Ongeldige keuze, kies uit 1 t/m 6.\n");
                    continue;
                }
                else
                {
                    Console.WriteLine("Geef graag een (oude)naam op om aan te passen!");
                    string name = Console.ReadLine();

                    if (Edit_User_Choice == 1)
                    {
                        foreach (Address address in addressList)
                        {
                            if (!(address.Name.ToLower() == name.ToLower()))
                            {
                                Console.WriteLine($"{address.Name} does not exist");
                                continue;
                            }

                            Console.WriteLine("Wat word de nieuwe naam?:");
                            addressBook.setName(Console.ReadLine(), name);

                            Console.WriteLine("-------------------------------------------\n"
                            + $"Naam: {address.Name}\n"
                            + $"Adres: {address.StreetName} {address.HouseNumber}\n"
                            + $"Stad: {address.City}\n"
                            + $"Postcode: {address.PostCode}\n"
                            + $"E-mail: {address.Email}\n"
                            + "-------------------------------------------\n");
                            Thread.Sleep(200);
                            break;
                        }
                        Thread.Sleep(1000);
                    }

                    if (Edit_User_Choice == 2)
                    {
                        foreach (Address address in addressList)
                        {
                            if (!(address.Name.ToLower() == name.ToLower()))
                            {
                                Console.WriteLine($"{address.Name} does not exist");
                                continue;
                            }

                            Console.WriteLine("Wat word de nieuwe straatnaam?:");
                            addressBook.setStreetName(name, Console.ReadLine());

                            Console.WriteLine("-------------------------------------------\n"
                            + $"Naam: {address.Name}\n"
                            + $"Adres: {address.StreetName} {address.HouseNumber}\n"
                            + $"Stad: {address.City}\n"
                            + $"Postcode: {address.PostCode}\n"
                            + $"E-mail: {address.Email}\n"
                            + "-------------------------------------------\n");
                            Thread.Sleep(200);
                            break;
                        }
                        Thread.Sleep(1000);
                    }

                    if (Edit_User_Choice == 3)
                    {
                        foreach (Address address in addressList)
                        {
                            if (!(address.Name.ToLower() == name.ToLower()))
                            {
                                Console.WriteLine($"{address.Name} does not exist");
                                continue;
                            }

                            Console.WriteLine("Wat word het nieuwe huisnummer?:");
                            string streetNameInput = Console.ReadLine();

                            if (!int.TryParse(streetNameInput, out int streetName_User_Choice))
                            {
                                Console.WriteLine("Ongeldige invoer, voer een getal in.\n");
                                continue;
                            }

                            addressBook.setHouseNumber(name, streetName_User_Choice);

                            Console.WriteLine("-------------------------------------------\n"
                            + $"Naam: {address.Name}\n"
                            + $"Adres: {address.StreetName} {address.HouseNumber}\n"
                            + $"Stad: {address.City}\n"
                            + $"Postcode: {address.PostCode}\n"
                            + $"E-mail: {address.Email}\n"
                            + "-------------------------------------------\n");
                            Thread.Sleep(200);
                            break;
                        }
                        Thread.Sleep(1000);
                    }

                    if (Edit_User_Choice == 4)
                    {
                        foreach (Address address in addressList)
                        {
                            if (!(address.Name.ToLower() == name.ToLower()))
                            {
                                Console.WriteLine($"{address.Name} does not exist");
                                continue;
                            }

                            Console.WriteLine("Wat word het nieuwe stad/dorp?:");
                            addressBook.setCity(name, Console.ReadLine());

                            Console.WriteLine("-------------------------------------------\n"
                            + $"Naam: {address.Name}\n"
                            + $"Adres: {address.StreetName} {address.HouseNumber}\n"
                            + $"Stad: {address.City}\n"
                            + $"Postcode: {address.PostCode}\n"
                            + $"E-mail: {address.Email}\n"
                            + "-------------------------------------------\n");
                            Thread.Sleep(200);
                            break;
                        }
                        Thread.Sleep(1000);
                    }

                    if (Edit_User_Choice == 5)
                    {
                        foreach (Address address in addressList)
                        {
                            if (!(address.Name.ToLower() == name.ToLower()))
                            {
                                Console.WriteLine($"{address.Name} does not exist");
                                continue;
                            }

                            Console.WriteLine("Wat word de nieuwe postcode?:");
                            addressBook.setPostCode(name, Console.ReadLine());

                            Console.WriteLine("-------------------------------------------\n"
                            + $"Naam: {address.Name}\n"
                            + $"Adres: {address.StreetName} {address.HouseNumber}\n"
                            + $"Stad: {address.City}\n"
                            + $"Postcode: {address.PostCode}\n"
                            + $"E-mail: {address.Email}\n"
                            + "-------------------------------------------\n");
                            Thread.Sleep(200);
                            break;
                        }
                        Thread.Sleep(1000);
                    }

                    if (Edit_User_Choice == 6)
                    {
                        foreach (Address address in addressList)
                        {
                            if (!(address.Name.ToLower() == name.ToLower()))
                            {
                                Console.WriteLine($"{address.Name} does not exist");
                                continue;
                            }

                            Console.WriteLine("Wat word de nieuwe E-mail?:");
                            addressBook.setEmail(name, Console.ReadLine());

                            Console.WriteLine("-------------------------------------------\n"
                            + $"Naam: {address.Name}\n"
                            + $"Adres: {address.StreetName} {address.HouseNumber}\n"
                            + $"Stad: {address.City}\n"
                            + $"Postcode: {address.PostCode}\n"
                            + $"E-mail: {address.Email}\n"
                            + "-------------------------------------------\n");
                            Thread.Sleep(200);
                            break;
                        }
                        Thread.Sleep(1000);
                    }
                }

            }

            // Remove someone
            if (user_choice == 5)
            {
                Console.WriteLine("Geef een naam op die je wilt verwijderen:");
                string Name = Console.ReadLine();

                List<Address> foundAddresses = addressBook.GetNames(Name);

                if (foundAddresses.Count == 0)
                {
                    Console.WriteLine("\nEr is niemand gevonden met deze naam.\n");
                    return;
                }
                else if (foundAddresses.Count > 1)
                {
                    Console.WriteLine("\nDe volgende mensen zijn gevonden met deze naam. Specificeer beter:");
                    foreach (var address in foundAddresses)
                    {
                        Console.WriteLine($"Naam: {address.Name}, Straat: {address.StreetName}, Huisnummer: {address.HouseNumber}, Stad: {address.City}");
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
        }
    }

    static void Filter(int input, AddressBook addressBook)
    {

        Console.WriteLine("Voer een zoekterm in: ");
        string FilterInput = Console.ReadLine();
        List<Address> filteredAddresses = new List<Address>();

        switch (input)
        {
            case 1:
                filteredAddresses = addressBook.GetNames(FilterInput);
                break;
            case 2:
                filteredAddresses = addressBook.GetByStreetName(FilterInput);
                break;
            case 3:
                if (int.TryParse(FilterInput, out int houseNumber))
                {
                    filteredAddresses = addressBook.GetByHouseNumber(houseNumber);
                }
                else
                {
                    Console.WriteLine("Voer een geldig huisnummer in.");
                    return;
                }
                break;
            case 4:
                filteredAddresses = addressBook.GetByCity(FilterInput);
                break;
            case 5:
                filteredAddresses = addressBook.GetByPostCode(FilterInput);
                break;
            case 6:
                filteredAddresses = addressBook.GetByEmail(FilterInput);
                break;
        }

        if (filteredAddresses.Count > 0)
        {
            foreach (Address address in filteredAddresses)
            {
                Console.WriteLine("-------------------------------------------\n"
                + $"Naam: {address.Name}\n"
                + $"Adres: {address.StreetName} {address.HouseNumber}\n"
                + $"Stad: {address.City}\n"
                + $"Postcode: {address.PostCode}\n"
                + $"E-mail: {address.Email}\n"
                + "-------------------------------------------\n");
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
            + "2 - Straatnaam\n"
            + "3 - Huisnummer\n"
            + "4 - Stad/Dorp\n"
            + "5 - Postcode\n"
            + "6 - Email\n"
            + "\nx - Voer 'x' in om terug te gaan.\n"
            + "\n-------------------------------------------\n");
    }

    static void editAdress()
    {
        Console.WriteLine("-------------------------------------------\n"
            + "Wat wil je editen?:\n"
            + "\n1 - Naam\n"
            + "2 - Straatnaam\n"
            + "3 - Huisnummer\n"
            + "4 - Stad/Dorp\n"
            + "5 - Postcode\n"
            + "6 - Email\n"
            + "\nx - Voer 'x' in om terug te gaan.\n"
            + "\n-------------------------------------------\n");
    }
}

public class Address
{
    public string Name { get; set; }
    public string StreetName { get; set; }
    public int HouseNumber { get; set; }
    public string City { get; set; }
    public string PostCode { get; set; }
    public string Email { get; set; }

    public Address(string name, string streetName, int houseNumber, string city, string postCode, string email)
    {
        Name = name;
        StreetName = streetName;
        HouseNumber = houseNumber;
        City = city;
        PostCode = postCode;
        Email = email;
    }
}

public class AddressBook
{
    private List<Address> addresses;

    public AddressBook(List<Address> initialAddresses)
    {
        addresses = initialAddresses;
    }

    public List<Address> GetNames(string input)
    {
        return addresses.Where(a => a.Name.StartsWith(input, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public List<Address> GetByStreetName(string input)
    {
        return addresses.Where(a => a.StreetName.StartsWith(input, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public List<Address> GetByHouseNumber(int number)
    {
        return addresses.Where(a => a.HouseNumber == number).ToList();
    }

    public List<Address> GetByCity(string input)
    {
        return addresses.Where(a => a.City.StartsWith(input, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public List<Address> GetByPostCode(string input)
    {
        return addresses.Where(a => a.PostCode.StartsWith(input, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public List<Address> GetByEmail(string input)
    {
        return addresses.Where(a => a.Email.StartsWith(input, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public void setName(string newName, string oldName)
    {
        if (newName == null)
        {
            return;
        }

        List<Address> foundAddresses = GetNames(oldName);

        if (foundAddresses.Count == 0)
        {
            Address newPerson = new Address(newName, "Onbekende Straat", 0, "Onbekende Stad", "0000AA", "onbekend@example.com");
            addresses.Add(newPerson);
            Console.WriteLine($"Nieuwe contactpersoon '{newPerson.Name}' is toegevoegd!");
            return;
        }
        else
        {
            Address person = foundAddresses.First();
            person.Name = newName;
            Console.WriteLine($"{person.Name} is toegevoegd!");
        }
    }

    public void setStreetName(string name, string newStreetName)
    {
        if (newStreetName == null)
        {
            return;
        }

        List<Address> foundAddresses = GetNames(name);

        if (foundAddresses.Count == 0)
        {
            Console.WriteLine("Er is niemand gevonden met deze naam.");
            return;
        }
        else if (foundAddresses.Count > 1)
        {
            Console.WriteLine("De volgende mensen zijn gevonden met deze naam. Specificeer beter:");
            foreach (var address in foundAddresses)
            {
                Console.WriteLine($"Naam: {address.Name}, Straat: {address.StreetName}, Huisnummer: {address.HouseNumber}, Stad: {address.City}");
            }
            return;
        }
        else
        {
            Address person = foundAddresses.First();
            person.StreetName = newStreetName;
            Console.WriteLine($"Straatnaam van {person.Name} succesvol aangepast naar {newStreetName}.");
        }
    }

    public void setHouseNumber(string name, int houseNumber)
    {
        if (houseNumber == 0)
        {
            return;
        }

        List<Address> foundAddresses = GetNames(name);

        if (foundAddresses.Count == 0)
        {
            Console.WriteLine("Er is niemand gevonden met deze naam.");
            return;
        }
        else if (foundAddresses.Count > 1)
        {
            Console.WriteLine("De volgende mensen zijn gevonden met deze naam. Specificeer beter:");
            foreach (var address in foundAddresses)
            {
                Console.WriteLine($"Naam: {address.Name}, Straat: {address.StreetName}, Huisnummer: {address.HouseNumber}, Stad: {address.City}");
            }
            return;
        }
        else
        {
            Address person = foundAddresses.First();
            person.HouseNumber = houseNumber;
            Console.WriteLine($"Huisnummer van {person.Name} succesvol aangepast naar {houseNumber}.");
        }
    }

    public void setCity(string name, string CityName)
    {
        if (CityName == null)
        {
            return;
        }

        List<Address> foundAddresses = GetNames(name);

        if (foundAddresses.Count == 0)
        {
            Console.WriteLine("Er is niemand gevonden met deze naam.");
            return;
        }
        else if (foundAddresses.Count > 1)
        {
            Console.WriteLine("De volgende mensen zijn gevonden met deze naam. Specificeer beter:");
            foreach (var address in foundAddresses)
            {
                Console.WriteLine($"Naam: {address.Name}, Straat: {address.StreetName}, Huisnummer: {address.HouseNumber}, Stad: {address.City}");
            }
            return;
        }
        else
        {
            Address person = foundAddresses.First();
            person.City = CityName;
            Console.WriteLine($"Stad/Dorp van {person.Name} succesvol aangepast naar {CityName}.");
        }
    }

    public void setPostCode(string name, string postCode)
    {
        if (postCode == null)
        {
            return;
        }

        List<Address> foundAddresses = GetNames(name);

        if (foundAddresses.Count == 0)
        {
            Console.WriteLine("Er is niemand gevonden met deze naam.");
            return;
        }
        else if (foundAddresses.Count > 1)
        {
            Console.WriteLine("De volgende mensen zijn gevonden met deze naam. Specificeer beter:");
            foreach (var address in foundAddresses)
            {
                Console.WriteLine($"Naam: {address.Name}, Straat: {address.StreetName}, Huisnummer: {address.HouseNumber}, Stad: {address.City}");
            }
            return;
        }
        else
        {
            Address person = foundAddresses.First();
            person.PostCode = postCode;
            Console.WriteLine($"Postcode van {person.Name} succesvol aangepast naar {postCode}.");
        }
    }

    public void setEmail(string name, string email)
    {
        if (email == null)
        {
            return;
        }

        List<Address> foundAddresses = GetNames(name);

        if (foundAddresses.Count == 0)
        {
            Console.WriteLine("Er is niemand gevonden met deze naam.");
            return;
        }
        else if (foundAddresses.Count > 1)
        {
            Console.WriteLine("De volgende mensen zijn gevonden met deze naam. Specificeer beter:");
            foreach (var address in foundAddresses)
            {
                Console.WriteLine($"Naam: {address.Name}, Straat: {address.StreetName}, Huisnummer: {address.HouseNumber}, Stad: {address.City}");
            }
            return;
        }
        else
        {
            Address person = foundAddresses.First();
            person.Email = email;
            Console.WriteLine($"Email van {person.Name} succesvol aangepast naar {email}.");
        }
    }

    public void RemoveByName(string name)
    {
        List<Address> foundAddresses = GetNames(name);

        if (foundAddresses.Count == 0)
        {
            Console.WriteLine("Er is niemand gevonden met deze naam.");
            return;
        }
        else if (foundAddresses.Count > 1)
        {
            Console.WriteLine("De volgende mensen zijn gevonden met deze naam. Specificeer beter:");
            foreach (var address in foundAddresses)
            {
                Console.WriteLine($"Naam: {address.Name}, Straat: {address.StreetName}, Huisnummer: {address.HouseNumber}, Stad: {address.City}");
            }
            return;
        }
        else
        {
            Thread.Sleep(300);
            Address person = foundAddresses.First();
            addresses.Remove(person);
            Console.WriteLine($"{person.Name} is succesvol verwijderd.\n");
        }
    }
}