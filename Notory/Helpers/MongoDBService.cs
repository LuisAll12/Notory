using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media.Animation;
using System.Xml.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using Notory.Models;

namespace Notory.Helpers
{
    public class MongoDBService
    {
        private string connectionString = "mongodb://127.0.0.1:27017";


        private ObservableCollection<CalendarPost> _entries;

        public ObservableCollection<CalendarPost> Entries
        {
            get => _entries;
            set
            {
                _entries = value;
                //OnPropertyChanged(nameof(Entries));
            }
        }

        public void SetPost(BsonDocument document)
        {
            var client = new MongoClient(connectionString);

            var database = client.GetDatabase("Notory");

            var collection = database.GetCollection<BsonDocument>("CalendarPost");

            document = new BsonDocument
            {
                { "Id", 13 },
                { "Date", "27.03.2025" },
                { "TimeFrom", "10:00" },
                { "TimeTo", "11:00" },
                { "Title", "Team Meeting" },
                { "Subtitle", "Weekly sync-up" },
                { "BackgroundColor", "#ff5733" },
                { "Content", "Discuss project progress and upcoming tasks." }
            };


            collection.InsertOne(document);
            Console.WriteLine("Dokument eingeführt");
        }


        // Save a CalendarPost
        public void SavePost(string xamlContent, CalendarPost SelectedPost)
        {
            try
            {
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("Notory");
                var collection = database.GetCollection<BsonDocument>("CalendarPost");


                // Create update definition
                var update = Builders<BsonDocument>.Update
                    .Set("Title", SelectedPost.Title)
                    .Set("Date", SelectedPost.Date.ToString("dd.MM.yyyy"))
                    .Set("Text", xamlContent)
                    .Set("LastModified", DateTime.UtcNow);

                // Execute update
                var result = collection.UpdateOne(
                    Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(SelectedPost.MongoId)),
                    update);

                if (result.ModifiedCount == 0)
                {
                    Console.WriteLine("No documents were modified. Document may not exist.");
                }
                else
                {
                    Console.WriteLine($"Successfully updated document with ID: {SelectedPost.Id}");
                }

                var doc = collection.Find(Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(SelectedPost.MongoId))).FirstOrDefault();
                Console.WriteLine($"Found document: {doc?.ToJson()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to MongoDB: {ex.Message}");
            }
        }

        public async Task<CalendarPost> GetPost(string mongoId)
        {
            if (string.IsNullOrWhiteSpace(mongoId))
            {
                Console.WriteLine("MongoId ist null oder leer!");
                return null;
            }

            try
            {
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("Notory");
                var collection = database.GetCollection<BsonDocument>("CalendarPost");

                ObjectId objectId;
                try
                {
                    objectId = new ObjectId(mongoId);
                }
                catch
                {
                    Console.WriteLine($"Ungültiges ObjectId-Format: {mongoId}");
                    return null;
                }

                var filter = Builders<BsonDocument>.Filter.Eq("_id", objectId);
                var document = await collection.Find(filter).FirstOrDefaultAsync();

                if (document == null)
                {
                    Console.WriteLine($"Dokument mit ID {mongoId} nicht gefunden");
                    return null;
                }

                return new CalendarPost
                {
                    MongoId = document["_id"].ToString(),
                    Id = document.Contains("Id") ? document["Id"].ToInt32() : 0,
                    Title = document.Contains("Title") ? document["Title"].AsString : string.Empty,
                    Date = document.Contains("Date") ? DateTime.Parse(document["Date"].AsString) : DateTime.MinValue,
                    Content = document.Contains("Text") ? document["Text"].AsString : string.Empty
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler in GetPost: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return null;
            }
        }
        public async Task<ObservableCollection<CalendarPost>> LoadEntries()
        {
            try
            {
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("Notory");
                var collection = database.GetCollection<BsonDocument>("CalendarPost");

                var documents = await collection.Find(new BsonDocument()).ToListAsync();

                var entries = new ObservableCollection<CalendarPost>(
                    documents.Select(doc => new CalendarPost
                    {
                        MongoId = doc["_id"].ToString(),
                        Id = doc.Contains("Id") ? doc["Id"].ToInt32() : 0,
                        Title = doc.Contains("Title") ? doc["Title"].AsString : string.Empty,
                        Date = doc.Contains("Date") ? DateTime.Parse(doc["Date"].AsString) : DateTime.MinValue,
                        TimeFrom = doc.Contains("TimeFrom") && !doc["TimeFrom"].IsBsonNull ? doc["TimeFrom"].AsString : "00:00",
                        TimeTo = doc.Contains("TimeTo") && !doc["TimeTo"].IsBsonNull ? doc["TimeTo"].AsString : "00:00",
                        Content = doc.Contains("Text") ? doc["Text"].AsString : string.Empty,
                        BackgroundColor = doc["BackgroundColor"].ToString()
                    })) ;

                Console.WriteLine($"Successfully loaded {entries.Count} entries");

                // Aktualisiere die Property UND gebe die Einträge zurück
                Entries = entries;
                return entries;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading entries: {ex.Message}");

                // Fallback: Leere Collection zurückgeben
                var emptyCollection = new ObservableCollection<CalendarPost>();
                Entries = emptyCollection;
                return emptyCollection;
            }
        }
    }
}
