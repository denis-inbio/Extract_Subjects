using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extragere
{
    class Student
    {
        public String name;
        public List<int> group_subjects;
        public List<int> individual_subjects;

        public Student(string name)
        {
            this.name = name;
            this.group_subjects = new List<int>();
            this.individual_subjects = new List<int>();
        }
        public Student(string name, List<int> group_subjects)
        {
            this.name = name;
            this.group_subjects = group_subjects;
            this.individual_subjects = new List<int>();
        }

        public bool addGroupProject(int project)
        {
            if (!group_subjects.Contains(project))
            {
                group_subjects.Add(project);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool addIndividualProject(int project)
        {
            if (!individual_subjects.Contains(project))
            {
                individual_subjects.Add(project);
                return true;
            }
            else
            {
                return false;
            }
        }

        String toString() { 
            String result = this.name;


            result += "\nProiect(e) de grup: [";
            for (int i = 0; i < this.group_subjects.Count; i++) {
                result += this.group_subjects[i];

                if (i < this.group_subjects.Count - 1) {
                    result += ", ";
                }
            }
            result += "]";


            result += "\nProiect(e) individual(e): [";
            for (int i = 0; i < this.individual_subjects.Count; i++)
            {
                result += this.individual_subjects[i];

                if (i < this.individual_subjects.Count - 1)
                {
                    result += ", ";
                }
            }
            result += "]";


            return result;
        }

        String toString(List<string> subject_names)
        {
            String result = this.name;


            result += "\nProiect(e) de grup: [";
            for (int i = 0; i < this.group_subjects.Count; i++)
            {
                if (i < subject_names.Count) {
                    result += $"\"{subject_names[this.group_subjects[i]]}\"";
                }
                else {
                    result += "(Subject's name is not available)";
                }

                if (i < this.group_subjects.Count - 1)
                {
                    result += ", ";
                }
            }
            result += "]";


            result += "\nProiect(e) individual(e): [";
            for (int i = 0; i < this.individual_subjects.Count; i++)
            {
                if (i < subject_names.Count)
                {
                    result += $"\"{subject_names[this.individual_subjects[i]]}\"";
                }
                else
                {
                    result += "(Subject's name is not available)";
                }

                if (i < this.individual_subjects.Count - 1)
                {
                    result += ", ";
                }
            }
            result += "]";


            return result;
        }
    }

}
