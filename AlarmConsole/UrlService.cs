using System.Text.Json;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace AlarmConsole
{
    class UrlService
    {
        private static string JsonFilePath
        { get { return Path.Combine(AppContext.BaseDirectory, @"urls.json"); } set { } }

        private Dictionary<string, string> videos
            = JsonSerializer.Deserialize<Dictionary<string, string>>(System.IO.File.ReadAllText(JsonFilePath));


        public string GetURl()
        {
            return videos.ElementAt(Random.Shared.Next(videos.Count)).Value;
        }
        public string GetURl(int id)
        {
            return videos.ElementAt(id-1).Value;
        }
        public Dictionary<string, string> GetAllURLs()
        {
            return videos;
        }
        public async Task<Dictionary<string, string>> AddNewURL(string name, string url)
        {
            videos.Add(name, url);
            await UpdateUrlJsonFile();

            return videos;
        }

        public async Task<Dictionary<string, string>> DelOneURL(string name, string url)
        {
            videos.Remove(name);
            await UpdateUrlJsonFile();
            return videos;
        }

        private async Task UpdateUrlJsonFile()
        {
            if (File.Exists(@"urls.json"))
            {
                File.Delete(@"urls.json");
            }
            await File.WriteAllLinesAsync("urls.json", new List<string> { JsonSerializer.Serialize(videos) });
        }



    }
}
