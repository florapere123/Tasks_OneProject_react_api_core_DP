using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Tasks_Project.Utils
{
    /// <summary>
    /// session extension class helps deserialize complex objects from string to list or to single object
    /// also has helper method in order to encapsulate the complexity of working with complexData 
    /// </summary>
    public static class SessionExtensions 
    {
        /// <summary>
        /// check if session exists by specified key
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns>boolean</returns>
        public static bool IsSessionExits(this ISession session, string key)
        {
           return session.Keys.Contains(key) ? true :false ;
            
        }
        /// <summary>
        ///gets single object from session by specified key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns>T -   object or priimitive type</returns>
        public static T GetObject<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(data);
         
        }
        /// <summary>
        /// gets list of objects from session by specified key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<T> GetListObject<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return new List<T>(); 
            }
            return JsonConvert.DeserializeObject<List<T>>(data);

        }
        /// <summary>
        /// sets the specified data by passed key as param
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetSession(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        /// <summary>
        /// removes current session - currently no use
        /// </summary>
        /// <param name="session"></param>
        /// <param name="sessionName"></param>
        public static void RemoveSessionItem(this ISession session, string sessionName)
        {
            session.Remove(sessionName);
        }
    }
}
