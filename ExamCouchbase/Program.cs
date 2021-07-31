using Couchbase;
using Couchbase.Search.Queries.Compound;
using Couchbase.Search.Queries.Range;
using Couchbase.Search.Queries.Simple;
using System;
using System.Threading.Tasks;

namespace ExamCouchbase
{
    class Program
    {
        static async Task Main()
        {
            try
            {
                var cluster = await Cluster.ConnectAsync("couchbase://192.168.0.11", "user1", "password");

                // get a bucket reference
                var bucket = await cluster.BucketAsync("travel-sample");

                var scope = bucket.Scope("inventory");
                var collection = scope.Collection("airline");

                // get a collection reference
                // var collection = bucket.DefaultCollection();

                // get document from collection
                var strKey = $"airline_10";
                var getResult = await collection.GetAsync(strKey);
                Console.WriteLine($"getResult cas: {getResult.Cas}\n{getResult.ContentAs<dynamic>()}");

                // get documents from cluster QueryAsync
                var queryResult = await cluster.QueryAsync<dynamic>("SELECT t.* FROM `travel-sample` t WHERE t.type=$1 LIMIT 5",
                options =>
                {
                    options.Parameter("landmark");
                    options.FlexIndex(true);
                    options.ScanCap(100);
                }).ConfigureAwait(false);


                int resultCount = 0;
                await foreach (var row in queryResult)
                {
                    Console.WriteLine(row);
                    resultCount++;
                }
                Console.WriteLine($"Complete! resultCount: {resultCount}");

                // check exist primary index
                var indexResult = await cluster.QueryAsync<dynamic>("SELECT name FROM system:indexes WHERE name ='def_primary'and is_primary=true and keyspace_id='travel-sample'").ConfigureAwait(false);
                
                await foreach (var row in indexResult)
                {
                    Console.WriteLine(row);                    
                }
            }
            catch (AuthenticationFailureException ex)
            {
                Console.WriteLine($"AuthenticationFailureException Reason: {ex}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Reason: {ex}");
                //throw;
            }

        }
    }
}
