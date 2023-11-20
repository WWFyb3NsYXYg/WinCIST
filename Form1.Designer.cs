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
            groupBox1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            groupBox2.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
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
            get_btn.Text = "GET";
            get_btn.UseVisualStyleBackColor = true;
            get_btn.Click += button1_Click;
            // 
            // txtbox
            // 
            txtbox.EnableAutoDragDrop = true;
            txtbox.Location = new Point(3, 191);
            txtbox.Name = "txtbox";
            txtbox.ReadOnly = true;
            txtbox.Size = new Size(364, 336);
            txtbox.TabIndex = 13;
            txtbox.Text = "";
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.Controls.Add(groupBox2);
            flowLayoutPanel3.Controls.Add(groupBox1);
            flowLayoutPanel3.Controls.Add(get_btn);
            flowLayoutPanel3.Controls.Add(txtbox);
            flowLayoutPanel3.Dock = DockStyle.Fill;
            flowLayoutPanel3.Location = new Point(0, 0);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(374, 539);
            flowLayoutPanel3.TabIndex = 14;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(374, 539);
            Controls.Add(flowLayoutPanel3);
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
            ResumeLayout(false);
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
    }
}