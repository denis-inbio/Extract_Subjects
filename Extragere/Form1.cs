using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Extragere
{
    public partial class Form1 : Form
    {
        private List<int> possible_subjects_indices = new List<int>();  // why do this ? because if specific
            // subjects are taken out due to being "too easy" for an individual project, or any such
            // modification that causes a discontinuous interval instead of simply [1, n], this list
            // would be needed
        private List<string> possible_subjects_names = new List<string>();

        // <TODO> the names list will be deleted eventually; the names will directly produce Student's and will
            // be sorted based on the name field in Student
        private List<String> student_names = new List<String>();
        private List<Student> students = new List<Student>();

        public Form1()
        {
            InitializeComponent();

            clearTerminal();
            clearOutput();

            setOutputFont();
        }


        // <TODO> change to reference the richTextBox
        int countPreviousLinesInRichTextBox(RichTextBox richTextBox) {
            if (richTextBox.Text.Length == 0) { return 0; }
            else {
                char[] separator = { '\n' };
                String[] lines = richTextBox.Text.Split(separator); // I think that this will take
                                                        // empty lines as well and put them in the array
                return lines.Length;
            }

            // <e.g.> empty string -> 0
            // <e.g.> "asdasd" -> 1
            // <e.g.> "asdasd\n" -> 1
            // <e.g.> "asdasd\n." -> 2
            // <e.g.> "asdasd\nasjdjhjh\n" -> 2
            // <e.g.> "asdasd\n\n\n" -> ?? (I'd prefer 1, but.. ?)
            // <design question>: lines of actual content or endlines ? - with special behaviour for empty string and
                // for the first line ?

        }
        void clearTerminal() {
            richTextBox1.Text = "";
        }
        void clearOutput() {
            richTextBox2.Text = "";
        }
        void setOutputFont() {
            Font font = new Font(FontFamily.GenericSansSerif, 19.0f);
            richTextBox2.Font = font;
        }
        void setFilenameTextBox(string filename)
        {
            textBox1.Text = filename;
        }

        
        void appendLineToRichTextBox(RichTextBox richTextBox, string line, int separation_lines = 0) {

            if (countPreviousLinesInRichTextBox(richTextBox) == 0) { ; } 
            else 
            {
                for (int i = line.Length - 1; i > 0 && line[i] == '\n'; i--)
                {
                    separation_lines--;
                }
                for (int i = 0; i < separation_lines; i++)
                {
                    richTextBox.Text += "\n";
                }
            }

            richTextBox.Text += line + "\n";
        }
        void appendLinesToRichTextBox(RichTextBox richTextBox, string[] lines, int separation_lines = 0) {

            for (int line_index = 0; line_index < lines.Length; line_index++)
            {
                string line = lines[line_index];

                if (countPreviousLinesInRichTextBox(richTextBox) == 0) {; }
                else
                {
                    for (int i = line.Length - 1; i > 0 && line[i] == '\n'; i--)
                    {
                        separation_lines--;
                    }
                    for (int i = 0; i < separation_lines; i++)
                    {
                        richTextBox.Text += "\n";
                    }
                }

                richTextBox.Text += line + "\n";
            }
        }





        bool isOnlyWhiteSpace(string text) {
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ' || text[i] != '\t')
                    return false;
            }

            return true;
        }


        string extractStudentName__intolerant(string line) {
            return "STUDENT";
        }
        string extractStudentName__tolerant(string line) {
            return "STUDENT";
        }


        // Browse File - let's the user find a file to open
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    List<String> list_of_student_names = new List<String>();
                    var filePath = openFileDialog1.FileName;


                    // check the filename is correct, else don't write anything in textBox and print the error to the terminal
                    setFilenameTextBox(filePath);
                    // if trying to read an empty file (because it was ulteriorly modified, again throw an error (maybe
                        // change the directory somehow ? I'd think that you should search up one layer then down one layer,
                        // then up two layers and down two layers, until the file is found, up to maybe 5 layers; or if a large
                        // number of files end up being iterated over, maybe also threshold it like that)

                    appendLineToRichTextBox(richTextBox1, $"File path: {filePath}", 1);

                }
                catch (System.Security.SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        // Read File - it actually looks in $textBox1 for the path
        private void button2_Click(object sender, EventArgs e)
        {
            // <TODO> first, check that the path is valid [not empty string, filesystem.exists(path), ..?]
            // <TODO> open the file and read it all into a list of strings (~ split by '\n')
            // <TODO> (!) define the "specification" and the "tolerance" for reading each type of input file
                // <e.g.> subject names
                // <e.g.> student names
                // <e.g.> each student's group subjects
                // <e.g.> global constraints for individual subjects (maybe filter out some subjects ?
                    // maybe the ones that are too easy ?)
                // <e.g.> ..

            clearOutput();
            List<String> list_of_student_names = new List<String>{"Student 1 eminent", "Student 2 less eminent",
                "Simply Student 3", "Complicated Student 4", "Overconfident Student 5"};

            
//            grp.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height);

            for (int i = 0; i < list_of_student_names.Count; i++)
            {
                //                grp.DrawString(list_of_student_names[i], font, Brushes.Black, 10.0f, 20.0f * i);
                richTextBox2.Text += list_of_student_names[i] + "\n";
            }

            richTextBox2.Text += "\n";
            list_of_student_names.Sort();

            for (int i = 0; i < list_of_student_names.Count; i++)
            {
                //                grp.DrawString(list_of_student_names[i], font, Brushes.Black, 10.0f, 20.0f * i);
                richTextBox2.Text += list_of_student_names[i] + "\n";
            }

            //using (System.IO.Stream str = openFileDialog1.OpenFile())
            //{
            //    var sr = new System.IO.StreamReader(openFileDialog1.FileName);

            //    String file_content = sr.ReadToEnd();
            //    char[] separators = new char[1];
            //    separators[0] = '\n';

            //    String[] student_names = file_content.Split(separators);


            //    for (int i = 0; i < student_names.Length; i++)
            //    {
            //        if (student_names[i].Length > 0 || isOnlyWhiteSpace(student_names[i]))
            //        {
            //            list_of_student_names.Add(student_names[i]);
            //        }
            //    }

            //    list_of_student_names.Sort();

            //    // cast the List<String> -> List<Student> (which means extracting the name, extracting the excluded subjects)

            //    // print to screen
            //    grp.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height);
            //    Font font = new Font(FontFamily.GenericSansSerif, 19.0f);

            //    for (int i = 0; i < list_of_student_names.Count; i++)
            //    {
            //        grp.DrawString(list_of_student_names[i], font, Brushes.Black, 10.0f, 20.0f * i);
            //    }
            //}
        }


        void prototype__setPossibleSubjectsIndices()
        {
            possible_subjects_indices.Clear();
            for (int i = 0; i < 10; i++) {
                possible_subjects_indices.Add(i + 1);
            }
        }
        void prototype__setPossibleSubjectsNames() {
            possible_subjects_names.Clear();

            List<String> temp_subjects_names = new List<string> {
                "1. Algoritmul Midpoint pentru linie",
                "2. Algoritmul Midpoint pentru linie folosind \"double step\"",
                "3. Algoritmul Midpoint pentru linie cu opțiune pentru grosimea liniei",
                "4. Desenarea unui triunghi folosind algoritmul Midpoint pentru liniile triunghiului",
                "5. Desenarea unui dreptunghi (pătratul fiind un caz particular) folosind algoritmul Midpoint pentru liniile dreptunghiului",
                "6. Desenarea unui poligon cu mai mult de 4 laturi folosind algoritmul Midpoint pentru liniile poligonului",
                "7. Desenarea unui cerc folosind simetria pe 8 căi",
                "8. Desenarea unei elipse folosind simetria pe 4 căi",
                "9. Algoritmul general de desenare a unei elipse, unde cercul este un caz particular, folosind simetria pe 4 căi",
                "10. Bla, ai trecut în baza prezenței. Felicitări, să ai noroc.. mda",
            };

            for (int i = 0; i < 10; i++)
            {
                possible_subjects_names.Add(temp_subjects_names[i]);
            }
        }
        void prototype__setStudentNames() {
            student_names.Clear();

            List<String> temp_student_names = new List<string> {
                "A a",
                "B b",
                "C c",
                "A b",
                "F g",
                "C e",
                "U i",
                "J k",
                "E s",
                "B a",
            };

            for (int i = 0; i < 10; i++)
            {
                student_names.Add(temp_student_names[i]);
            }
        }
        void prototype__setStudentGroupProjects_randomized() {
            List<int> temp_allowed_indices = new List<int>();

            for (int i = 0; i < possible_subjects_indices.Count; i++) {
                temp_allowed_indices.Add(possible_subjects_indices[i]);
            }


        }

        // Show Read Data
        private void button3_Click(object sender, EventArgs e)
        {

        }

        // Random choice - individual projects
        private void button4_Click(object sender, EventArgs e)
        {

        }

        
    }

}
