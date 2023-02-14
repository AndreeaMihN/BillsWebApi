namespace Bill.Domain.CommonModels
{
    public record ResponseRecord
    {
        public string ResponseCode { get; init; } = ResponseCodes.Success;
        public string DetailedResponseCode { get; init; } = "000"; //Success

        public ResponseRecord()
        {
        }

        public ResponseRecord(string responseCode)
        {
            ResponseCode = responseCode;
        }

        public ResponseRecord(string responseCode, string detailedResponseCode)
            : this(responseCode)
        {
            DetailedResponseCode = detailedResponseCode;
        }
    }
}