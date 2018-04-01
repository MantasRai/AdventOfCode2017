using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventCode
{
    public class Day7
    {
        internal class TowerItem
        {
            public string Name { get; set; }
            public TowerItem Parent { get; set; }
            public List<TowerItem> Children { get; set; }
            public int Weight { get; set; }
            public int PrevWeight { get; set; }
        }

        public static void RunDay7()
        {
            var towerItems = ProcessInput();

            var rootItem = towerItems.Single(x => x.Parent == null);
            Console.WriteLine($"7.1 puzzle answer is: {rootItem.Name}");

            AssignDescWeights(rootItem);
            Console.WriteLine($"7.2 puzzle answer is: {FindUnbalancedNodeCorrectWeight(rootItem)}");
        }

        private static int AssignDescWeights(TowerItem towerItem)
        {
            if (towerItem.Children == null)
            {
                towerItem.PrevWeight = 0;
                return towerItem.Weight;
            }

            towerItem.PrevWeight = towerItem.Children.Sum(AssignDescWeights);
            return towerItem.PrevWeight + towerItem.Weight;
        }

        private static IEnumerable<TowerItem> ProcessInput()
        {
            var towerItems = new List<TowerItem>();

            var lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Inputs/input7.txt"));
            foreach (var line in lines)
            {
                var name = line.Split()[0];
                var towerItem = GetChild(name, towerItems);
                towerItem.Weight = int.Parse(Regex.Match(line, @"\((\w+)\)").Groups[1].Value);
                CreateRelationships(line, towerItem, towerItems);
            }

            return towerItems;
        }

        private static void CreateRelationships(string input, TowerItem towerItem, ICollection<TowerItem> towerItems)
        {
            var childNames = GetNames(input);
            if (childNames == null)
            {
                return;
            }
            foreach (var childName in childNames)
            {
                var childItem = GetChild(childName, towerItems);
                towerItem.Children.Add(childItem);
                childItem.Parent = towerItem;
            }
        }
       
        private static int FindUnbalancedNodeCorrectWeight(TowerItem towerItem)
        {
            if (towerItem.Children == null)
            {
                return -1;
            }

            foreach (var child in towerItem.Children)
            {
                var childResult = FindUnbalancedNodeCorrectWeight(child);
                if (childResult != -1)
                {
                    return childResult;
                }
            }

            if (towerItem.Children.Select(childItem => childItem.PrevWeight + childItem.Weight).Distinct().Count() > 1)
            {
                return BalancingTheWeight(towerItem);
            }

            return -1;
        }

        private static int BalancingTheWeight(TowerItem towerItem)
        {
            var unbalancedItem = towerItem.Children.GroupBy(x => x.PrevWeight + x.Weight).Select(g => new { Weight = g.Key, Count = g.Count() }).FirstOrDefault(x => x.Count == 1);
            var unbalanced = towerItem.Children.Single(x => unbalancedItem != null && x.Weight + x.PrevWeight == unbalancedItem.Weight);

            var correctedWeight = towerItem.Children[0] == unbalanced
                ? towerItem.Children[1].Weight + towerItem.Children[1].PrevWeight
                : towerItem.Children[0].Weight + towerItem.Children[0].PrevWeight;

            return correctedWeight - unbalanced.PrevWeight;
        }
        private static TowerItem GetChild(string name, ICollection<TowerItem> towerItems)
        {
            var towerItem = towerItems.SingleOrDefault(x => x.Name == name);
            if (towerItem != null) return towerItem;
            towerItem = new TowerItem { Name = name, Children = new List<TowerItem>() };
            towerItems.Add(towerItem);
            return towerItem;
        }

        private static string[] GetNames(string input)
        {
            var names = input.Split(new[] { "->" }, StringSplitOptions.RemoveEmptyEntries);
            return names.Length > 1 ? names[1].Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries) : null;
        }
    }
}