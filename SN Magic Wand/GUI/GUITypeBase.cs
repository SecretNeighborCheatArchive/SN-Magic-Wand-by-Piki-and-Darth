using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace SecretNeighbour.UI
{
    internal abstract class GUITypeBase
    {
        internal GUITypeBase()
        {
            mets = GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Where(x => x.CustomAttributes.FirstOrDefault(z => z.AttributeType == typeof(DrawGUIAttribute)) != default && x.GetParameters().Length == 0).OrderByDescending(x => (int)x.CustomAttributes.FirstOrDefault(z => z.AttributeType == typeof(DrawGUIAttribute)).ConstructorArguments[0].Value);
        }

        internal void Draw()
        {
            foreach (var b in mets)
            {
                b.Invoke(this, null);
            }
        }

        private IOrderedEnumerable<MethodInfo> mets;
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class DrawGUIAttribute : Attribute
    {
        public DrawGUIAttribute(int layer)
        {

        }
    }
}
