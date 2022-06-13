
namespace server
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.mode_label = new System.Windows.Forms.Label();
            this.question_textBox = new System.Windows.Forms.TextBox();
            this.send_button = new System.Windows.Forms.Button();
            this.players_listBox = new System.Windows.Forms.ListBox();
            this.players_label = new System.Windows.Forms.Label();
            this.port_label = new System.Windows.Forms.Label();
            this.ip_label = new System.Windows.Forms.Label();
            this.port_textBox = new System.Windows.Forms.TextBox();
            this.ip_textBox = new System.Windows.Forms.TextBox();
            this.trigger_button = new System.Windows.Forms.Button();
            this.mode_textBox = new System.Windows.Forms.TextBox();
            this.answer_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mode_label
            // 
            this.mode_label.AutoSize = true;
            this.mode_label.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mode_label.Location = new System.Drawing.Point(241, 230);
            this.mode_label.Name = "mode_label";
            this.mode_label.Size = new System.Drawing.Size(83, 42);
            this.mode_label.TabIndex = 19;
            this.mode_label.Text = "Mode";
            // 
            // question_textBox
            // 
            this.question_textBox.Font = new System.Drawing.Font("Segoe UI Emoji", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.question_textBox.Location = new System.Drawing.Point(330, 306);
            this.question_textBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.question_textBox.Multiline = true;
            this.question_textBox.Name = "question_textBox";
            this.question_textBox.Size = new System.Drawing.Size(188, 75);
            this.question_textBox.TabIndex = 18;
            this.question_textBox.Text = "Welcome";
            // 
            // send_button
            // 
            this.send_button.BackColor = System.Drawing.Color.LightCyan;
            this.send_button.Font = new System.Drawing.Font("Segoe Print", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.send_button.Location = new System.Drawing.Point(544, 249);
            this.send_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.send_button.Name = "send_button";
            this.send_button.Size = new System.Drawing.Size(140, 132);
            this.send_button.TabIndex = 17;
            this.send_button.Text = "Send Question";
            this.send_button.UseVisualStyleBackColor = false;
            this.send_button.Click += new System.EventHandler(this.send_button_Click_1);
            // 
            // players_listBox
            // 
            this.players_listBox.Font = new System.Drawing.Font("Segoe UI Emoji", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.players_listBox.FormattingEnabled = true;
            this.players_listBox.ItemHeight = 27;
            this.players_listBox.Location = new System.Drawing.Point(33, 67);
            this.players_listBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.players_listBox.Name = "players_listBox";
            this.players_listBox.Size = new System.Drawing.Size(166, 301);
            this.players_listBox.TabIndex = 16;
            // 
            // players_label
            // 
            this.players_label.AutoSize = true;
            this.players_label.Font = new System.Drawing.Font("Segoe Print", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.players_label.Location = new System.Drawing.Point(24, 13);
            this.players_label.Name = "players_label";
            this.players_label.Size = new System.Drawing.Size(186, 49);
            this.players_label.TabIndex = 15;
            this.players_label.Text = "Players List";
            // 
            // port_label
            // 
            this.port_label.AutoSize = true;
            this.port_label.Font = new System.Drawing.Font("Segoe Print", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.port_label.Location = new System.Drawing.Point(222, 70);
            this.port_label.Name = "port_label";
            this.port_label.Size = new System.Drawing.Size(187, 49);
            this.port_label.TabIndex = 14;
            this.port_label.Text = "Server Port";
            // 
            // ip_label
            // 
            this.ip_label.AutoSize = true;
            this.ip_label.Font = new System.Drawing.Font("Segoe Print", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ip_label.Location = new System.Drawing.Point(258, 16);
            this.ip_label.Name = "ip_label";
            this.ip_label.Size = new System.Drawing.Size(151, 49);
            this.ip_label.TabIndex = 13;
            this.ip_label.Text = "Server Ip";
            // 
            // port_textBox
            // 
            this.port_textBox.Font = new System.Drawing.Font("Segoe UI Emoji", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.port_textBox.Location = new System.Drawing.Point(424, 80);
            this.port_textBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.port_textBox.Multiline = true;
            this.port_textBox.Name = "port_textBox";
            this.port_textBox.Size = new System.Drawing.Size(260, 38);
            this.port_textBox.TabIndex = 12;
            // 
            // ip_textBox
            // 
            this.ip_textBox.Font = new System.Drawing.Font("Segoe UI Emoji", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ip_textBox.Location = new System.Drawing.Point(424, 26);
            this.ip_textBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ip_textBox.Multiline = true;
            this.ip_textBox.Name = "ip_textBox";
            this.ip_textBox.Size = new System.Drawing.Size(260, 38);
            this.ip_textBox.TabIndex = 11;
            // 
            // trigger_button
            // 
            this.trigger_button.BackColor = System.Drawing.Color.LightCyan;
            this.trigger_button.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trigger_button.Location = new System.Drawing.Point(568, 136);
            this.trigger_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trigger_button.Name = "trigger_button";
            this.trigger_button.Size = new System.Drawing.Size(118, 60);
            this.trigger_button.TabIndex = 10;
            this.trigger_button.Text = "Trigger";
            this.trigger_button.UseVisualStyleBackColor = false;
            this.trigger_button.Click += new System.EventHandler(this.trigger_button_Click_1);
            // 
            // mode_textBox
            // 
            this.mode_textBox.Font = new System.Drawing.Font("Segoe UI Emoji", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mode_textBox.Location = new System.Drawing.Point(330, 212);
            this.mode_textBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mode_textBox.Multiline = true;
            this.mode_textBox.Name = "mode_textBox";
            this.mode_textBox.Size = new System.Drawing.Size(188, 75);
            this.mode_textBox.TabIndex = 20;
            // 
            // answer_label
            // 
            this.answer_label.AutoSize = true;
            this.answer_label.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.answer_label.Location = new System.Drawing.Point(222, 322);
            this.answer_label.Name = "answer_label";
            this.answer_label.Size = new System.Drawing.Size(108, 42);
            this.answer_label.TabIndex = 21;
            this.answer_label.Text = "Answer";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 409);
            this.Controls.Add(this.answer_label);
            this.Controls.Add(this.mode_textBox);
            this.Controls.Add(this.mode_label);
            this.Controls.Add(this.question_textBox);
            this.Controls.Add(this.send_button);
            this.Controls.Add(this.players_listBox);
            this.Controls.Add(this.players_label);
            this.Controls.Add(this.port_label);
            this.Controls.Add(this.ip_label);
            this.Controls.Add(this.port_textBox);
            this.Controls.Add(this.ip_textBox);
            this.Controls.Add(this.trigger_button);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing_1);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label mode_label;
        private System.Windows.Forms.TextBox question_textBox;
        private System.Windows.Forms.Button send_button;
        private System.Windows.Forms.ListBox players_listBox;
        private System.Windows.Forms.Label players_label;
        private System.Windows.Forms.Label port_label;
        private System.Windows.Forms.Label ip_label;
        private System.Windows.Forms.TextBox port_textBox;
        private System.Windows.Forms.TextBox ip_textBox;
        private System.Windows.Forms.Button trigger_button;
        private System.Windows.Forms.TextBox mode_textBox;
        private System.Windows.Forms.Label answer_label;
    }
}

