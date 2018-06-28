namespace Com.Foo
{

    // Import log4net classes.
    using log4net;
    using log4net.Config;


    public class MyApp
    {
        // Define a static logger variable so that it references the
        // Logger instance named "MyApp".
        private static readonly ILog log = LogManager.GetLogger(typeof(MyApp));

        static void Main1(string[] args)
        {
            var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
            // Set up a simple configuration that logs on the console.
            //BasicConfigurator.Configure(logRepository);
            XmlConfigurator.Configure(logRepository,new System.IO.FileInfo("log4net\\log.config"));
            log.Info("Entering application.");
            System.Console.WriteLine("test");
            Bar bar = new Bar();
            bar.DoIt();

            log.Info("Exiting application.");
        }
    }
    public class Bar
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Bar));

        public void DoIt()
        {
            log.Debug("Did it again!");
        }
    }
}