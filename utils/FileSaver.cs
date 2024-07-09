public class FileSaver{
public static async Task<string> SaveFileAsync(IFormFile file)
{
    var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

    if (!Directory.Exists(uploadsFolderPath))
    {
        Directory.CreateDirectory(uploadsFolderPath);
    }

    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
    var filePath = Path.Combine(uploadsFolderPath, uniqueFileName);

    using (var fileStream = new FileStream(filePath, FileMode.Create))
    {
        await file.CopyToAsync(fileStream);
    }

    return Path.Combine("uploads", uniqueFileName);
}

}