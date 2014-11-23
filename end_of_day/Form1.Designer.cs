namespace end_of_day
{
    using System.Data.SqlClient;
    using System.Windows.Forms;
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
            this.stocklist = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btn_area = new System.Windows.Forms.FlowLayoutPanel();
            this.button5 = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.stock_options = new System.Windows.Forms.Panel();
            this.filterbtnarea = new System.Windows.Forms.Panel();
            this.run_stock = new System.Windows.Forms.Button();
            this.option_filters = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.outofstock = new System.Windows.Forms.CheckBox();
            this.allfilter = new System.Windows.Forms.CheckBox();
            this.btn_area.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.stock_options.SuspendLayout();
            this.filterbtnarea.SuspendLayout();
            this.option_filters.SuspendLayout();
            this.SuspendLayout();
            // 
            // stocklist
            // 
            this.stocklist.AutoSize = true;
            this.stocklist.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.stocklist.Location = new System.Drawing.Point(218, 3);
            this.stocklist.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.stocklist.Name = "stocklist";
            this.stocklist.Size = new System.Drawing.Size(68, 27);
            this.stocklist.TabIndex = 3;
            this.stocklist.Text = "Stock list";
            this.stocklist.UseVisualStyleBackColor = true;
            this.stocklist.Click += new System.EventHandler(this.createfliterarea_Click);
            // 
            // button2
            // 
            this.button2.AutoSize = true;
            this.button2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button2.Location = new System.Drawing.Point(125, 3);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(85, 27);
            this.button2.TabIndex = 2;
            this.button2.Text = "Find Images";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button4
            // 
            this.button4.AutoSize = true;
            this.button4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button4.Location = new System.Drawing.Point(4, 3);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(113, 27);
            this.button4.TabIndex = 4;
            this.button4.Text = "Print Out of Stock";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btn_area
            // 
            this.btn_area.Controls.Add(this.button4);
            this.btn_area.Controls.Add(this.button2);
            this.btn_area.Controls.Add(this.stocklist);
            this.btn_area.Controls.Add(this.button5);
            this.btn_area.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_area.Location = new System.Drawing.Point(0, 32);
            this.btn_area.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_area.Name = "btn_area";
            this.btn_area.Size = new System.Drawing.Size(476, 51);
            this.btn_area.TabIndex = 5;
            // 
            // button5
            // 
            this.button5.AutoSize = true;
            this.button5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button5.Location = new System.Drawing.Point(294, 3);
            this.button5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(99, 27);
            this.button5.TabIndex = 5;
            this.button5.Text = "Connect to site";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.webBrowser1);
            this.flowLayoutPanel2.Controls.Add(this.stock_options);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Font = new System.Drawing.Font("Myriad Pro Black Cond", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 83);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(476, 925);
            this.flowLayoutPanel2.TabIndex = 6;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(4, 3);
            this.webBrowser1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.webBrowser1.MaximumSize = new System.Drawing.Size(492, 653);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(492, 131);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(492, 311);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Visible = false;
            // 
            // stock_options
            // 
            this.stock_options.AutoSize = true;
            this.stock_options.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.stock_options.Controls.Add(this.filterbtnarea);
            this.stock_options.Controls.Add(this.option_filters);
            this.stock_options.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stock_options.Location = new System.Drawing.Point(4, 320);
            this.stock_options.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.stock_options.MinimumSize = new System.Drawing.Size(492, 0);
            this.stock_options.Name = "stock_options";
            this.stock_options.Size = new System.Drawing.Size(492, 133);
            this.stock_options.TabIndex = 1;
            this.stock_options.Visible = false;
            // 
            // filterbtnarea
            // 
            this.filterbtnarea.AutoSize = true;
            this.filterbtnarea.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.filterbtnarea.Controls.Add(this.run_stock);
            this.filterbtnarea.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.filterbtnarea.Location = new System.Drawing.Point(0, 99);
            this.filterbtnarea.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.filterbtnarea.Name = "filterbtnarea";
            this.filterbtnarea.Size = new System.Drawing.Size(492, 34);
            this.filterbtnarea.TabIndex = 1;
            // 
            // run_stock
            // 
            this.run_stock.Location = new System.Drawing.Point(0, 0);
            this.run_stock.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.run_stock.Name = "run_stock";
            this.run_stock.Size = new System.Drawing.Size(468, 31);
            this.run_stock.TabIndex = 0;
            this.run_stock.Text = "Create Stock List";
            this.run_stock.UseVisualStyleBackColor = true;
            this.run_stock.Click += new System.EventHandler(this.start_pdf_output);
            // 
            // option_filters
            // 
            this.option_filters.AutoSize = true;
            this.option_filters.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.option_filters.Controls.Add(this.allfilter);
            this.option_filters.Controls.Add(this.outofstock);
            this.option_filters.Controls.Add(this.label2);
            this.option_filters.Controls.Add(this.label1);
            this.option_filters.Dock = System.Windows.Forms.DockStyle.Top;
            this.option_filters.Location = new System.Drawing.Point(0, 0);
            this.option_filters.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.option_filters.MaximumSize = new System.Drawing.Size(492, 13075);
            this.option_filters.MinimumSize = new System.Drawing.Size(492, 99);
            this.option_filters.Name = "option_filters";
            this.option_filters.Size = new System.Drawing.Size(492, 99);
            this.option_filters.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Myriad Pro Black Cond", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 36);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(566, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "Departments:";
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Myriad Pro Black Cond", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(492, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "FILTERS:";
            // 
            // outofstock
            // 
            this.outofstock.AutoSize = true;
            this.outofstock.Location = new System.Drawing.Point(59, 8);
            this.outofstock.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.outofstock.Name = "outofstock";
            this.outofstock.Size = new System.Drawing.Size(74, 17);
            this.outofstock.TabIndex = 0;
            this.outofstock.Text = "Out of Stock";
            this.outofstock.UseVisualStyleBackColor = true;
            // 
            // allfilter
            // 
            this.allfilter.AutoSize = true;
            this.allfilter.Checked = true;
            this.allfilter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.allfilter.Location = new System.Drawing.Point(96, 37);
            this.allfilter.Name = "allfilter";
            this.allfilter.Size = new System.Drawing.Size(64, 17);
            this.allfilter.TabIndex = 3;
            this.allfilter.Text = "Select All";
            this.allfilter.UseVisualStyleBackColor = true;
            this.allfilter.CheckedChanged += new System.EventHandler(this.allfilter_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 1015);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.btn_area);
            this.Font = new System.Drawing.Font("Myriad Pro Black Cond", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(0, 32, 0, 7);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "End of day";
            this.btn_area.ResumeLayout(false);
            this.btn_area.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.stock_options.ResumeLayout(false);
            this.stock_options.PerformLayout();
            this.filterbtnarea.ResumeLayout(false);
            this.option_filters.ResumeLayout(false);
            this.option_filters.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button stocklist;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.FlowLayoutPanel btn_area;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private Panel stock_options;
        private Button run_stock;
        private Panel option_filters;
        private CheckBox outofstock;
        private Panel filterbtnarea;
        private Label label2;
        private Label label1;
        private CheckBox allfilter;





    }
}

