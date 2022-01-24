using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedMoon.DepInjector
{
    public interface IClient
    {
        void NewProviderAvailable(IProvider newProvider);
        void ProviderRemoved(IProvider removeProvider);
        void NewProviderFullyInstalled(IProvider newProvider);
    }
}