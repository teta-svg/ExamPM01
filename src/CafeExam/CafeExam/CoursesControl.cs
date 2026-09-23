using System.IO;

namespace CafeExam
{
    public class CoursesControl
    {
        public Course[] Courses { get; set; }

        private void SwapCourses(int index1, int index2)
        {
            Course temp = Courses[index1];
            Courses[index1] = Courses[index2];
            Courses[index2] = temp;
        }

        public void SortCoursesByDurationAndPrice()
        {
            for (int i = 0; i < Courses.Length; i++)
            {
                for (int j = 0; j < Courses.Length - 1; j++)
                {
                    if (Courses[j] == null || Courses[j + 1] == null) continue;

                    if (Courses[j].Duration == Courses[j + 1].Duration)
                    {
                        if (Courses[j].Price > Courses[j + 1].Price)
                        {
                            SwapCourses(j, j + 1);
                        }
                    }
                    else if (Courses[j].Duration > Courses[j+1].Duration)
                    {
                        SwapCourses(j, j + 1);
                    }
                }
            }
        }

        public void SaveToFile()
        {
            string data = "Название;Цена;Время приготовления\n";

            foreach (var course in Courses)
            {
                if (course == null) continue;

                data += $"{course.Name};{course.Price};{course.Duration}\n";
            }

            using FileStream file = new("courses.txt", FileMode.Create);

            byte[] dataMass = System.Text.Encoding.Default.GetBytes(data);

            file.Write(dataMass, 0, dataMass.Length);
        }
    }
}
