using System.Reflection;

namespace Notepad
{
    public partial class FormMain : Form
    {
        string current_filename = "";
        bool wasChanged = false;
        string sign;

        public FormMain()
        {
            InitializeComponent();
            this.FormClosing += FormMain_FormClosing;
        }

        private void âûõîäToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        bool SaveChanges()
        {
            if (!wasChanged)
                return true;

            DialogResult result = MessageBox.Show(
                "Õîòèòå ñîõğàíèòü èçìåíåíèÿ?",
                "Áûëè èçìåíåíèÿ â ôàéëå",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                ñîõğàíèòüToolStripMenuItem_Click(this, EventArgs.Empty);
                return !wasChanged;
            }

            return result != DialogResult.Cancel;
        }

        private void ñîçäàòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SaveChanges())
                return;

            textBoxField.Text = "";
            current_filename = "";
            wasChanged = false;
        }

        private void îòêğûòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SaveChanges())
                return;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(openFileDialog.FileName))
                {
                    textBoxField.Text = sr.ReadToEnd();
                }
                current_filename = openFileDialog.FileName;
                wasChanged = false;
            }
        }

        void SaveToFile(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);
            sw.WriteLine(textBoxField.Text);
            sw.WriteLine(sign);
            sw.Close();
            wasChanged = false;
        }

        private void ñîõğàíèòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(current_filename))
            {
                ñîõğàíèòüÊàêToolStripMenuItem_Click(sender, e);
                return;
            }
            SaveToFile(current_filename);
        }

        private void ñîõğàíèòüÊàêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveToFile(saveFileDialog.FileName);
                current_filename = saveFileDialog.FileName;
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("settings.txt");
            textBoxField.Font = new Font(textBoxField.Font.FontFamily, int.Parse(sr.ReadLine()));
            sign = "";
            while (!sr.EndOfStream)
            {
                sign += "\r\n" + sr.ReadLine();
            }
            sr.Close();

            fontDialog.Font = textBoxField.Font;
            colorDialog.Color = textBoxField.ForeColor;
            Clipboard.Clear();
        }

        private void textBoxField_TextChanged(object sender, EventArgs e)
        {
            wasChanged = true;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SaveChanges())
                e.Cancel = true;
        }

        private void øğèôòToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxField.Font = fontDialog.Font;
            }
        }

        private void öâåòToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxField.ForeColor = colorDialog.Color;
            }
        }

        private void âûäåëèòüÂñåToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxField.SelectAll();
        }

        private void êîïèğîâàòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxField.Copy();
        }

        private void âûğåçàòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxField.Cut();
        }

        private void âñòàâèòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxField.Paste();
        }

        private void íàñòğîéêèToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSettings frm = new FormSettings();
            frm.ShowDialog();
        }

        private void FormMain_Activated(object sender, EventArgs e)
        {

        }
    }
}
