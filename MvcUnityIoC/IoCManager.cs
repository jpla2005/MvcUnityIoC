using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Practices.Unity;

namespace MvcUnityIoC
{
    public static class IoCManager
    {
        #region Fields

        private static IUnityContainer _container;

        #endregion

        #region Properties

        public static IUnityContainer Container
        {
            get { return _container ?? (_container = CreateContainer()); }
        }

        #endregion

        private static IUnityContainer CreateContainer()
        {
            var ass = System.Reflection.Assembly.GetAssembly(typeof(IoCManager));
            var path = new Uri(ass.CodeBase.Substring(0, ass.CodeBase.LastIndexOf("/", StringComparison.Ordinal))).AbsolutePath;
            var ext = new AssemblyNamingConventionScanExtension(path);

            var inp = new FileStream(Path.Combine(path, "ioc.config"), FileMode.Open);

            try
            {
                var root = XElement.Load(inp);
                var conventions = root.Elements("NamingConvention");
                foreach (var convention in conventions)
                {
                    var type = convention.Attribute("type");
                    if (type != null)
                    {
                        var convType = Type.GetType(type.Value);
                        if (convType != null)
                        {
                            var obj = Activator.CreateInstance(convType);
                            var conv = obj as NamingConvention;
                            if (conv != null)
                            {
                                var assemblies = convention.Elements("Assembly").Select(asm =>
                                    new Assembly
                                    {
                                        Name = asm.Attribute("name").Value,
                                        FullName = asm.Attribute("fullname").Value
                                    });

                                conv.Assemblies = assemblies;
                                ext.RegisterNamingConvention(conv);
                            }
                        }
                    }
                }

                var container = new UnityContainer();
                container.AddExtension(ext);

                return container;
            }
            catch (Exception)
            {
                throw new IoCException("IoC configuration corrupted");
            }
            finally
            {
                inp.Close();
            }
        }
    }
}