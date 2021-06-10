using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalc.Other
{
    public class TreeCollection<T>
    {
        private Node<T> root;

        public bool Add(T item)
        {
            var newNode = new Node<T>(item); 
            if(root == null)
            {
                root = newNode;
            } 
            else
            {
                root.Children.Add(newNode);
            }
            return true;
        }
        public bool Add(T item, T parent)
        {
            var parentNode =  FindNode(root, parent);
            if (parentNode == null)
                return false;
            parentNode.Children.Add(new Node<T>(item));
            return true;
        }

        public List<T> FindChildren(T item)
        {
            Node<T> parent = FindNode(root, item);
            if (parent == null)
                return null;
            return parent.Children.ConvertAll(new Converter<Node<T>, T>(ExtractItem));
        }

        public T getRoot()
        {
            return root.Item;
        }

        private T ExtractItem(Node<T> node)
        {
            return node.Item;
        }

        private Node<T> FindNode(Node<T> current, T item)
        {
            if (item.Equals(current.Item))
                return current;
            foreach(Node<T> node in current.Children)
                if (FindNode(node, item) != null)
                    return node;
            return null;
        }

        private class Node<A>
        {
            private readonly List<Node<A>> children;
            private A item;

            public Node(A item)
            {
                this.Item = item;
                children = new List<Node<A>>();
            }

            public A Item { get => item; set => item = value; }
            public List<Node<A>> Children { get => children;}
        }
    }
}
