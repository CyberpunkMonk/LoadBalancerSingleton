using System;
using System.Collections.Generic;
using System.Threading;

namespace LoadBalancing {
	/// <summary>
	/// The "Singleton" class
	/// </summary>
	class LoadBalancer {
		private static LoadBalancer _instance;
		private List<string> _servers = new List<string>();
		private Random _random = new Random();

		//Lock synchronization object
		private static object syncLock = new object();
		
		//Constructor (protected)
		protected LoadBalancer() {
			//List of all available servers
			_servers.Add("ServerI");
			_servers.Add("serverII");
			_servers.Add("ServerIII");
			_servers.Add("ServerIV");
			_servers.Add("ServerV");
		}

		public static LoadBalancer GetLoadBalancer() {
			//Support multithreaded applications through
			//"Double Checked Locking" patern which
			//(once the instance exists) avoids locking
			//each time the method is invoked.
			if (_instance == null) {
				lock (syncLock) {
					if (_instance == null) {
						_instance = new LoadBalancer();
					}
				}
			}
			return _instance;
		}

		public string Server {
			get { 
				int r = _random.Next(_servers.Count);
				return _servers[r].ToString();
			}
		}
	}
}