using NationBuilder;
using NationBuilder.Models;
using System.Threading.Tasks;
using System;

namespace NationBuilderExercises
{
    class Program
    {
        //Place your slug
        private static string _slug = "rzraik";
        //Place your access token
        private static string _accessToken = "21eb924415a52a29a628793afffadc960d44d481861f693498067caf686a83d3";

        static void Main(string[] args)
        {
            try
            {
                TestPersons().Wait();
                TestEvents().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private static async Task TestPersons()
        {
            var client = new NBClient(_slug, _accessToken);

            // Get all events
            //var NBPersons = await client.GetAllRecords<NBPerson>();

            // Create a person
            var NBPerson = new NBPerson() { Sex = "M", FirstName = "Leo", LastName = "Hernandezz", Email = "LeoTest1010@test.com" };
            Console.WriteLine("Creating a person: Name = {0}, Email = {1} ...", NBPerson.FirstName + " " + NBPerson.LastName, NBPerson.Email);
            await client.CreateRecordAsync<NBPerson, NBPersonWrapper>(new NBPersonWrapper { Record = NBPerson });

            // Update the person
            NBPerson.Email = "test2@test.com";
            Console.WriteLine("Updating the person's email to {0} ...", NBPerson.Email);
            await client.UpdateRecordAsync<NBPerson, NBPersonWrapper>(NBPerson.Id, new NBPersonWrapper { Record = NBPerson });

            // Delete the person
            Console.WriteLine("Deleting the person...");
            await client.DeleteRecordAsync<NBPerson>(NBPerson.Id);
        }

        private static async Task TestEvents()
        {
            var client = new NBClient(_slug, _accessToken);
            
            // Get all events
            //var NPEvents = await client.GetAllRecords<NBEvent>();

            // Create an event
            var NBEvent = new NBEvent() { StartTime = "2013/01/04", EndTime = "2016/09/08", Intro = "Intro", Name = "Event Name", Status = "unlisted" };
            Console.WriteLine("Creating an event: Name = {0}, StartTime = {1}, EndTime = {2} ...", NBEvent.Name, NBEvent.StartTime, NBEvent.EndTime);
            await client.CreateRecordAsync<NBEvent, NBEventWrapper>(new NBEventWrapper { Record = NBEvent });

            // Update the event
            NBEvent.Name = "New Nam";
            Console.WriteLine("Updating the event's name to {0} ...", NBEvent.Name);
            await client.UpdateRecordAsync<NBEvent, NBEventWrapper>(NBEvent.Id, new NBEventWrapper { Record = NBEvent });

            // Delete the event
            Console.WriteLine("Deleting the event...");
            await client.DeleteRecordAsync<NBEvent>(NBEvent.Id);
        }
    }
}