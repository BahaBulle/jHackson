using Newtonsoft.Json.Serialization;
using System;
using Unity;

namespace jHackson.Core.Json.ContractResolver
{
    public class UnityContractResolver : DefaultContractResolver
    {
        private readonly IUnityContainer _container;

        public UnityContractResolver(IUnityContainer container)
        {
            this._container = container;
        }

        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            // use Unity to create types that have been registered with it
            if (this._container.IsRegistered(objectType))
            {
                var contract = this.ResolveContact(objectType);
                contract.DefaultCreator = () => this._container.Resolve(objectType);

                return contract;
            }

            return base.CreateObjectContract(objectType);
        }

        private JsonObjectContract ResolveContact(Type objectType)
        {
            // attempt to create the contact from the resolved type
            //IComponentRegistration registration;

            //if (this._container.ComponentRegistry.TryGetRegistration(new TypedService(objectType), out registration))
            //{
            //    Type viewType = (registration.Activator as ReflectionActivator)?.LimitType;
            //    if (viewType != null)
            //    {
            //        return base.CreateObjectContract(viewType);
            //    }
            //}

            // fall back to using the registered type
            return base.CreateObjectContract(objectType);
        }
    }
}