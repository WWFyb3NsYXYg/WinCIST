using Newtonsoft.Json;
using System.Text;
using System.Diagnostics;


namespace CIST_v1
{
    public partial class Form1 : Form
    {
        // CIST API endpoint
        private string ENDPOINT = "https://cist.nure.ua/ias/app/tt";

        private string settingsFilePath = "settings.json";
        string linkurl = "https://meet.google.com/";


        public List<GroupInfo> groupInfos; // List to store information about groups
        private List<View> settingsView;

        bool onStartLoad = false;

        // Form constructor
        public Form1()
        {
            settingsView = new List<View>();
            InitializeComponent();
            LoadGroups(); // Load groups when the form is created

        }

        // Load group information from the CIST API
        private async void LoadGroups()
        {
            // Make a GET request to CIST API to fetch group information
            string response = await MakeGetRequest();
            try
            {
                UniversityResponse universityResponse = JsonConvert.DeserializeObject<UniversityResponse>(response);

                if (universityResponse != null)
                {
                    // Extract group information from the API response
                    groupInfos = universityResponse.University.Faculties
                        .SelectMany(faculty => faculty.Directions
                            .SelectMany(direction => direction.Specialities
                                .SelectMany(speciality => speciality.Groups
                                    .Select(group => new GroupInfo($"{group.Name} ({group.Id})", group.Id))
                                )
                            )
                        )
                        .ToList();

                    // Set up the combo box for group selection
                    groupNameBox.DropDownStyle = ComboBoxStyle.DropDown; // Allow text input
                    groupNameBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend; // Enable autocomplete suggestions
                    groupNameBox.AutoCompleteSource = AutoCompleteSource.ListItems;

                    groupNameBox.DataSource = groupInfos;
                    groupNameBox.DisplayMember = "Name"; // Display group names
                    groupNameBox.ValueMember = "Id"; // Use group IDs as values
                    GroupInfo Group = LoadSettings(out GroupInfo savedGroup);
                    int index = groupInfos.FindIndex(group => group.Id == Group.Id);
                    if (index >= 0)
                    {
                        groupNameBox.SelectedIndex = index;

                        if (startupOnMenuItem.Checked)
                        {
                            DateTime selectedDateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1, 21, 59, 59);
                            DateTime selectedDateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 21, 59, 59);
                            //MessageBox.Show((int)(selectedDateFrom.Subtract(new DateTime(1970, 1, 1))).TotalSeconds + ", " + (int)(selectedDateTo.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
                            string response_now = await MakeGetRequestSchedule(savedGroup.Id, (int)(selectedDateFrom.Subtract(new DateTime(1970, 1, 1))).TotalSeconds, (int)(selectedDateTo.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);

                            // Display formatted schedule in the text box
                            txtbox.Text = FormatCN(response_now);
                        }
                    }

                }
                else
                {
                    MessageBox.Show("universityResponse is null. Unable to process data.");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Невозможно соедениться с сервером");
            }
        }


