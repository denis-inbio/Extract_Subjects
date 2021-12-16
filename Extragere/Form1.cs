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

    class Student {
        public int id;
        public string family_name;
        public string personal_name;
        public bool[] group;   // this is read from file
        public bool[] individual;  // init false; the size of the list is dependent on how many subjects there are

        public Student(int id, string family_name, string personal_name, bool[] group, bool[] individual) {
            this.id = id;
            this.family_name = family_name;
            this.personal_name = personal_name;

            if (group.Length != individual.Length) { 
                // <error>: this shouldn't happen, but how are you going to give up this function when
                // it's a constructor ? it always returns a Student, unless it throws an exception,
                // which I will not do -> so, it could return an invalid state of Student; but how would
                // the type system know that ?
            }

            for (int index = 0; index < group.Length; index++) {
                this.group[index] = group[index];
            }

            for (int index = 0; index < individual.Length; index++) {
                this.individual[index] = individual[index];
            }
        }

        //public bool addGroupProject(int project)
        //{
        //    if (!group_subjects.Contains(project))
        //    {
        //        group_subjects.Add(project);
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public bool addIndividualProject(int project)
        //{
        //    if (!individual_subjects.Contains(project))
        //    {
        //        individual_subjects.Add(project);
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //string toString()
        //{
        //    string result = this.name;


        //    result += "\nProiect(e) de grup: [";
        //    for (int i = 0; i < this.group_subjects.Count; i++)
        //    {
        //        result += this.group_subjects[i];

        //        if (i < this.group_subjects.Count - 1)
        //        {
        //            result += ", ";
        //        }
        //    }
        //    result += "]";


        //    result += "\nProiect(e) individual(e): [";
        //    for (int i = 0; i < this.individual_subjects.Count; i++)
        //    {
        //        result += this.individual_subjects[i];

        //        if (i < this.individual_subjects.Count - 1)
        //        {
        //            result += ", ";
        //        }
        //    }
        //    result += "]";


        //    return result;
        //}

        //string toString(List<string> subject_names)
        //{
        //    string result = this.name;


        //    result += "\nProiect(e) de grup: [";
        //    for (int i = 0; i < this.group_subjects.Count; i++)
        //    {
        //        if (i < subject_names.Count)
        //        {
        //            result += $"\"{subject_names[this.group_subjects[i]]}\"";
        //        }
        //        else
        //        {
        //            result += "(Subject's name is not available)";
        //        }

        //        if (i < this.group_subjects.Count - 1)
        //        {
        //            result += ", ";
        //        }
        //    }
        //    result += "]";


        //    result += "\nProiect(e) individual(e): [";
        //    for (int i = 0; i < this.individual_subjects.Count; i++)
        //    {
        //        if (i < subject_names.Count)
        //        {
        //            result += $"\"{subject_names[this.individual_subjects[i]]}\"";
        //        }
        //        else
        //        {
        //            result += "(Subject's name is not available)";
        //        }

        //        if (i < this.individual_subjects.Count - 1)
        //        {
        //            result += ", ";
        //        }
        //    }
        //    result += "]";


        //    return result;
        //}
    
    };

