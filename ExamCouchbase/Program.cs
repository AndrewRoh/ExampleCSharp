using Couchbase;
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
                var cluster = await Cluster.ConnectAsync("couchbase://192.168.0.94", "user1", "password");

                // get a bucket reference
                var bucket = await cluster.BucketAsync("travel-sample");

                //var scope = bucket.Scope("myscope");
                //var collection = scope.Collection("mycollection");

                // get a collection reference
                var collection = bucket.DefaultCollection();

                // get document from collection
                var strKey = $"airline_10";
                var getResult = await collection.GetAsync(strKey);
                Console.WriteLine($"getResult cas: {getResult.Cas}\n{getResult.ContentAs<dynamic>()}");

                // get documents from cluster
                var queryResult = await cluster.QueryAsync<dynamic>("SELECT t.* FROM `travel-sample` t WHERE t.type=$1 LIMIT 500",
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
