
using Azure.Data.Tables;

var connectionString = "DefaultEndpointsProtocol=https;AccountName=myblobstorageaccount6;AccountKey=UKCLnOz3i8hlhyXnZeQ09ho7GGd3tm48jf1piMI8hAaYHo3iVJQMtPWtDXlMwdtlS2EgBAO91rAW+ASt0Zo2Zw==;EndpointSuffix=core.windows.net";


string tableName = "Players";

var tableClient = new TableClient(connectionString, tableName);
await tableClient.CreateIfNotExistsAsync();

 
//var entity = new TableEntity("Players", "player6")
            //{
            // { "firstname", "john" },
              // { "country", "brasil" },
               //{ "phonenumber", "123456" }, 
     //};
//await tableClient.AddEntityAsync(entity);
//Console.WriteLine("Entity added to the table.");



var entity = new TableEntity("Players", "player6");
string partitionKeyFilter = $"PartitionKey eq '{entity.PartitionKey}'";
await foreach (var e in tableClient.QueryAsync<TableEntity>(partitionKeyFilter))
{
  Console.WriteLine($"" +
       $"PartitionKey: {e.PartitionKey}, " +
       $"RowKey: {e.RowKey}, " +
       $"Property1: {e.GetString("country")}, " +
        $"Property2: {e.GetString("firstname")}, " +
       $"Property3: {e.GetString("lastname")}, " +
      $"Property4: {e.GetString("phonenumber")}, +" +
        $"Property5: {e.GetDateTime("databirth")}");
}

// updating 
//var entity = new TableEntity("volleyball", "player6");
//entity["country"] = "NewValue1";
//await tableClient.UpdateEntityAsync(entity, ETag.All);
//Console.WriteLine("Entity updated in the table.");

// deleting 
//var entity = new TableEntity("volleyball", "player6");
//await tableClient.DeleteEntityAsync(entity.PartitionKey, entity.RowKey);
//