using System.Collections.ObjectModel;
using System.Windows;
using TodoList.Data;
using TodoList.Models;

namespace TodoList
{
    public partial class MainWindow : Window
    {
        private readonly AppDbContext _context;
        private readonly User _loggedInUser;
        public ObservableCollection<TaskItem> Tasks { get; set; }

        public MainWindow(AppDbContext context, User loggedInUser)
        {
            InitializeComponent();
            _context = context;
            _loggedInUser = loggedInUser;
            LoadTasks();
        }

        private void LoadTasks()
        {
            Tasks = new ObservableCollection<TaskItem>(_context.Tasks.Where(w => w.UserId == _loggedInUser.Id).ToList());
            TaskList.ItemsSource = Tasks;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            var title = TaskInput.Text.Trim();
            if (!string.IsNullOrEmpty(title))
            {
                var newTask = new TaskItem { Title = title, UserId = _loggedInUser.Id };
                _context.Tasks.Add(newTask);
                _context.SaveChanges();
                Tasks.Add(newTask);
                TaskInput.Clear();
            }
        }
        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as FrameworkElement)?.DataContext is TaskItem task)
            {
                var result = MessageBox.Show(
                    "Czy na pewno chcesz zapisać zmiany?",
                    "Potwierdzenie edycji",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    _context.Tasks.Update(task);
                    _context.SaveChanges();
                }
            }
        }


        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as FrameworkElement)?.DataContext is TaskItem task)
            {
                var result = MessageBox.Show(
                    "Czy na pewno chcesz usunąć to zadanie?",
                    "Potwierdzenie usunięcia",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {

                    _context.Tasks.Remove(task);
                    _context.SaveChanges();
                    Tasks.Remove(task);
                }
            }
        }
        

    }
}
