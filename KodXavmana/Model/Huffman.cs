using KodXavmana.Infastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KodXavmana.Model
{
    internal class Huffman
    {
        private List<Node> _nodes = new();
        private Node _root;
        private Dictionary<char, string> _codeTable = new();
        private Dictionary<char, double> _frequencyTable = new();
        public Dictionary<char, string> CodeTable { get => _codeTable; }

        private double _hx;
        public double Hx { get => _hx; }

        private double _redundancy;
        public double Redundancy { get => _redundancy; }



        public Huffman(Dictionary<char, double> frequencyTable)
        {
            _frequencyTable = frequencyTable;
            Node root = BuildHuffmanTree();
            TraverseTree(this, root, "");
            _hx = GetHx();
            _redundancy = 1 - _hx / GetHmax();
        }

        // Создаем список узлов для каждого символа и его частоты
        private static void BuildNodeList(Huffman huffman)
        {
            foreach (var item in huffman._frequencyTable)
            {
                huffman._nodes.Add(new Node { Character = item.Key, Frequency = item.Value });
            }
        }

        // Рекурсивно обходим дерево и строим таблицу кодов
        private static void TraverseTree(Huffman huffman, Node node, string code)
        {
            if (node.Left == null && node.Right == null)
            {
                huffman._codeTable[node.Character] = code;
            }
            else
            {
                TraverseTree(huffman, node.Left, code + "0");
                TraverseTree(huffman, node.Right, code + "1");
            }
        }


        // Строим дерево Хаффмана
        private Node BuildHuffmanTree()
        {
            BuildNodeList(this);

            while (_nodes.Count > 1)
            {
                Node left = _nodes[0];
                Node right = _nodes[1];
                Node parent = new Node { Character = ' ', Frequency = left.Frequency + right.Frequency, Left = left, Right = right };

                _nodes.RemoveRange(0, 2);
                _nodes.Add(parent);
            }
            return _nodes[0];
        }

        private double GetHx()
        {
            double sum = 0;
            foreach (var x in _frequencyTable)
            {
                sum += x.Value * Math.Log2(x.Value);
            }
            return -sum;
        }

        private double GetHmax()
        {
            return Math.Log2(_frequencyTable.Count);
        }
    }
}
