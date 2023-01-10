using PolizaSOAT.Core.CustomEntities;

namespace PolizaSOAT.Responses
{
    public class ApiResponse<T> 
    {
        public Metadata Meta { get; set; }
        public T Data { get; set; }
        public ApiResponse(T data, Metadata metadata)
        {
            Data = data;
            Meta = metadata;
        }
        public ApiResponse(T data)
        {
            Data=data;
        }
        public static ApiResponse<T> Create(T entity, Metadata metadata)
        {

            return new ApiResponse<T>(entity, metadata);
        }
        
        public static ApiResponse<T> Create(T entity)
        {

            return new ApiResponse<T>(entity);
        }


    }
}
