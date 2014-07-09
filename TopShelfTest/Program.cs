using System;
using Nancy.Hosting.Self;
using Topshelf;

namespace TopShelfTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var port = 5000;
            if (args.Length > 0)
            {
                int.TryParse(args[0], out port);
            }
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
