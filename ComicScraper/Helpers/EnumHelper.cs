﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace ComicScraper.Helpers
{
    /// <summary>
    /// From https://stackoverflow.com/a/27125869
    /// </summary>
    public static class EnumHelper
    {
        // This extension method is broken out so you can use a similar pattern with 
        // other MetaData elements in the future. This is your base method for each.
        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);

            // check if no attributes have been specified.
            if (((Array)attributes).Length > 0)
            {
                return (T)attributes[0];
            }
            else
            {
                return null;
            }
        }

        // This method creates a specific call to the above method, requesting the
        // Description MetaData attribute.
        public static string ToName(this Enum value)
        {
            var attribute = value.GetAttribute<DescriptionAttribute>();
            return attribute == null ? value.ToString() : attribute.Description;
        }

        /// <summary>
        /// Find the enum from the description attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="desc"></param>
        /// <returns></returns>
        public static T FromName<T>(this string desc) where T : struct
        {
            string attr;
            Boolean found = false;
            T result = (T)Enum.GetValues(typeof(T)).GetValue(0);

            foreach (object enumVal in Enum.GetValues(typeof(T)))
            {
                attr = ((Enum)enumVal).ToName();

                if (attr == desc)
                {
                    result = (T)enumVal;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                throw new Exception();
            }

            return result;
        }
    }
}
