using System;
using System.Collections.Generic;
using d01_ex01.Events;

namespace d01_ex01.Tasks
{
    public class Task
    {
        private string _title;
        private string _summary;
        private DateTime? _dueDate;
        private List<Event> _stateEvents;
        private TaskType _type;
        private TaskPirority _priority;

        public TaskState State
        {
            get { return _stateEvents[_stateEvents.Count - 1].State; }
        }

        public void Done()
        {
            _stateEvents.Add(new TaskDoneEvent());
        }
        
        public void Close()
        {
            _stateEvents.Add(new TaskWontDoEvent());
        }
        
        public Task(string title, string summary, DateTime? dueDate, TaskType type, TaskPirority priority = TaskPirority.Normal)
        {
            _title = title;
            _summary = summary;
            _type = type;
            _dueDate = dueDate;
            _priority = priority;
            _stateEvents = new List<Event>();
            _stateEvents.Add(new CreatedEvent());
        }

        public override string ToString()
        {
            string result = $"- {_title} \n" +
                            $"[{_type}] [{State}]\n" +
                            $"Priority: {_priority}";
            if (_dueDate != null)
                result += $", Due till {_dueDate:d}";
            if (!string.IsNullOrEmpty(_summary))
                result += $"\n{_summary}";

            return result;
        }
        
        public string GetTitle()
        {
           return _title;
        }
    }
}