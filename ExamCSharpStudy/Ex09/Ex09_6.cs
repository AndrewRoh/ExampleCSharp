namespace CSarpEx
{
    public class Engine { }
    public class V6Engine : Engine { }

    public abstract class Car
    {
        public abstract Engine GetEngine();
    }

    public class Audi : Car
    {
        public override Engine GetEngine()
        {
            return new V6Engine();
        }
    }

    class Ex09_6
    {
        static void Main09_6()
        {
            
            Audi audi = new();

            _ = audi.GetEngine();
        }
    }
}
