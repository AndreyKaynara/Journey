namespace JourneyGUI
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.btnClose = new System.Windows.Forms.Button();
            this.pictureLogo = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblDesc1 = new System.Windows.Forms.Label();
            this.lblDesc2 = new System.Windows.Forms.Label();
            this.lblDesc3 = new System.Windows.Forms.Label();
            this.lblYears = new System.Windows.Forms.Label();
            this.lblDesc4 = new System.Windows.Forms.Label();
            this.lblDesc5 = new System.Windows.Forms.Label();
            this.lblDesc6 = new System.Windows.Forms.Label();
            this.lblDesc7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(129, 328);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pictureLogo
            // 
            this.pictureLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureLogo.Image")));
            this.pictureLogo.InitialImage = null;
            this.pictureLogo.Location = new System.Drawing.Point(12, 12);
            this.pictureLogo.Name = "pictureLogo";
            this.pictureLogo.Size = new System.Drawing.Size(102, 97);
            this.pictureLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureLogo.TabIndex = 1;
            this.pictureLogo.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(124, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Journey";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(266, 8);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(58, 13);
            this.lblVersion.TabIndex = 3;
            this.lblVersion.Text = "lblVersion";
            // 
            // lblDesc1
            // 
            this.lblDesc1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesc1.Location = new System.Drawing.Point(126, 44);
            this.lblDesc1.Name = "lblDesc1";
            this.lblDesc1.Size = new System.Drawing.Size(198, 65);
            this.lblDesc1.TabIndex = 4;
            this.lblDesc1.Text = "Решатель симметричной задачи коммивояжёра. Реализованы алгоритмы ближайшего сосед" +
    "а, 2-опт и Лина-Кернигана.";
            // 
            // lblDesc2
            // 
            this.lblDesc2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesc2.Location = new System.Drawing.Point(12, 112);
            this.lblDesc2.Name = "lblDesc2";
            this.lblDesc2.Size = new System.Drawing.Size(303, 50);
            this.lblDesc2.TabIndex = 5;
            this.lblDesc2.Text = "Данные задаются в формате TSP в нескольких поддерживаемых формах: в виде двухмерн" +
    "ых или трёхмерных координат и в виде матрицы стоимости.";
            // 
            // lblDesc3
            // 
            this.lblDesc3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesc3.Location = new System.Drawing.Point(12, 162);
            this.lblDesc3.Name = "lblDesc3";
            this.lblDesc3.Size = new System.Drawing.Size(303, 59);
            this.lblDesc3.TabIndex = 6;
            this.lblDesc3.Text = "Расчитанный тур можно сохранить. Также можно загрузить уже ранее вычисленный тур " +
    "и попробовать его улучшить.";
            // 
            // lblYears
            // 
            this.lblYears.AutoSize = true;
            this.lblYears.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYears.Location = new System.Drawing.Point(265, 26);
            this.lblYears.Name = "lblYears";
            this.lblYears.Size = new System.Drawing.Size(59, 13);
            this.lblYears.TabIndex = 7;
            this.lblYears.Text = "2015-2016";
            // 
            // lblDesc4
            // 
            this.lblDesc4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesc4.Location = new System.Drawing.Point(12, 212);
            this.lblDesc4.Name = "lblDesc4";
            this.lblDesc4.Size = new System.Drawing.Size(303, 54);
            this.lblDesc4.TabIndex = 8;
            this.lblDesc4.Text = "Программа была разработана в 2015-2016 годах в рамках работы над ВКРБ «Алгоритмы " +
    "и программы решения задачи маршрутизации транспортных средств» студентом ФБИ-21 " +
    "Андреем Кайнара. ";
            // 
            // lblDesc5
            // 
            this.lblDesc5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesc5.Location = new System.Drawing.Point(9, 266);
            this.lblDesc5.Name = "lblDesc5";
            this.lblDesc5.Size = new System.Drawing.Size(303, 20);
            this.lblDesc5.TabIndex = 9;
            this.lblDesc5.Text = "Кафедра экономической информатики";
            this.lblDesc5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDesc6
            // 
            this.lblDesc6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesc6.Location = new System.Drawing.Point(9, 282);
            this.lblDesc6.Name = "lblDesc6";
            this.lblDesc6.Size = new System.Drawing.Size(303, 14);
            this.lblDesc6.TabIndex = 10;
            this.lblDesc6.Text = "Факультет бизнеса";
            this.lblDesc6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDesc7
            // 
            this.lblDesc7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesc7.Location = new System.Drawing.Point(38, 296);
            this.lblDesc7.Name = "lblDesc7";
            this.lblDesc7.Size = new System.Drawing.Size(250, 29);
            this.lblDesc7.TabIndex = 11;
            this.lblDesc7.Text = "Новосибирский государственный технический университет";
            this.lblDesc7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 355);
            this.ControlBox = false;
            this.Controls.Add(this.lblDesc7);
            this.Controls.Add(this.lblDesc6);
            this.Controls.Add(this.lblDesc5);
            this.Controls.Add(this.lblDesc4);
            this.Controls.Add(this.lblYears);
            this.Controls.Add(this.lblDesc3);
            this.Controls.Add(this.lblDesc2);
            this.Controls.Add(this.lblDesc1);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureLogo);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "О программе";
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox pictureLogo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblDesc1;
        private System.Windows.Forms.Label lblDesc2;
        private System.Windows.Forms.Label lblDesc3;
        private System.Windows.Forms.Label lblYears;
        private System.Windows.Forms.Label lblDesc4;
        private System.Windows.Forms.Label lblDesc5;
        private System.Windows.Forms.Label lblDesc6;
        private System.Windows.Forms.Label lblDesc7;
    }
}