using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TDMPW_412_3P_PR02;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{

    public class TaskItem : INotifyPropertyChanged
    {
        private string title;
        private string completed;
       


        public string Title
        {
            get => title;
            set
            {
                    title = value;
                    OnPropertyChanged(Title);
                
            }
        }

        public string Completed
        {
            get => completed;
            set
            {
                
                    completed = value;
                    OnPropertyChanged(nameof(Completed));
                
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TaskItem(string title)
        {
            Title = title;
            Completed = "Pendiente";
        }
    }


    private ObservableCollection<TaskItem> tasks;
    private List<TaskItem> selectedTasks;



    public ObservableCollection<TaskItem> Tasks
    {
        get => tasks;
        set
        {
            tasks = value;
            OnPropertyChanged();
        }
    }

    public MainPage()
    {
        InitializeComponent();
        Tasks = new ObservableCollection<TaskItem>();
        selectedTasks = new List<TaskItem>();
        taskListView.BindingContext = this;
    }

    private void check_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        CheckBox checkBox = (CheckBox)sender;
        TaskItem task = (TaskItem)checkBox.BindingContext;
        



        if (checkBox.IsChecked)
        {
            task.Completed = "Completada";
            btnEliminar.IsEnabled = true;
            selectedTasks.Add(task);


        }
        else
        {
            task.Completed = "Pendiente";
            btnEliminar.IsEnabled= false;
            selectedTasks.Remove(task);


        }

        if (task != null)
        {
            btnEliminar.IsEnabled = true;

        }
        else
        {
            btnEliminar.IsEnabled = true;
        }

    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        
       
        foreach (TaskItem task in selectedTasks)
        {
            Tasks.Remove(task);
            
        }
        selectedTasks.Clear();
        btnEliminar.IsEnabled = false;


    }

    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
        string newTaskTitle = txtNewTask.Text;
        if (!string.IsNullOrEmpty(newTaskTitle))
        {
            TaskItem newTask = new TaskItem(newTaskTitle);
            Tasks.Add(newTask);
            txtNewTask.Text = string.Empty;
            txtAdvertencia.Text = "Agrega una nueva tarea:";
        }
        else
        {
            txtAdvertencia.Text = "El campo esta vacío";
        }
    }

    
}

