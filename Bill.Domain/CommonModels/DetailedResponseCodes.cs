namespace Bill.Domain.CommonModels
{
    public static class DetailedResponseCodes
    {
        public static class ClientErrorCodes
        {
            public const string CreateClientFailed = "001";
            public const string NotFoundClient = "002";
            public const string UpdateClientFailed = "003";
            public const string DeleteClientFailed = "004";
        }

        public static class GeneralErrorCodes
        {
            public const string InternalServerError = "013";
            public const string HttpRequestFailedWithConnectionError = "017";
        }

        public static class SuccessCodes
        {
            public const string Success = "000";
        }
    }
}