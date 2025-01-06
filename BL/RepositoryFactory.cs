using LukomskiMajorkowski.KeyboardCatalog.INTERFACES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace LukomskiMajorkowski.KeyboardCatalog.BL
{
    public static class DAOFactory
    {
        public static IDAO CreateDAO()
        {
            string libraryName = ConfigurationManager.AppSettings["libraryFile"];

            Assembly assembly = Assembly.UnsafeLoadFrom(libraryName);
            Type typeToCreate = null;

            foreach (Type t in assembly.GetTypes())
            {
                if (t.IsAssignableTo(typeof(IDAO)))
                {
                    typeToCreate = t;
                    break;
                }
            }
            return Activator.CreateInstance(typeToCreate) as IDAO;
        }

    }
}
