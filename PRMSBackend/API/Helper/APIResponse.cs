namespace ERPClean.Helper
{
    public class APIResponse<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public object Errors { get; set; }
        
        public static APIResponse<T> Success(T data, string message = "")
            => new APIResponse<T> { Status = true, Data = data, Message = message };

        public static APIResponse<T> Fail(string message, object errors = null)
            => new APIResponse<T> { Status = false, Message = message, Errors = errors };
    }
}



//{
//    "status": true,
//  "message": "User created successfully.",
//  "data": {
//        "id": 10,
//    "username": "ahmed99"
//  },
//  "errors": null
//}


//{
//    "status": false,
//  "message": "Validation failed.",
//  "data": null,
//  "errors": {
//        "email": "Email is already in use.",
//    "password": "Password must be at least 8 characters."
//  }
//}


