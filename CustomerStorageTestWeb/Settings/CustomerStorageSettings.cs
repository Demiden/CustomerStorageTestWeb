namespace CustomerStorageTestWeb.Settings
{
    public class CustomerStorageSettings
    {
        public const string SectionName = "CustomerStorage";
        public StorageTypeEnum StorageType { get; set; }
    }

    public enum StorageTypeEnum
    {
        File,
        Cache
    }
}