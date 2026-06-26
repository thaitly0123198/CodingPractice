using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using PracticeProblems.Core.Entities;

namespace PracticeProblems.Data;

// Connection + Configuration — owns MongoClient,
// IMongoDatabase, BSON class maps, index creation,
// collection handles
public class MongoContext
{
    // define gateway to CRUD + queries + indexes on collections 
    public IMongoCollection<Problem> Problems { get; }

    // mapping c# entities class to Problem document
    static MongoContext(){
        BsonClassMap.RegisterClassMap<Problem>(classMap =>
            {
                classMap.AutoMap();
                classMap.MapIdMember(pblm => pblm.Id);
                classMap.MapMember(pblm => pblm.Title);
                classMap.MapMember(pblm => pblm.Description);
                classMap.MapMember(pblm => pblm.Examples);
                classMap.MapMember(pblm => pblm.Difficulty);
                classMap.MapMember(pblm => pblm.Category);
            });
    }

    public MongoContext(MongoDbSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.Database);

        Problems = database.GetCollection<Problem>("problems");
    }

}