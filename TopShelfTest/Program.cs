﻿using System;
using Nancy.Hosting.Self;
using Topshelf;

namespace TopShelfTest
{
    class Program
    {
        private const int DEFAULT_PORT = 3579;

        static void Main(string[] args)
        {
            int port;

            var newPort = Environment.GetEnvironmentVariable("PORT");
            if (!int.TryParse(newPort, out port)) port = DEFAULT_PORT;

            HostFactory.Run(x =>
            {
                x.UseLinuxIfAvailable();
                x.Service<NancyApp>(s =>
                {
                    s.ConstructUsing(name => new NancyApp("http://localhost:"+port));
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
            });
        }
    }

    class NancyApp
    {
        private readonly NancyHost _host;

        public NancyApp(string uri)
        {
            _host = new NancyHost(new Uri(uri));
        }

        public void Start()
        {
            _host.Start();
        }

        public void Stop()
        {
            _host.Stop();
        }
    }
}
