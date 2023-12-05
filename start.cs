    public class Program
    {
        [STAThread]
        public static void Main()
        {
            Console.WriteLine("start顺利启动！");

            Application.Run(RoundForm.GetInstance());
        }
    }