using NationBuilder;
using NationBuilder.Models;
using System.Threading.Tasks;

namespace NationBuilderExercises
{
    class Program
    {
        private static string _slug = "YOUR_SLUG";
        private static string _accessToken = "ACCESS_TOKEN";

        static void Main(string[] args)
        {
            TestPersons().Wait();
            TestEvents().Wait();
        }

        private static async Task TestPersons()
        {
            var client = new NBClient(_slug, _accessToken);
            
            // Get all events
            var NBPersons = await client.GetAllRecords<NBPerson>();

            // Create a person
            var NBPerson = new NBPerson() { Sex = "M", FirstName = "Leo", LastName = "Hernandez", Email = "test@test.com" };
            await client.CreateRecordAsync<NBPerson, NBPersonWrapper>(new NBPersonWrapper { Record = NBPerson });

            // Update the person
            NBPerson.Email = "test2@test.com";
            await client.UpdateRecordAsync<NBPerson, NBPersonWrapper>(NBPerson.Id, new NBPersonWrapper { Record = NBPerson });

            // Delete the person
            await client.DeleteRecordAsync<NBPerson>(NBPerson.Id);
        }

        private static async Task TestEvents()
        {
            var client = new NBClient(_slug, _accessToken);
            
            // Get all events
            var NPEvents = await client.GetAllRecords<NBEvent>();

            // Create an event
            var NBEvent = new NBEvent() { StartTime = "2013/01/04", EndTime = "2016/09/08", Intro = "Intro", Name = "Event Name", Status = "unlisted" };
            await client.CreateRecordAsync<NBEvent, NBEventWrapper>(new NBEventWrapper { Record = NBEvent });

            // Update the event
            NBEvent.Name = "New Nam";
            await client.UpdateRecordAsync<NBEvent, NBEventWrapper>(NBEvent.Id, new NBEventWrapper { Record = NBEvent });

            // Delete the event
            await client.DeleteRecordAsync<NBEvent>(NBEvent.Id);
        }
    }
}