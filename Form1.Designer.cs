namespace CIST_v1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            time_from_pick = new DateTimePicker();
            time_from_check = new CheckBox();
            time_to_check = new CheckBox();
            time_to_pick = new DateTimePicker();
            groupNameBox = new ComboBox();
            label1 = new Label();
            groupBox1 = new GroupBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            groupBox2 = new GroupBox();
            flowLayoutPanel2 = new FlowLayoutPanel();
            get_btn = new Button();
            txtbox = new RichTextBox();
            flowLayoutPanel3 = new FlowLayoutPanel();
            menuStrip1 = new MenuStrip();
            toolStripComboBox1 = new ToolStripMenuItem();
            saveAsMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            exitMenuItem = new ToolStripMenuItem();
            viewMenuItem = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            audiencesMenuItem = new ToolStripMenuItem();
            teacherMenuItem = new ToolStripMenuItem();
            groupsMenuItem = new ToolStripMenuItem();
            dateMenuItem = new ToolStripMenuItem();
            timeMenuItem = new ToolStripMenuItem();
            linksMenuItem = new ToolStripMenuItem();
            linkOnMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            setupMenuItem = new ToolStripMenuItem();
            уведомленияToolStripMenuItem = new ToolStripMenuItem();
            alarmCoupleMenuItem = new ToolStripMenuItem();
            startupOnMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            linkOnGitMenuItem = new ToolStripMenuItem();
            aboutMenuItem = new ToolStripMenuItem();
            groupBox1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            groupBox2.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // time_from_pick
            // 
            time_from_pick.Enabled = false;
            time_from_pick.Location = new Point(124, 3);
            time_from_pick.Name = "time_from_pick";
            time_from_pick.Size = new Size(215, 23);
            time_from_pick.TabIndex = 0;
            // 
            // time_from_check
            // 
            time_from_check.AutoSize = true;
            time_from_check.Location = new Point(3, 3);
            time_from_check.Name = "time_from_check";
            time_from_check.Size = new Size(115, 19);
            time_from_check.TabIndex = 1;
            time_from_check.Text = "Начальная дата:";
            time_from_check.UseVisualStyleBackColor = true;
            time_from_check.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // time_to_check
            // 
            time_to_check.AutoSize = true;
            time_to_check.Location = new Point(3, 32);
            time_to_check.Name = "time_to_check";
            time_to_check.Size = new Size(114, 19);
            time_to_check.TabIndex = 3;
            time_to_check.Text = "Конечная дата:  ";
            time_to_check.UseVisualStyleBackColor = true;
            time_to_check.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // time_to_pick
            // 
            time_to_pick.Enabled = false;
            time_to_pick.Location = new Point(123, 32);
            time_to_pick.Name = "time_to_pick";
            time_to_pick.Size = new Size(216, 23);
            time_to_pick.TabIndex = 2;
            // 
            // groupNameBox
            // 
            groupNameBox.BackColor = Color.White;
            groupNameBox.FormattingEnabled = true;
            groupNameBox.Location = new Point(118, 3);
            groupNameBox.Name = "groupNameBox";
            groupNameBox.RightToLeft = RightToLeft.No;
            groupNameBox.Size = new Size(215, 23);
            groupNameBox.TabIndex = 5;
            groupNameBox.Text = "Выбери группу";
            groupNameBox.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(109, 15);
            label1.TabIndex = 6;
            label1.Text = "Название группы: ";
            label1.Click += label1_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(flowLayoutPanel1);
            groupBox1.Location = new Point(3, 68);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(364, 88);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Выборка";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(time_from_check);
            flowLayoutPanel1.Controls.Add(time_from_pick);
            flowLayoutPanel1.Controls.Add(time_to_check);
            flowLayoutPanel1.Controls.Add(time_to_pick);
            flowLayoutPanel1.Location = new Point(6, 22);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(352, 61);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(flowLayoutPanel2);
            groupBox2.Location = new Point(3, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(364, 59);
            groupBox2.TabIndex = 8;
            groupBox2.TabStop = false;
            groupBox2.Text = "Группа";
            groupBox2.Enter += groupBox2_Enter;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(label1);
            flowLayoutPanel2.Controls.Add(groupNameBox);
            flowLayoutPanel2.Location = new Point(12, 22);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(346, 29);
            flowLayoutPanel2.TabIndex = 0;
            // 
            // get_btn
            // 
            get_btn.Location = new Point(3, 162);
            get_btn.Name = "get_btn";
            get_btn.Size = new Size(364, 23);
            get_btn.TabIndex = 10;
            get_btn.Text = "Получить расписание";
            get_btn.UseVisualStyleBackColor = true;
            get_btn.Click += button1_Click;
            // 
            // txtbox
            // 
            txtbox.EnableAutoDragDrop = true;
            txtbox.Location = new Point(3, 191);
            txtbox.Name = "txtbox";
            txtbox.ReadOnly = true;
            txtbox.Size = new Size(364, 324);
            txtbox.TabIndex = 13;
            txtbox.Text = "";
            txtbox.LinkClicked += txtbox_LinkClicked;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.Controls.Add(groupBox2);
            flowLayoutPanel3.Controls.Add(groupBox1);
            flowLayoutPanel3.Controls.Add(get_btn);
            flowLayoutPanel3.Controls.Add(txtbox);
            flowLayoutPanel3.Dock = DockStyle.Fill;
            flowLayoutPanel3.Location = new Point(0, 24);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(374, 515);
            flowLayoutPanel3.TabIndex = 14;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripComboBox1, viewMenuItem, toolStripMenuItem1 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(374, 24);
            menuStrip1.TabIndex = 15;
            menuStrip1.Text = "menuStrip1";
            menuStrip1.ItemClicked += menuStrip1_ItemClicked;
            // 
            // toolStripComboBox1
            // 
            toolStripComboBox1.DropDownItems.AddRange(new ToolStripItem[] { saveAsMenuItem, toolStripSeparator1, exitMenuItem });
            toolStripComboBox1.Name = "toolStripComboBox1";
            toolStripComboBox1.Size = new Size(48, 20);
            toolStripComboBox1.Text = "Файл";
            toolStripComboBox1.Click += toolStripComboBox1_Click;
            // 
            // saveAsMenuItem
            // 
            saveAsMenuItem.Name = "saveAsMenuItem";
            saveAsMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            saveAsMenuItem.Size = new Size(309, 22);
            saveAsMenuItem.Text = "Сохранить расписание как...";
            saveAsMenuItem.Click += saveAsMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(306, 6);
            // 
            // exitMenuItem
            // 
            exitMenuItem.Name = "exitMenuItem";
            exitMenuItem.RightToLeftAutoMirrorImage = true;
            exitMenuItem.ShortcutKeys = Keys.Control | Keys.Q;
            exitMenuItem.Size = new Size(309, 22);
            exitMenuItem.Text = "Выход";
            exitMenuItem.Click += exitMenuItem_Click_1;
            // 
            // viewMenuItem
            // 
            viewMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem2, уведомленияToolStripMenuItem, startupOnMenuItem });
            viewMenuItem.Name = "viewMenuItem";
            viewMenuItem.Size = new Size(39, 20);
            viewMenuItem.Text = "Вид";
            viewMenuItem.Click += видToolStripMenuItem_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.DropDownItems.AddRange(new ToolStripItem[] { audiencesMenuItem, teacherMenuItem, groupsMenuItem, dateMenuItem, timeMenuItem, linksMenuItem });
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(310, 22);
            toolStripMenuItem2.Text = "Элементы отображения";
            // 
            // audiencesMenuItem
            // 
            audiencesMenuItem.Name = "audiencesMenuItem";
            audiencesMenuItem.Size = new Size(180, 22);
            audiencesMenuItem.Text = "Аудитории";
            audiencesMenuItem.Click += audiencesMenuItem_Click;
            // 
            // teacherMenuItem
            // 
            teacherMenuItem.Checked = true;
            teacherMenuItem.CheckState = CheckState.Checked;
            teacherMenuItem.Name = "teacherMenuItem";
            teacherMenuItem.Size = new Size(180, 22);
            teacherMenuItem.Text = "Преподователь";
            teacherMenuItem.Click += teacherMenuItem_Click;
            // 
            // groupsMenuItem
            // 
            groupsMenuItem.Name = "groupsMenuItem";
            groupsMenuItem.Size = new Size(180, 22);
            groupsMenuItem.Text = "Группы";
            groupsMenuItem.Click += groupsMenuItem_Click;
            // 
            // dateMenuItem
            // 
            dateMenuItem.Checked = true;
            dateMenuItem.CheckState = CheckState.Checked;
            dateMenuItem.Name = "dateMenuItem";
            dateMenuItem.Size = new Size(180, 22);
            dateMenuItem.Text = "Дата";
            dateMenuItem.Click += dateMenuItem_Click;
            // 
            // timeMenuItem
            // 
            timeMenuItem.Checked = true;
            timeMenuItem.CheckState = CheckState.Checked;
            timeMenuItem.Name = "timeMenuItem";
            timeMenuItem.Size = new Size(180, 22);
            timeMenuItem.Text = "Время";
            timeMenuItem.Click += timeMenuItem_Click;
            // 
            // linksMenuItem
            // 
            linksMenuItem.DropDownItems.AddRange(new ToolStripItem[] { linkOnMenuItem, toolStripSeparator2, setupMenuItem });
            linksMenuItem.Name = "linksMenuItem";
            linksMenuItem.Size = new Size(180, 22);
            linksMenuItem.Text = "Ссылки";
            // 
            // linkOnMenuItem
            // 
            linkOnMenuItem.Name = "linkOnMenuItem";
            linkOnMenuItem.Size = new Size(132, 22);
            linkOnMenuItem.Text = "Включить";
            linkOnMenuItem.Click += linkOnMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(129, 6);
            // 
            // setupMenuItem
            // 
            setupMenuItem.Name = "setupMenuItem";
            setupMenuItem.Size = new Size(132, 22);
            setupMenuItem.Text = "Настроить";
            setupMenuItem.Click += setupMenuItem_Click;
            // 
            // уведомленияToolStripMenuItem
            // 
            уведомленияToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { alarmCoupleMenuItem });
            уведомленияToolStripMenuItem.Enabled = false;
            уведомленияToolStripMenuItem.Name = "уведомленияToolStripMenuItem";
            уведомленияToolStripMenuItem.Size = new Size(310, 22);
            уведомленияToolStripMenuItem.Text = "Уведомления";
            // 
            // alarmCoupleMenuItem
            // 
            alarmCoupleMenuItem.Name = "alarmCoupleMenuItem";
            alarmCoupleMenuItem.Size = new Size(193, 22);
            alarmCoupleMenuItem.Text = "Уведомление на пару";
            // 
            // startupOnMenuItem
            // 
            startupOnMenuItem.Name = "startupOnMenuItem";
            startupOnMenuItem.Size = new Size(310, 22);
            startupOnMenuItem.Text = "При старте делать запрос на текущий день";
            startupOnMenuItem.Click += startupOnMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { linkOnGitMenuItem, aboutMenuItem });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(65, 20);
            toolStripMenuItem1.Text = "Справка";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // linkOnGitMenuItem
            // 
            linkOnGitMenuItem.Name = "linkOnGitMenuItem";
            linkOnGitMenuItem.Size = new Size(171, 22);
            linkOnGitMenuItem.Text = "Проект на GitHub";
            linkOnGitMenuItem.Click += linkOnGitMenuItem_Click;
            // 
            // aboutMenuItem
            // 
            aboutMenuItem.Enabled = false;
            aboutMenuItem.Name = "aboutMenuItem";
            aboutMenuItem.Size = new Size(171, 22);
            aboutMenuItem.Text = "О программе";
            aboutMenuItem.Click += aboutMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(374, 539);
            Controls.Add(flowLayoutPanel3);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(390, 578);
            MinimumSize = new Size(390, 578);
            Name = "Form1";
            Text = "CIST v1";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            groupBox2.ResumeLayout(false);
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker time_from_pick;
        private CheckBox time_from_check;
        private CheckBox time_to_check;
        private DateTimePicker time_to_pick;
        private ComboBox groupNameBox;
        private Label label1;
        private GroupBox groupBox1;
        private FlowLayoutPanel flowLayoutPanel1;
        private GroupBox groupBox2;
        private FlowLayoutPanel flowLayoutPanel2;
        private Button get_btn;
        private RichTextBox txtbox;
        private FlowLayoutPanel flowLayoutPanel3;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripComboBox1;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem saveAsMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exitMenuItem;
        private ToolStripMenuItem viewMenuItem;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem audiencesMenuItem;
        private ToolStripMenuItem timeMenuItem;
        private ToolStripMenuItem dateMenuItem;
        private ToolStripMenuItem groupsMenuItem;
        private ToolStripMenuItem linksMenuItem;
        private ToolStripMenuItem linkOnMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem setupMenuItem;
        private ToolStripMenuItem уведомленияToolStripMenuItem;
        private ToolStripMenuItem alarmCoupleMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem linkOnGitMenuItem;
        private ToolStripMenuItem aboutMenuItem;
        private ToolStripMenuItem startupOnMenuItem;
        private ToolStripMenuItem сохранитьНастройкиToolStripMenuItem;
        private ToolStripMenuItem teacherMenuItem;
    }
}