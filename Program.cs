// See https://aka.ms/new-console-template for more information
using System.Collections;
using System.Diagnostics.Contracts;
using Microsoft.VisualBasic;

Console.WriteLine("Hello, World!");

public class MyList<T> : ICollection<T>
{
    private MyNode<T> head = new MyNode<T>();
    private MyNode<T> last = new MyNode<T>();

    public int Count {get; private set;}

    public bool IsReadOnly => false;

    public void Add(T item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));
        head ??= new MyNode<T>();
        last??= head;

        if(last.IsFull())
            last = new MyNode<T>();

        last.Add(item);

        Count++;
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public bool Contains(T item)
    {
        MyNode<T> node = head;

        while(node != null)
        {
            if(node.Contains(item) >= 0)
                return true;

            node = node.GetNext();
        }
        
        return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        MyNode<T> node = head;

        for(int i = arrayIndex; i < arrayIndex + Count; i++)
        {
            for(int j = 0; j < node.GetSize(); j++)
            {
                array[i] = node.Get(j);
                i++;
            }
            node = node.GetNext();
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public bool Remove(T item)
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public class MyNode<T>
    {
        private T[] array = new T[32];
        private int size = 0;
        private MyNode<T> next = null;

        private MyNode<T> previous = null;

        public void Add(T value)
        {
            array[size] = value;
            size++;
        }

        public bool Remove(T item)
        {
            int index = Contains(item);

            if (index < 0)
                return false;


            for (int i = index; i < size; i++)
            {
                array[i] = array[i + 1];
            }
            size--;

            if(size == 0)
                previous.SetNext(next);

            return true;
        }

        public int Contains(T item)
        {
            for (int i = 0; i < size; i++)
            {
                if (array[i]?.Equals(item) ?? false)
                {
                    return i;
                }
            }

            return -1;
        }

        public MyNode<T> GetNext()
        {
            return next;
        }

        public void SetNext(MyNode<T> node)
        {
            next = node;
        }

        public void RemoveNext()
        {
            next = null;
        }

         public MyNode<T> GetPrevious()
        {
            return previous;
        }

        public void SetPrevious(MyNode<T> node)
        {
            previous = node;
        }

        public void RemovePrevious()
        {
            next = null;
        }

        public bool IsFull()
        {
            return size == 0;
        }

        public int GetSize()
        {
            return size;
        }

        public T Get(int index)
        {
            return array[index];
        }


    }
}

public class MyIterator<T> : IEnumerable<T>
{
    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}