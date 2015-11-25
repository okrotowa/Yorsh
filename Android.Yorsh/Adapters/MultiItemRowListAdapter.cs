using System;
using Android.Content;
using Android.Database;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Math = Java.Lang.Math;
using Object = Java.Lang.Object;

namespace ClickPizza.Android.Adapters
{
    public class MultiItemRowListAdapter :  Object, IWrapperListAdapter
    {
            private readonly IListAdapter _adapter;
            private readonly int _itemsPerRow;
            private readonly int _cellSpacing;
            private readonly bool _canClickAtRow;
            private readonly WeakReference<Context> _contextReference;
            private readonly LinearLayout.LayoutParams _itemLayoutParams;
            private readonly AbsListView.LayoutParams _rowLayoutParams;

        public MultiItemRowListAdapter(Context context, IListAdapter adapter, int itemsPerRow, int cellSpacing, bool canClickAtRow = false)
        {
            if (itemsPerRow <= 0) {
                throw new IllegalArgumentException("Number of items per row must be positive");
                }
                _contextReference = new WeakReference<Context>(context);
                _adapter = adapter;
                _itemsPerRow = itemsPerRow;
                _cellSpacing = cellSpacing;
            _canClickAtRow = canClickAtRow;

            _itemLayoutParams = new LinearLayout.LayoutParams(0, LinearLayout.LayoutParams.MatchParent);
                _itemLayoutParams.SetMargins(cellSpacing, cellSpacing, 0, 0);
                _itemLayoutParams.Weight = 1;
                _rowLayoutParams = new AbsListView.LayoutParams(AbsListView.LayoutParams.MatchParent, AbsListView.LayoutParams.WrapContent);
        }


        public bool IsEmpty
        {
            get
            {
                return  (_adapter == null || _adapter.IsEmpty);
            }
        }


        public int ItemsPerRow 
        {
            get
            {
                return _itemsPerRow;
            }
        }

        public int Count
        {
            get
            {
                return _adapter != null ? (int) Math.Ceil(1.0f*_adapter.Count/_itemsPerRow) : 0;
            }
        }

        public bool AreAllItemsEnabled()
        {
            return _adapter == null || _adapter.AreAllItemsEnabled();
        }

        public bool IsEnabled(int position)
        {
            if (!_canClickAtRow) return false;
            if (_adapter == null) return true;
            // the cell is enabled if at least one item is enabled
            var enabled = false;
            for (var i = 0; i < _itemsPerRow; ++i)
            {
                int p = position * _itemsPerRow + i;
                if (p < _adapter.Count)
                {
                    enabled |= _adapter.IsEnabled(p);
                }
            }
            return enabled;
        }


        public Object GetItem(int position) {
            if (_adapter == null) return null;
            var items = new Object[_itemsPerRow];
            for (var i = 0; i < _itemsPerRow; ++i) 
            {
                var p = position * _itemsPerRow + i;
                if (p < _adapter.Count)
                {
                    items[i] = _adapter.GetItem(p);
                }
            }
            return items;
        }

        public long GetItemId(int position) 
        {
            if (_adapter != null) {
                return position;
            }
            return -1;
        }

        public bool HasStableIds
        {
            get
            {
                if (_adapter != null)
                {
                    return _adapter.HasStableIds;
                }
                return false;
            }
        }

    public View GetView(int position, View convertView, ViewGroup parent)
    {
        Context c;
        if (!_contextReference.TryGetTarget(out c) || _adapter == null) return null;

        LinearLayout view = null;
        if ( !(convertView is LinearLayout) || !((int)convertView.Tag).Equals(_itemsPerRow)) 
        {
            // create a linear Layout
            view = new LinearLayout(c);
            view.SetPadding(0, 0, _cellSpacing, 0);
            view.LayoutParameters = _rowLayoutParams;
            view.Orientation = Orientation.Horizontal;
            view.BaselineAligned  = false;
            view.Tag = Integer.ValueOf(_itemsPerRow);
        } 
        else 
        {
            view = (LinearLayout) convertView;
        }

        for (var i = 0; i < _itemsPerRow; ++i) 
        {
            var subView = i < view.ChildCount ? view.GetChildAt(i) : null;
            var p = position * _itemsPerRow + i;

            var newView = subView;
            if (p < _adapter.Count) 
            {
            	if (subView is PlaceholderView){
            		view.RemoveView(subView);
            		subView = null;
            	}
                newView = _adapter.GetView(p, subView, view);
            } 
            else if (subView == null || !(subView is PlaceholderView)) 
            {
                newView = new PlaceholderView(c);
            }
            if (newView != subView || i >= view.ChildCount) {
                if (i < view.ChildCount) {
                    view.RemoveView(subView);
                }
                newView.LayoutParameters =_itemLayoutParams;
                view.AddView(newView, i);
            }
        }

        return view;
    }


    public int GetItemViewType(int position) 
    {
        if (_adapter != null) 
        {
            return _adapter.GetItemViewType(position);
        }
        return -1;
    }

        public int ViewTypeCount
        {
            get
            {
                return _adapter != null ? _adapter.ViewTypeCount : 1;
            }
        }

        public void RegisterDataSetObserver(DataSetObserver observer)
        {
            if (_adapter == null) return;
            _adapter.RegisterDataSetObserver(observer);
        }

        public void UnregisterDataSetObserver(DataSetObserver observer)
        {
            if (_adapter == null) return;
            _adapter.UnregisterDataSetObserver(observer);
        }

        public IListAdapter WrappedAdapter 
        {
            get { return _adapter; }
        }

        private class PlaceholderView : View 
        {
            public PlaceholderView(Context context) :base(context){
            }

        }
    }
}