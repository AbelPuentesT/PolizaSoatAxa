 namespace PolizaSOAT.Core.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException()
        {

        }
        public BusinessException(string message) : base(message)
        {

        }
        public int MyProperty { get; set; }
    }
}
