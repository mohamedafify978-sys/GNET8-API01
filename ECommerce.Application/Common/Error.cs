using System.Text.Json.Serialization;

namespace ECommerce.Application.Common
{
    public record Error(string Code,string Description, ErrorType ErrorType = ErrorType.Failure)
    {
        public static Error Failure(string code = " General.Failure ", string description = " General Failure Has Occurred ") => new (code, description, ErrorType.Failure); 


        public static Error Validation(string code = " General.Validation ", string description = " Validation Failure Has Occurred ") => new (code, description, ErrorType.Validation);
        public static Error NotFound(string code = " General.NotFound ", string description = " Not Found Failure Has Occurred ") => new (code, description, ErrorType.NotFound);

        public static Error Unauthorized(string code = " General.Unauthorized ", string description = " Unauthorized Failure Has Occurred ") => new (code, description, ErrorType.Unauthorized);
        public static Error Forbidden(string code = " General.Forbidden ", string description = " Forbidden Failure Has Occurred ") => new (code, description, ErrorType.Forbidden);
        public static Error Conflict(string code = " General.Conflict ", string description = " Conflict Failure Has Occurred ") => new (code, description, ErrorType.Conflict);
        public static Error InvalidCredentials(string code = " General.InvalidCredentials ", string description = " Invalid Credentials Failure Has Occurred ") => new (code, description, ErrorType.InvalidCredentials);





    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ErrorType
    {
        Failure=0,
        Validation=1,
        NotFound=2,
        Unauthorized=4,
        Forbidden=5,
        Conflict=3,
        InvalidCredentials=6,
    }
}