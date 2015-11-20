using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Android.Yorsh.Helpers;

namespace Android.Yorsh.Model
{
    public class TaskList : IEnumerable<TaskTable>
    {
        private readonly IList<TaskTable> _tasks;
        private readonly Dictionary<int, CategoryTable> _category;
        private readonly Dictionary<int, TaskTable> _taskDictionary;
        private readonly TaskEnumerator _enumerator;

        public TaskList(IList<TaskTable> tasks, IEnumerable<CategoryTable> categories)
        {
            _tasks = tasks;
            _taskDictionary = _tasks.ToDictionary(task => task.Id);
            _category = categories.ToDictionary(cat => cat.Id);
            _enumerator = new TaskEnumerator(this);
        }
        
        public IEnumerator<TaskTable> GetEnumerator()
        {
            return _enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _enumerator;
        }

        public CategoryTable GetCategory(int categoryId)
        {
            return _category[categoryId];
        }

        public void Clear()
        {
			_tasks.Shuffle ();
			_enumerator.Reset();
        }

        public bool IsBear(int categoryId)
        {
            return categoryId == 13;
        }

        public bool IsBear(TaskTable task)
        {
            return IsBear(task.CategoryId);
        }

        public int Count
        {
            get { return _tasks.Count; }
        }

        public TaskTable GetTask(int taskId)
        {
            return _taskDictionary[taskId];
        }

        public TaskTable this[int index]
        {
            get { return _tasks[index]; }
            set { _tasks[index] = value; }
        }

        #region TaskEnumerator

        public bool MoveNext()
        {
            return _enumerator.MoveNext();
        }

        public void Reset()
        {
            _enumerator.Reset();
        }

        public void SetCurrent(int position)
        {
            _enumerator.SetCurrent(position);
        }

        public TaskTable Current
        {
            get { return _enumerator.Current; }
        }

        #endregion
    }
    class TaskEnumerator : IEnumerator<TaskTable>
    {
        private readonly TaskList _taskList;
        private int _current;

        public TaskEnumerator(TaskList taskList)
        {
            _taskList = taskList;
            _current = -1;
        }

        public bool MoveNext()
        {
            if (_current >= _taskList.Count - 1) return false;
            _current++;
            return true;
        }

        public void Reset()
        {
            _current = -1;
        }

        public void SetCurrent(int position)
        {
            _current = position;
        }

        public TaskTable Current
        {
            get { return _taskList[_current]; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public void Dispose()
        {
        }
    }
}