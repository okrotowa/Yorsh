using Java.Lang;
using SQLite;

namespace Android.Yorsh.Model
{
    public class TaskTable : Object
    {
        public TaskTable()
        {

        }
        public TaskTable(int categoryId, string taskName, int score)
        {
            CategoryId = categoryId;
            TaskName = taskName;
            Score = score;
        }
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int CategoryId { get; set; }
        public string TaskName { get; set; }
        public int Score { get; set; }

        public bool IsBear
        {
            get { return CategoryId % 13 == 0; }
        }

        public override string ToString()
        {
            return "TaskTable";
        }
    }
}
