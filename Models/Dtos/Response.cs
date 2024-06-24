namespace Showdown_hub.Models.Dtos
{
    public class Response
    {
         public string ResponseCode { get; private set; }
        public string ResponseMessageText { get; private set; }

        // Define the ApiResponse constants public string ResponseCode { get; private set; }
        public string ResponseMessage { get; private set; }

        // Define the Response constants
        public static readonly Response SUCCESS = new Response("00", "Approved or completed successfully");
        public static readonly Response FAILED = new Response("99", "Unsuccessful transaction");
        public static readonly Response UNKNOWN = new Response("01", "Status unknown, please wait for settlement report");
        public static readonly Response INVALID_ACCOUNT = new Response("07", "Request processing in progress");
        public static readonly Response REQUEST_PROCESSING = new Response("09", "Status unknown, please wait for settlement report");
        public static readonly Response INVALID_TRANSACTION = new Response("12", "Invalid transaction");
        public static readonly Response INVALID_BANK_CODE = new Response("16", "Unknown Bank Code");
        public static readonly Response NO_ACTION = new Response("21", "No action is taken");
        public static readonly Response INSUFFICIENT_FUNDS = new Response("51", "No sufficient funds");
        public static readonly Response ACCOUNT_BLOCK = new Response("69", "Unsuccessful Account/Amount block");
        public static readonly Response LIMIT_EXCEEDED = new Response("61", "Transfer limit Exceeded");
        public static readonly Response DAILY_LIMIT_EXCEEDED = new Response("61", "Daily limit Exceeded");
        public static readonly Response BALANCE_LIMIT_EXCEEDED = new Response("61", "Deposit/Balance limit Exceeded");
        public static readonly Response ACCOUNT_UNBLOCK = new Response("70", "Unsuccessful Account/Amount unblock");
        public static readonly Response DUPLICATE_REQUEST = new Response("94", "Duplicate request or transaction");
        public static readonly Response SYSTEM_MALFUNCTION = new Response("96", "System malfunction");
        public static readonly Response TIMEOUT = new Response("97", "Timeout waiting for a response from destination");
        public static readonly Response ACCOUNT_ALREADY_EXISTS = new Response("98", "Account already exists");
        public static readonly Response ACCOUNT_ALREADY_IN_USE = new Response("105", "Account already in use");
        public static readonly Response INVALID_SOURCE_ACCOUNT = new Response("99", "Invalid Source Account");
        public static readonly Response INVALID_BENEFICIARY_ACCOUNT = new Response("100", "Invalid Beneficiary Account");
        public static readonly Response SOURCE_ACCOUNT_BLOCK = new Response("101", "Unsuccessful Source Account/Amount block");
        public static readonly Response BENEFICIARY_ACCOUNT_BLOCK = new Response("102", "Unsuccessful Beneficiary Account/Amount block");
        public static readonly Response DEBIT_OK = new Response("103", "Debit ok");
        public static readonly Response INVALID_DATE_FORMAT = new Response("104", "Invalid Date format");
        public static readonly Response CUSTOMER_ACCOUNT_RECORD_MISMATCH = new Response("105", "Customer account mismatch");
        public static readonly Response INVALID_OTP = new Response("106", "Invalid otp");
        public static readonly Response INVALID_ACTIVATION_KEY = new Response("107", "Invalid activation key");
        public static readonly Response PROFILE_ALREADY_ACTIVATED = new Response("108", "Profile already activated");
        public static readonly Response INVALID_PROFILE = new Response("109", "Invalid Profile");
        public static readonly Response DUPLICATE_RECORD = new Response("110", "Duplicate record");
        public static readonly Response INVALID_AUTHORITIES = new Response("402", "Invalid Authorities");

        private Response(string responseCode, string responseMessage)
        {
            ResponseCode = responseCode;
            ResponseMessage = responseMessage;
        }

    }
    }





    
    
