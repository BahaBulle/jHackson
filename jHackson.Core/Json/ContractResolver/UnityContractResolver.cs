// <copyright file="UnityContractResolver.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Json.ContractResolver
{
    using System;
    using Newtonsoft.Json.Serialization;
    using Unity;

    public class UnityContractResolver : DefaultContractResolver
    {
        private readonly IUnityContainer container;

        public UnityContractResolver(IUnityContainer container)
        {
            this.container = container;
        }

        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            // use Unity to create types that have been registered with it
            if (this.container.IsRegistered(objectType))
            {
                var contract = this.ResolveContact(objectType);
                contract.DefaultCreator = () => this.container.Resolve(objectType);

                return contract;
            }

            return base.CreateObjectContract(objectType);
        }

        private JsonObjectContract ResolveContact(Type objectType)
        {
            // fall back to using the registered type
            return base.CreateObjectContract(objectType);
        }
    }
}