using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using AntAlgoritm.ACS;
using AntAlgoritm.Graph;
using Point = AntAlgoritm.Graph.Point;


namespace AntColonySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            
            /*
            string fileName = @"D:\\AntAlgoritm-bugfix\\BPLAInput.txt";
            string outputFolder = @"D:\\AntAlgoritm-bugfix\\output";
            string separator = "----------";
            int fileCount = 0;

            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line == separator)
                    {
                        fileCount++;
                    }
                    else
                    {
                        string outputFileName = Path.Combine(outputFolder, $"output_{fileCount}.txt");
                        using (StreamWriter writer = new StreamWriter(outputFileName, true))
                        {
                            writer.WriteLine(line);
                        }
                    }
                }
            }
            */ //Divide file
            DivideFile();
            string folderPath = @"D:\\AntAlgoritm-bugfix\\output4";
            List<Point> points = new List<Point>();
            int LimitedDistance=0;
            string[] filePaths = Directory.GetFiles(folderPath);
            foreach (string filePath in filePaths)
            {
                string[] lines = File.ReadAllLines(filePath);
                int countbase = 0;
                for(int i=4; i<lines.Length; i++)
                {
                    if (lines[i]== "TheNearestNeighbour")
                    {
                        break;
                    }
                    countbase++;
                }
                LimitedDistance = Convert.ToInt32(lines[3])*10;
                Point start = new Point()
                {
                    Id = 1,
                    X = Convert.ToInt32(lines[1].Split(" ")[0]),
                    Y = Convert.ToInt32(lines[1].Split(" ")[1]),

                };
                Point end = new Point()
                {
                    Id = 2,
                    X = Convert.ToInt32(lines[2].Split(" ")[0]),
                    Y = Convert.ToInt32(lines[2].Split(" ")[1]),

                };
                points.Add(start);
                points.Add(end);
                for(int i=4;i<countbase+4;i++)
                {
                    Point point = new Point()
                    {
                        Id = i - 1,
                        X = Convert.ToInt32(lines[i].Split(" ")[0]),
                        Y = Convert.ToInt32(lines[i].Split(" ")[1]),
                    };
                points.Add(point);
       
                }
                Ant bestant = Calculate(points, LimitedDistance);
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("AntColony-Serhii");
                    writer.WriteLine(bestant.Distance / 10 * 60);
                    writer.WriteLine(bestant.VisitedNodes.Count-2);
                    writer.WriteLine("----------");

                }
                points.Clear();
                countbase = 0;
            }

            CombineAllTxt();
        }
        public static Ant Calculate(List<Point> points,int LimitedDistance)
        {
            Graph graph = new Graph(points, true);  // Create Graph


            Parameters parameters = new Parameters()  // Most parameters will be default. We only have to set T0 (initial pheromone level)
            {
                T0 = (1.0 / (graph.Dimensions * 21))
            };
            parameters.Show();

            Solver solver = new Solver(parameters, graph, LimitedDistance);
            List<Ant> results = solver.RunAcs(); // Run ACS
            for (int i = 0; i < results.Count; i++)
            {
                for (int j = 0; j < results[i].VisitedNodes.Count; j++)
                {
                    Console.Write(results[i].VisitedNodes[j].Id + " ");
                }
                Console.WriteLine(
                            "Current Global Best: " + results[i].Distance + "; Count object :" + results[i].VisitedNodes.Count + "; Time fly :" + results[i].Distance / 10 * 60);
            }
            
            Console.WriteLine("Time: " + solver.GetExecutionTime());
            return results.FirstOrDefault();
        }
        public static void CombineAllTxt()
        {
            string folderPath = @"D:\AntAlgoritm-bugfix\output4";
            string[] filePaths = Directory.GetFiles(folderPath, "*.txt");
            string outputFilePath = @"D:\AntAlgoritm-bugfix\Alltxt.txt";

            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                foreach (string filePath in filePaths)
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            writer.WriteLine(line);
                        }
                    }
                }
            }
        }
        public static void DivideFile()
        {
            string fileName = @"D:\\AntAlgoritm-bugfix\\rez6_100.txt";
            string outputFolder = @"D:\\AntAlgoritm-bugfix\\output4";
            string separator = "----------";
            int fileCount = 0;

            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line == separator)
                    {
                        fileCount++;
                    }
                    else
                    {
                        string outputFileName = Path.Combine(outputFolder, $"output_{fileCount}.txt");
                        using (StreamWriter writer = new StreamWriter(outputFileName, true))
                        {
                            writer.WriteLine(line);
                        }
                    }
                }
            }
        }
    }
}

