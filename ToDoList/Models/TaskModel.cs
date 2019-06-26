using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList.Models
{
    public class TaskListModel
    {
        public List<TaskModel> TaskList { get; set; }
    }

    public class TaskModel
    {
        public int TaskID { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
    }
}