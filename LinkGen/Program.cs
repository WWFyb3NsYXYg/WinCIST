using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Enter ID:");
        string ID = Console.ReadLine();

        string response = await MakeGetRequestSchedule(ID);

        var settings = new JsonSerializerSettings
        {
            MissingMemberHandling = MissingMemberHandling.Error,
        };

        // Deserialize CIST API response into timetable object
        Timetable timetable = JsonConvert.DeserializeObject<Timetable>(response, settings);

        Dictionary<int, List<Dictionary<int, string>>> urls = new Dictionary<int, List<Dictionary<int, string>>>();

        foreach (var subject in timetable.Subjects)
        {
            int subject_id = subject.Id;
            string subject_title = subject.Brief;
            var teachers_data = subject.Hours;
            var teacher_set = new HashSet<int>();
            var teacher_urls = new List<Dictionary<int, string>>();

            foreach (var hour in teachers_data)
            {
                foreach (var teacher_id in hour.Teachers)
                {
                    if (!teacher_set.Contains(teacher_id))
                    {
                        List<Teacher> teachers = timetable.Teachers;
                        string teacher_name = teachers.First(t => t.Id == teacher_id).ShortName;

                        Console.WriteLine($"\nСсылки должны быть в формате https://meet.google.com/xxx-xxxx-xxx. \n\nВведите код встречи для {teacher_name} {subject_title}: ");
                        string teacher_url = Console.ReadLine();

                        teacher_set.Add(teacher_id);
                        teacher_urls.Add(new Dictionary<int, string> { { teacher_id, teacher_url } });
                        urls[subject_id] = teacher_urls;
                    }
                }
            }
        }

        // Сохранение в JSON файл
        string json = JsonConvert.SerializeObject(urls, Formatting.Indented);
        File.WriteAllText("output.json", json);
        OpenFileInDefaultTextEditor("output.json");
    }
    static void OpenFileInDefaultTextEditor(string filePath)
    {
        try
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "notepad.exe", // Use the appropriate text editor for your platform
                Arguments = filePath,
                UseShellExecute = true,
                RedirectStandardOutput = false,
                CreateNoWindow = true
            };
            // Используйте Process.Start для открытия файла в текстовом редакторе
            Process.Start(psi);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка при открытии файла: {ex.Message}");
        }
    }
    private static async Task<string> MakeGetRequestSchedule(string ID)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                string url = $"https://cist.nure.ua/ias/app/tt/P_API_EVENTS_GROUP_JSON?p_id_group={ID}&idClient=KNURESked";

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    byte[] contentBytes = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    Encoding encodingWindows1251 = Encoding.GetEncoding(1251);
                    string content = encodingWindows1251.GetString(contentBytes);
                    return content;
                }
                else
                {
                    return $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                return $"Exception: {ex.Message}";
            }
        }
    }
    class Timetable
    {
        [JsonProperty("time-zone")]
        public string TimeZone { get; set; }
        [JsonProperty("events")]
        public List<Event> Events { get; set; }
        [JsonProperty("groups")]
        public List<Group> Groups { get; set; }
        [JsonProperty("teachers")]
        public List<Teacher> Teachers { get; set; }
        [JsonProperty("subjects")]
        public List<Subject> Subjects { get; set; }
        [JsonProperty("types")]
        public List<Type> Types { get; set; }
    }
    public class Group
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
    // Class representing an event in the timetable
    class Event
    {
        [JsonProperty("subject_id")]
        public int SubjectId { get; set; }
        [JsonProperty("start_time")]
        public long StartTime { get; set; }
        [JsonProperty("end_time")]
        public long EndTime { get; set; }
        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("number_pair")]
        public int NumberPair { get; set; }
        [JsonProperty("auditory")]
        public string Auditory { get; set; }
        [JsonProperty("teachers")]
        public List<int> Teachers { get; set; }
        [JsonProperty("groups")]
        public List<int> Groups { get; set; }
    }

    // Class representing information about a teacher
    class Teacher
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("full_name")]
        public string FullName { get; set; }
        [JsonProperty("short_name")]
        public string ShortName { get; set; }
    }
    // Class representing information about a subject
    class Subject
    {
        public int Id { get; set; }
        public string Brief { get; set; }
        public string Title { get; set; }
        public List<Hour> Hours { get; set; }
    }
    // Class representing information about hours for a subject
    class Hour
    {
        public int Type { get; set; }
        public int Val { get; set; }
        public List<int> Teachers { get; set; }
    }
    public class Type
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("short_name")]
        public string ShortName { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("id_base")]
        public int IdBase { get; set; }

        [JsonProperty("type")]
        public string TypeProperty { get; set; }
    }
}