using Microsoft.WindowsAzure.Storage;

var connectionString = "DefaultEndpointsProtocol=https;AccountName=myblobstorageaccount6;AccountKey=UKCLnOz3i8hlhyXnZeQ09ho7GGd3tm48jf1piMI8hAaYHo3iVJQMtPWtDXlMwdtlS2EgBAO91rAW+ASt0Zo2Zw==;EndpointSuffix=core.windows.net";

var shareName = "myfiles";


var storageAccount = CloudStorageAccount.Parse(connectionString);
var fileClient = storageAccount.CreateCloudFileClient();
var share = fileClient.GetShareReference(shareName);
var rootDirectory = share.GetRootDirectoryReference();

Console.WriteLine("Enter the file path of the image to upload:");
var imagePath = Console.ReadLine();

using var fileStream = File.OpenRead(imagePath);
var fileName = Path.GetFileName(imagePath);

var file = rootDirectory.GetFileReference(fileName);
await file.UploadFromStreamAsync(fileStream);

Console.WriteLine($"Image '{imagePath}' uploaded successfully to the File Share.");

Console.WriteLine("Enter the name of the image to download from the File Share:");
var imageNameToDownload = Console.ReadLine();

var downloadFile = rootDirectory.GetFileReference(imageNameToDownload);
Console.WriteLine("Enter the destination file path to save the downloaded image:");
var destinationPath = Console.ReadLine();

using var downloadFileStream = File.OpenWrite(destinationPath);
await downloadFile.DownloadToStreamAsync(downloadFileStream);

Console.WriteLine($"Image '{imageNameToDownload}' downloaded successfully from the File Share and saved to '{destinationPath}'.");
