using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;

namespace ConsoleApp1
{
    class Program
    {
        public static StringBuilder aml = new StringBuilder("");
        static void Main(string[] args)
        {
            Console.Write("Input path: ");
            string rootPath = Console.ReadLine();
            //string rootPath = @"F:\test\company";
            string treeRoot= Path.GetFileName(rootPath);
            Console.WriteLine(treeRoot);
            
            List<string> dirs = new List<string>(Directory.GetDirectories(rootPath, "*.*", SearchOption.AllDirectories));
            List<string> listData = new List<string>();
            //Console.WriteLine(string.Join("\n ", dirs));
           
            int size = dirs.Count();
            // child # root

            listData.Add(treeRoot + "#"); // root
            for (int i = 0; i < size; i++)
            {
                string child = Path.GetFileName(dirs[i]);
                string root = Path.GetFileName(Path.GetDirectoryName(dirs[i]));
                listData.Add(child + "#" + root);
                //Console.WriteLine(child + "#" + root);

            }

            

            int maxN = listData.Count();
            Boolean[] marked = new Boolean[maxN];
            IDictionary<string, List<string>> tree = new Dictionary<string, List<String>>();
            Boolean first = true;
            // root and list child
            foreach (string s in listData)
            {
                if (first)
                {
                    first = false;
                    continue;
                }
                string[] data = s.Split('#');
                string key = data[1];
                string value = data[0];
                if (tree.ContainsKey(key))
                {
                    tree[key].Add(value);
                }
                else
                {
                    List<string> list = new List<string>();
                    list.Add(value);
                    tree.Add(key, list);
                }
            }
            foreach (var s in tree)
            {
                Console.WriteLine(s.Key + ":" + "["+string.Join(",",s.Value)+"]");
            }

            
            Console.WriteLine("----------------------- TREE FOLDER--------------------------");
            BFS(tree, listData, "", treeRoot, aml,0);
            
            /*string path = @"C:\Users\This PC\Desktop\hieu\myTest0.txt";
           
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(aml.ToString());
                }
            */
         

        }
        static void BFS(IDictionary<string, List<string>> tree,List<string> listData, string root, string value, StringBuilder aml,int space)
        {
        
            string v = value;
            string k = v + "#" + root;
            int maxN = listData.Count();
            Boolean[] visited = new Boolean[maxN];
            for (int i = 0 ; i < maxN; i++) visited[i] = false;
            
            LinkedList<string> queue = new LinkedList<string>();
            //aml.Append(string.Format("<Item type =\"{0}\" action=\"{1}\" where=\"{2}\">\n","Folder","edit","[folder].name='"+v+"'"));
            //aml.Append(string.Format("<Item type =\"{0}\" action=\"{1}\"><name>{2}</name></Item>\n", "Folder", "add",v));
            //Console.WriteLine(v.PadLeft(0, '-'));
            int r = listData.IndexOf(k);
            listData[r] = "";
            
            visited[r] = true;
            queue.AddLast(v);
            while (queue.Any())
            {
                v = queue.First();
                //Console.WriteLine(v);
                if (r == 0) Console.WriteLine(v);
                else Console.WriteLine("".PadLeft(space,' ')+ "".PadLeft(1, '|')+"".PadLeft(1, '_') + v.PadLeft(space, '_'));
                queue.RemoveFirst();
                if (!tree.ContainsKey(v)) continue;
                List<string> vKey = tree[v];
                foreach (string val in vKey)
                {
                    
                    if (listData.IndexOf(val + "#" + v) == -1) continue;
                    if (!visited[listData.IndexOf(val+"#"+v)])
                    {
                        visited[listData.IndexOf(val + "#" + v)] = true;
                        //queue.AddLast(val);
                    }

                    
                    aml.Append(string.Format("<Item type =\"{0}\" action=\"{1}\" where=\"{2}\">\n", "Folder", "edit", "[folder].name='" + v + "'"));
                    aml.Append(string.Format("<Relationships><Item type=\"Relationship Folder\" action=\"add\"><related_id><Item type=\"{0}\" action=\"{1}\" maxRecords=\"1\">{2}</Item></related_id></Item></Relationships></Item>\n", "Folder", "get", "<name>" + val + "</name>"));
                    BFS(tree, listData, v, val, aml,space+2);


                }
            }
            
           


        }

    }
}




