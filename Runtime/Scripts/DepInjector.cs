using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Linq;
#if UNIRX_PRESENT
using UniRx;
#endif

namespace RedMoon.Injector
{
    public static class DepInjector
    {
        public static void AddProvider(IProvider addProvider)
        {
            if (addProvider == null || providers.Contains(addProvider))
            {
                return;
            }

            foreach (IClient client in clients)
            {
                client.NewProviderAvailable(addProvider);
            }

            if (addProvider is IClient addClient)
            {
                AddClient(addClient);
            }

            providers.Add(addProvider);
            foreach (IClient client in clients)
            {
                client.NewProviderFullyInstalled(addProvider);
            }
        }

        public static void AddClient(IClient addClient)
        {
            if (addClient == null || clients.Contains(addClient))
            {
                return;
            }

            foreach (IProvider provider in providers)
            {
                addClient.NewProviderAvailable(provider);
            }

            clients.Add(addClient);
            foreach (IProvider provider in providers)
            {
                addClient.NewProviderFullyInstalled(provider);
            }
        }

        public static bool MapProvider<T, U>(IProvider provider, ref U val) where T : SingletonToProvider<U> where U : MonoBehaviour
        {
            if (provider is T ret)
            {
                val = ret.getRef();
                return true;
            }
            return false;
        }
        public static bool UnmapProvider<T, U>(IProvider provider, ref U val) where T : SingletonToProvider<U> where U : MonoBehaviour
        {
            if (provider is T)
            {
                val = default(U);
                return true;
            }
            return false;
        }
        public static bool MapProvider<T>(IProvider provider, ref T val) where T : IProvider
        {
            if (provider is T ret)
            {
                val = ret;
                return true;
            }
            return false;
        }
        public static bool UnmapProvider<T>(IProvider provider, ref T val) where T : IProvider
        {
            if (provider is T)
            {
                val = default(T);
                return true;
            }
            return false;
        }

        public static void Remove<T>(T o)
        {
            if (o is IProvider p)
            {
                RemoveProvider(p);
            }

            if (o is IClient c)
            {
                RemoveClient(c);
            }
        }
        private static void RemoveProvider(IProvider removeProvider)
        {
            if (!providers.Contains(removeProvider))
            {
                return;
            }
            foreach (IClient client in clients)
            {
                if (client != removeProvider)
                {
                    client.ProviderRemoved(removeProvider);
                }
            }
            providers.Remove(removeProvider);
        }
        private static void RemoveClient(IClient removeClient)
        {
            if (clients.Contains(removeClient))
            {
                clients.Remove(removeClient);
            }
        }

#if UNIRX_PRESENT
        public static bool MapProvider<T, U>(IProvider provider, ReactiveProperty<U> val) where T : SingletonToProvider<U> where U : MonoBehaviour
        {
            if (provider is T ret)
            {
                val.Value = ret.getRef();
                return true;
            }
            return false;
        }

        public static bool UnmapProvider<T, U>(IProvider provider, ReactiveProperty<U> val) where T : SingletonToProvider<U> where U : MonoBehaviour
        {
            if (provider is T)
            {
                val.Value = default(U);
                return true;
            }
            return false;
        }
        public static bool MapProvider<T>(IProvider provider, ReactiveProperty<T> val) where T : IProvider
        {
            if (provider is T ret)
            {
                val.Value = ret;
                return true;
            }
            return false;
        }

        public static bool UnmapProvider<T>(IProvider provider, ReactiveProperty<T> val) where T : IProvider
        {
            if (provider is T)
            {
                val.Value = default(T);
                return true;
            }
            return false;
        }
#endif
        public static T GetProvider<T>() where T : IProvider
        {
            return providers.OfType<T>().FirstOrDefault();
        }
        public static List<T> GetProviders<T>() where T: IProvider
        {
            return providers.OfType<T>().ToList();
        }
        public static T GetClient<T>() where T : IClient
        {
            return clients.OfType<T>().FirstOrDefault();
        }
        public static List<T> GetClients<T>() where T : IClient
        {
            return clients.OfType<T>().ToList();
        }

        private static volatile List<IProvider> providers = new List<IProvider>();
        private static volatile List<IClient> clients = new List<IClient>();
    }
}