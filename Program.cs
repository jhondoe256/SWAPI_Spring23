
//I need an HttpClient to make calls over the wire (online)


using Newtonsoft.Json;

HttpClient _client = new HttpClient();

var response = await _client.GetAsync("https://swapi.dev/api/people/1");

if (response.IsSuccessStatusCode)
{
    //This is for API consumption Get()
    string content = await response.Content.ReadAsStringAsync();
    System.Console.WriteLine(content);

    //How can we turn this Json Data to a C# object?
    Person person = JsonConvert.DeserializeObject<Person>(content);

    System.Console.WriteLine($"{person.Name} - {person.BirthYear}");

    //Another way: 
    Person luke = await response.Content.ReadAsAsync<Person>();
    System.Console.WriteLine(luke.Name + " " + luke.EyeColor);

    foreach (string vehicleUrl in luke.Vehicles)
    {
        HttpResponseMessage vehResponse = await _client.GetAsync(vehicleUrl);
       // System.Console.WriteLine(await vehResponse.Content.ReadAsStringAsync());

       Vehicle vehicle = await vehResponse.Content.ReadAsAsync<Vehicle>();
       System.Console.WriteLine($"{vehicle.Name} - {vehicle.CargoCapacity}");
    }
}

Console.ForegroundColor= ConsoleColor.DarkBlue;
System.Console.WriteLine("=== Using SWAPI Service ===");
Console.ResetColor();

SWAPI_Service service = new SWAPI_Service();

Person person1 = await service.GetPersonAsync("http://swapi.dev/api/people/11");
if(person1 != null)
{
    System.Console.WriteLine(person1.Name);

    foreach (string vehicleURL in person1.Vehicles)
    {
        Vehicle vehicle = await service.GetVehicleAsync(vehicleURL);
        System.Console.WriteLine(vehicle.Name); 
    }
}
else
{
    System.Console.WriteLine("The item doesn't exist.");
}

Console.ForegroundColor = ConsoleColor.DarkBlue;
Console.WriteLine("=== Using SWAPI Service Generics (vehicles) ===");
Console.ResetColor();

Vehicle veh = await service.GetAsync<Vehicle>(service.vehicleURL,30);
if(veh !=null)
{
    System.Console.WriteLine(veh.Name);
}
else
{
    System.Console.WriteLine("Targeted object not found!");
}

Console.ForegroundColor = ConsoleColor.DarkBlue;
Console.WriteLine("=== Using SWAPI Service Generics (Person) ===");
Console.ResetColor();

Person solo = await service.GetAsync<Person>(service.peopleURL,14);
if(solo != null)
{
    System.Console.WriteLine(solo.Name);
}
else
{
    System.Console.WriteLine("Targeted object not found!");
}

Console.ForegroundColor = ConsoleColor.DarkBlue;
Console.WriteLine("=== Using SWAPI Person Search ===");
Console.ResetColor();

var personSearch = await service.GetPersonSearchAsync("skywalker");
if(personSearch != null)
{
    //I just want to see the results
    foreach (var searchItem in personSearch.Results)
    {
        System.Console.WriteLine(searchItem.Name);
    }
}
else
{
    System.Console.WriteLine("Not found.");
}

var dynamicSearch = await service.GetSearchResultAsync<Vehicle>("speed","vehicles");
if(dynamicSearch != null)
{
     foreach (var searchItem in dynamicSearch.Results)
    {
        System.Console.WriteLine(searchItem.Name);
    }
}
else
{
     System.Console.WriteLine("Not found.");
}