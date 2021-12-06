using Advent.Runner.File;
using Advent.Utilities.Extensions;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ReverseMarkdown;
using System;
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
        public async Task<ExerciseMetadata> RetrieveExerciseMetadata(int year, int day)
        {
            ExerciseMetadata fetched = null;

            var client = HttpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{this.SiteOptions.Value.SiteUrl}/{year}/day/{day}");

            request.Headers.Add("cookie", $"session={this.SiteOptions.Value.CookieSessionToken}");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (!content.Equals(ExerciseNotAvailable))
                {
                    string title = null;
                    string firstAnswer = null;
                    string firstExerciseHtml = null;
                    string secondAnswer = null;
                    string secondExerciseHtml = null;

                    var html = new HtmlDocument();
                    html.LoadHtml(content);
                    var docNode = html.DocumentNode;

                    var puzzleTitle = docNode.QuerySelector(".day-desc h2");
                    if (puzzleTitle != null)
                    {
                        title = puzzleTitle.InnerText.Replace("---", "").Split(": ", 1, System.StringSplitOptions.RemoveEmptyEntries).LastOrDefault()?.Trim();
                    }

                    var potentialAnswers = docNode.QuerySelectorAll(".day-desc + p");
                    int answerCount = 0;
                    foreach (var potential in potentialAnswers)
                    {
                        if (potential != null && potential.InnerText.StartsWith("Your puzzle answer was"))
                        {
                            // found an answer block
                            if (answerCount == 0)
                            {
                                // first answer.
                                firstAnswer = potential.QuerySelector("code").InnerText;
                            }
                            else
                            {
                                // second answer.
                                secondAnswer = potential.QuerySelector("code").InnerText;
                            }

                            answerCount++;
                        }
                    }

                    var puzzleDescriptions = docNode.QuerySelectorAll(".day-desc");
                    foreach (var puzzleDescription in puzzleDescriptions)
                    {
                        bool isPart2 = false;
                        var h2 = puzzleDescription.QuerySelector("h2");

                        if (h2 != null && h2.InnerText.Contains("Part Two", System.StringComparison.InvariantCultureIgnoreCase))
                        {
                            isPart2 = true;
                        }

                        if (isPart2)
                        {
                            secondExerciseHtml = puzzleDescription.InnerHtml;
                        }
                        else
                        {
                            firstExerciseHtml = puzzleDescription.InnerHtml;
                        }
                    }

                    fetched = new ExerciseMetadata
                    {
                        Title = title,
                        Year = year,
                        Day = day,
                        FirstExerciseHtml = firstExerciseHtml,
                        FirstAnswer = firstAnswer,
                        SecondExerciseHtml = secondExerciseHtml,
                        SecondAnswer = secondAnswer,
                    };
                }
            }

            return fetched;
        }

        // GET [year]
        public async Task<TimeSpan> RetrieveTimeUntilNextUnlock(int year)
        {
            // $("#calendar-countdown")
            // var server_eta = 22832;
            var now = DateTime.Now;
            return new DateTime(now.Year, now.Month, now.Day + 1, 0, 0, 0) - DateTime.Now;
        }

        // GET [site]/year/day/[day]/input
        public async Task<bool> RetrieveInput(int year, int day, bool overwrite = false)
        {
            bool fetched = false;

            var outputPath = System.IO.Path.Combine(this.AppOptions.Value.InputDataDirectory.Interpolate(("year", year), ("day", day.ToString().PadLeft(2, '0'))));

            if (!System.IO.Directory.Exists(outputPath))
            {
                System.IO.Directory.CreateDirectory(outputPath);
            }

            var fileName = $"{outputPath}\\Part1.txt";

            if (!overwrite)
            {
                if (System.IO.File.Exists(fileName))
                {
                    return false;
                }
            }

            var client = HttpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{this.SiteOptions.Value.SiteUrl}/{year}/day/{day}/input");

            request.Headers.Add("cookie", $"session={this.SiteOptions.Value.CookieSessionToken}");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                await System.IO.File.WriteAllTextAsync(fileName, content);

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

                        var outputPath = System.IO.Path.Combine(this.AppOptions.Value.MarkdownDirectory.Interpolate(("year", year), ("day", day)), year.ToString());

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
