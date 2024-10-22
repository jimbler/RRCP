using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRCP
{
    public class Property
    {
        public string Text { get; set; }
        public int Index { get; set; }
        public int ParentIndex { get; set; }
        public int Depth { get; set; }
        public List<Property> Properties { get; set; } = new List<Property>();
        public string ToStringFormat
        {
            get
            {
                var stringFormat = "";
                for (var i = 0; i < Depth; i++)
                {
                    stringFormat += " ";
                }
                stringFormat += "- " + Text + "\n";
                return stringFormat;
            }
        }

        public static string FormatProps(List<Property> props)
        {
            var stringFormat = "";

            foreach (var prop in props)
            {
                stringFormat += prop.ToStringFormat;
                if (prop.Properties.Count() > 0)
                {
                    stringFormat += FormatProps(prop.Properties);
                }
            }

            return stringFormat;
        }

        public static List<Property> SortProps(List<Property> props)
        {
            var sortedProps = props.OrderBy(p => p.Text);
            foreach (var prop in sortedProps)
            {
                if (prop.Properties.Count() > 0)
                {
                    prop.Properties = prop.Properties.OrderBy(p => p.Text).ToList();
                }
            }
            return sortedProps.ToList();
        }

        public static List<Property> CreatePropertiesFromString(string input)
        {
            var rawProperties = new List<Property>();
            var structuredProperties = new List<Property>();

            //remove leadind trailing parens
            var formattedInput = input.Substring(1, input.Length - 2);

            //add comma-delimitter
            formattedInput = formattedInput.Replace("(", "(, ");
            var splits = formattedInput.Split(",");
            var parentIndex = 0;
            var depth = 0;

            //Parse structure of properties from string input
            for (var i = 0; i < splits.Length; i++)
            {
                //property without children
                if (!splits[i].Contains("("))
                {
                    rawProperties.Add(new Property()
                    {
                        Text = splits[i].Trim().Replace("(", "").Replace(")", ""),
                        Index = i,
                        ParentIndex = parentIndex > 0 ? parentIndex : 0,
                        Depth = depth
                    });
                }
                //property has children
                else
                {
                    rawProperties.Add(new Property()
                    {
                        Text = splits[i].Trim().Replace("(", "").Replace(")", ""),
                        Index = i,
                        ParentIndex = parentIndex > 0 ? parentIndex : 0,
                        Depth = depth
                    });
                    parentIndex = i;
                    depth++;
                }
                //close parent
                if (splits[i].Contains(")"))
                {
                    depth = depth - splits[i].Count(s => s == ')');
                    parentIndex = 0;
                }
            }

            //Create structured objects
            foreach (var prop in rawProperties)
            {
                if (prop.ParentIndex > 0)
                {
                    var parent = rawProperties.Where(p => p.Index == prop.ParentIndex).FirstOrDefault();
                    parent.Properties.Add(prop);
                }
                //dedupe
                structuredProperties = rawProperties.Where(p => p.ParentIndex == 0).ToList();
            }

            return structuredProperties;
        }
    }
}