        // Button click event handler
        private async void button1_Click(object sender, EventArgs e)
        {
            string text = "";
            int unixTimestamp_to = 0, unixTimestamp_from = 0;

            // Process 'From' date if enabled
            if (time_from_pick.Enabled)
            {
                DateTime selectedDateFrom = time_from_pick.Value.Date.AddDays(-1);
                unixTimestamp_from += (int)(selectedDateFrom.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                text += "From: " + unixTimestamp_from.ToString();
            }

            // Process 'To' date if enabled
            if (time_to_pick.Enabled)
            {
                DateTime selectedDateTo = time_to_pick.Value.Date.AddDays(1).AddSeconds(-10810);
                unixTimestamp_to += (int)(selectedDateTo.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                text += "\nTo: " + unixTimestamp_to.ToString() + "\n";
            }

            // Get selected group information
            string selectedText = groupNameBox.Text;
            int selectedId = 0;

            if (groupNameBox.SelectedItem is GroupInfo selectedGroup)
            {
                selectedId = selectedGroup.Id;
                text += "Group ID: " + selectedId;
                settingsView[0].Id = selectedId;
                SaveSettings();
            }

            // Make a GET request to CIST API to fetch schedule for the selected group
            string response = await MakeGetRequestSchedule(selectedId, unixTimestamp_from, unixTimestamp_to);

            // Display formatted schedule in the text box
            txtbox.Text = FormatCN(response);
        }
        // Make a GET request to CIST API
        private async Task<string> MakeGetRequest()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = $"{ENDPOINT}/P_API_GROUP_JSON"; // API endpoint for group information

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

        // Read the selected group from the configuration file

        private void startupOnMenuItem_Click(object sender, EventArgs e)
        {
            startupOnMenuItem.Checked = !startupOnMenuItem.Checked;
            UpdateSettingsAndSave(0, "startupOn", startupOnMenuItem.Checked);
        }
        private void SaveSheduleAs(string content)
        {
            // Создаем экземпляр SaveFileDialog
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // Устанавливаем фильтры для файлов
            saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

            saveFileDialog.FileName = $"schedule_{DateTime.Now:dd_MM}.txt";

            // Открываем диалог сохранения файла
            DialogResult result = saveFileDialog.ShowDialog();

            // Проверяем, был ли файл выбран и диалог подтвержден
            if (result == DialogResult.OK)
            {
                // Получаем путь и имя выбранного файла
                string filePath = saveFileDialog.FileName;

                // Текст, который вы хотите записать в файл

                try
                {
                    // Используем File.WriteAllText для записи текста в выбранный файл
                    System.IO.File.WriteAllText(filePath, content);

                    MessageBox.Show("Файл успешно сохранен по выбранному пути.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Отменено пользователем.");
            }
        }

        // Make a GET request to CIST API to fetch schedule for the selected group within a time range
        public async Task<string> MakeGetRequestSchedule(int ID, int time_from, int time_to)
        {
            string t_from, t_to;

            // Add 'time_from' parameter if not zero
            if (time_from != 0)
            {
                t_from = $"&time_from={time_from}";
            }
            else
            {
                t_from = "";
            }

            // Add 'time_to' parameter if not zero
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
                    // Build the URL for the schedule request
                    string url = $"{ENDPOINT}/P_API_EVENTS_GROUP_JSON?p_id_group={ID}{t_from}{t_to}&idClient=KNURESked";

                    // Make the GET request
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

        // Checkbox event handler for 'To' date
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            time_to_pick.Enabled = time_to_check.Checked;
        }

        // Event handler for combo box selection change
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Currently empty, can be used for additional functionality if needed
        }

        // Event handler for 'From' date checkbox
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            time_from_pick.Enabled = time_from_check.Checked;
        }

        // Method to format CIST API response into readable event information
        private string FormatCN(string response)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error, // Throw an error if a property is not found during deserialization
                };

                // Deserialize CIST API response into timetable object
                var timetable = Newtonsoft.Json.JsonConvert.DeserializeObject<Timetable>(response, settings);
                string eventInfo = "";

                var urlsJson = File.ReadAllText("links.json");
                var urls = JsonConvert.DeserializeObject<Dictionary<int, List<Dictionary<int, string>>>>(urlsJson);

