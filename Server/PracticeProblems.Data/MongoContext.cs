using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
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

    // mapping c# entities class to mongo document
    static MongoContext(){
        BsonClassMap.RegisterClassMap<Problem>(classMap =>
            {
                classMap.MapIdMember(pblm => pblm.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance);
                classMap.MapMember(pblm => pblm.Title);
                classMap.MapMember(pblm => pblm.LongDescription);
                classMap.MapMember(pblm => pblm.ShortDescription);
                classMap.MapMember(pblm => pblm.Difficulty);
            });
    }

    public MongoContext(MongoDbSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.Database);

        Problems = database.GetCollection<Problem>("problems");
    }

}