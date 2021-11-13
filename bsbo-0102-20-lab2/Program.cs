using System;
using System.Collections.Generic;
using System.Linq;

namespace bsbo_0102_20_lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Матрица инцендентости
            List<List<int>> gr = new List<List<int>>();
            //Вершина а             1   2  3  4  5  6  7  8  9 10 11 12 13 14
            gr.Add(new List<int>() {1, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            //В                    1   2  3  4  5  6  7  8  9  10 11 12 13 14
            gr.Add(new List<int>() { -1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 });
            //c                    1   2  3  4  5  6  7 8  9  10 11 12 13 14
            gr.Add(new List<int>() { 0, 0, 0, -1, -1, 1, 0, 0, 0, 1, 0, 0, 0, 0 });
            //d
            gr.Add(new List<int>() { 0, 0, 0, 0, 1, -1, -1, 1, 0, 1, 0, 0, 0, 0 });
            //e
            gr.Add(new List<int>() { 0, 1, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 });
            //f
            gr.Add(new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1, 1, -1, -1 });
            //g
            gr.Add(new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, -1, -1, 1, -1, 0, 0 });
            //h
            gr.Add(new List<int>() { 0, 0, 0, 0, 0, 0, 1, -1, 1, 0, 0, 0, 0, 0 });

            List<int> order = new List<int>();
            List<int> component = new List<int>();

            List<bool> used = Enumerable.Repeat(false, gr.Count).ToList();
            for (int i = 0; i < gr.Count; ++i)
                if (!used[i])
                    dfs1(i, ref order, ref used, gr);

            used = Enumerable.Repeat(false, gr.Count).ToList();

            List<List<int>> transMat = TransMat(gr);

            for (int i = 0; i < gr.Count; ++i)
            {
                int v = order[gr.Count - i - 1];

                if (!used[v])
                {
                    dfs2(v, ref component, ref used, transMat);
                    foreach (var l in component)
                        Console.Write(l + " ");
                    Console.WriteLine();

                    component.Clear();
                }
            }
        }

        static List<List<int>> TransMat(List<List<int>> graph)
        {
            List<List<int>> result_graph = new List<List<int>>();

            foreach (var node in graph)
            {
                List<int> new_node = new List<int>();

                for (int i = 0; i < node.Count; i++)
                {
                    new_node.Add(-node[i]);
                }

                result_graph.Add(new_node);
            }

            return result_graph;
        }

        /// <summary>
        /// Обход вглубину
        /// </summary>
        /// <param name="v"></param>
        /// <param name="order"></param>
        /// <param name="used"></param>
        /// <param name="graph"></param>
        static void dfs1(int v, ref List<int> order, ref List<bool> used, List<List<int>> graph)
        {
            used[v] = true;

            var temp = Ver(v, graph);
            foreach (var t in temp)
            {
               if(!used[t])
                    dfs1(t, ref order, ref used, graph);
            }

            order.Add(v);

        }
        /// <summary>
        /// Обход вглубину у транспонированной матрицы
        /// </summary>
        /// <param name="v"></param>
        /// <param name="component"></param>
        /// <param name="used"></param>
        /// <param name="graphTrans"></param>
        static void dfs2(int v, ref List<int> component, ref List<bool> used, List<List<int>> graphTrans)
        {
            used[v] = true;
            component.Add(v);

            foreach (var t in Ver(v, graphTrans))
            {
                if (!used[t])
                    dfs2(t, ref component, ref used, graphTrans);
            }
        }

        static List<int> Ver(int v, List<List<int>> graph)
        {
            List<int> result = new List<int>();
            //List<int> rebra = new List<int>();

            for(int i = 0; i < graph.Count; ++i)
            {
                if (i == v)
                    continue;
                for (int j = 0; j < graph[i].Count; ++j)
                {

                    if (graph[i][j] == -graph[v][j] && graph[i][j] != 0 && graph[v][j] == 1)
                        result.Add(i);
                }
            }


            return result;
        }
    }
}
