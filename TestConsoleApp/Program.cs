using Backend.Models;

static void Main(string[] args)
{
    using (var db = new BBAcademyDbContext())
    {
        Console.WriteLine(db.Database.ProviderName);
    }
}