using Advent.Runner.File;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ReverseMarkdown;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Advent.Runner.Web
{
    public class ExerciseService
    {
        private const string ExerciseNotAvailable = "Please don't repeatedly request this endpoint before it unlocks! The calendar countdown is synchronized with the server time; the link will be enabled on the calendar the instant this puzzle becomes available.\n";

        public ExerciseService(
                    ILogger<ExerciseService> logger,
                    IOptions<ApplicationOptions> appOptions,
                    IOptions<SiteOptions> siteOptions,
                    IHttpClientFactory httpClientFactory)
        {
            Logger = logger;
            AppOptions = appOptions;
            SiteOptions = siteOptions;
            HttpClientFactory = httpClientFactory;
        }

        public ILogger<ExerciseService> Logger { get; }
        public IOptions<ApplicationOptions> AppOptions { get; }
        public IOptions<SiteOptions> SiteOptions { get; }

        public IHttpClientFactory HttpClientFactory { get; }

        // GET [site]/year/day/[day]
        public async Task<bool> Fetch(int year, int day, int part = 1)
        {
            bool fetched = false;

            var client = HttpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{this.SiteOptions.Value.SiteUrl}/{year}/day/{day}");

            request.Headers.Add("cookie", $"session={this.SiteOptions.Value.CookieSessionToken}");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (!content.Equals(ExerciseNotAvailable))
                {
                    var html = new HtmlDocument();
                    html.LoadHtml(content);
                    var docNode = html.DocumentNode;
                    var config = new ReverseMarkdown.Config
                    {
                        // Include the unknown tag completely in the result (default as well)
                        UnknownTags = Config.UnknownTagsOption.Bypass,
                        // generate GitHub flavoured markdown, supported for BR, PRE and table tags
                        GithubFlavored = true,
                        // will ignore all comments
                        RemoveComments = true,
                        // remove markdown output for links where appropriate
                        SmartHrefHandling = true
                    };
                    var converter = new ReverseMarkdown.Converter(config);

                    var puzzleDescription = docNode.QuerySelector("main");

                    if (puzzleDescription != null)
                    {
                        var convert = converter.Convert(puzzleDescription.InnerHtml);

                        var outputPath = System.IO.Path.Combine(this.AppOptions.Value.MarkdownDirectory, year.ToString());

                        if (!System.IO.Directory.Exists(outputPath))
                        {
                            System.IO.Directory.CreateDirectory(outputPath);
                        }

                        await System.IO.File.WriteAllTextAsync($"{outputPath}\\Day_{day.ToString().PadLeft(2, '0')}.md", convert);

                        fetched = true;
                    }
                }
            }

            return fetched;
        }

        public async Task<ScriptModel> RetrieveExercise(int year, int day)
        {
            ScriptModel fetched = null;

            var client = HttpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{this.SiteOptions.Value.SiteUrl}/{year}/day/{day}");

            request.Headers.Add("cookie", $"session={this.SiteOptions.Value.CookieSessionToken}");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (!content.Equals(ExerciseNotAvailable))
                {
                    var html = new HtmlDocument();
                    html.LoadHtml(content);
                    var docNode = html.DocumentNode;

                    var puzzleTitle = docNode.QuerySelector(".day-desc h2");
                    if (puzzleTitle != null)
                    {

                        fetched = new ScriptModel
                        {
                            Title = puzzleTitle.InnerText.Replace("---", "").Split(": ", 1, System.StringSplitOptions.RemoveEmptyEntries).LastOrDefault()?.Trim(),
                            Year = year,
                            Day = day,
                        };
                    }
                }
            }

            return fetched;
        }

        // GET [site]/year/day/[day]/input
        public async Task<bool> RetrieveInput(int year, int day, int part = 1)
        {
            bool fetched = false;

            var client = HttpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{this.SiteOptions.Value.SiteUrl}/{year}/day/{day}/input");

            request.Headers.Add("cookie", $"session={this.SiteOptions.Value.CookieSessionToken}");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var outputPath = System.IO.Path.Combine(
                                string.Format(
                                        this.AppOptions.Value.InputDataDirectory,
                                        year,
                                        day.ToString().PadLeft(2, '0')));

                if (!System.IO.Directory.Exists(outputPath))
                {
                    System.IO.Directory.CreateDirectory(outputPath);
                }

                await System.IO.File.WriteAllTextAsync($"{outputPath}\\Part{part}.txt", content);

                fetched = true;
            }

            return fetched;
        }

        // POST [site]/[year]/day/[day]/answer
        public async Task<bool> SubmitAnswer(int year, int day, int part, object answer)
        {
            // answer will always accept the input for the current part.

            bool fetched = false;

            var client = HttpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Post, $"{this.SiteOptions.Value.SiteUrl}/{year}/day/{day}/answer");

            request.Headers.Add("cookie", $"session={this.SiteOptions.Value.CookieSessionToken}");

            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "level", part.ToString()},
                { "answer", answer.ToString()}
            });

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (!content.Equals(ExerciseNotAvailable))
                {
                    var html = new HtmlDocument();
                    html.LoadHtml(content);
                    var docNode = html.DocumentNode;

                    var puzzleDescription = docNode.QuerySelector("main");

                    if (puzzleDescription != null)
                    {

                        var config = new ReverseMarkdown.Config
                        {
                            // Include the unknown tag completely in the result (default as well)
                            UnknownTags = Config.UnknownTagsOption.Bypass,
                            // generate GitHub flavoured markdown, supported for BR, PRE and table tags
                            GithubFlavored = true,
                            // will ignore all comments
                            RemoveComments = true,
                            // remove markdown output for links where appropriate
                            SmartHrefHandling = true
                        };

                        var converter = new ReverseMarkdown.Converter(config);

                        var convert = converter.Convert(puzzleDescription.InnerHtml);

                        var outputPath = System.IO.Path.Combine(this.AppOptions.Value.MarkdownDirectory, year.ToString());

                        if (!System.IO.Directory.Exists(outputPath))
                        {
                            System.IO.Directory.CreateDirectory(outputPath);
                        }

                        await System.IO.File.WriteAllTextAsync($"{outputPath}\\Day_{day.ToString().PadLeft(2, '0')}_AnswerResponse_Part{part}.md", convert);

                        fetched = true;
                    }
                }
            }

            return fetched;
        }
    }
}
