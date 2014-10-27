using System;
using System.Linq;
using System.Reflection;

namespace MyApp
{
    public class AssemblyInfoHelper
    {
        private static Assembly _assembly;

        public AssemblyInfoHelper(Type type)
        {
            if (null == type)
            {
                throw new ArgumentNullException("type");
            }

            _assembly = type.GetTypeInfo().Assembly;
        }

        private static T CustomAttributes<T>() where T : Attribute
        {
            object[] customAttributes = _assembly.GetCustomAttributes(typeof (T)).ToArray();

            if ((customAttributes.Length > 0))
            {
                return ((T) customAttributes[0]);
            }

            throw new InvalidOperationException();
        }

        public string Title
        {
            get { return CustomAttributes<AssemblyTitleAttribute>().Title; }
        }

        public string Description
        {
            get { return CustomAttributes<AssemblyDescriptionAttribute>().Description; }
        }

        public string Company
        {
            get { return CustomAttributes<AssemblyCompanyAttribute>().Company; }
        }

        public string Product
        {
            get { return CustomAttributes<AssemblyProductAttribute>().Product; }
        }

        public string Copyright
        {
            get { return CustomAttributes<AssemblyCopyrightAttribute>().Copyright; }
        }

        public string Trademark
        {
            get { return CustomAttributes<AssemblyTrademarkAttribute>().Trademark; }
        }

        public string InformationalVersion
        {
            get { return CustomAttributes<AssemblyInformationalVersionAttribute>().InformationalVersion; }
        }

        public string AssemblyVersion
        {
            get { return _assembly.GetName().Version.ToString(); }
        }
    }
}