//    class Student_allocatedSubjects -> I could further decompose this as I would in an SQL model

    class Classroom {
        // takes the subjects and all the needed files, processes them in the correct order
        // and then creates the (classroom of) students
        List<Tuple<int, string>> subjects;
        List<Student> students;

        public Classroom(string subjects_string, string students_string) {
            // parse subjects from string
            subjects = parseSubjects(subjects_string);


            // sample Student
            int id = 1;
            string family_name = "";
            string personal_name = "";
            bool[] group = { false, true, false, false, false, false, false, false, false, true};
            bool[] individual = { false, false, true, false, false, true, true, true, false, false};

            Student student = new Student(id, family_name, personal_name, group, individual);


            // parse student_names from string (might be empty, simply wrong, etc.) - it is not essential,
            // but the number of students IS essential (if nothing else)
            students = parseStudents(students_string);

        }


        List<Tuple<int, string>> parseSubjects(string subjects_string) {
        // <TODO> design resilience for premature termination
        // <TODO> design resilience for file ending in many line delimiters, or containing many contiguous
            // line delimiters anywhere in its content
        // <TODO> ..

            List<Tuple<int, string>> result = new List<Tuple<int, string>>();

            // the loop for a line-parsing state-machine
            for (int index = 0; index < subjects_string.Length;) {
                string id_string= "";
                string name = "";

                if (index < subjects_string.Length) {
                    // [ \t]*
                    while (index < subjects_string.Length && Util.isSpace(subjects_string[index]))
                    {
                        index++;
                    }

                    // [0-9]* -> a number is expected though, and prefered
                    while (index < subjects_string.Length && Util.isDecimal(subjects_string[index])) {
                        id_string += subjects_string[index];
                        index++;
                    }

                    int id;
                    if (id_string.Length > 0)
                        int.TryParse(id_string, out id);
                    else
                        id = 0;

                    // [ \t]*
                    while (index < subjects_string.Length && Util.isSpace(subjects_string[index]))
                    {
                        index++;
                    }

                    // [.-]? -> it might also just be separated by ' ' or by '-'
                    if (index < subjects_string.Length && Util.isSeparator_Id_Title(subjects_string[index])) {
                        index++;
                    }

                    // [ \t]*
                    while (index < subjects_string.Length && Util.isSpace(subjects_string[index])) {
                        index++;
                    }

                    // [{text_Romanian}]*
                    while (index < subjects_string.Length && Util.isText_Romanian(subjects_string[index])) {
                        name += subjects_string[index];
                        index++;
                    }

                    if(name.Length > 0) {
                        result.Add(new Tuple<int, string>(id, name));
                    }

                    // [\n]*
                    while (index < subjects_string.Length && Util.isLineDelimiter(subjects_string[index]))
                    {
                        index++;
                    }

                    // the state-machine for parsing a line terminates here
                }

            }

            return result;
        }
        string extractStudentName__intolerant(string line)
        {
            return "STUDENT";
        }
        string extractStudentName__tolerant(string line)
        {
            return "STUDENT";
        }
        List<Student> parseStudents(string students_string) {
            // (*?): a rather important aspect about this parser state-machine is what does it do when
            // it "doesn't match correctly"; does it consume (up to that point or to the end) or does it revert ?
            // does it try to tolerate and always try and return something, in worst case returning some defaults ?
            // does it return a ~ "state - fault / mismatch encountered" ? does it perhaps get stuck in an infinite
            // loop or does it eventually reach the end of the string, unconditionally ?
            // ..

            List<Student> result = new List<Student>();

            // the loop for a line-parsing state-machine
            for (int index = 0; index < students_string.Length;)
            {
                string id_string = "";
                string family_name = "";
                string personal_name = "";

                if (index < students_string.Length)
                {
                    // [ \t]*
                    while (index < students_string.Length && Util.isSpace(students_string[index]))
                    {
                        index++;
                    }

                    // [0-9]* -> a number is expected though, and prefered
                    while (index < students_string.Length && Util.isDecimal(students_string[index]))
                    {
                        id_string += students_string[index];
                        index++;
                    }

                    int id;
                    if (id_string.Length > 0)
                        int.TryParse(id_string, out id);
                    else
                        id = 0;

                    // [ \t]*
                    while (index < students_string.Length && Util.isSpace(students_string[index]))
                    {
                        index++;
                    }

                    // [.-]? -> it might also just be separated by ' ' or by '-'
                    if (index < students_string.Length && Util.isSeparator_Id_Title(students_string[index]))
                    {
                        index++;
                    }

                    // [ \t]*
                    while (index < students_string.Length && Util.isSpace(students_string[index]))
                    {
                        index++;
                    }

                    // ([{uppercase_letter_Romanian}] [{lowercase_letter_Romanian}]* [ \t]* [-]{0, 1})*
                    // -> 0 switches to personal name, 1 extends the family name
// (or use a) label: -> the thing is, this is hard to structure using just a while() and if(); ah, but there
    // is also break, so this can actually work fine
// (*): if(), while(), break - basis of operations for compositioning
// (*): for now, the way the state-machine reacts to pattern mismatch is to simply skip and attempt the next part
    // of the system of patterns (which are "expressed in the string", so to speak, as would be the case in a regex)
                    
                    while (index < students_string.Length && Util.isUpperCase_Romanian(students_string[index]))
                    {
                        if (index < students_string.Length && Util.isUpperCase_Romanian(students_string[index]))
                        {
                            family_name += students_string[index];
                            index++;


                            while (index < students_string.Length && Util.isLowerCase_Romanian(students_string[index]))
                            {
                                family_name += students_string[index];
                                index++;
                            }

                            // (*): take this more as a sample for ideas; it is not actually necessary here, in this case
                            //if (index < students_string.Length && Util.isSpace(students_string[index])) {
                            //    family_name += ' ';
                            //    index++;
                            //}

                            while (index < students_string.Length && Util.isSpace(students_string[index]))
                            {
                                index++;
                            }

                            // break depending on the -'s appearance or not
                            if (index < students_string.Length && Util.isSeparator_IntraFamilyName(students_string[index]))
                            {
                                family_name += students_string[index];
                                index++;
// (possibly handled - POST, but correctly ?) <TODO #family_name>: unhandled case -> there is a '-' but there is no uppercase 
    // romanian letter following, so it should actually remove the right-trailing '-'

                            }
                            else
                            {
                                break;
                            }

                            while (index < students_string.Length && Util.isSpace(students_string[index]))
                            {
                                index++;
                            }
                        }
                        else 
                        {
                            break;
                        }
                    }

                    while (family_name.EndsWith("-")) {
                        family_name = family_name.Substring(0, family_name.Length - 1);
                    }


                    // ([{uppercase_letter_Romanian}] [{lowercase_letter_Romanian}]+ [ \t]* [-]{0, 1} [ \t]*)*
                    // -> 0 switches to group projects, 1 extends the personal name

                    while (index < students_string.Length && Util.isUpperCase_Romanian(students_string[index]))
                    {
                        if (index < students_string.Length && Util.isUpperCase_Romanian(students_string[index]))
                        {
                            personal_name += students_string[index];
                            index++;


                            while (index < students_string.Length && Util.isLowerCase_Romanian(students_string[index]))
                            {
                                personal_name += students_string[index];
                                index++;
                            }

                            while (index < students_string.Length && Util.isSpace(students_string[index]))
                            {
                                index++;
                            }

                            // break depending on the -'s appearance or not
                            if (index < students_string.Length && Util.isSeparator_IntraFamilyName(students_string[index]))
                            {
// <TODO #personal_name>: same as <TODO #family_name>
                                personal_name += students_string[index];
                                index++;
                            }
                            else
                            {
                                break;
                            }

                            while (index < students_string.Length && Util.isSpace(students_string[index]))
                            {
                                index++;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    while (personal_name.EndsWith("-"))
                    {
                        personal_name = personal_name.Substring(0, personal_name.Length - 1);
                    }

                    // [ \t]*
                    while (index < students_string.Length && Util.isSpace(students_string[index]))
                    {
                        index++;
                    }

                    // [<][0-9]*[>] | [(][0-9]*[)] | [\[][0-9]*[\]] | [{][0-9]*[}] 




                    if (family_name.Length > 0 && personal_name.Length > 0)
                    {
                        //                        result.Add(new Tuple<int, string>(id, name));
                    }

                    // [\n]*
                    while (index < students_string.Length && Util.isLineDelimiter(students_string[index]))
                    {
                        index++;
                    }

                    // the state-machine for parsing a line terminates here
                }

            }

            return result;
        }
    };
    class ExtractionMethod {
        // the extraction method might extended the complexity that is composed and can be read from files, such
        // as the possibility to specify new constraints

        // an extraction method might involve multiple classrooms

        // maybe scheduling could also be done similarly, with a nice GUI

        Classroom classroom;
    };
}
