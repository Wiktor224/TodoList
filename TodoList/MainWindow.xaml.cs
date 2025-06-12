using System.Collections.ObjectModel;

using System.Windows;

using TodoList.Models;

namespace TodoList
{
    public partial class MainWindow : Window
    {
        private readonly AppDbContext _context;
        public ObservableCollection<Task> Tasks { get; set; }

        public MainWindow(AppDbContext context)
        {
            InitializeComponent();
            _context = context;
            LoadTasks();
        }

        private void LoadTasks()
        {
            Tasks = new ObservableCollection<Task>(_context.Tasks.ToList());
            TaskList.ItemsSource = Tasks;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            var title = TaskInput.Text.Trim();
            if (!string.IsNullOrEmpty(title))
            {
                var newTask = new TaskItem { Title = title };
                _context.Tasks.Add(newTask);
                _context.SaveChanges();
                Tasks.Add(newTask);
                TaskInput.Clear();
            }
        }
    }
}
