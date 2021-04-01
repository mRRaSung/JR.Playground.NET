namespace DesignPattern
{
    public class Singleton
    {
        private static Singleton instance;

        private static readonly object objLock = new object();

        private Singleton() {}

        public static Singleton getInstance1()
            => instance ?? new Singleton();

        public static Singleton getInstance2()
        {
            if(instance == null)
            {
                lock (objLock)
                {
                    if (instance == null)
                        instance = new Singleton();
                }
            }

            return instance;
        }
    }
}
