using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace FileSearcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string[] drives = System.IO.Directory.GetLogicalDrives();
            foreach (string drive in drives)
            {
                comboBox1.Items.Add(drive);
            }
            textBox1.Text = ".txt";
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread searchThread = new Thread(SearchFiles);
            searchThread.Start();
        }

        private void SearchFiles()
        {
            listView1.Items.Clear();
            string rootdir = comboBox1.Text + textBox3.Text;

            try
            {
                string[] files = Directory.GetFileSystemEntries(rootdir, "*" + textBox1.Text, SearchOption.AllDirectories);
                foreach (var item in files)
                {

                    FileInfo fileInfo = new FileInfo(item);
                    if (textBox2.Text != null)
                    {
                        if (!fileInfo.Name.Contains(textBox2.Text))
                        {
                            continue;
                        }
                    }

                    ListViewItem list_item = new ListViewItem(fileInfo.Name);
                    list_item.SubItems.Add(fileInfo.Directory.ToString());
                    list_item.SubItems.Add(fileInfo.Length.ToString());
                    list_item.SubItems.Add(fileInfo.LastWriteTime.ToString());
                    listView1.Items.Add(list_item);
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Access to the file was denied, try to change the directory");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label5.Text = comboBox1.Text;
            textBox3.Text = "";
        }

    }
    }

