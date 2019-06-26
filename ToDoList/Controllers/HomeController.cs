using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new TaskListModel();
            model.TaskList = new List<TaskModel>();

            return View(model);
        }

        //Function that adds the new task to the task list
        [HttpPost]
        public ActionResult AddTask(string taskListEncoded, string taskDescription)
        {
            TaskListModel taskListmodel = JsonConvert.DeserializeObject<TaskListModel>(taskListEncoded);

            //Initialise the new task
            TaskModel taskModel = new TaskModel();

            //Creating a random object
            Random random = new Random();

            //Add values to the new task class
            //Check if the task list has tasks
            if (taskListmodel.TaskList.Count != 0)
            {
                bool uniqueID = false;
                int randomID = 0;

                //Check if the random ID created is unique
                while (uniqueID == false)
                {
                    randomID = random.Next(1000);

                    int index = taskListmodel.TaskList.FindIndex(task => task.TaskID == randomID);
                    if (index >= 0)
                    {
                        uniqueID = false;
                    }
                    else
                    {
                        uniqueID = true;
                    }
                }

                taskModel.TaskID = randomID;
            }
            else
            {
                int randomID = random.Next(1000);
                taskModel.TaskID = randomID;
            }
            taskModel.Description = taskDescription;
            taskModel.Completed = false;

            //Add new task class to the task list
            taskListmodel.TaskList.Add(taskModel);

            return View("Index", taskListmodel);
        }

        //Function dealing with completed and uncompleted requests
        [HttpPost]
        public ActionResult TaskChanged(string taskListEncoded, string taskID, string completed)
        {
            TaskListModel taskListmodel = JsonConvert.DeserializeObject<TaskListModel>(taskListEncoded);

            //Loop through all the existing tasks
            foreach (var task in taskListmodel.TaskList)
            {
                //Check if the next task is the task selected
                if (task.TaskID.ToString() == taskID)
                {
                    if (completed != null)
                    {
                        if (completed.Equals("false"))
                        {
                            task.Completed = true;
                        }
                        else if(completed.Equals("true"))
                        {
                            task.Completed = false;
                        }
                        break;
                    }
                    else
                    {
                        task.Completed = false;
                    }
                }
            }

            return View("Index", taskListmodel);
        }

        //Function dealing with deleting tasks
        [HttpPost]
        public ActionResult TaskDeleted(string taskListEncoded, string taskID, string deleteButton)
        {
            TaskListModel taskListmodel = JsonConvert.DeserializeObject<TaskListModel>(taskListEncoded);

            //Loop through all the existing tasks
            foreach (var task in taskListmodel.TaskList)
            {
                //Check if the next task is the task selected
                if (task.TaskID.ToString() == taskID)
                {
                    if (deleteButton != null)
                    {
                        taskListmodel.TaskList.Remove(task);
                        break;
                    }
                }
            }

            return View("Index", taskListmodel);
        }


    }
}