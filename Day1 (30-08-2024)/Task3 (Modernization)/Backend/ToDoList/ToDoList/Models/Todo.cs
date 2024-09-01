using System;

namespace TodoApp.Models
{
    public class Todo
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public DateTime TargetDate { get; set; }
        public bool Status { get; set; }

        // Default constructor
        public Todo()
        {
        }

        // Constructor with all properties
        public Todo(long id, string title, string username, string description, DateTime targetDate, bool isDone)
        {
            Id = id;
            Title = title;
            Username = username;
            Description = description;
            TargetDate = targetDate;
            Status = isDone;
        }

        // Constructor without ID (useful for creating new Todos)
        public Todo(string title, string username, string description, DateTime targetDate, bool isDone)
        {
            Title = title;
            Username = username;
            Description = description;
            TargetDate = targetDate;
            Status = isDone;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;
            Todo other = (Todo)obj;
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
