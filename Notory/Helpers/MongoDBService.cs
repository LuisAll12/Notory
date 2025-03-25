using System;
using System.Collections.Generic;
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

        public void SetPost(BsonDocument document, CalendarPost SelectedPost)
        {
            var client = new MongoClient(connectionString);

            var database = client.GetDatabase("Notory");

            var collection = database.GetCollection<BsonDocument>("CalendarPost");

            //document = new BsonDocument
            //{
            //    {"Title",  SelectedPost.Title },
            //    {"Date", "19.06.2020" },
            //    {"Text", "Blablabla bliblibliblibli, hubububububububububububububuububub, linganguliguligu niwapa linganggu linganggu" }
            //};

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
    }
}
