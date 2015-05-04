using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSuiteExercise
{
    public class LruCache : iLruCache
    {
        private int _maximumSize;
        private LinkedList<KeyValuePair<object, object>> _list;
        private Dictionary<object, LinkedListNode<KeyValuePair<object, object>>> _index; 

        public LruCache(int maximumSize)
        {
            _maximumSize = maximumSize;
            _list = new LinkedList<KeyValuePair<object, object>>();
            _index = new Dictionary<object, LinkedListNode<KeyValuePair<object, object>>>();
        }

        public object Get(object key)
        {
            if (ItemExists(key))
                return GetValue(key);

            return null;
        }

        public void Put(object key, object value)
        {
            if (ItemExists(key))
            {
                UpdateItem(key, value);
            }
            else
            {
                if (IsCacheFull())
                    RemoveLeastUsedItem();

                InsertItem(key, value);
            }
        }

        public int GetMaxSize()
        {
            return _maximumSize;
        }

        /// <summary>
        /// Output the items in the cache in order of recent use.
        /// </summary>
        public override string ToString()
        {
            var result = new StringBuilder();
            foreach (var node in _list)
            {
                result.AppendLine(string.Format("Key: {0:s}, Value: {1:s}", node.Key.ToString(), node.Value.ToString()));
            }

            return result.ToString();
        }

        private Boolean ItemExists(object key)
        {
            return _index.ContainsKey(key);
        }

        private Boolean IsCacheFull()
        {
            return _list.Count >= _maximumSize;
        }

        private void RemoveLeastUsedItem()
        {
            var nodeToRemove = _list.Last;
            _list.Remove(nodeToRemove);
            _index.Remove(nodeToRemove.Value.Key);
        }

        private void InsertItem(object key, object value)
        {
            var nodeValue = new KeyValuePair<object, object>(key, value);

            //Insert node at the top of the list.
            var node = _list.AddFirst(nodeValue);

            _index[key] = node;
        }

        private void UpdateItem(object key, object value)
        {
            var node = _index[key];
            node.Value = new KeyValuePair<object, object>(key, value);

            //Move the node to the top of the list.
            _list.Remove(node);
            _list.AddFirst(node);

            _index[key] = node;
        }

        private object GetValue(object key)
        {
            var node = _index[key];

            //Move the node to the top of the list.
            _list.Remove(node);
            _list.AddFirst(node);

            return node.Value.Value;
        }
    }
}
