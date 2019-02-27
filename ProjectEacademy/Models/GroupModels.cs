using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectEacademy.Models
{
    public class GroupModels
    {
    }
    public class StudentGroupViewModels
    {
        public string ClassName { get; set; }
        public string SubjectName { get; set; }
        public string TeacherName { get; set; }
    }
    public class TeacherGroupViewModels
    {
        public string ClassID { get; set; }
        public string ClassName { get; set; }
        public string SubjectName { get; set; }
    }
    public class TeacherGroupDetailViews
    {
        public string ClassID { get; set; }
        public ClassName Classname { get; set; }
        public List<StudentListModels> lstStudent { get; set; }
    }
    public class StudentListModels
    {
        public string StudentName { get; set; }
    }
    public class ClassName
    {
        public string Classname { get; set; }
    }
    public class CreateGroupModels
    {
        public string ClassID { get; set; }
        public string ClassName { get; set; }
        public string TeacherID { get; set; }
        public string SubjectName { get; set; }
        public string Color { get; set; }
    }
    public class JoinGroupModels
    {
        public string ClassID { get; set; }
        public string StudentID { get; set; }
        public string classdetail { get; set; }
    }
}