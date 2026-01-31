namespace PharouhsTourism.FileHandler
{
    public static class FileHandler
    {
        public static async Task<string> Store(IFormFile file, string folderPath)
        {
            if(!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fileName = Guid.NewGuid().ToString() + "_" + file.Name;

            string filePath = Path.Combine(folderPath, fileName);

            using(var fileStrem = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStrem);
            }

            string fileURL = Path.Combine(folderPath, fileName);
            return filePath;
        }
    }
}
