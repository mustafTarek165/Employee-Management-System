using Blazored.LocalStorage;

public class LocalStorageService
{
    private readonly ILocalStorageService localStorageService;
    private const string StorageKey = "authentication-token";

    public LocalStorageService(ILocalStorageService localStorageService)
    {
        this.localStorageService = localStorageService;
    }

    public async Task<string?> GetToken()
    {
        try
        {
            return await localStorageService.GetItemAsStringAsync(StorageKey);
        }
        catch (Exception ex)
        {
            // Handle any exception (e.g., localStorageService is null or not available)
            Console.Error.WriteLine($"Error fetching token from local storage: {ex.Message}");
            return null;
        }
    }

    public async Task SetToken(string item)
    {
        try
        {
            await localStorageService.SetItemAsStringAsync(StorageKey, item);
        }
        catch (Exception ex)
        {
            // Handle any exception (e.g., localStorageService is null or not available)
            Console.Error.WriteLine($"Error setting token in local storage: {ex.Message}");
           
        }
    }

    public async Task RemoveToken()
    {
        try
        {
            await localStorageService.RemoveItemAsync(StorageKey);
        }
        catch (Exception ex)
        {
            // Handle any exception (e.g., localStorageService is null or not available)
            Console.Error.WriteLine($"Error removing token from local storage: {ex.Message}");
        }
    }
}
