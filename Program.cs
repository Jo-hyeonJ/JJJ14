using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace JJJ14
{
    internal class Program
    {
        // ArrayList, List<T> : '배열 기반'의 리스트(=개수의 제한이 없는 자료구조)
        // 장점 : 접근이 빠르다. (자료 형태와 인덱스가 정해져 있기 때문) + 검색이 빠르다.
        // 단점 : 값의 추가, 삭제, 삽입이 용이하지 않다.

        // Linked-List
        // 장점 : 값의 추가, 삭제, 삽입이 편하다.
        // 단점 : 접근과 검색이 느림

        // 빅오표기법

        // 링크드 리스트
        // 싱글 링크드 리스트 - 단방향
        // 더블 링크드 리스트 - 양방향 (이전 노드도 알고 있음. 마지막 노드에서 검색을 시작하는 등이 가능)
        // 서클 링크드 리스트 - 첫 노드와 마지막 노드가 이어져있음
        class LinkedList<T> : IEnumerable<T>
        {
            class Node<T>
            {
                public T value;
                public Node<T> next;

                public Node(T value)
                {
                    this.value = value;
                    next = null;
                }
            }

            Node<T> root;          // 시작
            int count;          // 가진 요소의 수
            Node<T> last;

            public LinkedList() 
            {
                root = null;
                count = 0;
            }

            private Node<T> SearchNode(int index)
            {
                Node<T> search = root;
                for (int i = 0; i < index; i++)
                {
                    search = search.next;
                }
                return search;
            }

            public void Add(T value)
            {
                // 최초 대입
                if (count <= 0)
                {
                    root = new Node<T>(value);
                    last = root;
                    count++;
                    return;
                }
                else
                {
                    Node<T> newNode = new Node<T>(value);
                    last.next = newNode;
                    last = newNode;
                    count++;
                    return;
                }
            }


            public T this[int index]
            {
                get
                { 
                    Node<T> search = SearchNode(index);
                    return search.value;
                }
            }

            public void RemoveAt(int index)
            {
                // 범위를 넘어선 인덱스가 요청된 경우
                if (index >= count)
                {
                    Console.WriteLine($"{index}번째는 범위를 넘어섰습니다.");
                    return;
                }
                // 정상 기동
                if (index <= 0)
                {
                    root = root.next;
                    count--;
                    return;
                }
                Node<T> prevNode = SearchNode(index - 1);
                Node<T> curNode = prevNode.next;
                prevNode.next = curNode.next;
                count--;

                // index의 이전 노드의 다음이 없다면 마지막 노드이기 때문에 갱신해준다.
                if (prevNode.next == null)
                {
                    last = curNode;
                }
            }

            public bool Remove(string value)
            {
                Node<T> search = root;
                int index = 0;
                while(search != null)
                {
                    if(search.value.Equals(value))
                    {
                        RemoveAt(index);
                        return true;
                    }

                    // 다음 노드로 갱신
                    search = search.next;
                    index++;
                }

                return false;
            }

            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                Node<T> current = root;
                while (current != null)
                {
                    yield return current.value;
                    current = current.next;
                }
            }

            // 제네릭 타입의 열거자와 사실상 내용이 같기 떄문에
            // 제네릭 타입의 열거자를 객체로 생성 후 해당 객체를 활용한다.
            IEnumerator IEnumerable.GetEnumerator()
            {
                IEnumerator<T> ie = GetEnumerator();
                while (ie.MoveNext())
                    yield return ie.Current;
            }

            private IEnumerator<T> GetEnumerator()
            {
                Node<T> current = root;
                while (current != null)
                {
                    yield return current.value;
                    current = current.next;
                }
            }
        }

        class Item
        {
            public string name;
            public int level;
            public Item(string name, int level)
            {
                this.name = name;
                this.level = level;
            }

            public override string ToString()
            {
                return $"{name}, {level}";
            
            }



        }



        static void Main(string[] args)
        {

            Item item = new Item("빨간물약", 1);
            Console.WriteLine(item.name == "빨간물약");
            Console.WriteLine(item.name.GetHashCode());
            string name = "빨간물약";
            Console.WriteLine(name.GetHashCode());

            LinkedList<string> list = new LinkedList<string>();
            list.Add("C#");
            list.Add("visual basic");
            list.Add("Java-scripts");
            list.Add("Ruby");

            Console.WriteLine(list[2]);


            list.RemoveAt(0);
            Console.WriteLine(list[0]);
            Console.WriteLine("자바를 성공적으로 지웠는가?" + list.Remove("Java-scripts"));
            Console.WriteLine(list[1]);


            Console.WriteLine();
            LinkedList<Item> inventory = new LinkedList<Item>();
            inventory.Add(new Item("빨간물약", 1));
            inventory.Add(new Item("파란물약", 2));

            Console.WriteLine(inventory[0]);
            Console.WriteLine(inventory[1]);

            foreach(Item i in inventory)
            {  Console.WriteLine(i.ToString()); }    


        }
    }
}