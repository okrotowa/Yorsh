using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Yorsh.Helpers;
using SQLite;
using Path = System.IO.Path;
using System.Linq;

namespace Android.Yorsh.Model
{
    public class Rep
    {
        private static Rep _instance;
        private readonly PlayerList _players;
        private TaskList _tasks;
        private IList<BonusTable> _bonuses;

        private Rep()
        {
            _players = new PlayerList();
        }

        public static Rep Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = new Rep();
                return _instance;
            }
        }

        public string DataBaseFile
        {
            get
            {
                var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                return Path.Combine(documentsPath, "Yoursh.db");
            }
        }

        public PlayerList Players 
		{ 
			get 
			{ 
				return _players; 
			} 
		}
        public TaskList Tasks { get { return _tasks; } }
        public IList<BonusTable> Bonuses { get { return _bonuses; } }

        public async Task TaskGenerateAsync(int count)
        {
            var connect = new SQLiteAsyncConnection(DataBaseFile);
			var taskList = await connect.Table<TaskTable>().Take(count).ToListAsync();
			taskList.Shuffle ();
            var categoryList = await connect.Table<CategoryTable>().ToListAsync();
            _tasks = new TaskList(taskList, categoryList);
        }

        public async Task BonusGenerateAsync(int count)
        {
            var connect = new SQLiteAsyncConnection(DataBaseFile);
            _bonuses = await connect.Table<BonusTable>().Take(count).ToListAsync();
            _bonuses.Shuffle();
        }

        public void Clear()
        {
            _players.Reset();
            _tasks.Clear();
        }
    }
}
