using System.Collections;
using System.Data;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.VisualBasic;

List<int> list = [1, 2, 3, 4, 5, 6, 7, 8, 9];
List<int> list2 = [1, 2, 3];
List<int> list3 = [7, 8, 9];
var query = list.Take(3);
var query2 = list.Skip(3);
var query3 = list.Count();
var query4 = list.ToArray();
var query5 = list2.Zip(list2, list3);
foreach(var value in query5)
  Console.WriteLine(value);

Func<int, int, int> variavel = minhaFunc;

var valor = variavel(6,4);

int minhaFunc(int a, int b)
{
    return a+b;
}  


var funcao = (int x, int z) => x + z; // buscando o Func atrevez do var
var chamanVezes = (Action func, int n) =>
{
    int total = 0;
    for (int i = 0; i < n;)
      func();

    return total;
};

chamanVezes(
    () => Console.WriteLine("Olá mundo"),
    100
);

Func<int, Func<int>> func = n =>
{
    return () => n +5;
};

Func<int[], int, Func<int, int[]>> paginacao = (dados, tamanho) =>
{
    return(pagina) =>
    {
        int[] paginaDados = new int [tamanho];
        Array.Copy(
            dados, tamanho * pagina,
            paginaDados, 0,
            tamanho
        );
        return paginaDados;
    };
};
int[] valoreskk = [0,1,2];
var paginas = paginacao(valoreskk,4);
var dadosDaPagina2 = paginas(4);

public static class Enumerable
{
    public static IEnumerable<T> Skip<T>(
        this IEnumerable<T> collection, int count
    )
    {
        var it = collection.GetEnumerator();

        for (int i = 1; i > 0 && it.MoveNext(); i++)
        {
            if (i > count)
                yield return it.Current;
        }
    }

    public static int Count<T>(
        this IEnumerable<T> collection
    )

    {
        var it = collection.GetEnumerator();
        var count = 0;

        for (int i = 1; i > 0 && it.MoveNext(); i++)
        {
            count++;
        }

        return count;
    }

    public static IEnumerable<T> Take<T>(
        this IEnumerable<T> collection, int count)
    {
        var it = collection.GetEnumerator();
        for (int i = 0; i < count && it.MoveNext(); i++)
            yield return it.Current;
    }

     public static T[] ToArray<T>(
        this IEnumerable<T> collection)
    {
        T[] array = new T[collection.Count()];
        var it = collection.GetEnumerator();

        for (int i = 0; i < collection.Count() && it.MoveNext(); i++)
        {
            array.Append(it.Current);
        }
        return array;
    }

    public static IEnumerable<T> Append<T>(
        this IEnumerable<T> collection, T value
    )
    {
        var it = collection.GetEnumerator();
        

        for (int i = 0; i < collection.Count() && it.MoveNext(); i++) 
        {
            yield return it.Current;
        }

        yield return value;
    }

    public static IEnumerable<T> Preappend<T>(
        this IEnumerable<T> collection, T value
    )
    {
        var it = collection.GetEnumerator();
        
        yield return value;

        for (int i = 0; i < collection.Count() && it.MoveNext(); i++) 
        {
            yield return it.Current;
        }
    }

     public static T FirstOrDefault<T>(
        this IEnumerable<T> collection
    )
    {
        var it = collection.GetEnumerator();
        return it.MoveNext() ? it.Current : default;
    }

    public static IEnumerable<(T,R)> Zip<T,R>(
        this IEnumerable<T> collection,
        IEnumerable<R> other
    )
    {
        var it = collection.GetEnumerator();
        var it2 = other.GetEnumerator();

        var countCol = collection.Count();
        var countOther = other.Count();

        var smaller = countCol > countOther ? countOther : countCol;

        for (int i = 0; i < smaller && it.MoveNext() && it2.MoveNext(); i++)
        {
            yield return (it.Current, it2.Current);
        }
    }

    // public static IEnumerable<T[]> Chunk<T>(
    //     this IEnumerable<T> collection,
    //     int size
    // )
    // {

    // }

}

public class MyList<T> : ICollection<T>
{
    private MyNode<T> head = new MyNode<T>();
    private MyNode<T> last = new MyNode<T>();

    public int Count { get; private set; }

    public bool IsReadOnly => false;

    public void Add(T item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));
        head ??= new MyNode<T>();
        last ??= head;

        if (last.IsFull())
            last = new MyNode<T>();

        last.Add(item);

        Count++;
    }

    public void Clear()
    {
        head = null;
        last = null;
    }

    public bool Contains(T item)
    {
        MyNode<T> node = head;

        while (node != null)
        {
            if (node.Contains(item) >= 0)
                return true;

            node = node.GetNext();
        }

        return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        MyNode<T> node = head;

        //ajustar aqui
        for (int i = arrayIndex; i < arrayIndex + Count; i++)
        {
            for (int j = 0; j < node.GetSize(); j++)
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

            if (size == 0)
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