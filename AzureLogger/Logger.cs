using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Threading.Tasks;
namespace AzureLogger
{

    public class Logger : ICosmosLogger
    {
        private readonly LoggerPOCO _configuration;
        CloudTable _table;
        string tableName = "demo" + Guid.NewGuid().ToString().Substring(0, 5);



        public Logger(LoggerPOCO configuration)
        {
            _configuration = configuration;
            this._table = CreateTableAsync(tableName).Result;
        }

        #region Service Methods
        public async Task<CloudTable> CreateTableAsync(string tableName)
        {

            CloudStorageAccount storageAccount = CreateStorageAccountFromConnectionString(_configuration.StorageConnectionString);

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            Console.WriteLine("Create a Table for the demo");

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference(tableName);
            try
            {
                if (await table.CreateIfNotExistsAsync())
                {
                    Console.WriteLine("Created Table named: {0}", tableName);
                }
                else
                {
                    Console.WriteLine("Table {0} already exists", tableName);
                }
            }
            catch (StorageException)
            {
                Console.WriteLine("If you are running with the default configuration please make sure you have started the storage emulator. Press the Windows key and type Azure Storage to select and run it from the list of applications - then restart the sample.");
                Console.ReadLine();
                throw;
            }
            return table;
        }

        public static CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
        {

            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the application.");
                throw;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
                Console.ReadLine();
                throw;
            }

            return storageAccount;
        }

        #endregion

        #region Table Storage methods
        public async void Insert(string message)
        {
            //CustomerEntity customer = new CustomerEntity("10", LoggingEvents.GetItem.ToString())
            CustomerEntity customer = new CustomerEntity("10", message)
            {
                //Email = "Walter@contoso.com",
                //PhoneNumber = "425-555-0101"
            };

            // Demonstrate how to insert the entity
            Console.WriteLine("Insert an Entity.");
            customer = await InsertOrMergeEntityAsync(_table, customer);

            // Demonstrate how to Update the entity by changing the phone number
            Console.WriteLine("Update an existing Entity using the InsertOrMerge Upsert Operation.");
            customer.PhoneNumber = "425-555-0105";
            await InsertOrMergeEntityAsync(_table, customer);
            Console.WriteLine();

            // Demonstrate how to Read the updated entity using a point query 
            Console.WriteLine("Reading the updated Entity.");
            customer = await RetrieveEntityUsingPointQueryAsync(_table, "Harp", "Walter");
            Console.WriteLine();

            Console.WriteLine("query:");
            try
            {
                // Create the range query using the fluid API 
                TableQuery<CustomerEntity> rangeQuery = new TableQuery<CustomerEntity>().Where(
                            TableQuery.GenerateFilterCondition("PhoneNumber", QueryComparisons.Equal, "425-555-0105")
                           );

                var res = _table.ExecuteQuerySegmentedAsync(rangeQuery, new TableContinuationToken());

            }
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }



            // Demonstrate how to Delete an entity
            //Console.WriteLine("Delete the entity. ");
            //await DeleteEntityAsync(table, customer);
            Console.WriteLine();
        }
        #endregion

        private static async Task<CustomerEntity> InsertOrMergeEntityAsync(CloudTable table, CustomerEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            try
            {
                // Create the InsertOrReplace table operation
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity);

                // Execute the operation.
                TableResult result = await table.ExecuteAsync(insertOrMergeOperation);
                CustomerEntity insertedCustomer = result.Result as CustomerEntity;

                return insertedCustomer;
            }
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
        }
        private static async Task<CustomerEntity> RetrieveEntityUsingPointQueryAsync(CloudTable table, string partitionKey, string rowKey)
        {

            try
            {
                TableOperation retrieveOperation = TableOperation.Retrieve<CustomerEntity>(partitionKey, rowKey);
                TableResult result = await table.ExecuteAsync(retrieveOperation);
                CustomerEntity customer = result.Result as CustomerEntity;
                if (customer != null)
                {
                    Console.WriteLine("\t{0}\t{1}\t{2}\t{3}", customer.PartitionKey, customer.RowKey, customer.Email, customer.PhoneNumber);
                }

                return customer;
            }
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
        }
    }
}




