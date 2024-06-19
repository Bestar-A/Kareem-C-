using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question3
{
    internal class Program
    {
        class Task
        {
            public string TaskName { get; set; }
            public string Description { get; set; }

            public Task(string name, string desc)
            {
                TaskName = name;
                Description = desc;
            }
        }

        class TaskNode
        {
            public Task Task { get; set; }
            public TaskNode Next { get; set; }

            public TaskNode(Task task)
            {
                Task = task;
                Next = null;
            }
        }

        class TaskLinkedList
        {
            private TaskNode head;

            public void AddTask(Task task)
            {
                TaskNode newNode = new TaskNode(task);
                if (head == null)
                {
                    head = newNode;
                }
                else
                {
                    TaskNode current = head;
                    while (current.Next != null)
                    {
                        current = current.Next;
                    }
                    current.Next = newNode;
                }
            }

            public void RemoveTask(string taskName)
            {
                TaskNode current = head;
                TaskNode previous = null;

                while (current != null)
                {
                    if (current.Task.TaskName == taskName)
                    {
                        if (previous != null)
                        {
                            previous.Next = current.Next;
                        }
                        else
                        {
                            head = current.Next;
                        }
                        return;
                    }

                    previous = current;
                    current = current.Next;
                }

                Console.WriteLine("Task not found with Name: " + taskName);
            }

            public void DisplayTasks()
            {
                Console.WriteLine("Task List :");
                TaskNode current = head;
                while (current != null)
                {
                    Console.WriteLine("Name: " + current.Task.TaskName + ", Description: " + current.Task.Description);
                    current = current.Next;
                }
            }
        }

        static void Main()
        {
            // Create a sample project with a linked list of tasks
            TaskLinkedList projectTasks1 = new TaskLinkedList();

            // Adding tasks to the project
            projectTasks1.AddTask(new Task("Task 1", "Complete project analysis"));
            projectTasks1.AddTask(new Task("Task 2", "Develop project plan"));
            projectTasks1.AddTask(new Task("Task 3", "Implement project features"));
            projectTasks1.AddTask(new Task("Task 4", "Test project functionality"));

            // Displaying tasks
            projectTasks1.DisplayTasks();

            // Removing a task
            projectTasks1.RemoveTask("Task 2");

            // Displaying tasks after removal
            projectTasks1.DisplayTasks();
        }
    }
}
