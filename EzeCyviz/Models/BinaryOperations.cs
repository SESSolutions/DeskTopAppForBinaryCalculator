using System;
using System.Collections.Generic;

namespace EzeCyviz.Models { 

    public class BinaryOperations
    {
        public static Func<string, string, string> FunctionPointer = null;
        public string FirstByte { get; set; }
        public string SecondByte { get; set; }
        public string OnscreenByte { get; set; }
        public string Operator { get; set; }
        
        public BinaryOperations()
        { }

        /// The function that gets bits into the two fields: firstly, the first field is filled then the second field.
        public void GetBitIntoByteField(string bit)
        {        
            try  
            {    
                string NewBit = bit;
                if (FunctionPointer != null)   
                {
                    SecondByte = SecondByte + NewBit;
                    OnscreenByte = SecondByte;
                }
                else
                {
                    FirstByte = FirstByte + NewBit;
                    OnscreenByte = FirstByte;
                }
            }
            catch { return; }
        }

        // It sets the operations
        public void ExecuteOperation(string operation)
        {
            try
            {

                if (operation == "=")
                {
                    ExecuteFunction();
                    Operator = operation;
                }

                if ((FunctionPointer != null) && (operation == "-" || operation == "+"))
                {
                    if (Operator == "=" && operation == "-" || Operator == "=" && operation == "+")
                    {
                        FunctionPointer = null;
                        SecondByte = "";
                    }
                    ExecuteFunction();
                    SecondByte = "";
                    Operator = operation;
                }

                FunctionPointer = SelectOperator(operation);
            }
            catch { return; }
        }

        // It performs the operation
        public void ExecuteFunction()
        {          
            if (FunctionPointer != null)
            {
             FirstByte = FunctionPointer(FirstByte, SecondByte);
             DisplayResult();
            }
        }

        // It returns a reference, pointer to a method depending on the operator that is pressed.
        private static Func<string, string, string> SelectOperator(string Operator)
        {
             
            switch (Operator)
            {
                case "+":
                    return (FirstByte, SecondByte) => BytesAddition(FirstByte, SecondByte);

                case "-":
                    return (FirstByte, SecondByte) => BytesSubtraction(FirstByte, SecondByte);
                case "=":
                    return FunctionPointer;

                default:
                    throw new ArgumentException("Invalid Operation");
            }
        }

        // It performs the bytes subtraction using one's complement
        private  static string BytesSubtraction(string FirstByte, string SecondByte) // static
        {
         
            try
            {

                char[] InvertedBits = new char[ (FirstByte.Length-1)];
                char Bits;            

                for (int i = (FirstByte.Length-1), j = (SecondByte.Length-1); i <= 0; i--, j--)
                {
                    

                    if (j >= 0)
                    {
                        Bits = SecondByte[j] == '0' ? '1' : '0';
                        InvertedBits[i] = Bits;
                    }

                    else if (j < 0)
                    {
                        InvertedBits[i] = '1';
                    }
                   
                }

                var Result = new string(InvertedBits);
                Result = BytesAddition(Result, "1");
                Result = BytesAdditionWithNoLastCarry(FirstByte, Result);
                return Result;
            }
            catch
            {
                return "";
            }
        }


        // It performs the bytes addition with the last carry
        private static string BytesAddition(string FirstByte, string SecondByte)
        {
        
                    if (FirstByte.Length < SecondByte.Length)
                    {
                        BytesAddition(SecondByte, FirstByte);
                    }

                    char[] BinarySum = new char[FirstByte.Length + 1];
                    bool carry = false;

                    for (int i = FirstByte.Length - 1, j = SecondByte.Length - 1, k = BinarySum.Length - 1; i >= 0; i--, j--, k--)
                    {
                        char BitFromFirstByte = FirstByte[i];
                        char BitFromSecondByte = j >= 0 ? SecondByte[j] : '0';

                        if (carry)
                        {
                            BinarySum[k] = BitFromFirstByte == BitFromSecondByte ? '1' : '0';
                            carry = BitFromFirstByte == '1' || BitFromSecondByte == '1';
                        }
                        else
                        {
                            BinarySum[k] = BitFromFirstByte == BitFromSecondByte ? '0' : '1';
                            carry = BitFromFirstByte == '1' && BitFromSecondByte == '1';
                        }
                    }
                    string ByteOutputString;
                    if (carry)
                    {
                        BinarySum[0] = '1';
                        ByteOutputString = new string(BinarySum);
                        return ByteOutputString;
                    }
                    ByteOutputString = new string(BinarySum, 1, BinarySum.Length - 1);
                    return ByteOutputString;
 
        }



        // It performs the bytes addition without the last carry
        private static string BytesAdditionWithNoLastCarry(string FirstByte, string SecondByte)
        {
            try
            {
                {
                    if (FirstByte.Length < SecondByte.Length)
                    {
                        BytesAddition(SecondByte, FirstByte);
                    }

                    char[] BinarySum = new char[FirstByte.Length + 1];
                    bool carry = false;

                    for (int i = FirstByte.Length - 1, j = SecondByte.Length - 1, k = BinarySum.Length - 1; i >= 0; i--, j--, k--)
                    {
                        char BitFromFirstByte = FirstByte[i];
                        char BitFromSecondByte = j >= 0 ? SecondByte[j] : '0';

                        if (carry)
                        {
                            BinarySum[k] = BitFromFirstByte == BitFromSecondByte ? '1' : '0';
                            carry = BitFromFirstByte == '1' || BitFromSecondByte == '1';
                        }
                        else
                        {
                            BinarySum[k] = BitFromFirstByte == BitFromSecondByte ? '0' : '1';
                            carry = BitFromFirstByte == '1' && BitFromSecondByte == '1';
                        }
                    }
                    string ByteOutputString;
                    ByteOutputString = new string(BinarySum, 1, BinarySum.Length - 1);
                    return ByteOutputString;
                }

            }
            catch { return ""; }
        }

        // It deplays the result
        void DisplayResult()
        {     
            OnscreenByte = string.Format("{0:00000000}", FirstByte);      
        }

    }
}
