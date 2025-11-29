using System.Text;
using UnityEngine;

namespace Tea.Log
{
    public static class Print
    {
        /// <summary>
        /// 模仿lua的print功能
        /// </summary>
        public static string print(params System.Object[] objs)
        {
            var sb = new StringBuilder();
            foreach (var obj in objs)
            {
                if (sb.Length != 0) sb.Append("  ");
                sb.Append(obj);
            }

            var str = sb.ToString();
            Debug.Log(str);
            return str;
        }
    }
}
