using System;
using System.Threading.Tasks;
using LanguageScraper;

public class LanguageScraperService
{
    public async Task RunScraperAsync()
    {
        Console.WriteLine("Language Scraper for GTranslate");
        Console.WriteLine();

        var scrapers = new ILanguageScraper[]
        {
            new GoogleLanguageScraper(),
            new YandexLanguageScraper(),
            new MicrosoftLanguageScraper()
        };

        foreach (var scraper in scrapers)
        {
            Console.WriteLine($"Started displaying missing languages for {scraper.TranslationService}.");
            await scraper.DisplayMissingLanguagesAsync();
            Console.WriteLine($"Stopped displaying missing languages for {scraper.TranslationService}.");
        }
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        var scraperService = new LanguageScraperService();
        await scraperService.RunScraperAsync();

        Console.Write("Press any key to exit...");
        Console.ReadKey(true);
    }
}