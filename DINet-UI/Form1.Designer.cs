namespace DINet_UI
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.txtInputVideo = new System.Windows.Forms.TextBox();
            this.btnBrowseInputVideo = new System.Windows.Forms.Button();
            this.btnBrowseInputAudio = new System.Windows.Forms.Button();
            this.txtInputAudio = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.txtShellOutput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input Video:";
            // 
            // txtInputVideo
            // 
            this.txtInputVideo.AllowDrop = true;
            this.txtInputVideo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtInputVideo.Location = new System.Drawing.Point(102, 9);
            this.txtInputVideo.Name = "txtInputVideo";
            this.txtInputVideo.Size = new System.Drawing.Size(283, 23);
            this.txtInputVideo.TabIndex = 1;
            this.txtInputVideo.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtInputVideo_DragDrop);
            this.txtInputVideo.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtInputVideo_DragEnter);
            // 
            // btnBrowseInputVideo
            // 
            this.btnBrowseInputVideo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnBrowseInputVideo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnBrowseInputVideo.Location = new System.Drawing.Point(391, 7);
            this.btnBrowseInputVideo.Name = "btnBrowseInputVideo";
            this.btnBrowseInputVideo.Size = new System.Drawing.Size(77, 25);
            this.btnBrowseInputVideo.TabIndex = 2;
            this.btnBrowseInputVideo.Text = "Browse...";
            this.btnBrowseInputVideo.UseVisualStyleBackColor = true;
            this.btnBrowseInputVideo.Click += new System.EventHandler(this.btnBrowseInputVideo_Click);
            // 
            // btnBrowseInputAudio
            // 
            this.btnBrowseInputAudio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnBrowseInputAudio.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnBrowseInputAudio.Location = new System.Drawing.Point(391, 38);
            this.btnBrowseInputAudio.Name = "btnBrowseInputAudio";
            this.btnBrowseInputAudio.Size = new System.Drawing.Size(77, 26);
            this.btnBrowseInputAudio.TabIndex = 5;
            this.btnBrowseInputAudio.Text = "Browse...";
            this.btnBrowseInputAudio.UseVisualStyleBackColor = true;
            this.btnBrowseInputAudio.Click += new System.EventHandler(this.btnBrowseInputAudio_Click);
            // 
            // txtInputAudio
            // 
            this.txtInputAudio.AllowDrop = true;
            this.txtInputAudio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtInputAudio.Location = new System.Drawing.Point(102, 40);
            this.txtInputAudio.Name = "txtInputAudio";
            this.txtInputAudio.Size = new System.Drawing.Size(283, 23);
            this.txtInputAudio.TabIndex = 4;
            this.txtInputAudio.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtInputAudio_DragDrop);
            this.txtInputAudio.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtInputAudio_DragEnter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(9, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Input Audio:";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.btnGenerate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGenerate.Location = new System.Drawing.Point(159, 126);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(130, 48);
            this.btnGenerate.TabIndex = 6;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // txtShellOutput
            // 
            this.txtShellOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtShellOutput.Location = new System.Drawing.Point(12, 214);
            this.txtShellOutput.Multiline = true;
            this.txtShellOutput.Name = "txtShellOutput";
            this.txtShellOutput.ReadOnly = true;
            this.txtShellOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtShellOutput.Size = new System.Drawing.Size(877, 271);
            this.txtShellOutput.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(8, 191);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Shell Output:";
            // 
            // txtLog
            // 
            this.txtLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtLog.Location = new System.Drawing.Point(474, 7);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(415, 193);
            this.txtLog.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 497);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtShellOutput);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnBrowseInputAudio);
            this.Controls.Add(this.txtInputAudio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnBrowseInputVideo);
            this.Controls.Add(this.txtInputVideo);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "DINet UI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInputVideo;
        private System.Windows.Forms.Button btnBrowseInputVideo;
        private System.Windows.Forms.Button btnBrowseInputAudio;
        private System.Windows.Forms.TextBox txtInputAudio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.TextBox txtShellOutput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLog;
    }
}

