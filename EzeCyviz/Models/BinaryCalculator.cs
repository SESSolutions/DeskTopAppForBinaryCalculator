
namespace EzeCyviz.Models
{
    public class BinaryCalculator : BinaryOperations
    {

        public BinaryCalculator() { }

        public void Clear(string Clear)
        {     
            // d is CE - it resets everything to start
            if (Clear == "d")  
            {
                FirstByte = "";
                SecondByte = "";
                OnscreenByte = "0";
                FunctionPointer = null;
            }
           // it resets the last step and starts the last step again
            else if (Clear == "C" || FunctionPointer != null)
            {
                SecondByte = "";
                OnscreenByte = "0";
            }
            else if (Clear == "C" && FunctionPointer == null)
            {
                FirstByte = "";
                OnscreenByte = "0";
            }
        }

    }
}
