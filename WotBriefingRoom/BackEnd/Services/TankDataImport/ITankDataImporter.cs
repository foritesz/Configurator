namespace BackEnd.Services.TankDataImport
{
    public interface ITankDataImporter
    {
        Task ImportXmlData(string folderPath);
    }
}