                foreach (var ev in timetable.Events)
                {
                    var subject = timetable.Subjects.FirstOrDefault(s => s.Id == ev.SubjectId);
                    var eventType = timetable.Types.FirstOrDefault(t => t.Id == ev.Type)?.ShortName ?? "Unknown";

                    if (subject != null)
                    {
                        string subjectTitle = subject.Title;

                        // Get teacher information for the event
                        var teachers = ev.Teachers.Select(teacherId => timetable.Teachers.FirstOrDefault(t => t.Id == teacherId)).Where(teacher => teacher != null);
                        List<string> teacherNames = teachers.Select(teacher => teacher.ShortName).ToList();
                        string Date = DateTimeOffset.FromUnixTimeSeconds(ev.StartTime).ToLocalTime().ToString("dd.MM.yyyy");
                        string startTime = DateTimeOffset.FromUnixTimeSeconds(ev.StartTime).ToLocalTime().ToString("HH:mm");
                        string endTime = DateTimeOffset.FromUnixTimeSeconds(ev.EndTime).ToLocalTime().ToString("HH:mm");
                        var groups = ev.Groups.Select(groupId => timetable.Groups.FirstOrDefault(g => g.Id == groupId)).Where(group => group != null);
                        List<string> groupNames = groups.Select(group => group.Name).ToList();
                        string teacherUrl = GetTeacherUrl(ev.SubjectId, ev.Teachers, urls);

                        // Build event information string
                        eventInfo += $"💼 {subjectTitle} ({eventType})\n";
                        if (audiencesMenuItem.Checked)
                        {
                            eventInfo += $"🏫Аудитория: {ev.Auditory}\n";
                        }
                        if (groupsMenuItem.Checked)
                        {
                            eventInfo += $"🤝Группы: {string.Join(", ", groupNames)}\n";
                        }
                        if (teacherMenuItem.Checked && teacherNames.Any())
                        {
                            eventInfo += $"👩‍🏫 Преподаватель: {string.Join(", ", teacherNames)}\n";
                        }
                        if (dateMenuItem.Checked)
                        {
                            eventInfo += $"📅 Дата: {Date}\n";
                        }
                        if (timeMenuItem.Checked)
                        {
                            eventInfo += $"🕖 Время: {startTime} - {endTime}\n";
                        }
                        if (linksMenuItem.Checked && teacherNames.Any())
                        {
                            eventInfo += $"🔗 Ссылка: {linkurl + teacherUrl}\n";
                        }
                        eventInfo += "\n\n";
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
                return "Нет встреч";
            }
        }
        private string GetTeacherUrl(int subjectId, List<int> teacherIds, Dictionary<int, List<Dictionary<int, string>>> urls)
        {
            string teacherUrl = "None";

            if (urls.TryGetValue(subjectId, out var subjectUrls))
            {
                foreach (var innerDict in subjectUrls)
                {
                    foreach (var entry in innerDict)
                    {
                        if (teacherIds.Contains(entry.Key))
                        {
                            teacherUrl = entry.Value;
                            break;
                        }
                    }
                }
            }

            return teacherUrl;
        }
        // Convert Unix timestamp to DateTime
        static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        // Event handler for text change in the group name combo box
        private void groupNameBox_TextChanged(object sender, EventArgs e)
        {
            // Currently empty, can be used for additional functionality if needed
        }

