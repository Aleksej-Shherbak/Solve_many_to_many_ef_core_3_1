namespace WebApplication2.Models
{
    public class ClassStudent
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int ClassId { get; set; }
        public Class Class { get; set; }
    }
}