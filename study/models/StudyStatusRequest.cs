using System;

namespace  study
{
    public class StudyStatusRequest
    {
        public string Token { get; set; }
        public string CourseTitle { get; set; }
        public byte[] Pictures { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}