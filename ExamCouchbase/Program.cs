using Couchbase;
using Couchbase.Management.Buckets;
using Couchbase.Management.Collections;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamCouchbase
{
    class Program
    {
        //async Task<IBucket> GetIBucket(string bucketName)
        //{

        //}

        static async Task Main()
        {
            try
            {
                string connectionString = "couchbase://192.168.0.21";
                string username = "Administrator";
                string password = "tmxpdj";
                string bucketName = "kr-region1-logs";
                string scopeSpecName = "logs_region1";
                string collectionSpecName = "log_kind_01";

                // 클러스터 연결 (Server = Cluster)                
                var iCluster = await Cluster.ConnectAsync(connectionString, username, password);

                // 버킷 생성 확인 (Database = Bucket) iCluster.BucketAsync()은 Exception이 발생하므로 확인 불가
                var bucketManager = iCluster.Buckets;
                var bucketSettings = bucketManager.GetAllBucketsAsync();
                if (bucketSettings.Result.ContainsKey(bucketName) is false)
                {
                    BucketSettings settings = new()
                    {
                        Name = bucketName,
                        BucketType = BucketType.Couchbase,
                        RamQuotaMB = 512,
                        FlushEnabled = false,
                        NumReplicas = 0,
                        ReplicaIndexes = false,
                        ConflictResolutionType = ConflictResolutionType.SequenceNumber,
                        EvictionPolicy = EvictionPolicyType.ValueOnly,
                        //EjectionMethod = EvictionPolicyType.ValueOnly,
                        MaxTtl = 0,
                        CompressionMode = CompressionMode.Off,
                        DurabilityMinimumLevel = Couchbase.KeyValue.DurabilityLevel.None
                    };

                    await bucketManager.CreateBucketAsync(settings, default);

                    // 생성후 컬렉션을 사용할 경우 Eception발생 Ready 상태 확인 필요 iBucket.Collections 
                    //await Task.Delay(TimeSpan.FromSeconds(7));
                    await iCluster.WaitUntilReadyAsync(TimeSpan.FromSeconds(10));
                }

                // 버킷 연결 (Database = Bucket)
                var iBucket = await iCluster.BucketAsync(bucketName);
                if (iBucket is null)
                {
                    Console.WriteLine($"ERROR iBucket: {bucketName}");
                    return;
                }

                // 스코프 생성 확인 (Schema = Scope) iCollectionMgr.GetScopeAsync() Exception, iBucket.Scope()은 실제 Scope가 없어도 넘어가므로 확인 불가
                var iCollectionMgr = iBucket.Collections;
                var scopeSpecs = await iCollectionMgr.GetAllScopesAsync();

                List<string> scopeSpecNames = new();
                foreach (var scope in scopeSpecs)
                {
                    scopeSpecNames.Add(scope.Name);
                }
                if (scopeSpecNames.Contains(scopeSpecName) is false)
                {
                    await iCollectionMgr.CreateScopeAsync(scopeSpecName);
                }

                var scopeSpec = await iCollectionMgr.GetScopeAsync(scopeSpecName);

                List<string> collectionNames = new();
                foreach (var collectionSpec in scopeSpec.Collections)
                {
                    collectionNames.Add(collectionSpec.Name);
                }

                if (collectionNames.Contains(collectionSpecName) is false)
                {
                    var collectionSpec = new CollectionSpec(scopeSpecName, collectionSpecName);
                    await iCollectionMgr.CreateCollectionAsync(collectionSpec);
                }


                //// get document from collection
                //var strKey = $"airline_10";
                //var getResult = await collection.GetAsync(strKey);
                //Console.WriteLine($"getResult cas: {getResult.Cas}\n{getResult.ContentAs<dynamic>()}");

                // get documents from cluster QueryAsync
                var queryResult = await iCluster.QueryAsync<dynamic>("SELECT t.* FROM `travel-sample` t WHERE t.type=$1 LIMIT 5",
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
                var indexResult = await iCluster.QueryAsync<dynamic>("SELECT name FROM system:indexes WHERE name ='def_primary'and is_primary=true and keyspace_id='travel-sample'").ConfigureAwait(false);

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
