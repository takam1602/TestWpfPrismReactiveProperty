namespace PrismSample.Lib.Models
{
    public class Model : IModel
    {
        public Model()
        { }

        public double Calculate(double operand)
        {
            return operand * operand;
        }
    }
}