        // Class representing the CIST API response for university information
        private void label1_Click(object sender, EventArgs e)
        {
            // Currently empty, can be used for additional functionality if needed
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {
            // Currently empty, can be used for additional functionality if needed
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Currently empty, can be used for additional functionality if needed
        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {
            // Currently empty, can be used for additional functionality if needed
        }
        public class UniversityResponse
        {
            public University University { get; set; }
        }

        // Class representing university information
        public class University
        {
            [JsonProperty("short_name")]
            public string ShortName { get; set; }

            [JsonProperty("faculties")]
            public List<Faculty> Faculties { get; set; }
        }

        // Class representing faculty information
        public class Faculty
        {
            [JsonProperty("short_name")]
            public string ShortName { get; set; }

            [JsonProperty("directions")]
            public List<Direction> Directions { get; set; }
        }

        // Class representing academic direction information
        public class Direction
        {
            [JsonProperty("short_name")]
            public string ShortName { get; set; }

            [JsonProperty("specialities")]
            public List<Speciality> Specialities { get; set; }
        }

        // Class representing academic speciality information
        public class Speciality
        {
            [JsonProperty("groups")]
            public List<Group> Groups { get; set; }
        }

        // Class representing academic group information
        public class Group
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
        }

        // Method to get CIST information (currently empty)
        static Dictionary<string, object> GetCistInfo()
        {
            return new Dictionary<string, object>();
        }

        // Class representing information about a group
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

        // Class representing the timetable structure
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
        // Class representing information about a type (e.g., lecture, seminar) associated with a subject
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
        class View
        {
            public int Id { get; set; } = 0;
            public bool startupOn { get; set; } = false;
            public bool audiences { get; set; } = false;
            public bool teacher { get; set; } = true;
            public bool time { get; set; } = true;
            public bool date { get; set; } = true;
            public bool groups { get; set; } = false;
            public bool links { get; set; } = false;
        }
        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }
        private void видToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void saveAsMenuItem_Click(object sender, EventArgs e)
        {
            SaveSheduleAs(txtbox.Text);
        }
        private void exitMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        private void saveSentingsMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void linkOnGitMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Используем Process.Start для открытия веб-страницы в браузере
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://github.com/WWFyb3NsYXYg/WinCIST",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            about aboutForm = new about();
            aboutForm.ShowDialog();
        }
        public GroupInfo LoadSettings(out GroupInfo savedGroup)
        {
            bool return_val = false;
            if (File.Exists(settingsFilePath))
            {
                string json = File.ReadAllText(settingsFilePath);
                settingsView = JsonConvert.DeserializeObject<List<View>>(json);
                return_val = true;

            }
            else
            {
                // Create default settings if the file doesn't exist
                settingsView.Add(new View());

            }

            // Update menu items based on loaded settings
            savedGroup = new GroupInfo("", settingsView[0].Id);
            linkOnMenuItem.Checked = linksMenuItem.Checked = settingsView[0].links;
            startupOnMenuItem.Checked = settingsView[0].startupOn;
            audiencesMenuItem.Checked = settingsView[0].audiences;
            teacherMenuItem.Checked = settingsView[0].teacher;
            timeMenuItem.Checked = settingsView[0].time;
            dateMenuItem.Checked = settingsView[0].date;
            groupsMenuItem.Checked = settingsView[0].groups;
            return savedGroup;

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
                MessageBox.Show($"Произошла ошибка при открытии файла: {ex.Message}");
                File.WriteAllText("err.json", ex.Message);
            }
        }
        private void SaveSettings()
        {
            string json = JsonConvert.SerializeObject(settingsView, Formatting.Indented);
            File.WriteAllText(settingsFilePath, json);
        }
        private void UpdateSettingsAndSave(int index, string propertyName, bool value)
        {
            if (index >= 0 && index < settingsView.Count)
            {
                // Use reflection to dynamically update the specified property
                var property = typeof(View).GetProperty(propertyName);
                if (property != null && property.PropertyType == typeof(bool))
                {
                    property.SetValue(settingsView[index], value);
                    SaveSettings();
                }
            }
        }
        private void audiencesMenuItem_Click(object sender, EventArgs e)
        {
            audiencesMenuItem.Checked = !audiencesMenuItem.Checked;
            UpdateSettingsAndSave(0, "audiences", audiencesMenuItem.Checked);
        }

        private void timeMenuItem_Click(object sender, EventArgs e)
        {
            timeMenuItem.Checked = !timeMenuItem.Checked;
            UpdateSettingsAndSave(0, "time", timeMenuItem.Checked);
        }

        private void dateMenuItem_Click(object sender, EventArgs e)
        {
            dateMenuItem.Checked = !dateMenuItem.Checked;
            UpdateSettingsAndSave(0, "date", dateMenuItem.Checked);
        }

        private void groupsMenuItem_Click(object sender, EventArgs e)
        {
            groupsMenuItem.Checked = !groupsMenuItem.Checked;
            UpdateSettingsAndSave(0, "groups", groupsMenuItem.Checked);
        }

        private void teacherMenuItem_Click(object sender, EventArgs e)
        {
            teacherMenuItem.Checked = !teacherMenuItem.Checked;
            UpdateSettingsAndSave(0, "teacher", teacherMenuItem.Checked);
        }

        private void setupMenuItem_Click(object sender, EventArgs e)
        {
            // Укажите путь к файлу, который нужно создать и открыть
            string filePath = "links.json";

            // Создание файла и запись в него текста
            
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "");
            }

            // Открытие файла в текстовом редакторе
            OpenFileInDefaultTextEditor(filePath);

        }

        private void linkOnMenuItem_Click(object sender, EventArgs e)
        {
            string filePath = "links.json";

            // Проверка существования файла
            if (File.Exists(filePath))
            {
                linkOnMenuItem.Checked = linksMenuItem.Checked = !linkOnMenuItem.Checked;
                UpdateSettingsAndSave(0, "links", linkOnMenuItem.Checked);
            }
            else
            {
                MessageBox.Show("Файл не существует.");
            }
        }

        private void txtbox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.LinkText) { UseShellExecute = true });
        }
    }
}



