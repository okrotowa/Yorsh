using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Android.Graphics;

namespace Android.Yorsh.Model
{
    public class PlayerList : IList<Player>
    {
        private readonly PlayerEnumerator _enumerator;
        private readonly IList<Player> _players;

        public PlayerList()
        {
            _enumerator = new PlayerEnumerator(this);
            _players = new List<Player>();
        }

        public bool IsAllPlay
        {
            get
            {
                return _players.All(t => t.IsPlay);
            }
        }

        public void Add(string name, Bitmap image, bool isPlay = false)
        {
            _players.Add(new Player(name, image, isPlay));
        }

        public int GetPosition(Player player)
        {
            return _players.Count(p => p.Score > player.Score);
        }

        public void Reset()
        {
            foreach (var player in _players)
            {
                player.IsPlay = false;
                player.Score = 0;
            }
            _enumerator.Reset();
        }

        #region Implemented elements

		public event EventHandler ItemRemoved;
		protected void OnItemRemoved()
		{
			var handler = ItemRemoved;
			if (handler != null) handler(this, EventArgs.Empty); 
		}

        public int Count
        {
            get { return _players.Count; }
        }

        public void Add(Player item)
        {
            _players.Add(item);
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Clear()
        {
            _players.Clear();
        }

        public bool Contains(Player item)
        {
            return _players.Contains(item);
        }

        public void CopyTo(Player[] array, int arrayIndex)
        {
            _players.CopyTo(array, arrayIndex);
        }


        public int IndexOf(Player player)
        {
            return _players.IndexOf(player);
        }

        public void Insert(int index, Player item)
        {
            _players.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _players.RemoveAt(index);
            OnItemRemoved();
        }

        public bool Remove(Player player)
        {
            OnItemRemoved();
            return _players.Remove(player);
        }

        public Player this[int index]
        {
            get { return _players[index]; }
            set { _players[index] = value; }
        }

        #endregion

        #region Enumerator

        public PlayerEnumerator Enumerator
        {
            get { return _enumerator; }
        }

        public IEnumerator<Player> GetEnumerator()
        {
            return _enumerator;
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _enumerator;
        }

        public void SetCurrent(int position)
        {
            _enumerator.SetCurrent(position);
        }

        public void SetCurrent(Player player)
        {
            _enumerator.SetCurrent(_players.IndexOf(player));
        }

        public bool MoveNext()
        {
            return _enumerator.MoveNext();
        }

        public Player Current
        {
            get { return _enumerator.Current; }
        }
        #endregion
    }

    #region Enumerator [class]

    public class PlayerEnumerator : IEnumerator<Player>
    {
        private readonly PlayerList _playersList;
        private int _current;

        public PlayerEnumerator(PlayerList playersList)
        {
            _playersList = playersList;
            _current = -1;
        }

        public bool MoveNext()
        {
            if (_current >= _playersList.Count - 1)
                _current = 0;
            else
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

        public Player Current
        {
            get { return _playersList[_current]; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public void Dispose()
        {
        }
    }

    #endregion
}