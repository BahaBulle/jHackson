using System.Linq;
using Unity;

namespace jHackson
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();

            BootStrapper.Init(container);

            container.Resolve<Batch>().Run(args.ToList());
        }
    }
}