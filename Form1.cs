using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Globalization;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;
using System.Security.Policy;
using System.Xml;



namespace CIST_v1
{


    public partial class Form1 : Form
    {
        string ENDPOINT = "https://cist.nure.ua/ias/app/tt";
        private List<GroupInfo> groupInfos; // Список для хранения информации о группах

        // =============================================================================================
        //  ===== DEBUG MODE == DEBUG MODE == DEBUG MODE == DEBUG MODE == DEBUG MODE == DEBUG MODE =====
        //  ============================================================================================

                                       bool isDebug = false;       // Подмена цистовского адреса на свой

        // =============================================================================================
        //  ===== DEBUG MODE == DEBUG MODE == DEBUG MODE == DEBUG MODE == DEBUG MODE == DEBUG MODE =====
        //  ============================================================================================

        public Form1()
        {
            InitializeComponent();
            LoadGroups();
        }
        private async void LoadGroups()
        {


                // Call the asynchronous method to make the GET request
                string response = await MakeGetRequest();
                //File.WriteAllText("ApiResponse_step0.json", response);
                //string bodyContent = ExtractBodyContent(response);
                /*MessageBox.Show(bodyContent);
                File.WriteAllText("ApiResponse_step1.json", bodyContent);
                //MessageBox.Show("ok");
                */
                // Теперь парсим JSON
                UniversityResponse universityResponse = JsonConvert.DeserializeObject<UniversityResponse>(response);

                // Используем LINQ для получения списка информации о группах

                if (universityResponse != null)
                {
                    var groupInfos = universityResponse.University.Faculties
                        .SelectMany(faculty => faculty.Directions
                            .SelectMany(direction => direction.Specialities
                                .SelectMany(speciality => speciality.Groups
                                    .Select(group => new GroupInfo($"{group.Name} ({group.Id})", group.Id))
                                )
                            )
                        )
                        .ToList();

                    groupNameBox.DropDownStyle = ComboBoxStyle.DropDown; // Разрешаем ввод текста
                    groupNameBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend; // Подсказки при вводе
                    groupNameBox.AutoCompleteSource = AutoCompleteSource.ListItems;

                groupNameBox.DataSource = groupInfos;

                // Try to read the selected group's information from the config file
                if (TryReadSelectedGroup(out GroupInfo savedGroup))
                {
                    // Find and select the saved group in the ComboBox
                    int index = groupInfos.FindIndex(group => group.Id == savedGroup.Id);
                    if (index >= 0)
                    {
                        groupNameBox.SelectedIndex = index;
                    }
                }
                groupNameBox.DisplayMember = "Name"; // Отображаемое значение
                groupNameBox.ValueMember = "Id";     // Значение, которое будет выбрано при выборе элемента
                }
                else
                {
                    // Handle the case where universityResponse is null
                    MessageBox.Show("universityResponse is null. Unable to process data.");
                }
            
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string text = "";
            int unixTimestamp_to = 0, unixTimestamp_from = 0;
            if (time_from_pick.Enabled)
            {
                DateTime selectedDateFrom = time_from_pick.Value.Date.AddDays(-1); // Устанавливаем время в 00:00:00

                // Переводим дату в формат Unix Timestamp
                unixTimestamp_from += (int)(selectedDateFrom.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                text += "From: " + unixTimestamp_from.ToString();
            }

            if (time_to_pick.Enabled)
            {
                DateTime selectedDateTo = time_to_pick.Value.Date.AddDays(1).AddSeconds(-10810); // Устанавливаем время в 23:59:59

                // Переводим дату в формат Unix Timestamp
                unixTimestamp_to += (int)(selectedDateTo.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                text += "\nTo: " + unixTimestamp_to.ToString() + "\n";
            }


            string selectedText = groupNameBox.Text;
            int selectedId = 0;

            // Получить выбранное значение (Id)
            if (groupNameBox.SelectedItem is GroupInfo selectedGroup)
            {
                selectedId = selectedGroup.Id;
                text += "Group ID: " + selectedId;
                SaveSelectedGroup(selectedGroup);
            }

            // Используем unixTimestamp.ToString()
            //MessageBox.Show(text);
            //File.WriteAllText("params.txt", text);
            string response = await MakeGetRequestSchedule(selectedId, unixTimestamp_from, unixTimestamp_to);

            //string bodyContent = ExtractBodyContent(response);
            txtbox.Text = FormatCN(response);

        }
        /*
        private string ExtractBodyContent(string html)
        {
            int bodyStartIndex = html.IndexOf("<body>", StringComparison.OrdinalIgnoreCase);
            int bodyEndIndex = html.IndexOf("</body>", StringComparison.OrdinalIgnoreCase);

            if (bodyStartIndex != -1 && bodyEndIndex != -1)
            {
                int contentStartIndex = bodyStartIndex + "<body>".Length;
                int contentLength = bodyEndIndex - contentStartIndex;

                return html.Substring(contentStartIndex, contentLength);
            }

            return string.Empty;
        }*/
        private async Task<string> MakeGetRequest()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = "";
                    if (isDebug)
                    { 
                        url = "https://demouserin.github.io/"; //debug url
                                                              
                    }
                    else 
                    { 
                        url = $"{ENDPOINT}/P_API_GROUP_JSON"; 
                    }
                    

                    // Send GET request

                    HttpResponseMessage response = await client.GetAsync(url);

                    // Check if the request was successful (status code 200-299)

                    if (response.IsSuccessStatusCode)
                    {

                        byte[] contentBytes = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                        // Determine the encoding based on the Content-Type header
                        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                        // Теперь вы можете использовать Windows-1251
                        Encoding encodingWindows1251 = Encoding.GetEncoding(1251);

                        // Convert the byte array to a string using the determined encoding
                        string content = encodingWindows1251.GetString(contentBytes);

                        return content;
                    }
                    else
                    {
                        MessageBox.Show($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                        return $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Exception: {ex.Message}");
                    return $"Exception: {ex.Message}";
                }
            }
        }
        private bool TryReadSelectedGroup(out GroupInfo savedGroup)
        {
            string filePath = "configCist.ini";

            // Check if the file exists
            if (File.Exists(filePath))
            {
                try
                {
                    // Read the saved group information from the file
                    string[] lines = File.ReadAllLines(filePath);
                    if (lines.Length == 2 && int.TryParse(lines[1], out int savedId))
                    {
                        savedGroup = new GroupInfo(lines[0], savedId);
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    // Handle file read error if necessary
                    MessageBox.Show("Error reading config file: " + ex.Message);
                }
            }

            savedGroup = null;
            return false;
        }

        private void SaveSelectedGroup(GroupInfo selectedGroup)
        {
            string filePath = "configCist.ini";

            try
            {
                // Save the selected group's information to the config file
                File.WriteAllLines(filePath, new[] { selectedGroup.Name, selectedGroup.Id.ToString() });
            }
            catch (Exception ex)
            {
                // Handle file write error if necessary
                MessageBox.Show("Error writing to config file: " + ex.Message);
            }
        }
        private async Task<string> MakeGetRequestSchedule(int ID, int time_from, int time_to)
        {
            string t_from, t_to;

            if (time_from != 0)
            {
                t_from = $"&time_from={time_from}";
            }
            else
            {
                t_from = "";
            }

            if (time_to != 0)
            {
                t_to = $"&time_to={time_to}";
            }
            else
            {
                t_to = "";
            }

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = "";
                    if (isDebug)
                    {
                        url = "https://demouserin.github.io/shedule/"; //debug url

                    }
                    else
                    {
                        url = $"{ENDPOINT}/P_API_EVENTS_GROUP_JSON?p_id_group={ID}{t_from}{t_to}&idClient=KNURESked";
                    }
                     
                    //File.WriteAllText("ApiURL.txt", url);
                    //MessageBox.Show(url, "URL Адресс запроса", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    // Send GET request
                    HttpResponseMessage response = await client.GetAsync(url);

                    // Check if the request was successful (status code 200-299)
                    if (response.IsSuccessStatusCode)
                    {

                        byte[] contentBytes = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                        // Determine the encoding based on the Content-Type header
                        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                        // Теперь вы можете использовать Windows-1251
                        Encoding encodingWindows1251 = Encoding.GetEncoding(1251);

                        // Convert the byte array to a string using the determined encoding
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
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

            time_to_pick.Enabled = time_to_check.Checked;

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Если CheckBox выбран, активировать DateTimePicker
            // В противном случае отключить DateTimePicker
            time_from_pick.Enabled = time_from_check.Checked;
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        private string FormatCN(string response)
        {
            //string schedule = ExtractBodyContent(response);
            //File.WriteAllText("responseTimetable.txt", response.ToString());
            try
            {
                var settings = new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error, // This will throw an error if a property is not found during deserialization
                };

                var timetable = Newtonsoft.Json.JsonConvert.DeserializeObject<Timetable>(response, settings);
                //MessageBox.Show($"Deserialized Timetable: {timetable}");
                //File.WriteAllText("DeserializedTimetable.txt", timetable.ToString());
                string eventInfo = "";

                // Display events
                foreach (var ev in timetable.Events)
                {
                    //MessageBox.Show($"Event ID: {ev.SubjectId}"); // Debugging line

                    var subject = timetable.Subjects.FirstOrDefault(s => s.Id == ev.SubjectId);
                    //MessageBox.Show($"Found Subject: {Newtonsoft.Json.JsonConvert.SerializeObject(subject)}"); // Debugging line

                    var eventType = timetable.Types.FirstOrDefault(t => t.Id == ev.Type)?.ShortName ?? "Unknown";

                    if (subject != null)
                    {
                        string subjectTitle = subject.Title;

                        var teachers = ev.Teachers.Select(teacherId => timetable.Teachers.FirstOrDefault(t => t.Id == teacherId)).Where(teacher => teacher != null);
                        List<string> teacherNames = teachers.Select(teacher => teacher.ShortName).ToList();
                        string Date = DateTimeOffset.FromUnixTimeSeconds(ev.StartTime).ToLocalTime().ToString("dd.MM.yyyy");
                        string startTime = DateTimeOffset.FromUnixTimeSeconds(ev.StartTime).ToLocalTime().ToString("HH:mm");
                        string endTime = DateTimeOffset.FromUnixTimeSeconds(ev.EndTime).ToLocalTime().ToString("HH:mm");

                        eventInfo += $"💼 {subjectTitle} ({eventType})\n📅 Дата: {Date}\n🕖 Время: {startTime} - {endTime}\n👩‍🏫 Преподаватель: {string.Join(", ", teacherNames)}\n\n";
                    }
                    else
                    {
                        MessageBox.Show($"Warning: Subject not found for event with subject_id {ev.SubjectId}");
                    }
                }

                return eventInfo;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Нет встреч в данном промежутке"); 
                //MessageBox.Show($Error during deserialization: {ex.Message}");
                return "Нет встреч";
            }
        }
        static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        private void groupNameBox_TextChanged(object sender, EventArgs e)
        {

        }
        public class UniversityResponse
        {
            //[JsonProperty("university")]
            public University University { get; set; }
        }
        public class University
        {
            [JsonProperty("short_name")]
            public string ShortName { get; set; }

            [JsonProperty("faculties")]
            public List<Faculty> Faculties { get; set; }
        }
        public class Faculty
        {
            [JsonProperty("short_name")]
            public string ShortName { get; set; }

            [JsonProperty("directions")]
            public List<Direction> Directions { get; set; }
        }
        public class Direction
        {
            [JsonProperty("short_name")]
            public string ShortName { get; set; }

            [JsonProperty("specialities")]
            public List<Speciality> Specialities { get; set; }
        }
        public class Speciality
        {
            [JsonProperty("groups")]
            public List<Group> Groups { get; set; }
        }
        public class Group
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
        }
        static Dictionary<string, object> GetCistInfo()
        {
            // Implement the logic to fetch your cist_info data here
            // Return a Dictionary<string, object> representing your data structure
            return new Dictionary<string, object>();
        }
        public class GroupInfo : IComparable<GroupInfo>
        {
            public string Name { get; set; }
            public int Id { get; set; }

            public GroupInfo(string name, int id)
            {
                Name = name;
                Id = id;
            }

            public int CompareTo(GroupInfo other)
            {
                return string.Compare(Name, other.Name, StringComparison.Ordinal);
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
        class Teacher
        {
            [JsonProperty("id")]
            public int Id { get; set; }
            [JsonProperty("full_name")]
            public string FullName { get; set; }
            [JsonProperty("short_name")]
            public string ShortName { get; set; }
        }
        class Subject
        {
            public int Id { get; set; }
            public string Brief { get; set; }
            public string Title { get; set; }
            public List<Hour> Hours { get; set; }
        }
        class Hour
        {
            public int Type { get; set; }
            public int Val { get; set; }
            public List<int> Teachers { get; set; }
        }
        class Type
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
            public string TypeName { get; set; }
        }
    }
}


