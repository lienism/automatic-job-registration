namespace Auto_AI_Barcode_Data
{
    partial class TimeSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Introduce_label = new System.Windows.Forms.Label();
            this.Time_start = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Schedule_name_label = new System.Windows.Forms.Label();
            this.Schedule_name_setting_textbox = new System.Windows.Forms.TextBox();
            this.Schedule_Delete_Label = new System.Windows.Forms.Label();
            this.Schedule_Create_Label = new System.Windows.Forms.Label();
            this.scheduledeletebutton = new System.Windows.Forms.Button();
            this.Schedule_name_label2 = new System.Windows.Forms.Label();
            this.Schedule_name_delete_textbox = new System.Windows.Forms.TextBox();
            this.process_time_lebel = new System.Windows.Forms.Label();
            this.process_time_hour_textbox = new System.Windows.Forms.TextBox();
            this.kuri_label = new System.Windows.Forms.Label();
            this.kuri_textbox = new System.Windows.Forms.TextBox();
            this.Hour_label = new System.Windows.Forms.Label();
            this.Minutes_label = new System.Windows.Forms.Label();
            this.process_time_minutes_textbox = new System.Windows.Forms.TextBox();
            this.infor_textbox = new System.Windows.Forms.TextBox();
            this.Comment_label = new System.Windows.Forms.Label();
            this.taskschd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Introduce_label
            // 
            this.Introduce_label.AutoSize = true;
            this.Introduce_label.Font = new System.Drawing.Font("MS UI Gothic", 20F);
            this.Introduce_label.Location = new System.Drawing.Point(165, 32);
            this.Introduce_label.Name = "Introduce_label";
            this.Introduce_label.Size = new System.Drawing.Size(120, 27);
            this.Introduce_label.TabIndex = 2;
            this.Introduce_label.Text = "作業登録";
            // 
            // Time_start
            // 
            this.Time_start.Location = new System.Drawing.Point(74, 286);
            this.Time_start.Name = "Time_start";
            this.Time_start.Size = new System.Drawing.Size(75, 23);
            this.Time_start.TabIndex = 3;
            this.Time_start.Text = "登録";
            this.Time_start.UseVisualStyleBackColor = true;
            this.Time_start.Click += new System.EventHandler(this.Time_start_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Schedule_name_label
            // 
            this.Schedule_name_label.AutoSize = true;
            this.Schedule_name_label.Location = new System.Drawing.Point(12, 113);
            this.Schedule_name_label.Name = "Schedule_name_label";
            this.Schedule_name_label.Size = new System.Drawing.Size(63, 12);
            this.Schedule_name_label.TabIndex = 5;
            this.Schedule_name_label.Text = "作業名前 : ";
            // 
            // Schedule_name_setting_textbox
            // 
            this.Schedule_name_setting_textbox.Location = new System.Drawing.Point(110, 110);
            this.Schedule_name_setting_textbox.Name = "Schedule_name_setting_textbox";
            this.Schedule_name_setting_textbox.Size = new System.Drawing.Size(93, 19);
            this.Schedule_name_setting_textbox.TabIndex = 4;
            // 
            // Schedule_Delete_Label
            // 
            this.Schedule_Delete_Label.AutoSize = true;
            this.Schedule_Delete_Label.Location = new System.Drawing.Point(309, 82);
            this.Schedule_Delete_Label.Name = "Schedule_Delete_Label";
            this.Schedule_Delete_Label.Size = new System.Drawing.Size(53, 12);
            this.Schedule_Delete_Label.TabIndex = 6;
            this.Schedule_Delete_Label.Text = "作業削除";
            // 
            // Schedule_Create_Label
            // 
            this.Schedule_Create_Label.AutoSize = true;
            this.Schedule_Create_Label.Location = new System.Drawing.Point(45, 82);
            this.Schedule_Create_Label.Name = "Schedule_Create_Label";
            this.Schedule_Create_Label.Size = new System.Drawing.Size(53, 12);
            this.Schedule_Create_Label.TabIndex = 7;
            this.Schedule_Create_Label.Text = "作業作成";
            // 
            // scheduledeletebutton
            // 
            this.scheduledeletebutton.Location = new System.Drawing.Point(311, 286);
            this.scheduledeletebutton.Name = "scheduledeletebutton";
            this.scheduledeletebutton.Size = new System.Drawing.Size(75, 23);
            this.scheduledeletebutton.TabIndex = 8;
            this.scheduledeletebutton.Text = "削除";
            this.scheduledeletebutton.UseVisualStyleBackColor = true;
            this.scheduledeletebutton.Click += new System.EventHandler(this.scheduledeletebutton_Click);
            // 
            // Schedule_name_label2
            // 
            this.Schedule_name_label2.AutoSize = true;
            this.Schedule_name_label2.Location = new System.Drawing.Point(262, 188);
            this.Schedule_name_label2.Name = "Schedule_name_label2";
            this.Schedule_name_label2.Size = new System.Drawing.Size(53, 12);
            this.Schedule_name_label2.TabIndex = 10;
            this.Schedule_name_label2.Text = "作業名前";
            // 
            // Schedule_name_delete_textbox
            // 
            this.Schedule_name_delete_textbox.Location = new System.Drawing.Point(360, 185);
            this.Schedule_name_delete_textbox.Name = "Schedule_name_delete_textbox";
            this.Schedule_name_delete_textbox.Size = new System.Drawing.Size(72, 19);
            this.Schedule_name_delete_textbox.TabIndex = 9;
            // 
            // process_time_lebel
            // 
            this.process_time_lebel.AutoSize = true;
            this.process_time_lebel.Location = new System.Drawing.Point(12, 150);
            this.process_time_lebel.Name = "process_time_lebel";
            this.process_time_lebel.Size = new System.Drawing.Size(53, 12);
            this.process_time_lebel.TabIndex = 12;
            this.process_time_lebel.Text = "実行時間";
            // 
            // process_time_hour_textbox
            // 
            this.process_time_hour_textbox.Location = new System.Drawing.Point(110, 147);
            this.process_time_hour_textbox.Name = "process_time_hour_textbox";
            this.process_time_hour_textbox.Size = new System.Drawing.Size(24, 19);
            this.process_time_hour_textbox.TabIndex = 11;
            // 
            // kuri_label
            // 
            this.kuri_label.AutoSize = true;
            this.kuri_label.Location = new System.Drawing.Point(12, 188);
            this.kuri_label.Name = "kuri_label";
            this.kuri_label.Size = new System.Drawing.Size(69, 12);
            this.kuri_label.TabIndex = 14;
            this.kuri_label.Text = "繰り返す日 : ";
            // 
            // kuri_textbox
            // 
            this.kuri_textbox.Location = new System.Drawing.Point(110, 185);
            this.kuri_textbox.Name = "kuri_textbox";
            this.kuri_textbox.Size = new System.Drawing.Size(93, 19);
            this.kuri_textbox.TabIndex = 13;
            // 
            // Hour_label
            // 
            this.Hour_label.AutoSize = true;
            this.Hour_label.Location = new System.Drawing.Point(140, 150);
            this.Hour_label.Name = "Hour_label";
            this.Hour_label.Size = new System.Drawing.Size(13, 12);
            this.Hour_label.TabIndex = 15;
            this.Hour_label.Text = "H";
            // 
            // Minutes_label
            // 
            this.Minutes_label.AutoSize = true;
            this.Minutes_label.Location = new System.Drawing.Point(189, 150);
            this.Minutes_label.Name = "Minutes_label";
            this.Minutes_label.Size = new System.Drawing.Size(14, 12);
            this.Minutes_label.TabIndex = 17;
            this.Minutes_label.Text = "M";
            // 
            // process_time_minutes_textbox
            // 
            this.process_time_minutes_textbox.Location = new System.Drawing.Point(159, 147);
            this.process_time_minutes_textbox.Name = "process_time_minutes_textbox";
            this.process_time_minutes_textbox.Size = new System.Drawing.Size(24, 19);
            this.process_time_minutes_textbox.TabIndex = 16;
            // 
            // infor_textbox
            // 
            this.infor_textbox.Location = new System.Drawing.Point(14, 240);
            this.infor_textbox.Multiline = true;
            this.infor_textbox.Name = "infor_textbox";
            this.infor_textbox.Size = new System.Drawing.Size(189, 40);
            this.infor_textbox.TabIndex = 18;
            // 
            // Comment_label
            // 
            this.Comment_label.AutoSize = true;
            this.Comment_label.Location = new System.Drawing.Point(81, 225);
            this.Comment_label.Name = "Comment_label";
            this.Comment_label.Size = new System.Drawing.Size(61, 12);
            this.Comment_label.TabIndex = 19;
            this.Comment_label.Text = "一言(説明)";
            // 
            // taskschd
            // 
            this.taskschd.Location = new System.Drawing.Point(360, 12);
            this.taskschd.Name = "taskschd";
            this.taskschd.Size = new System.Drawing.Size(75, 23);
            this.taskschd.TabIndex = 20;
            this.taskschd.Text = "作業リスト";
            this.taskschd.UseVisualStyleBackColor = true;
            this.taskschd.Click += new System.EventHandler(this.taskschd_Click);
            // 
            // TimeSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 335);
            this.Controls.Add(this.taskschd);
            this.Controls.Add(this.Comment_label);
            this.Controls.Add(this.infor_textbox);
            this.Controls.Add(this.Minutes_label);
            this.Controls.Add(this.process_time_minutes_textbox);
            this.Controls.Add(this.Hour_label);
            this.Controls.Add(this.kuri_label);
            this.Controls.Add(this.kuri_textbox);
            this.Controls.Add(this.process_time_lebel);
            this.Controls.Add(this.process_time_hour_textbox);
            this.Controls.Add(this.Schedule_name_label2);
            this.Controls.Add(this.Schedule_name_delete_textbox);
            this.Controls.Add(this.scheduledeletebutton);
            this.Controls.Add(this.Schedule_Create_Label);
            this.Controls.Add(this.Schedule_Delete_Label);
            this.Controls.Add(this.Schedule_name_label);
            this.Controls.Add(this.Schedule_name_setting_textbox);
            this.Controls.Add(this.Time_start);
            this.Controls.Add(this.Introduce_label);
            this.Name = "TimeSetting";
            this.Text = "作業登録";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Introduce_label;
        private System.Windows.Forms.Button Time_start;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label Schedule_name_label;
        private System.Windows.Forms.TextBox Schedule_name_setting_textbox;
        private System.Windows.Forms.Label Schedule_Delete_Label;
        private System.Windows.Forms.Label Schedule_Create_Label;
        private System.Windows.Forms.Button scheduledeletebutton;
        private System.Windows.Forms.Label Schedule_name_label2;
        private System.Windows.Forms.TextBox Schedule_name_delete_textbox;
        private System.Windows.Forms.Label process_time_lebel;
        private System.Windows.Forms.TextBox process_time_hour_textbox;
        private System.Windows.Forms.Label kuri_label;
        private System.Windows.Forms.TextBox kuri_textbox;
        private System.Windows.Forms.Label Hour_label;
        private System.Windows.Forms.Label Minutes_label;
        private System.Windows.Forms.TextBox process_time_minutes_textbox;
        private System.Windows.Forms.TextBox infor_textbox;
        private System.Windows.Forms.Label Comment_label;
        private System.Windows.Forms.Button taskschd;
    }
}

