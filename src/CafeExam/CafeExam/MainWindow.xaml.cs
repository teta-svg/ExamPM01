using System.Windows;

namespace CafeExam
{
    public partial class MainWindow : Window
    {
        private int _count = -1;
        private int _index = 0;

        private readonly CoursesControl _coursesControl;

        public Course[] SourseCourses { get; set; }
        public Course[] SortedCourses { get; set; }


        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            _coursesControl = new();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(CourseCount.Text, out int count))
            {
                MessageBox.Show("Введите корректное количество блюд.");
                return;
            }

            if (count <= 0)
            {
                MessageBox.Show("Количество блюд должно быть больше нуля.");
                return;
            }

            if (_count > 0)
            {
                MessageBox.Show("Количество блюд уже было введено.");
                return;
            }

            _count = count;

            SourseCourses = new Course[_count];

            _coursesControl.Courses = new Course[_count];

            MessageBox.Show($"Количество блюд успешно установлено: {_count}");
        }

        private void AddCourseButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameBox.Text) || string.IsNullOrEmpty(PriceBox.Text) || string.IsNullOrEmpty(DurationBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            if (!double.TryParse(PriceBox.Text, out double price))
            {
                MessageBox.Show("Введите корректную цену.");
                return;
            }

            if (!int.TryParse(DurationBox.Text, out int duration))
            {
                MessageBox.Show("Введите корректное время приготовления.");
                return;
            }

            if (price <= 0)
            {
                MessageBox.Show("Цена должна быть больше 0.");
                return;
            }

            if (duration <= 0)
            {
                MessageBox.Show("Время приготовления должно быть больше 0.");
                return;
            }

            if (_index >= _count)
            {
                MessageBox.Show("Вы уже добавили все блюда.");
                return;
            }


            Course course = new()
            {
                Name = NameBox.Text,
                Price = price,
                Duration = duration
            };

            SourseCourses[_index] = course;
            _coursesControl.Courses[_index] = course;

            _index++;

            SourceArr.ItemsSource = null;
            SourceArr.ItemsSource = SourseCourses;

            NameBox.Clear();
            PriceBox.Clear();
            DurationBox.Clear();
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            _coursesControl.SortCoursesByDurationAndPrice();
            _coursesControl.SaveToFile();

            SortedCourses = _coursesControl.Courses;

            SortedArr.ItemsSource = null;
            SortedArr.ItemsSource = SortedCourses;

        }
    }
}