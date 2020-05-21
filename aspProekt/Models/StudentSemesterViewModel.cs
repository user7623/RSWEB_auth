using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspProekt.Models
{
    public class StudentSemesterViewModel
    {
        public List<Student> Students { get; set; }

        public SelectList Semesters { get; set; }
        public string StudentSemester { get; set; }
        public string SearchName { get; set; }
        public string SearchLastName { get; set; }
        public string SearchStudentID { get; set; }

    }
}
