using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public static class SessionExtensionMetods
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            string objectString = JsonConvert.SerializeObject(value);
            session.SetString(key, objectString);
        }
        public static void SetObjectList(this ISession session, string key, object value)
        {
            List<string> oldlist = new List<string>();
            var oldStr = session.GetStr(key);
            if (oldStr != null)
                oldlist = JsonConvert.DeserializeObject<List<string>>(oldStr);

            string objectString = JsonConvert.SerializeObject(value);
            oldlist.Add(objectString);
            string objectList = JsonConvert.SerializeObject(oldlist);
            session.SetString(key, objectList);
        }
        public static List<T> GetObjectList<T>(this ISession session, string key) where T : class
        {
            List<T> objList = new List<T>();
            string objectString = session.GetString(key);
            if (string.IsNullOrEmpty(objectString)) return null;

            var objStrList = JsonConvert.DeserializeObject<List<string>>(objectString);

            foreach (var item in objStrList)
            {
                var obj = JsonConvert.DeserializeObject<T>(item);
                objList.Add(obj);
            }
            return objList;
        }
        public static T GetObject<T>(this ISession session, string key) where T : class
        {
            string objectString = session.GetString(key);
            if (string.IsNullOrEmpty(objectString)) return null;
            T value = JsonConvert.DeserializeObject<T>(objectString);
            return value;
        }
        public static string GetStr(this ISession session, string key)
        {
            var a = session.GetString(key);
            return session.GetString(key);
        }
    }
}
