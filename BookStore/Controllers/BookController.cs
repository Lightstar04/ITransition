using Bogus;
using BookStoreManager.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;

namespace BookStoreManager.Controllers;

[ApiController]
public class BookController : Controller
{
    [HttpGet("/Book/Index")]
    public IActionResult Index()
    {
        return View("Index");
    }

    [HttpGet]
    [Route("/api/book")]
    public IActionResult GetBooks(
        [FromQuery] int seed,
        [FromQuery] int page = 1,
        [FromQuery] string language = "en",
        [FromQuery] float avgLikes = 3.0f,
        [FromQuery] float avgReviews = 1.0f)
    {
        int total = page == 1 ? 20 : 10;
        int combinedSeed = HashSeed(seed, page, language, avgLikes, avgReviews);
        var faker = new Faker(locale: language);
        faker.Random = new Randomizer(combinedSeed);

        var books = GenerateBooks(total, page, faker, avgLikes, avgReviews);

        return Ok(books);
    }

    private int HashSeed(int seed, int page, string lang, float avgLikes, float avgReviews)
    {
        string input = $"{seed}-{page}-{lang}-{avgLikes}-{avgReviews}";
        using var sha = SHA256.Create();
        var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
        return BitConverter.ToInt32(hash, 0);
    }

    private List<Book> GenerateBooks(
        int total, int page,
        Faker faker, float avgLikes,
        float avgReviews)
    {
        var books = Enumerable.Range(1, total).Select(index =>
        {
            var book = new Book
            {
                Index = (page - 1) * 10 + index,
                Isbn = faker.Random.Replace("###-#-###-#####"),
                Title = faker.Lorem.Sentence(3, 3).Titleize(),
                Author = string.Join(",", faker.Make(faker.Random.Int(1, 3), () => faker.Name.FullName())),
                Publisher = faker.Company.CompanyName(),
                Likes = GenerateCount(avgLikes, faker),
                Reviews = GenerateReviews(avgReviews, faker)
            };
            return book;
        }).ToList();

        return books;
    }

    private int GenerateCount(double avg, Faker faker)
    {
        if (avg != 0)
        {
            if (avg < 1)
                return faker.Random.Bool((float)avg) ? 1 : 0;
            return (int)Math.Floor(avg);
        }

        return 0;
    }

    private List<Review> GenerateReviews(double avgReviews, Faker faker)
    {
        var reviews = new List<Review>();

        if (avgReviews == 0) return reviews;

        int count = GenerateCount(avgReviews, faker);

        for (int i = 0; i < count; i++)
        {
            reviews.Add(new Review
            {
                Author = faker.Name.FullName(),
                Text = faker.Lorem.Sentences()
            });
        }

        return reviews;
    }
}