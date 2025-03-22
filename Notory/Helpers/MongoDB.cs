using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Notory.Helpers
{
    internal class MongoDB
    {
        public void SetPost()
        {
            string connectionString = "mongodb://127.0.0.1:27017";
            var client = new MongoClient(connectionString);

            var database = client.GetDatabase("Notory");

            var collection = database.GetCollection<BsonDocument>("CalendarPost");

            var document = new BsonDocument
            {
                {"Title", "Exam.net downloaded" },
                {"Date", "19.06.2020" },
                {"Text", "Blablabla bliblibliblibli, hubububububububububububububuububub, linganguliguligu niwapa linganggu linganggu" }
            };

            collection.InsertOne(document);
            Console.WriteLine("Dokument eingeführt");
        }
    }
}
